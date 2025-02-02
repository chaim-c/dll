using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002EA RID: 746
	public struct SocketId
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0001DD1D File Offset: 0x0001BF1D
		// (set) Token: 0x0600141D RID: 5149 RVA: 0x0001DD25 File Offset: 0x0001BF25
		public string SocketName { get; set; }

		// Token: 0x0600141E RID: 5150 RVA: 0x0001DD2E File Offset: 0x0001BF2E
		internal void Set(ref SocketIdInternal other)
		{
			this.SocketName = other.SocketName;
		}
	}
}
