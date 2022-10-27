using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Hola.Models;
using Hola.Repository;

namespace Hola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {

        [HttpGet(Name = "GetVenta")]
        public List<Venta> Get()
        {
            return ADO_Venta.GetVenta();
        }

        [HttpDelete(Name = "DeleteVenta")]
        public void Eliminar([FromBody] int id)
        {
            ADO_Venta.EliminarVenta(id);
        }

        [HttpPost]
        public void Agregar([FromBody] ListaVenta vn)
        {
            ADO_Venta.CargarVenta(vn);
        }
    }
}

