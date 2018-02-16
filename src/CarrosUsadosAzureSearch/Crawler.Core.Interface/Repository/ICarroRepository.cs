using CarrosUsadosAzureSearch.Domain.Models;
using Crawler.Core.Interface.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Core.Interface.Repository
{
    public interface ICarroRepository
    {
        void Save(IList<Carro> values);

        IList<Carro> GetByWords(string words);
    }
}
