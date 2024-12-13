using Microsoft.EntityFrameworkCore;
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
            // Configuration des tables pour TPT
            modelBuilder.Entity<Enregistrement>().ToTable("Enregistrements");
            modelBuilder.Entity<Anomalie>().ToTable("Anomalies");

            // Configuration des propriétés communes : `CardNumber` et `Date` seront présentes dans les deux tables
        }
    }
}
