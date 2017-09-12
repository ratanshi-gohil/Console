using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class SQLDBContext : DbContext
    {
        public SQLDBContext() : base("name=DBConnection")
        { }

        public DbSet<Company> Companies { get; set; }
    }
}
