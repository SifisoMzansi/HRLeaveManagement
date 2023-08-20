using HR.LeaveManagement.MVC.Contracts;
using HR.LeaveManagement.MVC.Services;
using HR.LeaveManagement.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews();

ConfigureServices(builder.Services);
 void ConfigureServices(IServiceCollection services)
{
    services.AddHttpContextAccessor();

    services.Configure<CookiePolicyOptions>(options =>
    {
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });
    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
    services.AddScoped<IClient>(_ => new Client("https://localhost:7286/", new HttpClient()));

    services.AddTransient<IAuthenticationService, AuthenticationService>();

    // services.AddScoped<IClient>( new Func<IServiceProvider, IClient>)
    //  services.AddScoped<IClient>(_ => new Client(new Uri("https://localhost:7286/") , new ));

    //<IClient, Client>(c1 => c1.BaseAddress = new Uri("https://localhost:7286/"));
   // services.AddScoped<IClient>( ); 

    //services.AddHttpClient<IClient, Client>(c1 => c1.BaseAddress = new Uri("https://localhost:7286/"));

    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddSingleton<ILocalStorageService, LocalStorageService>();
    services.AddScoped<ILeaveTypeService, LeaveTypeService>();
    services.AddScoped<ILeaveAllocationService, LeaveAllocationService>();

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
