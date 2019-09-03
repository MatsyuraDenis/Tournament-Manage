using System.ComponentModel.DataAnnotations;
using Tournament.Models.ValAtributes;

namespace Tournament.Models.Dto
{
    public class GameDto
    {
        public int Id { get; set; }
        public string LeftTeamName { get; set; }
        public string RightTeamName { get; set; }

        public int LeftTeamId { get; set; }
        public int RightTeamId { get; set; }

        [Required]
        [HaveValidScoreLeft]
        public int? LeftTeamScore { get; set; }

        [Required]
        [HaveValidScoreRight]
        public int? RightTeamScore { get; set; }

        

        public void SetGame(Game game)
        {
            Id = game.Id;
            LeftTeamName = game.Teams[0].Name;
            RightTeamName = game.Teams[1].Name;

            LeftTeamId = game.Teams[0].Id;
            RightTeamId = game.Teams[0].Id;

            LeftTeamScore = game.TeamAScore;
            RightTeamScore = game.TeamBScore;
        }
    }
}