using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tournament.Models
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        /// <summary>
        /// This field needs to check for existing of this team,
        /// to load it for the potential future competition
        /// /// </summary>
        [Required]
        public bool IsExist { get; protected set; }
        public bool isSelected;
        public List<Game> Games { get; set; }
        public List<TournamentTable> Tournaments { get; set; }

        public Team()
        {
            IsExist = true;
        }

        public Team(string name) : this()
        {
            Name = name;
        }

        public void Disband()
        {
            IsExist = false;
        }
    }
}