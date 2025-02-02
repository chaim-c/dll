using System;
using TaleWorlds.CampaignSystem.LogEntries;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors.CommentBehaviors
{
	// Token: 0x020003F6 RID: 1014
	public class CommentOnPlayerMeetLordBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003EB2 RID: 16050 RVA: 0x00133B8D File Offset: 0x00131D8D
		public override void RegisterEvents()
		{
			CampaignEvents.OnPlayerMetHeroEvent.AddNonSerializedListener(this, new Action<Hero>(this.OnPlayerMetCharacter));
		}

		// Token: 0x06003EB3 RID: 16051 RVA: 0x00133BA6 File Offset: 0x00131DA6
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003EB4 RID: 16052 RVA: 0x00133BA8 File Offset: 0x00131DA8
		private void OnPlayerMetCharacter(Hero hero)
		{
			if (hero.Mother != Hero.MainHero && hero.Father != Hero.MainHero)
			{
				LogEntry.AddLogEntry(new PlayerMeetLordLogEntry(hero));
			}
		}
	}
}
