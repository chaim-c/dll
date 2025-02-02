using System;
using System.Threading.Tasks;

namespace TaleWorlds.Library.Http
{
	// Token: 0x020000AD RID: 173
	public interface IHttpDriver
	{
		// Token: 0x0600064A RID: 1610
		Task<string> HttpGetString(string url, bool withUserToken);

		// Token: 0x0600064B RID: 1611
		Task<string> HttpPostString(string url, string postData, string mediaType, bool withUserToken);

		// Token: 0x0600064C RID: 1612
		Task<byte[]> HttpDownloadData(string url);

		// Token: 0x0600064D RID: 1613
		IHttpRequestTask CreateHttpPostRequestTask(string address, string postData, bool withUserToken);

		// Token: 0x0600064E RID: 1614
		IHttpRequestTask CreateHttpGetRequestTask(string address, bool withUserToken);
	}
}
