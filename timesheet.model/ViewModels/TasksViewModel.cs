using System;
using System.Collections.Generic;
using System.Text;

namespace timesheet.model.ViewModels
{
    public class TasksViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<EmployeeTaskDayViewModel> EmployeeTasks { get; set; }

    }
}
