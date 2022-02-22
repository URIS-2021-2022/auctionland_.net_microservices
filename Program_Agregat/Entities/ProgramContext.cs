using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection.Emit;


namespace Program_Agregat.Entities
{
    public class ProgramContext : DbContext
    {
        private readonly IConfiguration configuration;


        public ProgramContext(DbContextOptions<ProgramContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }
        public DbSet<PredlogPlana> PredloziPlana { get; set; }
        public DbSet<ProgramEntity> Programi { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ProgramDB"));
        }
        /*
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProgramEntity>().HasData(new
            {
                ProgramId = Guid.Parse("6a411c13-a195-48f7-8dbd-67596c3974c0"),
                MaksimalnoOgranicenje = "234234",
                Licitacije = "423423",
            });
            builder.Entity<PredlogPlana>().HasData(new
            {
                PredlogPlanaId = Guid.Parse("1c7ea607-8ddb-493a-87fa-4bf5893e965b"),
                BrojDokumenta = "342432457",
                MisljenjeKomisije = "pozitivno",
                Usvojen = true,
                DatumDokumenta = new DateTime(2022, 2, 1),
            }); 
     
        }
        */
    }
}