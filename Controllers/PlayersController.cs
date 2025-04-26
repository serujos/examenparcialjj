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
            // Cargar todos los equipos disponibles para seleccionarlos en el formulario
            ViewBag.Teams = _context.Teams.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Player player)
        {
            if (ModelState.IsValid)
            {
                // Agregar el jugador a la base de datos
                _context.Players.Add(player);
                _context.SaveChanges();
                
                // Verificar si el jugador se guarda correctamente
                var playerInDb = _context.Players.FirstOrDefault(p => p.Id == player.Id);
                if (playerInDb != null)
                {
                    // Imprimir en la consola del servidor el jugador guardado
                    Console.WriteLine($"Jugador guardado: {playerInDb.Nombre}");
                }
                else
                {
                    // Imprimir en caso de que no se haya guardado correctamente
                    Console.WriteLine("Error al guardar el jugador.");
                }

                // Redirigir a la lista de jugadores después de guardar
                return RedirectToAction("List");
            }

            // Si el modelo no es válido, recargar los equipos en la vista
            ViewBag.Teams = _context.Teams.ToList();
            return View(player);
        }

        public IActionResult Assign(int? id)
        {
            if (id == null) return NotFound();

            var player = _context.Players.FirstOrDefault(p => p.Id == id);
            if (player == null) return NotFound();

            ViewBag.Teams = _context.Teams.ToList();
            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Assign(int id, int teamId)
        {
            var player = _context.Players.FirstOrDefault(p => p.Id == id);
            if (player == null) return NotFound();

            var exists = _context.Assignments.Any(a => a.PlayerId == id && a.TeamId == teamId);
            if (!exists)
            {
                var assignment = new Assignment
                {
                    PlayerId = id,
                    TeamId = teamId
                };

                _context.Assignments.Add(assignment);
                _context.SaveChanges();
            }

            return RedirectToAction("ListWithTeams");
        }

        public IActionResult List()
        {
            var playersWithTeams = _context.Players
                .Include(p => p.Team) // Incluye el equipo relacionado
                .ToList();

            Console.WriteLine($"Número de jugadores: {playersWithTeams.Count}");

            return View(playersWithTeams);
        }
    }
}
