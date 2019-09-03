using System.ComponentModel.DataAnnotations;
using Tournament.Models.Dto;

namespace Tournament.Models.ValAtributes
{
    public class HaveValidScoreLeft : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var game = (GameDto) context.ObjectInstance;

            if (game.LeftTeamScore < 0)
            {
                //return new ValidationResult("Score cant be less than 0");
                return new ValidationResult("Счёт не может быть меньше 0");
            }

            return ValidationResult.Success;
        }
    }

    public class HaveValidScoreRight : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var game = (GameDto)context.ObjectInstance;

            if (game.RightTeamScore < 0)
            {
                //return new ValidationResult("Score cant be less than 0");
                return new ValidationResult("Счёт не может быть меньше 0");
            }

            return ValidationResult.Success;
        }
    }
}