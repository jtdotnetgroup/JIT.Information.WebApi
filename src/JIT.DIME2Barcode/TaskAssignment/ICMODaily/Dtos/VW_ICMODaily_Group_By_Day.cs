﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode.TaskAssignment.ICMODaily.Dtos
{
    public class VW_ICMODaily_Group_By_Day:Entity<String>
    {
        [Key]
        public override string Id { get; set; }

        public DateTime FDate { get; set; }
        public string FMOBillNo { get; set; }
        public string DisplayName { get; set; }
        public string FItemID { get; set; }

        public string FItemName { get; set; }

        public string FItemModel { get; set; }

        public decimal TotalPlanAuxQty { get; set; }
    }
}