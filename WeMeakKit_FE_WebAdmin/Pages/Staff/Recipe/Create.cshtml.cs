using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using WeMeakKit_FE_WebAdmin.Models;
using static System.Net.WebRequestMethods;

namespace WeMeakKit_FE_WebAdmin.Pages.Staff.Recipe
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CreateModel> _logger;
        private readonly IConfiguration _configuration;

        public CreateModel(HttpClient httpClient, ILogger<CreateModel> logger, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Recipe recipeCreate { get; set; } = new Recipe();

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        _logger.LogWarning("Model state is invalid.");
        //        return Page();
        //    }

        //    _logger.LogInformation("Model state is valid. Proceeding with API call.");

        //    try
        //    {
        //        // Extract RecipeIngredients and RecipeSteps from form data
        //        var recipeIngredients = HttpContext.Request.Form["recipeCreate.Ingredients"];
        //        var recipeSteps = HttpContext.Request.Form["recipeCreate.Steps"];

        //        _logger.LogInformation($"Recipe Ingredients Data: {recipeIngredients}");
        //        _logger.LogInformation($"Recipe Steps Data: {recipeSteps}");

        //        recipeCreate.RecipeIngredients = JsonConvert.DeserializeObject<List<RecipeIngredient>>(recipeIngredients);
        //        recipeCreate.RecipeSteps = JsonConvert.DeserializeObject<List<RecipeStep>>(recipeSteps);


        //        var apiURL = "https://api.wemealkit.ddns.net/api/recipes/create-new";

        //        var jsonContent = JsonConvert.SerializeObject(recipeCreate);
        //        _logger.LogInformation($"Request body: {jsonContent}");

        //        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        //        var response = await _httpClient.PostAsync(apiURL, content);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["SuccessMessage"] = "Recipe created successfully!";
        //            _logger.LogInformation("Recipe created successfully.");
        //            return RedirectToPage("./Index");
        //        }
        //        else
        //        {
        //            var responseBody = await response.Content.ReadAsStringAsync();
        //            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        //            _logger.LogDebug($"Failed to create recipe. Status Code: {response.StatusCode}, Response: {responseBody}");
        //            Console.WriteLine($"Failed to create recipe. Status Code: {response.StatusCode}, Response: {responseBody}");
        //            ModelState.AddModelError(string.Empty, $"Error occurred while creating the recipe: {apiResponse?.Message ?? "Unknown error"}");
        //            return Page();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while creating the recipe.");
        //        ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
        //        return Page();
        //    }
        //}


        public partial class Recipe
        {
            public Guid Id { get; set; }

            public string Name { get; set; } = null!;

            public int ServingSize { get; set; }

            public int Difficulty { get; set; }

            public string? Description { get; set; }

            public string? Img { get; set; }

            public double Price { get; set; }

            public int Popularity { get; set; }

            public int ProcessStatus { get; set; }

            public DateTime? CreatedAt { get; set; }

            public string CreatedBy { get; set; } = null!;

            public DateTime? ApprovedAt { get; set; }

            public string? ApprovedBy { get; set; }

            public DateTime? UpdatedAt { get; set; }

            public string? UpdatedBy { get; set; }

            public string? Notice { get; set; }

            public int BaseStatus { get; set; }

            public int CookingTime { get; set; }

            public virtual ICollection<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();

            public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

            public virtual RecipeNutrient? RecipeNutrient { get; set; }

            public virtual ICollection<RecipeStep> RecipeSteps { get; set; } = new List<RecipeStep>();

            public virtual ICollection<RecipesPlan> RecipesPlans { get; set; } = new List<RecipesPlan>();
        }

        public class RecipeIngredient
        {
            public Guid IngredientId { get; set; }
            public double Amount { get; set; }
        }


        public class RecipeStep
        {
            public int Index { get; set; }
            public string Name { get; set; } = string.Empty;
            public string MediaURL { get; set; } = string.Empty;
            public string ImageLink { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
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
