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
    public class FeedbackController : ControllerBase
    {
        [HttpPost("feedback")]
        public void ProcesarFeedback([FromBody] Feedback feedback)
        {
            TiendaDBHandle tiendaDB = new TiendaDBHandle();
            tiendaDB.NewFeedback(feedback);
        }
    }
}
