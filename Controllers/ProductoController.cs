using Hola.Repository;
using Hola.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Hola.Controllers.UsuarioController;

namespace Hola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {

        [HttpGet(Name = "GetProductos")]
        public List<Producto> Get()
        {
            return ADO_Producto.GetProductos();
        }

        [HttpDelete(Name = "DeleteProductos")]
        public void Eliminar([FromBody] int id)
        {
            ADO_Producto.EliminarProducto(id);
        }

        [HttpPut(Name = "ModificarProductos")]
        public void Modificar([FromBody] Producto pr)
        {
            ADO_Producto.ModificarProducto(pr);
        }

        [HttpPost(Name = "AgregarProductos")]
        public void Agregar([FromBody] Producto pr)
        {
            ADO_Producto.AgregarProducto(pr);
        }
    }
}

