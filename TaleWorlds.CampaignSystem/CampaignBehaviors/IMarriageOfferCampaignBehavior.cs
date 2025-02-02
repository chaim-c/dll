﻿using System;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200039E RID: 926
	public interface IMarriageOfferCampaignBehavior : ICampaignBehavior
	{
		// Token: 0x06003795 RID: 14229
		void OnMarriageOfferedToPlayer(Hero suitor, Hero maiden);

		// Token: 0x06003796 RID: 14230
		void OnMarriageOfferCanceled(Hero suitor, Hero maiden);

		// Token: 0x06003797 RID: 14231
		MBBindingList<TextObject> GetMarriageAcceptedConsequences();

		// Token: 0x06003798 RID: 14232
		void OnMarriageOfferAcceptedOnPopUp();

		// Token: 0x06003799 RID: 14233
		void OnMarriageOfferDeclinedOnPopUp();
	}
}
