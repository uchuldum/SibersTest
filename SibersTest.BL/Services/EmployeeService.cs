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
    public class EmployeeService<T> : IService<EmployeeDTO>
    {
        IUnitOfWork database { get; set; }

        public EmployeeService(IUnitOfWork uow)
        {
            database = uow;
        }

        public async Task Create(EmployeeDTO emp)
        {
            await database.Employees.Create(CreateByMapper.CreateEmployeeByMapper(emp));
            await database.Save();
        }

        public async Task Delete(EmployeeDTO emp)
        {
            Employee e = CreateByMapper.CreateEmployeeByMapper(emp);
            await database.Employees.Delete(e);
            IEnumerable<ProjectsEmployees> projectsEmployees = database.ProjectsEmployees.Find(pe => pe.EmployeeId == e.EmployeeId);
            foreach (var pes in projectsEmployees)
                await database.ProjectsEmployees.Delete(pes);
            await database.Save();
        }

        public async Task Edit(EmployeeDTO source, EmployeeDTO dest)
        {
            await database.Employees.Update(CreateByMapper.CreateEmployeeByMapper(source), CreateByMapper.CreateEmployeeByMapper(dest));
            await database.Save();
        }

        public async Task<EmployeeDTO> Get(int? id)
        {
            if (id == null) return null;
            Employee employee = await database.Employees.Get(id);
            if (employee == null)
                throw new ValidationException("Сотрудник не найден", "");
            return new EmployeeDTO { EmployeeId = employee.EmployeeId, Name = employee.SurName + " " + employee.Name + " " + employee.Patronymic, Email = employee.Email };
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(x => x.Name, x => x.MapFrom(m => m.SurName + " " + m.Name + " " + m.Patronymic)))
                .CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(await database.Employees.GetAll());
        }

        public async Task<IEnumerable<EmployeeDTO>> Find(int id)
        {
            return null;
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}