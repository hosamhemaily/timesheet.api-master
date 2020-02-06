using System;
using System.Collections.Generic;
using System.Text;

namespace timesheet.model.ViewModels
{
    public class EmployeeTaskDayViewModel
    {
        public int? Sat { get; set; }
        public int? Sun { get; set; }
        public int? Mon { get; set; }
        public int? Tue { get; set; }
        public int? Wed { get; set; }
        public int? Thu { get; set; }
        public int? Fri { get; set; }

        public string TaskName { get; set; }
    }
}
