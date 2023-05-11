using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tfg_api.Model.AreaConocimiento

{
    /// <summary>
    /// clase de la asignatura
    /// </summary>
    public class AreaConocimiento
    {
        /// <summary>
        /// identificador de la asignatura
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdArea{ get; set; } 

        /// <summary>
        /// nombre de la asignatura
        /// </summary>
        [Required, StringLength(50)]
        public string? Nombre { get; set; }

     

    }
}
