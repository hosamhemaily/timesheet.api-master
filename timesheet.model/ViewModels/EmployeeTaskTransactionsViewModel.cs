using System;
using System.Collections.Generic;
using System.Text;

namespace timesheet.model.ViewModels
{
    public class EmployeeTaskTransactionsViewModel
    {
        public long Id { get; set; }
        public int? EmployeeId { get; set; }
        public int? TaskId { get; set; }
        public string DayId { get; set; }
        public int? Hours { get; set; }

        
    }
}
