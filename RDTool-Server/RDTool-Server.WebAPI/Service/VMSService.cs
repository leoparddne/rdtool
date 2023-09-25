using RDTool.Server.WebAPI.Dto.OutDto.VMS;
using RDTool.Server.WebAPI.Helper;
using RDTool.Server.WebAPI.IService;
using System.Collections.Generic;
using System.Linq;

namespace RDTool.Server.WebAPI.Service
{
    public class VMSService : IVMSService
    {
        public VMBoxConfig VMCofnig { get; }

        public VMSService()
        {
            VMCofnig = VMBoxHelper.Load();
        }

        public List<VMSModel> GetVMS()
        {
            return ParseVMSByCommand("list vms");
        }

        public List<VMSModel> GetRunningVMS()
        {
            return ParseVMSByCommand("list runningvms");
        }

        public string ExecCommand(string command)
        {
            //var exec = new ProcessCommandBase("VBoxManage.exe");
            var exec = new ProcessCommandBase(VMCofnig.Exe);
            exec.AddParameter(command);
            var exeResult = exec.Exec(true);

            return exeResult;
        }

        public List<VMSModel> ParseVMSByCommand(string command)
        {
            var result = new List<VMSModel>();
            var exeResult = ExecCommand(command);

            //TODO - 解析
            var parseValue = VMSModel.BatchParse(exeResult);
            if (parseValue == null || parseValue.Count == 0)
            {
                return result;
            }

            return parseValue;
        }

        public List<VMSModel> GetVMSState()
        {
            var result = new List<VMSModel>();

            var vms = GetVMS();

            var runningvms = GetRunningVMS();

            if (vms == null || vms.Count == 0)
            {
                return result;
            }

            Dictionary<string, VMSModel> runningDic = null;
            if (!(runningvms == null || runningvms.Count == 0))
            {
                runningDic = runningvms.ToDictionary(f => f.ID, f => f);
            }

            foreach (VMSModel vm in vms)
            {
                if (runningDic != null && runningDic.ContainsKey(vm.ID))
                {
                    vm.ISRunning = true;
                }
                result.Add(vm);
            }

            return result;
        }

        public void Start(VMSModel model)
        {
            ExecCommand($"startvm \"{model.Name}\" --type headless");
        }

        public void Stop(VMSModel model)
        {
            ExecCommand($"controlvm \"{model.Name}\" poweroff");
        }
    }
}
