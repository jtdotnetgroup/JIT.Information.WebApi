using System; 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
using Abp.Domain.Entities;
using JetBrains.Annotations;

namespace JIT.DIME2Barcode.Entities
{
    /// <summary>
    /// 基础资料信息表
    /// </summary>
    public class Sys_BasicInfo : Entity<int>
    {
        [NotMapped()]
        public override int Id
        {
            get { return BasicInfoId; }
            set { Id = BasicInfoId; }
        }

        [Key] public int BasicInfoId { get; set; }
        public string BICode { get; set; }
        public string BIName { get; set; }
        [CanBeNull] public string BIType { get; set; }
        [CanBeNull] public string BIDescribe { get; set; }
        [CanBeNull] public string BIOrder { get; set; }
        [CanBeNull] public string BIJson { get; set; }
        [CanBeNull] public string BIURL { get; set; }
        [CanBeNull] public string BIState { get; set; }
        public bool? IsDeleted { get; set; }
        public int? ParentId { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateUserId { get; set; }
        [CanBeNull] public string Remark { get; set; }

    }
}
