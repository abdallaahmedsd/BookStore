using BookStore.BusinessLogic.Services;
using BookStore.DataAccess.DbInitializer;
using BookStore.DataAccess.EntityFrameworkCore.Data;
using BookStore.DataAccess.Repositories;
using BookStore.Models.Identity;
using BookStore.Utilties;
using BookstoreBackend.BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});


#region Custom Services
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddScoped<BookServices>();
builder.Services.AddScoped<CategoryServices>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<LanguageServices>();
builder.Services.AddScoped<ShoppingCartServices>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<SessionService>();
#endregion

#region Session Management
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
#endregion


var app = builder.Build();

SeedDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{Area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

void SeedDatabase()
{
    using (IServiceScope Scope = app.Services.CreateScope())
    {
        IDbInitializer initializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();

        initializer.Initialize();
    }
}
