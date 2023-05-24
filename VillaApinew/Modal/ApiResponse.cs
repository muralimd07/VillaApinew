using System.Net;

namespace VillaApinew.Modal
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode {  get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorsMessages { get; set; }
        public object Result { get; set; }

    }
}
