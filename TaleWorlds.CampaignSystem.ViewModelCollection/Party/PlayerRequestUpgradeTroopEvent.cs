using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x0200002A RID: 42
	public class PlayerRequestUpgradeTroopEvent : EventBase
	{
		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00018D3E File Offset: 0x00016F3E
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x00018D46 File Offset: 0x00016F46
		public CharacterObject SourceTroop { get; private set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x00018D4F File Offset: 0x00016F4F
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x00018D57 File Offset: 0x00016F57
		public CharacterObject TargetTroop { get; private set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x00018D60 File Offset: 0x00016F60
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x00018D68 File Offset: 0x00016F68
		public int Number { get; private set; }

		// Token: 0x06000456 RID: 1110 RVA: 0x00018D71 File Offset: 0x00016F71
		public PlayerRequestUpgradeTroopEvent(CharacterObject sourceTroop, CharacterObject targetTroop, int num)
		{
			this.SourceTroop = sourceTroop;
			this.TargetTroop = targetTroop;
			this.Number = num;
		}
	}
}
