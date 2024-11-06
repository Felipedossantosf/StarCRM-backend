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
    }
}
