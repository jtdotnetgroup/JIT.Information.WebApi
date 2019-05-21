using System.ComponentModel.DataAnnotations;

namespace JIT.InformationSystem.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}