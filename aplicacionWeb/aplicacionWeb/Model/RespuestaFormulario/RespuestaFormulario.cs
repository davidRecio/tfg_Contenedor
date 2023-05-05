

namespace aplicacionWeb.Model.RespuestaFormulario
{
    /// <summary>
    /// clase de la respuesta del formulario
    /// </summary>

    public class RespuestaFormulario
    {

        /// <summary>
        /// referencia al usuario
        /// </summary>

        public Guid IdUsuario { get; set; }
        /// <summary>
        /// referencia a la pregunta
        /// </summary>

        public int IdPregunta { get; set; }
        /// <summary>
        /// la respuesta
        /// </summary>

        public string? Valor { get; set; }

        
    }

}
