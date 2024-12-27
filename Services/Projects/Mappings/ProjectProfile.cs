using AutoMapper;
using Core.Entities.Projects;
using Services.Projects.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Projects.Mappings
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>()
                .ForMember(
                    dest => dest.AttachedFileIds,
                    opt => opt.MapFrom(
                        src => src.AttachedFiles.Select(file => file.Id).ToList()
                    )
                );

            CreateMap<ProjectDto, Project>()
                .ForMember(
                    dest => dest.AttachedFiles,
                    opt => opt.Ignore()
                );

            CreateMap<ProjectCreateDto, Project>()
                .ForMember(
                    dest => dest.AttachedFiles,
                    opt => opt.MapFrom(
                        src => src.AttachedFileIds.Select(id => new Core.Entities.Projects.File { Id = id }).ToList()
                    )
                );

            CreateMap<Project, ProjectCreateDto>()
                .ForMember(
                    dest => dest.AttachedFileIds,
                    opt => opt.MapFrom(src => src.AttachedFiles.Select(f => f.Id).ToList())
                );

            CreateMap<ProjectPage, ProjectPageDto>().ReverseMap();

            CreateMap<ProjectPageCreateDto, ProjectPage>();
 
            CreateMap<ProjectPage, ProjectPageCreateDto>();
        }
    }
}
