using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serveur.Entities;
namespace Serveur.Datas
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions options)
            :base(options)
        {
            
        }

        public DbSet<Enregistrement> Enregistrements { get; set; }
        public DbSet<Anomalie> Anomalies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Enregistrement>().ToTable("Enregistrements");
            modelBuilder.Entity<Anomalie>().ToTable("Anomalies");

            
        }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=dbCB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }*/
    }
}
