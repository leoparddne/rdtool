using HandyControl.Controls;
using Newtonsoft.Json;
using RDTool.Base;
using RDTool.Helper;
using RDTool.Models.Consul;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RDTool.ViewModel.Consul
{
    public class VMConsul : ViewModelBase
    {

        public string IP { get; set; } = "192.168.2.49";
        public int Port { get; set; } = 7500;

        /// <summary>
        /// 选择的KEY
        /// </summary>
        private string selectKV;

        public string SelectKV
        {
            get { return selectKV; }
            set
            {
                selectKV = value;
                LoadKV(value);
            }
        }

        public bool EnableSaveBtn
        {
            get
            {
                return !string.IsNullOrEmpty(SelectKV);
            }
        }

        /// <summary>
        /// consul kv实际配置的数据
        /// </summary>
        public ConsulOutDto ConsulKVInfo { get; set; }

        public ObservableCollection<string> KVList { get; set; } = new ObservableCollection<string>();

        public ICommand GetKVCommand { get; set; }
        public ICommand SaveKVCommand { get; set; }


        public VMConsul()
        {
            GetKVCommand = new BaseCommand(async (para) =>
            {
                var result = await HttpHelper.GetAsync<List<string>>($"http://{IP}:{Port}/v1/kv/?keys", null, null, 3000);

                KVList = new ObservableCollection<string>(result);
            });

            SaveKVCommand = new BaseCommand(async (para) =>
            {
                if (string.IsNullOrEmpty(ConsulKVInfo.RawValue))
                {
                    Growl.Error("未配置对应key");
                    return;
                }

                var jsonObj = JsonConvert.DeserializeObject(ConsulKVInfo.RawValue);
                if (jsonObj == null)
                {
                    Growl.Error("无法解析key为json对象");
                    return;
                }
                var result = await HttpHelper.PutAsync($"http://{IP}:{Port}/v1/kv/{ConsulKVInfo.Key}", jsonObj, null, 3000);
                Growl.Success("保存成功");
            });
        }


        private async void LoadKV(string value)
        {
            var result = await HttpHelper.GetAsync<List<ConsulOutDto>>($"http://{IP}:{Port}/v1/kv/{value}", null, null, 3000);

            if (result == null || result.Count <= 0)
            {
                ConsulKVInfo = null;
            }

            ConsulKVInfo = result.First();
        }
    }
}
