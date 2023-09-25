using RDTool.Base;

namespace RDTool.ViewModel
{
    public class VMConnectionItem : ViewModelBase
    {
        public string Name { get; set; }

        public string IP { get; set; }
        public uint Port { get; set; }

        public string Username { get; set; }
    }
}
