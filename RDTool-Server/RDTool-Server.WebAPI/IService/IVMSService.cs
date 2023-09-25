using RDTool.Server.WebAPI.Dto.OutDto.VMS;
using System.Collections.Generic;

namespace RDTool.Server.WebAPI.IService
{
    public interface IVMSService
    {
        List<VMSModel> GetRunningVMS();
        List<VMSModel> GetVMS();
        List<VMSModel> GetVMSState();
        void Start(VMSModel model);
        void Stop(VMSModel model);
    }
}
