using System.Globalization;
using GerenciadorProdutos.Data;
using GerenciadorProdutos.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Padrao")));

var app = builder.Build();

var cultura = new CultureInfo("pt-BR");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultura),
    SupportedCultures = new List<CultureInfo> { cultura },
    SupportedUICultures = new List<CultureInfo> { cultura }
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Produtos.Any())
    {
        db.Produtos.AddRange(
            new Produto { Nome = "Teclado Mecânico", Descricao = "Teclado mecânico ABNT2 com switches azuis e iluminação RGB.", Preco = 289.90m, QuantidadeEstoque = 25 },
            new Produto { Nome = "Mouse Gamer", Descricao = "Mouse óptico com 6 botões programáveis e 12.000 DPI.", Preco = 149.50m, QuantidadeEstoque = 40 },
            new Produto { Nome = "Monitor 24 polegadas", Descricao = "Monitor Full HD IPS de 24 polegadas com 75 Hz.", Preco = 799.00m, QuantidadeEstoque = 12 }
        );
        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Produtos/Index");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produtos}/{action=Index}/{id?}");

app.Run();
