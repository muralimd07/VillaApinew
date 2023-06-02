using System.Net;

namespace VillaMvc.Modal
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorsMessages { get; set; }
        public object Result { get; set; }

    }
}
