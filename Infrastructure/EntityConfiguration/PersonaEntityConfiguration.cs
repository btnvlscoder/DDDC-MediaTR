using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfiguration;

public class PersonaEntityConfiguration : IEntityTypeConfiguration<Persona>
{
    public void Configure(EntityTypeBuilder<Persona> builder)
    {
        builder.ToTable("persona");

        builder.HasKey(e => e.IdPersona);

        builder.Property(e => e.IdPersona).HasColumnName("id");
        builder.Property(e => e.NombrePersona).HasColumnName("nombre");
        builder.Property(e => e.ApellidoPersona).HasColumnName("apellido");
        builder.Property(e => e.RutPersona).HasColumnName("rut").HasMaxLength(12).IsRequired();
        builder.Property(e => e.TelefonoPersona).HasColumnName("telefono");
        builder.Property(e => e.FechaNacimientoPersona).HasColumnName("fecha_nacimiento");
        builder.Property(e => e.UsuarioId).HasColumnName("usuario_id");

        builder.HasOne(p => p.Usuario)
               .WithMany()
               .HasForeignKey(p => p.UsuarioId);
    }
}