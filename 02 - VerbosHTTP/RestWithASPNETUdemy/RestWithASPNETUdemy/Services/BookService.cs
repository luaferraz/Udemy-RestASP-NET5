using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Services
{
    public interface BookService
    {

        Book Add(Book book);
        Book FindByID(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);

    }
}
