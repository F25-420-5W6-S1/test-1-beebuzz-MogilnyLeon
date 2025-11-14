using BeeBuzz.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace BeeBuzz.Data.Seeding
{
    public class BeeBuzzSeeder
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hosting;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public BeeBuzzSeeder(ApplicationDbContext context, IWebHostEnvironment hosting, RoleManager<IdentityRole<int>> roleManager, UserManager<ApplicationUser> userManager)
        {
            _db = context;
            _hosting = hosting;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            const string adminRoleName = "Admin";
            const string adminUsername = "admin";
            const string adminEmail = "admin@BeeBuzz.com";
            const string adminPassword = "Password321?";

            _db.Database.EnsureCreated();

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole<int>("Admin"));
                await _roleManager.CreateAsync(new IdentityRole<int>("Supervisor"));
                await _roleManager.CreateAsync(new IdentityRole<int>("Default"));
            }

            var organization = new Organization()
            {
                Name = "0000-0000-0000-0000"
            };
            // Stuck here v : Invalid Object Name
            if (!_db.Organization.Any())
            {
                _db.Organization.Add(organization);
                _db.SaveChanges();
            }

            ApplicationUser admin = await SeedAdminUser(organization);

            var seeData = LoadSeedData();

            if(!_db.Beehive.Any())
            {
                foreach (Beehive hive in seeData)
                {
                    hive.User = admin;
                }
                _db.Beehive.AddRange(seeData);

                _db.SaveChanges();
            }

        }
        private IEnumerable<Beehive> LoadSeedData()
        {
            var filePath = Path.Combine(_hosting.ContentRootPath, "Data\\data.json");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Seed data file not found: {filePath}");

            var json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<IEnumerable<Beehive>>(json, options);
            return data ?? [];
        }

        private async Task<ApplicationUser> SeedAdminUser(Organization organization)
        {
            const string adminRoleName = "Admin";
            const string adminUsername = "admin";
            const string adminEmail = "admin@ngg.com";
            const string adminPassword = "Password321?";

            // Ensure the Admin role exists
            if (!await _roleManager.RoleExistsAsync(adminRoleName))
            {
                await _roleManager.CreateAsync(new IdentityRole<int>(adminRoleName));
            }

            // Check if admin user already exists
            var existingAdmin = await _userManager.FindByEmailAsync(adminEmail);
            if (existingAdmin == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Organization = organization
                };

                var result = await _userManager.CreateAsync(adminUser, adminPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUser, adminRoleName);
                    _db.SaveChanges();
                    return adminUser;
                }
                else
                {
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            return existingAdmin;
        }
    }
}
