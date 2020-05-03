using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veggie_Recipes.Models
{
    public class PuppyRecipes
    {
        public string Title { get; set; }

        public string Version { get; set; }

        public string hRef { get; set; }

        public List<Recipe> results { get; set; }
    }
}
