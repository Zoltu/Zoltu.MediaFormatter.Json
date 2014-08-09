using System;
using System.Diagnostics.Contracts;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StayUpdated.MonitorAdapter.Formatters
{
	public class JsonMediaFormatter : MediaTypeFormatter
	{
		private readonly JsonSerializer _jsonSerializer = new JsonSerializer();

		[ContractInvariantMethod]
		private void ContractInvariants()
		{
			Contract.Invariant(_jsonSerializer != null);
		}

		public JsonMediaFormatter()
		{
			Contract.Assume(SupportedMediaTypes != null);

			SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
		}

		public override Boolean CanReadType(Type type)
		{
			if (type == null)
				return false;

			return true;
		}

		public override Boolean CanWriteType(Type type)
		{
			if (type == null)
				return false;

			return true;
		}

		public override Task<Object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (type == null)
				throw new ArgumentNullException("type");
			if (readStream == null)
				throw new ArgumentNullException("readStream");

			return Task.Factory.StartNew(() => Deserialize(readStream, type));
		}

		public override Task WriteToStreamAsync(Type type, Object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			if (writeStream == null)
				throw new ArgumentNullException("writeStream");

			return Task.Factory.StartNew(() => Serialize(writeStream, value));
		}

		private Object Deserialize(Stream readStream, Type type)
		{
			Contract.Requires(readStream != null);
			Contract.Requires(type != null);

			var streamReader = new StreamReader(readStream);
			var result = _jsonSerializer.Deserialize(streamReader, type);
			if (result == null)
				throw new JsonSerializationException("Attempted to deserialize an empty string.");
			return result;
		}

		private void Serialize(Stream writeStream, Object value)
		{
			Contract.Requires(writeStream != null);
			Contract.Requires(value != null);

			var streamWriter = new StreamWriter(writeStream);
			_jsonSerializer.Serialize(streamWriter, value);
			streamWriter.Flush();
		}
	}
}
