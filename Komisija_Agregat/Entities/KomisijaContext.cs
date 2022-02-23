using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Emit;


namespace Komisija_Agregat.Entities
{
    public class KomisijaContext : DbContext
    {
        private readonly IConfiguration configuration;


        public KomisijaContext(DbContextOptions<KomisijaContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }




        public DbSet<Komisija> Komisije { get; set; }
        public DbSet<ClanKomisije> ClanoviKomisije { get; set; }
        public DbSet<Predsednik> Predsednici { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("KomisijaDB"));
        }
       

    }
}