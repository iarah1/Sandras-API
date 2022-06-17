using Microsoft.AspNetCore.Mvc;
using Sandras_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sandras_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        // POST api/<PedidoController>
        [HttpPost("procesarpedido")]
        public int ProcesarPedido([FromBody] Pedido pedido)
        {
            TiendaDBHandle tienda = new TiendaDBHandle();
            int pedidoId = tienda.ProcesarPedido(pedido.cliente, pedido.pedidoDetalle);

            return pedidoId;
        }

    }
}
