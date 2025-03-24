using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcCoreEmpleadosMultiplesRutas.Models;
using MvcCoreEmpleadosMultiplesRutas.Services;
using NugetApiModelsVCR;

namespace MvcCoreEmpleadosMultiplesRutas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private EmpleadosService _empleadosService;

        public HomeController(ILogger<HomeController> logger, EmpleadosService service)
        {
            _logger = logger;
            _empleadosService = service;
        }

        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados = await _empleadosService.GetEmpleadosAsync();
            List<string> oficios = await _empleadosService.GetOficiosAsync();

            ViewBag.Oficios = oficios;
            return View(empleados);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string oficio)
        {
            List<Empleado> empleados = new List<Empleado>();
            if (oficio == "all")
            {
                empleados = await _empleadosService.GetEmpleadosAsync();
            } else
            {
                empleados = await _empleadosService.GetEmpleadosOficioAsync(oficio);
            }
            List<string> oficios = await _empleadosService.GetOficiosAsync();
            ViewBag.Oficios = oficios;
            ViewBag.Oficio = oficio;
            return View(empleados);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
