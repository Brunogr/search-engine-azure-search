using System;
using System.Collections.Generic;
using System.Text;
using CarrosUsadosAzureSearch.Application.ViewModel;
using Crawler.Core.Interface.Repository;
using AutoMapper;

namespace CarrosUsadosAzureSearch.Application.Services
{
    public class CarroService : ICarroService
    {
        private ICarroRepository _carroRepository;
        private IMapper _mapper;

        public CarroService(ICarroRepository carroRepository, IMapper mapper)
        {
            _carroRepository = carroRepository;
            _mapper = mapper;
        }

        public List<CarroViewModel> BuscaBooleana(string query)
        {
            var carros = _carroRepository.Get(query);

            return _mapper.Map<List<CarroViewModel>>(carros);
        }

        public List<CarroViewModel> BuscaCompleta(string termos)
        {
            var carros = _carroRepository.Get(termos);

            return _mapper.Map<List<CarroViewModel>>(carros);
        }
    }
}
