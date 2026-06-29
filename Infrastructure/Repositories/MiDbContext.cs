using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories;

public class MiDbContext : DbContext
{
    public const string DEFAULT_SCHEMA = "public";

    public MiDbContext(DbContextOptions<MiDbContext> options) : base(options)
    {
        this.ChangeTracker.LazyLoadingEnabled = false;
    }

    public DbSet<Persona> Personas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Aplicamos la configuración
        modelBuilder.ApplyConfiguration(new PersonaEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UsuarioEntityConfiguration());
        //Llamamos a la base y LISTO
        base.OnModelCreating(modelBuilder);
    }
}
