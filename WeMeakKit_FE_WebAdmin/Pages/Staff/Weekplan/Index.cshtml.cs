using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WeMeakKit_FE_WebAdmin.Models;

namespace WeMeakKit_FE_WebAdmin.Pages.Staff.Weekplan
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public IndexModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<WeeklyPlan> weeklyPlanList { get; set; } = new List<WeeklyPlan>();
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public int PageSize { get; set; } = 6; // Number of items per page
        public int TotalItems { get; set; } = 0;

        public async Task OnGetAsync(int pageNumber = 1)
        {
            CurrentPage = pageNumber;

            var apiURL = "https://api.wemealkit.ddns.net/api/weeklyplan/get-all";
            var response = await _httpClient.GetAsync(apiURL);
            response.EnsureSuccessStatusCode();

            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<List<WeeklyPlan>>>();
            if (apiResponse != null && apiResponse.Data != null)
            {
                TotalItems = apiResponse.Data.Count;
                TotalPages = (int)Math.Ceiling(TotalItems / (double)PageSize);
                weeklyPlanList = apiResponse.Data
                    .Skip((CurrentPage - 1) * PageSize)
                    .Take(PageSize)
                    .ToList();
            }
            else
            {
                weeklyPlanList = new List<WeeklyPlan>();
            }
        }


        public partial class WeeklyPlan
        {
            public Guid Id { get; set; }
            public string? Title { get; set; }

            public string? UrlImage { get; set; }
        }
        public class ApiResponse<T>
        {
            [JsonProperty("statusCode")]
            public int StatusCode { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("list")]
            public object List { get; set; }

            [JsonProperty("data")]
            public List<WeeklyPlan> Data { get; set; }
        }
    }
}
