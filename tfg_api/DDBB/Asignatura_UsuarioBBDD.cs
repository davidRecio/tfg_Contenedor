using Microsoft.EntityFrameworkCore;


namespace tfg_api.DDBB
{
    /// <summary>
    /// clase de conexion con BBDD
    /// </summary>
    public class Asignatura_UsuarioBBDD : DbContext
    {
        /// <summary>
        /// constructor por defecto
        /// </summary>
        /// <param name="options"></param>
        public Asignatura_UsuarioBBDD(DbContextOptions<Asignatura_UsuarioBBDD>  options):base (options) {
        
        }
        /// <summary>
        /// Interaccion con la BBDD
        /// </summary>
        public DbSet<Model.Asignatura_UsuarioBBDD.Asignatura_UsuarioBBDD> Asignatura_Usuarios { get; set; }
    }
}
