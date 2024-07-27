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
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public string Message { get; set; }
        public ApiResponse apiResponse { get; set; }

        public List<Recipe> recipeList { get; set; } = new List<Recipe>();
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; }

        public async Task<IActionResult> OnGetAsync(int pageNumber = 1)
        {
            PageNumber = pageNumber;

            var apiURL = "https://api.wemealkit.ddns.net/api/recipes/get-all";
            var response = await _httpClient.GetAsync(apiURL);
            var responseString = await response.Content.ReadAsStringAsync();
            apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);
            recipeList = apiResponse.Data;

            TotalPages = (int)Math.Ceiling(recipeList.Count / 6.0);

            recipeList = recipeList.Skip((PageNumber - 1) * 6).Take(6).ToList();

            Message = recipeList.Any() ? recipeList[0].Name : "No recipes found.";
            return Page();
        }

        public class Recipe
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Img { get; set; }
            public string ProcessStatus { get; set; }
            public int BaseStatus { get; set; }
            public List<RecipeCategory> RecipeCategories { get; set; }
        }

        public class RecipeCategory
        {
            public Guid? Id { get; set; }

            public Guid? CategoryId { get; set; }

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

        public class ApiResponse
        {
            [JsonProperty("statusCode")]
            public int StatusCode { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("list")]
            public object List { get; set; }

            [JsonProperty("data")]
            public List<Recipe> Data { get; set; }
        }

    }
}
