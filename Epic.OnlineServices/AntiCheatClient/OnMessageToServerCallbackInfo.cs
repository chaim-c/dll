using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200062D RID: 1581
	public struct OnMessageToServerCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x0003C1CA File Offset: 0x0003A3CA
		// (set) Token: 0x0600284F RID: 10319 RVA: 0x0003C1D2 File Offset: 0x0003A3D2
		public object ClientData { get; set; }

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x0003C1DB File Offset: 0x0003A3DB
		// (set) Token: 0x06002851 RID: 10321 RVA: 0x0003C1E3 File Offset: 0x0003A3E3
		public ArraySegment<byte> MessageData { get; set; }

		// Token: 0x06002852 RID: 10322 RVA: 0x0003C1EC File Offset: 0x0003A3EC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x0003C207 File Offset: 0x0003A407
		internal void Set(ref OnMessageToServerCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.MessageData = other.MessageData;
		}
	}
}
