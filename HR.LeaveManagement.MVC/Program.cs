using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services;
using HR.LeaveManagement.MVC.Services.Base;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

ConfigureServices(builder.Services);
 void ConfigureServices(IServiceCollection services)
{
    services.AddHttpClient<IClient, Client>(c1 => c1.BaseAddress = new Uri("https://localhost:7286/"));
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddSingleton<ILocalStorageService, LocalStorageService>();
    services.AddScoped<ILeaveTypeService, LeaveTypeService>();
    services.AddControllersWithViews();
}

var app = builder.Build();

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

//app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
