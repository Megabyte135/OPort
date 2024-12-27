using API.Services.Resumes.DTOs;
using AutoMapper;
using Core.Entities.Portfolio;
using Core.Entities.Resumes;
using Core.Entities.Specialities;
using Services.Resumes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Resumes.Mappings
{
    public class ResumeProfile : Profile
    {
        public ResumeProfile()
        {
            CreateMap<KeyValuePair<string, string>, SocialMediaDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Key))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<SocialMediaDto, SocialMedia>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<SocialMedia, SocialMediaDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value));

            CreateMap<ResumeCreateDto, Resume>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => new Speciality { Name = src.Speciality }))
                .ForMember(dest => dest.WorkExperienceId, opt => opt.Ignore())
                .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => new WorkExperience
                {
                    Name = src.ExperienceName,
                    Employer = src.ExperienceEmployer,
                    Responsibilities = src.ExperienceResponsibilities,
                    Description = src.ExperienceDescription,
                    StartDate = src.ExperienceStartDate,
                    EndDate = src.ExperienceEndDate
                }))
                .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.SocialMedia
                    .Select(kvp => new SocialMedia
                    {
                        Name = kvp.Key,
                        Value = kvp.Value
                    }).ToList()));

            CreateMap<Resume, ResumeCreateDto>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => src.Speciality.Name))
                .ForMember(dest => dest.ExperienceName, opt => opt.MapFrom(src => src.WorkExperience.Name))
                .ForMember(dest => dest.ExperienceEmployer, opt => opt.MapFrom(src => src.WorkExperience.Employer))
                .ForMember(dest => dest.ExperienceResponsibilities, opt => opt.MapFrom(src => src.WorkExperience.Responsibilities))
                .ForMember(dest => dest.ExperienceDescription, opt => opt.MapFrom(src => src.WorkExperience.Description))
                .ForMember(dest => dest.ExperienceStartDate, opt => opt.MapFrom(src => src.WorkExperience.StartDate))
                .ForMember(dest => dest.ExperienceEndDate, opt => opt.MapFrom(src => src.WorkExperience.EndDate));

            CreateMap<ResumeDto, Resume>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => new Speciality { Name = src.Speciality }))
                .ForMember(dest => dest.WorkExperienceId, opt => opt.Ignore())
                .ForMember(dest => dest.WorkExperience, opt => opt.MapFrom(src => new WorkExperience
                {
                    Name = src.ExperienceName,
                    Employer = src.ExperienceEmployer,
                    Responsibilities = src.ExperienceResponsibilities,
                    Description = src.ExperienceDescription,
                    StartDate = src.ExperienceStartDate,
                    EndDate = src.ExperienceEndDate
                }))
                .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.SocialMedia
                    .Select(kvp => new SocialMedia
                    {
                        Name = kvp.Key,
                        Value = kvp.Value
                    }).ToList()));

            CreateMap<Resume, ResumeDto>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => src.Speciality.Name))
                .ForMember(dest => dest.ExperienceName, opt => opt.MapFrom(src => src.WorkExperience.Name))
                .ForMember(dest => dest.ExperienceEmployer, opt => opt.MapFrom(src => src.WorkExperience.Employer))
                .ForMember(dest => dest.ExperienceResponsibilities, opt => opt.MapFrom(src => src.WorkExperience.Responsibilities))
                .ForMember(dest => dest.ExperienceDescription, opt => opt.MapFrom(src => src.WorkExperience.Description))
                .ForMember(dest => dest.ExperienceStartDate, opt => opt.MapFrom(src => src.WorkExperience.StartDate))
                .ForMember(dest => dest.ExperienceEndDate, opt => opt.MapFrom(src => src.WorkExperience.EndDate))
                .ForMember(dest => dest.SocialMedia, opt => opt.MapFrom(src => src.SocialMedias
                    .Select(sm => new KeyValuePair<string, string>(sm.Name, sm.Value))
                    .ToList()));
        }
    }
}
