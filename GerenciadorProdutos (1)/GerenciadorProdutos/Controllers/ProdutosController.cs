using GerenciadorProdutos.Data;
using GerenciadorProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorProdutos.Controllers;

public class ProdutosController(AppDbContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        var produtos = await context.Produtos.AsNoTracking().OrderBy(p => p.Nome).ToListAsync();
        return View(produtos);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var produto = await context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (produto is null) return NotFound();

        return View(produto);
    }

    public IActionResult Create() => View(new Produto());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Produto produto)
    {
        if (!ModelState.IsValid) return View(produto);

        context.Add(produto);
        await context.SaveChangesAsync();
        TempData["Sucesso"] = "Produto cadastrado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null) return NotFound();

        var produto = await context.Produtos.FindAsync(id);
        if (produto is null) return NotFound();

        return View(produto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Produto produto)
    {
        if (id != produto.Id) return NotFound();
        if (!ModelState.IsValid) return View(produto);

        try
        {
            context.Update(produto);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await context.Produtos.AnyAsync(p => p.Id == id)) return NotFound();
            throw;
        }

        TempData["Sucesso"] = "Produto atualizado com sucesso.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var produto = await context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (produto is null) return NotFound();

        return View(produto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var produto = await context.Produtos.FindAsync(id);
        if (produto is not null)
        {
            context.Produtos.Remove(produto);
            await context.SaveChangesAsync();
            TempData["Sucesso"] = "Produto excluído com sucesso.";
        }

        return RedirectToAction(nameof(Index));
    }
}
