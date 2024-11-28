
using AccesoDatos;
using AccesoDatos.Interfaces;
using AccesoDatos.Repositorios;
using LogicaAplicacion.CasosDeUso.Usuarios;
using LogicaAplicacion.Interfaces.Usuarios;
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
            builder.Services.AddSwaggerGen();

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


            // CASOS DE USO

            // Usuario
            builder.Services.AddScoped<IAltaUsuario, AltaUsuario>();
            builder.Services.AddScoped<ILogin, Login>();
            builder.Services.AddScoped<IObtenerUsuario, ObtenerUsuario>();

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