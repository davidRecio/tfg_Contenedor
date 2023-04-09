using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace tfg_api.Model.PreguntaFormulario
{
    /// <summary>
    /// clase de preguntas del formulario
    /// </summary>
    public class PreguntaFormulario
    {
        /// <summary>
        /// identificador de la pregunta
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPregunta { get; set; }
        /// <summary>
        /// contenido de la pregunta
        /// </summary>
        [Required, StringLength(50)]
        public string? Contenido { get; set; }
        /// <summary>
        /// url de la imagen asociada
        /// </summary>
        [Required, StringLength(200)]
        public string? Imagen_url { get; set; }
        /// <summary>
        /// tipo del formulario Toulose o CHASIDE
        /// </summary>
        [StringLength(10)]
        public string? Tipo { get; set; }
    }
}
