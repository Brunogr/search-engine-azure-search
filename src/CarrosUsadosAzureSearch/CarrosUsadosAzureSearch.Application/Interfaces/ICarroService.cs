using CarrosUsadosAzureSearch.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarrosUsadosAzureSearch.Application
{
    public interface ICarroService
    {
        List<CarroViewModel> BuscaCompleta(string termos);

        List<CarroViewModel> BuscaBooleana(string query);
    }
}
