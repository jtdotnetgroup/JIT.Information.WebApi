using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Organizations;
using CommonTools;

namespace JIT.DIME2Barcode.SystemSetting.Organization.Dtos
{
    public class OrganizationCreateInput:EntityDto<int>
    {
        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        public virtual int TenantId { get; set; }

        /// <summary>
        /// Parent <see cref="OrganizationUnit"/> Id.
        /// Null, if this OU is root.
        /// </summary>
        public virtual long ParentId { get; set; }

        /// <summary>
        /// Hierarchical Code of this organization unit.
        /// Example: "00001.00042.00005".
        /// This is a unique code for a Tenant.
        /// It's changeable if OU hierarch is changed.
        /// </summary>
        [Required]
        public virtual string Code { get; set; }

        /// <summary>
        /// Display name of this role.
        /// </summary>
        [Required]
        public virtual string DisplayName { get; set; }

        public  virtual  bool FWorkshopType { get; set; }

        public DateTime CreationTime { get; set; }

        public int  CreatorUserId { get; set; }

        public virtual PublicEnum.OrganizationType OrganizationType { get; set; }
        public virtual string DataBaseConnection { get; set; }
        public virtual int? ERPOrganizationLeader { get; set; }
        public virtual int? ERPOrganization { get; set; }
        public virtual string Remark { get; set; }
    }
}