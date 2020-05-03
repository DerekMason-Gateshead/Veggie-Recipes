using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veggie_Recipes.Models
{
    public class Recipe
    {
        public string Title { get; set; }
        public string Href { get; set; }
        public string ingredients { get; set; }
        public string thumbnail { get; set; }
    }
}
