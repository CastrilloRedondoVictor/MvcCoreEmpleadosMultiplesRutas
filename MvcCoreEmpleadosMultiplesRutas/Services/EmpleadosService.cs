using System.Net.Http.Headers;
using NugetApiModelsVCR;

namespace MvcCoreEmpleadosMultiplesRutas.Services
{
    public class EmpleadosService
    {
        private string ApiUrl;

        private MediaTypeWithQualityHeaderValue Header;

        public EmpleadosService(IConfiguration configuration)
        {
            ApiUrl = configuration.GetValue<string>("ApiUrls:ApiEmpleados");
            Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            List<Empleado> empleados = new List<Empleado>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(Header);
                HttpResponseMessage response = await client.GetAsync("api/empleados");
                if (response.IsSuccessStatusCode)
                {
                    empleados = await response.Content.ReadAsAsync<List<Empleado>>();
                }
                else
                {
                    return null;
                }
            }
            return empleados;
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            List<string> oficios = new List<string>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(Header);
                HttpResponseMessage response = await client.GetAsync("api/empleados/oficios");
                if (response.IsSuccessStatusCode)
                {
                    oficios = await response.Content.ReadAsAsync<List<string>>();
                }
                else
                {
                    return null;
                }
            }
            return oficios;

        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            List<Empleado> empleados = new List<Empleado>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(Header);
                HttpResponseMessage response = await client.GetAsync($"api/empleados/oficio/{oficio}");
                if (response.IsSuccessStatusCode)
                {
                    empleados = await response.Content.ReadAsAsync<List<Empleado>>();
                }
                else
                {
                    return null;
                }
            }
            return empleados;
        }
    }
}
