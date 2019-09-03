using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Tournament.Models;

namespace Tournament.Models.Dto
{
    public class TeamDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        //public Team LoadTeamInfo()
        //{
        //    using (var context = new ApplicationDbContext())
        //    {
        //        var team = context.Teams.SingleOrDefault(t=>t.Id == Id);
        //        return team;
        //    }
        //}
    }
}