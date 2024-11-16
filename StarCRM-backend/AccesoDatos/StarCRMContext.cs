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
            optionsBuilder.UseSqlServer("SERVER=DESKTOP-UTUI63B\\SQLEXPRESS;INTEGRATED SECURITY=TRUE;ENCRYPT=FALSE;Initial Catalog=StarCRM-Test");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
