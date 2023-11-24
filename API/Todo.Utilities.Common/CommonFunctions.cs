using Microsoft.AspNetCore.Mvc;

namespace Todo.Utilities.Common
{
    public static class CommonFunctions
    {
        public static ProblemDetails GetProblemDetails(int statusCode, string title = null)
        {
            //var problemDetails = new ProblemDetails
            //{
            //    Status = StatusCodes.Status403Forbidden,
            //    Type = "https://example.com/probs/out-of-credit",
            //    Title = "You do not have enough credit.",
            //    Detail = "Your current balance is 30, but that costs 50.",
            //    Instance = HttpContext.Request.Path
            //};

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title
            };

            return problemDetails;
        }
    }
}
