using System;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003A1 RID: 929
	public interface IRetrainOutlawPartyMembersCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x060037A2 RID: 14242
		int GetRetrainedNumber(CharacterObject character);

		// Token: 0x060037A3 RID: 14243
		void SetRetrainedNumber(CharacterObject character, int number);
	}
}
