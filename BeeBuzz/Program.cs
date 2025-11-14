using BeeBuzz.Data;
using BeeBuzz.Data.Entities;
using BeeBuzz.Data.Interfaces;
using BeeBuzz.Data.Repositories;
using BeeBuzz.Data.Seeding;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString)
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IApplicationUserRepository, BeeBuzzApplicationUserRepository>(); 
builder.Services.AddScoped<IOrganizationRepository, BeeBuzzOrganizationRepository>(); 
builder.Services.AddScoped<IBeehiveRepository, BeeBuzzBeehiveRepository>(); 

builder.Services.AddTransient<BeeBuzzSeeder>(); 

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
static async Task RunSeeding(WebApplication app)
{
    var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
    {
        var seeder = scope.ServiceProvider.GetService<BeeBuzzSeeder>();
        await seeder.Seed();
    }
}
await RunSeeding(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


