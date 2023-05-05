using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacionWeb.Model.Asignatura
{
    /// <summary>
    /// clase de la asignatura
    /// </summary>
    public class AsignaturaSort
    {
        /// <summary>
        /// 
        /// </summary>
        public Uri? UriAsignatura { get; set; }

        /// <summary>
        /// nombre de la asignatura
        /// </summary>

        public string? Nombre { get; set; }

     

    }
}
