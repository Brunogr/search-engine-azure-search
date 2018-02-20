using CarrosUsadosAzureSearch.Domain.Models;
using Crawler.Core.Interface.Repository;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarrosUsadosAzureSearch.Infra.Data
{
    public class CarroRepository : ICarroRepository
    {
        private SearchServiceClient _searchServiceClient;
        private ISearchIndexClient _searchIndexClient;
        private ISearchIndexClient _saveIndexClient;
        static string _searchServiceName = "cooktest";
        static string _adminApiKey = "C77CC6A4D40AA1E051DFCAED622AA5C3";
        static string _queryKey = "AC4A2986B25CD291C34A57E96DAE12A0";
        static string _index = "carros";

        public CarroRepository()
        {
            _searchServiceClient = new SearchServiceClient(_searchServiceName, new SearchCredentials(_adminApiKey));

            if (!_searchServiceClient.Indexes.Exists(_index))
            {
                var definition = new Index()
                {
                    Name = _index,
                    Fields = FieldBuilder.BuildForType<Carro>()
                };

                _searchServiceClient.Indexes.Create(definition);
            }

            _saveIndexClient = _searchServiceClient.Indexes.GetClient(_index);
            _searchIndexClient = new SearchIndexClient(_searchServiceName, _index, new SearchCredentials(_queryKey));
        }

        public void Save(IList<Carro> values)
        {
            var carrosASeremIncluidos = new List<Carro>();

            var existingRecipes = GetAll();

            if (existingRecipes.Count > 0)
            {
                foreach (var value in values)
                {
                    if (!existingRecipes.Any(r => r.Document.Nome == value.Nome))
                        carrosASeremIncluidos.Add(value);
                }
            }
            else
            {
                carrosASeremIncluidos = values.ToList();
            }

            var batch = IndexBatch.Upload(carrosASeremIncluidos);
            _saveIndexClient.Documents.Index(batch);
        }

        public IList<SearchResult<Carro>> GetAll()
        {
            var results = _searchIndexClient.Documents.Search<Carro>("*");

            return results.Results;
        }

        public IList<Carro> Get(string words)
        {
            var results = _searchIndexClient.Documents.Search<Carro>(words);

            return results.Results.Select(a => a.Document).ToList();
        }
    }
}
