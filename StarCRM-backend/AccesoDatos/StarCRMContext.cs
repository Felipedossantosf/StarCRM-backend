using LogicaNegocio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class StarCRMContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Comercial> Comerciales { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<NotificacionUsuario> NotificacionesUsuario { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet<Actividad> Actividades { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<EventoComercial> EventoComerciales { get; set; }
        public DbSet<EventoUsuario> EventoUsuarios { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; } 
        public DbSet<LineaCotizacion> LineasCotizacion { get; set; }

        public StarCRMContext(DbContextOptions<StarCRMContext> options) : base(options) { }
        public StarCRMContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=tcp:starcrm.database.windows.net,1433;Initial Catalog=StarCRM;Persist Security Info=False;User ID=starcrm;Password=upsWcAat@gc4MtC;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Usuario
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

            // Comercial
            modelBuilder.Entity<Comercial>()
                .ToTable("Comercial")
                .HasKey(c => c.id);

            modelBuilder.Entity<Comercial>()
                .HasDiscriminator<string>("TipoComercial")
                .HasValue<Cliente>("Cliente")
                .HasValue<Proveedor>("Proveedor");


            // Notificaciones
            modelBuilder.Entity<Notificacion>()
                .ToTable("Notificacion");

            modelBuilder.Entity<NotificacionUsuario>()
                .ToTable("NotificacionUsuario");

            modelBuilder.Entity<NotificacionUsuario>()
                .HasOne(nu => nu.notificacion)
                .WithMany()
                .HasForeignKey(nu => nu.notificacion_id)
                .OnDelete(DeleteBehavior.Cascade); // Configuración de comportamiento de eliminación adecuado.

            modelBuilder.Entity<Notificacion>()
                .HasOne<Cliente>() // Configura la relación con Cliente si es necesaria.
                .WithMany()
                .HasForeignKey(n => n.cliente_id)
                .OnDelete(DeleteBehavior.Restrict); // O DeleteBehavior.SetNull

            // Asignaciones
            modelBuilder.Entity<Asignacion>()
                .ToTable("Asignacion");

            // Actividades
            modelBuilder.Entity<Actividad>()
                .ToTable("Actividad");

            // Eventos
            modelBuilder.Entity<Evento>()
                .ToTable("Evento");

            modelBuilder.Entity<EventoUsuario>()
                .HasOne(eu => eu.evento)
                .WithMany()
                .HasForeignKey(eu => eu.evento_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventoComercial>()
                .HasOne(ec => ec.evento)
                .WithMany()
                .HasForeignKey(ec => ec.evento_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventoComercial>()
                .HasKey(ec => new { ec.evento_id, ec.comercial_id });

            modelBuilder.Entity<EventoUsuario>()
                .HasKey(eu => new { eu.evento_id, eu.usuario_id });

            // Cotizacion
            modelBuilder.Entity<LineaCotizacion>()
                .HasOne(lc => lc.cotizacion)
                .WithMany()
                .HasForeignKey(lc => lc.cotizacion_id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cotizacion>()
                .HasOne<Cliente>() // Configura la relación con Cliente si es necesaria.
                .WithMany()
                .HasForeignKey(c => c.cliente_id)
                .OnDelete(DeleteBehavior.Restrict); // O DeleteBehavior.SetNull

            modelBuilder.Entity<Cotizacion>()
                .HasOne<Usuario>() // Configura la relación con Cliente si es necesaria.
                .WithMany()
                .HasForeignKey(c => c.usuario_id)
                .OnDelete(DeleteBehavior.Restrict); // O DeleteBehavior.SetNull

            base.OnModelCreating(modelBuilder);
        }
    }
}
