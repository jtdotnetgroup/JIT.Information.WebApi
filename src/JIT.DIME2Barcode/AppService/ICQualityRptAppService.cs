using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using Abp.Domain.Repositories;

namespace JIT.DIME2Barcode.AppService
{
    public  class ICQualityRptAppService: ApplicationService
    {
        public IRepository<DIME2Barcode.Entities.ICQualityRpt, string> Repository { get; set; }
        
    }
}
