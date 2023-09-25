using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDTool.Models
{
    public class CommonOutDto<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }


        public bool ISSuccess => Code == 0;
    }
}
