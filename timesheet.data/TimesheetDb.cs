using Microsoft.EntityFrameworkCore;
using System;
using timesheet.model;
using timesheet.model.Enities;

namespace timesheet.data
{
    public class TimesheetDb : DbContext
    {
        public TimesheetDb(DbContextOptions<TimesheetDb> options)
            : base(options)
        {
        }

        public TimesheetDb()
        {

        }

        public DbSet<Employees> Employees { get; set; }
        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<EmployeeTasks> EmployeeTasks { get; set; }
    }
}
