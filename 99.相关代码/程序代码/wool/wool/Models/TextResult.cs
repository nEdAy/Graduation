using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace wool.Models
{
    public class TextResult : IHttpActionResult
    {
        string _value;
        HttpRequestMessage _request;
        HttpStatusCode _statusCode;

        public TextResult(string value, HttpRequestMessage request, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            _value = value;
            _request = request;
            _statusCode=statusCode;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                StatusCode=_statusCode,
                Content = new StringContent(_value, Encoding.GetEncoding("UTF-8"), "application/json"),
                RequestMessage = _request
            };
            return Task.FromResult(response);
        }
    }
}