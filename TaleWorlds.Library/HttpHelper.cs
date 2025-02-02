using System;
using System.Threading.Tasks;
using TaleWorlds.Library.Http;

namespace TaleWorlds.Library
{
	// Token: 0x02000034 RID: 52
	public static class HttpHelper
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00006CD3 File Offset: 0x00004ED3
		public static Task<string> DownloadStringTaskAsync(string url)
		{
			return HttpDriverManager.GetDefaultHttpDriver().HttpGetString(url, false);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006CE1 File Offset: 0x00004EE1
		public static Task<byte[]> DownloadDataTaskAsync(string url)
		{
			return HttpDriverManager.GetDefaultHttpDriver().HttpDownloadData(url);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006CEE File Offset: 0x00004EEE
		public static Task<string> PostStringAsync(string url, string postData, string mediaType = "application/json")
		{
			return HttpDriverManager.GetDefaultHttpDriver().HttpPostString(url, postData, mediaType, false);
		}
	}
}
