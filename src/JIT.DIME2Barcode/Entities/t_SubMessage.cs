using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{

    public class t_SubMessage : Entity
    {
        [NotMapped]
        public override int Id { get; set; }

        public string FBrNo { get; set; }
        [Key]
        public int FInterID { get; set; }
        public string FID { get; set; }
        public int FParentID { get; set; }
        public string FName { get; set; }
        public int FTypeID { get; set; }
        public int FDetail { get; set; }
        public int FDeleted { get; set; }
        public byte[] FModifyTime { get; set; }
        public int FSystemType { get; set; }
        public string FSpec { get; set; }
        public string UUID { get; set; }
    }

    //此类用于同步
    public class t_SubMessage_Sync : t_SubMessage
    {
        [ForeignKey("FTypeID")]
        public t_SubMesType_Sync SubMessageType { get; set; }
    }
}