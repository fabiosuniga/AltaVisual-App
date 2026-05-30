using AltaVisual.Data;
using AltaVisual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AltaVisual.Controllers
{
    public class AdesivosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdesivosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Adesivos
        public async Task<IActionResult> Index()
        {
            // Filtra para mostrar apenas os Adesivos onde o PedidoId está vazio (null)
            var adesivosSoltos = await _context.Adesivos
                .Where(a => EF.Property<int?>(a, "PedidoId") == null)
                .ToListAsync();

            return View(adesivosSoltos);
        }

        // GET: Adesivos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adesivo = await _context.Adesivos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adesivo == null)
            {
                return NotFound();
            }

            return View(adesivo);
        }

        // GET: Adesivos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adesivos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,PrecoBase,LarguraCm,AlturaCm,Quantidade")] Adesivo adesivo)
        {
            if (ModelState.IsValid)
            {
                adesivo.CalcularValor();

                _context.Add(adesivo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adesivo);
        }

        // GET: Adesivos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adesivo = await _context.Adesivos.FindAsync(id);
            if (adesivo == null)
            {
                return NotFound();
            }
            return View(adesivo);
        }

        // POST: Adesivos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,PrecoBase,LarguraCm,AlturaCm,Quantidade")] Adesivo adesivo)
        {
            if (id != adesivo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // O sistema recalcula o valor e os metros com as medidas novas
                    adesivo.CalcularValor();

                    _context.Update(adesivo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdesivoExists(adesivo.Id))
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
            return View(adesivo);
        }

        // GET: Adesivos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adesivo = await _context.Adesivos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adesivo == null)
            {
                return NotFound();
            }

            return View(adesivo);
        }

        // POST: Adesivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adesivo = await _context.Adesivos.FindAsync(id);
            if (adesivo != null)
            {
                _context.Adesivos.Remove(adesivo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdesivoExists(int id)
        {
            return _context.Adesivos.Any(e => e.Id == id);
        }

        // GET: Adesivos/Vincular/5
        public IActionResult Vincular(int? id)
        {
            if (id == null) return NotFound();

            var adesivo = _context.Adesivos.Find(id);
            if (adesivo == null) return NotFound();

            // puxa o pedido, inclui o Cliente e cria um texto personalizado
            var pedidosParaDropdown = _context.Pedidos
                .Include(p => p.Cliente)
                .Select(p => new
                {
                    Id = p.Id,
                    Texto = $"#{p.Id} - {p.Cliente.Nome}" // junção de ID e Nome
                })
                .ToList();

            ViewData["PedidoId"] = new SelectList(pedidosParaDropdown, "Id", "Texto");

            return View(adesivo);
        }
        

        // POST: Adesivos/Vincular/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vincular(int id, int PedidoId)
        {
            var adesivo = await _context.Adesivos.FindAsync(id);

            // Busca o pedido já trazendo a lista de itens junto
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == PedidoId);

            if (adesivo != null && pedido != null)
            {
                // O Pedido aceita o Adesivo do mesmo jeito que aceitou o Banner
                pedido.Itens.Add(adesivo);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Pedidos");
            }

            return NotFound();
        }
    }
}
