using Microsoft.EntityFrameworkCore;
using tfg_api.Model.RespuestaFormulario;

namespace tfg_api.DDBB
{
    /// <summary>
    /// clase de conexion con BBDD
    /// </summary>
    public class RespuestaFormularioBBDD : DbContext
    {
        /// <summary>
        /// constructor por defecto
        /// </summary>
        /// <param name="options"></param>
        public RespuestaFormularioBBDD(DbContextOptions<RespuestaFormularioBBDD> options):base (options) {
        
        }
        /// <summary>
        /// Interaccion con la BBDD
        /// </summary>
        public DbSet<RespuestaFormulario> RespuestaFormularios { get; set; }
    }
}
