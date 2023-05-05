using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
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
        [Route("usuarios/{idUsuario}/asignaturas/{idAsignatura}")]

        public async Task<ActionResult> GetAsignaturasUsuario(Guid idUsuario,Guid idAsignatura)
        {

           
            AsignaturaUsuario nota = await asignaturaUsuarioBBDD.AsignaturaUsuarios.FindAsync(idAsignatura, idUsuario);

            AsignaturaUsuarioGet asignaturaUsuarioGet = new() {
                UrlAsignatura = new Uri(Request.GetEncodedUrl().Split("usuarios")[0] + "asignaturas/" + idAsignatura),
                TiempoEstudio=nota.TiempoEstudio,
                TiempoRecomendado=nota.TiempoRecomendado,
                Riesgo=nota.Riesgo,
                Nota=nota.Nota,
            };

            return Ok(asignaturaUsuarioGet);
        }
        /// <summary>
        /// Obtiene todos las asignaturas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("usuarios/{idUsuario}/asignaturas")]

        public async Task<ActionResult> GetAsignaturasUsuario(Guid idUsuario)
        {
            List<AsignaturaUsuarioGetSort> listAsignaturaUsuarioGetSort = new();
            List<AsignaturaUsuario> listaNotas = await asignaturaUsuarioBBDD.AsignaturaUsuarios.ToListAsync();

            foreach(AsignaturaUsuario asignaturaUsuario in listaNotas)
            {
                Asignatura asignatura = await asignaturaBBDD.Asignaturas.FindAsync(asignaturaUsuario.IdAsignatura);
                AsignaturaUsuarioGetSort asignaturaUsuarioGetSort = new()
                {
                   
                UrlAsignatura = new Uri(Request.GetEncodedUrl() + "/" + asignatura.IdAsignatura),
                    Riesgo=asignaturaUsuario.Riesgo,
                    Nombre=asignatura.Nombre,
                    Nota=asignaturaUsuario.Nota             
                };
                listAsignaturaUsuarioGetSort.Add(asignaturaUsuarioGetSort);

            }

            return Ok(listAsignaturaUsuarioGetSort);
        }
        /// <summary>
        /// Crea la asignatura del usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("usuarios/{idUsuario}/asignaturas/{idAsignatura}")]

        public async Task<ActionResult> CrearAsignaturasUsuario(Guid idUsuario,Guid idAsignatura,[FromBody] AsignaturaUsuarioCreate asignaturaUsuarioCreate)
        {

            AsignaturaUsuarioGet asignaturaUsuarioGet = new();
            if (idAsignatura != null && idUsuario != null)
            {
                if (!asignaturaBBDD.Asignaturas.FindAsync(idAsignatura).IsFaulted)
                {
                    AsignaturaUsuario asignaturaUsuario = new()
                    {
                        IdUsuario = idUsuario,
                        IdAsignatura = idAsignatura,
                        Nota = asignaturaUsuarioCreate.Nota,
                        TiempoEstudio = asignaturaUsuarioCreate.TiempoEstudio,

                    };
                     asignaturaUsuarioBBDD.AsignaturaUsuarios.Add(asignaturaUsuario);
                    asignaturaUsuarioBBDD.SaveChanges();


                    
                }
                else {
                    return NotFound(idAsignatura);
                }
                
            }
            return Ok(new Uri(Request.GetEncodedUrl() + "/" + idUsuario));
        }

        /// <summary>
        /// Actualiza la asignatura del usuario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAsignaturaRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("usuarios/{idUsuario}/asignaturas/{idAsignatura}")]
        public async Task<ActionResult> UpdateAsignatura( Guid idUsuario, Guid idAsignatura,[FromBody] AsignaturaUsuarioUpdate asignaturaUsuarioUpdate)
        {

            var asignaturaUsuario = await asignaturaUsuarioBBDD.AsignaturaUsuarios.FindAsync(idAsignatura, idUsuario);
               

            if (asignaturaUsuario != null)
            {

                asignaturaUsuario.Nota = asignaturaUsuarioUpdate.Nota;
                asignaturaUsuario.TiempoEstudio = asignaturaUsuarioUpdate.TiempoEstudio;
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
        [Route("usuarios/{idUsuario}/asignaturas/{idAsignatura}")]
        public async Task<ActionResult> DeleteAsignatura(Guid idUsuario, Guid idAsignatura)
        {
            var asignaturaUsuario = await asignaturaUsuarioBBDD.AsignaturaUsuarios.FindAsync(idAsignatura, idUsuario);

            if (asignaturaUsuario != null)
            {
                asignaturaUsuarioBBDD.Remove(asignaturaUsuario);
                await asignaturaUsuarioBBDD.SaveChangesAsync();
                return Ok(asignaturaUsuario);

            }
            return NotFound();
        }


        #region "asignatura"


        /// <summary>
        /// Obtiene todos las asignaturas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("asignaturas")]

        public async Task<ActionResult> GetAsignaturas()
        {
            List<AsignaturaSort> asignaturaSorts = new();
            List<Asignatura> listAsignaturas = await asignaturaBBDD.Asignaturas.ToListAsync();
            foreach(Asignatura asignatura in listAsignaturas)
            {
                asignaturaSorts.Add(new AsignaturaSort {Nombre=asignatura.Nombre,UriAsignatura=new Uri(Request.GetEncodedUrl()+"/"+asignatura.IdAsignatura) });
            }
            return Ok(asignaturaSorts);
        }


        /// <summary>
        ///  Función encargada de recibir una id de una asignatura y mostrar sus datos
        /// </summary>
        /// <param name="idAsignatura"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("asignaturas/{idAsignatura}")]
        public async Task<ActionResult> GetAsignatura(Guid idAsignatura)
        {
            var asignatura = await asignaturaBBDD.Asignaturas.FindAsync(idAsignatura);

            if (asignatura != null)
            {
                AsignaturaGet asignaturaGet = new()
                {
                    Nombre = asignatura.Nombre,
                    Tipo = asignatura.Tipo,
                };
                return Ok(asignaturaGet);

            }
            return NotFound();
        }
        /// <summary>
        /// Añade una asignatura
        /// </summary>
        /// <param name="addAsignaturaRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("asignaturas")]
        public async Task<ActionResult> AddAsignatura(AddAsignaturaRequest addAsignaturaRequest)
        {
            var asignatura = new Asignatura()
            {
                IdAsignatura = Guid.NewGuid(),
                Tipo = addAsignaturaRequest.Tipo,
                Nombre = addAsignaturaRequest.Nombre
            };

            await asignaturaBBDD.Asignaturas.AddAsync(asignatura);
            await asignaturaBBDD.SaveChangesAsync();

            return Ok(new Uri(Request.GetEncodedUrl() + "/" + asignatura.IdAsignatura));


        }
        /// <summary>
        /// Actualiza la asignatura
        /// </summary>
        /// <param name="idAsignatura"></param>
        /// <param name="updateAsignaturaRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("asignaturas/{idAsignatura}")]
        public async Task<ActionResult> UpdateAsignatura(Guid idAsignatura, [FromBody]UpdateAsignaturaRequest updateAsignaturaRequest)
        {

            var asignatura = await asignaturaBBDD.Asignaturas.FindAsync(idAsignatura);

            if (asignatura != null)
            {

                asignatura.Tipo = updateAsignaturaRequest.Tipo;
                asignatura.Nombre = updateAsignaturaRequest.Nombre;

                await asignaturaBBDD.SaveChangesAsync();

                return Ok(new Uri(Request.GetEncodedUrl() + "/" + idAsignatura));

            }
            return NotFound();

        }
        /// <summary>
        /// Borra la asignatura
        /// </summary>
        /// <param name="idAsignatura"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("asignaturas/{idAsignatura}")]
        public async Task<ActionResult> DeleteAsignatura(Guid idAsignatura)
        {
            var asignatura = await asignaturaBBDD.Asignaturas.FindAsync(idAsignatura);

            if (asignatura != null)
            {
                asignaturaBBDD.Remove(asignatura);
                await asignaturaBBDD.SaveChangesAsync();
                return Ok(asignatura);

            }
            return NotFound();
        }

        #endregion 

    }
}
