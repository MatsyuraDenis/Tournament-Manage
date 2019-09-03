using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Tournament.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Net;
using Tournament.Models.Tournament;
using System.Collections.Generic;
using System;
using Tournament.Models.Helpers;

namespace Tournament.Controllers
{
    public class TournamentController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;



        public TournamentController()
        {
            _context = new ApplicationDbContext();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        }
        // GET: TournamentTable
        public async Task<ActionResult> Index()
        {
            ViewBag.Title = "Info";
            var tournaments = _context.Tournaments.Include(t=>t.Games).ToList();

            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var user = await _userManager.FindByNameAsync(userName);
                if(user != null)
                {
                    var isAdmin = await _userManager.IsInRoleAsync(user.Id, RoleHelper.Admin);

                    if (isAdmin)
                        return View("AdminIndex", tournaments);
                }
                
            }
            return View(tournaments);
        }

        // GET: TournamentTable/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var tournament = _context.Tournaments
                .Include(t => t.Teams)
                .Include(t=>t.Games)
                .SingleOrDefault(t => t.Id == id);

            var teamStatistics = _context.Statistics.Where(s => s.TournamentTableId == tournament.Id).OrderByDescending(s=>s.Points).ToList();

            if (tournament == null)
                return HttpNotFound();

            if (teamStatistics == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var viewModel = new TournamentViewModel
            {
                TournamentTable = tournament,
                TeamStatistics = teamStatistics
            };

            return View(viewModel);
        }

        // GET: TournamentTable/MapFromTeamTpDto
        [HttpGet]
        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult Create()
        {
            TournamentBindingModel model = GetReadyModel();        
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = RoleHelper.Admin)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TournamentBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                TournamentBindingModel model1 = GetReadyModel(model);
                return View(model1);
            }
                
            List<Team> teams = new List<Team>();
            Team team;
            foreach(var teamId in model.Selected)
            {
                int id = int.Parse(teamId);
                team = _context.Teams.SingleOrDefault(t=>t.Id == id);
                teams.Add(team);
            }
            var tournament = new TournamentTable(teams, model.Name);
            tournament.GenerateRounds();

            _context.Tournaments.Add(tournament);
            _context.SaveChanges();
            return RedirectToAction("Edit", tournament);
        }

        [HttpGet]
        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var tournament = _context.Tournaments.SingleOrDefault(t=>t.Id == id);

            if (tournament == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            return View("Delete", tournament);
        }

        // GET: TournamentTable/Delete/5
        [Authorize(Roles = RoleHelper.Admin)]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tournament = _context.Tournaments.SingleOrDefault(t => t.Id == id);
            var games = _context.Games.Where(g => g.TournamentTableId == tournament.Id);
            var teamStatistic = _context.Statistics.Where(t => t.TournamentTableId == id);

            _context.Statistics.RemoveRange(teamStatistic);
            _context.Games.RemoveRange(games);
            _context.Tournaments.Remove(tournament);
            _context.SaveChanges();
            return View("AdminIndex", _context.Tournaments.ToList());
        }


        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var tournament = _context.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Statistics)
                .Include(t => t.Games)
                .SingleOrDefault(t => t.Id == id);

            if (tournament == null)
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            tournament.Statistics = tournament.Statistics.OrderByDescending(s => s.Points).ToList();



            return View("Edit", tournament);
        
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
            base.Dispose(disposing);
        }




        private List<SelectListItem> LoadAllTeams()
        {
            var teams = _context.Teams.Where(t => t.IsExist).ToList();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var team in teams)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = team.Name,
                    Value = team.Id.ToString(),
                    Selected = team.isSelected
                };
                items.Add(item);
            }

            return items;
        }

        private TournamentBindingModel GetReadyModel()
        {
            List<SelectListItem> items = LoadAllTeams();
            TournamentBindingModel model = new TournamentBindingModel();
            model.Teams = items;

            return model;
        }

        private TournamentBindingModel GetReadyModel(TournamentBindingModel m)
        {
            TournamentBindingModel model = GetReadyModel();
            model.Name = m.Name;

            return model;
        }
    }
}
