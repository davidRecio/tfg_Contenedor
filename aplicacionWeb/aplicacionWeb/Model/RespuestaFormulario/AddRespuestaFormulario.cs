using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacionWeb.Model.RespuestaFormulario
{
    /// <summary>
    /// clase de la respuesta del formulario
    /// </summary>
    public class AddRespuestaFormulario
    {
      
        /// <summary>
        /// referencia a la pregunta
        /// </summary>
        public int IdPregunta { get; set; }
        /// <summary>
        /// la respuesta
        /// </summary>
        [Required, StringLength(500)]
        public string? Valor { get; set; }

        
    }

}
