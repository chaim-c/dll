using System;
using Helpers;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.GameComponents
{
	// Token: 0x02000123 RID: 291
	public class DefaultPartyTradeModel : PartyTradeModel
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060016D3 RID: 5843 RVA: 0x0006FA79 File Offset: 0x0006DC79
		public override int CaravanTransactionHighestValueItemCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060016D4 RID: 5844 RVA: 0x0006FA7C File Offset: 0x0006DC7C
		public override int SmallCaravanFormingCostForPlayer
		{
			get
			{
				if (CharacterObject.PlayerCharacter.Culture.HasFeat(DefaultCulturalFeats.AseraiTraderFeat))
				{
					return MathF.Round(15000f * DefaultCulturalFeats.AseraiTraderFeat.EffectBonus);
				}
				return 15000;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060016D5 RID: 5845 RVA: 0x0006FAAF File Offset: 0x0006DCAF
		public override int LargeCaravanFormingCostForPlayer
		{
			get
			{
				if (CharacterObject.PlayerCharacter.Culture.HasFeat(DefaultCulturalFeats.AseraiTraderFeat))
				{
					return MathF.Round(22500f * DefaultCulturalFeats.AseraiTraderFeat.EffectBonus);
				}
				return 22500;
			}
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0006FAE4 File Offset: 0x0006DCE4
		public override float GetTradePenaltyFactor(MobileParty party)
		{
			ExplainedNumber explainedNumber = new ExplainedNumber(1f, false, null);
			SkillHelper.AddSkillBonusForParty(DefaultSkills.Trade, DefaultSkillEffects.TradePenaltyReduction, party, ref explainedNumber);
			return 1f / explainedNumber.ResultNumber;
		}
	}
}
