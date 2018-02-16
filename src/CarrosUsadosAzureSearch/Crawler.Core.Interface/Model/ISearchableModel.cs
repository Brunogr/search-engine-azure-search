using System;
using System.Collections.Generic;
using System.Text;

namespace Crawler.Core.Interface.Model
{
    public interface ISearchableModel
    {
        string Id { get; set; }
        string Name { get; set; } 
    }
}
