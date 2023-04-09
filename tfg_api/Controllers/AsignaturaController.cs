using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tfg_api.DDBB;
using tfg_api.Model.Asignatura;

namespace tfg_api.Controllers
{
    /// <summary>
    /// Controlador de la asignatura
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AsignaturaController : Controller
    {
        private readonly AsignaturaBBDD asignaturaBBDD;
        /// <summary>
        /// Elemento que interactura como puente en base de datos y ruta
        /// </summary>
        /// <param name="asignaturaBBDD"></param>
        public AsignaturaController(AsignaturaBBDD asignaturaBBDD)
        {
            this.asignaturaBBDD = asignaturaBBDD;
        }

        /// <summary>
        /// Obtiene todos las asignaturas
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [Route("All")]

        public async Task<ActionResult> GetAsignaturas()
        {

            return Ok(await asignaturaBBDD.Asignaturas.ToListAsync());
        }

        /// <summary>
        ///  Función encargada de recibir una id de una asignatura y mostrar sus datos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet,Authorize]
        public async Task<ActionResult> GetAsignatura([FromHeader] Guid id)
        {
            var asignatura = await asignaturaBBDD.Asignaturas.FindAsync(id);

            if (asignatura != null)
            {
                return Ok(asignatura);

            }
            return NotFound();
        }
        /// <summary>
        /// Añade una asignatura
        /// </summary>
        /// <param name="addAsignaturaRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddAsignatura(AddAsignaturaRequest addAsignaturaRequest)
        {
            var asignatura = new Model.Asignatura.Asignatura_UsuarioBBDD()
            {
                IdAsignatura = Guid.NewGuid(),
                Tipo = addAsignaturaRequest.Tipo,
                Nombre = addAsignaturaRequest.Nombre
            };

            await asignaturaBBDD.Asignaturas.AddAsync(asignatura);
            await asignaturaBBDD.SaveChangesAsync();

            return Ok(asignatura);


        }
        /// <summary>
        /// Actualiza la asignatura
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAsignaturaRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateAsignatura([FromHeader] Guid id, UpdateAsignaturaRequest updateAsignaturaRequest)
        {

            var asignatura = await asignaturaBBDD.Asignaturas.FindAsync(id);

            if (asignatura != null)
            {

                asignatura.Tipo = updateAsignaturaRequest.Tipo;
                asignatura.Nombre = updateAsignaturaRequest.Nombre;

                await asignaturaBBDD.SaveChangesAsync();

                return Ok(asignatura);

            }
            return NotFound();

        }
        /// <summary>
        /// Borra la asignatura
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteAsignatura([FromHeader] Guid id)
        {
            var asignatura = await asignaturaBBDD.Asignaturas.FindAsync(id);

            if (asignatura != null)
            {
                asignaturaBBDD.Remove(asignatura);
                await asignaturaBBDD.SaveChangesAsync();
                return Ok(asignatura);

            }
            return NotFound();
        }
    }
}
