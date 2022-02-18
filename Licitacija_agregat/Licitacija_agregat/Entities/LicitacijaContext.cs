using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licitacija_agregat.Entities
{
    public class LicitacijaContext : DbContext
    {
        public LicitacijaContext(DbContextOptions<LicitacijaContext> options) : base(options) 
        {

        }

        public DbSet<Licitacija>  Licitacije { get; set; }
        public DbSet<Etapa>  Etape{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}
