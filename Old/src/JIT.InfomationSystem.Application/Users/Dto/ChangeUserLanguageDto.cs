using System.ComponentModel.DataAnnotations;

namespace JIT.InfomationSystem.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}