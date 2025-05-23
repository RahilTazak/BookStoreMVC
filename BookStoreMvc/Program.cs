using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookStoreMvc.Models.Domain;
using BookStoreMvc.Repositories.Abstract;
using BookStoreMvc.Repositories.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));

// For Identity  
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

//For redirecting unauthorized users to login page
builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/UserAuthentication/Login");

// For removing null fields in json results
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

//To disable caching globally
app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-store, no-cache";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";
    await next();
});
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
