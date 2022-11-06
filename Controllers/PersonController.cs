using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using API.Interface;
using API.Models;
using API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[Controller]")]
    [Authorize]

    public class PersonController : ControllerBase
    {

        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            this._personService = personService;
        }


        [HttpPost("AddPerson")]
        public async Task<ServiceResponse<List<GetPersonDto>>> PostPerson(AddPersonDto newPerson)
        {

            return (await _personService.AddPerson(newPerson));
        }
        [AllowAnonymous]
        [HttpGet("GetAllPerson")]
        public async Task<ServiceResponse<List<GetPersonDto>>>GetAllPerson(){
            return (await _personService.GetAllPerson());
        }
        [HttpGet("GetPersonById/{id}")]
        public async Task<ActionResult<ServiceResponse<GetPersonDto>>>GetPersonById(int id){
            var response=await _personService.GetPersonById(id);
            if(response.Data==null)return NotFound("Person not found!");
            return(await _personService.GetPersonById(id));
        }
        [HttpDelete("DeletePerson/{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetPersonDto>>>>DeletePerson(int id){
            var response=await _personService.GetPersonById(id);
            if(response.Data==null)return NotFound("Person not found!");
            return(await _personService.DeletePerson(id));
            
            
            
        }
    }
}
