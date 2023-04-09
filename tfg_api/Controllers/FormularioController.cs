﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/[controller]")]
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
        [Route("Pregunta")]
        public async Task<IEnumerable<PreguntaFormulario>> GetPreguntas([FromHeader] string tipo)
        {
            var preguntas = await preguntaFormularioBBDD.PreguntaFormularios.Where(p=>p.Tipo.Equals(tipo)).Select(
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
        [Route("Respuesta")]
        public async Task<IEnumerable<RespuestaFormulario>> GetRespuestas([FromHeader] Guid IdUsuario)
        {
            var respuestas = await respuestaFormularioBBDD.RespuestaFormularios.Where(r => r.IdUsuario.Equals(IdUsuario)).Select(
                r => new RespuestaFormulario
                {
                    IdPregunta = r.IdPregunta,
                     Valor= r.Valor
                }).ToListAsync();

            return respuestas;


        }
        /// <summary>
        /// Crea todas las respuestas
        /// </summary>
        /// <returns></returns>
        [HttpPost, Authorize]
        [Route("Respuesta")]
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
        [Route("Respuesta")]
        public async Task<ActionResult> DeleteRespuestas([FromHeader] Guid id, string tipo )
        {
            if (tipo.Equals("C")) {

                for (int i = 0; i <= 20; i++) {

                    var respuesta = respuestaFormularioBBDD.RespuestaFormularios.FindAsync(id, tipo);
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

                    var respuesta = respuestaFormularioBBDD.RespuestaFormularios.FindAsync(id, tipo);
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
        [Route("Respuesta")]
        public ActionResult UpdateRespuestas([FromHeader] Guid IdUsuario, [FromHeader] string tipo, [FromBody] IEnumerable<RespuestaFormulario> RespuestaFormulariosBody)
        {
            try
            {
                Task<ActionResult> task = DeleteRespuestas(IdUsuario, tipo);
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