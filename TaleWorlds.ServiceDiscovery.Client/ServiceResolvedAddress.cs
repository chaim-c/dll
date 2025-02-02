using System;

namespace TaleWorlds.ServiceDiscovery.Client
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class ServiceResolvedAddress
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023D9 File Offset: 0x000005D9
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000023E1 File Offset: 0x000005E1
		public string Address { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023EA File Offset: 0x000005EA
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000023F2 File Offset: 0x000005F2
		public int Port { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023FB File Offset: 0x000005FB
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002403 File Offset: 0x00000603
		public bool IsSecure { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000240C File Offset: 0x0000060C
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002414 File Offset: 0x00000614
		public string[] Tags { get; private set; }

		// Token: 0x0600001F RID: 31 RVA: 0x0000241D File Offset: 0x0000061D
		public ServiceResolvedAddress(string address, int port, bool isSecure, string[] tags)
		{
			this.Address = address;
			this.Port = port;
			this.IsSecure = isSecure;
			this.Tags = tags;
		}
	}
}
