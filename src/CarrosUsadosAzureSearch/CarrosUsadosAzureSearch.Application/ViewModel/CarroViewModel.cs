using System;
using System.Collections.Generic;
using System.Text;

namespace CarrosUsadosAzureSearch.Application.ViewModel
{
    public class CarroViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Detalhes { get; set; }
        public string Ano { get; set; }
        public string Km { get; set; }
        public string Cor { get; set; }
        public int Portas { get; set; }
        public string Cambio { get; set; }
        public List<string> Adicionais { get; set; }
        public string Local { get; set; }
        public string Preco { get; set; }
        public string Fonte { get; set; }
    }
}
