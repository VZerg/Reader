using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BashReader
{
    class Context: DbContext
    {
        public Context():base("BashCon")
        { }

        public DbSet<Post> Posts{ get; set; }

        
    }
}
