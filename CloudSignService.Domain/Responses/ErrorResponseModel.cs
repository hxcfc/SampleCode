namespace CloudSignService.Domain.Responses
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel(Exception ex, int code, bool includeStack = true)
        {
            Success = false;
            Code = code;
            var ErrorList = new List<object>();
            ErrorList.Add($"MESSAGE: {ex.Message}");
            if (includeStack)
            {
                ErrorList.Add($"STACK: {ex.StackTrace}");
                ErrorList.Add($"INNER EXCEPTION: {ex.InnerException}");
            }
            Errors = ErrorList.ToArray();
        }

        public int Code { get; set; }
        public object[] Errors { get; set; }
        public bool Success { get; set; }
    }
}