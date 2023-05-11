

namespace tfg_api.Model.AsignaturaUsuario
{
    /// <summary>
    /// clase de la asignatura-Usuario
    /// </summary>
    
    public class AsignaturaUsuarioGetSort
    {
        /// <summary>
        /// identificador de la asignatura
        /// </summary>
        public Uri? UrlAsignatura { get; set; }
        /// <summary>
        /// nombre de la asignatura
        /// </summary>

        public string? Nombre { get; set; }

        /// <summary>
        /// Riesgo en referente a la nota sacada
        /// </summary>
        public int Riesgo { get; set; }

        /// <summary>
        /// Nota de la asignatura
        /// </summary>

        public decimal Nota { get; set; }

    }
}
