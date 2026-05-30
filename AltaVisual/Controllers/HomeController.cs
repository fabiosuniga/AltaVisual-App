using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necessário para o CountAsync
using AltaVisual.Data; // Nome do seu namespace de dados
using AltaVisual.Models;
using System.Diagnostics;

namespace AltaVisual.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        // O construtor abaixo é o que permite o Controller acessar o Banco de Dados
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Busca os números reais do seu banco de dados
            // Se as tabelas tiverem nomes diferentes, ajuste aqui (ex: _context.SuaTabela.CountAsync)
            ViewBag.TotalClientes = await _context.Clientes.CountAsync();
            ViewBag.TotalPedidos = await _context.Pedidos.CountAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}