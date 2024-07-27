using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WeMeakKit_FE_WebAdmin.Models;

namespace WeMeakKit_FE_WebAdmin.Pages.Staff.Ingredient
{
    public class UpdateModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public UpdateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [BindProperty]
        public Ingredient IngredientUpdate { get; set; } = new Ingredient();

        public SelectList IngredientCategoryList { get; set; } = new SelectList(Enumerable.Empty<SelectListItem>());

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            // Correctly format the API URL with the ingredient id
            var apiUrl = $"https://api.wemealkit.ddns.net/api/ingredients/get/{id}";

            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                IngredientUpdate = await response.Content.ReadFromJsonAsync<Ingredient>();
                if (IngredientUpdate == null)
                {
                    return RedirectToPage("./Index");
                }
            }
            else
            {
                return RedirectToPage("./Index");
            }

            // Fetch ingredient categories for dropdown
            var categoryUrl = "https://api.wemealkit.ddns.net/api/ingredientcategories/get-all";
            var categoryResponse = await _httpClient.GetAsync(categoryUrl);
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categories = await categoryResponse.Content.ReadFromJsonAsync<List<IngredientCategory>>();
                IngredientCategoryList = new SelectList(categories, "Id", "Name", IngredientUpdate.IngredientCategoryId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload category list if model state is invalid
                var categoryUrl = "https://api.wemealkit.ddns.net/api/ingredientcategories/get-all";
                var categoryResponse = await _httpClient.GetAsync(categoryUrl);
                if (categoryResponse.IsSuccessStatusCode)
                {
                    var categories = await categoryResponse.Content.ReadFromJsonAsync<List<IngredientCategory>>();
                    IngredientCategoryList = new SelectList(categories, "Id", "Name", IngredientUpdate.IngredientCategoryId);
                }

                return Page();
            }

            var apiUrl = "https://api.wemealkit.ddns.net/api/ingredients/update";

            // Prepare request body
            var requestBody = new
            {
                id = IngredientUpdate.Id,
                ingredientCategoryId = IngredientUpdate.IngredientCategoryId,
                name = IngredientUpdate.Name,
                img = IngredientUpdate.Img,
                unit = IngredientUpdate.Unit,
                price = IngredientUpdate.Price,
                packagingMethod = IngredientUpdate.PackagingMethod,
                preservationMethod = IngredientUpdate.PreservationMethod,
                status = IngredientUpdate.Status,
                updatedAt = DateTime.UtcNow, // Use current date-time for the update
                updatedBy = "Current User", // Replace with actual user if available
                updateIngredientNutrientRequest = new
                {
                    calories = IngredientUpdate.IngredientNutrient?.Calories ?? 0,
                    fat = IngredientUpdate.IngredientNutrient?.Fat ?? 0,
                    saturatedFat = IngredientUpdate.IngredientNutrient?.SaturatedFat ?? 0,
                    sugar = IngredientUpdate.IngredientNutrient?.Sugar ?? 0,
                    carbonhydrate = IngredientUpdate.IngredientNutrient?.Carbohydrate ?? 0,
                    dietaryFiber = IngredientUpdate.IngredientNutrient?.DietaryFiber ?? 0,
                    protein = IngredientUpdate.IngredientNutrient?.Protein ?? 0,
                    sodium = IngredientUpdate.IngredientNutrient?.Sodium ?? 0
                }
            };

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var jsonContent = new StringContent(JsonSerializer.Serialize(requestBody, jsonOptions), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(apiUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the ingredient.");
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
