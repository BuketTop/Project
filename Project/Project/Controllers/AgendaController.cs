using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class AgendaController : Controller
    {
        // GET: Agenda
        public ActionResult Index()
        {
            return View();
        }
    }
}