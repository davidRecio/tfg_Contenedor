using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace tfg_api.Model.AsignaturaArea

{
    /// <summary>
    /// clase de la asignatura-Usuario
    /// </summary>
    [PrimaryKey(nameof(IdAsignatura), nameof(IdArea))]
    public class AsignaturaArea
    {
        /// <summary>
        /// identificador de la asignatura
        /// </summary>
        [Column(Order = 0)]
        public Guid IdAsignatura { get; set; }
        /// <summary>
        /// referencia al area de conocimiento
        /// </summary>
        [Column(Order = 1)]
        public Guid IdArea { get; set; }
   



    }
}
