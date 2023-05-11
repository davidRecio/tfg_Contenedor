using aplicacionWeb.Model.Asignatura;
using aplicacionWeb.Model.AsignaturaUsuario;
using aplicacionWeb.Model.UsuarioContenedor;



namespace aplicacionWeb.Servicios
{
    public interface IServicio_API_Usuario
    {
        Task<List<UsuarioGetSort>> Lista();
        Task<UsuarioGet> Obtener();

        Task<bool> Guardar(AddUsuarioRequest objeto);

        Task<bool> Editar(UpdateUsuarioRequest objeto);

        Task<bool> Eliminar();
        Task<string> Autenticar(string nombre, string ps);
    }
    public interface IServicio_API_Asignatura
    {
        void SetToken(string token);
        #region "sin usuario"
        Task<List<AsignaturaSort>> ListaAsignatura();
        Task<AsignaturaUsuarioGet> Obtener(string idAsignatura);

        Task<bool> Crear(string idAsignatura, string nota_input, string tiempoEstudio_input);

        Task<bool> Editar(AsignaturaUsuarioUpdate objeto, string idAsignatura);

        Task<bool> Eliminar(string idAsignatura);
        #endregion

        #region "con usuario"
        Task<List<AsignaturaUsuarioGetSort>> ListaAsignaturaUsuario();
        #endregion
    }
}
