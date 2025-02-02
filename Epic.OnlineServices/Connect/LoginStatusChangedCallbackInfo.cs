using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200053E RID: 1342
	public struct LoginStatusChangedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x00033826 File Offset: 0x00031A26
		// (set) Token: 0x06002272 RID: 8818 RVA: 0x0003382E File Offset: 0x00031A2E
		public object ClientData { get; set; }

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06002273 RID: 8819 RVA: 0x00033837 File Offset: 0x00031A37
		// (set) Token: 0x06002274 RID: 8820 RVA: 0x0003383F File Offset: 0x00031A3F
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06002275 RID: 8821 RVA: 0x00033848 File Offset: 0x00031A48
		// (set) Token: 0x06002276 RID: 8822 RVA: 0x00033850 File Offset: 0x00031A50
		public LoginStatus PreviousStatus { get; set; }

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002277 RID: 8823 RVA: 0x00033859 File Offset: 0x00031A59
		// (set) Token: 0x06002278 RID: 8824 RVA: 0x00033861 File Offset: 0x00031A61
		public LoginStatus CurrentStatus { get; set; }

		// Token: 0x06002279 RID: 8825 RVA: 0x0003386C File Offset: 0x00031A6C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600227A RID: 8826 RVA: 0x00033887 File Offset: 0x00031A87
		internal void Set(ref LoginStatusChangedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PreviousStatus = other.PreviousStatus;
			this.CurrentStatus = other.CurrentStatus;
		}
	}
}
