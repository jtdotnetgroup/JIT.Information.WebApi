using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using CommonTools;
using JIT.DIME2Barcode.TaskAssignment.t_equipment.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 
    /// </summary>
    public class t_equipmentAppService:BaseAppService
    {
        /// <summary>
        /// 设备列表
        /// </summary>
        /// <param name="FName">设备名称</param>
        /// <param name="FWorkCenterID">车间ID,0 为查询全部</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<t_equipmentListDto>> t_equipmentList(t_equipmentListInputDto input)
        {
            return await JIT_t_equipment.GetAll().Join(JIT_t_OrganizationUnit.GetAll(), A => A.FWorkCenterID, B => B.Id,
                    (A, B) => new
                    {
                        A.FInterID,
                        A.FNumber,
                        A.FName,
                        B.DisplayName,
                        A.FType,
                        A.FWorkCenterID
                    })
                .Where(w => w.FType == PublicEnum.EquipmentType.设备 && w.FName.Contains(input.FName) &&
                            (input.FWorkCenterID.Equals(0) || w.FWorkCenterID.Equals(input.FWorkCenterID))).Select(s =>
                    new t_equipmentListDto
                    {
                        FInterID = s.FInterID,
                        FNumber = s.FNumber,
                        FName = s.FName,
                        DisplayName = s.DisplayName,
                        FWorkCenterID = s.FWorkCenterID
                    })
                .PageBy(input)
                .ToListAsync();
        }
    }
}
