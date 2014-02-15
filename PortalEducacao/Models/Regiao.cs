using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalEducacao.Models
{
    public class Regiao
    {
        public int RegiaoID { get; set; }

        [Required(ErrorMessage = "Nome da região é obrigatório.")]
        [MaxLength(150)]
        public string Nome { get; set; }

        // Propriedades de navegação
        public IEnumerable<UF> UFs { get; set; }
    }
}