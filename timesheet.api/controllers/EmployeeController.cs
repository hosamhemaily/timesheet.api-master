using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using timesheet.business;
using timesheet.model.Enities;
using timesheet.model.ViewModels;

namespace timesheet.api.controllers
{
    //[Route("api/v1/employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;
        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll(string text)
        {
            var items = this.employeeService.GetEmployees();
            return new ObjectResult(items);
        }

        [HttpGet(nameof(GetAllEmpTasks))]
        public IActionResult GetAllEmpTasks(string employee)
        {
            var items = this.employeeService.GetEmployeeTaskDays();
            return new ObjectResult(items);
        }

        [HttpPost(nameof(InsertEmpTask))]
        public async Task< IActionResult > InsertEmpTask(EmployeeTaskTransactionsViewModel employeeTaskDay)
        {
            var result = await this.employeeService.InsertEmployeeTask(employeeTaskDay);
            return new ObjectResult(result);
        }

        [HttpPost(nameof(UpdateEmpTask))]
        public async Task<IActionResult> UpdateEmpTask(EmployeeTaskTransactionsViewModel employeeTaskDay)
        {
            var result = await this.employeeService.UpdateEmployeeTask(employeeTaskDay);
            return new ObjectResult(result);
        }

        [HttpDelete("" + nameof(DeleteEmpTask) + "/{id}")]
        public async Task<IActionResult> DeleteEmpTask(int id)
        {
            var result = await this.employeeService.DeleteEmployeeTask(id);
            return new ObjectResult(result);
        }
    }
}