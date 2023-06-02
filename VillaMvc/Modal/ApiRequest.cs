using static VillaUtility.SD;

namespace VillaMvc.Modal
{
    public class ApiRequest
    {
        public APiType apiType { get; set; } = APiType.GET;

        public string Url { get; set; }

        public object Data { get; set; }
    }
}
