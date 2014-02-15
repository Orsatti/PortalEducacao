using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalEducacao.ViewModels;
using PortalEducacao.Models;
using PortalEducacao.DAL;
using PagedList;

namespace PortalEducacao.Controllers
{
    public class IesController : Controller
    {
        private PEContext db = new PEContext();
        
        public ActionResult Index(string sortOrder, int? page, int? RegiaoID, int? UFID, string busca)
        {
            // TODO Paginacao ignora os dados do form
            // TODO Sorting de cima pra baixo e vice-versa nao esta funcionando
            
            
            ViewBag.instituicaoSelecionado = true;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NomeSortParm = String.IsNullOrEmpty(sortOrder) ? "Nome_desc" : "";
            ViewBag.RegiaoSortParm = sortOrder == "Regiao" ? "Regiao_desc" : "Regiao";
            ViewBag.UFSortParm = sortOrder == "UF" ? "UF_desc" : "UF";
           
            // Parâmetros
            int pageSize = 10;
            int pageNumber = (page ?? 1);

             var iess = db.Iess
                .Include(i => i.Municipio.UF.Regiao)
                .Include(i => i.Municipio.UF)
                .Include(i => i.Municipio);

             if (RegiaoID != null)
             {
                 iess = iess.Where(i => i.Municipio.UF.RegiaoID == RegiaoID);
             }

             if (UFID != null)
             {
                 iess = iess.Where(i => i.Municipio.UFID == UFID);
             }

             if (busca != null)
             {
                 iess = iess.Where(i => i.Nome.Contains(busca));
             }

            switch (sortOrder)
            {
                case "Name_desc":
                    iess = iess.OrderByDescending(i => i.Nome)
                        .ThenBy(i => i.Municipio.UF.Regiao.Nome)
                        .ThenBy(i => i.Municipio.UF.Sigla)
                        .ThenBy(i => i.Municipio.Nome)
                        .ThenBy(i => i.CategoriaAdministrativa.Nome)
                        .ThenBy(i => i.OrganizacaoAcademica.Nome);
                    break;
                case "Regiao":
                    iess = iess.OrderBy(i => i.Municipio.UF.Regiao.Nome)
                        .ThenBy(i => i.Nome)
                        .ThenBy(i => i.Municipio.UF.Sigla)
                        .ThenBy(i => i.Municipio.Nome)
                        .ThenBy(i => i.CategoriaAdministrativa.Nome)
                        .ThenBy(i => i.OrganizacaoAcademica.Nome);
                    break;
                case "Regiao_desc":
                    iess = iess.OrderByDescending(i => i.Municipio.UF.Regiao.Nome)
                        .ThenBy(i => i.Nome)
                        .ThenBy(i => i.Municipio.UF.Sigla)
                        .ThenBy(i => i.Municipio.Nome)
                        .ThenBy(i => i.CategoriaAdministrativa.Nome)
                        .ThenBy(i => i.OrganizacaoAcademica.Nome);
                    break;
                case "UF":
                    iess = iess.OrderBy(i => i.Municipio.UF.Sigla)
                        .ThenBy(i => i.Nome)
                        .ThenBy(i => i.Municipio.UF.Regiao.Nome)
                        .ThenBy(i => i.Municipio.Nome)
                        .ThenBy(i => i.CategoriaAdministrativa.Nome)
                        .ThenBy(i => i.OrganizacaoAcademica.Nome);
                    break;
                case "UF_desc":
                    iess = iess.OrderByDescending(i => i.Municipio.UF.Sigla)
                        .ThenBy(i => i.Nome)
                        .ThenBy(i => i.Municipio.UF.Regiao.Nome)
                        .ThenBy(i => i.Municipio.Nome)
                        .ThenBy(i => i.CategoriaAdministrativa.Nome)
                        .ThenBy(i => i.OrganizacaoAcademica.Nome);
                    break;
                default:
                    iess = iess.OrderBy(i => i.Nome)
                        .ThenBy(i => i.Municipio.UF.Regiao.Nome)
                        .ThenBy(i => i.Municipio.UF.Sigla)
                        .ThenBy(i => i.Municipio.Nome)
                        .ThenBy(i => i.CategoriaAdministrativa.Nome)
                        .ThenBy(i => i.OrganizacaoAcademica.Nome);
                    break;
            }

            var iessP = iess.ToPagedList(pageNumber, pageSize);


            if (Request.IsAjaxRequest())
            {
                return PartialView("_tabelaIes", iessP);
            }

            var regioes = db.Regioes.OrderBy(r => r.Nome);
            var ufs = db.UFs.OrderBy(u => u.Sigla);

            var iesData = new IesData();
            iesData.Iess = iessP;
            iesData.RegiaoLista = new SelectList(regioes, "RegiaoID", "Nome");
            iesData.UFLista = new SelectList(ufs, "UFID", "Sigla");

            return View(iesData);
        }
    }
}
