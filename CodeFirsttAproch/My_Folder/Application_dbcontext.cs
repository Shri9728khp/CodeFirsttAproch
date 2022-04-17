using CodeFirsttAproch.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirsttAproch.My_Folder
{
    public class Application_dbcontext : DbContext
    {
        

        public Application_dbcontext(DbContextOptions<Application_dbcontext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees
        {
            get; set;

        }
        public DbSet<LodinClass> LodinClass
        {
            get; set;

        }
    }


}