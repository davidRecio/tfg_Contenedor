
using aplicacionWeb.Model.Asignatura;
using aplicacionWeb.Model.AsignaturaUsuario;
using aplicacionWeb.Model.UsuarioContenedor;
using aplicacionWeb.Model.Utils;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace aplicacionWeb.Servicios;
    public class Servicio_API_Usuario : IServicio_API_Usuario
{
        private static string _usuario;
        private static string _clave;
        private static string _baseUrl;
        private static string _token;
        private static string routePrincipal = "api/usuarios";
        private static JwtSecurityTokenHandler tokenHandler;
        private static string idUsuario;
    public Servicio_API_Usuario()
        {

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            _usuario = builder.GetSection("ApiSetting:usuario").Value;
            _clave = builder.GetSection("ApiSetting:clave").Value;
            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        //USAR REFERENCIAS 
        public async Task<string> Autenticar(string nombre, string ps)
        {
        string roll="";
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
        LoginRequest login = new() {
            Username = nombre,
            Password=ps
        };

        var content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("api/admin/login", content);
            _token  = await response.Content.ReadAsStringAsync();

        
            List<Claim> claims =tokenHandler.ReadJwtToken(_token).Claims.ToList();
        foreach (Claim claim in claims) {
            if (claim.Type.Equals("nameid")) {
                idUsuario = claim.Value.ToString();               
            }
            if (claim.Type.Equals("role"))
            {
                roll = claim.Value.ToString();
            }
        }
    

        return _token;
    }

        public async Task<List<UsuarioGetSort>> Lista()
        {
            List<UsuarioGetSort> lista = new List<UsuarioGetSort>();

        


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync(routePrincipal);

            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
            lista = JsonConvert.DeserializeObject<List<UsuarioGetSort>>(json_respuesta);
                
            }

            return lista;
        }

        public async Task<UsuarioGet> Obtener()
        {
            UsuarioGet usuarioGet = new UsuarioGet();

        


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            var response = await cliente.GetAsync(routePrincipal+"/"+idUsuario);

            if (response.IsSuccessStatusCode)
            {
            
                var json_respuesta = await response.Content.ReadAsStringAsync();
                 usuarioGet = JsonConvert.DeserializeObject<UsuarioGet>(json_respuesta);
               
            }

            return usuarioGet;
        }

        public async Task<bool> Guardar(AddUsuarioRequest addUsuario)
        {
            bool respuesta = false;
       

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(addUsuario), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync(routePrincipal, content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Editar(UpdateUsuarioRequest updateUsuarioRequest)
        {
            bool respuesta = false;

        


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var content = new StringContent(JsonConvert.SerializeObject(updateUsuarioRequest), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync(routePrincipal+"/"+idUsuario , content);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }

        public async Task<bool> Eliminar()
        {
            bool respuesta = false;

       

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);
            cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


            var response = await cliente.DeleteAsync(routePrincipal + "/" + idUsuario);

            if (response.IsSuccessStatusCode)
            {
                respuesta = true;
            }

            return respuesta;
        }


}

public class Servicio_API_Asignatura : IServicio_API_Asignatura
{
    private static string _usuario;
    private static string _clave;
    private static string _baseUrl;
    private static string _token;
    private static string routePrincipal = "api/";
    private static JwtSecurityTokenHandler tokenHandler;
    private static string idUsuario;
    public Servicio_API_Asignatura()
    {

        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        _usuario = builder.GetSection("ApiSetting:usuario").Value;
        _clave = builder.GetSection("ApiSetting:clave").Value;
        _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
    }

    //USAR REFERENCIAS 
  
    public async Task<List<AsignaturaSort>> ListaAsignatura()
    {
        List<AsignaturaSort> lista = new();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await cliente.GetAsync(routePrincipal + "asignaturas");

        if (response.IsSuccessStatusCode)
        {

            var json_respuesta = await response.Content.ReadAsStringAsync();
            lista = JsonConvert.DeserializeObject<List<AsignaturaSort>>(json_respuesta);

        }

        return lista;
    }

    Task<AsignaturaGet> IServicio_API_Asignatura.Obtener()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Guardar(AddAsignaturaRequest objeto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Editar(UpdateAsignaturaRequest objeto)
    {
        throw new NotImplementedException();
    }

    public async Task<List<AsignaturaUsuarioGetSort>> ListaAsignaturaUsuario()
    {
        List<AsignaturaUsuarioGetSort> lista = new();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await cliente.GetAsync(routePrincipal+ "usuarios/" + idUsuario+"/asignaturas");

        if (response.IsSuccessStatusCode)
        {

            var json_respuesta = await response.Content.ReadAsStringAsync();
            lista = JsonConvert.DeserializeObject<List<AsignaturaUsuarioGetSort>>(json_respuesta);

        }

        return lista;
    }

    public Task<bool> Eliminar()
    {
        throw new NotImplementedException();
    }

    public void SetToken(string token)
    {
        _token = token;
        string roll = "";
        var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

        List<Claim> claims = tokenHandler.ReadJwtToken(_token).Claims.ToList();
        foreach (Claim claim in claims)
        {
            if (claim.Type.Equals("nameid"))
            {
                idUsuario = claim.Value.ToString();
            }
            if (claim.Type.Equals("role"))
            {
                roll = claim.Value.ToString();
            }
        }

    }
}

