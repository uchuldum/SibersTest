using System;
using System.Collections.Generic;
using System.Text;
using SibersTest.BL.Interfaces;
using SibersTest.BL.ModelsDTO;
using SibersTest.BL.Infrastructure;
using SibersTest.DAL.Interfaces;
using AutoMapper;
using SibersTest.DAL.Models;

namespace SibersTest.BL.Services
{
    public class ProjectService<T> : IService<ProjectDTO>
    {
        IUnitOfWork database { get; set; }

        public ProjectService(IUnitOfWork uow)
        {
            database = uow;
        }

        private Project CreateProjectByMapper(ProjectDTO projectDTO)
        {
            if (projectDTO == null)
                throw new ValidationException("ПРИЛЕТЕЛО НУЛЛ", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>()).CreateMapper();
            return mapper.Map<ProjectDTO, Project>(projectDTO);
        }


        public void Create(ProjectDTO projectDTO)
        {
            database.Projects.Create(CreateProjectByMapper(projectDTO));
            database.Save();
        }

        public void Delete(ProjectDTO projectDTO)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Edit(ProjectDTO source, ProjectDTO dest)
        {
            throw new NotImplementedException();
        }

        public ProjectDTO Get(ProjectDTO item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Project>, List<ProjectDTO>>(database.Projects.GetAll());
        }
    }
}
