using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using JIT.DIME2Barcode.Entities;

namespace JIT.DIME2Barcode.AppService
{
    public class TB_BadItemRelationAppService: ApplicationService
    {

        public IRepository<DIME2Barcode.Entities.TB_BadItemRelation, int> Repository { get; set; }

        /// <summary>
        /// 工序不良项目表
        /// </summary>
        /// <returns></returns>
        public List<TB_BadItemRelation> GetAll()
        {
            return Repository.GetAll().ToList();
        }
    }
}
