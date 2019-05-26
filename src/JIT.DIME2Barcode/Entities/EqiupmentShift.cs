using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JIT.DIME2Barcode.Entities
{
    public class EqiupmentShift:Entity
    {
        public int FEqiupmentID { get; set; }
        public int FEmployeeID { get; set; }
        public string FShift { get; set; }


        [ForeignKey("FEqiupmentID")]
        public Equipment Equipment { get; set; }
        [ForeignKey("FEmployeeID")]
        public Employee Employee { get; set; }
    }
}