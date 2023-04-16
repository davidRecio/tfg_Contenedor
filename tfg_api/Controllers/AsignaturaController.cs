using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tfg_api.DDBB;
using tfg_api.Model.Asignatura;
using tfg_api.Model.AsignaturaUsuario;
using tfg_api.Model.Interna;
using tfg_api.Model.UsuarioContenedor;

namespace tfg_api.Controllers
{
    /// <summary>
    /// Controlador de la asignatura
    /// </summary>
    [ApiController]
    [Route("api")]
    public class AsignaturaController : Controller
    {
        private readonly AsignaturaBBDD asignaturaBBDD;
        private readonly AsignaturaUsuarioDDBB asignaturaUsuarioBBDD;
        private readonly UsuarioBBDD usuarioBBDD;
        /// <summary>
        /// Elemento que interactura como puente en base de datos y ruta
        /// </summary>
        /// <param name="asignaturaBBDD"></param>
        public AsignaturaController(AsignaturaBBDD asignaturaBBDD, AsignaturaUsuarioDDBB asignaturaUsuarioBBDD, UsuarioBBDD usuarioBBDD)
        {
            this.asignaturaBBDD = asignaturaBBDD;
            this.asignaturaUsuarioBBDD = asignaturaUsuarioBBDD;
            this.usuarioBBDD = usuarioBBDD;
        }

        /// <summary>
        /// Obtiene todos las asignaturas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Usuario/{idUsuario}/[controller]")]

        public async Task<ActionResult> GetAsignaturasUsuario(Guid idUsuario)
        {
            List<AsignaturaCompleta> asignaturaCompletas = new();
            List<AsignaturaUsuario> listaNotas = await asignaturaUsuarioBBDD.AsignaturaUsuarios.ToListAsync();

            foreach(AsignaturaUsuario asignaturaUsuario in listaNotas)
            {
                ValueTask<Asignatura> asig =  asignaturaBBDD.Asignaturas.FindAsync(asignaturaUsuario.IdAsignatura);
                AsignaturaCompleta asignaturaCompleta = new()
                {
                    IdAsignatura = asignaturaUsuario.IdAsignatura,
                    IdUsuario = asignaturaUsuario.IdUsuario,
                    Nombre = asig.Result.Nombre,
                    Tipo = asig.Result.Tipo,
                    Nota = asignaturaUsuario.Nota,
                    Riesgo = asignaturaUsuario.Riesgo,
                    TiempoEstudio = asignaturaUsuario.TiempoEstudio,
                    TiempoRecomendado = asignaturaUsuario.TiempoRecomendado
                };
                asignaturaCompletas.Add(asignaturaCompleta);

            }

            return Ok(asignaturaCompletas);
        }
        /// <summary>
        /// Crea la asignatura del usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Usuario/{idUsuario}/[controller]")]

        public async Task<ActionResult> CrearAsignaturasUsuario(Guid idUsuario, AsignaturaCompleta asignaturaCompleta)
        {

            Guid IdAsignatura = Guid.NewGuid();
            if (asignaturaCompleta != null)
            {
                if (!asignaturaBBDD.Asignaturas.FindAsync(asignaturaCompleta.IdAsignatura).IsFaulted)
                {
                    AsignaturaUsuario asignaturaUsuario = new()
                    {
                        IdUsuario = idUsuario,
                        IdAsignatura = IdAsignatura,
                        Nota = asignaturaCompleta.Nota,
                        TiempoEstudio = asignaturaCompleta.TiempoEstudio,

                    };
                     asignaturaUsuarioBBDD.AsignaturaUsuarios.Add(asignaturaUsuario);
                    asignaturaUsuarioBBDD.SaveChanges();
                   
                }
                else {
                    return NotFound(asignaturaCompleta);
                }
                
            }
            return Ok(asignaturaCompleta);
        }

        /// <summary>
        /// Actualiza la asignatura del usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAsignaturaRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Usuario/{idUsuario}/[controller]")]
        public async Task<ActionResult> UpdateAsignatura( Guid idUsuario, AsignaturaCompleta asignaturaCompleta)
        {

            var asignaturaUsuario = await asignaturaUsuarioBBDD.AsignaturaUsuarios.FindAsync(asignaturaCompleta.IdAsignatura, idUsuario);
               

            if (asignaturaUsuario != null)
            {

                asignaturaUsuario.Nota = asignaturaCompleta.Nota;
                asignaturaUsuario.TiempoEstudio = asignaturaCompleta.TiempoEstudio;
                await asignaturaBBDD.SaveChangesAsync();

                return Ok(asignaturaUsuario);

            }
            return NotFound();

        }
        /// <summary>
        /// Borra la asignatura del usuario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Usuario/{idUsuario}/[controller]")]
        public async Task<ActionResult> DeleteAsignatura(Guid idUsuario, AsignaturaCompleta asignaturaCompleta)
        {
            var asignaturaUsuario = await asignaturaUsuarioBBDD.AsignaturaUsuarios.FindAsync(asignaturaCompleta.IdAsignatura, idUsuario);

            if (asignaturaUsuario != null)
            {
                asignaturaUsuarioBBDD.Remove(asignaturaUsuario);
                await asignaturaUsuarioBBDD.SaveChangesAsync();
                return Ok(asignaturaUsuario);

            }
            return NotFound();
        }
        /// <summary>
        /// Actualiza aptitudes segun las notas de todas las asignaturas
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Usuario/{idUsuario}/[controller]/Aptitudes")]
        public  ActionResult CalcularAptitudesAsignatura(Guid idUsuario)
        {
            ValueTask<Usuario> usuarioResult = usuarioBBDD.Usuarios.FindAsync(idUsuario);
            InternalClass internalClass = new();
            if (usuarioResult.Result != null)
            {
                List<AsignaturaUsuario> listaAsignaturas = asignaturaUsuarioBBDD.AsignaturaUsuarios.Where(p => p.IdUsuario.Equals(idUsuario)).ToList();
                foreach (AsignaturaUsuario asignaturaUsuario in listaAsignaturas)
                {
                    ValueTask<Asignatura> asignatura = asignaturaBBDD.Asignaturas.FindAsync(asignaturaUsuario.IdAsignatura);
                    internalClass.CalcularAptitudesAsignatura(usuarioResult.Result, asignaturaUsuario, asignatura.Result.Tipo);
                }
                return Ok("Ya se han actulaizado las aptitudes según las asignaturas");
            }
            else {
                return NotFound();
            }
          
        }
        /// <summary>
        /// Actualiza recomendaciones segun las notas de todas las asignaturas
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Usuario/{idUsuario}/[controller]/Recomendaciones")]
        public ActionResult CalcularRecomendacionesAsignatura(Guid idUsuario)
        {
            ValueTask<Usuario> usuarioResult = usuarioBBDD.Usuarios.FindAsync(idUsuario);
            InternalClass internalClass = new();
            if (usuarioResult.Result != null)
            {
                List<AsignaturaUsuario> listaAsignaturas = asignaturaUsuarioBBDD.AsignaturaUsuarios.Where(p => p.IdUsuario.Equals(idUsuario)).ToList();
                foreach (AsignaturaUsuario asignaturaUsuario in listaAsignaturas)
                {
                    ValueTask<Asignatura> asignatura = asignaturaBBDD.Asignaturas.FindAsync(asignaturaUsuario.IdAsignatura);
                    internalClass.Recomendaciones(usuarioResult.Result, asignaturaUsuario, asignatura.Result.Tipo);
                }
                return Ok("Ya se han actulaizado las aptitudes según las asignaturas");
            }
            else
            {
                return NotFound();
            }

        }
    }
}
