using System;
using System.Net;

namespace TaleWorlds.PlayerServices
{
	// Token: 0x02000006 RID: 6
	public class TimeoutWebClient : WebClient
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000029BB File Offset: 0x00000BBB
		public TimeoutWebClient()
		{
			this.Timeout = 15000;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029CE File Offset: 0x00000BCE
		public TimeoutWebClient(int timeout)
		{
			this.Timeout = timeout;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000029DD File Offset: 0x00000BDD
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000029E5 File Offset: 0x00000BE5
		public int Timeout { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x000029EE File Offset: 0x00000BEE
		protected override WebRequest GetWebRequest(Uri address)
		{
			WebRequest webRequest = base.GetWebRequest(address);
			webRequest.Timeout = this.Timeout;
			return webRequest;
		}
	}
}
