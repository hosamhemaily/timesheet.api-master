using System;
using System.Collections.Generic;

namespace timesheet.model.Enities
{
    public partial class Employees
    {
        public Employees()
        {
            EmployeeTasks = new HashSet<EmployeeTasks>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmployeeTasks> EmployeeTasks { get; set; }
    }
}
