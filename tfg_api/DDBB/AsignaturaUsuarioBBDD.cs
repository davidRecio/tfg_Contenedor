using Microsoft.EntityFrameworkCore;
using tfg_api.Model.AsignaturaUsuario;

namespace tfg_api.DDBB
{
    /// <summary>
    /// clase de conexion con BBDD
    /// </summary>
    public class AsignaturaUsuarioDDBB : DbContext
    {
        /// <summary>
        /// constructor por defecto
        /// </summary>
        /// <param name="options"></param>
        public AsignaturaUsuarioDDBB(DbContextOptions<AsignaturaUsuarioDDBB>  options):base (options) {
        
        }
        /// <summary>
        /// Interaccion con la BBDD
        /// </summary>
        public DbSet<AsignaturaUsuario> AsignaturasUsuarios { get; set; }
    }
}
