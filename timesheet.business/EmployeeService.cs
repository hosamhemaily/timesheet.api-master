using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using timesheet.data;
using timesheet.model;
using timesheet.model.Enities;
using timesheet.model.ViewModels;

namespace timesheet.business
{
    public class EmployeeService
    {
        public readonly IMapper _mapper;

        public TimesheetDb db { get; }
        public EmployeeService(TimesheetDb dbContext, IMapper mapper)
        {
            db = dbContext;
            _mapper = mapper;
        }

        public List<EmployeesViewModel> GetEmployees()
        {
            var employees = db.Employees.ToList();
            var employeeViewModels =  _mapper.Map<List<EmployeesViewModel>>(employees);
            return employeeViewModels;
        }

        public List<EmployeeTaskDayViewModel> GetEmployeeTaskDays()
        {
            List<EmployeeTaskDayViewModel> employeeTaskDays = new List<EmployeeTaskDayViewModel>();
            var tasks = this.db.Tasks;
            var employeeTasks = this.db.EmployeeTasks;
            foreach (var task in tasks.ToList())
            {
                employeeTaskDays.Add(new EmployeeTaskDayViewModel
                {
                    Fri = employeeTasks.Where(x=>x.DayId == "Fri" && x.EmployeeId == 1 ).FirstOrDefault().Hours??0,
                    Mon = employeeTasks.Where(x => x.DayId == "Mon" && x.EmployeeId == 1).FirstOrDefault().Hours??0,
                    Sat = employeeTasks.Where(x => x.DayId == "Sat" && x.EmployeeId == 1).FirstOrDefault().Hours??0,
                    Sun = employeeTasks.Where(x => x.DayId == "Sun" && x.EmployeeId == 1).FirstOrDefault().Hours??0,
                    TaskName = task.Name,
                    Thu = employeeTasks.Where(x => x.DayId == "Thu" && x.EmployeeId == 1).FirstOrDefault().Hours??0,
                    Tue = employeeTasks.Where(x => x.DayId == "Tue" && x.EmployeeId == 1).FirstOrDefault().Hours??0,
                    Wed = employeeTasks.Where(x => x.DayId == "Wed" && x.EmployeeId == 1).FirstOrDefault().Hours??0
                });
            }           
            return employeeTaskDays;
        }

        public async Task<ResultSave> InsertEmployeeTask(EmployeeTaskTransactionsViewModel employeeTaskDay)
        {
            //var EmplyeeTask = new EmployeeTasks()
            //{
            //    DayId = employeeTaskDay.DayId,
            //    EmployeeId = employeeTaskDay.EmployeeId,
            //    Hours = employeeTaskDay.Hours,
            //    TaskId = employeeTaskDay.TaskId
            //};
            var EmplyeeTask = _mapper.Map<EmployeeTasks>(employeeTaskDay);
            await this.db.EmployeeTasks.AddAsync(EmplyeeTask);
            var resultSave = await this.db.SaveChangesAsync();
            if (resultSave == 0)
            {
                return new ResultSave
                {
                    Message = "",
                    Success = false
                };
            }
            else
            {
                return new ResultSave
                {
                    Message = "",
                    Success = true
                };
            }            
        }

        public async Task<ResultSave> UpdateEmployeeTask(EmployeeTaskTransactionsViewModel employeeTaskDay)
        {
            //var empTask = await this.db.EmployeeTasks.FindAsync(employeeTaskDay.Id);
            //empTask.Hours = employeeTaskDay.Hours;
            //empTask.TaskId = employeeTaskDay.TaskId;
            //empTask.EmployeeId = employeeTaskDay.EmployeeId;
            //empTask.DayId = employeeTaskDay.DayId;
            var empTask = _mapper.Map<EmployeeTasks>(employeeTaskDay);
            this.db.Set<EmployeeTasks>().Attach(empTask);
            this.db.Entry(empTask).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            var resultSave = await this.db.SaveChangesAsync();
            if (resultSave == 0)
            {
                return new ResultSave
                {
                    Message = "",
                    Success = false
                };
            }
            else
            {
                return new ResultSave
                {
                    Message = "",
                    Success = true
                };
            }

        }

        public async Task<bool> DeleteEmployeeTask(long id)
        {
            var empTask =  db.EmployeeTasks.FirstOrDefault(x=>x.Id == id);
            db.EmployeeTasks.Remove(empTask);
            var resultDelete = await db.SaveChangesAsync();
            if (resultDelete == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
