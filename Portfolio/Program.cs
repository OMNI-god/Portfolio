using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Data.Iservices;
using Portfolio.Data.services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppliDB>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("con")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IInvestmentServices,InvestmentServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<ILogsServices, LogsServices>();
builder.Services.AddSession(o => o.IdleTimeout = TimeSpan.FromMinutes(5));
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
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
