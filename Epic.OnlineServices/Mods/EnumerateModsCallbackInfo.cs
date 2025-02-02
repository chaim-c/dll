using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002EE RID: 750
	public struct EnumerateModsCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0001DE92 File Offset: 0x0001C092
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x0001DE9A File Offset: 0x0001C09A
		public Result ResultCode { get; set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0001DEA3 File Offset: 0x0001C0A3
		// (set) Token: 0x06001431 RID: 5169 RVA: 0x0001DEAB File Offset: 0x0001C0AB
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x0001DEBC File Offset: 0x0001C0BC
		public object ClientData { get; set; }

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0001DEC5 File Offset: 0x0001C0C5
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x0001DECD File Offset: 0x0001C0CD
		public ModEnumerationType Type { get; set; }

		// Token: 0x06001436 RID: 5174 RVA: 0x0001DED8 File Offset: 0x0001C0D8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0001DEF5 File Offset: 0x0001C0F5
		internal void Set(ref EnumerateModsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
			this.Type = other.Type;
		}
	}
}
