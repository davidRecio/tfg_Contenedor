using Microsoft.EntityFrameworkCore;
using tfg_api.Model.AsignaturaArea;

namespace tfg_api.DDBB
{
    /// <summary>
    /// clase de conexion con BBDD
    /// </summary>
    public class AsignaturaAreaBBDD : DbContext
    {
        /// <summary>
        /// constructor por defecto
        /// </summary>
        /// <param name="options"></param>
        public AsignaturaAreaBBDD(DbContextOptions<AsignaturaAreaBBDD>  options):base (options) {
        
        }
        /// <summary>
        /// Interaccion con la BBDD
        /// </summary>
        public DbSet<AsignaturaArea> AsignaturasAreas { get; set; }
    }
}
