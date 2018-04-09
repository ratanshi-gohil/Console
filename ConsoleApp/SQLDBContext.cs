using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class SQLDBContext : DbContext
    {
        public SQLDBContext() : base("name=DBConnection")
        { }

        public DbSet<Company> Companies { get; set; }
    }
}
