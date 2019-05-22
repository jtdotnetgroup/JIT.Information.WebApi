using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.TaskAssignment.T_PrintTemplate.Dtos
{
    public partial class T_PrintTemplate : Entity<string>
    {
        [NotMapped]
        public override string Id { get => FInterID; set => FInterID = value; }
        [StringLength(200)]
        public string FInterID { get; set; }
        public int? FUserID { get; set; }
        public int FTranType { get; set; }
        [Required]
        [StringLength(100)]
        public string FType { get; set; }
        public int FItemID { get; set; }
        [StringLength(200)]
        public string FFileDir { get; set; }
        [Column(TypeName = "image")]
        public byte[] FFile { get; set; }
        [StringLength(200)]
        public string FNote { get; set; }
    }
    public class T_PrintTemplateDto : IEntityDto<string>
    {
        public string Id { get => FInterID; set => FInterID = value; }
        [StringLength(200)]
        public string FInterID { get; set; }
        public int? FUserID { get; set; }
        public int FTranType { get; set; }
        [Required]
        [StringLength(100)]
        public string FType { get; set; }
        public int FItemID { get; set; }
        [StringLength(200)]
        public string FFileDir { get; set; }
        public byte[] FFile { get; set; }
        [StringLength(200)]
        public string FNote { get; set; }
    }
    public class T_PrintTemplateInput : IEntityDto<string>
    {
        public string Id { get => FInterID; set => FInterID = value; }
        public string FInterID { get; set; }
        public int? FUserID { get; set; }
        public int FTranType { get; set; }
        public string FType { get; set; }
        public int FItemID { get; set; }
        public string FFileDir { get; set; }
        public byte[] FFile { get; set; }
        public string FNote { get; set; }
    }
    public class T_PrintTemplateGetAllInput : PagedResultRequestDto
    {

    }
    public class T_PrintTemplateGetAllOutput : PagedResultDto<List<T_PrintTemplateDto>>
    {

    }
    public class T_PrintTemplateCreateDto : IEntityDto<string>
    {
        public string Id { get => FInterID; set => FInterID = value; }
        [StringLength(200)]
        public string FInterID { get; set; }
        public int? FUserID { get; set; }
        public int FTranType { get; set; }
        [Required]
        [StringLength(100)]
        public string FType { get; set; }
        public int FItemID { get; set; }
        [StringLength(200)]
        public string FFileDir { get; set; }
        public byte[] FFile { get; set; }
        [StringLength(200)]
        public string FNote { get; set; }

    }
}
