using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfiguration;

public class UsuarioEntityConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(u => u.IdUsuario);

        builder.Property(u => u.IdUsuario).HasColumnName("id");
        builder.Property(u => u.NombreUsuario).HasColumnName("nombre").IsRequired().HasMaxLength(100);
        builder.Property(u => u.EmailUsuario).HasColumnName("email").IsRequired().HasMaxLength(100);
        builder.Property(u => u.FechaRegistroUsuario).HasColumnName("fecha_registro").IsRequired().HasMaxLength(50);
    }
}
