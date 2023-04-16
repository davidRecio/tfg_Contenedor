using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tfg_api.DDBB;
using tfg_api.Model.Interna;
using tfg_api.Model.PreguntaFormulario;
using tfg_api.Model.RespuestaFormulario;
using tfg_api.Model.UsuarioContenedor;

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
        private readonly UsuarioBBDD usuarioBBDD;
        /// <summary>
        /// Elemento que interactura como puente en base de datos y ruta
        /// </summary>
        /// <param name="respuestaFormularioBBDD"></param>
        /// <param name="preguntaFormularioBBDD"></param>
        public FormularioController(RespuestaFormularioBBDD respuestaFormularioBBDD, PreguntaFormularioBBDD preguntaFormularioBBDD, UsuarioBBDD usuarioBBDD)
        {
            this.respuestaFormularioBBDD = respuestaFormularioBBDD;
            this.preguntaFormularioBBDD = preguntaFormularioBBDD;
            this.usuarioBBDD = usuarioBBDD;
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
                    Tipo=p.Tipo,
                }).ToListAsync();

            return preguntas;

  
        }
        /// <summary>
        /// Obtiene todas las respuestas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Usuario/{idUsuario}/[controller]/{idFormulario}/Respuesta")]
        public async Task<IEnumerable<RespuestaFormulario>> GetRespuestasFoprmulario( Guid idUsuario, int idFormulario)
        {
            List<int> listaChaside = Enumerable.Range(1, 98).ToList();
            List<int> listaToulouse = Enumerable.Range(99, 107).ToList();
           
            if (idFormulario==0) {

                var respuestas = await respuestaFormularioBBDD.RespuestaFormularios.Where(r => r.IdUsuario==idUsuario).Where(r => listaChaside.Any(lC => lC == r.IdPregunta)).Select(
                   r => new RespuestaFormulario
                   {
                       IdUsuario = r.IdUsuario,
                       IdPregunta = r.IdPregunta,
                       Valor = r.Valor
                   }).ToListAsync();
                return respuestas;
            }
            else {
                var respuestas = await respuestaFormularioBBDD.RespuestaFormularios.Where(r => r.IdUsuario.Equals(idUsuario)).Where(r => listaToulouse.Any(lC => lC == r.IdPregunta)).Select(
               r => new RespuestaFormulario
               {
                   IdUsuario = r.IdUsuario,
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
        [HttpGet]
        [Route("Usuario/{idUsuario}/[controller]/Respuesta")]
        public async Task<IEnumerable<RespuestaFormulario>> GetRespuestas(Guid idUsuario)
        {
            var respuestas = await respuestaFormularioBBDD.RespuestaFormularios.Where(r => r.IdUsuario.Equals(idUsuario)).Select(
                r => new RespuestaFormulario
                {
                    IdUsuario= r.IdUsuario,
                    IdPregunta = r.IdPregunta,
                    Valor = r.Valor
                }).ToListAsync();

            return respuestas;


        }
        /// <summary>
        /// Crea todas las respuestas
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Usuario/{IdUsuario}/[controller]/Respuesta")]
        public async Task<ActionResult> CrearRespuestas(Guid IdUsuario, [FromBody]IEnumerable<AddRespuestaFormulario> RespuestaFormulariosBody)
        {
            try
            {
            

            foreach (AddRespuestaFormulario addRespuestaFormulario in RespuestaFormulariosBody) {
                    RespuestaFormulario respuestaFormulario = new()
                    {
                        IdPregunta = addRespuestaFormulario.IdPregunta,
                        Valor = addRespuestaFormulario.Valor,
                        IdUsuario = IdUsuario
                    };
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
        public async Task<ActionResult> DeleteRespuestas( Guid idUsuario, int idFormulario)
        {


            List<int> listaChaside = Enumerable.Range(1, 98).ToList();
            List<int> listaToulouse = Enumerable.Range(99, 107).ToList();
            List<RespuestaFormulario> respuestas;
            if (idFormulario == 0)
            {

                 respuestas = await respuestaFormularioBBDD.RespuestaFormularios.Where(r => r.IdUsuario == idUsuario).Where(r => listaChaside.Any(lC => lC == r.IdPregunta)).Select(
                   r => new RespuestaFormulario
                   {
                       IdUsuario = r.IdUsuario,
                       IdPregunta = r.IdPregunta,
                       Valor = r.Valor
                   }).ToListAsync();
             
            }
            else
            {
                 respuestas = await respuestaFormularioBBDD.RespuestaFormularios.Where(r => r.IdUsuario.Equals(idUsuario)).Where(r => listaToulouse.Any(lC => lC == r.IdPregunta)).Select(
               r => new RespuestaFormulario
               {
                   IdUsuario = r.IdUsuario,
                   IdPregunta = r.IdPregunta,
                   Valor = r.Valor
               }).ToListAsync();
         
            }
            if (respuestas.Count > 0)
            {

                foreach (RespuestaFormulario r in respuestas)
                {
                    respuestaFormularioBBDD.Remove(r);
                    await respuestaFormularioBBDD.SaveChangesAsync();

                }
           
                return Ok("ya estan borradas las respuestas del formulario");
            }
         

            return NotFound();
        }
  
        /// <summary>
        /// Actualiza las respuestas
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("Usuario/{idUsuario}/[controller]/{idFormulario}/Respuesta")]
        public async Task<ActionResult> UpdateRespuestas(Guid idUsuario, int idFormulario, [FromBody] IEnumerable<AddRespuestaFormulario> RespuestaFormulariosBody)
        {
            try
            {
                foreach (AddRespuestaFormulario respuesta in RespuestaFormulariosBody) {
                    var respuestaFormulario = await respuestaFormularioBBDD.RespuestaFormularios.FindAsync(idUsuario, respuesta.IdPregunta);

                    if(respuestaFormulario != null)
                    {
                        respuestaFormulario.Valor = respuesta.Valor;
                        await respuestaFormularioBBDD.SaveChangesAsync();
                    }            
                }
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(502);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="idFormulario"></param>
        /// <param name="RespuestaFormulariosBody"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Usuario/{idUsuario}/[controller]/{idFormulario}/Calcular")]
        public ActionResult FormularioCalcular(Guid idUsuario, int idFormulario, [FromBody] IEnumerable<RespuestaFormulario> RespuestaFormulariosBody)
        {
            string nombreFormulario;
            ValueTask<Usuario> usuarioResult = usuarioBBDD.Usuarios.FindAsync(idUsuario);
            InternalClass internalClass = new();
            if (usuarioResult.Result != null)
            {
                if (idFormulario == 0)
                {
                    internalClass.CalcularChasideAsync(usuarioResult.Result, RespuestaFormulariosBody);
                    nombreFormulario = "CHASIDE";
                }
                else
                {
                    internalClass.CalcularTolouseAsync(usuarioResult.Result, RespuestaFormulariosBody);
                    nombreFormulario = "TOULOUSE";
                }

            }
            else
            {
                return NotFound();
            }
            return Ok("Calculados las respuestas del formulario de " + nombreFormulario);

        }
    }
}
