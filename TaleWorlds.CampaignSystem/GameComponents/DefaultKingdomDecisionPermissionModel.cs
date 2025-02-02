using System;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000111 RID: 273
	public class DefaultKingdomDecisionPermissionModel : KingdomDecisionPermissionModel
	{
		// Token: 0x06001603 RID: 5635 RVA: 0x000690EB File Offset: 0x000672EB
		public override bool IsPolicyDecisionAllowed(PolicyObject policy)
		{
			return true;
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x000690EE File Offset: 0x000672EE
		public override bool IsWarDecisionAllowedBetweenKingdoms(Kingdom kingdom1, Kingdom kingdom2, out TextObject reason)
		{
			reason = TextObject.Empty;
			return true;
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x000690F8 File Offset: 0x000672F8
		public override bool IsPeaceDecisionAllowedBetweenKingdoms(Kingdom kingdom1, Kingdom kingdom2, out TextObject reason)
		{
			reason = TextObject.Empty;
			return true;
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00069102 File Offset: 0x00067302
		public override bool IsAnnexationDecisionAllowed(Settlement annexedSettlement)
		{
			return true;
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00069105 File Offset: 0x00067305
		public override bool IsExpulsionDecisionAllowed(Clan expelledClan)
		{
			return true;
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00069108 File Offset: 0x00067308
		public override bool IsKingSelectionDecisionAllowed(Kingdom kingdom)
		{
			return true;
		}
	}
}
