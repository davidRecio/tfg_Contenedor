
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using NLog;
    using System;
    using System.Configuration;
    using System.Web;
using LogLevel = NLog.LogLevel;

namespace tfg_api.Utils
    {
        /// <summary>
        /// Clase logs
        /// </summary>
        public static class Logs
        {

            #region - Atributos -

            /// <summary>
            /// Gestor de logs Nlog
            /// </summary>
            private static Logger Logger;

            /// <summary>
            /// Log Trace.
            /// </summary>
            private static bool TraceLog;
            /// <summary>
            /// Log Debug.
            /// </summary>
            private static bool DebugLog;
            /// <summary>
            /// Log Info.
            /// </summary>
            private static bool InfoLog;
            /// <summary>
            /// Log Warn.
            /// </summary>
            private static bool WarnLog;
            /// <summary>
            /// Log Error.
            /// </summary>
            private static bool ErrorLog;
            /// <summary>
            /// Log Fatal.
            /// </summary>
            private static bool FatalLog;

            /// <summary>
            /// Mensaje de Log.
            /// </summary>
            private static string Message;

            /// <summary>
            /// Controlador desde el que se lanza el log.
            /// </summary>
            private static string Controller;
            /// <summary>
            /// Accsión desde la que se lanza el log.
            /// </summary>
            private static string Action;
            /// <summary>
            /// Usuario que lanza el log.
            /// </summary>
            private static string LoggedUserSessionVariable;

            #endregion

            /// <summary>
            /// Función de comprobación de existencia del fichero de Logs.
            /// </summary>
            private static void CheckFile()
            {
                string date = DateTime.Now.ToString().Split()[0].Replace('/', '-');
                var config = new NLog.Config.LoggingConfiguration();

                //Si es la conexión por SSL se crea otro fichero
                var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "Logs/Log_File_" + date + ".txt" };

                //if (HttpContext.Current != null && HttpContext.Current.Request.IsSecureConnection)
                //{
                //    logfile = new NLog.Targets.FileTarget("logfile") { FileName = "Logs/Log_File_" + date + "_SSL.txt" };
                //}

                logfile.Encoding = System.Text.Encoding.UTF8;

                var logconsole = new NLog.Targets.ConsoleTarget("logconsole");
                config.AddRule(LogLevel.Info, LogLevel.Fatal, logconsole);
                config.AddRule(LogLevel.Trace, LogLevel.Fatal, logfile);
                NLog.LogManager.Configuration = config;
            
                TraceLog = Boolean.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logs")["Trace"]);
                DebugLog = Boolean.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logs")["Debug"]);
                InfoLog = Boolean.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logs")["Info"]);
                WarnLog = Boolean.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logs")["Warn"]);
                ErrorLog = Boolean.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logs")["Error"]);
                FatalLog = Boolean.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Logs")["Fatal"]);
                Logger = NLog.LogManager.GetCurrentClassLogger();
                Message = "";
        }

            /// <summary>
            /// Trace
            /// </summary>
            /// <param name="Log">Mensaje Log.</param>
            /// <param name="FromBody">Json FromBody.</param>
            /// <param name="inactive">Activa o desactiva el log (para delagados)</param>
            public static void Trace(string Log, object FromBody = null, bool inactive = false)
            {
                if (!inactive)
                {
                    CheckFile();
                    if (TraceLog)
                    {
                        string bodyJson = "null";

                        if (FromBody != null)
                        {
                            bodyJson = JsonConvert.SerializeObject(FromBody).Replace("%2C", ",");
                        }

                        if (LoggedUserSessionVariable != null)
                        {
                            Message = Controller + "Controller." + Action + "|" + LoggedUserSessionVariable + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Trace(LoggedUserSessionVariable + "|" + Log + ".");
                        }
                        else
                        {
                            Message = Controller + "Controller." + Action + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Trace(Message);
                        }
                    }
                }
            }

            /// <summary>
            /// Debug
            /// <param name="Log">Mensaje Log.</param>
            /// <param name="FromBody">Json FromBody.</param>
            /// <param name="inactive"></param>
            /// </summary>
            public static void Debug(string Log, object FromBody = null, bool inactive = false)
            {
                if (!inactive)
                {
                    CheckFile();
                    if (DebugLog)
                    {
                        string bodyJson = "null";

                        if (FromBody != null)
                        {
                            bodyJson = JsonConvert.SerializeObject(FromBody).Replace("%2C", ",");
                        }

                        if (LoggedUserSessionVariable != null)
                        {
                            Message = Controller + "Controller." + Action + "|" + LoggedUserSessionVariable + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Debug(LoggedUserSessionVariable + "|" + Log + ".");
                        }
                        else
                        {
                            Message = Controller + "Controller." + Action + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Debug(Message);
                        }
                    }
                }
            }

            /// <summary>
            /// Info
            /// </summary>
            /// <param name="Log">Mensaje Log.</param>
            /// <param name="FromBody">Json FromBody.</param>
            /// <param name="inactive"></param>
            public static void Info(string Log, object FromBody = null, bool inactive = false)
            {
                if (!inactive)
                {
                    CheckFile();
                    if (InfoLog)
                    {
                        string bodyJson = "null";

                        if (FromBody != null)
                        {
                            bodyJson = JsonConvert.SerializeObject(FromBody).Replace("%2C", ",");
                        }

                        if (LoggedUserSessionVariable != null)
                        {
                            Message = Controller + "Controller." + Action + "|" + LoggedUserSessionVariable + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Info(LoggedUserSessionVariable + "|" + Log + ".");
                        }
                        else
                        {
                            Message = Controller + "Controller." + Action + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Info(Message);
                        }
                    }
                }
            }

            /// <summary>
            /// Warn
            /// </summary>
            /// <param name="Log">Mensaje Log.</param>
            /// <param name="FromBody">Json FromBody.</param>
            /// <param name="inactive"></param>
            public static void Warn(string Log, object FromBody = null, bool inactive = false)
            {
                if (!inactive)
                {
                    CheckFile();
                    if (WarnLog)
                    {
                        string bodyJson = "null";

                        if (FromBody != null)
                        {
                            bodyJson = JsonConvert.SerializeObject(FromBody).Replace("%2C", ",");
                        }

                        if (LoggedUserSessionVariable != null)
                        {
                            Message = Controller + "Controller." + Action + "|" + LoggedUserSessionVariable + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Warn(LoggedUserSessionVariable + "|" + Log + ".");
                        }
                        else
                        {
                            Message = Controller + "Controller." + Action + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Warn(Message);
                        }
                    }
                }
            }

            /// <summary>
            /// Error
            /// </summary>
            /// <param name="Log">Mensaje Log.</param>
            /// <param name="FromBody">Json FromBody.</param>
            /// <param name="inactive"></param>
            public static void Error(string Log, object FromBody = null, bool inactive = false)
            {
                if (!inactive)
                {
                    CheckFile();
                    if (ErrorLog)
                    {
                        string bodyJson = "null";

                        if (FromBody != null)
                        {
                            bodyJson = JsonConvert.SerializeObject(FromBody).Replace("%2C", ",");
                        }

                        if (LoggedUserSessionVariable != null)
                        {
                            Message = Controller + "Controller." + Action + "|" + LoggedUserSessionVariable + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Error(LoggedUserSessionVariable + "|" + Log + ".");
                        }
                        else
                        {
                            Message = Controller + "Controller." + Action + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Error(Message);
                        }
                    }
                }
            }

            /// <summary>
            /// Fatal
            /// </summary>
            /// <param name="Log">Mensaje Log.</param>
            /// <param name="FromBody">Json FromBody.</param>
            /// <param name="inactive"></param>
            public static void Fatal(string Log, object FromBody = null, bool inactive = false)
            {
                if (!inactive)
                {
                    CheckFile();
                    if (FatalLog)
                    {
                        string bodyJson = "null";

                        if (FromBody != null)
                        {
                            bodyJson = JsonConvert.SerializeObject(FromBody).Replace("%2C", ",");
                        }

                        if (LoggedUserSessionVariable != null)
                        {
                            Message = Controller + "Controller." + Action + "|" + LoggedUserSessionVariable + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Fatal(LoggedUserSessionVariable + "|" + Log + ".");
                        }
                        else
                        {
                            Message = Controller + "Controller." + Action + "|" + Log + "." + "FromBody: " + bodyJson;
                            Logger.Fatal(Message);
                        }
                    }
                }
            }

        }

    }

