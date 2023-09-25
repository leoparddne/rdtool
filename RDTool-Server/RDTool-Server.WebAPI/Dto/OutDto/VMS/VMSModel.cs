using System.Collections.Generic;

namespace RDTool.Server.WebAPI.Dto.OutDto.VMS
{
    public class VMSModel
    {
        public string Name { get; set; }
        public string ID { get; set; }

        /// <summary>
        /// 是否运行中
        /// </summary>
        public bool ISRunning { get; set; } = false;

        public static VMSModel Parse(string value)
        {
            //#if DEBUG
            //            //value = "\"test1\" {ccda8cc4-a670-4450-ae4e-071917ba0e53}";
            //            value = "\"UOS Server\" {837ccab2-fd28-4ae9-8fc5-fa9a19a485ee}";
            //#endif
            var info = value.Trim();
            //根据【" 】(引号+空格)
            var splitList = info.Split("\" ");
            if (splitList.Length != 2)
            {
                return null;
            }


            var Name = splitList[0].Trim('\"');
            var ID = splitList[1].Trim('{');
            ID = ID.Trim('}');
            return new VMSModel { Name = Name, ID = ID };
        }


        public static List<VMSModel> BatchParse(string value)
        {
            var result = new List<VMSModel>();
            var list = value.Split('\n');
            foreach (var item in list)
            {
                var parseValue = Parse(item);
                if (parseValue == null)
                {
                    continue;
                }
                result.Add(parseValue);
            }

            return result;
        }
    }
}
