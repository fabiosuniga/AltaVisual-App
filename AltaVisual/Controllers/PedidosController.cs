using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AltaVisual.Data;
using AltaVisual.Models;

namespace AltaVisual.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Pedidos.Include(p => p.Cliente);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Itens)   // trás os produtos juntos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome");
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,Status")] Pedido pedido)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Itens");

            if (ModelState.IsValid)
            {
                pedido.Data = DateTime.Now;
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // retorno caso o modelo for inválido
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            return View(pedido);
        }

        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,Data,Status")] Pedido pedido)
        {
            if (id != pedido.Id) return NotFound();

            ModelState.Remove("Cliente");
            ModelState.Remove("Itens");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // Retorno de sucesso
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Pedidos.Any(e => e.Id == pedido.Id)) return NotFound();
                    else throw;
                }
            }

            //Se o ModelState for inválido, o código precisa saber para onde ir
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Nome", pedido.ClienteId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Busca o pedido incluindo a lista de itens
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pedido != null)
            {
                // solta os itens: remove o vínculo do PedidoId deles
                pedido.Itens.Clear();

                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
