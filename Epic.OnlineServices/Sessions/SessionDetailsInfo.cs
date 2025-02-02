using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000129 RID: 297
	public struct SessionDetailsInfo
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x0000CABC File Offset: 0x0000ACBC
		// (set) Token: 0x060008CC RID: 2252 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		public Utf8String SessionId { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0000CACD File Offset: 0x0000ACCD
		// (set) Token: 0x060008CE RID: 2254 RVA: 0x0000CAD5 File Offset: 0x0000ACD5
		public Utf8String HostAddress { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0000CADE File Offset: 0x0000ACDE
		// (set) Token: 0x060008D0 RID: 2256 RVA: 0x0000CAE6 File Offset: 0x0000ACE6
		public uint NumOpenPublicConnections { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0000CAEF File Offset: 0x0000ACEF
		// (set) Token: 0x060008D2 RID: 2258 RVA: 0x0000CAF7 File Offset: 0x0000ACF7
		public SessionDetailsSettings? Settings { get; set; }

		// Token: 0x060008D3 RID: 2259 RVA: 0x0000CB00 File Offset: 0x0000AD00
		internal void Set(ref SessionDetailsInfoInternal other)
		{
			this.SessionId = other.SessionId;
			this.HostAddress = other.HostAddress;
			this.NumOpenPublicConnections = other.NumOpenPublicConnections;
			this.Settings = other.Settings;
		}
	}
}
