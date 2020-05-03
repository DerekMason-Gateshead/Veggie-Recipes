using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veggie_Recipes.Models
{
    public class PuppyRecipesQuery
    {
        public PuppyRecipesQuery()
        {
            page = 1;
            recipeQuery = "";
            ingrediants = new List<string>();
        }

        public string recipeQuery { get; set; }

        public List<string> ingrediants { get; set; }

        public int page { get; set; }

        public string BuildQuery()
        {
            string data = "?";

            if (ingrediants.Count > 0)
            {
                data += "i=";

                data += ingrediants[0];

                for (int i = 1; i < ingrediants.Count; i++)
                {
                    data += ",";

                    data += ingrediants[i];
                }

            }

            if (recipeQuery.Length > 0)
            {
                data += "&q=";
                data += recipeQuery;
            }

            data += "&p=";

            data += page;

            return data;
        }
    }
}
