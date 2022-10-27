using Hola.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using Hola.Models;
using Hola.Repository;

namespace Hola.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> Get()
        {
            return ADO_Usuario.GetUsuarios();
        }

        [HttpDelete (Name = "DeleteUsuario")]
        public void Eliminar([FromBody] int id)
        {
            ADO_Usuario.EliminarUsuario(id);
        }

        [HttpPut(Name = "ModificarUsuario")]
        public void Modificar([FromBody] Usuario us)
        {
            ADO_Usuario.ModificarUsuario(us);
        }

        [HttpPost(Name = "AgregarUsuario")]
        public void Agregar([FromBody] Usuario us)
        {
            ADO_Usuario.AgregarUsuario(us);
        }
    }
}
