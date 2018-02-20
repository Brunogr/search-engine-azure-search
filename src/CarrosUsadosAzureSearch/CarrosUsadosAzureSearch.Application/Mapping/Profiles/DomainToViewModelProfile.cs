using AutoMapper;
using CarrosUsadosAzureSearch.Application.ViewModel;
using CarrosUsadosAzureSearch.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarrosUsadosAzureSearch.Application.Mapping.Profiles
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<Carro, CarroViewModel>();
        }
    }
}
