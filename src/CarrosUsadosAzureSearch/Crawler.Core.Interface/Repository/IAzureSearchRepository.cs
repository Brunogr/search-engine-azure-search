using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Core.Interface.Repository
{
    public interface IAzureSearchRepository<TEntity>
    {
        void Save(IList<TEntity> values);

        IList<TEntity> GetByWords(string words);
    }
}
