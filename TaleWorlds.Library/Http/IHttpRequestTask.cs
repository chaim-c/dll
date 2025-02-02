using System;

namespace TaleWorlds.Library.Http
{
	// Token: 0x020000AE RID: 174
	public interface IHttpRequestTask
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600064F RID: 1615
		HttpRequestTaskState State { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000650 RID: 1616
		bool Successful { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000651 RID: 1617
		string ResponseData { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000652 RID: 1618
		Exception Exception { get; }

		// Token: 0x06000653 RID: 1619
		void Start();
	}
}
