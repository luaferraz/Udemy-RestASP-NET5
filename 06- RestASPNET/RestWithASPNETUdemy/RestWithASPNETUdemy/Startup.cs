using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestWithASPNETUdemy.Model.Context;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Business.Implementations;
using RestWithASPNETUdemy.Repository;
using Serilog;
using System;
using System.Collections.Generic;
using RestWithASPNETUdemy.Repository.Generic;
using Microsoft.Net.Http.Headers;
using RestWithASPNETUdemy.Hypermedia.Filters;
using RestWithASPNETUdemy.Hypermedia.Enricher;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Rewrite;

namespace RestWithASPNETUdemy
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //CORS
            services.AddCors(options => options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddControllers();

            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));
            
            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            //Habilitando o uso de XML e JSON na api
            services.AddMvc(options =>
            {
                options.RespectBrowserAcceptHeader = true;

                options.FormatterMappings.SetMediaTypeMappingForFormat("json",
                    MediaTypeHeaderValue.Parse("application/json"));
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml",
                    MediaTypeHeaderValue.Parse("application/xml"));
            })
            .AddXmlSerializerFormatters();

            //HATEOAS
            var filterOptions = new HyperMediaFilterOptions();
            filterOptions.ContentResponseEnricherList.Add(new PersonEnricher());
            filterOptions.ContentResponseEnricherList.Add(new BookEnricher());

            services.AddSingleton(filterOptions);

            //Versioning API
            services.AddApiVersioning();

            services.AddSwaggerGen(c =>{
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "REST API's From 0 To Azure with ASP.NET Core 5 and Docker",
                        Version = "v1",
                        Description = "API RestFul developed in course 'REST API's From 0 To Azure with ASP.NET Core 5 and Docker'",
                        Contact = new OpenApiContact
                        {
                            Name = "Lua",
                            Url = new Uri("https://github.com/luaferraz")
                        }
                    });
            });

            //Dependency Injection
            services.AddScoped<IPersonService, PersonServiceImplementation>();
            services.AddScoped<IBookService, BookServiceImplementation>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // o cors sempre tem que ficar depois de UseHttpsRedirection/UseRouting e antes de
            //UseEndPoints, caso contr�rio n�o vai funcionar direito;
            app.UseCors();

            //Sweagger start
            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", 
                    "REST API's From 0 To Azure with ASP.NET Core 5 and Docker - v1");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("DefaultApi", "{controller=values}/{id?}");
            });
        }
        //Evolve (database)
        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw;
            }
        }
    }
}
