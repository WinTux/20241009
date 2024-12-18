using Campus.ComunicacionAsync;
using Campus.Eventos;

namespace Campus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<IProcesadorDeEventos,ProcesadorDeEventos>();
            builder.Services.AddHostedService<BusDeMensajesSuscriptor>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            //app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
