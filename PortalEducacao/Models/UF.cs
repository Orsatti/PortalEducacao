using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalEducacao.Models
{
    /// <summary>
    /// Modelo para representar municipios
    /// </summary>
    public class UF
    {
        public int UFID { get; set; }

        public int? codigoSensoSuperior { get; set; }

        [Required(ErrorMessage = "Sigla da UF é obrigatório.")]
        [MaxLength(2)]
        public string Sigla { get; set; }


        public int RegiaoID { get; set; }

        // Propriedades de navegação
        public IEnumerable<Municipio> Municipios { get; set; }
        public Regiao Regiao { get; set; }

    }
}