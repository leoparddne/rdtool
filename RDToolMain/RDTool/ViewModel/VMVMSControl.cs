using HandyControl.Controls;
using RDTool.Base;
using RDTool.Helper;
using RDTool.Models;
using RDTool.Models.VMS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RDTool.ViewModel
{
    public class VMVMSControl : ViewModelBase
    {
        public VMVMSItem SelectVMS { get; set; }
        public ObservableCollection<VMVMSItem> VMSList { get; set; } = new ObservableCollection<VMVMSItem>() { new VMVMSItem { Name = "test" } };

        public ICommand StartCommand { get; set; }
        public ICommand StopCommand { get; set; }

        public VMVMSControl()
        {
            LoadVMS();

            StartCommand = new BaseCommand(async p =>
            {
                if (!(p is VMVMSItem vms))
                {
                    return;
                }

                if (vms == null)
                {
                    return;
                }

                await StartVMS(vms.Name);
                vms.ISRunning = true;
            });

            StopCommand = new BaseCommand(async p =>
            {
                if (!(p is VMVMSItem vms))
                {
                    return;
                }

                if (vms == null)
                {
                    return;
                }

                await StopVMS(vms.Name);
                vms.ISRunning = false;
            });
        }

        private async Task StartVMS(string name)
        {
            var result = await HttpHelper.PostAsync<CommonOutDto<List<VMSOutDto>>>(AppSettingSingleton.Instance.VMS.BaseURL + "/api/rdtool/VBoxManage/Start", new
            {
                Name = name
            }, null, 10 * 1000);
            //var result = callResult.Result;
            if (!result.ISSuccess)
            {
                Growl.Error(result.Message);
                return;
            }

            Growl.Info("开机完成");
            return;
        }

        private async Task StopVMS(string name)
        {
            var result = await HttpHelper.PostAsync<CommonOutDto<List<VMSOutDto>>>(AppSettingSingleton.Instance.VMS.BaseURL + "/api/rdtool/VBoxManage/Stop", new
            {
                Name = name
            }, null, 3 * 1000);
            //var result = callResult.Result;
            if (!result.ISSuccess)
            {
                Growl.Error(result.Message);
                return;
            }

            Growl.Info("关机完成");
            return;
        }

        private async Task LoadVMS()
        {
            var result = await HttpHelper.GetAsync<CommonOutDto<List<VMSOutDto>>>(AppSettingSingleton.Instance.VMS.BaseURL + "/api/rdtool/VBoxManage/GetVMS", null, null, 3 * 1000);
            //var result = callResult.Result;
            if (!result.ISSuccess)
            {
                Growl.Error(result.Message);
                return;
            }

            if (result.Data == null || result.Data.Count <= 0)
            {
                VMSList = new ObservableCollection<VMVMSItem>();
                return;
            }

            VMSList.Clear();
            foreach (var item in result.Data)
            {
                VMSList.Add(new VMVMSItem
                {
                    Name = item.Name,
                    ID = item.ID,
                    ISRunning = item.ISRunning
                });
            }

            return;
        }
    }

}
