namespace CloudSignService.Domain.Responses
{
    public class ResponseWithCountModel<T> where T : class
    {
        public T Data { get; set; }
        public int Records { get; set; }
    }
}