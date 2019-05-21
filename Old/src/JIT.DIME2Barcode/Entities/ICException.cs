using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public partial class ICException:Entity<string>
    {
        [NotMapped]
        public override string Id { get; set; }
        [Key]
        public string FID { get; set; }
        public string FSrcID { get; set; }
        public string FBiller { get; set; }
        public DateTime? FTime { get; set; }
        public string FRemark { get; set; }
        public DateTime? FRecoverTime { get; set; }
        public string FNote { get; set; }
    }
}