using LoadDllExample.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace LoadDllExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public string Inventory()
        {
            //var path = Directory.GetCurrentDirectory() + "\\..";
            //var root = Directory.GetDirectoryRoot(path);
            //Console.WriteLine(Directory.GetCurrentDirectory());

            //var p = "D:\\Office\\Task\\OSL-Medical_Inventory\\Medical Inventory\\obj\\Debug\\net6.0\";
            var path = "D:\\Office\\Task\\OSL-Medical_Inventory\\Medical Inventory\\bin\\Debug\\net6.0";
            //var dllFile = path + "\\ReflectionLib.dll";
            var dllFile = path + "\\Medical Inventory.dll";

            var className = "Medical_Inventory.Controllers.CategoriesController";

            Type[] types;
            try
            {
                Console.WriteLine(dllFile);
                //Assembly assembly = Assembly.LoadFile(dllFile);
                Assembly assembly = Assembly.LoadFile(dllFile);
                Predicate<Type> predicate;

                types = assembly.GetTypes().Where(i => i != null && i.Assembly == assembly).ToArray();

                foreach (var type in types)
                {
                    Console.WriteLine(type.Name);
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
                Console.WriteLine(errorMessage);
                //Display or log the error based on your application.
            }



            return "loaded";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}