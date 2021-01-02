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
    public class EmployeeController : ControllerBase
    {
        private CompanyDbContext context;
        public EmployeeController(CompanyDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var response = new List<BesicEmployeeViewModel>();
            var employees = context.Employees.ToList();

            foreach(var item in employees)
            {
                var vm = new BesicEmployeeViewModel();
                vm.Department = item.Department.Name;
                vm.Email = item.Email;
                vm.Id = item.Id;
                vm.Name = item.FirstName + " " + item.LastName;

                response.Add(vm);
            }
            return Ok(response);
        }
        [HttpPost]
        public IActionResult SaveNewEmployees(EmployeeViewModel vm)
        {
            var response = new ResponseViewModel();
            
            try
            {
                var employee = new Employee();
                employee.DeptId = vm.DepartmentId;
                employee.Email = vm.Email;
                employee.FirstName = vm.FirstName;
                employee.LastName = vm.LastName;
                employee.Phone = vm.Phone;

                context.Employees.Add(employee);

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

        [HttpPut]
        public IActionResult UpdateEmployee(EmployeeViewModel vm)
        {

            var response = new ResponseViewModel();

            

            try
            {
                //var employee = new context.Employee.FirstOrDefault(x => x.Id == vm.Id);
               // var employee = new Employee(x => x.Id == vm.Id);
                var employee = context.Employees.FirstOrDefault(x => x.Id == vm.Id);

                if (employee != null)
                {
                    employee.DeptId = vm.DepartmentId;
                    employee.Email = vm.Email;
                    employee.FirstName = vm.FirstName;
                    employee.LastName = vm.LastName;

                    context.Employees.Update(employee);
                    context.SaveChanges();
                    response.IsSuccess = true;
                    response.Message = "Succesfully Update";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Employee not exist";
                }

            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return Ok(response);

        }
        [HttpDelete]
        public IActionResult DeleteEmployee(int id)
        {
            var response = new ResponseViewModel();
            var employee = context.Employees.FirstOrDefault(x => x.Id == id);

            try
            {
                if (employee != null)
                {
                    context.Remove<Employee>(employee);
                    context.SaveChanges();
                    response.IsSuccess = true;
                    response.Message = "Succesfully Update";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Employee not exist";
                }
           
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.ToString();
            }
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetEmployeeId(int id)
        {
            var response = new ResponseViewModel();
            var employee = context.Employees.FirstOrDefault(x => x.Id == id);
        }

    }
}
