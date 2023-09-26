using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDTool.Models.Consul
{
    public class ConsulOutDto
    {
        public int LockIndex { get; set; }
        public string Key { get; set; }
        public int Flags { get; set; }

        private string consulRawValue;

        public string Value
        {
            get { return consulRawValue; }
            set
            {
                consulRawValue = value;
                FlushValue();
            }
        }

        private void FlushValue()
        {
            if (string.IsNullOrEmpty(Value))
            {
                RawValue = string.Empty;
                return;
            }

            byte[] base64Byte = Convert.FromBase64String(Value);
            var rawString = Encoding.Default.GetString(base64Byte);

            RawValue = rawString;
        }

        public int CreateIndex { get; set; }
        public int ModifyIndex { get; set; }


        public string RawValue
        {
            get; set;
        }
    }
}
