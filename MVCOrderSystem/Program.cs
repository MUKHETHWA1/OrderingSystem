using Azure.Storage.Queues;
using Microsoft.EntityFrameworkCore;
using MVCOrderSystem.Services;
using SharedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("RealTimeDb")));

//Queue backend(we'll default to storage for local dev)
var backend = builder.Configuration["QueueBackend"] ?? "Storage";
if (backend.Equals("Storage", StringComparison.OrdinalIgnoreCase))
{
    var stgConn = builder.Configuration.GetConnectionString("StorageQueue")
        ?? "UseDevelopmentStorage=true";
    builder.Services.AddSingleton(new QueueServiceClient(stgConn));
    builder.Services.AddScoped<IQueueSender, QueueStorageSender>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
