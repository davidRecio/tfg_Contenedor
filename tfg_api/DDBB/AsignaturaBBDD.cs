using Microsoft.EntityFrameworkCore;
using tfg_api.Model.Asignatura;

namespace tfg_api.DDBB
{
    /// <summary>
    /// clase de conexion con BBDD
    /// </summary>
    public class AsignaturaBBDD : DbContext
    {
        /// <summary>
        /// constructor por defecto
        /// </summary>
        /// <param name="options"></param>
        public AsignaturaBBDD(DbContextOptions<AsignaturaBBDD>  options):base (options) {
        
        }
        /// <summary>
        /// Interaccion con la BBDD
        /// </summary>
        public DbSet<Model.Asignatura.Asignatura_UsuarioBBDD> Asignaturas { get; set; }
    }
}
