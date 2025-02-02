using System;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F8 RID: 1528
	public struct LogPlayerUseAbilityOptions
	{
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x0003A48D File Offset: 0x0003868D
		// (set) Token: 0x0600271A RID: 10010 RVA: 0x0003A495 File Offset: 0x00038695
		public IntPtr PlayerHandle { get; set; }

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x0003A49E File Offset: 0x0003869E
		// (set) Token: 0x0600271C RID: 10012 RVA: 0x0003A4A6 File Offset: 0x000386A6
		public uint AbilityId { get; set; }

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x0003A4AF File Offset: 0x000386AF
		// (set) Token: 0x0600271E RID: 10014 RVA: 0x0003A4B7 File Offset: 0x000386B7
		public uint AbilityDurationMs { get; set; }

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x0003A4C0 File Offset: 0x000386C0
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x0003A4C8 File Offset: 0x000386C8
		public uint AbilityCooldownMs { get; set; }
	}
}
