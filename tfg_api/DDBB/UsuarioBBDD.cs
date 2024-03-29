﻿using Microsoft.EntityFrameworkCore;
using tfg_api.Model.UsuarioContenedor;


namespace tfg_api.DDBB
{
    /// <summary>
    /// clase de conexion con BBDD
    /// </summary>
    public class UsuarioBBDD : DbContext
    {
        /// <summary>
        /// constructor por defecto
        /// </summary>
        /// <param name="options"></param>
        public UsuarioBBDD(DbContextOptions <UsuarioBBDD> options):base (options) {
        
        }
     
        /// <summary>
        /// Interaccion con la BBDD
        /// </summary>
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
