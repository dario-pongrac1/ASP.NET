using lab_1.Data;
using lab_1.Services.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WorkshopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WorkshopDbContext")));
builder.Services.AddScoped<IWorkshopReadRepository, WorkshopReadRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<WorkshopDbContext>();
        dbContext.Database.Migrate();
        WorkshopSeedData.Initialize(dbContext);
        Console.WriteLine("✓ Database migration and seed completed successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Error during database initialization: {ex.Message}");
        Console.WriteLine(ex.StackTrace);
        throw;
    }
}

app.MapControllerRoute(
    name: "customers_list",
    pattern: "kupci",
    defaults: new { controller = "Customers", action = "Index" });

app.MapControllerRoute(
    name: "customers_details",
    pattern: "kupci/{id:int}",
    defaults: new { controller = "Customers", action = "Details" });

app.MapControllerRoute(
    name: "vehicles_list",
    pattern: "vozila",
    defaults: new { controller = "Vehicles", action = "Index" });

app.MapControllerRoute(
    name: "vehicles_details",
    pattern: "vozila/{id:int}",
    defaults: new { controller = "Vehicles", action = "Details" });

app.MapControllerRoute(
    name: "mechanics_list",
    pattern: "mehanicari",
    defaults: new { controller = "Mechanics", action = "Index" });

app.MapControllerRoute(
    name: "mechanics_details",
    pattern: "mehanicari/{id:int}",
    defaults: new { controller = "Mechanics", action = "Details" });

app.MapControllerRoute(
    name: "serviceorders_list",
    pattern: "radni-nalozi",
    defaults: new { controller = "ServiceOrders", action = "Index" });

app.MapControllerRoute(
    name: "serviceorders_details",
    pattern: "radni-nalozi/{id:int}",
    defaults: new { controller = "ServiceOrders", action = "Details" });

app.MapControllerRoute(
    name: "servicecategories_list",
    pattern: "kategorije-usluga",
    defaults: new { controller = "ServiceCategories", action = "Index" });

app.MapControllerRoute(
    name: "servicecategories_details",
    pattern: "kategorije-usluga/{id:int}",
    defaults: new { controller = "ServiceCategories", action = "Details" });

app.MapControllerRoute(
    name: "serviceitems_list",
    pattern: "usluge",
    defaults: new { controller = "ServiceItems", action = "Index" });

app.MapControllerRoute(
    name: "serviceitems_details",
    pattern: "usluge/{id:int}",
    defaults: new { controller = "ServiceItems", action = "Details" });

app.MapControllerRoute(
    name: "orderlines_list",
    pattern: "stavke-naloga",
    defaults: new { controller = "OrderLines", action = "Index" });

app.MapControllerRoute(
    name: "orderlines_details",
    pattern: "stavke-naloga/{id:int}",
    defaults: new { controller = "OrderLines", action = "Details" });

app.MapControllerRoute(
    name: "appointmentslots_list",
    pattern: "termini",
    defaults: new { controller = "AppointmentSlots", action = "Index" });

app.MapControllerRoute(
    name: "appointmentslots_details",
    pattern: "termini/{id:int}",
    defaults: new { controller = "AppointmentSlots", action = "Details" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
