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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Predsednik>().HasData(new
            {
                PredsednikId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                ImePredsednika = "Petar",
                PrezimePredsednika= "Markovic",
                EmailPredsednika = "markuza@mail.com"
            });
            builder.Entity<ClanKomisije>().HasData(new
            {
                ClanId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                ImeClana = "Nenad",
                PrezimeClana = "Jeckovic",
                EmailClana = "jocko@mail.com"
            });
            builder.Entity<Komisija>().HasData(new
            {
                KomisijaId= Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                Predsednik = "sd",
                Clanovi = "sdd"
            });
        }
        
    
       
    }
}
