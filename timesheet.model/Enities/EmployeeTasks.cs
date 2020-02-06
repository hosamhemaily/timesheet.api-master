using System;
using System.Collections.Generic;

namespace timesheet.model.Enities
{
    public partial class EmployeeTasks
    {
        public long Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? TaskId { get; set; }
        public string DayId { get; set; }
        public int? Hours { get; set; }

        public virtual Employees Employee { get; set; }
        public virtual Tasks Task { get; set; }
    }
}
