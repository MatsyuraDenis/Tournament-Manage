using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Tournament.Models;
using Tournament.Models.Dto;
using Tournament.Models.GameModels;
using Tournament.Models.Helpers;

namespace Tournament.Controllers
{
    public class GameController : Controller
    {
        private ApplicationDbContext _context;
        private GameFactory _factory;



        public GameController()
        {
            _context = new ApplicationDbContext();
            _factory = new GameFactory();
        }
        // GET: Game
        public async Task<ActionResult> Index()
        {
            var games = await _context.Games.ToListAsync();
            return View(games);
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var game = await _context.Games.SingleOrDefaultAsync(g => g.Id == id);

            if(game == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View(_factory.CreateDetails(game));
        }


        [Authorize(Roles = RoleHelper.Admin)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var game = await _context.Games.Include(t => t.Teams).SingleOrDefaultAsync(g => g.Id == id);

            if (game == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var gameDto = _factory.CreateDto(game);
            return View("GameForm", gameDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult Save(GameDto game)
        {
            if (!ModelState.IsValid)
            {
                return View("GameForm", game);
            }
            var dbGame = _context.Games.Include(g => g.Teams).SingleOrDefault(g => g.Id == game.Id);

            if (dbGame == null)
                return HttpNotFound();

            
            var tournament = _context.Tournaments.Include(s=>s.Statistics).SingleOrDefault(t => t.Id == dbGame.TournamentTableId);
            dbGame.SetScore(game.LeftTeamScore, game.RightTeamScore, tournament);
            //tournamentTable.Update(dbGame);

            _context.SaveChanges();
            return RedirectToAction("Edit", "Tournament", new{ id = tournament.Id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}