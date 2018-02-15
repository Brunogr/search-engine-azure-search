using Cook.Crawler.Core.Interface;
using Cook.Crawler.Core.Interface.Repository;
using Cook.Crawler.Parser.Interface;
using Cook.Domain;
using Crawler.Core.Interface;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cook.Crawler.Parser.Base
{
    public class ParserBase : IParser
    {
        public List<Recipe> _recipes;
        private ISpecificParser _parser;
        private IRecipeRepository _recipeRepository;

        public ParserBase(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
            _recipes = new List<Recipe>();
        }

        public void ExtractRecipe(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var recipe = _parser.ExtractRecipe(doc);

            if (!_recipes.Any(r => r.Nome == recipe.Nome))
                _recipes.Add(recipe);
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

        public ISpecificParser GetParser()
        {
            return _parser;
        }

        private ISpecificParser Factory(string url)
        {
            switch (url.Split('.')[1])
            {
                case "vovopalmirinha":
                    return new PalmirinhaParser();
                default:
                    return new PalmirinhaParser();
            }
        }

        public void SaveRecipes()
        {
            if (_recipes.Count > 0)
                _recipeRepository.SaveRecipes(_recipes);
        }
    }
}
