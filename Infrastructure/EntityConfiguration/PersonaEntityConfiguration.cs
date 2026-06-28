using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MiPracticaCasera.Infrastructure.EntityConfiguration;

public class PersonaEntityConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona");

        builder.HasKey(e => e.IdPersona);

        builder.Property(e => e.IdPersona).HasColumnName("id_persona");
        builder.Property(e => e.NombrePersona).HasColumnName("nombre_persona");
        builder.Property(e => e.ApellidoPersona).HasColumnName("apellido_persona");
        builder.Property(e => e.RutPersona).HasColumnName("rut_persona");
        builder.Property(e => e.TelefonoPersona).HasColumnName("telefono_persona");
        builder.Property(e => e.FechaNacimientoPersona).HasColumnName("fecha_nacimiento_persona");
    }
}