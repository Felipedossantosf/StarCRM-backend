using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class StarCRMContext: DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }   
        public StarCRMContext(DbContextOptions<StarCRMContext> options) : base(options) { }
        public StarCRMContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:starcrm.database.windows.net,1433;Initial Catalog=StarCRM;Persist Security Info=False;User ID=starcrm;Password=upsWcAat@gc4MtC;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Username)
                .IsUnique()
                .HasName("Index_Username");

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique()
                .HasName("Index_Email");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.UserId)
                .HasColumnName("id");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Nombre)
                .HasColumnName("nombre");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Apellido)
                .HasColumnName("apellido");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Username)
                .HasColumnName("nombreUsuario");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Password)
                .HasColumnName("contraseña");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Rol)
                .HasColumnName("rol");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Cargo)
                .HasColumnName("cargo");

            modelBuilder.Entity<Usuario>()
                .Property(u => u.Email)
                .HasColumnName("correo");

            base.OnModelCreating(modelBuilder);
        }
    }
}
