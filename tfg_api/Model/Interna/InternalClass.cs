
using tfg_api.DDBB;
using tfg_api.Model.UsuarioContenedor;
using tfg_api.Model.AsignaturaUsuario;

namespace tfg_api.Model.Interna
{
    /// <summary>
    /// Clase encargada de gestionar los procesos internos
    /// </summary>
    public class InternalClass
    {

        // hay 8 figras y cada columna representa el numero de figuras que hay en cada fila de la figura1 a la 8
        private readonly int[,] resultadoToulouse = new int[,] {
            {5,3,5,5,3,3,3,3},
            {5,3,4,3,6,4,1,4},
            {4,6,4,4,2,3,3,4},
            {2,6,3,3,5,4,4,3},
            {5,4,3,3,3,3,5,4},
            {3,2,4,3,8,3,4,3},
            {2,0,2,4,7,5,5,5},
            {4,2,5,2,4,4,4,5},
            {2,3,3,5,5,4,3,5},
            {2,2,5,3,6,4,2,6},
            {4,2,6,4,4,2,5,3},
            {5,2,3,5,4,4,5,2},
            {5,2,2,2,5,5,5,4},
            {3,6,4,4,1,1,6,5},
            {4,3,5,4,4,3,4,3},
            {3,2,4,5,6,4,3,3},
            {5,2,3,6,4,4,2,4},
            {4,3,4,2,4,5,4,4},
            {5,4,4,4,2,2,4,5},
            {2,3,5,4,6,4,4,2},
            {4,1,2,5,3,4,7,4},
            {3,4,4,2,5,2,4,6},
            {3,4,4,4,5,4,3,3},
            {4,4,4,4,4,4,3,3},
            {4,3,3,5,5,4,4,2},
            {5,2,4,5,4,4,3,3},
            {3,4,3,4,2,5,5,4},
            {5,3,4,3,3,4,3,5},
            {3,4,5,4,4,3,3,4},
            {5,2,4,5,3,4,4,3},
            {3,4,4,3,5,3,3,5},
            {5,4,4,4,3,3,4,3},
            {3,4,3,5,5,4,3,3},
            {4,3,3,3,3,6,4,4},
            {4,2,4,3,5,3,4,5},
            {3,3,3,4,4,4,5,4},
            {3,2,4,3,5,5,4,4},
            {4,3,4,4,4,3,4,4},
            {4,2,4,4,4,4,5,3},
            {4,1,3,3,6,4,5,4},
            {150,119,151,152,171,148,156,153}
            };
        private readonly UsuarioBBDD? usuarioBBDD;


