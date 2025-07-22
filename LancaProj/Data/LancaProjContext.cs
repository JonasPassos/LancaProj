using Microsoft.EntityFrameworkCore;
using LancaProj.Pages.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace LancaProj.Data
{
    public class LancaProjContext : DbContext
    {
        public LancaProjContext(DbContextOptions<LancaProjContext> options)
            : base(options)
        {
        }

        public DbSet<Adm> Adm { get; set; } = default!;
        public DbSet<Horario> Horarios { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relação 1:N: Um Adm tem muitos Horarios
            modelBuilder.Entity<Horario>()
                .HasOne(h => h.Adm)
                .WithMany(a => a.HorariosRegistrados)
                .HasForeignKey(h => h.AdmID)
                .OnDelete(DeleteBehavior.Cascade); // Opcional: Deleta os horários se o Adm for deletado

            // Conversão automática para UTC (TimeZone)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(
                            new ValueConverter<DateTime, DateTime>(
                                v => v.ToUniversalTime(),
                                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                    }

                    if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(
                            new ValueConverter<DateTime?, DateTime?>(
                                v => v.HasValue ? v.Value.ToUniversalTime() : v,
                                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v));
                    }
                }
            }
        }
    }
}
