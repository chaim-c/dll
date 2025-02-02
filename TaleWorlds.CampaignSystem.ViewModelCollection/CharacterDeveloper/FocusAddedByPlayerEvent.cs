using System;
using TaleWorlds.Core;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.CharacterDeveloper
{
	// Token: 0x02000127 RID: 295
	public class FocusAddedByPlayerEvent : EventBase
	{
		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06001D33 RID: 7475 RVA: 0x00068F81 File Offset: 0x00067181
		// (set) Token: 0x06001D34 RID: 7476 RVA: 0x00068F89 File Offset: 0x00067189
		public Hero AddedPlayer { get; private set; }

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00068F92 File Offset: 0x00067192
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x00068F9A File Offset: 0x0006719A
		public SkillObject AddedSkill { get; private set; }

		// Token: 0x06001D37 RID: 7479 RVA: 0x00068FA3 File Offset: 0x000671A3
		public FocusAddedByPlayerEvent(Hero addedPlayer, SkillObject addedSkill)
		{
			this.AddedPlayer = addedPlayer;
			this.AddedSkill = addedSkill;
		}
	}
}
