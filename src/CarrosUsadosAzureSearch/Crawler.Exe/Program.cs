using Crawler.Core.Interface;
using Crawler.Infra.IoC;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Exe
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            InitializeContainer(container);

            container.Verify();

            var crawler = container.GetInstance<ICrawler>();

            string[] pages = new string[1];
            pages[0] = "https://estadodeminas.vrum.com.br/";
            //pages[1] = "https://www.autoline.com.br/";

            crawler.StartCrawl(pages);

            Console.ReadKey();
        }
        static void InitializeContainer(Container container)
        {
            Bootstrapper.Register(container);
        }
    }
}
