using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SibersTest.BL.Interfaces;
using SibersTest.BL.ModelsDTO;
using SibersTest.BL.Services;
using SibersTest.DAL.Repositories;
using SibersTest.DAL.Interfaces;
using SibersTest.DAL.Models;

namespace SibersTest.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("LocalPolicy", builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
            }));
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddAutoMapper(typeof(Startup));
            services.AddScoped(typeof(IRepository<Employee>), typeof(EmployeeRepository<Employee>));
            services.AddScoped(typeof(IRepository<Project>), typeof(ProjectRepository<Project>));
            services.AddScoped(typeof(IRepository<ProjectsEmployees>), typeof(EmployeeRepository<ProjectsEmployees>));
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();
         
            services.AddTransient(typeof(IService<EmployeeDTO>), typeof(EmployeeService<EmployeeDTO>));
            services.AddTransient(typeof(IService<ProjectDTO>), typeof(ProjectService<ProjectDTO>));
            services.AddTransient(typeof(IService<ProjectsEmployeesDTO>), typeof(ProjectsEmployeesService<ProjectsEmployeesDTO>));
            services.AddDbContext<SibersTest.DAL.SibersTestDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("LocalPolicy");
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes
                .MapRoute("Employee", "{controller=Employee}/{action=Index}/{id?}")
                .MapRoute("ControllerAction", "api/{controller}/{action}");
            });
        }
    }
}
