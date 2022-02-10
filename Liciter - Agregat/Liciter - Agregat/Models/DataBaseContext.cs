using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liciter___Agregat.Models
{
    public class DataBaseContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DataBaseContext (DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DataBaseContext()
        {

        }

        public DbSet<FizickoLiceModel> FizickoLiceModels { get; set; }
        public DbSet<PravnoLiceModel> PravnoLiceModels { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("LiciterDB"));
        }
    }
}
