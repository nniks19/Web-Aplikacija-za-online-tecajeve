using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WAZOT.DataAccess;
using WAZOT.DataAccess.Repository;
using WAZOT.DataAccess.Repository.IRepository;
using WAZOT.Middlewares;
using WAZOT.Repository;
using WAZOT.Repository.IRepository;
using WAZOT.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSession();
builder.Services.AddScoped<OsobaService, OsobaServiceImpl>();

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

app.UseMiddleware<AuthMiddleware>();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Posjetitelj}/{controller=HomePosjetitelj}/{action=Index}/{id?}");

app.Run();
