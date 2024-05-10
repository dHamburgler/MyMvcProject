using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMvcProject.Data;
using MyMvcProject.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace MyMvcProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var parties = await _context.Parties.Include(x => x.Guests).ToListAsync();
            return View(parties);
        }


        public async Task<IActionResult> Join(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // look up party in the db
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
            {
                return NotFound();
            }
            
            // get user from DB
            var user = await _userManager.GetUserAsync(User);
            var guests = party.Guests.ToList();

            // check if the guest already has joined the group
            var exists = _context.Parties.Any(x => x.Id == party.Id && x.Guests.Any(g => g.Id == user.Id));
            if (exists == false)
            {
                party.Guests.Add(user);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Unjoin(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var party = await _context.Parties.Include(x => x.Guests).FirstAsync(x => x.Id == id);
            if (party == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            var guests = party.Guests.ToList();

            party.Guests.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
