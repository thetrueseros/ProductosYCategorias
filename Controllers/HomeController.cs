using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductosYCategorias.Data;
using ProductosYCategorias.Models;
using ProductosYCategorias.Models.ViewModels;

namespace ProductosYCategorias.Controllers;

public class HomeController : Controller
{
    private const int LowStockThreshold = 5;

    private readonly SampleDbContext _context;

    public HomeController(SampleDbContext context)
    {
        _context = context;
    }

    // GET: Home
    public async Task<IActionResult> Index()
    {
        var totalProducts = await _context.Products.CountAsync();
        var totalCategories = await _context.Categories.CountAsync();

        var totalStock = await _context.Products.SumAsync(p => (int?)p.Stock) ?? 0;
        var inventoryValue = await _context.Products
            .SumAsync(p => (decimal?)(p.Price * p.Stock)) ?? 0m;

        var lowStockCount = await _context.Products
            .CountAsync(p => p.Stock < LowStockThreshold);
        var outOfStockCount = await _context.Products.CountAsync(p => p.Stock == 0);

        var latestProducts = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .OrderByDescending(p => p.FechaCreacion)
            .ThenByDescending(p => p.Id)
            .Take(5)
            .ToListAsync();

        var lowStockProducts = await _context.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Where(p => p.Stock < LowStockThreshold)
            .OrderBy(p => p.Stock)
            .ThenBy(p => p.Name)
            .Take(5)
            .ToListAsync();

        var categoryRows = await _context.Categories
            .AsNoTracking()
            .Select(c => new
            {
                c.Id,
                c.Name,
                ProductCount = c.Products.Count
            })
            .OrderByDescending(c => c.ProductCount)
            .ThenBy(c => c.Name)
            .ToListAsync();

        var topCategories = categoryRows
            .Select(c => new CategorySummary(
                c.Id,
                c.Name,
                c.ProductCount,
                totalProducts == 0 ? 0 : c.ProductCount * 100d / totalProducts))
            .ToList();

        var model = new HomeViewModel
        {
            TotalProducts = totalProducts,
            TotalCategories = totalCategories,
            TotalStock = totalStock,
            InventoryValue = inventoryValue,
            LowStockCount = lowStockCount,
            OutOfStockCount = outOfStockCount,
            LatestProducts = latestProducts,
            LowStockProducts = lowStockProducts,
            TopCategories = topCategories
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? statusCode = null)
    {
        var code = statusCode ?? StatusCodes.Status500InternalServerError;
        Response.StatusCode = code;

        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            StatusCode = code,
            Message = code == StatusCodes.Status404NotFound
                ? "La página que buscas no existe."
                : "Ha ocurrido un error inesperado. Inténtalo de nuevo."
        });
    }
}
