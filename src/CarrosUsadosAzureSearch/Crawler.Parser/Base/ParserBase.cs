using CarrosUsadosAzureSearch.Domain.Models;
using Cook.Crawler.Parser.Interface;
using Crawler.Core.Interface;
using Crawler.Core.Interface.Repository;
using Crawler.Parser;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cook.Crawler.Parser.Base
{
    public class ParserBase : IParser
    {
        public List<Carro> _carros;
        private ICarroParser _parser;
        private ICarroRepository _carroRepository;
        private object _lock = new object();

        public ParserBase(ICarroRepository carroRepository)
        {
            _carroRepository = carroRepository;
            _carros = new List<Carro>();
        }

        public void Extract(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var carro = _parser.Extract(doc);

            lock (_lock)
            {
                if (!_carros.Any(r => r.Nome == carro.Nome))
                    _carros.Add(carro);
            }
        }

        public void IdentifyParser(string url)
        {
            _parser = Factory(url);
        }

        public bool IsPageValid(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            return _parser.IsPageValid(doc);
        }

        public ICarroParser GetParser()
        {
            return _parser;
        }

        private ICarroParser Factory(string url)
        {
            switch (url.Split('.')[1])
            {
                case "vrum":
                    return new VrumParser();
                case "autoline":
                    return new AutolineParser();
                default:
                    return new VrumParser();
            }
        }

        public void Save()
        {
            if (_carros.Count > 0)
                _carroRepository.Save(_carros);
        }
    }
}
