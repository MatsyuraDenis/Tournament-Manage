using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tournament.Models.Dto;

namespace Tournament.Models.GameModels
{
    public class GameFactory
    {
        public ReturnGameBindingModel CreateDetails(Game game)
        {
            ReturnGameBindingModel model = new ReturnGameBindingModel
            {
                Id = game.Id,
                Round = game.Round,
                TournamentName = game.TournamentName,

                LeftTeamName = game.GetLeftTeam().Name,
                RightTeamName = game.GetRightTeam().Name,

                LeftTeamId = game.GetLeftTeam().Id,
                RightTeamId = game.GetRightTeam().Id,

                LeftTeamScore = game.TeamAScore,
                RightTeamScore = game.TeamBScore
            };

            return model;
        }

        public GameDto CreateDto(Game game)
        {
            GameDto gameDto = new GameDto();
            gameDto.SetGame(game);

            return gameDto;
        }

    }
}