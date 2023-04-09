using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tfg_api.Model.RespuestaFormulario
{
    /// <summary>
    /// clase de la respuesta del formulario
    /// </summary>
    public class RespuestaFormulario
    {
        /// <summary>
        /// referencia al usuario
        /// </summary>
        [Key, Column(Order = 0)]
        public Guid IdUsuario { get; set; }
        /// <summary>
        /// referencia a la pregunta
        /// </summary>
        [Key, Column(Order = 1)]
        public int IdPregunta { get; set; }
        /// <summary>
        /// la respuesta
        /// </summary>
        [Required, StringLength(500)]
        public string? Valor { get; set; }

        
    }

}
