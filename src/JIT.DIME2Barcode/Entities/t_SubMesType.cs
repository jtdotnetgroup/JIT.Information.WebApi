using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public class t_SubMesType:Entity<int>
    {

        [NotMapped]
        public override int Id { get; set; }

           [Key]
           [DataType("Int32")]
           public int  FTypeID { get; set; }
           public string FName { get; set; }  
           public int FDetail { get; set; }
           public int FType { get; set; }
           public int FGRType { get; set; }
           public byte[] FModifyTime { get; set; }
           public string UUID { get; set; }
    }

    //此类用于同步
    public class t_SubMesType_Sync : t_SubMesType
    {

    }
}
