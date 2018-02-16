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

            return new Carro(nome, descricao, anokmcorportas[0], anokmcorportas[1], anokmcorportas[2], Convert.ToInt32(anokmcorportas[3]), local, preco, adicionais);
        }

        public bool IsPageValid(HtmlDocument htmlDocument)
        {
            var nome = htmlDocument.DocumentNode
                    .Descendants("div")
                    .Where(o => o.GetAttributeValue("class", "")
                        .Contains("col-xs-7 col-sm-8 col-sxs-12 col-md-9"))
                    .SelectMany(div => div.Descendants("h1"));

            return nome.Count > 0;
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

            return nome.InnerText.Replace("\t", "").Replace("\n", "");
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
                .Descendants("div")
                .Where(o => o.GetAttributeValue("span", "")
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
