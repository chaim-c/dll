using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003E7 RID: 999
	public class CommentCharacterBornBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003E75 RID: 15989 RVA: 0x001334F3 File Offset: 0x001316F3
		public override void RegisterEvents()
		{
			CampaignEvents.HeroCreated.AddNonSerializedListener(this, new Action<Hero, bool>(this.HeroCreated));
		}

		// Token: 0x06003E76 RID: 15990 RVA: 0x0013350C File Offset: 0x0013170C
		private void HeroCreated(Hero hero, bool isBornNaturally)
		{
			if (isBornNaturally)
			{
				LogEntry.AddLogEntry(new CharacterBornLogEntry(hero));
			}
		}

		// Token: 0x06003E77 RID: 15991 RVA: 0x0013351C File Offset: 0x0013171C
		public override void SyncData(IDataStore dataStore)
		{
		}
	}
}
