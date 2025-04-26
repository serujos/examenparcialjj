using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using examenparcialjj.Data;
using examenparcialjj.Models;

namespace examenparcialjj.Controllers
{
    public class PlayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Players.Add(player);
                _context.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.Teams = _context.Teams.ToList();
            return View(player);
        }
    }
}
