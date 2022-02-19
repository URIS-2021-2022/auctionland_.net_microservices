using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Entities
{
    public class LicitacijaContext : DbContext
    {
        private readonly IConfiguration configuration;

        public LicitacijaContext(DbContextOptions<LicitacijaContext> options, IConfiguration configuration) : base(options) 
        {
            this.configuration = configuration;
        }

        public LicitacijaContext()
        {

        }

        public DbSet<Licitacija> Licitacije { get; set; }
        public DbSet<Etapa> Etape{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("LicitacijaDB"));
        }

    }
}
