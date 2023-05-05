using System.ComponentModel.DataAnnotations;

namespace aplicacionWeb.Model.Asignatura
{
    public class AddAsignaturaRequest
    {
        /// <summary>
        /// nombre de la asignatura
        /// </summary>
        public string? Nombre { get; set; }
        public string Tipo { get; set; }
    }
}
