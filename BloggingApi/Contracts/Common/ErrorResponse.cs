using Application.Enums;
using Application.Models;

namespace BloggingApi.Contracts.Common;

public class ErrorResponse
{
    public ErrorResponse()
    {
        TimeStap = DateTime.Now;
        Errors = new List<string>();
    }

    public string StatusPhrase { get; set; }
    public int StatusCode { get; set; }
    public List<string> Errors { get; set; } = new();
    public DateTime TimeStap { get; set; }
}