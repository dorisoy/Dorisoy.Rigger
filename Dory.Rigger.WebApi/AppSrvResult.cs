using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dory.Rigger.WebApi
{
    /// <summary>
    /// Application返回结果包装类,无返回类型(void,task)
    /// </summary>
    public sealed class AppSrvResult
    {
        public AppSrvResult()
        {

        }
        public AppSrvResult([NotNull] ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }
        public bool IsSuccess
        {
            get
            {
                return ProblemDetails == null;
            }
        }
        public ProblemDetails ProblemDetails { get; set; }

        public static implicit operator AppSrvResult([NotNull] ProblemDetails problemDetails)
        {
            return new AppSrvResult
            {
                ProblemDetails = problemDetails
            };
        }
    }
    public sealed class AppSrvResult<TValue>
    {
        public AppSrvResult() { }

        public AppSrvResult([NotNull] TValue value)
        {
            Content = value;
        }

        public AppSrvResult([NotNull] ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }

        public bool IsSuccess
        {
            get
            {
                return ProblemDetails == null && Content != null;
            }
        }

        public TValue Content { get; set; }

        public ProblemDetails ProblemDetails { get; set; }

        public static implicit operator AppSrvResult<TValue>(AppSrvResult result)
        {
            return new AppSrvResult<TValue>
            {
                Content = default(TValue),
                ProblemDetails = result.ProblemDetails
            };
        }

        public static implicit operator AppSrvResult<TValue>(ProblemDetails problemDetails)
        {
            return new AppSrvResult<TValue>
            {
                Content = default(TValue)
                ,
                ProblemDetails = problemDetails
            };
        }

        public static implicit operator AppSrvResult<TValue>(TValue value)
        {
            return new AppSrvResult<TValue>(value);
        }

    }
    public sealed class ProblemDetails
    {
        public ProblemDetails()
        {

        }
        public ProblemDetails(HttpStatusCode? statusCode = null, string detail = null, string title = null, string instance = null, string type = null)
        {
            var status = statusCode.HasValue ? (int)statusCode.Value : (int)HttpStatusCode.BadRequest;
            Status = status;
            Title = title ?? "参数错误";
            Detail = detail;
            Instance = instance;
            Type = type ?? string.Concat("https://httpstatuses.com/", status);
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, SystemTextJsonHelper.GetAdncDefaultOptions());
        }
        [JsonPropertyName("detail")]
        public string Detail { get; set; }

        [JsonExtensionData]
        public IDictionary<string, object> Extensions { get; }

        [JsonPropertyName("instance")]
        public string Instance { get; set; }

        [JsonPropertyName("status")]
        public int? Status { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
