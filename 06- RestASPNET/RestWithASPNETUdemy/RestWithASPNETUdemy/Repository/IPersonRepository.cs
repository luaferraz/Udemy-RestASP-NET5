using RestWithASPNETUdemy.Model;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Repository
{
    public interface IPersonRepository : IPersonRepository<Person>
    {
        public Person Disable(long id);

        List<Person> FindByName(string firstName, string lastName);
    }
}
