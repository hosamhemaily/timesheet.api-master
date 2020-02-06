using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using timesheet.model.Enities;
using timesheet.model.ViewModels;

namespace timesheet.model
{
    public class MapProfile: Profile
    {
        public MapProfile()
        {
            CreateMap<Employees, EmployeesViewModel>().ReverseMap();
            CreateMap<EmployeeTasks, EmployeeTaskTransactionsViewModel>().ReverseMap();

        }

    }
}
