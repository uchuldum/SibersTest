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
    public class EmployeeController : ControllerBase
    {
        public class Name
        {
            public string name { get; set; }
        }

        IService<EmployeeDTO> service;

        public EmployeeController(IService<EmployeeDTO> service)
        {
            this.service = service;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> GetId(int? id)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()
               .ForMember(x => x.SurName, x => x.MapFrom(m => m.Name.Split(new char[] { ' ' })[0]))
               .ForMember(x => x.Name, x => x.MapFrom(m => m.Name.Split(new char[] { ' ' })[1]))
               .ForMember(x => x.Patronymic, x => x.MapFrom(m => m.Name.Split(new char[] { ' ' })[2])))
               .CreateMapper();
            if (id != null)
            {
                EmployeeDTO employeeDTO = await service.Get(id);
                var employee = mapper.Map<EmployeeDTO, EmployeeViewModel>(employeeDTO);
                return Ok(employee);
            }
            else
            {
                IEnumerable<EmployeeDTO> employeeDTOs = await service.GetAll();
                var employees = mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employeeDTOs);
                return Ok(employees);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetName([FromBody] Name name)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, EmployeeViewModel>()
              .ForMember(x => x.SurName, x => x.MapFrom(m => m.Name.Split(new char[] { ' ' })[0]))
              .ForMember(x => x.Name, x => x.MapFrom(m => m.Name.Split(new char[] { ' ' })[1]))
              .ForMember(x => x.Patronymic, x => x.MapFrom(m => m.Name.Split(new char[] { ' ' })[2])))
              .CreateMapper();
            IEnumerable<EmployeeDTO> employeeDTOs = null;
            if (name.name != null)
            {
                employeeDTOs = await service.Find(name.name);
                if (employeeDTOs.Count() == 0)
                  return  RedirectToAction("GetId");
            }
            else
            {
                return RedirectToAction("GetId");
            }
            var employees = mapper.Map<IEnumerable<EmployeeDTO>, List<EmployeeViewModel>>(employeeDTOs);
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeViewModel emp)
        {
            try
            {
                EmployeeDTO employeeDTO = new EmployeeDTO { Name = emp.SurName + " " + emp.Name + " " + emp.Patronymic, Email = emp.Email };
                await service.Create(employeeDTO);
                return RedirectToAction("GetId");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] EmployeeViewModel[] emps)
        {
            try
            {
                if (emps.Length == 2)
                {
                    EmployeeDTO source = new EmployeeDTO
                    {
                        //EmployeeId = emps[0].EmployeeId,
                        Name = emps[0].SurName + " " + emps[0].Name + " " + emps[0].Patronymic,
                        Email = emps[0].Email
                    };
                    EmployeeDTO dest = new EmployeeDTO
                    {
                        //EmployeeId = emps[1].EmployeeId,
                        Name = emps[1].SurName + " " + emps[1].Name + " " + emps[1].Patronymic,
                        Email = emps[1].Email
                    };
                    await service.Edit(source, dest);
                    return RedirectToAction("GetId");
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] EmployeeViewModel emp)
        {
            try
            {
                EmployeeDTO employeeDTO = new EmployeeDTO
                {
                    EmployeeId = emp.EmployeeId,
                    Name = emp.SurName + " " + emp.Name + " " + emp.Patronymic,
                    Email = emp.Email
                };
                await service.Delete(employeeDTO);
                return RedirectToAction("GetId");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(ex.Property, ex.Message);
            }
            return Ok();
        }


    }
}