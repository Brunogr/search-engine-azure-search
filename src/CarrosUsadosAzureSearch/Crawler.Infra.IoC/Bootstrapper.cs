using CarrosUsadosAzureSearch.Infra.Data;
using Cook.Crawler.Parser.Base;
using Crawler.Core;
using Crawler.Core.Interface;
using Crawler.Core.Interface.Repository;
using SimpleInjector;
using System;

namespace Crawler.Infra.IoC
{
    public static class Bootstrapper
    {
        public static void Register(Container container)
        {
            container.Register<ICrawler, AbotCrawler>();
            container.Register<IParser, ParserBase>();
            container.Register<ICarroRepository, CarroRepository>();
        }
    }
}
