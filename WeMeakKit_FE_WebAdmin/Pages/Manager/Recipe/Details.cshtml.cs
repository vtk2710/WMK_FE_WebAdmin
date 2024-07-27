using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeMeakKit_FE_WebAdmin.Models;

namespace WeMeakKit_FE_WebAdmin.Pages.Manager.Recipe
{
    public enum DifficultLevel
    {
        Dễ = 0,
        Vừa = 1,
        Khó = 2
    }
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Recipe recipeDetails { get; set; }

        public ApiResponse apiResponse { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiURL = $"https://api.wemealkit.ddns.net/api/recipes/get-by-id?id={id}";
            var response = await _httpClient.GetAsync(apiURL);
            var responseString = await response.Content.ReadAsStringAsync();
            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);
            recipeDetails = apiResponse.Data;
            return Page();
        }

        public class Recipe
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public int ServingSize { get; set; }
            public int CookingTime { get; set; }
            public int Difficulty { get; set; }
            public string Description { get; set; }
            public string Notice { get; set; }
            public string Img { get; set; }
            public decimal Price { get; set; }
            public int Popularity { get; set; }
            public string ProcessStatus { get; set; }
            public int BaseStatus { get; set; }
            public DateTime? CreatedAt { get; set; }
            public Guid? CreatedBy { get; set; }
            public DateTime? ApprovedAt { get; set; }
            public Guid? ApprovedBy { get; set; }
            public DateTime? UpdatedAt { get; set; }
            public Guid? UpdatedBy { get; set; }
            public List<RecipeIngredient> recipeIngredients { get; set; }
            public List<RecipeCategory> recipeCategories { get; set; }
            public RecipeNutrient recipeNutrient { get; set; }
            public List<RecipeStep> recipeSteps { get; set; }
        }

        public partial class RecipeNutrient
        {
            public Guid Id { get; set; }

            public Guid RecipeId { get; set; }

            public double Calories { get; set; }

            public double Fat { get; set; }

            public double SaturatedFat { get; set; }

            public double Sugar { get; set; }

            public double Carbonhydrate { get; set; }

            public double DietaryFiber { get; set; }

            public double Protein { get; set; }

            public double Sodium { get; set; }
        }

        public class RecipeIngredient
        {
            public Guid Id { get; set; }

            public Guid RecipeId { get; set; }

            public Guid IngredientId { get; set; }

            public double Amount { get; set; }

            public Ingredient Ingredient { get; set; } = null!;
        }

        public class Ingredient
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string? Img { get; set; }

            public string Unit { get; set; } = null!;

            public double Price { get; set; }

            public string PackagingMethod { get; set; } = null!;

            public string PreservationMethod { get; set; } = null!;
        }

        public class RecipeCategory
        {
            public Guid Id { get; set; }

            public Guid CategoryId { get; set; }

            public Guid RecipeId { get; set; }

            public Category Category { get; set; } = null!;
        }

        public class Category
        {
            public Guid Id { get; set; }
            public string Type { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Status { get; set; }
        }

        public class RecipeStep
        {
            public Guid Id { get; set; }

            public Guid RecipeId { get; set; }

            public int Index { get; set; }

            public string? MediaUrl { get; set; }

            public string? ImageLink { get; set; }

            public string? Description { get; set; }

            public string Name { get; set; } = null!;
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
            public Recipe Data { get; set; }
        }
    }
}
