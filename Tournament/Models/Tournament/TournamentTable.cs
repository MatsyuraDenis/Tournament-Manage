using System.Collections.Generic;
using System.Linq;
using Tournament.Models;

namespace Tournament.Models
{
    public class TournamentTable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private ApplicationDbContext _context;
        public List<Team> Teams { get; set; }
        public List<Game> Games { get; set; }
        public List<TeamStatistic> Statistics { get; set; }

        public TournamentTable() { }

        public TournamentTable(List<Team> teams, string name)
        {
            Teams = teams;
            Games = new List<Game>();
            Name = name;
            _context = new ApplicationDbContext();
            Statistics = new List<TeamStatistic>();
            foreach (var team in Teams)
            {
                Statistics.Add(new TeamStatistic(team));
            }
        }

        public bool IsHaveTeam(Team team)
        {
            if (Teams.Contains(team))
                return true;

            return false;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/1293058/round-robin-tournamentTable-algorithm-in-c-sharp
        /// </summary>
        public void GenerateRounds()
        {
            int numDays = (Teams.Count - 1);
            int halfSize = Teams.Count / 2;

            List<Team> teams = new List<Team>();

            teams.AddRange(Teams.Skip(halfSize).Take(halfSize));
            teams.AddRange(Teams.Skip(1).Take(halfSize - 1).ToArray().Reverse());

            int teamsSize = teams.Count;

            for (int day = 0; day < numDays; day++)
            {
                int teamIdx = day % teamsSize;
                int round = day;
                round++;
                var Game = new Game(teams[teamIdx], Teams[0], round, Name);
                Games.Add(Game); for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;
                    Game game = new Game(teams[firstTeam], teams[secondTeam], round, Name);

                    Games.Add(game);

                    //Console.WriteLine("{0} vs {1}", teams[firstTeam], teams[secondTeam]);
                }
            }
        }

        public void Update(Game game)
        {
            bool isNotEdit = game.IsForEdit;
            foreach (var statistic in Statistics)
            {

                if (statistic.TeamId == game.Teams[0].Id)
                {
                    statistic.Update(game, true, isNotEdit);
                }

                if (statistic.TeamId == game.Teams[1].Id)
                {
                    statistic.Update(game, false, isNotEdit);
                }
            }
        }
    }
}