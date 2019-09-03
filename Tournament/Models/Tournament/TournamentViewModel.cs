using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tournament.Models.Tournament
{
    public class TournamentViewModel
    {
        public List<TeamStatistic> TeamStatistics { get; set; }
        public TournamentTable TournamentTable { get; set; }
    }
}