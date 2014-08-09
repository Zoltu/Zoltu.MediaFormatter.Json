using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace StayUpdated.MonitorAdapter.Filters
{
	public sealed class DeserializationExceptionFilterAttribute : ExceptionFilterAttribute
	{
		public override void OnException(HttpActionExecutedContext actionExecutedContext)
		{
			Contract.Assume(actionExecutedContext != null);

			var exception = actionExecutedContext.Exception;
			if (exception is Newtonsoft.Json.JsonSerializationException || exception is Newtonsoft.Json.JsonReaderException)
			{
				actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, exception.Message);
			}
		}
	}
}
