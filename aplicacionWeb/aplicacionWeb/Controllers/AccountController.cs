using aplicacionWeb.Model;
using aplicacionWeb.Model.UsuarioContenedor;
using aplicacionWeb.Servicios;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace aplicacionWeb.Controllers
{
    public class AccountController : Controller
    {

        private IServicio_API_Usuario _servicioApiUsuario;

        public AccountController(IServicio_API_Usuario servicioApiUsuario)
            {

            _servicioApiUsuario = servicioApiUsuario;
        }

            public IActionResult Login()
            {
                return View();
            }
            public IActionResult Registro()
            {
                return View();
            }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(string usuario, string pass, string passRepeat)
        {
            bool respuesta=false;
            AddUsuarioRequest usuarioRequest = new()
            {
                Nombre = usuario,
                Pass = pass,
            };

            if (!usuario.Equals("")|| !pass.Equals("")|| pass!=passRepeat) {
                respuesta = _servicioApiUsuario.Guardar(usuarioRequest).Result;
            }

            if (respuesta)
                return RedirectToAction("Index");
            else
                return NoContent();

        }
        [HttpPost]
        public async Task<IActionResult> Autenticar(string usuario, string pass)
        {
            string respuesta = "";
            AddUsuarioRequest usuarioRequest = new()
            {
                Nombre = usuario,
                Pass = pass,
            };

            if (!usuario.Equals("") || !pass.Equals(""))
            {
                respuesta = _servicioApiUsuario.Autenticar(usuario, pass).Result;
            }

            if (respuesta != "") {

                var cookieOptions = new CookieOptions
                {
                   
                    Secure= true,
                    HttpOnly= true,
                    Path="/Home",
                    
                };
                
                Response.Cookies.Append("token", respuesta,cookieOptions);
                Response.Cookies.Append("usuario", usuario, cookieOptions);
                ViewBag.UsuarioName = usuario;
                return Redirect("~/Home/Index");
        }
                return NoContent();

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        
    }
}
