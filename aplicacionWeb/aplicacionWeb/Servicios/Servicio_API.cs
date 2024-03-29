﻿
using aplicacionWeb.Model.Asignatura;
using aplicacionWeb.Model.AsignaturaUsuario;
using aplicacionWeb.Model.PreguntaFormulario;
using aplicacionWeb.Model.RespuestaFormulario;
using aplicacionWeb.Model.UsuarioContenedor;
using aplicacionWeb.Model.Utils;
using aplicacionWeb.Models.AsignaturaUsuario;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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

    public async Task<bool> Recomendaciones()
    {
        bool respuesta = false;



        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);


        var response = await cliente.GetAsync(routePrincipal + "/" + idUsuario+"/recomendaciones");

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

    public async Task<AsignaturaUsuarioGet> Obtener(string idAsignatura)
    {
        AsignaturaUsuarioGet asignaturaUsuario = new();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await cliente.GetAsync(routePrincipal + "usuarios/" + idUsuario + "/asignaturas/"+ idAsignatura);

        if (response.IsSuccessStatusCode)
        {

            var json_respuesta = await response.Content.ReadAsStringAsync();
            asignaturaUsuario = JsonConvert.DeserializeObject<AsignaturaUsuarioGet>(json_respuesta);

        }

        return asignaturaUsuario;
    }

    public async Task<bool> Crear(string idAsignatura, string nota_input, string tiempoEstudio_input)
    {
        bool respuesta = false;

        AsignaturaUsuarioCreate asignaturaUsuarioCreate = new ()
        {
            Nota = Double.Parse(nota_input),
            TiempoEstudio = Int32.Parse(tiempoEstudio_input),

        };

        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var content = new StringContent(JsonConvert.SerializeObject(asignaturaUsuarioCreate), Encoding.UTF8, "application/json");

        var response = await cliente.PostAsync(routePrincipal + "usuarios/" + idUsuario + "/asignaturas/" + idAsignatura, content);

        if (response.IsSuccessStatusCode)
        {
            respuesta = true;
        }

        return respuesta;
    }

    public async Task<bool> Editar(AsignaturaUsuarioUpdate objeto, string idAsignatura)
    {
        bool respuesta = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

        var response = await cliente.PutAsync(routePrincipal + "usuarios/" + idUsuario + "/asignaturas/" + idAsignatura, content);

        if (response.IsSuccessStatusCode)
        {
            respuesta = true;
        }

        return respuesta;
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

    public async Task<bool> Eliminar(string idAsignatura)
    {
        bool respuesta = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        

        var response = await cliente.DeleteAsync(routePrincipal + "usuarios/" + idUsuario + "/asignaturas/" + idAsignatura);

        if (response.IsSuccessStatusCode)
        {
            respuesta = true;
        }

        return respuesta;
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

public class Servicio_API_Formulario : IServicio_API_Formulario
{
    private static string _usuario;
    private static string _clave;
    private static string _baseUrl;
    private static string _token;
    private static string routePrincipal = "api/";
    private static JwtSecurityTokenHandler tokenHandler;
    private static string idUsuario;
    public Servicio_API_Formulario()
    {

        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

        _usuario = builder.GetSection("ApiSetting:usuario").Value;
        _clave = builder.GetSection("ApiSetting:clave").Value;
        _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
    }

    public async Task<bool> GuardarRespuestas(string[] listaRespuestasUsuario,int tipo)
    {
        bool respuesta = false;
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        List<AddRespuestaFormulario> listaRespuestas= new();
        int contador = 1;
        string  valor;
       
        
        foreach (string respuestaUsuario in listaRespuestasUsuario)
        {
            if (respuestaUsuario != "")
            {
                if (tipo == 1)
                { 
                    AddRespuestaFormulario respuestaFormulario = new()
                    {
                        IdPregunta = Int32.Parse(respuestaUsuario.Split("_")[1]),
                        Valor = respuestaUsuario.Split("_")[0],
                    };

                    listaRespuestas.Add(respuestaFormulario);
                }
                else
                {
                    AddRespuestaFormulario respuestaFormulario = new()
                    {
                        IdPregunta = contador,
                        Valor = respuestaUsuario,
                    };

                    listaRespuestas.Add(respuestaFormulario);
                }
             

                contador++;
            }
        
        }

        var json = JsonConvert.SerializeObject(listaRespuestas);



            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await cliente.PutAsync(routePrincipal + "usuarios/" + idUsuario + "/formularios/" + tipo, content);
            if (response.IsSuccessStatusCode)
            {

                var json_respuesta = await response.Content.ReadAsStringAsync();
                respuesta = true;

            }
        
       
       
        
        return respuesta;
    }



    //USAR REFERENCIAS 

    public async Task<List<PreguntaFormulario>> ListaPreguntaFormulario(int tipo)
    {
        List<PreguntaFormulario> lista = new();
        var cliente = new HttpClient();
        cliente.BaseAddress = new Uri(_baseUrl);
        cliente.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await cliente.GetAsync(routePrincipal + "formularios/"+tipo);

        if (response.IsSuccessStatusCode)
        {

            var json_respuesta = await response.Content.ReadAsStringAsync();
            lista = JsonConvert.DeserializeObject<List<PreguntaFormulario>>(json_respuesta);

        }

        return lista;
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
