using ConfigDetect.Helper;
using HandyControl.Controls;
using RDTool.Base;
using RDTool.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RDTool.ViewModel
{
    public class VMFastConnect : ViewModelBase
    {

        public VMConnectionItem SelectConnect { get; set; }

        /// <summary>
        /// 所有连接
        /// </summary>
        public ObservableCollection<VMConnectionItem> Connections { get; set; } = new ObservableCollection<VMConnectionItem>();


        public ICommand ConnectCommand { get; set; }


        public VMFastConnect()
        {
            LoadConfig();
            ConnectCommand = new BaseCommand(p =>
            {
                if (!(p is VMConnectionItem connectionItem))
                {
                    Growl.Error("无法解析连接信息");
                    return;
                }

                var connecting = new ProcessCommandBase("mstsc.exe");

                connecting.AddParameter($"/v:{connectionItem.IP}:{connectionItem.Port}");
                connecting.Exec();
            });
        }

        private void LoadConfig()
        {
            var config = AppSettingSingleton.Instance.Connections;
            if (config == null || config.Count == 0)
            {
                return;
            }


            foreach (var item in config)
            {
                Connections.Add(new VMConnectionItem
                {
                    IP = item.IP,
                    Name = item.Name,
                    Port = item.Port,
                    Username = item.Username
                });
            }
        }
    }
}
