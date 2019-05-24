using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public partial class OrganizationUnitsJT:Entity<int>
    {
        
        [StringLength(100)]
        [Required]
        public  string Code { get; set; }
        public DateTime? CreationTime { get; set; }
        public   long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime DeletionTime { get; set; }
        [StringLength(100)]
        [Required]
        public string DisplayName { get; set; }
        public   bool? IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public int? LastModifierUserId { get; set; }
        public int? ParentId { get; set; }
        public int? TenantId { get; set; }
        public int? OrganizationType { get; set; }
        public string DataBaseConnection { get; set; }
        public int? ERPOrganizationLeader { get; set; }
        public int? ERPOrganization { get; set; }
        public string Remark { get; set; }

        [ForeignKey("ParentId")]
        public OrganizationUnitsJT Parent { get; set; }

        public List<OrganizationUnitsJT> Children { get; set; }

    }
}
