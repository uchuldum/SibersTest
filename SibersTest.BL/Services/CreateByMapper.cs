using AutoMapper;
using SibersTest.BL.Infrastructure;
using SibersTest.BL.ModelsDTO;
using SibersTest.DAL.Models;
using System;
using System.Collections.Generic;

using System.Text;

namespace SibersTest.BL.Services
{
    static internal class CreateByMapper
    {
        static internal Employee CreateEmployeeByMapper(EmployeeDTO emp)
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

        static internal Project CreateProjectByMapper(ProjectDTO projectDTO)
        {
            if (projectDTO == null)
                throw new ValidationException("ПРИЛЕТЕЛО НУЛЛ", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectDTO, Project>()).CreateMapper();
            return mapper.Map<ProjectDTO, Project>(projectDTO);
        }

        static internal ProjectsEmployees CreateProjectEmployeeByMapper(ProjectsEmployeesDTO projectsEmployeesDTO)
        {
            if (projectsEmployeesDTO == null)
                throw new ValidationException("ПРИЛЕТЕЛО НУЛЛ", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ProjectsEmployeesDTO, ProjectsEmployees>()).CreateMapper();
            return mapper.Map<ProjectsEmployeesDTO, ProjectsEmployees>(projectsEmployeesDTO);
        }

        static internal IEnumerable<ProjectsEmployeesDTO> CreateProjectEmployeeDTOByMapper(IEnumerable<ProjectsEmployees> projectsEmployees)
        {
            if (projectsEmployees == null)
                throw new ValidationException("ПРИЛЕТЕЛО НУЛЛ", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<IEnumerable<ProjectsEmployees>, IEnumerable<ProjectsEmployeesDTO>>()).CreateMapper();
            return mapper.Map<IEnumerable<ProjectsEmployees>, IEnumerable<ProjectsEmployeesDTO>>(projectsEmployees);
        }
    }
}
