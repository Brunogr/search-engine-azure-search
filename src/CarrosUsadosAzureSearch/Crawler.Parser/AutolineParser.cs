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
    public class AutolineParser : ICarroParser
    {
        public Carro Extract(HtmlDocument htmlDocument)
        {
            throw new NotImplementedException();
        }

        public bool IsPageValid(HtmlDocument htmlDocument)
        {
            throw new NotImplementedException();
        }
    }
}
