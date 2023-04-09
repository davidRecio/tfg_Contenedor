using Microsoft.AspNetCore.Mvc;
using tfg_api.Utils;


namespace tfg_api.Controllers
{
    /// <summary>
    /// Exposición del recurso Usuario
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
    /// <summary>
    /// Genera el token
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Autenticar(LoginRequest login)
        {
            bool isCredentialValid = false;
            UtilsMain utils = new UtilsMain(this.HttpContext);
           var ID_LOG = utils.GetID_LOG();
            var IP = utils.GetIP();
            var URL = utils.GetURL();
            var Delegated = utils.GetDelegated();


            try
            {
                Logs.Trace("ID: " + ID_LOG + ", Inicio llamada WS, IP: " + IP + " URL: " + URL + " USER: " + login.Username, null, Delegated);
                if (utils.IsAuthorized(login.Username, login.Password))
                {
                    isCredentialValid = true;
                }

                Logs.Trace("ID: " + ID_LOG + ", Fin llamada WS, resultados: 1, IP: " + IP + " URL: " + URL, null, Delegated);

                if (isCredentialValid)
                {
                    var token = TokenGenerator.GenerateTokenJwt(login.Username);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception ex)
            {
                Logs.Error("ID: " + ID_LOG + ", Error ws: " + ex.Message + ", IP: " + IP + " URL: " + URL);
                throw ex;
            }
        }
       
    }
}


