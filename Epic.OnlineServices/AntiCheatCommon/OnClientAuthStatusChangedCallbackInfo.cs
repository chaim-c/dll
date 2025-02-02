using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x02000600 RID: 1536
	public struct OnClientAuthStatusChangedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x0003AC20 File Offset: 0x00038E20
		// (set) Token: 0x06002767 RID: 10087 RVA: 0x0003AC28 File Offset: 0x00038E28
		public object ClientData { get; set; }

		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x0003AC31 File Offset: 0x00038E31
		// (set) Token: 0x06002769 RID: 10089 RVA: 0x0003AC39 File Offset: 0x00038E39
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x0003AC42 File Offset: 0x00038E42
		// (set) Token: 0x0600276B RID: 10091 RVA: 0x0003AC4A File Offset: 0x00038E4A
		public AntiCheatCommonClientAuthStatus ClientAuthStatus { get; set; }

		// Token: 0x0600276C RID: 10092 RVA: 0x0003AC54 File Offset: 0x00038E54
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x0003AC6F File Offset: 0x00038E6F
		internal void Set(ref OnClientAuthStatusChangedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.ClientHandle = other.ClientHandle;
			this.ClientAuthStatus = other.ClientAuthStatus;
		}
	}
}
