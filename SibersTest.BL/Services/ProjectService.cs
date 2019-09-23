using System;
using System.Collections.Generic;
using System.Text;
using SibersTest.BL.Interfaces;
using SibersTest.BL.ModelsDTO;
using SibersTest.BL.Infrastructure;
using SibersTest.DAL.Interfaces;
using AutoMapper;
using SibersTest.DAL.Models;
using System.Threading.Tasks;

namespace SibersTest.BL.Services
{
    public class ProjectService<T> : IService<ProjectDTO>
    {
        IUnitOfWork database { get; set; }

        public ProjectService(IUnitOfWork uow)
        {
            database = uow;
        }

        public async Task Create(ProjectDTO projectDTO)
        {
            await database.Projects.Create(CreateByMapper.CreateProjectByMapper(projectDTO));
            await database.Save();
        }

        public async Task Delete(ProjectDTO projectDTO)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task Edit(ProjectDTO source, ProjectDTO dest)
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectDTO> Get(int? id)
        {
            if (id == null) return null;
            Project project = await database.Projects.Get(id);
            if (project == null)
                throw new ValidationException ( "Проект не найден", "" );
            return new ProjectDTO{
                                    ProjectId = project.ProjectId,
                                    Name = project.Name,
                                    LeadId = project.LeadId,
                                    Customer = project.Customer,
                                    Performer = project.Performer,
                                    Priority = project.Priority,
                                    StartDate = project.StartDate,
                                    FinishDate = project.FinishDate
                                 };
        }

        public async Task<IEnumerable<ProjectDTO>> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Project, ProjectDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Project>, List<ProjectDTO>>(await database.Projects.GetAll());
        }
    }
}
