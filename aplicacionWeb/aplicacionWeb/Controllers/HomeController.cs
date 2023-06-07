using aplicacionWeb.Model;
using aplicacionWeb.Model.Asignatura;
using aplicacionWeb.Model.AsignaturaUsuario;
using aplicacionWeb.Model.Interna;
using aplicacionWeb.Model.PreguntaFormulario;
using aplicacionWeb.Model.RespuestaFormulario;
using aplicacionWeb.Model.UsuarioContenedor;
using aplicacionWeb.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace aplicacionWeb.Controllers
{
    public class HomeController : Controller
    {
        private IServicio_API_Asignatura _servicioApiAsignatura;
        private IServicio_API_Formulario _servicio_API_Formulario;
        private IServicio_API_Usuario _servicio_API_Usuario;

        public HomeController(IServicio_API_Asignatura servicioApiAsignatura, IServicio_API_Formulario servicio_API_Formulario, IServicio_API_Usuario servicio_API_Usuario)
        {

            _servicioApiAsignatura = servicioApiAsignatura;
            _servicio_API_Formulario = servicio_API_Formulario;
            _servicio_API_Usuario = servicio_API_Usuario;
        }

        public IActionResult Index()
        {
            string cookieValueFromReq = Request.Cookies["token"].ToString();
            _servicioApiAsignatura.SetToken(cookieValueFromReq);
            _servicio_API_Formulario.SetToken(cookieValueFromReq);
            return View();
        }

        public IActionResult Asignaturas()
        {
            
            return View();
        }
        public IActionResult Chaside()
        {

            return View();
        }
        public IActionResult Toulouse()
        {

            return View();
        }
        public IActionResult Manual()
        {

            return View();
        }
        [HttpGet]
        public async Task<List<AsignaturaUsuarioGetSort>> GetDatosListaAsignaturaUsuario()
        {
            var b = await _servicioApiAsignatura.ListaAsignaturaUsuario();
            
            return b;
        }
        [HttpGet]
        public async Task<List<AsignaturaSort>> GetDatosListaAsignatura()
        {
            var b = await _servicioApiAsignatura.ListaAsignatura();

            return b;
        }
        [HttpGet]
        public async Task<AsignaturaUsuarioGet> InfoAsignaturaUsuario(string datos)
        {
            string url = datos.Split("asignaturas/")[1];
            var b = await _servicioApiAsignatura.Obtener(url);

            var c = await _servicio_API_Usuario.Recomendaciones();
            return b;
        }
        [HttpDelete]
        public async Task<Boolean> DeleteAsignaturaUsuario(string datos)
        {
            
            var b = await _servicioApiAsignatura.Eliminar(datos);

            return b;
        }
        [HttpPut]
        public async Task<Boolean> UpdateAsignaturaUsuario(string datos)
        {

            string[] contentDatos = datos.Split("€");
            string idAsignatura = contentDatos[0].ToUpper().Replace("-", "").ToString();
            string nota = contentDatos[1].Replace(".", "");
            string tiempoEstudio = contentDatos[2];
            string tiempoRecomendado = contentDatos[3];
            string riesgo = contentDatos[4];
           AsignaturaUsuarioUpdate asignaturaUsuarioUpdate = new()
            {
                Nota=Double.Parse(nota),
                TiempoEstudio=Int32.Parse(tiempoEstudio),
                TiempoRecomendado= Int32.Parse(tiempoRecomendado),
                Riesgo= Int32.Parse(riesgo)

            };
            bool b = await _servicioApiAsignatura.Editar(asignaturaUsuarioUpdate, idAsignatura);

            return b;
        }
        
        [HttpPost]
        public async Task<Boolean> AddAsignaturaUsuario(string datos) {

            string[] contentDatos = datos.Split("€");
            string idAsignatura = contentDatos[0];
            string nota = contentDatos[1];
            string tiempoEstudio = contentDatos[2];

            bool b = await _servicioApiAsignatura.Crear(idAsignatura.ToUpper().Replace("-", "").ToString(), nota.Replace(".", ""), tiempoEstudio);

            return b;
        }

        [HttpGet]
        public async Task<List<PreguntaFormulario>> GetDatosListaPreguntasChaside()
        {
            
            var b = await _servicio_API_Formulario.ListaPreguntaFormulario(0);

            return b;
        }

        [HttpPut]
        public async Task<Boolean> UpdateRespuestasChaside(string datos)
        {
          
            string[] contentDatos = datos.Split("€");
       
            var b = await _servicio_API_Formulario.GuardarRespuestas(contentDatos,0);

            return b;
        }

        [HttpGet]
        public async Task<List<PreguntaFormulario>> GetDatosListaPreguntasToulouse()
        {
            List<PreguntaFormulario> preguntasFinales= new();
            var b = await _servicio_API_Formulario.ListaPreguntaFormulario(1);
         
            b.Remove(b.LastOrDefault());

            for (int i = 0; i < 3; i++) {
                Random random = new Random();
                int randomNumber = random.Next(0, b.Count - 1);
                preguntasFinales.Add(b[randomNumber]);
                b.Remove(b[randomNumber]);
            }
           

            return preguntasFinales;
        }
        [HttpPut]
        public async Task<Boolean> UpdateRespuestasToulouse(string datos)
        {

            string[] contentDatos = datos.Split("€");

            var b = await _servicio_API_Formulario.GuardarRespuestas(contentDatos, 1);

            return b;
        }
        [HttpGet]
        public async Task<UsuarioGet> GetUsuarioInfo()
        {
            var b = await _servicio_API_Usuario.Obtener();

            return b;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}