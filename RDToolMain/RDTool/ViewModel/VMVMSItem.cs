using RDTool.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDTool.ViewModel
{
    public class VMVMSItem : ViewModelBase
    {
        public string Name { get; set; }


        public string ID { get; set; }
        public bool ISRunning { get; set; }

        public bool CanStart
        {
            get
            {
                return !ISRunning;
            }
        }


        public string RunningState
        {
            get
            {
                return ISRunning ? "运行中" : "停止";
            }
        }
    }
}
