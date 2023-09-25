using ConfigDetect.Helper;
using HandyControl.Controls;
using RDTool.Base;
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
        public ObservableCollection<VMConnectionItem> Connections { get; set; } = new ObservableCollection<VMConnectionItem>() {
            new VMConnectionItem{
                Name="192.168.2.49",
                IP="192.168.2.49",
                Port=3389,
                Username="administrator",
            },
            new VMConnectionItem{
                Name="192.168.2.48",
                IP="192.168.2.48",
                Port=3389,
                Username="administrator",
            }
        };


        public ICommand ConnectCommand { get; set; }


        public VMFastConnect()
        {
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
    }
}
