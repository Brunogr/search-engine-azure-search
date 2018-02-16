using Abot.Crawler;
using Abot.Poco;
using Crawler.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crawler.Core
{
    public class AbotCrawler : ICrawler
    {
        private static IParser _parser;
        private CrawlConfiguration _crawlConfiguration;
        public AbotCrawler(IParser parser)
        {
            _parser = parser;
        }
        public void ConfigureCrawler(int maxPagesToCrawl, int timeoutSeconds, int maxConcurrentThreads)
        {
            _crawlConfiguration = new CrawlConfiguration();
            _crawlConfiguration.CrawlTimeoutSeconds = timeoutSeconds;
            _crawlConfiguration.MaxConcurrentThreads = maxConcurrentThreads;
            _crawlConfiguration.MaxPagesToCrawl = maxPagesToCrawl;
        }

        public void StartCrawl(string[] pages)
        {
            CrawlConfiguration();

            PoliteWebCrawler crawler = new PoliteWebCrawler(_crawlConfiguration);

            crawler.PageCrawlStartingAsync += Crawler_PageCrawlStartingAsync;
            crawler.PageCrawlCompletedAsync += Crawler_PageCrawlCompletedAsync;
            crawler.PageCrawlDisallowedAsync += Crawler_PageCrawlDisallowedAsync;
            crawler.PageLinksCrawlDisallowedAsync += Crawler_PageLinksCrawlDisallowedAsync;
            
            foreach (var page in pages)
            {
                _parser.IdentifyParser(page);

                var result = crawler.Crawl(new Uri(page));

                if (result.ErrorOccurred)
                    Console.WriteLine("Crawl of {0} completed with error: {1}", result.RootUri.AbsoluteUri, result.ErrorException.Message);
                else
                    Console.WriteLine("Crawl of {0} completed without error.", result.RootUri.AbsoluteUri);

                _parser.Save();
            }
        }

        private static void Crawler_PageLinksCrawlDisallowedAsync(object sender, PageLinksCrawlDisallowedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;
            Console.WriteLine("Did not crawl the links on page {0} due to {1}", crawledPage.Uri.AbsoluteUri, e.DisallowedReason);
        }

        private static void Crawler_PageCrawlDisallowedAsync(object sender, PageCrawlDisallowedArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;
            Console.WriteLine("Did not crawl page {0} due to {1}", pageToCrawl.Uri.AbsoluteUri, e.DisallowedReason);
        }

        private static void Crawler_PageCrawlCompletedAsync(object sender, PageCrawlCompletedArgs e)
        {
            CrawledPage crawledPage = e.CrawledPage;

            if (_parser.IsPageValid(e.CrawledPage.Content.Text))
            {
                _parser.Extract(e.CrawledPage.Content.Text);
            }            
        }

        private static void Crawler_PageCrawlStartingAsync(object sender, PageCrawlStartingArgs e)
        {
            PageToCrawl pageToCrawl = e.PageToCrawl;

            Console.WriteLine("About to crawl link {0} which was found on page {1}", pageToCrawl.Uri.AbsoluteUri, pageToCrawl.ParentUri.AbsoluteUri);
        }

        private void CrawlConfiguration()
        {
            if (_crawlConfiguration == null)
            {
                _crawlConfiguration = new Abot.Poco.CrawlConfiguration();
                _crawlConfiguration.CrawlTimeoutSeconds = 100;
                _crawlConfiguration.MaxConcurrentThreads = 10;
                _crawlConfiguration.MaxPagesToCrawl = 1000;
                _crawlConfiguration.IsRespectRobotsDotTextEnabled = true;
                _crawlConfiguration.IsRespectMetaRobotsNoFollowEnabled = false;
                _crawlConfiguration.UserAgentString = "abot v1.0 http://code.google.com/p/abot";
            }
        }

    }
}
