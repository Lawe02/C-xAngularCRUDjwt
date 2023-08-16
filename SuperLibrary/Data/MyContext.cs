using Microsoft.EntityFrameworkCore;
using SuperLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperLibrary.Data
{
    public class MyContext : DbContext
    {
        public MyContext() { }

        public DbSet<Book> Books { get; set; }
    }
}
