using RabbitMQ.Client;

namespace PreGrado.ComunicacionAsync
{
    public class ImpBusDeMensajesCliente
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
            
            }
        }
    }
}
