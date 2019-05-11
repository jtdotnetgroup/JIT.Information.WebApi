using System.ComponentModel.DataAnnotations;

namespace JIT.DIME2Barcode.TaskAssignment.TaskScheduling.Dtos
{
    public class TaskScheduleDetailInput
    {
        [Required]
        public int FMOInterID { get; set; }
    }
}