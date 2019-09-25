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
    public class ProjectsEmployeesService<T> : IService<ProjectsEmployeesDTO>
    {
        IUnitOfWork database { get; set; }

        public ProjectsEmployeesService(IUnitOfWork uow)
        {
            database = uow;
        }

        public async Task Create(ProjectsEmployeesDTO projectsEmployeesDTO)
        {
            ProjectsEmployees pe = CreateByMapper.CreateProjectEmployeeByMapper(projectsEmployeesDTO);
            IEnumerable<Employee> emps = database.Employees.Find(e => e.EmployeeId == pe.EmployeeId);
            IEnumerable<Project> ps = database.Projects.Find(p => p.ProjectId == pe.ProjectId);
            if (emps != null && ps != null)
            {
                await database.ProjectsEmployees.Create(pe);
                await database.Save();
            }
        }

        public async Task Delete(ProjectsEmployeesDTO projectsEmployeesDTO)
        {
            await database.ProjectsEmployees.Delete(CreateByMapper.CreateProjectEmployeeByMapper(projectsEmployeesDTO));
            await database.Save();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task Edit(ProjectsEmployeesDTO source, ProjectsEmployeesDTO dest)
        {
            await database.ProjectsEmployees
                .Update(CreateByMapper.CreateProjectEmployeeByMapper(source),
                        CreateByMapper.CreateProjectEmployeeByMapper(dest));
            await database.Save();
        }

        public Task<ProjectsEmployeesDTO> Get(int? id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProjectsEmployeesDTO>> Find(int? projectId)
        {
            return CreateByMapper.CreateProjectEmployeeDTOByMapper(database.ProjectsEmployees.Find(pe => pe.ProjectId == projectId));
        }

        public async Task<IEnumerable<ProjectsEmployeesDTO>> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectsEmployees, ProjectsEmployeesDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<ProjectsEmployees>, List<ProjectsEmployeesDTO>>(await database.ProjectsEmployees.GetAll());
        }
    }
}
