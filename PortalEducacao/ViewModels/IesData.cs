using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalEducacao.Models;
using System.Web.Mvc;

namespace PortalEducacao.ViewModels
{
    public class IesData
    {
        public IEnumerable<Ies> Iess { get; set; }
        public SelectList RegiaoLista { get; set; }
        public SelectList UFLista { get; set; }
        public int? RegiaoID { get; set; }
        public int? UFID { get; set; }
        public string busca { get; set; }
    }
}