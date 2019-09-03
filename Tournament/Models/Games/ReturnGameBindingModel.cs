using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournament.Models.GameModels
{
    public class ReturnGameBindingModel
    {
        public int Id { get; set; }
        public int? Round { get; set; }
        public string TournamentName { get; set; }

        public string LeftTeamName { get; set; }
        public string RightTeamName { get; set; }


        // Need for redirecting to team details page
        public int LeftTeamId { get; set; }
        public int RightTeamId { get; set; }

        public int? LeftTeamScore { get; set; }
        public int? RightTeamScore { get; set; }

        //TODO: add players who play the game
    }
}