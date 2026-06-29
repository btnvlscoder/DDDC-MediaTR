using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityConfiguration;

public class ProyectoEntityConfiguration : IEntityTypeConfiguration<Proyecto>
{
    public void Configure(EntityTypeBuilder<Proyecto> builder)
    {
        builder.ToTable("proyecto");
        builder.HasKey(e => e.IdProyecto);

        builder.Property(e => e.IdProyecto).HasColumnName("id");
        builder.Property(e => e.NombreProyecto).HasColumnName("nombre").IsRequired();
        builder.Property(e => e.DescripcionProyecto).HasColumnName("descripcion");
        builder.Property(e => e.FechaInicioProyecto).HasColumnName("fecha_inicio");
    }
}
