using Microsoft.EntityFrameworkCore;
using WeMeakKit_FE_WebAdmin.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

builder.Services.AddDbContext<WeMealKitContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnect")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
