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
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsEmployeesController : ControllerBase
    {
        IService<ProjectsEmployeesDTO> service;

        public ProjectsEmployeesController(IService<ProjectsEmployeesDTO> service)
        {
            this.service = service;
        }

        [HttpGet("{projectId?}")]
        public async Task<IActionResult> Get(int? projectId)
        {
            IEnumerable<ProjectsEmployeesDTO> projectsEmployeesDTOs;
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectsEmployeesDTO, ProjectsEmployeesViewModel>()).CreateMapper();
            if(projectId == null)
                projectsEmployeesDTOs = await service.GetAll();
            else
                projectsEmployeesDTOs = await service.Find(projectId);
            if (projectsEmployeesDTOs == null) return NotFound();
            var project = mapper.Map<IEnumerable<ProjectsEmployeesDTO>, IEnumerable<ProjectsEmployeesViewModel>>(projectsEmployeesDTOs);
            return Ok(projectsEmployeesDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProjectsEmployeesViewModel projectsEmployeesView)
        {
            try
            {
                if (projectsEmployeesView != null)
                {
                    ProjectsEmployeesDTO peDTO = new ProjectsEmployeesDTO
                    {
                        ProjectId = projectsEmployeesView.ProjectId,
                        EmployeeId = projectsEmployeesView.EmployeeId
                    };
                    await service.Create(peDTO);
                }
            }
            catch(ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] ProjectsEmployeesViewModel[] projectsEmployeesView)
        {
            try
            {
                if (projectsEmployeesView.Length == 2)
                {
                    ProjectsEmployeesDTO source = new ProjectsEmployeesDTO
                    {
                        Id = projectsEmployeesView[0].Id,
                        ProjectId = projectsEmployeesView[0].ProjectId,
                        EmployeeId = projectsEmployeesView[0].EmployeeId
                    };
                    ProjectsEmployeesDTO dest = new ProjectsEmployeesDTO
                    {
                        Id = projectsEmployeesView[1].Id,
                        ProjectId = projectsEmployeesView[1].ProjectId,
                        EmployeeId = projectsEmployeesView[1].EmployeeId
                    };
                    await service.Edit(source, dest);
                    return Ok();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] ProjectsEmployeesViewModel projectsEmployeesView)
        {
            try
            {
                ProjectsEmployeesDTO projectsEmployeesDTO = new ProjectsEmployeesDTO
                {
                    Id = projectsEmployeesView.Id,
                    ProjectId = projectsEmployeesView.EmployeeId,
                    EmployeeId = projectsEmployeesView.EmployeeId
                };
                await service.Delete(projectsEmployeesDTO);
                return Ok();
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }
    }
}