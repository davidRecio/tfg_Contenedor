﻿
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacionWeb.Model.AsignaturaUsuario
{
    /// <summary>
    /// clase de la asignatura-Usuario
    /// </summary>
    public class AsignaturaUsuario
    {
        /// <summary>
        /// identificador de la asignatura
        /// </summary>
        public Guid IdAsignatura { get; set; }
        /// <summary>
        /// referencia al usuario
        /// </summary>
        public Guid IdUsuario { get; set; }
        /// <summary>
        /// Nota de la asignatura
        /// </summary>
     
        public double Nota { get; set; }
        /// <summary>
        /// horas de estudio
        /// </summary>
        
        public int TiempoEstudio { get; set; }
        /// <summary>
        /// horas de estudio recomendado
        /// </summary>
      
        public int TiempoRecomendado { get; set; }
        /// <summary>
        /// Riesgo en referente a la nota sacada
        /// </summary>
        public int Riesgo { get; set; }


    }
}
