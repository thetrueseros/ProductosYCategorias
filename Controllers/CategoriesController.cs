using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosYCategorias.Data;
using ProductosYCategorias.Models;

namespace ProductosYCategorias.Controllers;

public class CategoriesController : Controller
{
    private readonly SampleDbContext _context;

    public CategoriesController(SampleDbContext context)
    {
        _context = context;
    }

    // GET: Categories
    public async Task<IActionResult> Index()
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Products)
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(categories);
    }

    // GET: Categories/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        return category is null ? NotFound() : View(category);
    }

    // GET: Categories/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Categories/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Description")] Category category)
    {
        Normalize(category);

        if (await NameExistsAsync(category.Name))
        {
            ModelState.AddModelError(nameof(Category.Name), "Ya existe una categoría con ese nombre.");
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        _context.Categories.Add(category);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(Category.Name), "Ya existe una categoría con ese nombre.");
            return View(category);
        }

        TempData["Success"] = $"Categoría «{category.Name}» creada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return category is null ? NotFound() : View(category);
    }

    // POST: Categories/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category)
    {
        if (id != category.Id)
        {
            return NotFound();
        }

        Normalize(category);

        if (await NameExistsAsync(category.Name, excludeId: id))
        {
            ModelState.AddModelError(nameof(Category.Name), "Ya existe una categoría con ese nombre.");
        }

        if (!ModelState.IsValid)
        {
            return View(category);
        }

        var existing = await _context.Categories.FindAsync(id);
        if (existing is null)
        {
            return NotFound();
        }

        existing.Name = category.Name;
        existing.Description = category.Description;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(Category.Name), "Ya existe una categoría con ese nombre.");
            return View(category);
        }

        TempData["Success"] = $"Categoría «{existing.Name}» actualizada correctamente.";
        return RedirectToAction(nameof(Index));
    }

    // GET: Categories/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .AsNoTracking()
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        return category is null ? NotFound() : View(category);
    }

    // POST: Categories/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var category = await _context.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category is null)
        {
            return NotFound();
        }

        if (category.Products.Count > 0)
        {
            TempData["Error"] =
                $"No se puede eliminar «{category.Name}» porque tiene {category.Products.Count} producto(s) asociado(s).";
            return RedirectToAction(nameof(Index));
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();

        TempData["Success"] = $"Categoría «{category.Name}» eliminada.";
        return RedirectToAction(nameof(Index));
    }

    private static void Normalize(Category category)
    {
        category.Name = category.Name?.Trim() ?? string.Empty;
        category.Description = string.IsNullOrWhiteSpace(category.Description)
            ? null
            : category.Description.Trim();
    }

    private Task<bool> NameExistsAsync(string name, int? excludeId = null) =>
        _context.Categories.AnyAsync(c => c.Name == name && (excludeId == null || c.Id != excludeId));
}
