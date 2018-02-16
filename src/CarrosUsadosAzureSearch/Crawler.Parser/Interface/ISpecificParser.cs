using CarrosUsadosAzureSearch.Domain.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cook.Crawler.Parser.Interface
{
    public interface ICarroParser
    {
        bool IsPageValid(HtmlDocument htmlDocument);

        Carro Extract(HtmlDocument htmlDocument);
    }
}
