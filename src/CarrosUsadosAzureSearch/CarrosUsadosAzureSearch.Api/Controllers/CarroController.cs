using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarrosUsadosAzureSearch.Api.Controllers.Base;
using CarrosUsadosAzureSearch.Application;
using CarrosUsadosAzureSearch.Application.ViewModel;

namespace CarrosUsadosAzureSearch.Api.Controllers
{
    public class CarroController : BaseController
    {
        private ICarroService _carroService;
        public CarroController(ICarroService carroService)
        {
            _carroService = carroService;
        }

        [HttpGet]
        public List<CarroViewModel> BuscaCompleta(string termos)
        {
            var carros = _carroService.BuscaCompleta(termos);

            return carros;
        }

        [HttpGet("booleana")]
        public List<CarroViewModel> BuscaBooleana(string query)
        {
            var carros = _carroService.BuscaBooleana(query);

            return carros;
        }

    }
}
