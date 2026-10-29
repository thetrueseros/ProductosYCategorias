using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductosYCategorias.Data;
using ProductosYCategorias.Models;
using ProductosYCategorias.Models.ViewModels;

namespace ProductosYCategorias.Controllers;

public class ProductsController : Controller
{
    private const int DefaultPageSize = 8;
    private static readonly int[] AllowedPageSizes = [4, 8, 12, 24];

    private readonly SampleDbContext _context;

    public ProductsController(SampleDbContext context)
    {
        _context = context;
    }

    // GET: Products?search=...&categoryId=...&sortOrder=...&page=1&pageSize=8
    public async Task<IActionResult> Index(
        string? search,
        int? categoryId,
        string? sortOrder,
        int page = 1,
        int pageSize = DefaultPageSize)
    {
        if (!AllowedPageSizes.Contains(pageSize))
        {
            pageSize = DefaultPageSize;
        }

        if (page < 1)
        {
            page = 1;
        }

        IQueryable<Product> query = _context.Products
            .AsNoTracking()
            .Include(p => p.Category);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(p => p.Name.Contains(term));
        }

        if (categoryId is > 0)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        query = sortOrder switch
        {
            "name_desc" => query.OrderByDescending(p => p.Name),
            "price" => query.OrderBy(p => p.Price),
            "price_desc" => query.OrderByDescending(p => p.Price),
            "date" => query.OrderBy(p => p.FechaCreacion),
            "date_desc" => query.OrderByDescending(p => p.FechaCreacion),
            _ => query.OrderBy(p => p.Name)
        };

        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        if (totalPages > 0 && page > totalPages)
        {
            page = totalPages;
        }

        var products = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var model = new ProductIndexViewModel
        {
            Products = products,
            Categories = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync(),
            Search = search,
            CategoryId = categoryId,
            SortOrder = sortOrder,
            PageSize = pageSize,
            PageInfo = new PageInfo
            {
                TotalItems = totalItems,
                ItemsPerPage = pageSize,
                CurrentPage = page
            }
        };

        return View(model);
    }

    // GET: Products/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        return product is null ? NotFound() : View(product);
    }

    // GET: Products/Create
    public async Task<IActionResult> Create()
    {
        await PopulateCategoriesAsync();
        return View();
    }

    // POST: Products/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Name,Price,Stock,CategoryId")] Product product)
    {
        if (!await CategoryExistsAsync(product.CategoryId))
        {
            ModelState.AddModelError(nameof(Product.CategoryId), "Selecciona una categoría válida.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync(product.CategoryId);
            return View(product);
        }

        product.FechaCreacion = DateTime.UtcNow;
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        TempData["Success"] = $"Producto «{product.Name}» creado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    // GET: Products/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null)
        {
            return NotFound();
        }

        await PopulateCategoriesAsync(product.CategoryId);
        return View(product);
    }

    // POST: Products/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock,CategoryId")] Product product)
    {
        if (id != product.Id)
        {
            return NotFound();
        }

        if (!await CategoryExistsAsync(product.CategoryId))
        {
            ModelState.AddModelError(nameof(Product.CategoryId), "Selecciona una categoría válida.");
        }

        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync(product.CategoryId);
            return View(product);
        }

        var existing = await _context.Products.FindAsync(id);
        if (existing is null)
        {
            return NotFound();
        }

        // Se actualizan solo los campos editables: FechaCreacion no se toca.
        existing.Name = product.Name;
        existing.Price = product.Price;
        existing.Stock = product.Stock;
        existing.CategoryId = product.CategoryId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ProductExistsAsync(id))
            {
                return NotFound();
            }

            throw;
        }

        TempData["Success"] = $"Producto «{existing.Name}» actualizado correctamente.";
        return RedirectToAction(nameof(Index));
    }

    // GET: Products/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        return product is null ? NotFound() : View(product);
    }

    // POST: Products/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        TempData["Success"] = $"Producto «{product.Name}» eliminado.";
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateCategoriesAsync(int? selectedId = null)
    {
        var categories = await _context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync();

        ViewData["CategoryId"] = new SelectList(
            categories,
            nameof(Category.Id),
            nameof(Category.Name),
            selectedId);
    }

    private Task<bool> CategoryExistsAsync(int categoryId) =>
        _context.Categories.AnyAsync(c => c.Id == categoryId);

    private Task<bool> ProductExistsAsync(int id) =>
        _context.Products.AnyAsync(p => p.Id == id);
}
