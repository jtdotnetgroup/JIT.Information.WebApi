using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JIT.DIME2Barcode.Entities
{
    public partial class BillStatus:Entity<int>
    {
        [NotMapped]
        public override int Id { get; set; }
        [Key]
        public int FTranType { get; set; }
        public string FTranName { get; set; }
        public int FStatus { get; set; }
        public string FName { get; set; }
        public string FNote { get; set; }
    }
}