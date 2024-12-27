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
            CreateMap<ResumeCreateDto, Resume>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => new Speciality { Name = src.Speciality }))
                .ForMember(dest => dest.ExperienceId, opt => opt.Ignore())
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => new WorkExperience
                {
                    Name = src.ExperienceName,
                    Employer = src.ExperienceEmployer,
                    Responsibilities = src.ExperienceResponsibilities,
                    Description = src.ExperienceDescription,
                    StartDate = src.ExperienceStartDate,
                    EndDate = src.ExperienceEndDate
                }))
                .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.SocialMedia.Select(sm => new SocialMedia
                {
                    Name = sm.Key,
                    Value = sm.Value
                }).ToList()));

            CreateMap<Resume, ResumeCreateDto>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => src.Speciality.Name))
                .ForMember(dest => dest.ExperienceName, opt => opt.MapFrom(src => src.Experience.Name))
                .ForMember(dest => dest.ExperienceEmployer, opt => opt.MapFrom(src => src.Experience.Employer))
                .ForMember(dest => dest.ExperienceResponsibilities, opt => opt.MapFrom(src => src.Experience.Responsibilities))
                .ForMember(dest => dest.ExperienceDescription, opt => opt.MapFrom(src => src.Experience.Description))
                .ForMember(dest => dest.ExperienceStartDate, opt => opt.MapFrom(src => src.Experience.StartDate))
                .ForMember(dest => dest.ExperienceEndDate, opt => opt.MapFrom(src => src.Experience.EndDate))
                .ForMember(dest => dest.SocialMedia, opt => opt.MapFrom(src => src.SocialMedias.ToDictionary(sm => sm.Name, sm => sm.Value)));

            CreateMap<ResumeDto, Resume>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => new Speciality { Name = src.Speciality }))
                .ForMember(dest => dest.ExperienceId, opt => opt.Ignore())
                .ForMember(dest => dest.Experience, opt => opt.MapFrom(src => new WorkExperience
                {
                    Name = src.ExperienceName,
                    Employer = src.ExperienceEmployer,
                    Responsibilities = src.ExperienceResponsibilities,
                    Description = src.ExperienceDescription,
                    StartDate = src.ExperienceStartDate,
                    EndDate = src.ExperienceEndDate
                }))
                .ForMember(dest => dest.SocialMedias, opt => opt.MapFrom(src => src.SocialMedia.Select(sm => new SocialMedia
                {
                    Name = sm.Key,
                    Value = sm.Value
                }).ToList()));

            CreateMap<Resume, ResumeDto>()
                .ForMember(dest => dest.Speciality, opt => opt.MapFrom(src => src.Speciality.Name))
                .ForMember(dest => dest.ExperienceName, opt => opt.MapFrom(src => src.Experience.Name))
                .ForMember(dest => dest.ExperienceEmployer, opt => opt.MapFrom(src => src.Experience.Employer))
                .ForMember(dest => dest.ExperienceResponsibilities, opt => opt.MapFrom(src => src.Experience.Responsibilities))
                .ForMember(dest => dest.ExperienceDescription, opt => opt.MapFrom(src => src.Experience.Description))
                .ForMember(dest => dest.ExperienceStartDate, opt => opt.MapFrom(src => src.Experience.StartDate))
                .ForMember(dest => dest.ExperienceEndDate, opt => opt.MapFrom(src => src.Experience.EndDate))
                .ForMember(dest => dest.SocialMedia, opt => opt.MapFrom(src => src.SocialMedias.ToDictionary(sm => sm.Name, sm => sm.Value)));
        }
    }
}
