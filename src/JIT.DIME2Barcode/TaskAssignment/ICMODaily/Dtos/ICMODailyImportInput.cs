using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class ICMODailyImportInput
    {
        [Required]
        public IFormFile File { get; set; }
    }
}