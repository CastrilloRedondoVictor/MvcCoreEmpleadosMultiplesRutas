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
            return await CallApiAsync<List<Empleado>>("api/empleados");
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            return await CallApiAsync<List<string>>("api/empleados/oficios");

        }

        public async Task<List<Empleado>> GetEmpleadosOficioAsync(string oficio)
        {
            return await CallApiAsync<List<Empleado>>($"api/empleados/oficio/{oficio}");
        }


        private async Task<T> CallApiAsync<T>(string url)
        {
            T result = default(T);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(Header);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<T>();
                }
            }
            return result;
        }
    }
}
