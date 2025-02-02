using System;

namespace TaleWorlds.CampaignSystem.ViewModelCollection
{
	// Token: 0x02000010 RID: 16
	public struct CampaignOptionDisableStatus
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000405A File Offset: 0x0000225A
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004062 File Offset: 0x00002262
		public bool IsDisabled { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000406B File Offset: 0x0000226B
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004073 File Offset: 0x00002273
		public string DisabledReason { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000407C File Offset: 0x0000227C
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004084 File Offset: 0x00002284
		public float ValueIfDisabled { get; private set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x0000408D File Offset: 0x0000228D
		public CampaignOptionDisableStatus(bool isDisabled, string disabledReason, float valueIfDisabled = -1f)
		{
			this.IsDisabled = isDisabled;
			this.DisabledReason = disabledReason;
			this.ValueIfDisabled = valueIfDisabled;
		}
	}
}
