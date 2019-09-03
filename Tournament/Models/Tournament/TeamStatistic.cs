using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Tournament.Models;

namespace Tournament.Models
{
    public class TeamStatistic
    {
        public int Id { get; set; }
        public int TournamentTableId { get; set; }
        [Required]
        public TournamentTable TournamentTable { get; set; }
        public Team Team { get; set; }
        [Required]
        public string TeamName { get; set; }
        public int TeamId { get; set; }
        public List<Game> Games { get; set; } 

        public int Win { get; set; }
        public int Draw { get; set; } 
        public int Looses { get; set; }

        public int Scored { get; set; } 
        public int Missed { get; set; }

        public int Points { get; set; }

        public TeamStatistic() { Games = new List<Game>();}

        public TeamStatistic(Team team)
        {
            Team = team;
            Games = new List<Game>();
            TeamName = Team.Name;
        }


        /// <summary>
        /// game1 load because entity give error when i try to use hashtable or list of games
        /// </summary>
        /// <param name="game"></param>
        /// <param name="isTeamA"></param>
        /// <param name="isEdit"></param>
        public void Update(Game game, bool isTeamA, bool isEdit)
        {

            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var game1 = _context.Games.Include(t => t.Teams).SingleOrDefault(g => g.Id == game.Id);
                if (isTeamA)
                {
                    if (!isEdit)
                    {
                        if (game.Teams[0].Id == TeamId)
                            UpdateTeamA(game);

                        Games.Add(game);
                    }
                    else
                    {
                        RemoveGameA(game1);
                        UpdateTeamA(game);
                    }
                }
                else
                {
                    if (!isEdit)
                    {
                        if (game.Teams[1].Id == TeamId)
                            UpdateTeamB(game);

                        Games.Add(game);

                    }
                    else
                    {
                        RemoveGameB(game1);
                        UpdateTeamB(game);
                    }
                }
            }

            UpdatePoints();
        }

        private void UpdatePoints()
        {
            Points = Draw + Win * 3;
        }

        public void UpdateTeamA(Game game)
        {
            if (game.TeamAWin())
            {
                Win++;
            }else if (game.TeamBWin())
            {
                Looses++;
            }
            else
            {
                Draw++;
            }

            Scored += (int)game.TeamAScore;
            Missed += (int)game.TeamBScore;
        }

        public void UpdateTeamB(Game game)
        {
            if (game.TeamBWin())
            {
                Win++;
            }
            else if (game.TeamAWin())
            {
                Looses++;
            }
            else
            {
                Draw++;
            }

            Missed += (int)game.TeamAScore;
            Scored += (int)game.TeamBScore;
        }

        public void RemoveGameA(Game game)
        {
            if (game.TeamAWin())
            {
                Win--;
            }
            else if(game.TeamBWin())
            {
                Looses--;
            }
            else
            {
                Draw--;
            }

            Scored -= (int)game.TeamAScore;
            Missed -= (int)game.TeamBScore;
        }

        public void RemoveGameB(Game game)
        {
            if (game.TeamAWin())
            {
                Looses--;
            }
            else if (game.TeamBWin())
            {
                Win--;
            }
            else
            {
                Draw--;
            }

            Scored -= (int)game.TeamBScore;
            Missed -= (int)game.TeamAScore;
        }
    }
}