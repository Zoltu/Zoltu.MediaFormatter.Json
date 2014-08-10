using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Zoltu.MediaFormatter.Json
{
	public sealed class JsonDeserializationExceptionFilterAttribute : ExceptionFilterAttribute
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
