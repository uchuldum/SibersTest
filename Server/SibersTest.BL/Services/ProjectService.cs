﻿using System;
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

        public async Task<IEnumerable<ProjectDTO>> Create(ProjectDTO projectDTO)
        {
            Project project = CreateByMapper.CreateProjectByMapper(projectDTO);
            await database.Projects.Create(project);
            await database.Save();
            IEnumerable<Project> projects = database.Projects.Find(p =>
                                                p.Name == project.Name &&
                                                p.Customer == project.Customer &&
                                                p.Priority == project.Priority &&
                                                p.Performer == project.Performer &&
                                                p.StartDate == project.StartDate &&
                                                p.FinishDate == project.FinishDate &&
                                                p.LeadId == project.LeadId
                                                );
            if (projects == null) return null;
            IEnumerable<ProjectDTO> projectDTOs = CreateByMapper.CreateProjectDTOByMapper(projects);
            return projectDTOs;
        }

        public async Task Delete(ProjectDTO projectDTO)
        {
            Project p = CreateByMapper.CreateProjectByMapper(projectDTO);
            await database.Projects.Delete(p);
            IEnumerable<ProjectsEmployees> projectsEmployees = database.ProjectsEmployees.Find(pe => pe.ProjectId == p.ProjectId);
            if(projectsEmployees != null)
                foreach (var pes in projectsEmployees)
                    await database.ProjectsEmployees.Delete(pes);
            await database.Save();
            
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task Edit(ProjectDTO source, ProjectDTO dest)
        {
            await database.Projects.Update(CreateByMapper.CreateProjectByMapper(source), CreateByMapper.CreateProjectByMapper(dest));
            await database.Save();
        }

        public async Task<ProjectDTO> Get(int? id)
        {
            if (id == null) return null;
            Project project = await database.Projects.Get(id);
            if (project == null)
                throw new ValidationException("Проект не найден", "");
            return new ProjectDTO
            {
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

        public async Task<IEnumerable<ProjectDTO>> Find(int? id)
        {
            return null;
        }

        public async Task<IEnumerable<ProjectDTO>> Find(string name)
        {
            return null;
        }
    }
}