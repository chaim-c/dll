using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CharacterCreationContent
{
	// Token: 0x020001D3 RID: 467
	public class FaceGenMount
	{
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06001C07 RID: 7175 RVA: 0x0007F04E File Offset: 0x0007D24E
		// (set) Token: 0x06001C08 RID: 7176 RVA: 0x0007F056 File Offset: 0x0007D256
		public MountCreationKey MountKey { get; private set; }

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0007F05F File Offset: 0x0007D25F
		// (set) Token: 0x06001C0A RID: 7178 RVA: 0x0007F067 File Offset: 0x0007D267
		public ItemObject HorseItem { get; private set; }

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0007F070 File Offset: 0x0007D270
		// (set) Token: 0x06001C0C RID: 7180 RVA: 0x0007F078 File Offset: 0x0007D278
		public ItemObject HarnessItem { get; private set; }

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001C0D RID: 7181 RVA: 0x0007F081 File Offset: 0x0007D281
		// (set) Token: 0x06001C0E RID: 7182 RVA: 0x0007F089 File Offset: 0x0007D289
		public string ActionName { get; set; }

		// Token: 0x06001C0F RID: 7183 RVA: 0x0007F092 File Offset: 0x0007D292
		public FaceGenMount(MountCreationKey mountKey, ItemObject horseItem, ItemObject harnessItem, string actionName = "act_inventory_idle_start")
		{
			this.MountKey = mountKey;
			this.HorseItem = horseItem;
			this.HarnessItem = harnessItem;
			this.ActionName = actionName;
		}
	}
}
