namespace Mango.Web.Models
{
    public class BaseServiceResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T? Data { get; set; }

        public BaseServiceResponse()
        {
            Success = true;
            Errors = [];
        }

        public BaseServiceResponse(bool success, string message = null, List<string> errors = null, T? data = default)
        {
            Success = success;
            Message = message;
            Errors = errors ?? new List<string>();
            Data = data;
        }
    }
}
