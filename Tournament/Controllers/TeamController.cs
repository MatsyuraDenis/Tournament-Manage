using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using Tournament.Models;
using Tournament.Models.Dto;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Tournament.Models.Helpers;
using System.Net;

namespace Tournament.Controllers
{
    public class TeamController : Controller
    {
        private TeamFactory _factory;
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public TeamController()
        {
            _factory = new TeamFactory();
            _context = new ApplicationDbContext();
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_context));
        }

        // GET: Team
        public async Task<ActionResult> Index()
        {
            if (User.IsInRole(RoleHelper.Admin))
            {
                    return View("AdminIndex");
            }

            return View();
        }

        // GET: Team/Details/5
        public ActionResult Details(int? id)
        {
            var team = _context.Teams.SingleOrDefault(t => t.Id == id);
            if (team == null)
                return HttpNotFound();

            return View(team);
        }

        // GET: Team/MapFromTeamTpDto
        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult CreateNew(TeamBindingModel team)
        {

            if (!ModelState.IsValid)
            {
                return View("TeamForm", team);
            }

            var newTeam = _factory.CreateNewTeam(team);
            _context.Teams.Add(newTeam);


            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Team/Edit/5
        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult Edit(int? id)
        {
            var team = _context.Teams.SingleOrDefault(t => t.Id == id);

            if (team == null)
                return HttpNotFound();

            TeamDto dto = _factory.FromTeamToDto(team);

            return View("TeamForm", dto);
        }

        // GET: Team/Delete/5
        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var team = _context.Teams.SingleOrDefault(t => t.Id == id);

            if (team == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

            _context.SaveChanges();
            return View("Delete", team);
        }

        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult DeleteConfirmed(int id)
        {
            var team = _context.Teams.Include(t => t.Tournaments).SingleOrDefault(t => t.Id == id);

            if (team.Tournaments.Count == 0)
            {
                _context.Teams.Remove(team);
            }
            else
            {
                team.Disband();
            }

            _context.SaveChanges();
            return View("AdminIndex", _context.Teams.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleHelper.Admin)]
        public ActionResult Save(TeamDto team)
        {
            if (!ModelState.IsValid)
            {
                return View("TeamForm", team);
            }

            var dbTeam = _context.Teams.SingleOrDefault(t => t.Id == team.Id);

            if (dbTeam == null)
                return HttpNotFound();

            _factory.MapFromDtoToTeam(team,dbTeam);


            _context.SaveChanges();
            return RedirectToAction("Index");
        }




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
                if(_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}
