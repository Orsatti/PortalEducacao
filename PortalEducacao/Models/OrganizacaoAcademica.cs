using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalEducacao.Models
{
    /// <summary>
    /// Modelo para representar as organizacoes academicas de IESs
    /// </summary>
    public class OrganizacaoAcademica
    {
        public int OrganizacaoAcademicaID { get; set; }

        public int codigoCensoSuperior { get; set; }

        [Required(ErrorMessage = "Nome da categoria é obrigatório.")]
        [MaxLength(100)]
        public string Nome { get; set; }

        // Propriedades de navegação
        public IEnumerable<Ies> Iess { get; set; }
    }
}