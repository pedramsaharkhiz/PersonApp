using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Dto;
using API.Interface;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    public class PersonService : IPersonService
    {
        public static List<Person> lst = new List<Person> { new Person() };
        private readonly IMapper _mapper;

        private readonly DataContext _context;

        public PersonService(IMapper mapper, DataContext context)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetPersonDto>>> AddPerson(AddPersonDto newPerson)
        {
            var serviceResponse = new ServiceResponse<List<GetPersonDto>>();
            Person person = _mapper.Map<Person>(newPerson);
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            serviceResponse.Data =await _context.Persons.Select(x => _mapper.Map<GetPersonDto>(x)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPersonDto>>> DeletePerson(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetPersonDto>>();
            var person = await _context.Persons.FirstOrDefaultAsync(x => x.Id == id);
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            serviceResponse.Data =await _context.Persons.Select(x => _mapper.Map<GetPersonDto>(x)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetPersonDto>>> GetAllPerson()
        {
            var serviceResponse = new ServiceResponse<List<GetPersonDto>>();
            var dbPerson=await _context.Persons.ToListAsync();
            serviceResponse.Data = dbPerson.Select(x => _mapper.Map<GetPersonDto>(x)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetPersonDto>> GetPersonById(int id)
        {
            var serviceResponse = new ServiceResponse<GetPersonDto>();
            var person =await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
            serviceResponse.Data = _mapper.Map<GetPersonDto>(person);
            return serviceResponse;
        }
    }
}
