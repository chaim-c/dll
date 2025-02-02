using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x0200002B RID: 43
	public class PlayerToggledUpgradePopupEvent : EventBase
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00018D8E File Offset: 0x00016F8E
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00018D96 File Offset: 0x00016F96
		public bool IsOpened { get; private set; }

		// Token: 0x06000459 RID: 1113 RVA: 0x00018D9F File Offset: 0x00016F9F
		public PlayerToggledUpgradePopupEvent(bool isOpened)
		{
			this.IsOpened = isOpened;
		}
	}
}
