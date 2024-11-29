﻿using Gestion_de_Tareas.Models;
using Microsoft.EntityFrameworkCore;
namespace Gestion_de_Tareas.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        

        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación uno a muchos
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Tareas)
                .HasForeignKey(t => t.UsuarioId);

        }
        }
    }