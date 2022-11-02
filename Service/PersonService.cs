using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Service
{
    public class PersonService : IPersonService
    {
        public static  List<Person> lst = new List<Person>{
            new Person()
        };
        private readonly IMapper _mapper;

         public PersonService()
         {
            
         }

        public PersonService( IMapper mapper)
        {
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetPersonDto>>> AddPerson(AddPersonDto newPerson)
        {
            var serviceResponse=new ServiceResponse<List<GetPersonDto>>(); 
            Person person=_mapper.Map<Person>(newPerson);
            person.Id=lst.Max(x=>x.Id)+1;
            lst.Add(person);
            serviceResponse.Data = lst.Select(x => _mapper.Map<GetPersonDto>(x)).ToList();
            return serviceResponse;
        }
        

        public async Task<ServiceResponse<List<GetPersonDto>>> DeletePerson(int id)
        {
            var serviceResponse=new ServiceResponse<List<GetPersonDto>>();
            var person=lst.FirstOrDefault(x=>x.Id==id);
            lst.Remove(person);
            serviceResponse.Data=lst.Select(x=>_mapper.Map<GetPersonDto>(x)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPersonDto>>> GetAllPerson()
        {
            var serviceResponse=new ServiceResponse<List<GetPersonDto>>();
            serviceResponse.Data = lst.Select(x => _mapper.Map<GetPersonDto>(x)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPersonDto>> GetPersonById(int id)
        {
            var serviceResponse=new ServiceResponse<GetPersonDto>();
            var person=lst.FirstOrDefault(p=>p.Id==id);
            serviceResponse.Data=_mapper.Map<GetPersonDto>(person);
            return serviceResponse;

        }
    }
}
