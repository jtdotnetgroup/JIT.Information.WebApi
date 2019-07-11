using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace JIT.DIME2Barcode.Model
{
    public partial class JITPagedResultRequestDto:PagedAndSortedResultRequestDto
    {
        private string where;

        public  string Where
        {
            get { return string.IsNullOrEmpty(@where) ? "1=1" : @where; }

            set { this.@where = value; }
        }
    }
}
