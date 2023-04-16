using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Diagnostics;

namespace tfg_api.Utils
{
    
    public class UtilsMain 
    {
        
        /// <summary>
        /// Dirección IP del solicitador del servicio.
        /// </summary>
        protected string IP;

        /// <summary>
        /// URL del servicio solicitado.
        /// </summary>
        protected string URL;

        /// <summary>
        /// ID llamada.
        /// </summary>
        protected string ID_LOG;

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        protected string USER_NAME;

        /// <summary>
        /// Para saber si el WS es delegado o no
        /// </summary>
        public bool Delegated = false;
        public string GetIP() {
            return this.IP;
        }
        public string GetURL()
        {
            return this.URL;
        }
        public string GetID_LOG()
        {
            return this.ID_LOG;
        }
        public string GetUSER_NAME()
        {
            return this.USER_NAME;
        }
        public bool GetDelegated()
        {
            return this.Delegated;
        }

        public UtilsMain(HttpContext httpContext)
        {
            CapturarDatosLlamada( httpContext);
        }


        protected void CapturarDatosLlamada(HttpContext httpContext)
        {
        
            System.Text.Encoding utf_8 = System.Text.Encoding.UTF8;
            this.IP = httpContext.Request.Host.Value;
            this.URL = httpContext.Request.Path;
            Random generator = new Random();
            this.ID_LOG = Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
            this.USER_NAME = httpContext.User.Identity.Name;
        }

