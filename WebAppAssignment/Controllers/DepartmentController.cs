using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppAssignment.Context;
using WebAppAssignment.Models;
using WebAppAssignment.ViewModel;

namespace WebAppAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private CompanyDbContext context;

        public DepartmentController(CompanyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]

        public IActionResult GetAllDepartment()
        {
            var response = new List<DepartmentViewModel>();
            var departments = context.Departments.ToList();

            foreach(var dpt in departments)
            {
                var dvm = new DepartmentViewModel();
                dvm.Id = dpt.Id;
                dvm.DepartmentName = dpt.Name;
                dvm.Extention = dpt.Extention;

                response.Add(dvm);
            }
            return Ok(response);
        }

        [HttpPost]

        public IActionResult saveDepartment(DepartmentViewModel dvm)
        {
            var response = new ResponseViewModel();

            try
            {
                var department = new Department();

                department.Name = dvm.DepartmentName;
                department.Extention = dvm.Extention;
                department.Location = dvm.Location;

                context.Departments.Add(department);
                context.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Success!";
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return Ok(response);
        }

  

    }
}
