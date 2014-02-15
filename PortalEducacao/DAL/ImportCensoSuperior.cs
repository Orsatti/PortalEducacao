using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using PortalEducacao.Models;
using PortalEducacao.ViewModels;
using System.Text;

namespace PortalEducacao.DAL
{
    /// <summary>
    /// Classe com métodos para importação de dados do censo superior
    /// </summary>
    public class ImportCensoSuperior
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// Caminho para os dados. Completar o nome do folder com o ano para utilizar.
        /// </summary>
        public readonly string caminhoDados = HttpRuntime.AppDomainAppPath + "Dados\\CensoSuperior";

        private PEContext db = new PEContext();

        public void carregaDadosIES(int ano)
        {
            var func = "carregarDadosIES";
            var arquivo = this.caminhoDados + Convert.ToString(ano) + "\\INSTITUICAO.txt";
            var erro = new Erro();

            if (File.Exists(arquivo))
            {
                int counter = 0;
                string line;

                StreamReader leitor = new StreamReader(arquivo, Encoding.GetEncoding("iso-8859-1"));
                while ((line = leitor.ReadLine()) != null)
                {
                    // Cria o elemento novo
                    var ies = new Ies();
                    ies.AnoCensoSuperior = ano;

                    // Código do Censo Superior
                    try
                    {
                        // O código vai até a coluna número 9
                        var subCodigo = line.Substring(0, 8).Trim();
                        int codigo = Convert.ToInt32(subCodigo);
                        ies.CodigoCensoSuperior = codigo;
                    }
                    catch (Exception ex)
                    {
                        var mensagem = this.formaMensagemErro(func,ano,"CodigoCensoSuperior",counter,ex.Message);
                        logger.Error(mensagem);
                        erro.adicionaErro(mensagem);
                    }

                    // Nome
                    try
                    {
                        // O nome está sempre entre 9 e 200
                        var subNome = line.Substring(8, 199).Trim();
                        ies.Nome = subNome;
                    }
                    catch (Exception ex)
                    {
                        var mensagem = this.formaMensagemErro(func, ano, "Nome", counter, ex.Message);
                        logger.Error(mensagem);
                        erro.adicionaErro(mensagem);
                    }

                    // Código Mantenedora
                    try
                    {
                        // Código entre 200 e 217
                        var subMante = line.Substring(200, 16).Trim();
                        int codigo = Convert.ToInt32(subMante);
                        ies.CodigoMantenedoraCensoSuperior = codigo;
                    }
                    catch (Exception ex)
                    {
                        var mensagem = this.formaMensagemErro(func, ano, "CodigoMantenedora", counter, ex.Message);
                        logger.Error(mensagem);
                        erro.adicionaErro(mensagem);
                    }

                    // Categoria Administrativa
                    try
                    {
                        var subCatAdm = line.Substring(223, 1).Trim();
                        int codigo = Convert.ToInt32(subCatAdm);
                        var categoria = db.CategoriasAdministrativas.Where(c => c.codigoCensoSuperior == codigo).Single();
                        ies.CategoriaAdministrativaID = categoria.CategoriaAdministrativaID;                        

                    }
                    catch (Exception ex)
                    {
                        var mensagem = this.formaMensagemErro(func, ano, "CategoriaAdministrativa", counter, ex.Message);
                        logger.Error(mensagem);
                        erro.adicionaErro(mensagem);
                    }

                    // Organizacao Academica
                    try
                    {
                        var subOrgAc = line.Substring(331, 1).Trim();
                        int codigo = Convert.ToInt32(subOrgAc);
                        var organizacao = db.OrganizacoesAcademicas.Where(o => o.codigoCensoSuperior == codigo).Single();
                        ies.OrganizacaoAcademicaID = organizacao.OrganizacaoAcademicaID;            
                    }
                    catch (Exception ex)
                    {
                        var mensagem = this.formaMensagemErro(func, ano, "OrganizacaoAcademica", counter, ex.Message);
                        logger.Error(mensagem);
                        erro.adicionaErro(mensagem);
                    }

                    // Regiao, UF e Municipio
                    try
                    {
                        var subRegiao = line.Substring(600, 30).Trim();
                        // Verifica se Regiao ja existe no banco
                        var regiao = db.Regioes.Where(r => r.Nome == subRegiao).SingleOrDefault();
                        if (regiao == null)
                        {
                            regiao = new Regiao();
                            regiao.Nome = subRegiao;
                            db.Regioes.Add(regiao);
                            db.SaveChanges();
                        }

                        try
                        {
                            var subCodUF = line.Substring(590, 8).Trim();
                            int codigoUF = Convert.ToInt32(subCodUF);

                            // Verifica se a UF existe no banco
                            var uF = db.UFs.Where(u => u.codigoSensoSuperior == codigoUF).SingleOrDefault();
                            if (uF == null)
                            {
                                // Pega o nome do município para armazenar no banco
                                var subUF = line.Substring(598, 2).Trim();
                                uF = new UF();
                                uF.codigoSensoSuperior = codigoUF;
                                uF.Sigla = subUF;
                                uF.RegiaoID = regiao.RegiaoID;
                                db.UFs.Add(uF);
                                db.SaveChanges();
                            }

                            try
                            {
                                var subCodMun = line.Substring(432, 8).Trim();
                                int codigo = Convert.ToInt32(subCodMun);

                                // Verifica se o municipio existe no banco
                                var municipio = db.Municipios.Where(m => m.codigoSensoSuperior == codigo).SingleOrDefault();
                                if (municipio == null)
                                {
                                    // Pega o nome do município para armazenar no banco
                                    var subMun = line.Substring(440, 150).Trim();
                                    municipio = new Municipio();
                                    municipio.Nome = subMun;
                                    municipio.codigoSensoSuperior = codigo;
                                    municipio.UFID = uF.UFID;
                                    db.Municipios.Add(municipio);
                                    db.SaveChanges();
                                }

                                // Finalmente coloca o municipio na ies
                                ies.MunicipioID = municipio.MunicipioID;
                            }
                            catch (Exception ex)
                            {
                                var mensagem = this.formaMensagemErro(func, ano, "Municipio", counter, ex.Message);
                                logger.Error(mensagem);
                                erro.adicionaErro(mensagem);
                            }
                        }
                        catch (Exception ex)
                        {
                            var mensagem = this.formaMensagemErro(func, ano, "UF", counter, ex.Message);
                            logger.Error(mensagem);
                            erro.adicionaErro(mensagem);
                        }
                    }
                    catch (Exception ex)
                    {
                         var mensagem = this.formaMensagemErro(func, ano, "Regiao", counter, ex.Message);
                            logger.Error(mensagem);
                            erro.adicionaErro(mensagem);
                    }

                    // Verifica se a IES ja existe no banco
                    // Primeiro verifica pelo código
                    try
                    {
                        var iesInDb = db.Iess.Where(i => i.CodigoCensoSuperior == ies.CodigoCensoSuperior).SingleOrDefault();
                        if (iesInDb == null)
                        {
                            // Tenta pegar pelo nome
                            iesInDb = db.Iess.Where(i => i.Nome == ies.Nome).SingleOrDefault();
                            if (iesInDb == null)
                            {
                                // Não achou, então deve inserir
                                db.Iess.Add(ies);
                                db.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        var mensagem = this.formaMensagemErro(func, ano, "Ies", counter, ex.Message);
                        logger.Debug(mensagem);
                        erro.adicionaErro(mensagem);
                    }
                    counter++; 
                }

                leitor.Close();

            }
            else
            {
                throw new Exception("Erro: Arquivo " + arquivo + " não existe!");
            }
        }

        private string formaMensagemErro(string func, int ano, string parte, int linha, string erro)
        {
            return "[" + func + "][" + ano + "][" + parte + "][" + linha + "] " + erro;
        }
    }
}