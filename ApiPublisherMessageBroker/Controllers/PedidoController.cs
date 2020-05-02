using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ApiPublisherMessageBroker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace ApiPublisherMessageBroker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private ILogger<PedidoController> _logger;

        public PedidoController(ILogger<PedidoController> logger)
        {
            _logger = logger;

        }       


        public IActionResult Adicionar(Pedido pedido)
        {

            var factory = new ConnectionFactory()
            {
                HostName = "tksscirv",
                Password = "yQYTBf7E3aycV3I-WyISpNZRGhQ5yUq6",
                Uri = new System.Uri("amqp://tksscirv:yQYTBf7E3aycV3I-WyISpNZRGhQ5yUq6@owl.rmq.cloudamqp.com/tksscirv")
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "FilaPedidos",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonSerializer.Serialize(pedido);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "FilaPedidos",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" Pedido enviado: {0}", message);

                try
                {
                    return Accepted(pedido);
                }
                catch (Exception erro)
                {

                    _logger.LogError("Erro ao efetuar pedido", erro);
                    return new StatusCodeResult(500);
                }

            }


        }
    }
}