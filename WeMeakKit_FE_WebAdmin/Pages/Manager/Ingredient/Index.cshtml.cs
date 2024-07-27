using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeMeakKit_FE_WebAdmin.Models;

namespace WeMeakKit_FE_WebAdmin.Pages.Manager.Ingredient
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string Message { get; set; }
        public ApiResponse apiResponse { get; set; }

        public List<Ingredient> ingredientList { get; set; } = new List<Ingredient>();
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            PageNumber = pageNumber;

            var apiURL = "https://api.wemealkit.ddns.net/api/ingredients/get-all";
            var response = await _httpClient.GetAsync(apiURL);
            var responseString = await response.Content.ReadAsStringAsync();
            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);
            ingredientList = apiResponse.Data;

            TotalPages = (int)Math.Ceiling(ingredientList.Count / 6.0);

            ingredientList = ingredientList.Skip((PageNumber - 1) * 6).Take(6).ToList();

            Message = ingredientList.Any() ? ingredientList[0].Name : "No recipes found.";
            return Page();
        }


        public partial class Ingredient
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string? Img { get; set; }

            public string Unit { get; set; } = null!;

            public List<IngredientNutrient> ingredientNutrientList { get; set; }
            public List<IngredientCategory> ingredientCategoryList { get; set; }
        }

        public class IngredientNutrient
        {
            public Guid Id { get; set; }

            public Guid IngredientId { get; set; }

            public double Calories { get; set; }

            public double Fat { get; set; }

            public double SaturatedFat { get; set; }

            public double Sugar { get; set; }

            public double Carbonhydrate { get; set; }

            public double DietaryFiber { get; set; }

            public double Protein { get; set; }

            public double Sodium { get; set; }

            public virtual Ingredient Ingredient { get; set; } = null!;
        }

        public class IngredientCategory
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string Description { get; set; } = null!;

            public int Status { get; set; }

            public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        }

        public class ApiResponse
        {
            [JsonProperty("statusCode")]
            public int StatusCode { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("list")]
            public object List { get; set; }

            [JsonProperty("data")]
            public List<Ingredient> Data { get; set; }
        }
    }
}