        /// <summary>
        /// calcula los resultados del formulario de CHASIDE
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="formularioRespuestas"></param>
        public async void CalcularChasideAsync(Usuario usuario, IEnumerable<RespuestaFormulario.RespuestaFormulario> formularioRespuestas)
        {
            usuario.Administrativas_Contables_Int = 0;
            usuario.Humanisticas_Sociales_Int = 0;
            usuario.Artisticas_Int = 0;
            usuario.Medicina_CsSalud_Int = 0;
            usuario.Ingenieria_Computacion_Int = 0;
            usuario.DefensaSeguridad_Int = 0;
            usuario.CienciasExactas_Agrarias_Int = 0;
            usuario.Administrativas_Contables_Apt = 0;
            usuario.Humanisticas_Sociales_Apt = 0;
            usuario.Artisticas_Apt = 0;
            usuario.Medicina_CsSalud_Apt = 0;
            usuario.Ingenieria_Computacion_Apt = 0;
            usuario.DefensaSeguridad_Apt = 0;
            usuario.CienciasExactas_Agrarias_Apt = 0;
            foreach (RespuestaFormulario.RespuestaFormulario rf in formularioRespuestas.ToArray())
            {

                if (rf.Valor.Equals("S"))
                {
                    if (rf.IdPregunta == 98 || rf.IdPregunta == 12 || rf.IdPregunta == 64 || rf.IdPregunta == 53 || rf.IdPregunta == 85 || rf.IdPregunta == 1 || rf.IdPregunta == 78 || rf.IdPregunta == 20 || rf.IdPregunta == 71 || rf.IdPregunta == 91)
                    {
                        usuario.Administrativas_Contables_Int += 1;

                    }
                    if (rf.IdPregunta == 9 || rf.IdPregunta == 34 || rf.IdPregunta == 80 || rf.IdPregunta == 25 || rf.IdPregunta == 95 || rf.IdPregunta == 67 || rf.IdPregunta == 41 || rf.IdPregunta == 74 || rf.IdPregunta == 56 || rf.IdPregunta == 89)
                    {
                        usuario.Humanisticas_Sociales_Int += 1;

                    }
                    if (rf.IdPregunta == 21 || rf.IdPregunta == 45 || rf.IdPregunta == 96 || rf.IdPregunta == 57 || rf.IdPregunta == 28 || rf.IdPregunta == 11 || rf.IdPregunta == 5 || rf.IdPregunta == 3 || rf.IdPregunta == 81 || rf.IdPregunta == 36)
                    {
                        usuario.Artisticas_Int += 1;

                    }
                    if (rf.IdPregunta == 33 || rf.IdPregunta == 92 || rf.IdPregunta == 70 || rf.IdPregunta == 8 || rf.IdPregunta == 87 || rf.IdPregunta == 62 || rf.IdPregunta == 23 || rf.IdPregunta == 44 || rf.IdPregunta == 16 || rf.IdPregunta == 52)
                    {
                        usuario.Medicina_CsSalud_Int += 1;

                    }
                    if (rf.IdPregunta == 75 || rf.IdPregunta == 6 || rf.IdPregunta == 19 || rf.IdPregunta == 38 || rf.IdPregunta == 60 || rf.IdPregunta == 27 || rf.IdPregunta == 83 || rf.IdPregunta == 54 || rf.IdPregunta == 47 || rf.IdPregunta == 97)
                    {
                        usuario.Ingenieria_Computacion_Int += 1;

                    }
                    if (rf.IdPregunta == 84 || rf.IdPregunta == 31 || rf.IdPregunta == 48 || rf.IdPregunta == 73 || rf.IdPregunta == 5 || rf.IdPregunta == 65 || rf.IdPregunta == 14 || rf.IdPregunta == 37 || rf.IdPregunta == 58 || rf.IdPregunta == 24)
                    {
                        usuario.DefensaSeguridad_Int += 1;

                    }
                    if (rf.IdPregunta == 77 || rf.IdPregunta == 42 || rf.IdPregunta == 88 || rf.IdPregunta == 17 || rf.IdPregunta == 93 || rf.IdPregunta == 32 || rf.IdPregunta == 68 || rf.IdPregunta == 49 || rf.IdPregunta == 35 || rf.IdPregunta == 61)
                    {
                        usuario.CienciasExactas_Agrarias_Int += 1;
                    }

                    //aptitudes

                    if (rf.IdPregunta == 15 || rf.IdPregunta == 51 || rf.IdPregunta == 2 || rf.IdPregunta == 46)
                    {
                        usuario.Administrativas_Contables_Apt += 1;

                    }
                    if (rf.IdPregunta == 63 || rf.IdPregunta == 30 || rf.IdPregunta == 72 || rf.IdPregunta == 86)
                    {
                        usuario.Humanisticas_Sociales_Apt += 1;


                    }
                    if (rf.IdPregunta == 22 || rf.IdPregunta == 39 || rf.IdPregunta == 76 || rf.IdPregunta == 82)
                    {
                        usuario.Artisticas_Apt += 1;

                    }
                    if (rf.IdPregunta == 69 || rf.IdPregunta == 40 || rf.IdPregunta == 29 || rf.IdPregunta == 4)
                    {
                        usuario.Medicina_CsSalud_Apt += 1;

                    }

                    if (rf.IdPregunta == 26 || rf.IdPregunta == 59 || rf.IdPregunta == 90 || rf.IdPregunta == 10)
                    {
                        usuario.Ingenieria_Computacion_Apt += 1;


                    }
                    if (rf.IdPregunta == 13 || rf.IdPregunta == 66 || rf.IdPregunta == 18 || rf.IdPregunta == 43)
                    {
                        usuario.DefensaSeguridad_Apt += 1;
                    }

                    if (rf.IdPregunta == 94 || rf.IdPregunta == 7 || rf.IdPregunta == 79 || rf.IdPregunta == 55)
                    {
                        usuario.CienciasExactas_Agrarias_Apt += 1;
                    }
                }
                await usuarioBBDD.SaveChangesAsync();


            }
        }

