﻿using Microsoft.Azure.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarrosUsadosAzureSearch.Domain.Models
{
    public class Carro
    {
        int _portas;
        public Carro(string nome, string detalhes, string ano, string km, string cor, string portas, string local, string preco, string fonte, List<string> adicionais)
        {
            Id = Guid.NewGuid().ToString();
            Nome = nome;
            Detalhes = detalhes;
            Ano = ano;
            Km = km;
            Portas = Int32.TryParse(portas, out _portas) ? _portas : 0;
            Adicionais = adicionais;
            Cor = cor;
            Local = local;
            Preco = preco;
            Fonte = fonte;
        }
        [Key]
        [IsFilterable]
        public string Id { get; set; }
        [IsFilterable, IsSortable, IsFacetable, IsSearchable]
        public string Nome { get; set; }
        [IsSearchable]
        public string Detalhes { get; set; }
        [IsSearchable]
        public string Ano { get; set; }
        [IsSearchable]
        public string Km { get; set; }
        [IsSearchable]
        public string Cor { get; set; }
        public int Portas { get; set; }
        [IsSearchable]
        public string Cambio { get; set; }
        [IsSearchable]
        public List<string> Adicionais { get; set; }
        [IsSearchable]
        public string Local { get; set; }
        [IsSearchable, IsFilterable]
        public string Preco { get; set; }
        [IsSearchable]
        public string Fonte { get; set; }
    }
}
