using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RDTool.Server.WebAPI.Dto.OutDto.VMS;
using RDTool.Server.WebAPI.IService;
using System.Collections.Generic;

namespace RDTool.Server.WebAPI.Controllers
{
    public class VBoxManageController : BaseController
    {
        private IVMSService vmsServices { get; set; }

        public VBoxManageController(IVMSService vmsServices)
        {
            this.vmsServices = vmsServices;
        }


        /// <summary>
        /// 获取所有虚拟
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public List<VMSModel> GetVMS()
        {
            return vmsServices.GetVMSState();
        }

        [HttpPost]
        public void Start(VMSModel model)
        {
            vmsServices.Start(model);
        }


        [HttpPost]
        public void Stop(VMSModel model)
        {
            vmsServices.Stop(model);
        }
    }
}
