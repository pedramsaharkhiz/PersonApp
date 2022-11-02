using API.Dto;
using API.Models;
using AutoMapper;

namespace API.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
     public AutoMapperProfile()
     {
        CreateMap<Person,GetPersonDto>();
        CreateMap<AddPersonDto,Person>();
        
     }   
        
    }
}