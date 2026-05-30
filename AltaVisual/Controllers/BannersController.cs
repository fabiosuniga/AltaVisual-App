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
    public class BannersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BannersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Banners
        public async Task<IActionResult> Index()
        {
            // Filtra para mostrar apenas os Banners onde o PedidoId está vazio (null)
            var bannersSoltos = await _context.Banners
                .Where(b => EF.Property<int?>(b, "PedidoId") == null)
                .ToListAsync();

            return View(bannersSoltos);
        }

        // GET: Banners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // GET: Banners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Banners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,PrecoBase,Largura,Altura,Quantidade,Acabamento")] Banner banner)
        {
            if (ModelState.IsValid)
            {
       
                banner.CalcularValor();

                _context.Add(banner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        // GET: Banners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
            {
                return NotFound();
            }
            return View(banner);
        }

        // POST: Banners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Troquei o ValorFinal pelo Quantidade aqui no Bind:
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,PrecoBase,Largura,Altura,Quantidade,Acabamento")] Banner banner)
        {
            if (id != banner.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // RECALCULA O VALOR COM AS NOVAS MEDIDAS/QUANTIDADE ANTES DE SALVAR
                    banner.CalcularValor();

                    _context.Update(banner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BannerExists(banner.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(banner);
        }

        // GET: Banners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banner = await _context.Banners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banner == null)
            {
                return NotFound();
            }

            return View(banner);
        }

        // POST: Banners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner != null)
            {
                _context.Banners.Remove(banner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BannerExists(int id)
        {
            return _context.Banners.Any(e => e.Id == id);
        }

        // GET: Banners/Vincular/5 (Abre a tela e carrega os pedidos no Dropdown)
        public IActionResult Vincular(int? id)
        {
            if (id == null) return NotFound();

            var banner = _context.Banners.Find(id);
            if (banner == null) return NotFound();

            // puxa o pedido, inclui o Cliente e cria um texto personalizado
            var pedidosParaDropdown = _context.Pedidos
                .Include(p => p.Cliente)
                .Select(p => new
                {
                    Id = p.Id,
                    Texto = $"#{p.Id} - {p.Cliente.Nome}" // junção ID e nome
                })
                .ToList();

            ViewData["PedidoId"] = new SelectList(pedidosParaDropdown, "Id", "Texto");

            return View(banner);
        }

        // POST: Banners/Vincular/5 (Executa a ação de salvar a união no banco)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Vincular(int id, int PedidoId)
        {
            // Acha o banner pelo ID
            var banner = await _context.Banners.FindAsync(id);

            // Acha o pedido pelo ID e já traz a lista de itens dele junto
            var pedido = await _context.Pedidos
                .Include(p => p.Itens)
                .FirstOrDefaultAsync(p => p.Id == PedidoId);

            if (banner != null && pedido != null)
            {
              
              
                // adiciona o objeto na lista
                pedido.Itens.Add(banner);
                await _context.SaveChangesAsync();

                // Volta para a tela de Pedidos para o usuário ver que deu certo
                return RedirectToAction("Index", "Pedidos");
            }

            return NotFound();
        }
    }
}
