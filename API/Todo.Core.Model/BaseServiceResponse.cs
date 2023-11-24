using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Core.Model
{
    public class BaseServiceResponse
    {
        protected BaseServiceResponse()
        {
        }

        protected BaseServiceResponse(int httpStatusCode, int returnCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            ReturnCode = returnCode;
            Message = message;
        }

        protected BaseServiceResponse(int httpStatusCode, string message)
        {
            HttpStatusCode = httpStatusCode;
            Message = message;
        }

        public int HttpStatusCode { get; set; }
        public int ReturnCode { get; set; }
        public string Message { get; set; }
    }
}
