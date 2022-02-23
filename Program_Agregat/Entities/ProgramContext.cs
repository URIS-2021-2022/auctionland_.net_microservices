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
 
    }
}