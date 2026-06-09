using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using BoholBusTicketing.Core.Models;

namespace BoholBusTicketing.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Barangay> Barangays { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Conductor> Conductors { get; set; }
        public DbSet<RoutePreset> RoutePresets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Conductor>()
                .HasOne(c => c.FromMunicipality)
                .WithMany()
                .HasForeignKey(c => c.FromMunicipalityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conductor>()
                .HasOne(c => c.ToMunicipality)
                .WithMany()
                .HasForeignKey(c => c.ToMunicipalityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Barangay>()
                .HasOne(b => b.Municipality)
                .WithMany(m => m.Barangays)
                .HasForeignKey(b => b.MunicipalityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.FromBarangay)
                .WithMany()
                .HasForeignKey(t => t.FromBarangayId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.ToBarangay)
                .WithMany()
                .HasForeignKey(t => t.ToBarangayId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Conductor)
                .WithMany()
                .HasForeignKey(t => t.ConductorId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Conductor>()
                .HasOne(c => c.RoutePreset)
                .WithMany()
                .HasForeignKey(c => c.RoutePresetId)
                .OnDelete(DeleteBehavior.Restrict);

            var jsonOptions = new JsonSerializerOptions();
            modelBuilder.Entity<RoutePreset>()
                .Property(r => r.RouteMunicipalities)
                .HasConversion(new ValueConverter<List<string>, string>(
                    v => JsonSerializer.Serialize(v, jsonOptions),
                    v => JsonSerializer.Deserialize<List<string>>(v, jsonOptions) ?? new List<string>()))
                .HasColumnType("nvarchar(max)");
        }
    }
}