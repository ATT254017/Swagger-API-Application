using Microsoft.EntityFrameworkCore;
using SwaggerTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerTest.DAL
{
    public class DataAccessContext : DbContext
    {
        public DataAccessContext(DbContextOptions<DataAccessContext> options)
           : base(options)
        {
        }
        public DbSet<ValuesModel> ValuesModels { get; set; }

    }
}
