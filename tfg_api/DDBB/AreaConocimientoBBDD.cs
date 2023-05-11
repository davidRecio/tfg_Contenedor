using Microsoft.EntityFrameworkCore;
using tfg_api.Model.AreaConocimiento;

namespace tfg_api.DDBB
{
    /// <summary>
    /// clase de conexion con BBDD
    /// </summary>
    public class AreaConocimientoBBDD : DbContext
    {
        /// <summary>
        /// constructor por defecto
        /// </summary>
        /// <param name="options"></param>
        public AreaConocimientoBBDD(DbContextOptions<AreaConocimientoBBDD>  options):base (options) {
        
        }
        /// <summary>
        /// Interaccion con la BBDD
        /// </summary>
        public DbSet<AreaConocimiento> AreasConocimientos { get; set; }
    }
}
