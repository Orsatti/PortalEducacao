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
    public class Municipio
    {
        public int MunicipioID { get; set; }

        public int? codigoSensoSuperior { get; set; }

        [Required(ErrorMessage = "Nome da município é obrigatório.")]
        [MaxLength(150)]
        public string Nome { get; set; }

        public int UFID { get; set; }

        // Propriedades de navegação
        public UF UF { get; set; }
        public IEnumerable<Ies> Iess { get; set; }

    }
}