using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class ICMODailyGetAllListInput
    {
        [Required]
        public int FMOInterID { get; set; }
    }
}