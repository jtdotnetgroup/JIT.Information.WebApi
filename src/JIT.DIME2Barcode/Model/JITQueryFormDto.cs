using System;
using System.Collections.Generic;
using CommonTools;

namespace JIT.DIME2Barcode.Model
{
    public class JITQueryFormDto
    {
        public string Name { get; set; }
        public string DispName { get; set; }
        public string FieldType { get; set; }
        public List<Object> Values { get; set; }
    }
}