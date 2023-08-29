using BlogWebApp.Data;
using BlogWebApp.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppsDbContext>(Options=>Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => {
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;


}
).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppsDbContext>();//AddDefaultTokenProviders();
builder.Services.AddTransient<IRepository, Repository>();

var app = builder.Build();

//Identity & Admin Seed

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppsDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        db.Database.EnsureCreated();

        var adminRole = new IdentityRole("Admin");
        if (!db.Roles.Any())
        {
            //used the GetAwaiter 
            roleManager.CreateAsync(adminRole).GetAwaiter().GetResult();
        }
        if (!db.Users.Any(u => u.UserName == "admin"))
        {
            //create admin
            var adminUser = new IdentityUser { UserName = "admin", Email = "admin@gmail.com" };
            var result = userManager.CreateAsync(adminUser, "password").GetAwaiter().GetResult();
            //add role to user
            userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();

        }
    }

    catch (Exception ex)
    {
    
        Console.WriteLine(ex.ToString());
    }
}
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();



    
