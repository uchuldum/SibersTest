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
    public class EmployeeService<T> : IService<EmployeeDTO>
    {
        IUnitOfWork database { get; set; }

        public EmployeeService(IUnitOfWork uow)
        {
            database = uow;
        }

        private Employee CreateEmployeeByMapper(EmployeeDTO emp)
        {
            if (emp == null)
                throw new ValidationException("ПРИЛЕТЕЛО НУЛЛ", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, Employee>()
                .ForMember(x => x.SurName, x => x.MapFrom(m => m.Name.Split(new char[] { ' ' })[0]))
                .ForMember(x => x.Name, x => x.MapFrom(m => m.Name.Split(new char[] { ' ' })[1]))
                .ForMember(x => x.Patronymic, x => x.MapFrom(m => m.Name.Split(new char[] { ' ' })[2])))
                .CreateMapper();
            return mapper.Map<EmployeeDTO, Employee>(emp);
        }

        public void Create(EmployeeDTO emp)
        {
            database.Employees.Create(CreateEmployeeByMapper(emp));
            database.Save();
        }

        public void Delete(EmployeeDTO emp)
        {
            database.Employees.Delete(CreateEmployeeByMapper(emp));
            database.Save();
        }

        public void Edit(EmployeeDTO source, EmployeeDTO dest)
        {
            database.Employees.Update(CreateEmployeeByMapper(source), CreateEmployeeByMapper(dest));
            database.Save();
        }

        public EmployeeDTO Get(EmployeeDTO emp)
        {
            Employee employee = database.Employees.Get(CreateEmployeeByMapper(emp));
            if (employee == null)
                throw new ValidationException("Сотрудник не найден", "");

            return new EmployeeDTO {Name = employee.SurName + " " + employee.Name + " " + employee.Patronymic, Email = employee.Email};
        }

        public IEnumerable<EmployeeDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Employee, EmployeeDTO>()
                .ForMember(x => x.Name, x => x.MapFrom(m => m.SurName + " " + m.Name + " " + m.Patronymic)))
                .CreateMapper();
            return mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(database.Employees.GetAll());
        }

        public void Dispose()
        {
            database.Dispose();
        }
    }
}
