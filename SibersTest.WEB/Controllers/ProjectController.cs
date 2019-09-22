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
        IService<EmployeeDTO> service;

        public ProjectController(IService<EmployeeDTO> service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult Get()
        {
            IEnumerable<ProjectDTO> projectDTOs = service.GetAll();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()).CreateMapper();
            var employees = mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employeeDTOs);
            return Ok(employees);
        }

        [HttpPost]
        public ActionResult Create([FromBody] EmployeeViewModel emp)
        {
            try
            {
                EmployeeDTO employeeDTO = new EmployeeDTO { Name = emp.SurName + " " + emp.Name + " " + emp.Patronymic, Email = emp.Email };
                service.Create(employeeDTO);
                return RedirectToAction("Get");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult Put([FromBody] EmployeeViewModel[] emp)
        {
            try
            {
                EmployeeDTO s = new EmployeeDTO { Name = emp[0].SurName + " " + emp[0].Name + " " + emp[0].Patronymic, Email = emp[0].Email };
                EmployeeDTO d = new EmployeeDTO { Name = emp[1].SurName + " " + emp[1].Name + " " + emp[1].Patronymic, Email = emp[1].Email };
                service.Edit(s, d);
                return RedirectToAction("Get");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] EmployeeViewModel emp)
        {
            try
            {
                EmployeeDTO employeeDTO = new EmployeeDTO { Name = emp.SurName + " " + emp.Name + " " + emp.Patronymic, Email = emp.Email };
                service.Delete(employeeDTO);
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