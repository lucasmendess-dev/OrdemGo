using Microsoft.EntityFrameworkCore;
using OrdemGo.DAO;
using OrdemGo.Data;
using OrdemGo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<OrdemGoContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("OrdemGoConnection")
        ?? throw new InvalidOperationException("A conexão 'OrdemGoConnection' não foi configurada.")));

builder.Services.AddScoped<ClienteDAO>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<OrdemServicoDAO>();
builder.Services.AddScoped<OrdemServicoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
