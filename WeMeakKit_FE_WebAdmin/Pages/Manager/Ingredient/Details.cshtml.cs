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
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Ingredient ingredientDetails { get; set; } = default!;

        public ApiResponse apiResponse { get; set; }
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiURL = $"https://api.wemealkit.ddns.net/api/ingredients/get?id={id}";
            var response = await _httpClient.GetAsync(apiURL);
            var responseString = await response.Content.ReadAsStringAsync();
            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);
            ingredientDetails = apiResponse.Data;
            return Page();
        }


        public class Ingredient
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string? Img { get; set; }

            public string Unit { get; set; } = null!;

            public string Status { get; set; } = null!; // Changed to string

            public DateTime CreatedAt { get; set; }

            public string CreatedBy { get; set; } = null!;

            public DateTime? UpdatedAt { get; set; }

            public string? UpdatedBy { get; set; }

            public Guid IngredientCategoryId { get; set; }

            public double Price { get; set; }

            public string PackagingMethod { get; set; } = null!;

            public string PreservationMethod { get; set; } = null!;

            public IngredientCategory IngredientCategory { get; set; } = null!;

            public IngredientNutrient? IngredientNutrient { get; set; }
        }

        public class IngredientCategory
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public string Description { get; set; } = null!;

            public string Status { get; set; } = null!; // Changed to string
        }

        public class IngredientNutrient
        {
            public Guid Id { get; set; }

            public Guid IngredientId { get; set; }

            public double Calories { get; set; }

            public double Fat { get; set; }

            public double SaturatedFat { get; set; }

            public double Sugar { get; set; }

            public double Carbohydrate { get; set; } // Fixed typo from Carbonhydrate to Carbohydrate

            public double DietaryFiber { get; set; }

            public double Protein { get; set; }

            public double Sodium { get; set; }
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
            public Ingredient Data { get; set; }
        }

    }
}
