using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000602 RID: 1538
	public struct OnMessageToClientCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06002779 RID: 10105 RVA: 0x0003ADDE File Offset: 0x00038FDE
		// (set) Token: 0x0600277A RID: 10106 RVA: 0x0003ADE6 File Offset: 0x00038FE6
		public object ClientData { get; set; }

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x0600277B RID: 10107 RVA: 0x0003ADEF File Offset: 0x00038FEF
		// (set) Token: 0x0600277C RID: 10108 RVA: 0x0003ADF7 File Offset: 0x00038FF7
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x0003AE00 File Offset: 0x00039000
		// (set) Token: 0x0600277E RID: 10110 RVA: 0x0003AE08 File Offset: 0x00039008
		public ArraySegment<byte> MessageData { get; set; }

		// Token: 0x0600277F RID: 10111 RVA: 0x0003AE14 File Offset: 0x00039014
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0003AE2F File Offset: 0x0003902F
		internal void Set(ref OnMessageToClientCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.ClientHandle = other.ClientHandle;
			this.MessageData = other.MessageData;
		}
	}
}
