using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tournament.Models.Dto;

namespace Tournament.Models
{
    public class TeamFactory
    {
        /// <summary>
        /// MapFromTeamTpDto new team based on dto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Team CreateNewTeam(TeamBindingModel dto)
        {
            Team team = new Team(dto.Name);
            return team;
        }
        
        public void MapFromDtoToTeam(TeamDto dto,Team team)
        {
            team.Name = dto.Name;
        }

        public TeamDto FromTeamToDto(Team team)
        {
            TeamDto dto = SetTeamToDto(team);
            return dto;
        }

        public List<TeamDto> MapFromTeamsToDtos(List<Team> teams)
        {
            List<TeamDto> dtos = new List<TeamDto>();
            foreach (var team in teams)
            {
                dtos.Add(FromTeamToDto(team));
            }

            return dtos;
        }

        private TeamDto SetTeamToDto(Team team)
        {
            TeamDto dto = new TeamDto();
            dto.Id = team.Id;
            dto.Name = team.Name;
            return dto;
        }
    }
}