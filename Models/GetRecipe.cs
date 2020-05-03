// #define USE_FILE
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Threading.Tasks;

namespace Veggie_Recipes.Models
{
    public class GetRecipe
    {
        public GetRecipe()
        {
            recipes = new List<Recipe>();
        }
        public List<Recipe> recipes { get; set; }
        string[] meats = new string[] { "meats", "salmon", "steak", "bacon", "beef", "chicken", "prosciutto", "Prosciutto", "ham", "pork", "anchovy", "anchovies", "lamb", "cod", "squid", "tuna", "fish", "turkey", "salami", "pepperoni" };

        public string query { get; set; }

        public PuppyRecipes myJsonObject { get; set; }

        public async Task<bool> queryPuppyRecipes()
        {
            bool bResult = false;
            recipes.Clear();

            try
            {

                HttpRequestCachePolicy requestPolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.CacheIfAvailable);

                WebRequest request = WebRequest.Create(new Uri("http://www.recipepuppy.com/api/" + query));
                request.UseDefaultCredentials = true;

                // Set the policy for this request only.  
                request.CachePolicy = requestPolicy;

                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();


#if USE_FILE
                string myJsonString = System.IO.File.ReadAllText("C:\\Users\\Derek.Mason\\Documents\\recipejson.txt");

                if (true)
                {
                    myJsonObject = JsonConvert.DeserializeObject<PuppyRecipes>(myJsonString);
#else
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream s = response.GetResponseStream();
                    StreamReader reader = new StreamReader(s);

                    myJsonObject = JsonConvert.DeserializeObject<PuppyRecipes>(reader.ReadToEnd());

                    s.Close();
                    reader.Close();
                    response.Close();
#endif
                    foreach (var item in myJsonObject.results)
                    {
                        bool veggie = true;

                        foreach (var ingredient in meats)
                        {
                            if (item.ingredients.ToLower().Contains(ingredient))
                            {
                                veggie = false;
                            }

                            if (item.Title.ToLower().Contains(ingredient))
                            {
                                veggie = false;
                            }
                        }

                        if (veggie)
                        {
                            recipes.Add(item);
                            bResult = true;
                        }
                    }

                }
                else
                {
                    myJsonObject = null;
                }

            }
            catch
            {
                recipes.Clear();

            }

            return bResult;
        }

    }
}
