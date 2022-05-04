using EssentialConnection.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add-Migrations Initial-criacao -Context Context
// Update-database -Context Context

//StringDeConex�oGustavo : Favor, n�o excluir
//builder.Services.AddDbContext<Context>
//(options => options.UseSqlServer("Data Source=GUSTAVO-LAPTOP;Initial Catalog=EssentialConnection;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
builder.Services.AddDbContext<Context>(
    options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StrongFitGymDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
);
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Empresas}/{action=Index}/{id?}");


app.Run();
