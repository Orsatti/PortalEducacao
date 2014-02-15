using PortalEducacao.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalEducacao.ViewModels;

namespace PortalEducacao.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.AdminSelecionado = true;
            return View();
        }

        [HttpPost]
        public ActionResult CarregarDadosIES(int ano)
        {
            ViewBag.AdminSelecionado = true;
            var importador = new ImportCensoSuperior();
            try
            {
                importador.carregaDadosIES(ano);
                return View("ResumoImportacao");
            }
            catch (Exception ex)
            {
                return View("Error", new Erro(ex.Message));
            }
        }
    }
}
