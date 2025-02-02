using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x02000308 RID: 776
	public struct UpdateModCallbackInfo : ICallbackInfo
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x0001EE9A File Offset: 0x0001D09A
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x0001EEA2 File Offset: 0x0001D0A2
		public Result ResultCode { get; set; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060014E4 RID: 5348 RVA: 0x0001EEAB File Offset: 0x0001D0AB
		// (set) Token: 0x060014E5 RID: 5349 RVA: 0x0001EEB3 File Offset: 0x0001D0B3
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0001EEBC File Offset: 0x0001D0BC
		// (set) Token: 0x060014E7 RID: 5351 RVA: 0x0001EEC4 File Offset: 0x0001D0C4
		public object ClientData { get; set; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060014E8 RID: 5352 RVA: 0x0001EECD File Offset: 0x0001D0CD
		// (set) Token: 0x060014E9 RID: 5353 RVA: 0x0001EED5 File Offset: 0x0001D0D5
		public ModIdentifier? Mod { get; set; }

		// Token: 0x060014EA RID: 5354 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x0001EEFD File Offset: 0x0001D0FD
		internal void Set(ref UpdateModCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
			this.Mod = other.Mod;
		}
	}
}
