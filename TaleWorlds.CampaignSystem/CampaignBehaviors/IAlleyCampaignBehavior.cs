using System;
using System.Collections.Generic;
using TaleWorlds.CampaignSystem.Settlements;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x02000397 RID: 919
	public interface IAlleyCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x0600376C RID: 14188
		bool GetIsAlleyUnderAttack(Alley alley);

		// Token: 0x0600376D RID: 14189
		int GetPlayerOwnedAlleyTroopCount(Alley alley);

		// Token: 0x0600376E RID: 14190
		int GetResponseTimeLeftForAttackInDays(Alley alley);

		// Token: 0x0600376F RID: 14191
		void AbandonAlleyFromClanMenu(Alley alley);

		// Token: 0x06003770 RID: 14192
		Hero GetAssignedClanMemberOfAlley(Alley alley);

		// Token: 0x06003771 RID: 14193
		bool IsHeroAlleyLeaderOfAnyPlayerAlley(Hero hero);

		// Token: 0x06003772 RID: 14194
		List<Hero> GetAllAssignedClanMembersForOwnedAlleys();

		// Token: 0x06003773 RID: 14195
		void ChangeAlleyMember(Alley alley, Hero newAlleyLead);

		// Token: 0x06003774 RID: 14196
		void OnPlayerRetreatedFromMission();

		// Token: 0x06003775 RID: 14197
		void OnPlayerDiedInMission();
	}
}
