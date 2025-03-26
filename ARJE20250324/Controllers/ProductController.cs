using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ARJE20250324.AppWebMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace ARJE20250324.AppWebMVC.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly Test20250324DbContext _context;

        public ProductController(Test20250324DbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index(Product products, int topRegistro = 10)
        {
            var query = _context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(products.ProductName))
                query = query.Where(s => s.ProductName.Contains(products.ProductName));
            if (!string.IsNullOrWhiteSpace(products.Description))
                query = query.Where(s => s.Description.Contains(products.Description));
            if (products.BrandId > 0)
                query = query.Where(s => s.BrandId == products.BrandId);
            if (products.Id > 0)
                query = query.Where(s => s.Id == products.Id);
            if (topRegistro > 0)
                query = query.Take(topRegistro);
            query = query
                .Include(p => p.Warehouse).Include(p => p.Brand);

            var marcas = _context.Brands.ToList();
            marcas.Add(new Brand { BrandName = "SELECCIONAR", Id = 0 });

            var categorias = _context.Warehouses.ToList();
            categorias.Add(new Warehouse { WarehouseName = "SELECCIONAR", Id = 0 });

            ViewBag.bodegaId = new SelectList(categorias, "Id", "WarehouseName", 0);
            ViewBag.marcaId = new SelectList(marcas, "Id", "BrandName", 0);

            return View(await query.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            ViewBag.marcaId = new SelectList(_context.Brands, "Id", "BrandName");
            ViewBag.bodegaId = new SelectList(_context.Warehouses, "Id", "WarehouseName");
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Description,Price,PurchasePrice,WarehouseId,BrandId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag["marcaId"] = new SelectList(_context.Brands, "Id", "BrandName", product.BrandId);
            ViewData["bodegaId"] = new SelectList(_context.Warehouses, "Id", "WarehouseName", product.WarehouseId); 
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["marcaId"] = new SelectList(_context.Brands, "Id", "BrandName", product.BrandId);
            ViewData["bodegaId"] = new SelectList(_context.Warehouses, "Id", "WarehouseName", product.WarehouseId);
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Description,Price,PurchasePrice,WarehouseId,BrandId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["marcaId"] = new SelectList(_context.Brands, "Id", "BrandName", product.BrandId);
            ViewData["bodegaId"] = new SelectList(_context.Warehouses, "Id", "WarehouseName", product.WarehouseId);
            return View(product);   
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Brand)
                .Include(p => p.Warehouse)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
