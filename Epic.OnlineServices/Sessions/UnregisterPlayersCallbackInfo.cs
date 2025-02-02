using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200015C RID: 348
	public struct UnregisterPlayersCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0000EF6D File Offset: 0x0000D16D
		// (set) Token: 0x06000A04 RID: 2564 RVA: 0x0000EF75 File Offset: 0x0000D175
		public Result ResultCode { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0000EF7E File Offset: 0x0000D17E
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x0000EF86 File Offset: 0x0000D186
		public object ClientData { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x0000EF8F File Offset: 0x0000D18F
		// (set) Token: 0x06000A08 RID: 2568 RVA: 0x0000EF97 File Offset: 0x0000D197
		public ProductUserId[] UnregisteredPlayers { get; set; }

		// Token: 0x06000A09 RID: 2569 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0000EFBD File Offset: 0x0000D1BD
		internal void Set(ref UnregisterPlayersCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.UnregisteredPlayers = other.UnregisteredPlayers;
		}
	}
}