        /// <summary>
        /// calcula CC, IGAP, ICI a través de el formulario de Tolouse
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="formularioRespuestas"></param>
        public async void CalcularTolouseAsync(Usuario usuario, IEnumerable<RespuestaFormulario.RespuestaFormulario> formularioRespuestas)
        {

            usuario.CC = 0;
            usuario.ICI = 0;
            usuario.IGAP = 0;
            int omisionTotal = 0;
            int errorTotal = 0;
            int aciertosTotales = 0;
            String[] arrayFilas;
            String[] arrayColumnas;

            //CC = A – E / A + O
            //IGAP= ACIERTOS – (ERRORES + OMISIONES)
            //ICI = ACIERTOS – ERRORES / RESPUESTAS X 100

            //Donde: A es acierto, E es error y O es omisión

            foreach (RespuestaFormulario.RespuestaFormulario rf in formularioRespuestas.ToArray())
            {
                //formato de las respuestas 1,2,3,4,5,6,7,8;2,3,4,5,6,7,8,9;.....
                //cada columna representa la figuara, siendo la 1 la figura1 asi hasta la 8
                // en caso de ver -1 es saltable ya que no contempla esa figura
                // 39 filas en total + 1 que es la suma en total
                
                //separo en filas
                arrayFilas = rf.Valor.ToString().Split(";");
                int contadorFilas = 0;
                foreach (string fila in arrayFilas) {
                    int contadorColumnas = 0;
                    arrayColumnas = fila.ToString().Split(",");

                    foreach (string columna in arrayColumnas) { 
                      int respuestaUsuario = Int32.Parse(columna);
                      int respuestaPlantilla = Int32.Parse(resultadoToulouse[contadorFilas, contadorColumnas].ToString());

                        if (respuestaUsuario != -1)
                        {
                            if (respuestaUsuario < respuestaPlantilla)
                            {
                                omisionTotal += (respuestaPlantilla - respuestaUsuario);
                            }

                            if (respuestaUsuario > respuestaPlantilla)
                            {
                                errorTotal += (respuestaUsuario - respuestaPlantilla);
                            }
                            if (respuestaUsuario == respuestaPlantilla)
                            {
                                aciertosTotales += respuestaUsuario;
                            }
                        }
                        contadorColumnas += 1;

                    }
                    contadorFilas += 1;
                }

                usuario.CC = (aciertosTotales - errorTotal) / (aciertosTotales - omisionTotal);
                usuario.IGAP = aciertosTotales - (errorTotal - omisionTotal);
                usuario.ICI = ((aciertosTotales - errorTotal) / 39) * 100;
                await usuarioBBDD.SaveChangesAsync();

            }

        }

