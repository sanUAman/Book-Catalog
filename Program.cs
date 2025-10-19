using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorPagesBook.Data;
using RazorPagesBook.Models;
using RazorPagesBook.Services;
using RazorPagesMovie.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<RazorPagesBookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RazorPagesBookContext") ?? throw new InvalidOperationException("Connection string 'RazorPagesBookContext' not found.")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("RazorPagesBookContext")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin"));
});

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Books", "RequireAdministratorRole");
    options.Conventions.AuthorizeFolder("/Reviews", "RequireAdministratorRole");
    options.Conventions.AuthorizeFolder("/Admin", "RequireAdministratorRole");
});

builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("Email"));
builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAdminAsync(scope.ServiceProvider, app.Configuration);
}

app.Run();
