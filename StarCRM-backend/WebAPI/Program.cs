
using AccesoDatos;
using AccesoDatos.Interfaces;
using AccesoDatos.Repositorios;
using LogicaAplicacion.CasosDeUso.Actividades;
using LogicaAplicacion.CasosDeUso.Asignaciones;
using LogicaAplicacion.CasosDeUso.Clientes;
using LogicaAplicacion.CasosDeUso.Cotizaciones;
using LogicaAplicacion.CasosDeUso.Eventos;
using LogicaAplicacion.CasosDeUso.NotificacionesUsuario;
using LogicaAplicacion.CasosDeUso.Proveedor;
using LogicaAplicacion.CasosDeUso.Usuarios;
using LogicaAplicacion.Interfaces.Actividades;
using LogicaAplicacion.Interfaces.Asignaciones;
using LogicaAplicacion.Interfaces.Clientes;
using LogicaAplicacion.Interfaces.Cotizaciones;
using LogicaAplicacion.Interfaces.Eventos;
using LogicaAplicacion.Interfaces.Notificaciones;
using LogicaAplicacion.Interfaces.NotificacionesUsuario;
using LogicaAplicacion.Interfaces.Proveedor;
using LogicaAplicacion.Interfaces.Usuarios;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.IncludeXmlComments("WebAPI.xml");
            });

            ////Comienza JWT////
            var claveSecreta = "ZWRpw6fDo28gZW0gY29tcHV0YWRvcmE=";

            builder.Services.AddAuthentication(aut =>
            {
                aut.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                aut.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(aut =>
            {
                aut.RequireHttpsMetadata = false;
                aut.SaveToken = true;
                aut.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(claveSecreta)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //////////////////// FIN JWT ////////////////////////

            // Conexión Db
            // To Do
            ConfigurationBuilder configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("appsettings.json");
            string connection = configuration.Build().GetConnectionString("ConnectionStringDb");
            builder.Services.AddDbContext<StarCRMContext>(Options => Options.UseSqlServer(connection));


            // REPOSITORIOS
            builder.Services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            builder.Services.AddScoped<IRepositorioComercial, RepositorioComercial>();
            builder.Services.AddScoped<IRepositorio<Notificacion>, RepositorioNotificacion>();
            builder.Services.AddScoped<IRepositorioNotificacionUsuario, RepositorioNotificacionUsuario>();
            builder.Services.AddScoped<IRepositorio<Asignacion>, RepositorioAsignacion>();
            builder.Services.AddScoped<IRepositorio<Actividad>, RepositorioActividad>();
            builder.Services.AddScoped<IRepositorio<Evento>, RepositorioEvento>();
            builder.Services.AddScoped<IRepositorioEventoComercial, RepositorioEventoComercial>();
            builder.Services.AddScoped<IRepositorioEventoUsuario, RepositorioEventoUsuario>();
            builder.Services.AddScoped<IRepositorio<Cotizacion>, RepositorioCotizacion>();
            builder.Services.AddScoped<IRepositorioLineaCotizacion, RepositorioLineaCotizacion>();

            // CASOS DE USO

            // Usuario
            builder.Services.AddScoped<IAltaUsuario, AltaUsuario>();
            builder.Services.AddScoped<ILogin, Login>();
            builder.Services.AddScoped<IObtenerUsuario, ObtenerUsuario>();
            builder.Services.AddScoped<IObtenerUsuarios, ObtenerUsuarios>();
            builder.Services.AddScoped<IModificarUsuario, ModificarUsuario>();

            // Cliente
            builder.Services.AddScoped<IAltaCliente, AltaCliente>();
            builder.Services.AddScoped<IObtenerCliente, ObtenerCliente>();
            builder.Services.AddScoped<IObtenerClientes, ObtenerClientes>();
            builder.Services.AddScoped<IEliminarCliente, EliminarCliente>();    
            builder.Services.AddScoped<IModificarCliente, ModificarCliente>();

            // Proveedor
            builder.Services.AddScoped<IAltaProveedor, AltaProveedor>();
            builder.Services.AddScoped<IObtenerProveedor, ObtenerProveedor>();
            builder.Services.AddScoped<IObtenerProveedores, ObtenerProveedores>();
            builder.Services.AddScoped<IEliminarProveedor, EliminarProveedor>();
            builder.Services.AddScoped<IModificarProveedor, ModificarProveedor>();

            // Notificaciones
            builder.Services.AddScoped<IAltaNotificacionUsuario, AltaNotificacionUsuario>();
            builder.Services.AddScoped<IGetNotificacionesDeUsuario, GetNotificacionesDeUsuario>();
            builder.Services.AddScoped<IModificarNotificacionUsuario, ModificarNotificacionUsuario>();

            // Asignaciones
            builder.Services.AddScoped<IAltaAsignacion, AltaAsignacion>();
            builder.Services.AddScoped<IObtenerAsignacion, ObtenerAsignacion>();
            builder.Services.AddScoped<IModificarAsignacion, ModificarAsignacion>();
            builder.Services.AddScoped<IObtenerAsignaciones, ObtenerAsignaciones>();
            builder.Services.AddScoped<IEliminarAsignacion, EliminarAsignacion>();

            // Historial de actividades
            builder.Services.AddScoped<IObtenerActividad, ObtenerActividad>();
            builder.Services.AddScoped<IObtenerActividades, ObtenerActividades>();

            // Eventos
            builder.Services.AddScoped<IAltaEvento, AltaEvento>();
            builder.Services.AddScoped<IObtenerEvento, ObtenerEvento>();
            builder.Services.AddScoped<IObtenerEventos, ObtenerEventos>();
            builder.Services.AddScoped<IModificarEvento, ModificarEvento>();
            builder.Services.AddScoped<IEliminarEvento, EliminarEvento>();

            // Cotizaciones
            builder.Services.AddScoped<IGetCotizacion, GetCotizacion>();
            builder.Services.AddScoped<IAltaCotizacion, AltaCotizacion>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Nueva", app =>
                {
                    app.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("Nueva");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}