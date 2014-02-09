using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace PortalEducacao.Controllers
{
    public class IesController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.instituicaoSelecionado = true;
            return View();
        }
    }
}
