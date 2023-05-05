using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacionWeb.Model.UsuarioContenedor
{
    /// <summary>
    /// Clase Usuario
    /// </summary>
    public class UsuarioAptitudes
    {

        /// <summary>
        /// aptitudes del usuario
        /// </summary>
        [StringLength(10)]
        public int? Administrativas_Contables_Apt { get; set; }
        /// <summary>
        /// aptitudes del usuario
        /// </summary>
        [StringLength(10)]
        public int? Humanisticas_Sociales_Apt { get; set; }
        /// <summary>
        /// aptitudes del usuario
        /// </summary>
        [StringLength(10)]
        public int? Artisticas_Apt { get; set; }
        /// <summary>
        /// aptitudes del usuario
        /// </summary>
        [StringLength(10)]
        public int? Medicina_CsSalud_Apt { get; set; }
        /// <summary>
        /// aptitudes del usuario
        /// </summary>
        [StringLength(10)]
        public int? Ingenieria_Computacion_Apt { get; set; }
        /// <summary>
        /// aptitudes del usuario
        /// </summary>
        [StringLength(10)]
        public int? DefensaSeguridad_Apt { get; set; }
        /// <summary>
        /// aptitudes del usuario
        /// </summary>
        [StringLength(10)]
        public int? CienciasExactas_Agrarias_Apt { get; set; }
    }
}