        /// <summary>
        /// recalcula aptitudes segun notas
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="asignatura"></param>
        /// <param name="tipo"></param>
        public async void CalcularAptitudesAsignatura(Usuario usuario, AsignaturaUsuario.AsignaturaUsuario asignatura, string tipo) {
            usuario.Administrativas_Contables_Apt = 0;
            usuario.Humanisticas_Sociales_Apt = 0;
            usuario.Artisticas_Apt = 0;
            usuario.Medicina_CsSalud_Apt = 0;
            usuario.Ingenieria_Computacion_Apt = 0;
            usuario.DefensaSeguridad_Apt = 0;
            usuario.CienciasExactas_Agrarias_Apt = 0;

            decimal nota = decimal.Parse(asignatura.Nota.ToString());
            switch (tipo) {
         
                case "Administrativas_Contables": {
                        if (nota > 5 && nota<=8) {
                            usuario.Administrativas_Contables_Apt++;
                        }
                        if (nota > 8 && nota <= 10){
                            usuario.Administrativas_Contables_Apt++;
                            usuario.Administrativas_Contables_Apt++;
                        }
                        if (nota > 2 && nota < 5) {
                            usuario.Administrativas_Contables_Apt--;
                        }
                        if ( nota <= 2)
                        {
                            usuario.Administrativas_Contables_Apt--;
                            usuario.Administrativas_Contables_Apt--;
                        }
                        break;
                    }
                case "Humanisticas_Sociales":
                    {
                        if (nota > 5 && nota <= 8)
                        {
                            usuario.Humanisticas_Sociales_Apt++;
                        }
                        if (nota > 8 && nota <= 10)
                        {
                            usuario.Humanisticas_Sociales_Apt++;
                            usuario.Humanisticas_Sociales_Apt++;
                        }
                        if (nota > 2 && nota < 5)
                        {
                            usuario.Humanisticas_Sociales_Apt--;
                        }
                        if (nota <= 2)
                        {
                            usuario.Humanisticas_Sociales_Apt--;
                            usuario.Humanisticas_Sociales_Apt--;
                        }
                        break;
                    }
                case "Artisticas":
                    {
                        if (nota > 5 && nota <= 8)
                        {
                            usuario.Artisticas_Apt++;
                        }
                        if (nota > 8 && nota <= 10)
                        {
                            usuario.Artisticas_Apt++;
                            usuario.Artisticas_Apt++;
                        }
                        if (nota > 2 && nota < 5)
                        {
                            usuario.Artisticas_Apt--;
                        }
                        if (nota <= 2)
                        {
                            usuario.Artisticas_Apt--;
                            usuario.Artisticas_Apt--;
                        }
                        break;
                    }
                case "Medicina_CsSalud":
                    {
                        if (nota > 5 && nota <= 8)
                        {
                            usuario.Medicina_CsSalud_Apt++;
                        }
                        if (nota > 8 && nota <= 10)
                        {
                            usuario.Medicina_CsSalud_Apt++;
                            usuario.Medicina_CsSalud_Apt++;
                        }
                        if (nota > 2 && nota < 5)
                        {
                            usuario.Medicina_CsSalud_Apt--;
                        }
                        if (nota <= 2)
                        {
                            usuario.Medicina_CsSalud_Apt--;
                            usuario.Medicina_CsSalud_Apt--;
                        }
                        break;
                    }
                case "Ingenieria_Computacion":
                    {
                        if (nota > 5 && nota <= 8)
                        {
                            usuario.Ingenieria_Computacion_Apt++;
                        }
                        if (nota > 8 && nota <= 10)
                        {
                            usuario.Ingenieria_Computacion_Apt++;
                            usuario.Ingenieria_Computacion_Apt++;
                        }
                        if (nota > 2 && nota < 5)
                        {
                            usuario.Ingenieria_Computacion_Apt--;
                        }
                        if (nota <= 2)
                        {
                            usuario.Ingenieria_Computacion_Apt--;
                            usuario.Ingenieria_Computacion_Apt--;
                        }
                        break;
                    }
                case "DefensaSeguridad":
                    {
                        if (nota > 5 && nota <= 8)
                        {
                            usuario.DefensaSeguridad_Apt++;
                        }
                        if (nota > 8 && nota <= 10)
                        {
                            usuario.DefensaSeguridad_Apt++;
                            usuario.DefensaSeguridad_Apt++;
                        }
                        if (nota > 2 && nota < 5)
                        {
                            usuario.DefensaSeguridad_Apt--;
                        }
                        if (nota <= 2)
                        {
                            usuario.DefensaSeguridad_Apt--;
                            usuario.DefensaSeguridad_Apt--;
                        }
                        break;
                    }
                case "CienciasExactas_Agrarias":
                    {
                        if (nota > 5 && nota <= 8)
                        {
                            usuario.CienciasExactas_Agrarias_Apt++;
                        }
                        if (nota > 8 && nota <= 10)
                        {
                            usuario.CienciasExactas_Agrarias_Apt++;
                            usuario.CienciasExactas_Agrarias_Apt++;
                        }
                        if (nota > 2 && nota < 5)
                        {
                            usuario.CienciasExactas_Agrarias_Apt--;
                        }
                        if (nota <= 2)
                        {
                            usuario.CienciasExactas_Agrarias_Apt--;
                            usuario.CienciasExactas_Agrarias_Apt--;
                        }
                        break;
                    }


            }
            await usuarioBBDD.SaveChangesAsync();


        }

