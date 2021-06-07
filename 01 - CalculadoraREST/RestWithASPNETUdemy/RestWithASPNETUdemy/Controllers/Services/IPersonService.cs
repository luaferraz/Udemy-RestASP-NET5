using RestWithASPNETUdemy.Controllers.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Controllers.Services
{
    public interface IPersonService
    {
        Person Create(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
        object Create(PersonController person);
        object Update(PersonController person);
    }
}
