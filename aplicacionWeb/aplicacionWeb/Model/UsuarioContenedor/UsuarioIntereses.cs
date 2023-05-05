using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacionWeb.Model.UsuarioContenedor
{
    /// <summary>
    /// Clase Usuario
    /// </summary>
    public class UsuarioIntereses
    {

        /// <summary>
        /// intereses del usuario
        /// </summary>
        [ StringLength(10)]
        public int? Administrativas_Contables_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [ StringLength(10)]
        public int? Humanisticas_Sociales_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [ StringLength(10)]
        public int? Artisticas_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [ StringLength(10)]
        public int? Medicina_CsSalud_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [ StringLength(10)]
        public int? Ingenieria_Computacion_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [ StringLength(10)]
        public int? DefensaSeguridad_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [ StringLength(10)]
        public int? CienciasExactas_Agrarias_Int { get; set; }
      
    }
}
