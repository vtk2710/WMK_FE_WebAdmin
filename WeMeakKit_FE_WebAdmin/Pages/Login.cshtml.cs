using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace WeMeakKit_FE_WebAdmin.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public LoginModel(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }

        [Required]
        [BindProperty]
        public string Email { get; set; }

        [Required]
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; }

        public ApiResponse apiResponse { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var apiURL = "https://api.wemealkit.ddns.net/api/auth/login";
            var loginRequest = new LoginRequest 
            { 
                email = Email,
                password = Password
            };

            var jsonContent = JsonConvert.SerializeObject(loginRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(apiURL, content);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseString);
                var statusCode = apiResponse.StatusCode;
                var message = apiResponse.Message;
                if (statusCode == 200)
                {
                    _httpContextAccessor?.HttpContext?.Session.SetString("UserId", apiResponse.Data.Id);
                    if (apiResponse.Data.Role == "Admin")
                    {
                        return RedirectToPage("Admin/AdminPage");
                    }
                    else if(apiResponse.Data.Role == "Staff")
                    {
                        return RedirectToPage("Staff/StaffPage");
                    }
                    else
                    {
                        return RedirectToPage("Manager/ManagerPage");
                    }
                }
                else
                {
                    Message = apiResponse.Message;
                    return Page();
                }
            }
            return Page();
        }

        public class LoginRequest
        {
            [JsonProperty("emailOrUserName")]
            public string email { get; set; }

            [JsonProperty("password")]
            public string password { get; set; }
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
            public UserData Data { get; set; }
        }

        public class UserData
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("userName")]
            public string UserName { get; set; }

            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("lastName")]
            public string LastName { get; set; }

            [JsonProperty("gender")]
            public string Gender { get; set; }

            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("role")]
            public string Role { get; set; }
        }
    }
}
