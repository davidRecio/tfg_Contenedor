using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace tfg_api.Model.Asignatura
{
    /// <summary>
    /// clase de la asignatura
    /// </summary>
    public class AsignaturaGet
    {
 
        /// <summary>
        /// tipo de la asignatura( humanistica, cientifica...)
        /// </summary>
        public string? Tipo { get; set; }

        /// <summary>
        /// nombre de la asignatura
        /// </summary>
        public string? Nombre { get; set; }

     

    }
}
