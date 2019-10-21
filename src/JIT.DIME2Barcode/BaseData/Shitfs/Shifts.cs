using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace JIT.DIME2Barcode
{
    public class Shifts:Entity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StarTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public string Note { get; set; }
    }
}