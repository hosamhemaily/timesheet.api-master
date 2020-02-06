using System;
using System.Collections.Generic;

namespace timesheet.model.Enities
{
    public partial class Tasks
    {
        public Tasks()
        {
            EmployeeTasks = new HashSet<EmployeeTasks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<EmployeeTasks> EmployeeTasks { get; set; }
    }
}
