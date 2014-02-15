using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalEducacao.Models
{
    public class Ies
    {
        // Chave Primária
        public int IesID {get; set;}

        // Se veio do Censo do Ensino Superior, indica o ano
        public int? AnoCensoSuperior { get; set; }

        // Código no CensoEnsinoSuperior
        public int? CodigoCensoSuperior { get; set; }

        [Required(ErrorMessage = "Nome da IES é obrigatório.")]
        [MaxLength(200)]
        public string Nome { get; set; }

        // Código da Mantenedora no CensoEnsinoSuperior
        public int? CodigoMantenedoraCensoSuperior { get; set; }

        // Categoria Administrativa
        public int? CategoriaAdministrativaID { get; set; }

        // Organização Acadêmica
        public int? OrganizacaoAcademicaID { get; set; }

        // Municipio
        public int? MunicipioID { get; set; }

        // Propriedades de navegação
        public virtual CategoriaAdministrativa CategoriaAdministrativa { get; set; }
        public virtual OrganizacaoAcademica OrganizacaoAcademica { get; set; }
        public virtual Municipio Municipio { get; set; }

        // Até linha 12 do IES.txt

    }
}