        /// <summary>
        /// Obtención de la lista de registros de log
        /// </summary>
        /// <param name="rutaFicheros"></param>
        /// <param name="anio"></param>
        /// <param name="mes"></param>
        /// <param name="dia"></param>
        /// <param name="userWS"></param>
        /// <param name="nameWS"></param>
        /// <param name="datosWS"></param>
        /// <returns>Listado de logs</returns>
        public  List<LogItem> GetRegistrosLog(string rutaFicheros, string anio = null, string mes = null, string dia = null, string userWS = null, string nameWS = null, string datosWS = null)
        {
            int numLogItem = 1;
            int linea = 0;
            string line = "";
            List<LogItem> listado = new List<LogItem>();
            DateTime hoy = DateTime.Now;
            LogItem log = null;
            bool bError = false;
            string error = "";
            string idEncontrado = null;
            var INI_LOG_LINE = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logs")["INI_LOG_LINE"];
            var FIN_LOG_LINE = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logs")["FIN_LOG_LINE"];
            var ERROR_LOG_LINE = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logs")["ERROR_LOG_LINE"];

            //Obtenemos los ficheros de la ruta
            var files = Directory.GetFiles(rutaFicheros);

            //Montamos los filtros por defecto de año, mes y día actual
            if (String.IsNullOrEmpty(anio))
            {
                anio = hoy.Year.ToString();
            }
            if (String.IsNullOrEmpty(mes))
            {
                mes = hoy.Month.ToString();
            }

            //Formateamos las fechas
            if (Int32.Parse(mes) < 10 && mes.Length == 1)
            {
                mes = "0" + mes;
            }

            //Si no viene nada buscamos sólo por mes y año
            if (!String.IsNullOrEmpty(dia))
            {
                if (Int32.Parse(dia) < 10 && dia.Length == 1)
                {
                    dia = "0" + dia;
                }
            }
            else
            {
                dia = "";
            }

            //Leemos los ficheros
            foreach (string f in files)
            {
                //Filtro por día, mes y año por el comienzo de la línea
                if (f.Contains(dia + "-" + mes + "-" + anio))
                {
                    linea = 0;
                    error = "";
                    System.IO.StreamReader file = new System.IO.StreamReader(f);
                    while ((line = file.ReadLine()) != null)
                    {
                        linea++;

                        //Sólo recuperamos tres tipos de líneas: Inicio, Fin y Error.
                        if (line.Contains(INI_LOG_LINE) || line.Contains(FIN_LOG_LINE) || line.Contains(ERROR_LOG_LINE))
                        {
                            //Tipo de línea
                            bool esInicio = (line.Contains(INI_LOG_LINE)) ? true : false;
                            bool esFinal = (line.Contains(FIN_LOG_LINE)) ? true : false;
                            bool esError = (line.Contains(ERROR_LOG_LINE)) ? true : false;

                            //Buscamos todos o por un dato en concreto
                            datosWS = null;
                            if (String.IsNullOrEmpty(datosWS) || (datosWS != null && line.Contains(datosWS) || (idEncontrado != null && line.Contains(idEncontrado.ToString()))))
                            {
                                //Patrones de búsqueda
                                //ID
                                int indiceID = (line.IndexOf("ID:") == -1) ? -1 : line.IndexOf("ID: ") + 4;
                                int indiceIDFin = line.IndexOf(",");
                                //WebService
                                int indiceWS = (line.IndexOf("URL: ") == -1) ? -1 : line.IndexOf("URL: ") + 5;
                                int indiceWSFin = (line.IndexOf("?") == -1) ? -1 : line.IndexOf("?");

                                if (indiceWSFin == -1)
                                {
                                    if (line.Contains("USER: "))
                                    {
                                        indiceWSFin = (line.IndexOf(" USER:") == -1) ? line.Length : line.IndexOf(" USER:");
                                    }
                                    else
                                    {
                                        indiceWSFin = (line.IndexOf(".FromBody:") == -1) ? line.Length : line.IndexOf(".FromBody:");
                                    }
                                }
                                else
                                {
                                    if (indiceWSFin <= indiceWS)
                                    {
                                        indiceWSFin = line.Length;
                                    }
                                }
                                //IP
                                int indiceIP = (line.IndexOf("IP: ") == -1) ? -1 : line.IndexOf("IP: ") + 4;
                                int indiceIPFin = (indiceWS - 5) - indiceIP;

                                //User
                                int indiceUser = (line.ToLower().IndexOf("p1=") == -1) ? -1 : line.ToLower().IndexOf("p1=") + 3;
                                int indiceUserFin = (indiceUser == -1) ? -1 : indiceUser + line.ToLower().Substring(indiceUser).IndexOf("&p");
                                if (line.Contains("USER: "))
                                {
                                    indiceUser = (line.IndexOf("USER: ") == -1) ? -1 : line.IndexOf("USER: ") + 6;
                                    indiceUserFin = indiceUser + line.Substring(indiceUser).IndexOf(".");
                                }


                                indiceUserFin = (indiceUserFin < indiceUser) ? line.Length : indiceUserFin;

                                //Resultados
                                int indiceResultados = 0;
                                int indiceResultadosFin = 0;
                                if (line.IndexOf("resultados: ") > 0)
                                {
                                    indiceResultados = line.IndexOf("resultados: ") + 12;
                                    indiceResultadosFin = (line.IndexOf(", IP:") == -1) ? line.Length : line.IndexOf(", IP:");
                                }

                                //Obtenemos los datos del registro del log generales
                                string register = line;
                                string file_name = f.Substring(f.LastIndexOf("\\") + 1);
                                //Fila del fichero de inicio y fin
                                int file_init_line = (esInicio) ? linea : 0;
                                int file_final_line = (esInicio) ? 0 : linea;
                                //Fecha y hora
                                string date = line.Substring(0, 10);
                                string init_hour = (esInicio) ? line.Substring(0, 24) : "";
                                string final_hour = (esInicio) ? "" : line.Substring(0, 24);

                                //ID
                                string id_log = (indiceID == -1) ? numLogItem.ToString() : line.Substring(indiceID, indiceIDFin - indiceID);

                                //IP 
                                string ip = (indiceIP == -1) ? "" : line.Substring(indiceIP, (indiceWS - 5) - indiceIP);

                                //Servicio                            
                                string service = (indiceWS == -1) ? "" : line.Substring(indiceWS, indiceWSFin - indiceWS);

                                //url
                                string url = (indiceWS == -1) ? "" : line.Substring(indiceWS);

                                //Usuario (si lo hay)
                                string user = (indiceUser == -1) ? "" : line.Substring(indiceUser, indiceUserFin - indiceUser);

                                //Resultados                           
                                string results = (!esFinal) ? "0" : String.IsNullOrEmpty(line.Substring(indiceResultados, indiceResultadosFin - indiceResultados)) ? "1" : line.Substring(indiceResultados, indiceResultadosFin - indiceResultados);

                                //Evaluamos si hay que insertar o no el registro
                                bool bInsertar = true;
                                if (!string.IsNullOrEmpty(userWS) && !user.Equals(userWS))
                                    bInsertar = false;
                                if (!string.IsNullOrEmpty(nameWS) && !service.Contains(nameWS))
                                    bInsertar = false;

                                //Para buscar un log
                                /*if (id_log == 894394)
                                    id_log = 894394;*/

                                //Comprobamos si tiene ID, operativa nueva
                                if (indiceID != -1)
                                {
                                    //Si no es un error 
                                    if (!esError)
                                    {
                                        //Excluimos los que no cumplan el nombre del servicio y los que no sean del usuario filtrado
                                        if (bInsertar)
                                        {
                                            //Objeto para el registro del log
                                            log = new LogItem(id_log, file_init_line, file_final_line, file_name, ip, user, register, service, results, date, init_hour, final_hour);

                                            //Comprobamos si ya existe el log en el registro actualizamos los datos finales
                                            if (!esInicio && listado.Exists(l => l.file.Equals(file_name) && l.ip.Equals(ip) && service.Contains(l.service) && l.id == id_log))
                                            {
                                                //obtenemos el log
                                                log = listado.Find(l => l.file.Equals(file_name) && l.ip.Equals(ip) && service.Contains(l.service) && l.id == id_log);

                                                //actualizamos los datos cuando tenemos el registro final
                                                log.file_final_line = file_final_line;
                                                log.results = results.IndexOf("|") == -1 && results.IndexOf(".") == -1 ? results : "1";
                                                log.final_hour = DateTime.Parse(final_hour);
                                                log.register_final = line.Replace("'", "");
                                                log.tiempoEjecucion = log.GetTiempoRespuesta();
                                                idEncontrado = null;
                                            }
                                            else
                                            {
                                                //Comprobamos si ya existe para no insertarlo de nuevo
                                                log = listado.Find(l => l.file.Equals(file_name) && service.Contains(l.service) && l.id == id_log);
                                                if (log == null && esInicio)
                                                {
                                                    //Objeto para el registro del log
                                                    log = new LogItem(id_log, file_init_line, file_final_line, file_name, ip, user, register, service, results, date, init_hour, final_hour);
                                                    listado.Add(log);
                                                    idEncontrado = id_log;
                                                }
                                                else if (esInicio)
                                                {
                                                    idEncontrado = id_log;
                                                }
                                            }
                                            numLogItem++;
                                        }
                                    }
                                    else //Log de error
                                    {
                                        //Obtenemos el error
                                        bError = true;
                                        int indiceError = (line.IndexOf(ERROR_LOG_LINE) == -1) ? 0 : line.IndexOf(ERROR_LOG_LINE) + ERROR_LOG_LINE.Length;
                                        int indiceErrorFin = (indiceIP == -1) ? line.Length : indiceIP - 4;
                                        error = line.Substring(indiceError, indiceErrorFin - indiceError);

                                        //Si hay error marcamos donde se ha producido el error
                                        if (listado.Exists(l => l.file.Equals(file_name) && l.id == id_log))
                                        {
                                            //obtenemos el log
                                            log = listado.Find(l => l.file.Equals(file_name) && l.id == id_log);

                                            //actualizamos los datos cuando tenemos el registro final
                                            log.file_final_line = file_final_line;
                                            log.results = "0";
                                            log.final_hour = DateTime.Parse(final_hour);
                                            log.tiempoEjecucion = log.GetTiempoRespuesta();
                                            log.error = bError;
                                            log.error_message = error.Replace("'", "");
                                            log.register_final = line.Replace("'", "");
                                            error = "";
                                            bError = false;
                                            idEncontrado = null;
                                        }
                                    }
                                }
                                else //Operativa antigua
                                {
                                    //Si no es un error 
                                    if (!esError)
                                    {
                                        //Excluimos los que no cumplan el nombre del servicio y los que no sean del usuario filtrado
                                        if (bInsertar)
                                        {
                                            //Objeto para el registro del log
                                            log = new LogItem(id_log, file_init_line, file_final_line, file_name, ip, user, register, service, results, date, init_hour, final_hour);

                                            //Comprobamos si ya existe el log en el registro
                                            if (!esInicio && listado.Exists(l => l.file.Equals(file_name) && l.ip.Equals(ip) && l.user.Equals(user) && service.Contains(l.service) && l.s_date.Equals(date) && l.file_final_line == 0))
                                            {
                                                //obtenemos el log
                                                log = listado.Find(l => l.file.Equals(file_name) && l.ip.Equals(ip) && l.user.Equals(user) && service.Contains(l.service) && l.s_date.Equals(date) && l.file_final_line == 0);

                                                //actualizamos los datos cuando tenemos el registro final
                                                log.file_final_line = file_final_line;
                                                log.results = results;
                                                log.final_hour = DateTime.Parse(final_hour);
                                                log.register_final = line;
                                                log.tiempoEjecucion = log.GetTiempoRespuesta();
                                            }
                                            else
                                            {
                                                //Objeto para el registro del log
                                                log = new LogItem(id_log, file_init_line, file_final_line, file_name, ip, user, register, service, results, date, init_hour, final_hour);
                                                listado.Add(log);
                                            }

                                            numLogItem++;
                                        }
                                    }
                                    else //Error
                                    {
                                        //Obtenemos el error
                                        bError = true;
                                        int indiceError = (line.IndexOf(ERROR_LOG_LINE) == -1) ? 0 : line.IndexOf(ERROR_LOG_LINE) + ERROR_LOG_LINE.Length;
                                        int indiceErrorFin = (indiceIP == -1) ? line.Length : indiceIP - 4;
                                        error = line.Substring(indiceError, indiceErrorFin - indiceError);

                                        //Si hay error marcamos donde se ha producido el error
                                        if (listado.Exists(l => l.file.Equals(file_name) && l.register.Contains(url) && l.ip.Equals(ip) && l.file_final_line == 0))
                                        {
                                            //obtenemos el log
                                            log = listado.Find(l => l.file.Equals(file_name) && l.register.Contains(url) && l.ip.Equals(ip) && l.file_final_line == 0);

                                            //actualizamos los datos cuando tenemos el registro final
                                            log.file_final_line = file_final_line;
                                            log.results = "0";
                                            log.final_hour = DateTime.Parse(final_hour);
                                            log.tiempoEjecucion = log.GetTiempoRespuesta();
                                            log.error = bError;
                                            log.error_message = error.Replace("'", "");
                                            log.register_final = line.Replace("'", "");
                                            error = "";
                                            bError = false;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //Se cierra el fichero de log
                    file.Close();
                }
            }

            //Devolvemos los resultados
            return listado;
        }

        #region "Login"
       
        public bool IsAuthorized(string Nombre = null, string Token = null)
        {
            if (Nombre == "admin" || Token == "admin") {
                return true;
            }
            else {
                return false;
            }
           

        }
        #endregion
    }

    public class LoginRequest
    {
        /// <summary>
        /// User
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }
    }


    #region "Logs"
    public class LogItem
    {
        /// <summary>
        /// id
        /// </summary>
        public string id { get; }
        /// <summary>
        /// Linea de inicio del log
        /// </summary>
        public int file_init_line { get; set; }
        /// <summary>
        /// Línea final del log
        /// </summary>
        public int file_final_line { get; set; }
        /// <summary>
        /// Fichero
        /// </summary>
        public string file { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// IP normalizada
        /// </summary>
        public string ip_view { get; set; }
        /// <summary>
        /// User
        /// </summary>
        public string user { get; set; }
        /// <summary>
        /// Registro
        /// </summary>
        public string register { get; set; }
        /// <summary>
        /// Registro final
        /// </summary>
        public string register_final { get; set; }
        /// <summary>
        /// Servicio
        /// </summary>
        public string service { get; set; }
        /// <summary>
        /// Resultados
        /// </summary>
        public string results { get; set; }
        /// <summary>
        /// Fecha
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// Fecha formateada
        /// </summary>
        public string s_date { get; set; }
        /// <summary>
        /// Hora inicio
        /// </summary>
        public DateTime init_hour { get; set; }
        /// <summary>
        /// Hora final
        /// </summary>
        public DateTime final_hour { get; set; }
        /// <summary>
        /// Tiempo de ejecución
        /// </summary>
        public string tiempoEjecucion { get; set; }
        /// <summary>
        /// Error
        /// </summary>
        public bool error { get; set; }
        /// <summary>
        /// Mensaje de error
        /// </summary>
        public string error_message { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="file_init_line"></param>
        /// <param name="file_final_line"></param>
        /// <param name="file"></param>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="register"></param>
        /// <param name="service"></param>
        /// <param name="results"></param>
        /// <param name="date"></param>
        /// <param name="init_hour"></param>
        /// <param name="final_hour"></param>
        public LogItem(string id, int file_init_line, int file_final_line, string file, string ip, string user, string register, string service, string results, string date, string init_hour, string final_hour)
        {
            this.id = id;
            this.file_init_line = file_init_line;
            this.file_final_line = file_final_line;
            this.file = file;
            this.ip = ip;
            this.ip_view = ip.Trim().Equals("::1") ? "localhost" : ip;
            this.user = user.Replace(".", "");
            this.register = register.Replace("'", "");
            this.service = service.Substring(service.LastIndexOf("/") + 1);
            this.results = String.IsNullOrEmpty(results) ? "1" : results.IndexOf("|") == -1 ? results : "1";
            this.date = DateTime.Parse(date);
            this.s_date = date;
            if (!String.IsNullOrEmpty(init_hour))
            {
                this.init_hour = DateTime.Parse(init_hour);
            }
            if (!String.IsNullOrEmpty(final_hour))
            {
                this.final_hour = DateTime.Parse(final_hour);
            }
            this.tiempoEjecucion = "0";
            this.error = false;
            this.error_message = null;
        }

        /// <summary>
        /// Obtener tiempo de ejecución
        /// </summary>
        /// <returns></returns>
        public string GetTiempoRespuesta()
        {
            string tiempo = "";

            TimeSpan result = final_hour.Subtract(init_hour);
            tiempo = Convert.ToString(result.TotalSeconds);

            return tiempo;
        }
    }
    #endregion

    #region "Token"

    /// <summary>
    /// JWT Token generator class using "secret-key"
    /// more info: https://self-issued.info/docs/draft-ietf-oauth-json-web-token.html
    /// </summary>
    internal static class TokenGenerator
    {

        public static string GenerateTokenJwt(string username)
        {

            var secretKey = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Key"];
            var audienceToken = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Audience"];
            var issuerToken = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Issuer"];
            var expireTime = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Expire"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }

    /// <summary>
    /// Token validator for Authorization Request using a DelegatingHandler
    /// </summary>
    internal class TokenValidationHandler : DelegatingHandler
    {
        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

    }

    #endregion

    public class InteracionBBDD { 
      public void CargarBBDD(string direccionScriptCarga)
        {
            ProcessStartInfo cmd = new ProcessStartInfo("sqlcmd", "-S (localDB)\\Local -i" + direccionScriptCarga)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,                
                
            };

            using Process process = new()
            {
                StartInfo = cmd
            };
            process.Start();
        }

    }
 
}
