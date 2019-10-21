﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.AutoMapper;
using JIT.DIME2Barcode.Entities;
using JIT.DIME2Barcode.Model;
using JIT.DIME2Barcode.Permissions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace JIT.DIME2Barcode.AppService
{
    /// <summary>
    /// 基础资料信息
    /// </summary>
    public class Sys_BasicInfoAppService:BaseAppService
    {
        /// <summary>
        /// 查询所有目录
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        [AbpAuthorize(ProductionPlanPermissionsNames.BasicInfo_Get)]
        public async Task<List<Sys_BasicInfo>> GetAll(int? ParentId)
        {
            return await JIT_Sys_BasicInfo.GetAll().Where(w => w.ParentId.Equals(ParentId)).OrderBy(o=>o.BIOrder).ToListAsync();
        }
        /// <summary>
        /// 查询所有目录
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        //[AbpAuthorize(ProductionPlanPermissionsNames.BasicInfo_Get)]
        public async Task<List<treeData>> GetAll3(int? parentId)
        {
            var result = await JIT_Sys_BasicInfo.GetAll().ToListAsync();
            return await RecursionGetAll(result,parentId);
        }
        /// <summary>
        /// 所有子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task<List<treeData>> RecursionGetAll(List<Sys_BasicInfo> mSysBasicInfos,int? parentId)
        {
            // 
            var result = mSysBasicInfos
                .Where(w => w.ParentId.Equals(parentId))
                .OrderBy(o => o.BIOrder)
                .Select(s => new treeData
                {
                    BasicInfoId = s.BasicInfoId, title = s.BIName, key = s.BasicInfoId.ToString(), BIURL = s.BIURL,
                    children = new List<treeData>()
                })
                .ToList();
            // 
            foreach (var item in result)
            {
                item.children = await RecursionGetAll(mSysBasicInfos,item.BasicInfoId);
            }

            return result;
        }
        /// <summary>
        /// 新建和修改信息
        /// </summary>
        /// <returns></returns>
        public async Task<int> Create(List<Sys_BasicInfo> input)
        {
            foreach (var item in input)
            {
                item.CreateUserId = this.AbpSession.UserId.HasValue ? this.AbpSession.UserId.Value : 0;
                
                 await  JIT_Sys_BasicInfo.InsertOrUpdateAsync(item);
            }
            return  1;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input">要删除的集合</param>
        /// <returns>异常返回 -1，成功返回 1</returns>
        [HttpPost]
        public async Task<int> Delete(List<Sys_BasicInfo> input)
        {
            // 异常返回 -1，成功返回 1
            try
            {
                // 页面处理
                //input = input.Where(w => w.BasicInfoId > 0).ToList();
                // 删除选中节点
                foreach (var item in input)
                {
                    await JIT_Sys_BasicInfo.DeleteAsync(item);
                }
                // 查询选中子节点明细
                int[] bArray = input.Select(s => s.BasicInfoId).ToArray();
                var result = await JIT_Sys_BasicInfo.GetAll().Where(w => bArray.Contains(Convert.ToInt32(w.ParentId))).ToListAsync();
                // 删除所有的子节点
                if (result.Count > 0)
                {
                    return await Delete(result);
                }
                // 删除成功返回 1
                return 1;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // 删除成功返回 0
                return -1;
            }
        }
    }
}
