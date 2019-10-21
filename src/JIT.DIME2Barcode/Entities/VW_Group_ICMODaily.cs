﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.Entities
{
    public class VW_Group_ICMODaily:Entity<string>
    {
        [NotMapped]
        public override string Id { get; set; }

        [Key]
        public string FID { get; set; }
        public DateTime? 日期 { get; set; }
        public string 机台 { get; set; }
        public string 班次 { get; set; }
        public string 操作员 { get; set; }
        public decimal 计划数量 { get; set; }
        public decimal? 派工数量 { get; set; }
        public decimal? 完成数量 { get; set; }
        public decimal? 合格数量 { get; set; }
        public string FMOBillNo { get; set; }
        public int? FMOInterID { get; set; }
    }
}