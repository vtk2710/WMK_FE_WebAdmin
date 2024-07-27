using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeMeakKit_FE_WebAdmin.Models;
using System.Text.Json;

namespace WeMeakKit_FE_WebAdmin.Pages.Staff.Ingredient
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Ingredient IngredientCreate { get; set; } = new Ingredient();

        public string Message { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var apiURL = "https://api.wemealkit.ddns.net/api/ingredients/create-new";

            var requestBody = new
            {
                ingredientCategoryId = IngredientCreate.IngredientCategoryId,
                name = IngredientCreate.Name,
                img = IngredientCreate.Img,
                unit = IngredientCreate.Unit,
                price = IngredientCreate.Price,
                packagingMethod = IngredientCreate.PackagingMethod,
                preservationMethod = IngredientCreate.PreservationMethod,
                status = IngredientCreate.Status,
                createdBy = IngredientCreate.CreatedBy,
                nutrientInfo = new
                {
                    calories = IngredientCreate.IngredientNutrient?.Calories ?? 0,
                    fat = IngredientCreate.IngredientNutrient?.Fat ?? 0,
                    saturatedFat = IngredientCreate.IngredientNutrient?.SaturatedFat ?? 0,
                    sugar = IngredientCreate.IngredientNutrient?.Sugar ?? 0,
                    carbonhydrate = IngredientCreate.IngredientNutrient?.Carbohydrate ?? 0,
                    dietaryFiber = IngredientCreate.IngredientNutrient?.DietaryFiber ?? 0,
                    protein = IngredientCreate.IngredientNutrient?.Protein ?? 0,
                    sodium = IngredientCreate.IngredientNutrient?.Sodium ?? 0
                }
            };

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody, jsonOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, jsonContent);

            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseContent); // Log or output the response content

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Success"); // Adjust to your success page
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the ingredient.");
                Message = "Create failed";
                return Page();
            }
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
            public int Status { get; set; }
            public string CreatedBy { get; set; } = null!;
            public IngredientNutrient? IngredientNutrient { get; set; }
            public Guid IngredientCategoryId { get; set; }
        }

        public class IngredientNutrient
        {
            public Guid Id { get; set; }
            public Guid IngredientId { get; set; }
            public double Calories { get; set; }
            public double Fat { get; set; }
            public double SaturatedFat { get; set; }
            public double Sugar { get; set; }
            public double Carbohydrate { get; set; }
            public double DietaryFiber { get; set; }
            public double Protein { get; set; }
            public double Sodium { get; set; }
        }

        public class IngredientCategory
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = null!;
            public string Description { get; set; } = null!;
            public int Status { get; set; }
        }
    }
}
