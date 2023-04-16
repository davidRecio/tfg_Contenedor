using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace tfg_api.Model.AsignaturaUsuario
{
    /// <summary>
    /// clase de la asignatura-Usuario
    /// </summary>
    [PrimaryKey(nameof(IdAsignatura), nameof(IdUsuario))]
    public class AsignaturaUsuario
    {
        /// <summary>
        /// identificador de la asignatura
        /// </summary>
        [Column(Order = 0)]
        public Guid IdAsignatura { get; set; }
        /// <summary>
        /// referencia al usuario
        /// </summary>
        [Column(Order = 1)]
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
