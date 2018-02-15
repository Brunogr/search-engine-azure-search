using System;

namespace Crawler.Core.Interface
{
    public interface IParser
    {
        void IdentifyParser(string url);
        bool IsPageValid(string html);

        void Extract(string html);
        void Save();
    }
}
