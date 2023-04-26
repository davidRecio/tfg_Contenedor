using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tfg_api.Model.Asignatura
{
    /// <summary>
    /// clase de la asignatura
    /// </summary>
    public class Asignatura
    {
        /// <summary>
        /// identificador de la asignatura
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdAsignatura { get; set; } 
        /// <summary>
        /// tipo de la asignatura( humanistica, cientifica...)
        /// </summary>
        [Required, StringLength(50)]
        public string? Tipo { get; set; }

        /// <summary>
        /// nombre de la asignatura
        /// </summary>
        [Required, StringLength(50)]
        public string? Nombre { get; set; }

     

    }
}
