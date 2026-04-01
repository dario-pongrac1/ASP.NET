using lab_1.Data;
using lab_1.Models.Enums;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<WorkshopDataContext>();

var app = builder.Build();

var context = app.Services.GetRequiredService<WorkshopDataContext>();
await RunLab1DemoAsync(context);

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static async Task RunLab1DemoAsync(WorkshopDataContext context)
{
    Console.WriteLine("                                                            ");
    Console.WriteLine("                                                            ");
    Console.WriteLine("============================================================");
    Console.WriteLine("LAB 1 - Sustav za narucivanje kod automehanicara");
    Console.WriteLine("Minimalna demonstracija: model + LINQ + async/await");
    Console.WriteLine("============================================================");

    Console.WriteLine("SEED PODACI");
    Console.WriteLine($"Kupci: {context.Customers.Count}");
    Console.WriteLine($"Vozila: {context.Vehicles.Count}");
    Console.WriteLine($"Mehanicari: {context.Mechanics.Count}");
    Console.WriteLine($"Narudzbe: {context.ServiceOrders.Count}");
    Console.WriteLine($"Stavke narudzbi: {context.OrderLines.Count}");
    Console.WriteLine("------------------------------------------------------------");

    Console.WriteLine("LINQ UPITI");

    var orderCount = context.ServiceOrders.Count();
    Console.WriteLine($"1) Count: Ukupan broj narudzbi = {orderCount}");

    var firstForMechanic = context.ServiceOrders
        .Where(o => o.MechanicId == 2)
        .OrderBy(o => o.ScheduledAt)
        .FirstOrDefault();
    Console.WriteLine($"2) FirstOrDefault: Prva narudzba za mehanicara #2 = {firstForMechanic?.OrderNumber ?? "Nema"}");

    var orderByNumber = context.ServiceOrders
        .SingleOrDefault(o => o.OrderNumber == "SO-2026-002");
    Console.WriteLine($"3) SingleOrDefault: Trazenje SO-2026-002 = {orderByNumber?.Status.ToString() ?? "Nije pronadena"}");

    var ordersInPeriod = context.ServiceOrders
        .Where(o => o.ScheduledAt >= new DateTime(2026, 4, 2) && o.ScheduledAt <= new DateTime(2026, 4, 3, 23, 59, 59))
        .OrderBy(o => o.ScheduledAt)
        .ToList();
    Console.WriteLine($"4) Where + OrderBy + ToList: Narudzbe u periodu 2.4.-3.4.2026 = {ordersInPeriod.Count}");

    var activePerMechanic = context.Mechanics
        .Select(m => new
        {
            Mechanic = $"{m.FirstName} {m.LastName}",
            ActiveOrders = m.ServiceOrders.Count(o => o.Status == OrderStatus.Scheduled || o.Status == OrderStatus.InProgress)
        })
        .OrderByDescending(x => x.ActiveOrders)
        .ToList();
    foreach (var item in activePerMechanic)
    {
        Console.WriteLine($"5) Select + Count: {item.Mechanic} ima aktivnih narudzbi: {item.ActiveOrders}");
    }

    var topServices = context.OrderLines
        .GroupBy(line => line.ServiceItem!.Name)
        .Select(g => new { ServiceName = g.Key, UsageCount = g.Count() })
        .OrderByDescending(x => x.UsageCount)
        .ThenBy(x => x.ServiceName)
        .Take(3)
        .ToList();
    foreach (var item in topServices)
    {
        Console.WriteLine($"6) GroupBy + Take: {item.ServiceName} -> {item.UsageCount}");
    }

    var customersWithDiagnostic = context.Customers
        .Where(c => c.ServiceOrders.Any(order => order.Lines.Any(line => line.ServiceItem!.ServiceCategoryId == 2)))
        .ToList();
    Console.WriteLine($"7) Podupit Any(Any): kupci s dijagnostikom = {customersWithDiagnostic.Count}");
    Console.WriteLine("------------------------------------------------------------");

    Console.WriteLine("ASYNC/AWAIT DEMO");
    Console.WriteLine("8) Pokrecem paralelno brojanje statusa (Task.WhenAll)...");

    var scheduledTask = CountByStatusAsync(context, OrderStatus.Scheduled);
    var inProgressTask = CountByStatusAsync(context, OrderStatus.InProgress);
    await Task.WhenAll(scheduledTask, inProgressTask);

    Console.WriteLine($"9) Scheduled narudzbe: {scheduledTask.Result}");
    Console.WriteLine($"10) InProgress narudzbe: {inProgressTask.Result}");
    Console.WriteLine("------------------------------------------------------------");
    Console.WriteLine("Kraj Lab 1 demonstracije. Web dio ostaje spreman za sljedece labose.");
    Console.WriteLine("                                                            ");
    Console.WriteLine("                                                            ");
    Console.WriteLine("                                                            ");
    Console.WriteLine("                                                            ");



}

static async Task<int> CountByStatusAsync(WorkshopDataContext context, OrderStatus status)
{
    await Task.Delay(120);
    return context.ServiceOrders.Count(o => o.Status == status);
}
