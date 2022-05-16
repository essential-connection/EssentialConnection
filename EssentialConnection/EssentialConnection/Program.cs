using EssentialConnection.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using EssentialConnection.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("EssentialConnectionDB-lucas");
var connectionString = builder.Configuration.GetConnectionString("EssentialConnectionDB");

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDbContext<Context>(
    options => options.UseSqlServer(connectionString)
);


builder.Services.AddDefaultIdentity<EssentialConnectionUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<IdentityContext>();;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

// Add-Migration Initcial-criacao -Context Context
// Update-database -Context Context

//StringDeConex�oGustavo : Favor, n�o excluir
//builder.Services.AddDbContext<Context>
//(options => options.UseSqlServer("Data Source=GUSTAVO-LAPTOP;Initial Catalog=EssentialConnection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

//builder.Services.AddDbContext<Context>(
//    options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EssentialConnectionBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
//);
//Fim string de conex�o Gustavo : Favor nao excluir

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();


app.Run();
