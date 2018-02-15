using Cook.Domain;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cook.Crawler.Parser.Interface
{
    public interface ISpecificParser
    {
        bool IsPageValid(HtmlDocument htmlDocument);

        Recipe ExtractRecipe(HtmlDocument htmlDocument);
    }
}
