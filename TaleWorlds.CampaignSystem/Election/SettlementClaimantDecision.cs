﻿using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Election
{
	// Token: 0x02000270 RID: 624
	public class SettlementClaimantDecision : KingdomDecision
	{
		// Token: 0x060020EC RID: 8428 RVA: 0x0008C33C File Offset: 0x0008A53C
		internal static void AutoGeneratedStaticCollectObjectsSettlementClaimantDecision(object o, List<object> collectedObjects)
		{
			((SettlementClaimantDecision)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0008C34A File Offset: 0x0008A54A
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this.Settlement);
			collectedObjects.Add(this.ClanToExclude);
			collectedObjects.Add(this._capturerHero);
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0008C377 File Offset: 0x0008A577
		internal static object AutoGeneratedGetMemberValueSettlement(object o)
		{
			return ((SettlementClaimantDecision)o).Settlement;
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0008C384 File Offset: 0x0008A584
		internal static object AutoGeneratedGetMemberValueClanToExclude(object o)
		{
			return ((SettlementClaimantDecision)o).ClanToExclude;
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0008C391 File Offset: 0x0008A591
		internal static object AutoGeneratedGetMemberValue_capturerHero(object o)
		{
			return ((SettlementClaimantDecision)o)._capturerHero;
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0008C39E File Offset: 0x0008A59E
		public SettlementClaimantDecision(Clan proposerClan, Settlement settlement, Hero capturerHero, Clan clanToExclude) : base(proposerClan)
		{
			this.Settlement = settlement;
			this._capturerHero = capturerHero;
			this.ClanToExclude = clanToExclude;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0008C3BD File Offset: 0x0008A5BD
		public override bool IsAllowed()
		{
			return Campaign.Current.Models.KingdomDecisionPermissionModel.IsAnnexationDecisionAllowed(this.Settlement);
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0008C3D9 File Offset: 0x0008A5D9
		public override int GetProposalInfluenceCost()
		{
			return Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfAnnexation(base.ProposerClan);
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0008C3F5 File Offset: 0x0008A5F5
		public override TextObject GetSupportTitle()
		{
			TextObject textObject = new TextObject("{=Of7XnP5c}Vote for the new owner of {SETTLEMENT_NAME}", null);
			textObject.SetTextVariable("SETTLEMENT_NAME", this.Settlement.Name);
			return textObject;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0008C419 File Offset: 0x0008A619
		public override TextObject GetGeneralTitle()
		{
			TextObject textObject = new TextObject("{=2qZ81jPG}Owner of {SETTLEMENT_NAME}", null);
			textObject.SetTextVariable("SETTLEMENT_NAME", this.Settlement.Name);
			return textObject;
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0008C43D File Offset: 0x0008A63D
		public override TextObject GetSupportDescription()
		{
			TextObject textObject = new TextObject("{=J4UMplzb}{FACTION_LEADER} will decide who will own {SETTLEMENT_NAME}. You can give your support to one of the candidates.", null);
			textObject.SetTextVariable("FACTION_LEADER", this.DetermineChooser().Leader.Name);
			textObject.SetTextVariable("SETTLEMENT_NAME", this.Settlement.Name);
			return textObject;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0008C47D File Offset: 0x0008A67D
		public override TextObject GetChooseTitle()
		{
			TextObject textObject = new TextObject("{=2qZ81jPG}Owner of {SETTLEMENT_NAME}", null);
			textObject.SetTextVariable("SETTLEMENT_NAME", this.Settlement.Name);
			return textObject;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0008C4A4 File Offset: 0x0008A6A4
		public override TextObject GetChooseDescription()
		{
			TextObject textObject = new TextObject("{=xzq78nVm}As {?IS_FEMALE}queen{?}king{\\?} you must decide who will own {SETTLEMENT_NAME}.", null);
			textObject.SetTextVariable("IS_FEMALE", this.DetermineChooser().Leader.IsFemale ? 1 : 0);
			textObject.SetTextVariable("SETTLEMENT_NAME", this.Settlement.Name);
			return textObject;
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0008C4F5 File Offset: 0x0008A6F5
		protected override bool ShouldBeCancelledInternal()
		{
			return this.Settlement.MapFaction != base.Kingdom;
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0008C50D File Offset: 0x0008A70D
		protected override bool CanProposerClanChangeOpinion()
		{
			return true;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0008C510 File Offset: 0x0008A710
		public override float CalculateMeritOfOutcome(DecisionOutcome candidateOutcome)
		{
			SettlementClaimantDecision.ClanAsDecisionOutcome clanAsDecisionOutcome = (SettlementClaimantDecision.ClanAsDecisionOutcome)candidateOutcome;
			Clan clan = clanAsDecisionOutcome.Clan;
			float num = 0f;
			int num2 = 0;
			float num3 = Campaign.MapDiagonal + 1f;
			float num4 = Campaign.MapDiagonal + 1f;
			foreach (Settlement settlement in Settlement.All)
			{
				if (settlement.OwnerClan == clanAsDecisionOutcome.Clan && settlement.IsFortification && this.Settlement != settlement)
				{
					num += settlement.GetSettlementValueForFaction(clanAsDecisionOutcome.Clan.Kingdom);
					float num5;
					if (Campaign.Current.Models.MapDistanceModel.GetDistance(settlement, this.Settlement, num4, out num5))
					{
						if (num5 < num3)
						{
							num4 = num3;
							num3 = num5;
						}
						else if (num5 < num4)
						{
							num4 = num5;
						}
					}
					num2++;
				}
			}
			float num6 = Campaign.AverageDistanceBetweenTwoFortifications * 1.5f;
			float a = num6 * 0.25f;
			float b = num6;
			if (num4 < Campaign.MapDiagonal)
			{
				b = (num4 + num3) / 2f;
			}
			else if (num3 < Campaign.MapDiagonal)
			{
				b = num3;
			}
			float num7 = MathF.Pow(num6 / MathF.Max(a, MathF.Min(400f, b)), 0.5f);
			float num8 = clan.TotalStrength;
			if (this.Settlement.OwnerClan == clan && this.Settlement.Town != null && this.Settlement.Town.GarrisonParty != null)
			{
				num8 -= this.Settlement.Town.GarrisonParty.Party.TotalStrength;
				if (num8 < 0f)
				{
					num8 = 0f;
				}
			}
			float settlementValueForFaction = this.Settlement.GetSettlementValueForFaction(clanAsDecisionOutcome.Clan.Kingdom);
			bool flag = clanAsDecisionOutcome.Clan.Leader == clanAsDecisionOutcome.Clan.Kingdom.Leader;
			float num9 = (num2 == 0) ? 30f : 0f;
			float num10 = flag ? 60f : 0f;
			float num11 = (this.Settlement.Town != null && this.Settlement.Town.LastCapturedBy == clanAsDecisionOutcome.Clan) ? 30f : 0f;
			float num12 = (clanAsDecisionOutcome.Clan.Leader == Hero.MainHero) ? 30f : 0f;
			float num13 = (clanAsDecisionOutcome.Clan.Leader.Gold < 30000) ? MathF.Min(30f, 30f - (float)clanAsDecisionOutcome.Clan.Leader.Gold / 1000f) : 0f;
			return ((float)clan.Tier * 30f + num8 / 10f + num9 + num11 + num10 + num13 + num12) / (num + settlementValueForFaction) * num7 * 200000f;
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0008C7F0 File Offset: 0x0008A9F0
		public override IEnumerable<DecisionOutcome> DetermineInitialCandidates()
		{
			Kingdom kingdom = (Kingdom)this.Settlement.MapFaction;
			List<SettlementClaimantDecision.ClanAsDecisionOutcome> list = new List<SettlementClaimantDecision.ClanAsDecisionOutcome>();
			foreach (Clan clan in kingdom.Clans)
			{
				if (clan != this.ClanToExclude && !clan.IsUnderMercenaryService && !clan.IsEliminated && !clan.Leader.IsDead)
				{
					list.Add(new SettlementClaimantDecision.ClanAsDecisionOutcome(clan));
				}
			}
			return list;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0008C884 File Offset: 0x0008AA84
		public override Clan DetermineChooser()
		{
			return ((Kingdom)this.Settlement.MapFaction).RulingClan;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x0008C89C File Offset: 0x0008AA9C
		public override float DetermineSupport(Clan clan, DecisionOutcome possibleOutcome)
		{
			SettlementClaimantDecision.ClanAsDecisionOutcome clanAsDecisionOutcome = (SettlementClaimantDecision.ClanAsDecisionOutcome)possibleOutcome;
			float num = clanAsDecisionOutcome.InitialMerit;
			int traitLevel = clan.Leader.GetTraitLevel(DefaultTraits.Honor);
			num *= MathF.Clamp(1f + (float)traitLevel, 0f, 2f);
			if (clanAsDecisionOutcome.Clan == clan)
			{
				float settlementValueForFaction = this.Settlement.GetSettlementValueForFaction(clan);
				num += 0.2f * settlementValueForFaction * Campaign.Current.Models.DiplomacyModel.DenarsToInfluence();
			}
			else
			{
				float num2 = (clanAsDecisionOutcome.Clan != clan) ? ((float)FactionManager.GetRelationBetweenClans(clanAsDecisionOutcome.Clan, clan)) : 100f;
				int traitLevel2 = clan.Leader.GetTraitLevel(DefaultTraits.Calculating);
				num *= MathF.Clamp(1f + (float)traitLevel2, 0f, 2f);
				float num3 = num2 * 0.2f * (float)traitLevel2;
				num += num3;
			}
			int traitLevel3 = clan.Leader.GetTraitLevel(DefaultTraits.Calculating);
			float num4 = (traitLevel3 > 0) ? (0.4f - (float)MathF.Min(2, traitLevel3) * 0.1f) : (0.4f + (float)MathF.Min(2, MathF.Abs(traitLevel3)) * 0.1f);
			float num5 = 1f - num4 * 1.5f;
			num *= num5;
			float num6 = (clan == clanAsDecisionOutcome.Clan) ? 2f : 1f;
			return num * num6;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x0008C9F4 File Offset: 0x0008ABF4
		public override void DetermineSponsors(MBReadOnlyList<DecisionOutcome> possibleOutcomes)
		{
			foreach (DecisionOutcome decisionOutcome in possibleOutcomes)
			{
				decisionOutcome.SetSponsor(((SettlementClaimantDecision.ClanAsDecisionOutcome)decisionOutcome).Clan);
			}
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0008CA4C File Offset: 0x0008AC4C
		public override void ApplyChosenOutcome(DecisionOutcome chosenOutcome)
		{
			ChangeOwnerOfSettlementAction.ApplyByKingDecision(((SettlementClaimantDecision.ClanAsDecisionOutcome)chosenOutcome).Clan.Leader, this.Settlement);
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0008CA69 File Offset: 0x0008AC69
		protected override int GetInfluenceCostOfSupportInternal(Supporter.SupportWeights supportWeight)
		{
			switch (supportWeight)
			{
			case Supporter.SupportWeights.Choose:
			case Supporter.SupportWeights.StayNeutral:
				return 0;
			case Supporter.SupportWeights.SlightlyFavor:
				return 20;
			case Supporter.SupportWeights.StronglyFavor:
				return 60;
			case Supporter.SupportWeights.FullyPush:
				return 100;
			default:
				throw new ArgumentOutOfRangeException("supportWeight", supportWeight, null);
			}
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0008CAA3 File Offset: 0x0008ACA3
		public override TextObject GetSecondaryEffects()
		{
			return new TextObject("{=bHNU9uz2}All supporters gains some relation with the supported candidate clan and might lose with the others.", null);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0008CAB0 File Offset: 0x0008ACB0
		public override void ApplySecondaryEffects(MBReadOnlyList<DecisionOutcome> possibleOutcomes, DecisionOutcome chosenOutcome)
		{
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0008CAB4 File Offset: 0x0008ACB4
		public override TextObject GetChosenOutcomeText(DecisionOutcome chosenOutcome, KingdomDecision.SupportStatus supportStatus, bool isShortVersion = false)
		{
			TextObject textObject = TextObject.Empty;
			bool flag = ((SettlementClaimantDecision.ClanAsDecisionOutcome)chosenOutcome).Clan.Leader == this.Settlement.MapFaction.Leader;
			if (supportStatus == KingdomDecision.SupportStatus.Majority && flag)
			{
				textObject = new TextObject("{=Zckbdm4Z}{RULER.NAME} of the {KINGDOM} takes {SETTLEMENT} as {?RULER.GENDER}her{?}his{\\?} fief with {?RULER.GENDER}her{?}his{\\?} council's support.", null);
			}
			else if (supportStatus == KingdomDecision.SupportStatus.Minority && flag)
			{
				textObject = new TextObject("{=qa4FlTWS}{RULER.NAME} of the {KINGDOM} takes {SETTLEMENT} as {?RULER.GENDER}her{?}his{\\?} fief despite {?RULER.GENDER}her{?}his{\\?} council's opposition.", null);
			}
			else if (flag)
			{
				textObject = new TextObject("{=5bBAOHmC}{RULER.NAME} of the {KINGDOM} takes {SETTLEMENT} as {?RULER.GENDER}her{?}his{\\?} fief, with {?RULER.GENDER}her{?}his{\\?} council evenly split.", null);
			}
			else if (supportStatus == KingdomDecision.SupportStatus.Majority)
			{
				textObject = new TextObject("{=0nhqJewP}{RULER.NAME} of the {KINGDOM} grants {SETTLEMENT} to {LEADER.NAME} with {?RULER.GENDER}her{?}his{\\?} council's support.", null);
			}
			else if (supportStatus == KingdomDecision.SupportStatus.Minority)
			{
				textObject = new TextObject("{=Ktpia7Pa}{RULER.NAME} of the {KINGDOM} grants {SETTLEMENT} to {LEADER.NAME} despite {?RULER.GENDER}her{?}his{\\?} council's opposition.", null);
			}
			else
			{
				textObject = new TextObject("{=l5H9x7Lo}{RULER.NAME} of the {KINGDOM} grants {SETTLEMENT} to {LEADER.NAME}, with {?RULER.GENDER}her{?}his{\\?} council evenly split.", null);
			}
			textObject.SetTextVariable("SETTLEMENT", this.Settlement.Name);
			StringHelpers.SetCharacterProperties("LEADER", ((SettlementClaimantDecision.ClanAsDecisionOutcome)chosenOutcome).Clan.Leader.CharacterObject, textObject, false);
			StringHelpers.SetCharacterProperties("RULER", this.Settlement.MapFaction.Leader.CharacterObject, textObject, false);
			textObject.SetTextVariable("KINGDOM", this.Settlement.MapFaction.InformalName);
			textObject.SetTextVariable("CLAN", ((SettlementClaimantDecision.ClanAsDecisionOutcome)chosenOutcome).Clan.Name);
			return textObject;
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0008CBEB File Offset: 0x0008ADEB
		public override DecisionOutcome GetQueriedDecisionOutcome(MBReadOnlyList<DecisionOutcome> possibleOutcomes)
		{
			return (from t in possibleOutcomes
			orderby t.Merit descending
			select t).FirstOrDefault<DecisionOutcome>();
		}

		// Token: 0x04000A4E RID: 2638
		[SaveableField(300)]
		public readonly Settlement Settlement;

		// Token: 0x04000A4F RID: 2639
		[SaveableField(301)]
		public readonly Clan ClanToExclude;

		// Token: 0x04000A50 RID: 2640
		[SaveableField(302)]
		private readonly Hero _capturerHero;

		// Token: 0x02000580 RID: 1408
		public class ClanAsDecisionOutcome : DecisionOutcome
		{
			// Token: 0x060045BB RID: 17851 RVA: 0x0014A96C File Offset: 0x00148B6C
			internal static void AutoGeneratedStaticCollectObjectsClanAsDecisionOutcome(object o, List<object> collectedObjects)
			{
				((SettlementClaimantDecision.ClanAsDecisionOutcome)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x060045BC RID: 17852 RVA: 0x0014A97A File Offset: 0x00148B7A
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this.Clan);
			}

			// Token: 0x060045BD RID: 17853 RVA: 0x0014A98F File Offset: 0x00148B8F
			internal static object AutoGeneratedGetMemberValueClan(object o)
			{
				return ((SettlementClaimantDecision.ClanAsDecisionOutcome)o).Clan;
			}

			// Token: 0x060045BE RID: 17854 RVA: 0x0014A99C File Offset: 0x00148B9C
			public override TextObject GetDecisionTitle()
			{
				return this.Clan.Leader.Name;
			}

			// Token: 0x060045BF RID: 17855 RVA: 0x0014A9B0 File Offset: 0x00148BB0
			public override TextObject GetDecisionDescription()
			{
				TextObject textObject = new TextObject("{=QKIxepj5}The lordship of this fief should go to the {RECIPIENT.CLAN}", null);
				StringHelpers.SetCharacterProperties("RECIPIENT", this.Clan.Leader.CharacterObject, textObject, true);
				return textObject;
			}

			// Token: 0x060045C0 RID: 17856 RVA: 0x0014A9E7 File Offset: 0x00148BE7
			public override string GetDecisionLink()
			{
				return this.Clan.Leader.EncyclopediaLink.ToString();
			}

			// Token: 0x060045C1 RID: 17857 RVA: 0x0014A9FE File Offset: 0x00148BFE
			public override ImageIdentifier GetDecisionImageIdentifier()
			{
				return new ImageIdentifier(CharacterCode.CreateFrom(this.Clan.Leader.CharacterObject));
			}

			// Token: 0x060045C2 RID: 17858 RVA: 0x0014AA1A File Offset: 0x00148C1A
			public ClanAsDecisionOutcome(Clan clan)
			{
				this.Clan = clan;
			}

			// Token: 0x04001707 RID: 5895
			[SaveableField(300)]
			public readonly Clan Clan;
		}
	}
}
