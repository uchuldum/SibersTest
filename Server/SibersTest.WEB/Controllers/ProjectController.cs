using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SibersTest.BL.Infrastructure;
using SibersTest.BL.Services;
using SibersTest.BL.Interfaces;
using SibersTest.BL.ModelsDTO;
using AutoMapper;
using SibersTest.WEB.Models;

namespace SibersTest.WEB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        IService<ProjectDTO> service;

        public ProjectController(IService<ProjectDTO> service)
        {
            this.service = service;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Projects(int? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, ProjectViewModel>()).CreateMapper();
            if (id != null)
            {
                ProjectDTO projectDTO = await service.Get(id);
                var project = mapper.Map<ProjectDTO, ProjectViewModel>(projectDTO);
                return Ok(project);
            }
            else
            {
                IEnumerable<ProjectDTO> projectDTOs = await service.GetAll();
                var projects = mapper.Map<IEnumerable<ProjectDTO>, List<ProjectViewModel>>(projectDTOs);
                return Ok(projects);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProjectViewModel project)
        {
            try
            {
                ProjectDTO projectDTO = new ProjectDTO
                {
                    Name = project.Name,
                    Customer = project.Customer,
                    Performer = project.Performer,
                    LeadId = project.LeadId,
                    Priority = project.Priority,
                    StartDate = project.StartDate,
                    FinishDate = project.FinishDate
                };
                IEnumerable<ProjectDTO> projectDTOs = await service.Create(projectDTO);
                if(projectDTOs.Count() == 1)
                    project.ProjectId = projectDTOs.ToList()[0].ProjectId;
                return Ok(project.ProjectId);
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ProjectViewModel[] projects)
        {
            try
            {
                if (projects.Length == 2)
                {
                    ProjectDTO source = new ProjectDTO
                    {
                        Name = projects[0].Name,
                        Customer = projects[0].Customer,
                        LeadId = projects[0].LeadId,
                        StartDate = projects[0].StartDate,
                        FinishDate = projects[0].FinishDate,
                        Performer = projects[0].Performer,
                        Priority = projects[0].Priority,
                        ProjectId = projects[0].ProjectId
                    };
                    ProjectDTO dest = new ProjectDTO
                    {
                        Name = projects[1].Name,
                        Customer = projects[1].Customer,
                        LeadId = projects[1].LeadId,
                        StartDate = projects[1].StartDate,
                        FinishDate = projects[1].FinishDate,
                        Performer = projects[1].Performer,
                        Priority = projects[1].Priority,
                        ProjectId = projects[1].ProjectId
                    };
                    await service.Edit(source, dest);
                    return RedirectToAction("Projects");
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ProjectViewModel project)
        {
            try
            {
                ProjectDTO projectDTO = new ProjectDTO
                {
                    ProjectId = project.ProjectId,
                    Name = project.Name,
                    Customer = project.Customer,
                    Performer = project.Performer,
                    LeadId = project.LeadId,
                    Priority = project.Priority,
                    StartDate = project.StartDate,
                    FinishDate = project.FinishDate
                };
                await service.Delete(projectDTO);
                return RedirectToAction("Projects");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }
    }
}