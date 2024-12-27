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
                    opt => opt.MapFrom(src => src.AttachedFiles.Select(file => file.Id).Distinct().ToList()))
                .ForMember(
                    dest => dest.AttachedFileUrls,
                    opt => opt.MapFrom(
                        src => src.AttachedFiles.Select(file => $"/api/files/{file.Id}").Distinct().ToList()
                    )
                );

            CreateMap<ProjectDto, Project>()
                .ForMember(dest => dest.AttachedFiles, opt => opt.Ignore());

            CreateMap<ProjectCreateDto, Project>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.ProjectName))
                .ForMember(
                    dest => dest.AttachedFiles,
                    opt => opt.MapFrom(
                        src => src.AttachedFileIds.Distinct()
                            .Select(id => new Core.Entities.Projects.File { Id = id }).ToList()
                    )
                );

            CreateMap<Project, ProjectCreateDto>()
                .ForMember(
                    dest => dest.AttachedFileIds,
                    opt => opt.MapFrom(src => src.AttachedFiles.Select(f => f.Id).Distinct().ToList())
                );
        }

    }
}
