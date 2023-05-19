using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using tfg_api.DDBB;
using tfg_api.Model.AreaConocimiento;
using tfg_api.Model.Asignatura;
using tfg_api.Model.AsignaturaArea;
using tfg_api.Model.AsignaturaUsuario;
using tfg_api.Model.Interna;
using tfg_api.Model.UsuarioContenedor;

namespace tfg_api.Controllers
{
    /// <summary>
    /// Exposición del recurso Usuario
    /// </summary>
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : Controller
    {
        private readonly UsuarioBBDD usuarioBBDD;
        private readonly AsignaturaBBDD asignaturaBBDD;
        private readonly AsignaturaUsuarioDDBB asignaturaUsuarioBBDD;
        private readonly AsignaturaAreaBBDD asignaturaAreaBBDD;
        private readonly AreaConocimientoBBDD areaConocimientoBBDD;
        /// <summary>
        /// Elemento que interactura como puente en base de datos y ruta
        /// </summary>
        /// <param name="usuarioBBDD"></param>
        public UsuarioController(UsuarioBBDD usuarioBBDD, AsignaturaBBDD asignaturaBBDD, AsignaturaUsuarioDDBB asignaturaUsuarioBBDD,
             AsignaturaAreaBBDD asignaturaAreaBBDD, AreaConocimientoBBDD areaConocimientoBBDD)
        {
            this.usuarioBBDD = usuarioBBDD;
            this.asignaturaBBDD = asignaturaBBDD;
            this.asignaturaUsuarioBBDD = asignaturaUsuarioBBDD;
            this.asignaturaAreaBBDD = asignaturaAreaBBDD;
            this.areaConocimientoBBDD = areaConocimientoBBDD;
        }

        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
   
        public async Task<ActionResult> GetUsuarios()
        {
            List<Usuario> listaUsuarios = await usuarioBBDD.Usuarios.ToListAsync();
            List<UsuarioGetSort> usuarioGetSorts = new();
            foreach (Usuario usuario in listaUsuarios) {
                UsuarioGetSort usuarioGetSort =new()  { 
                Nombre= usuario.Nombre,
                UriUsuario= new Uri(Request.GetEncodedUrl() + "/" + usuario.IdUsuario),
                };
                usuarioGetSorts.Add(usuarioGetSort);
            }
            
            return Ok(usuarioGetSorts);
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
            Usuario usuario = await usuarioBBDD.Usuarios.FindAsync(idUsuario);

            if (usuario != null)
            {
                UsuarioGet usuarioGet = new() {
                Nombre=usuario.Nombre,
                Administrativas_Contables_Apt=usuario.Administrativas_Contables_Apt,
                Administrativas_Contables_Int=usuario.Administrativas_Contables_Int,
                Artisticas_Apt=usuario.Artisticas_Apt,
                Artisticas_Int=usuario.Artisticas_Int,
                CienciasExactas_Agrarias_Apt=usuario.CienciasExactas_Agrarias_Int,
                CienciasExactas_Agrarias_Int=usuario.CienciasExactas_Agrarias_Int,
                Medicina_CsSalud_Apt=usuario.Medicina_CsSalud_Int,
                Medicina_CsSalud_Int=usuario.Medicina_CsSalud_Int,
                DefensaSeguridad_Apt=usuario.DefensaSeguridad_Apt,
                DefensaSeguridad_Int=usuario.DefensaSeguridad_Int,
                Humanisticas_Sociales_Apt=usuario.Humanisticas_Sociales_Apt,
                Humanisticas_Sociales_Int=usuario.Humanisticas_Sociales_Int,
                Ingenieria_Computacion_Apt=usuario.Ingenieria_Computacion_Apt,
                Ingenieria_Computacion_Int=usuario.Ingenieria_Computacion_Int,
                ICI=usuario.ICI,
                IGAP=usuario.IGAP,
                CC=usuario.CC,
                };
                return Ok(usuarioGet);

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

            AddUsuarioResponse usuarioresponse = new ()
            {
                Nombre = usuario.Nombre,
                url=Request.GetEncodedUrl() + "/" + usuario.IdUsuario
            };
            return Created(new Uri(Request.GetEncodedUrl() + "/" + usuario.IdUsuario), usuarioresponse);
            //return Ok(usuarioresponse);


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
                 usuarioBBDD.Update(usuario);
                await usuarioBBDD.SaveChangesAsync();
                UsuarioGetSort usuarioGetSort = new()
                {
                    Nombre = usuario.Nombre,
                    UriUsuario = new Uri(Request.GetEncodedUrl() ),
                };

                return Ok(usuarioGetSort);

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
      
        /// <summary>
        /// Actualiza recomendaciones segun las notas de todas las asignaturas
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{idUsuario}/recomendaciones")]
        public async Task<ActionResult> CalcularRecomendacionesAsignatura(Guid idUsuario)
        {

            string tipoArea = "";
            ValueTask<Usuario> usuarioResult = usuarioBBDD.Usuarios.FindAsync(idUsuario);
            InternalClass internalClass = new();
            if (usuarioResult.Result != null)
            {
                List<AsignaturaUsuario> listaAsignaturas = asignaturaUsuarioBBDD.AsignaturasUsuarios.Where(p => p.IdUsuario.Equals(idUsuario)).ToList();
                foreach (AsignaturaUsuario asignaturaUsuario in listaAsignaturas)
                {
                    var asignaturaAreaList = await asignaturaAreaBBDD.AsignaturasAreas.Where(p => p.IdAsignatura.Equals(asignaturaUsuario.IdAsignatura)).ToListAsync();


                    foreach (AsignaturaArea asignaturaArea in asignaturaAreaList)
                    {
                        AreaConocimiento area = await areaConocimientoBBDD.AreasConocimientos.FindAsync(asignaturaArea.IdArea);
                        tipoArea = tipoArea + ", " + area.Nombre;
                    }
                    tipoArea = tipoArea.Substring(1);

                    AsignaturaUsuario asignaturaResultado = internalClass.Recomendaciones(usuarioResult.Result, asignaturaUsuario, tipoArea);

                    asignaturaUsuarioBBDD.Update(asignaturaResultado);
                   await asignaturaUsuarioBBDD.SaveChangesAsync();
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
