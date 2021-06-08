using RestWithASPNETUdemy.Data.Converter.Implemenentations;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Repository;
using System;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Business.Implementations
{
    public class PersonServiceImplementation : IPersonService
    {

        private readonly IRepository<Model.PersonVO> _repository;

        private readonly PersonConverter _converter;

        public PersonServiceImplementation(IRepository<Model.PersonVO> repository)
        {
            _repository = repository;
            _converter = new Converter();
        }

        public List<Data.VO.PersonVO> FindAll()
        {
            return _repository.FindAll();
        }

        public Data.VO.PersonVO FindByID(long id)
        {
            return _repository.FindByID(id);
        }

        public Data.VO.PersonVO Create(Data.VO.PersonVO person)
        {
            return _repository.Create(person);
        }

        public Data.VO.PersonVO Update(Data.VO.PersonVO person)
        {
            return _repository.Update(person);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
