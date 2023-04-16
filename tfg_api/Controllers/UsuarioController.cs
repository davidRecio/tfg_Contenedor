using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tfg_api.DDBB;
using tfg_api.Model.UsuarioContenedor;

namespace tfg_api.Controllers
{
    /// <summary>
    /// Exposición del recurso Usuario
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioBBDD usuarioBBDD;
        /// <summary>
        /// Elemento que interactura como puente en base de datos y ruta
        /// </summary>
        /// <param name="usuarioBBDD"></param>
        public UsuarioController(UsuarioBBDD usuarioBBDD)
        {
            this.usuarioBBDD = usuarioBBDD;
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet,Authorize]
        [Route("All")]
   
        public async Task<ActionResult> GetUsuarios()
        {

            return Ok(await usuarioBBDD.Usuarios.ToListAsync());
        }

        /// <summary>
        ///  Función encargada de recibir una id de un usuario y mostrar sus datos
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idUsuario}")]
        public async Task<ActionResult> GetUsuario( Guid idUsuario)
        {
            var usuario = await usuarioBBDD.Usuarios.FindAsync(idUsuario);

            if (usuario != null)
            {
                return Ok(usuario);

            }
            return NotFound();
        }
        /// <summary>
        /// Añade un usuario
        /// </summary>
        /// <param name="addUsuarioRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddUsuario(AddUsuarioRequest addUsuarioRequest)
        {
            var usuario = new Usuario()
            {
                IdUsuario = Guid.NewGuid(),
                Nombre = addUsuarioRequest.Nombre,
                Pass = addUsuarioRequest.Pass
            };

            await usuarioBBDD.Usuarios.AddAsync(usuario);
            await usuarioBBDD.SaveChangesAsync();

            return Ok(usuario);


        }
        /// <summary>
        /// Actualiza al usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="updateUsuarioRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{idUsuario}")]
    public async Task<ActionResult> UpdateUsuario(Guid idUsuario, UpdateUsuarioRequest updateUsuarioRequest)
        {

          var usuario=  await usuarioBBDD.Usuarios.FindAsync(idUsuario);

            if (usuario != null)
            {

                usuario.Nombre = updateUsuarioRequest.Nombre;
                usuario.Pass= updateUsuarioRequest.Pass;

                await usuarioBBDD.SaveChangesAsync();

                return Ok(usuario);

            }
            return NotFound();
        
        }
        /// <summary>
        /// Borra al usuario
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{idUsuario}")]
        public async Task<ActionResult> DeleteUsuario( Guid idUsuario)
        {
            var usuario = await usuarioBBDD.Usuarios.FindAsync(idUsuario);

            if (usuario != null)
            {
                usuarioBBDD.Remove(usuario);
                await usuarioBBDD.SaveChangesAsync();
                return Ok(usuario);

            }
            return NotFound();
        }
    }
}
