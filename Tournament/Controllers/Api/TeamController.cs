using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using Tournament.Models;
using Tournament.Models.Dto;
using Tournament.Models.Helpers;

namespace Tournament.Controllers.Api
{
    public class TeamController : ApiController
    {
        private ApplicationDbContext _context;
        private TeamFactory _factory;

        public TeamController()
        {
            _context = new ApplicationDbContext();
            _factory = new TeamFactory();
            //MappingProfile.Initialize();
        }

        // GET /api/customers
        public IHttpActionResult Get()
        {
            var teamDtos = _factory.MapFromTeamsToDtos(_context.Teams.ToList());

            return Ok(teamDtos);
        }

        // GET /api/customers/1
        public IHttpActionResult Get(int id)
        {
            var team = _context.Teams.SingleOrDefault(c => c.Id == id);

            if (team == null)
                return NotFound();


            var teamDto = _factory.FromTeamToDto(team);
            return Ok(teamDto);
        }

        // POST /api/customers
        [HttpPost]
        [Authorize(Roles = RoleHelper.Admin)]
        public IHttpActionResult Create(TeamBindingModel teamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var team = _factory.CreateNewTeam(teamDto);
            _context.Teams.Add(team);
            _context.SaveChanges();

            var dto = _factory.FromTeamToDto(team);
            return Created(new Uri(Request.RequestUri + "/" + dto.Id), dto);
        }

        // PUT /api/customers/1
        [HttpPut]
        [Authorize(Roles = RoleHelper.Admin)]
        public IHttpActionResult Update(int id, TeamDto teamDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var teamInDb = _context.Teams.SingleOrDefault(c => c.Id == id);

            if (teamInDb == null)
                return NotFound();

            _factory.MapFromDtoToTeam(teamDto, teamInDb);

            _context.SaveChanges();

            return Ok(teamDto);
        }

        // DELETE /api/customers/1
        [HttpDelete]
        [Authorize(Roles = RoleHelper.Admin)]
        public IHttpActionResult Delete(int id)
        {
            var teamInDb = _context.Teams.Include(t=>t.Tournaments).SingleOrDefault(c => c.Id == id);

            if (teamInDb == null)
                return NotFound();

            if (teamInDb.Tournaments.Count == 0)
            {
                _context.Teams.Remove(teamInDb);
            }
            else
            {
                teamInDb.Disband();
            }
            _context.SaveChanges();

            return Ok();
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
