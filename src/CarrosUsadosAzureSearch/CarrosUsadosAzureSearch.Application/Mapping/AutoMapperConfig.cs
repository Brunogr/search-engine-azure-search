using AutoMapper;
using CarrosUsadosAzureSearch.Application.Mapping.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarrosUsadosAzureSearch.Application.Mapping
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelProfile());                
            });
        }
    }
}
