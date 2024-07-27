using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Json;
using WeMeakKit_FE_WebAdmin.Models;
using Newtonsoft.Json;

namespace WeMeakKit_FE_WebAdmin.Pages.Staff.Weekplan
{
    public class DetailsModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public DetailsModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public ApiResponse apiResponse { get; set; }
        public WeeklyPlan weeklyPlan { get; set; } = new WeeklyPlan();

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apiURL = $"https://api.wemealkit.ddns.net/api/weeklyplan/get-id?id={id}";
            var response = await _httpClient.GetFromJsonAsync<ApiResponse>(apiURL);

            if (response == null || response.StatusCode != 200 || response.Data == null)
            {
                return NotFound();
            }

            apiResponse = response;
            return Page();
        }

        public class WeeklyPlan
        {
            public Guid Id { get; set; }

            public DateTime? BeginDate { get; set; }

            public DateTime? EndDate { get; set; }

            public string? Description { get; set; }

            public DateTime CreateAt { get; set; }

            public string CreatedBy { get; set; } = null!;

            public DateTime? ApprovedAt { get; set; }

            public string? ApprovedBy { get; set; }

            public DateTime? UpdatedAt { get; set; }

            public string? UpdatedBy { get; set; }

            public string? ProcessStatus { get; set; }

            public string? Notice { get; set; }

            public string? Title { get; set; }

            public string? UrlImage { get; set; }

            public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

            public virtual ICollection<RecipesPlan> RecipesPlans { get; set; } = new List<RecipesPlan>();
        }

        public class RecipesPlan
        {
            public Guid Id { get; set; }

            public Guid RecipeId { get; set; }

            public Guid StandardWeeklyPlanId { get; set; }

            public int Quantity { get; set; }

            public double Price { get; set; }

            public int DayInWeek { get; set; }

            public int MealInDay { get; set; }

            public virtual Recipe recipe { get; set; } = null!;

            public virtual WeeklyPlan StandardWeeklyPlan { get; set; } = null!;
        }

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

            public DateTime CreatedAt { get; set; }

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

        public class ApiResponse
        {
            [JsonProperty("statusCode")]
            public int StatusCode { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("list")]
            public object List { get; set; }

            [JsonProperty("data")]
            public WeeklyPlan Data { get; set; }
        }

    }
}
