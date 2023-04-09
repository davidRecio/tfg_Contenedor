using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tfg_api.Model.Asignatura_UsuarioBBDD
{
    /// <summary>
    /// clase de la asignatura-Usuario
    /// </summary>
    public class Asignatura_UsuarioBBDD
    {
        /// <summary>
        /// identificador de la asignatura
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdAsignatura { get; set; }
        /// <summary>
        /// referencia al usuario
        /// </summary>
        [Required, ForeignKey("IdUsuario")]
        public Guid IdUsuario { get; set; }
        /// <summary>
        /// Nota de la asignatura
        /// </summary>
        [StringLength(10)]
        public double? Nota { get; set; }
        /// <summary>
        /// nombre de la asignatura
        /// </summary>
        [Required, StringLength(50)]
        public string? Nombre { get; set; }
        /// <summary>
        /// Riesgo en referente a la nota sacada
        /// </summary>
        [StringLength(10)]
        public int Riesgo { get; set; }
        /// <summary>
        /// horas de estudio
        /// </summary>
        [StringLength(10)]
        public int TiempoEstudio { get; set; }
        /// <summary>
        /// horas de estudio recomendado
        /// </summary>
        [StringLength(10)]
        public int TiempoRecomendado { get; set; }
 

    }
}
