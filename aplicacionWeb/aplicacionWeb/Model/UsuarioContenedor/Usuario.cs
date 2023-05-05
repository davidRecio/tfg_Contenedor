using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacionWeb.Model.UsuarioContenedor
{
    /// <summary>
    /// Clase Usuario
    /// </summary>
    public class Usuario
    {
        /// <summary>
        /// identificador de usuario de tipo Guide
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdUsuario { get; set; }
        /// <summary>
        /// nombre del usuario
        /// </summary>
        [Required, StringLength(50)]
        public string? Nombre { get; set; }
        /// <summary>
        /// contraseña del usuario
        /// </summary>
        [Required, StringLength(50)]
        public string? Pass { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [StringLength(10)]
        public int? Administrativas_Contables_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [StringLength(10)]
        public int? Humanisticas_Sociales_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [StringLength(10)]
        public int? Artisticas_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [StringLength(10)]
        public int? Medicina_CsSalud_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [StringLength(10)]
        public int? Ingenieria_Computacion_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [StringLength(10)]
        public int? DefensaSeguridad_Int { get; set; }
        /// <summary>
        /// intereses del usuario
        /// </summary>
        [StringLength(10)]
        public int? CienciasExactas_Agrarias_Int { get; set; }
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
        /// <summary>
        /// CC del usuario
        /// </summary>
        [StringLength(50)]
        public decimal? CC { get; set; }
        /// <summary>
        /// IGAPdel usuario
        /// </summary>
        [StringLength(50)]
        public decimal? IGAP { get; set; }
        /// <summary>
        /// ICI del usuario
        /// </summary>
        [StringLength(50)]
        public decimal? ICI { get; set; }
    }
}
