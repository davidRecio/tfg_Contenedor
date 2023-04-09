using System.ComponentModel.DataAnnotations;

namespace tfg_api.Model.Asignatura
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
