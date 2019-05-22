using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;

namespace JIT.JIT.TaskAssignment.ICMaterialPicking.Dtos
{
    /// 1.Input结尾为条件，Dto为对象实体
    ///
    /// 
    /// <summary>
    /// 实体类
    /// </summary>
    public partial class ICMaterialPicking : Entity<string>
    {
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public override string Id { get => FID; set => FID = value; }
        [StringLength(100)]
        public string FID { get; set; }
        [Required]
        [StringLength(100)]
        public string FSrcID { get; set; }
        public int FEntryID { get; set; }
        public int? FItemID { get; set; }
        public int? FUnitID { get; set; }
        [StringLength(200)]
        public string FBatchNo { get; set; }
        [Column(TypeName = "decimal(28, 8)")]
        public decimal? FAuxQty { get; set; }
        [StringLength(100)]
        public string FBiller { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FDate { get; set; }
        [StringLength(255)]
        public string FNote { get; set; }
    }
    /// <summary>
    /// 操作实体类
    /// </summary>
    public class ICMaterialPickingDto : IEntityDto<string>
    {
        public string Id { get => FID; set => FID = value; }
        [StringLength(100)]
        public string FID { get; set; }
        [Required]
        [StringLength(100)]
        public string FSrcID { get; set; }
        public int FEntryID { get; set; }
        public int? FItemID { get; set; }
        public int? FUnitID { get; set; }
        [StringLength(200)]
        public string FBatchNo { get; set; }
        public decimal? FAuxQty { get; set; }
        [StringLength(100)]
        public string FBiller { get; set; }
        public DateTime? FDate { get; set; }
        [StringLength(255)]
        public string FNote { get; set; }
    }
    /// <summary>
    /// 查询全部
    /// </summary>
    public class ICMaterialPickingGetAllInput : PagedResultRequestDto
    {

    }
    /// <summary>
    /// 创建传进来的参数
    /// </summary>
    public class ICMaterialPickingCreateDto : IEntityDto<string>
    {
        public string Id { get => FID; set => FID = value; }
        [StringLength(100)]
        public string FID { get; set; }
        [Required]
        [StringLength(100)]
        public string FSrcID { get; set; }
        public int FEntryID { get; set; }
        public int? FItemID { get; set; }
        public int? FUnitID { get; set; }
        [StringLength(200)]
        public string FBatchNo { get; set; }
        public decimal? FAuxQty { get; set; }
        [StringLength(100)]
        public string FBiller { get; set; }
        public DateTime? FDate { get; set; }
        [StringLength(255)]
        public string FNote { get; set; }

    }
    /// <summary>
    /// 修改传进来的参数
    /// </summary>
    public class ICMaterialPickingUpdateDto : IEntityDto<string>
    {
        public string Id { get => FID; set => FID = value; }
        [StringLength(100)]
        public string FID { get; set; }
        [Required]
        [StringLength(100)]
        public string FSrcID { get; set; }
        public int FEntryID { get; set; }
        public int? FItemID { get; set; }
        public int? FUnitID { get; set; }
        [StringLength(200)]
        public string FBatchNo { get; set; }
        public decimal? FAuxQty { get; set; }
        [StringLength(100)]
        public string FBiller { get; set; }
        public DateTime? FDate { get; set; }
        [StringLength(255)]
        public string FNote { get; set; }

    }
    /// <summary>
    /// 查询明细
    /// </summary>
    public class ICMaterialPickingGetDto : IEntityDto<string>
    {
        public string Id { get => FID; set => FID = value; }
        [StringLength(100)]
        public string FID { get; set; }
    }
    /// <summary>
    /// 删除明细
    /// </summary>
    public class ICMaterialPickingDeleteDto : IEntityDto<string>
    {
        public string Id { get => FID; set => FID = value; }
        [StringLength(100)]
        public string FID { get; set; }
    }
    /// <summary>
    /// 批量删除
    /// </summary>
    public class ICMaterialPickingAllDeleteDto
    {
        public string[] Id { get; set; }
    }
}
