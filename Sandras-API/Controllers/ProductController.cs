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
    public class ProductController : ControllerBase
    {
        [HttpGet("products")]
        public IEnumerable<Product> GetProducts(int page, int cat, string search = "")
        {
            TiendaDBHandle tienda = new TiendaDBHandle();
            List<Product> data = new List<Product>();

            if (string.IsNullOrEmpty(search))
            {
                data = tienda.GetProducts(page, cat, "");
            }
            else
            {
                data = tienda.GetProducts(page, cat, search);
            }

            return data;
        }

        [HttpGet("listproducts")]
        public IEnumerable<Product> GetListProducts(string ids)
        {
            TiendaDBHandle tienda = new TiendaDBHandle();
            List<Product> data = new List<Product>();
            data = tienda.GetListProducts(ids);

            return data;
        }


    }
}
