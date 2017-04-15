namespace Bun.Blog.Web.Framework.Mvc.Json
{
    public class JsonResponse : IBunJsonResponse
    {
        public JsonResponse()
        {
            Status = JsonResponseStatus.success;
        }

        public JsonResponse(JsonResponseStatus status, string message)
        {
            Status = status;
            Message = message;
        }

        public JsonResponseStatus Status { get; set; }

        public string Message { get; set; }
    }

    public class JsonResponse<T> : JsonResponse
    {
        public JsonResponse(T content)
        {
            Status = JsonResponseStatus.success;
            Content = content;
        }

        public JsonResponse(T content, JsonResponseStatus status, string message)
        {
            Status = status;
            Message = message;
            Content = content;
        }

        public T Content { get; set; }
    }

    public class PlainJsonResponse : IBunJsonResponse
    {
        public PlainJsonResponse(object content)
        {
            Content = content;
        }

        public object Content { get; set; }
    }
}
