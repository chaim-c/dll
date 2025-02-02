using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005FE RID: 1534
	public struct OnClientActionRequiredCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x0003A965 File Offset: 0x00038B65
		// (set) Token: 0x0600274C RID: 10060 RVA: 0x0003A96D File Offset: 0x00038B6D
		public object ClientData { get; set; }

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x0003A976 File Offset: 0x00038B76
		// (set) Token: 0x0600274E RID: 10062 RVA: 0x0003A97E File Offset: 0x00038B7E
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x0003A987 File Offset: 0x00038B87
		// (set) Token: 0x06002750 RID: 10064 RVA: 0x0003A98F File Offset: 0x00038B8F
		public AntiCheatCommonClientAction ClientAction { get; set; }

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x0003A998 File Offset: 0x00038B98
		// (set) Token: 0x06002752 RID: 10066 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		public AntiCheatCommonClientActionReason ActionReasonCode { get; set; }

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x0003A9A9 File Offset: 0x00038BA9
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x0003A9B1 File Offset: 0x00038BB1
		public Utf8String ActionReasonDetailsString { get; set; }

		// Token: 0x06002755 RID: 10069 RVA: 0x0003A9BC File Offset: 0x00038BBC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x0003A9D8 File Offset: 0x00038BD8
		internal void Set(ref OnClientActionRequiredCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.ClientHandle = other.ClientHandle;
			this.ClientAction = other.ClientAction;
			this.ActionReasonCode = other.ActionReasonCode;
			this.ActionReasonDetailsString = other.ActionReasonDetailsString;
		}
	}
}
