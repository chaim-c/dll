using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000111 RID: 273
	public struct RegisterPlayersCallbackInfo : ICallbackInfo
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0000BF01 File Offset: 0x0000A101
		// (set) Token: 0x06000853 RID: 2131 RVA: 0x0000BF09 File Offset: 0x0000A109
		public Result ResultCode { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x0000BF12 File Offset: 0x0000A112
		// (set) Token: 0x06000855 RID: 2133 RVA: 0x0000BF1A File Offset: 0x0000A11A
		public object ClientData { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0000BF23 File Offset: 0x0000A123
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0000BF2B File Offset: 0x0000A12B
		public ProductUserId[] RegisteredPlayers { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0000BF34 File Offset: 0x0000A134
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0000BF3C File Offset: 0x0000A13C
		public ProductUserId[] SanctionedPlayers { get; set; }

		// Token: 0x0600085A RID: 2138 RVA: 0x0000BF48 File Offset: 0x0000A148
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0000BF65 File Offset: 0x0000A165
		internal void Set(ref RegisterPlayersCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.RegisteredPlayers = other.RegisteredPlayers;
			this.SanctionedPlayers = other.SanctionedPlayers;
		}
	}
}
