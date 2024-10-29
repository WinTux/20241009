using PreGrado.DTO;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace PreGrado.ComunicacionAsync
{
    public class ImpBusDeMensajesCliente : IBusDeMensajesCliente
    {
        private readonly IConfiguration configuration;
        private readonly IConnection conexion;
        private readonly IModel canal;
        public ImpBusDeMensajesCliente(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionFactory factory = new ConnectionFactory() { 
                HostName = configuration["Host_RabbitMQ"],
                Port = int.Parse(configuration["Puerto_RabbitMQ"])
            };
            try {
                conexion = factory.CreateConnection();
                canal = conexion.CreateModel();
                canal.ExchangeDeclare(
                    exchange: "mi_exchange",
                    type: ExchangeType.Fanout
                    );
            }
            catch (Exception e) {
                Console.WriteLine($"Error al tratar de establecer conexión con RabbitMQ: { e.Message}");
            }
        }
        //Método para publicar esetudiante:
        public void PublicarNuevoEstudiante(EstudiantePublisherDTO estudianteDTO)
        {
            //primero vamos a crear un objeto serializado del objeto estudianteDTO
            string mensaje =JsonSerializer.Serialize(estudianteDTO);
            if (conexion.IsOpen)
                Enviar(mensaje);//definiremos este método más abajo////Podríamos, además, poner un console diciendo que se está enviando mensaje
            else
                Console.WriteLine("No se pudo enviar el mensaje al bus de mensaje RabbitMQ");
        }
        private void Enviar(string msj)
        {
            var cuerpo = Encoding.UTF8.GetBytes(msj);
            canal.BasicPublish(
                exchange: "mi_exchange",
                routingKey: "",
                basicProperties: null,
                body: cuerpo
            );
            Console.WriteLine("Se envió mensaje al bus de mensajes");
        }
    }
}
