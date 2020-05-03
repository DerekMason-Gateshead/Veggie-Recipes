using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Veggie_Recipes.Models;

namespace Veggie_Recipes.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Veggie_Recipes.Data.VeggieData.query.Length == 0)
            {
                ViewData["query"] = "Not defined please consider a query value such as soup, pasta, etc";
            }
            else
            {
                ViewData["query"] = Data.VeggieData.query;
            }
        
            return View(Data.VeggieData.listIngredients);
        }

        // GET: Edit ingredients
        public IActionResult Edit(int id)
        {

            ViewData["Message"] = Data.VeggieData.listIngredients[id];
            ViewData["NumTimes"] = id;
                       
            return View();
        }


        public IActionResult ChangeQuery()
        {
     
            if (Veggie_Recipes.Data.VeggieData.query.Length == 0)
            {
                ViewData["Input"] = "input type = \"text\" id=\"query\" name=\"query\" class=\"form-control\"";
            }
            else
            {
                ViewData["Input"] = "input type = \"text\" id=\"query\" name=\"query\" " + " value = " + Data.VeggieData.query + " class=\"form-control\"";
            }
         
            return View();
        }

        // GET: Addingredient
        public IActionResult Addingredient()
        {
            return View();
        }


        // GET: Remove Ingredient
        public IActionResult Remove(int id)
        {
            Data.VeggieData.listIngredients.Remove(Data.VeggieData.listIngredients[id]);

            return RedirectToAction(nameof(Index));
        }


        // POST: Edit ingredient
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string ingredient) // int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ingredient == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (id > Data.VeggieData.listIngredients.Count)
            {
                return NotFound();
            }

            Data.VeggieData.listIngredients[id] = ingredient;

            return RedirectToAction(nameof(Index));
        }

        // POST: Edit Ingredient
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeQuery(string query) // int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (query == null)
            {
                Data.VeggieData.query = "";
            }
            else
            {
                Data.VeggieData.query = query;
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Edit ingredient
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Addingredient(string ingredient) // int id, [Bind("Id,Title,ReleaseDate,Genre,Price")] Movie movie)
        {
            if (ingredient != null)
            {
                Data.VeggieData.listIngredients.Add(ingredient);
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Showrecipes(string name, int ID = 1)
        {
            //            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {ID}");
            PuppyRecipesQuery recipeQUery = new PuppyRecipesQuery();

            foreach (var item in Data.VeggieData.listIngredients)
            {
                recipeQUery.ingrediants.Add(item);
            }

            recipeQUery.recipeQuery = Data.VeggieData.query;

            GetRecipe gRecipe = new GetRecipe();

            gRecipe.query = recipeQUery.BuildQuery();

            var TaskWait = gRecipe.queryPuppyRecipes();

            TaskWait.Wait();

            if (TaskWait.Result)
            {
               
            }

            if (gRecipe.recipes.Count > 0)
            {

                ViewData["Message"] = "Veggie recipies for you";
            }
            else
            {
                ViewData["Message"] = "No recipes found consider changing your recipe category and/or ingredients";

            }
            ViewData["NumTimes"] = 1;

            return View(gRecipe.recipes);

        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
