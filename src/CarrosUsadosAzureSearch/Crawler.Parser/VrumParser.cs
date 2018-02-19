using Cook.Crawler.Parser.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarrosUsadosAzureSearch.Domain.Models;
using HtmlAgilityPack;

namespace Crawler.Parser
{
    public class VrumParser : ICarroParser
    {
        public Carro Extract(HtmlDocument htmlDocument)
        {
            var nome = GetNome(htmlDocument);
            var descricao = GetDescricao(htmlDocument);
            var anokmcorportas = GetAnoKmCorPortas(htmlDocument);
            var local = GetLocal(htmlDocument);
            var preco = GetValor(htmlDocument);
            var adicionais = GetAdicionais(htmlDocument);
            var fonte = "Vrum";
            
            var ano = anokmcorportas[0] != null ? anokmcorportas[0] : string.Empty;
            var km = anokmcorportas[1] != null && anokmcorportas.Count > 1 ? anokmcorportas[1] : string.Empty;
            var cor = anokmcorportas[2] != null && anokmcorportas.Count > 2 ? anokmcorportas[2] : string.Empty;
            var portas = anokmcorportas[3] != null && anokmcorportas.Count > 3 ? anokmcorportas[3] : string.Empty;

            return new Carro(nome, descricao, ano, km, cor, portas, local, preco, fonte, adicionais);
        }

        public bool IsPageValid(HtmlDocument htmlDocument)
        {
            var nome = htmlDocument.DocumentNode
                    .Descendants("div")
                    .Where(o => o.GetAttributeValue("class", "")
                        .Contains("col-xs-7 col-sm-8 col-sxs-12 col-md-9"))
                    .SelectMany(div => div.Descendants("h1"));

            return nome.Count() > 0;
        }

        #region [ Métodos de Extração ]

        private string GetNome(HtmlDocument htmlDocument)
        {
            var nome = htmlDocument.DocumentNode
                .Descendants("div")
                .Where(o => o.GetAttributeValue("class", "")
                    .Contains("col-xs-7 col-sm-8 col-sxs-12 col-md-9"))
                .SelectMany(div => div.Descendants("h1"))
                .FirstOrDefault();

            var separator = new string[] { "&nbsp;" };

            return nome.InnerText.Replace("\t", "").Replace("\n", "").Split(separator, StringSplitOptions.None)[0];
        }

        private string GetLocal(HtmlDocument htmlDocument)
        {
            var nome = htmlDocument.DocumentNode
                .Descendants("span")
                .Where(o => o.GetAttributeValue("class", "")
                    .Contains("resultados-da-busca-localizacao"))
                .FirstOrDefault();

            return nome.InnerText;
        }

        private string GetValor(HtmlDocument htmlDocument)
        {
            var nome = htmlDocument.DocumentNode
                .Descendants("li")
                .Where(o => o.GetAttributeValue("class", "")
                    .Contains("item-valor margin-top-0 itenscustos__valor green-text"))
                .FirstOrDefault();

            return nome.InnerText;
        }

        private string GetDescricao(HtmlDocument htmlDocument)
        {
            var descricao = htmlDocument.DocumentNode
                .Descendants("div")
                .Where(o => o.GetAttributeValue("class", "")
                    .Contains("descricao__veiculo"))
                .SelectMany(div => div.Descendants("p"))
                .FirstOrDefault();

            return descricao.InnerText;
        }

        private List<string> GetAnoKmCorPortas(HtmlDocument htmlDocument)
        {
            var retorno = new List<string>();
            var items = htmlDocument.DocumentNode
                .Descendants("span")
                .Where(o => o.GetAttributeValue("class", "")
                    .Contains("item-descricao-conteudo"))
                .ToList();

            foreach (var item in items)
            {
                retorno.Add(item.InnerText);
            }

            return retorno;
        }

        private List<string> GetAdicionais(HtmlDocument htmlDocument)
        {
            var retorno = new List<string>();
            var items = htmlDocument.DocumentNode
                .Descendants("ul")
                .Where(o => o.GetAttributeValue("class", "")
                    .Contains("list-unstyled list-caracteristicas-veiculo"))
                .SelectMany(ul => ul.Descendants("li"))
                .ToList();

            foreach (var item in items)
            {
                retorno.Add(item.InnerText);
            }

            return retorno;
        }

        #endregion
    }
}
