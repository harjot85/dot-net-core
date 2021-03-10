namespace dotnetcore.Models
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = null;
    }
}