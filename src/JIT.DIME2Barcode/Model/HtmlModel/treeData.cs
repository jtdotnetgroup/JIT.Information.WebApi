using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace JIT.DIME2Barcode.Model
{
    /// <summary>
    /// 树形菜单
    /// </summary>
    public class treeData
    {
        public int? BasicInfoId { get; set; }
        [CanBeNull] public string key { get; set; }
        public string title { get; set; }
        [CanBeNull] public  string BIURL { get; set; }
        public List<treeData> children { get; set; }
    }
}
