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
    /// Controlador de la asignatura
    /// </summary>
    [ApiController]
    [Route("api")]
    public class AsignaturaController : Controller
    {
        private readonly AsignaturaBBDD asignaturaBBDD;
        private readonly AsignaturaUsuarioDDBB asignaturaUsuarioBBDD;
        private readonly AsignaturaAreaBBDD asignaturaAreaBBDD;
        private readonly AreaConocimientoBBDD areaConocimientoBBDD;
        private readonly UsuarioBBDD usuarioBBDD;
        /// <summary>
        /// Elemento que interactura como puente en base de datos y ruta
        /// </summary>
        /// <param name="asignaturaBBDD"></param>
        public AsignaturaController(AsignaturaBBDD asignaturaBBDD, AsignaturaUsuarioDDBB asignaturaUsuarioBBDD,
            UsuarioBBDD usuarioBBDD, AsignaturaAreaBBDD asignaturaAreaBBDD, AreaConocimientoBBDD areaConocimientoBBDD)
        {
            this.asignaturaBBDD = asignaturaBBDD;
            this.asignaturaUsuarioBBDD = asignaturaUsuarioBBDD;     
            this.asignaturaAreaBBDD= asignaturaAreaBBDD;
            this.areaConocimientoBBDD= areaConocimientoBBDD;
            this.usuarioBBDD=usuarioBBDD;
        }
        /// <summary>
        /// Obtiene todos las asignaturas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("usuarios/{idUsuario}/asignaturas/{idAsignatura}")]

        public async Task<ActionResult> GetAsignaturasUsuario(Guid idUsuario,Guid idAsignatura)
        {

           
            AsignaturaUsuario nota = await asignaturaUsuarioBBDD.AsignaturasUsuarios.FindAsync(idAsignatura, idUsuario);

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
            List<AsignaturaUsuario> listaNotas = await asignaturaUsuarioBBDD.AsignaturasUsuarios.ToListAsync();

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

            
            if (idAsignatura != null && idUsuario != null)
            {
                bool b = asignaturaBBDD.Asignaturas.FindAsync(idAsignatura).IsFaulted;
                if (b==false)
                {
                    AsignaturaUsuario asignaturaUsuario = new()
                    {
                        IdAsignatura = idAsignatura,
                        IdUsuario = idUsuario,
               
                        Nota = asignaturaUsuarioCreate.Nota,
                        TiempoEstudio = asignaturaUsuarioCreate.TiempoEstudio,
                        TiempoRecomendado=0,
                        Riesgo=0,

                    };
                    await asignaturaUsuarioBBDD.AsignaturasUsuarios.AddAsync(asignaturaUsuario);
                    await asignaturaUsuarioBBDD.SaveChangesAsync();
                    Usuario usuario=  await CalcularAptitudesAsignatura(idUsuario,idAsignatura,"crear");
                    usuarioBBDD.Usuarios.Update(usuario);
                    await usuarioBBDD.SaveChangesAsync();



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

            var asignaturaUsuario = await asignaturaUsuarioBBDD.AsignaturasUsuarios.FindAsync(idAsignatura, idUsuario);
               

            if (asignaturaUsuario != null)
            {

                try {
                    Usuario usuario = await CalcularAptitudesAsignatura(idUsuario, idAsignatura, "restar");
                    usuarioBBDD.Usuarios.Update(usuario);
                    await usuarioBBDD.SaveChangesAsync();


                    asignaturaUsuario.Nota = asignaturaUsuarioUpdate.Nota;
                    asignaturaUsuario.TiempoEstudio = asignaturaUsuarioUpdate.TiempoEstudio;
                    asignaturaUsuarioBBDD.Update(asignaturaUsuario);
                    usuario = await CalcularAptitudesAsignatura(idUsuario, idAsignatura, "crear");
                    usuarioBBDD.Usuarios.Update(usuario);
                    await usuarioBBDD.SaveChangesAsync();
                }
                catch (Exception e) {

                }
                
                await asignaturaUsuarioBBDD.SaveChangesAsync();
             
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
            var asignaturaUsuario = await asignaturaUsuarioBBDD.AsignaturasUsuarios.FindAsync(idAsignatura, idUsuario);

            if (asignaturaUsuario != null)
            {
            
                Usuario usuario = await CalcularAptitudesAsignatura(idUsuario, idAsignatura, "restar");
                usuarioBBDD.Usuarios.Update(usuario);
                await usuarioBBDD.SaveChangesAsync();
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
            string tipoArea = "";
            var asignatura = await asignaturaBBDD.Asignaturas.FindAsync(idAsignatura);
           
        
       
            if (asignatura != null)
            {
                var asignaturaAreaList = await asignaturaAreaBBDD.AsignaturasAreas.Where(p => p.IdAsignatura.Equals(idAsignatura)).ToListAsync();

                foreach (AsignaturaArea asignaturaArea in asignaturaAreaList)
                {

                    AreaConocimiento area = await areaConocimientoBBDD.AreasConocimientos.FindAsync(asignaturaArea.IdArea);
                    tipoArea = tipoArea + ", " + area.Nombre;

                }
                tipoArea=tipoArea.Substring(0, 1);
                AsignaturaCompleta asignaturaCompleta = new()
                {
                    Nombre = asignatura.Nombre,
                    Tipo = tipoArea,
                };

                return Ok(asignaturaCompleta);

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
            Guid guid = Guid.NewGuid();
            if (addAsignaturaRequest != null) {
  
                var asignatura = new Asignatura()
                {
                    IdAsignatura = guid,
                    Nombre = addAsignaturaRequest.Nombre
                };

                await asignaturaBBDD.Asignaturas.AddAsync(asignatura);
                await asignaturaBBDD.SaveChangesAsync();

                Array areasConocimientoAsignaturaArray = addAsignaturaRequest.Tipo.Split(",");
                foreach (string areasConocimientoAsignatura in areasConocimientoAsignaturaArray) {
                    AreaConocimiento areaConocimiento = await areaConocimientoBBDD.AreasConocimientos.Where(p => p.IdArea.Equals(areasConocimientoAsignatura)).FirstOrDefaultAsync();

                    var asignaturaArea = new AsignaturaArea()
                    {
                        IdArea = areaConocimiento.IdArea,
                        IdAsignatura = guid,
                    };

                    await asignaturaAreaBBDD.AsignaturasAreas.AddAsync(asignaturaArea);
                    await asignaturaAreaBBDD.SaveChangesAsync();
                }
             



            }
         


            return Ok(new Uri(Request.GetEncodedUrl() + "/" + guid));


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
                var asignaturaAreaList = await asignaturaAreaBBDD.AsignaturasAreas.Where(p => p.IdAsignatura.Equals(idAsignatura)).ToListAsync();

                foreach(AsignaturaArea asignaturaArea in asignaturaAreaList){

                    asignaturaAreaBBDD.AsignaturasAreas.Remove(asignaturaArea);
                    await asignaturaAreaBBDD.SaveChangesAsync();
                }

                Array areasConocimientoAsignaturaArray = updateAsignaturaRequest.Tipo.Split(",");
                foreach (string areasConocimientoAsignatura in areasConocimientoAsignaturaArray)
                {
                    AreaConocimiento areaConocimiento = await areaConocimientoBBDD.AreasConocimientos.Where(p => p.IdArea.Equals(areasConocimientoAsignatura)).FirstOrDefaultAsync();

                    var asignaturaArea = new AsignaturaArea()
                    {
                        IdArea = areaConocimiento.IdArea,
                        IdAsignatura = idAsignatura,
                    };

                    await asignaturaAreaBBDD.AsignaturasAreas.AddAsync(asignaturaArea);
                    await asignaturaAreaBBDD.SaveChangesAsync();
                }

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

        private async Task<Usuario> CalcularAptitudesAsignatura(Guid idUsuario, Guid idAsignatura,string formato)
        {
            List<AsignaturaArea> asignaturaAreaList= new();
            try { 
             asignaturaAreaList = await asignaturaAreaBBDD.AsignaturasAreas.Where(p => p.IdAsignatura.Equals(idAsignatura)).ToListAsync();

            }
            catch (Exception e) { }
            string tipoArea = "";
            ValueTask<Usuario> usuarioResult = usuarioBBDD.Usuarios.FindAsync(idUsuario);
            ValueTask<AsignaturaUsuario> asignaturaUsuario = asignaturaUsuarioBBDD.AsignaturasUsuarios.FindAsync(idAsignatura, idUsuario);
            InternalClass internalClass = new();
            foreach (AsignaturaArea asignaturaArea in asignaturaAreaList)
            {
                AreaConocimiento area = await areaConocimientoBBDD.AreasConocimientos.FindAsync(asignaturaArea.IdArea);
                tipoArea = tipoArea + ", " + area.Nombre;
            }
            //tipoArea = tipoArea.Substring(0, 1);
            Usuario usuarioResultante = internalClass.CalcularAptitudesAsignatura(usuarioResult.Result, asignaturaUsuario.Result, tipoArea, formato);
            if (formato.Equals("restar")) {
                usuarioResult.Result.Administrativas_Contables_Apt = usuarioResult.Result.Administrativas_Contables_Apt - usuarioResultante.Administrativas_Contables_Apt;
                usuarioResult.Result.Humanisticas_Sociales_Apt = usuarioResult.Result.Humanisticas_Sociales_Apt - usuarioResultante.Humanisticas_Sociales_Apt;
                usuarioResult.Result.Artisticas_Apt = usuarioResult.Result.Artisticas_Apt - usuarioResultante.Artisticas_Apt;
                usuarioResult.Result.Medicina_CsSalud_Apt = usuarioResult.Result.Medicina_CsSalud_Apt - usuarioResultante.Medicina_CsSalud_Apt;
                usuarioResult.Result.Ingenieria_Computacion_Apt = usuarioResult.Result.Ingenieria_Computacion_Apt - usuarioResultante.Ingenieria_Computacion_Apt;
                usuarioResult.Result.DefensaSeguridad_Apt = usuarioResult.Result.DefensaSeguridad_Apt -+usuarioResultante.DefensaSeguridad_Apt;
                usuarioResult.Result.CienciasExactas_Agrarias_Apt = usuarioResult.Result.CienciasExactas_Agrarias_Apt - usuarioResultante.CienciasExactas_Agrarias_Apt;
            
                return usuarioResult.Result;
            }
            else {
                return usuarioResultante;

            }
           
        }

    }
}