        /// <summary>
        /// genera las recomendaciones
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="asignatura"></param>
        /// <param name="tipo"></param>
        public async void Recomendaciones(Usuario usuario, AsignaturaUsuario.AsignaturaUsuario asignatura, string tipo) {
            // tiempo recomendado por asignatura de manera semanal son 2 horas minimo  maximo 6
            // si tienes un interes bajo lo mas seguro es que abandones la carrera, por lo que aumenta el riesgo de no estudiar

            asignatura.Riesgo = 0;
            asignatura.TiempoRecomendado = 4;
            int aptitudAsignatura = 0;
            int interesAsignatura = 0;
            switch (tipo) {

                case "Administrativas_Contables":
                    {
                            aptitudAsignatura = Int32.Parse(usuario.Administrativas_Contables_Apt.ToString());
                        interesAsignatura = Int32.Parse(usuario.Administrativas_Contables_Int.ToString());
                            break;
                    }
                case "Humanisticas_Sociales":
                    {
                        aptitudAsignatura = Int32.Parse(usuario.Humanisticas_Sociales_Apt.ToString());
                        interesAsignatura = Int32.Parse(usuario.Humanisticas_Sociales_Int.ToString());
                        break;
                    }
                case "Artisticas":
                    {
                            aptitudAsignatura = Int32.Parse(usuario.Artisticas_Apt.ToString());
                        interesAsignatura = Int32.Parse(usuario.Artisticas_Int.ToString());
                        break;
                    }
                case "Medicina_CsSalud":
                    {
                            aptitudAsignatura = Int32.Parse(usuario.Medicina_CsSalud_Apt.ToString());
                        interesAsignatura = Int32.Parse(usuario.Medicina_CsSalud_Int.ToString());
                        break;
                        }
                case "Ingenieria_Computacion":
                    {
                        aptitudAsignatura = Int32.Parse(usuario.Ingenieria_Computacion_Apt.ToString());
                        interesAsignatura = Int32.Parse(usuario.Ingenieria_Computacion_Int.ToString());
                        break;
                    }
                case "DefensaSeguridad":
                    {                   
                            aptitudAsignatura = Int32.Parse(usuario.DefensaSeguridad_Apt.ToString());
                        interesAsignatura = Int32.Parse(usuario.DefensaSeguridad_Int.ToString());
                        break;
                    }
                case "CienciasExactas_Agrarias":
                    {
                        aptitudAsignatura =Int32.Parse(usuario.CienciasExactas_Agrarias_Apt.ToString());
                        interesAsignatura = Int32.Parse(usuario.CienciasExactas_Agrarias_Int.ToString());
                        break;
                    }
            
            }
            #region "tiempo de partida"
            // si le dedicas tiempo en esceso puede o saturar o quitar tiempo para otras, y si es menor puede que no le dediques suficiente
            if (asignatura.TiempoEstudio >6 || asignatura.TiempoEstudio < 2)
            {
                asignatura.Riesgo++;
            }
            #endregion
            #region "interes"
            if (interesAsignatura > 6)
            {
                asignatura.Riesgo--;
            }
            if (interesAsignatura > 8)
            {
                asignatura.Riesgo--;
            }
            if (interesAsignatura < 5) {
                asignatura.Riesgo++;
            }
            if (interesAsignatura < 2)
            {
                asignatura.Riesgo++;
            }
            #endregion
            #region "aptitud"
            if (aptitudAsignatura < 2) {
                asignatura.TiempoRecomendado++;
            }
            if (aptitudAsignatura < 0)
            {
                asignatura.TiempoRecomendado++;
            }
            #endregion
            #region "notas"
            if (asignatura.Nota > 5 && asignatura.Nota <= 8)
            {
                asignatura.Riesgo--;
            }
            if (asignatura.Nota > 8 && asignatura.Nota <= 10)
            {
                asignatura.TiempoRecomendado--;
            }
            if (asignatura.Nota > 3 && asignatura.Nota < 5)
            {
                asignatura.Riesgo++;
            }
            if (asignatura.Nota <= 2)
            {
                asignatura.TiempoRecomendado++;
            }
            #endregion
            #region "concentracion, atencion/retencion e impulsividad"
            //capacidad de aprovechar el tiempo de estudio sin distraerse
            if (double.Parse(usuario.CC.ToString()) > 0.6)
            {
                asignatura.TiempoRecomendado--;
            }
            else if (double.Parse(usuario.CC.ToString()) < 0.49)
            {        
                asignatura.TiempoRecomendado++;
            }

            if (Int32.Parse(usuario.IGAP.ToString()) > 0) {
                asignatura.TiempoRecomendado--;
            }
            if (Int32.Parse(usuario.IGAP.ToString()) < 0)
            {
                asignatura.TiempoRecomendado++;
            }

            if (double.Parse(usuario.ICI.ToString()) > 0.6 && interesAsignatura>6 ){
                asignatura.Riesgo--;
            }
            if (double.Parse(usuario.ICI.ToString()) > 0.6 && interesAsignatura < 4)
            {
                asignatura.Riesgo++;
            }
            #endregion
            #region"tiempo final"
            // tiempo recomendado por asignatura de manera semanal son 2 horas minimo  maximo 6
            if (asignatura.TiempoRecomendado > 6) {
                asignatura.TiempoRecomendado = 6;
                asignatura.Riesgo++;
            }
            if (asignatura.TiempoRecomendado < 2)
            {
                asignatura.TiempoRecomendado = 2;
                asignatura.Riesgo--;
            }
            #endregion

            if (asignatura.Riesgo > 10) {
                asignatura.Riesgo = 10;
            }
            if (asignatura.Riesgo < 0) {
                asignatura.Riesgo = 0;
            }
            await usuarioBBDD.SaveChangesAsync();
        }


    }
}
