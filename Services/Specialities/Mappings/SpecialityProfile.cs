using AutoMapper;
using Services.Specialities.DTOs;
using Core.Entities.Specialities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specialities.Mappings
{
    public class SpecialityProfile : Profile
    {
        public SpecialityProfile()
        {
            CreateMap<Speciality, SpecialityDto>().ReverseMap();
            CreateMap<SpecialityCreateDto, Speciality>();
        }
    }
}
