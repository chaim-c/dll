using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000627 RID: 1575
	public struct OnClientIntegrityViolatedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x0003BFFD File Offset: 0x0003A1FD
		// (set) Token: 0x0600282C RID: 10284 RVA: 0x0003C005 File Offset: 0x0003A205
		public object ClientData { get; set; }

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x0003C00E File Offset: 0x0003A20E
		// (set) Token: 0x0600282E RID: 10286 RVA: 0x0003C016 File Offset: 0x0003A216
		public AntiCheatClientViolationType ViolationType { get; set; }

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x0003C01F File Offset: 0x0003A21F
		// (set) Token: 0x06002830 RID: 10288 RVA: 0x0003C027 File Offset: 0x0003A227
		public Utf8String ViolationMessage { get; set; }

		// Token: 0x06002831 RID: 10289 RVA: 0x0003C030 File Offset: 0x0003A230
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06002832 RID: 10290 RVA: 0x0003C04B File Offset: 0x0003A24B
		internal void Set(ref OnClientIntegrityViolatedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.ViolationType = other.ViolationType;
			this.ViolationMessage = other.ViolationMessage;
		}
	}
}
