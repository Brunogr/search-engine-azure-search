using System;
using System.Collections.Generic;
using System.Text;

namespace Cook.Crawler.Core.Interface
{
    public interface ICrawler
    {
        void StartCrawl(string[] pages);
        void ConfigureCrawler(int maxPagesToCrawl, int timeoutSeconds, int maxConcurrentThreads);
    }
}
