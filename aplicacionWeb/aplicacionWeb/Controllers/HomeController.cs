using aplicacionWeb.Model;
using aplicacionWeb.Model.Asignatura;
using aplicacionWeb.Model.AsignaturaUsuario;
using aplicacionWeb.Model.UsuarioContenedor;
using aplicacionWeb.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;


namespace aplicacionWeb.Controllers
{
    public class HomeController : Controller
    {
        private IServicio_API_Asignatura _servicioApiAsignatura;

        public HomeController(IServicio_API_Asignatura servicioApiAsignatura)
        {

            _servicioApiAsignatura = servicioApiAsignatura;
        }

        public IActionResult Index()
        {
            string cookieValueFromReq = Request.Cookies["token"].ToString();
            _servicioApiAsignatura.SetToken(cookieValueFromReq);
            return View();
        }

        public IActionResult Asignaturas()
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
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}