
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPublisherMessageBroker.Models
{
    public class Pedido
    {
        public Pedido()
        {
            Id = Guid.NewGuid();           
        }
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double Valor { get; set; }      

    }
}
