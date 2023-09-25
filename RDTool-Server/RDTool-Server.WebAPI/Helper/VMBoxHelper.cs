namespace RDTool.Server.WebAPI.Helper
{
    public class VMBoxHelper
    {
        public static VMBoxConfig Load()
        {
            return AppSettingsHelper.GetObject<VMBoxConfig>("VMBOX");
        }
    }


    public class VMBoxConfig
    {
        public string Exe { get; set; }
    }
}
