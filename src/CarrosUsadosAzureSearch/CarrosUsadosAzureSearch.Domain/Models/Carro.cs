using Crawler.Core.Interface.Model;
using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;

namespace CarrosUsadosAzureSearch.Domain.Models
{
    public class Carro : ISearchableModel
    {
        public Carro()
        {
            Id = Guid.NewGuid().ToString();
        }
        [IsFilterable]
        public string Id { get; set; }
        [IsFilterable, IsSortable, IsFacetable, IsSearchable]
        public string Name { get; set; }
        [IsSearchable]
        public string Detalhes { get; set; }
        [IsSearchable]
        public string Ano { get; set; }
        [IsSearchable]
        public string Km { get; set; }
        [IsSearchable]
        public string Cor { get; set; }
        [IsSearchable]
        public int Portas { get; set; }
        [IsSearchable]
        public string Cambio { get; set; }
        [IsSearchable]
        public List<string> Adicionais { get; set; }
        [IsSearchable]
        public string Descricao { get; set; }
    }
}
