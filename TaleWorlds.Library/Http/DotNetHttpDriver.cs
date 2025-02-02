using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaleWorlds.Library.Http
{
	// Token: 0x020000A8 RID: 168
	public class DotNetHttpDriver : IHttpDriver
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x000140BC File Offset: 0x000122BC
		public DotNetHttpDriver()
		{
			ServicePointManager.DefaultConnectionLimit = 5;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			this._httpClient = new HttpClient();
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000140DF File Offset: 0x000122DF
		IHttpRequestTask IHttpDriver.CreateHttpPostRequestTask(string address, string postData, bool withUserToken)
		{
			return new HttpPostRequest(this._httpClient, address, postData);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000140EE File Offset: 0x000122EE
		IHttpRequestTask IHttpDriver.CreateHttpGetRequestTask(string address, bool withUserToken)
		{
			return new HttpGetRequest(this._httpClient, address);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000140FC File Offset: 0x000122FC
		async Task<string> IHttpDriver.HttpGetString(string url, bool withUserToken)
		{
			return await this._httpClient.GetStringAsync(url);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001414C File Offset: 0x0001234C
		async Task<string> IHttpDriver.HttpPostString(string url, string postData, string mediaType, bool withUserToken)
		{
			HttpResponseMessage httpResponseMessage = await this._httpClient.PostAsync(url, new StringContent(postData, Encoding.Unicode, mediaType));
			string result;
			using (HttpResponseMessage response = httpResponseMessage)
			{
				using (HttpContent content = response.Content)
				{
					result = await content.ReadAsStringAsync();
				}
			}
			return result;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000141AC File Offset: 0x000123AC
		async Task<byte[]> IHttpDriver.HttpDownloadData(string url)
		{
			return await this._httpClient.GetByteArrayAsync(url);
		}

		// Token: 0x040001C9 RID: 457
		private HttpClient _httpClient;
	}
}
