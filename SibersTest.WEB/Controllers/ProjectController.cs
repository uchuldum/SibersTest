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
    public class ProjectController : ControllerBase
    {
        IService<ProjectDTO> service;

        public ProjectController(IService<ProjectDTO> service)
        {
            this.service = service;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int? id)
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
                await service.Create(projectDTO);
                return RedirectToAction("Get");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProjectViewModel[] projects)
        {
            try
            {
                //service.Edit(s, d);
                return RedirectToAction("Get");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] EmployeeViewModel project)
        {
            try
            {
                //service.Delete(employeeDTO);
                return RedirectToAction("Get");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }
    }
}