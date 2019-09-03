using System.Collections.Generic;
using Tournament.Models;

namespace Tournament.Models
{
    public class Game
    {
        public int Id { get; set; }

        /// <summary>
        /// 2 teams, that played this game; Teams[0] is leftTeam, Teams[1] is right team
        /// will changed in future
        /// </summary>
        public virtual List<Team> Teams { get; set; }
        public int? TournamentTableId { get; set; }

        public List<TeamStatistic> TeamStatistics { get; set; }
        /// <summary>
        /// In wich tournamentTable game played
        /// </summary>
        public TournamentTable TournamentTable { get; set; }
        /// <summary>
        /// Score of left team
        /// </summary>
        public int? TeamAScore { get; set; }
        /// <summary>
        /// Score of right team
        /// </summary>
        public int? TeamBScore { get; set; }
        /// <summary>
        /// This field need to check is game played first time to only update database,
        /// or game should be edit(delete this game than update) 
        /// </summary>
        public bool IsForEdit { get; protected set; }

        public int? Round { get; set; }
        public string TournamentName { get; set; }

        private ApplicationDbContext _context;

        public Game()
        {
            _context = new ApplicationDbContext();
        }

        public Game(Team teamA, Team teamB)
        {
            Teams = new List<Team>();
            _context = new ApplicationDbContext();
            Teams.Add(teamA);
            Teams.Add(teamB);
        }

        public Game(Team teamA, Team teamB, int round, string tournamentName)
        {
            Teams = new List<Team>();
            _context = new ApplicationDbContext();
            Teams.Add(teamA);
            Teams.Add(teamB);
            Round = round;
            TournamentName = tournamentName;
        }

        public bool TeamAWin()
        {
            if (TeamAScore > TeamBScore)
                return true;

            return false;
        }

        public bool TeamBWin()
        {
            if (TeamAScore < TeamBScore)
                return true;

            return false;
        }

        public bool Draw()
        {
            if (TeamAScore == TeamBScore)
                return true;

            return false;
        }

        public Team GetLeftTeam()
        {
            return Teams[0];
        }

        public Team GetRightTeam()
        {
            return Teams[1];
        }

        public string GetLeftTeamName()
        {
            return Teams[0].Name;
        }

        public string GetRightTeamName()
        {
            return Teams[1].Name;
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetScore(int? teamA, int? teamB, TournamentTable tournamentTable)
        {
            if (TeamAScore.HasValue && TeamBScore.HasValue)
                IsForEdit = true;
            else
                IsForEdit = false;
            TeamAScore = teamA;
            TeamBScore = teamB;

            tournamentTable.Update(this);
        }
    }
}