using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using GeocacheApp2.Models;
using Newtonsoft.Json;

namespace GeocacheApp2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Geocaching App";
            
            return View();
        }
    }
}
