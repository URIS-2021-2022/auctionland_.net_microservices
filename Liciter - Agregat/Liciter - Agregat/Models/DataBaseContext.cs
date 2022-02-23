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

        public DbSet<FizickoLiceModel> FizickaLica { get; set; }
        public DbSet<PravnoLiceModel> PravnaLica { get; set; }
        public DbSet<OvlascenoLiceModel> OvlascenaLica { get; set; }
        public DbSet<KupacModel> Kupci { get; set; }
        public DbSet<LiciterModel> Liciteri { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("LiciterDB"));
        }
    }
}
