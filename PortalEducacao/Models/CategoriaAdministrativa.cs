using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalEducacao.Models
{
    /// <summary>
    /// Modelo para representar as categorias administrativas de IESs
    /// </summary>
    public class CategoriaAdministrativa
    {
        public int CategoriaAdministrativaID { get; set; }

        [Required(ErrorMessage = "Nome da categoria é obrigatório.")]
        [MaxLength(50)]
        public string Nome { get; set; }

        // Propriedades de navegação
        public IEnumerable<Ies> Iess { get; set; }

    }
}