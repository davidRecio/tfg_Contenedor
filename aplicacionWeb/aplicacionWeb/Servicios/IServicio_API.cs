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
        Task<AsignaturaGet> Obtener();

        Task<bool> Guardar(AddAsignaturaRequest objeto);

        Task<bool> Editar(UpdateAsignaturaRequest objeto);

        Task<bool> Eliminar();
        #endregion

        #region "con usuario"
        Task<List<AsignaturaUsuarioGetSort>> ListaAsignaturaUsuario();
        #endregion
    }
}
