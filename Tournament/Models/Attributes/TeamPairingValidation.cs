using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Tournament.Models.Tournament;

namespace Tournament.Models.Attributes
{
    public class TeamPairingValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (TournamentBindingModel)validationContext.ObjectInstance;
            if(context.Selected.Count % 2 != 0)
            {
                //return new ValidationResult("Number of teams must be in pairs");
                return new ValidationResult("Количество команд должно быть чётным");
            }
            return ValidationResult.Success;
        }
    }
}