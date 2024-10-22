using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using PreGrado.ComunicacionSync.http;
using PreGrado.Repositories;

namespace PreGrado
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(
                s => s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver()
            );
            builder.Services.AddHttpClient<ICampusHistorialCliente,ImplCampusHistorialCliente>();
            builder.Services.AddDbContext<UniversidadDbContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("una_conexion")));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IEstudiante1Repository, ImplEstudiante1Repository>();
            builder.Services.AddScoped<IEstudianteRepository, ImplEstudianteRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
