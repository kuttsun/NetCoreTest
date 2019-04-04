using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HostedServiceTest.Models;
using Microsoft.Extensions.Hosting;

namespace HostedServiceTest.Controllers
{
    public class HomeController : Controller
    {
        IEnumerable<IHostedService> _hostedServices;

        //public HomeController(IEnumerable<IHostedService> hostedService)
        public HomeController(IEnumerable<IHostedService> hostedServices)
        {
            _hostedServices = hostedServices;
        }

        public IActionResult Index()
        {
            return View();
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
