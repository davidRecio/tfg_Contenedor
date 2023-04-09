﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;
using System;
using tfg_api.Utils;
using Microsoft.AspNetCore.Authorization;


namespace tfg_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _env;
        #region - Constructores -
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public AdminController(IWebHostEnvironment env)
        {
            _env = env;

        }
        #endregion

        /// <summary>
        /// Servicio para probar el estado del API
        /// </summary>
        /// <returns>Ping del API</returns>
        [HttpGet]
        [Route("Ping")]
        [Authorize]
        public StatusCodeResult Ping()
        {
            UtilsMain utils = new UtilsMain(this.HttpContext);
            var ID_LOG = utils.GetID_LOG();
            var IP = utils.GetIP();
            var URL = utils.GetURL();
            var Delegated = utils.GetDelegated();
            var USER_NAME = utils.GetUSER_NAME();
            try
            {
                Logs.Trace("ID: " + ID_LOG + ", Inicio llamada WS, IP: " + IP + " URL: " + URL + " USER: " + USER_NAME, null, Delegated);

                Logs.Trace("ID: " + ID_LOG + ", Fin llamada WS, resultados: 1, IP: " + IP + " URL: " + URL, null, Delegated);
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                Logs.Error("ID: " + ID_LOG + ", Error ws: " + ex.Message + ", IP: " + IP + " URL: " + URL);
                throw ex;
            }
        }

        /// <summary>
        /// Servicio de obtención de los logs de llamada a los WS
        /// </summary>
        /// <param name="p1">Nombre del Servicio</param>
        /// <param name="p2">User</param>
        /// <param name="p3">Fecha (YYYY-MM-DD)</param>
        /// <param name="p4">Incluir admin</param>
        /// <param name="p5">Datos Identificativos</param>
        /// <returns>Lista de Logs</returns>
        [HttpGet]
        [Route("APILogs")]
        [Authorize]
        public List<LogItem> APILogs(string p1 = null, string p2 = null, string p3 = null, bool p4 = false, string p5=null)
        {
            UtilsMain utils = new UtilsMain(this.HttpContext);
            var ID_LOG = utils.GetID_LOG();
            var IP = utils.GetIP();
            var URL = utils.GetURL();
            var Delegated = utils.GetDelegated();
            List<LogItem> resultados = new List<LogItem>();
            var USER_NAME = utils.GetUSER_NAME();

            try
            {
                Logs.Trace("ID: " + ID_LOG + ", Inicio llamada WS, IP: " + IP + " URL: " + URL + " USER: " + USER_NAME, null, Delegated);
                //if (IsAuthorized())
                //{
                string anio = null;
                string mes = null;
                string dia = null;

                if (!String.IsNullOrEmpty(p3))
                {
                    if (p3.Length >= 4)
                    {
                        anio = p3.Substring(0, 4);
                    }

                    if (p3.Length >= 5)
                    {
                        mes = p3.Substring(5, 2);
                    }

                    if (p3.Length >= 8)
                    {
                        dia = p3.Substring(8, 2);
                    }
                }

                //Obtenemos los resultados
                resultados = utils.GetRegistrosLog(_env.ContentRootPath + "Logs", anio, mes, dia, p2, p1, p5);

                ////Si no tiene que incluir los de admin los quitamos 
                //if (!p4 && string.IsNullOrEmpty(p2))
                //    resultados = resultados.Where(r => !r.user.Equals(Constantes.WS_ADMIN)).ToList();
                //}

                Logs.Trace("ID: " + ID_LOG + ", Fin llamada WS, resultados: " + resultados.Count().ToString() + ", IP: " + IP + " URL: " + URL, null, Delegated);
                return resultados.OrderBy(l => l.s_date).ToList();
            }
            catch (Exception ex)
            {
                Logs.Error("ID: " + ID_LOG + ", Error ws: " + ex.Message + ", IP: " + IP + " URL: " + URL);
                throw ex;
            }
        }

        [HttpGet]
        [Route("BBDD/Inicializar")]
        [Authorize]
        public StatusCodeResult Inicializar()
        {
            try {
        

                InteracionBBDD interacionBBDD = new();
                interacionBBDD.CargarBBDD("C:\\Users\\david\\Documents\\GitHub\\TfGContenedor\\tfg_api\\SQL\\ScriptBBDD0.sql");
                Task.Delay(1000).Wait();
                interacionBBDD.CargarBBDD("C:\\Users\\david\\Documents\\GitHub\\TfGContenedor\\tfg_api\\SQL\\ScriptBBDD1.sql");
                Task.Delay(1000).Wait();
                interacionBBDD.CargarBBDD("C:\\Users\\david\\Documents\\GitHub\\TfGContenedor\\tfg_api\\SQL\\ScriptBBDD2.sql");
                Task.Delay(1000).Wait();
                interacionBBDD.CargarBBDD("C:\\Users\\david\\Documents\\GitHub\\TfGContenedor\\tfg_api\\SQL\\ScriptBBDD3.sql");
                Task.Delay(1000).Wait();
                interacionBBDD.CargarBBDD("C:\\Users\\david\\Documents\\GitHub\\TfGContenedor\\tfg_api\\SQL\\ScriptBBDD4.sql");
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                return StatusCode(304);
            }
            
        }
        
    }
}



