
using System.ComponentModel.DataAnnotations.Schema;

namespace aplicacionWeb.ModelsAsignaturaUsuario
{
  
    public class AsignaturaUsuarioCreate
    {

        /// <summary>
        /// Nota de la asignatura
        /// </summary>
     
        public double Nota { get; set; }
        /// <summary>
        /// horas de estudio
        /// </summary>
        
        public int TiempoEstudio { get; set; }
   


    }
}
