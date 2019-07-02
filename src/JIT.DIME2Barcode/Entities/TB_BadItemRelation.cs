using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public partial class TB_BadItemRelation: Entity<int>
    {
        [NotMapped]
        public override int Id { get; set; }
        [Key]
        public int FID { get; set; }
        public int? FItemID { get; set; }
        public int FOperID { get; set; }
        public string FNumber { get; set; }
        public string FName { get; set; }

        [DefaultValue(false)]//设置默认值
        public bool FDeleted { get; set; }
        public string FRemark { get; set; }

        // public string FItemName { get; set; }//产品名称
        [ForeignKey("FOperID")]
        public t_SubMessage Operate { get; set; }

    }
}