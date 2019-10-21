using System;
using System.Collections.Generic;
using System.Text;

namespace JIT.DIME2Barcode.TaskAssignment.RemainderLCL
{
    /// <summary>
    /// 
    /// </summary>
    public class RemainderLCLCreateInput
    {
        public List<RemainderLCLMxCreateInput> LCLMxCreateInput { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class RemainderLCLMxCreateInput
    {
        public string ICMOInspectBillId { get; set; }
        public decimal SpelledQty { get; set; }
    }
}
