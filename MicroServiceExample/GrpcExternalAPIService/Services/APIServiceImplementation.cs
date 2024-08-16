using Grpc.Core;
using GrpcExternalAPIService;
using System.Text;

namespace GrpcExternalAPIService.Services
{
	public class APIServiceImplementation : APIService.APIServiceBase
	{
		private readonly HttpClient _httpClient;
		public APIServiceImplementation(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public override async Task<APIResponse> CallExternalAPI(APIRequest request, ServerCallContext context)
		{
			var httpRequest = new HttpRequestMessage
			{
				Method = new HttpMethod(request.Method),
				RequestUri = new Uri(request.Url)
			};

			if (request.Method == "POST" && !string.IsNullOrEmpty(request.Body))
			{
				httpRequest.Content = new StringContent(request.Body, Encoding.UTF8, "application/json");
			}

			var response = await _httpClient.SendAsync(httpRequest);
			var responseBody = await response.Content.ReadAsStringAsync();

			return new APIResponse
			{
				ResponseBody = responseBody,
				StatusCode = (int)response.StatusCode
			};
		}
	}
}
