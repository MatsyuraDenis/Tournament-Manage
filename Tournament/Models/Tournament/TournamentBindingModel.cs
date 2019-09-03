using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tournament.Models.Attributes;

namespace Tournament.Models.Tournament
{
    public class TournamentBindingModel
    {
        [Required]
        [Display(Name = "Название турнира")]
        public string Name { get; set; }

        public List<SelectListItem> Teams { get; set; }

        [Required]
        [TeamPairingValidation]
        public List<string> Selected { get; set; }

    }
}