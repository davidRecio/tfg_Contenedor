using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;
using tfg_api.DDBB;
using tfg_api.Model.Asignatura;
using tfg_api.Model.PreguntaFormulario;
using tfg_api.Model.RespuestaFormulario;

namespace tfg_api.Controllers
{
    /// <summary>
    /// Controlador del formulario 
    /// </summary>
    [ApiController]
    [Route("api")]
    public class FormularioController : Controller
    {
        private readonly RespuestaFormularioBBDD respuestaFormularioBBDD;
        private readonly PreguntaFormularioBBDD preguntaFormularioBBDD;
        /// <summary>
        /// Elemento que interactura como puente en base de datos y ruta
        /// </summary>
        /// <param name="respuestaFormularioBBDD"></param>
        public FormularioController(RespuestaFormularioBBDD respuestaFormularioBBDD, PreguntaFormularioBBDD preguntaFormularioBBDD)
        {
            this.respuestaFormularioBBDD = respuestaFormularioBBDD;
            this.preguntaFormularioBBDD = preguntaFormularioBBDD;
        }

        /// <summary>
        /// Obtiene todas las preguntas
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [Route("[controller]/{idFormulario}/Pregunta")]
        public async Task<IEnumerable<PreguntaFormulario>> GetPreguntas( string idFormulario)
        {
            var preguntas = await preguntaFormularioBBDD.PreguntaFormularios.Where(p=>p.Tipo.Equals(idFormulario)).Select(
                p => new PreguntaFormulario
                {
                    IdPregunta = p.IdPregunta,
                    Contenido = p.Contenido,
                    Imagen_url = p.Imagen_url,
                }).ToListAsync();

            return preguntas;

  
        }
        /// <summary>
        /// Obtiene todas las respuestas
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [Route("Usuario/{idUsuario}/[controller]/{idFormulario}/Respuesta")]
        public async Task<IEnumerable<RespuestaFormulario>> GetRespuestasFoprmulario( Guid IdUsuario, string idFormulario)
        {
            List<int> listaChaside = Enumerable.Range(1, 98).ToList();
            List<int> listaToulouse = Enumerable.Range(99, 107).ToList();
           
            if (idFormulario.Equals('0')) {

                var respuestas = await respuestaFormularioBBDD.RespuestaFormularios.Where(r => r.IdUsuario.Equals(IdUsuario)).Where(r => !listaChaside.Any(lC => lC == r.IdPregunta)).Select(
                   r => new RespuestaFormulario
                   {
                       IdPregunta = r.IdPregunta,
                       Valor = r.Valor
                   }).ToListAsync();
                return respuestas;
            }
            else {
                var respuestas = await respuestaFormularioBBDD.RespuestaFormularios.Where(r => r.IdUsuario.Equals(IdUsuario)).Where(r => !listaToulouse.Any(lC => lC == r.IdPregunta)).Select(
               r => new RespuestaFormulario
               {
                   IdPregunta = r.IdPregunta,
                   Valor = r.Valor
               }).ToListAsync();
                return respuestas;
            }
           

           


        }
        /// <summary>
        /// Obtiene todas las respuestas
        /// </summary>
        /// <returns></returns>
        [HttpGet, Authorize]
        [Route("Usuario/{idUsuario}/[controller]/Respuesta")]
        public async Task<IEnumerable<RespuestaFormulario>> GetRespuestas(Guid IdUsuario)
        {
            var respuestas = await respuestaFormularioBBDD.RespuestaFormularios.Where(r => r.IdUsuario.Equals(IdUsuario)).Select(
                r => new RespuestaFormulario
                {
                    IdPregunta = r.IdPregunta,
                    Valor = r.Valor
                }).ToListAsync();

            return respuestas;


        }
        /// <summary>
        /// Crea todas las respuestas
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize]
        [Route("Usuario/{idUsuario}/[controller]/Respuesta")]
        public async Task<ActionResult> CrearRespuestas([FromHeader] Guid IdUsuario, [FromBody]IEnumerable<RespuestaFormulario> RespuestaFormulariosBody)
        {
            try
            {
            

            foreach (RespuestaFormulario respuestaFormulario in RespuestaFormulariosBody) {
                await respuestaFormularioBBDD.RespuestaFormularios.AddAsync(respuestaFormulario);
                
            }
            await respuestaFormularioBBDD.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(502);
            }

        }

        /// <summary>
        /// Borra  las respuestas
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Usuario/{idUsuario}/[controller]/{idFormulario}/Respuesta")]
        public async Task<ActionResult> DeleteRespuestas( Guid id, string idFormulario)
        {
            //revisar
            if (idFormulario.Equals("0")) {

                for (int i = 0; i <= 20; i++) {

                    var respuesta = respuestaFormularioBBDD.RespuestaFormularios.FindAsync(id, idFormulario);
                    if (await respuesta != null)
                    {
                        respuestaFormularioBBDD.Remove(respuesta);
                        await respuestaFormularioBBDD.SaveChangesAsync();
                        return Ok(respuesta);

                    }

                }

            }
            else
            {
                for (int i = 20;  i <=30; i++)
                {

                    var respuesta = respuestaFormularioBBDD.RespuestaFormularios.FindAsync(id, idFormulario);
                    if (await respuesta != null)
                    {
                        respuestaFormularioBBDD.Remove(respuesta);
                        await respuestaFormularioBBDD.SaveChangesAsync();
                        return Ok(respuesta);

                    }

                }

            }

            return NotFound();
        }
        /// <summary>
        /// Actualiza las respuestas
        /// </summary>
        /// <returns></returns>
        [HttpPut, Authorize]
        [Route("Usuario/{idUsuario}/[controller]/{idFormulario}/Respuesta")]
        public ActionResult UpdateRespuestas(Guid IdUsuario, string idFormulario, [FromBody] IEnumerable<RespuestaFormulario> RespuestaFormulariosBody)
        {
            try
            {
                Task<ActionResult> task = DeleteRespuestas(IdUsuario, idFormulario);
                task = CrearRespuestas(IdUsuario, RespuestaFormulariosBody);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(502);
            }

        }
    }
}
