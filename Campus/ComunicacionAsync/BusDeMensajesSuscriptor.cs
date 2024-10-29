using Campus.Eventos;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Campus.ComunicacionAsync
{
    public class BusDeMensajesSuscriptor : BackgroundService
    {
        private readonly IConfiguration configuracion;
        private readonly IProcesadorDeEventos procesador;

        private IConnection conexion;
        private IModel canal;
        private string cola;
        public BusDeMensajesSuscriptor(IConfiguration configuracion, IProcesadorDeEventos procesador)
        {
            this.configuracion = configuracion;
            this.procesador = procesador;
            IniciarRabbitMQ();
        }

        private void IniciarRabbitMQ()
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuracion["Host_RabbitMQ"],
                Port = int.Parse(configuracion["Puerto_RabbitMQ"])
            };
            conexion = factory.CreateConnection();
            canal = conexion.CreateModel();
            canal.ExchangeDeclare(
                exchange: "mi_exchange",
                type: ExchangeType.Fanout
            );
            cola = canal.QueueDeclare().QueueName;
            canal.QueueBind(
                queue: cola,
                exchange: "mi_exchange",
                routingKey: ""
            );
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();//detener si se lo solicita
             var consumidor = new EventingBasicConsumer(canal);//establecer nuevo consumidor RabbitMQ
            consumidor.Received += (modulo, eveArgs) =>
            {
                Console.WriteLine("Un evento sucedió.");
                var cuerpo = eveArgs.Body;
                var mensaje = Encoding.UTF8.GetString(cuerpo.ToArray());
                procesador.ProcesarEvento(mensaje);
            };
            canal.BasicConsume(
                queue: cola,
                autoAck: true,
                consumer: consumidor
            );
            return Task.CompletedTask;
        }
    }
}
