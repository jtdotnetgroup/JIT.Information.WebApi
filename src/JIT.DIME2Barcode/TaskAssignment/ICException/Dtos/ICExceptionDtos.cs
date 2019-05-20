using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.TaskAssignment.ICException.Dtos
{
    public partial class ICException : Entity<string>
    {
        [NotMapped]
        public override string Id { get => FID; set => FID = value; }
        [StringLength(100)]
        public string FID { get; set; }
        [Required]
        [StringLength(100)]
        public string FSrcID { get; set; }
        [StringLength(100)]
        public string FBiller { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FTime { get; set; }
        [StringLength(255)]
        public string FRemark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FRecoverTime { get; set; }
        [StringLength(255)]
        public string FNote { get; set; }
    }
    public class ICExceptionDto : IEntityDto<string>
    {
        public string Id { get => FID; set => FID = value; }
        public string FID { get; set; }
        public string FSrcID { get; set; }
        public string FBiller { get; set; }
        public DateTime? FTime { get; set; }
        public string FRemark { get; set; }
        public DateTime? FRecoverTime { get; set; }
        public string FNote { get; set; }
    }
    public class ICExceptionInput  
    { 
        public string FID { get; set; }
        public string FSrcID { get; set; }
    }
    public class ICExceptionGetAllInput : PagedResultRequestDto
    {

    }
    public class ICExceptionGetAllOutput : PagedResultDto<List<ICExceptionDto>>
    {

    }
    public class ICExceptionCreateDto : IEntityDto<string>
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
