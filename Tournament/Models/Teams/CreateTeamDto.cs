using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tournament.Models.Dto
{
    public class TeamBindingModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}