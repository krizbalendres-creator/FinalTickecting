using Microsoft.EntityFrameworkCore;
using BoholBusTicketing.Data.Data;
using BoholBusTicketing.Data.Repositories;
using BoholBusTicketing.Core.Interfaces;
using BoholBusTicketing.Core.Services;

var builder = WebApplicationBuilder.CreateBuilder(args);

// Add services to the container
const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=BusTicketingDb;Trusted_Connection=True;MultipleActiveResultSets=true";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register repositories
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IBarangayRepository, BarangayRepository>();
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();

// Register services
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ITicketService, TicketService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed database on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.EnsureCreated();
    var seeder = new DatabaseSeeder(db);
    seeder.SeedAsync().Wait();
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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