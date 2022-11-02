using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dto;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Interface
{
    public interface IPersonService
    {
        Task<ServiceResponse<List<GetPersonDto>>>GetAllPerson();
        Task<ServiceResponse<GetPersonDto>>GetPersonById(int id);
        Task<ServiceResponse<List<GetPersonDto>>>AddPerson(AddPersonDto person);
        Task<ServiceResponse<List<GetPersonDto>>>DeletePerson(int id);
         
    }
}