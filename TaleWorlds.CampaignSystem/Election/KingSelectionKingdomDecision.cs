﻿using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;

namespace TaleWorlds.CampaignSystem.Election
{
	// Token: 0x02000275 RID: 629
	public class KingSelectionKingdomDecision : KingdomDecision
	{
		// Token: 0x0600215B RID: 8539 RVA: 0x0008DE8E File Offset: 0x0008C08E
		internal static void AutoGeneratedStaticCollectObjectsKingSelectionKingdomDecision(object o, List<object> collectedObjects)
		{
			((KingSelectionKingdomDecision)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0008DE9C File Offset: 0x0008C09C
		protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
		{
			base.AutoGeneratedInstanceCollectObjects(collectedObjects);
			collectedObjects.Add(this._clanToExclude);
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0008DEB1 File Offset: 0x0008C0B1
		internal static object AutoGeneratedGetMemberValue_clanToExclude(object o)
		{
			return ((KingSelectionKingdomDecision)o)._clanToExclude;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0008DEBE File Offset: 0x0008C0BE
		public KingSelectionKingdomDecision(Clan proposerClan, Clan clanToExclude = null) : base(proposerClan)
		{
			this._clanToExclude = clanToExclude;
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0008DECE File Offset: 0x0008C0CE
		public override bool IsAllowed()
		{
			return Campaign.Current.Models.KingdomDecisionPermissionModel.IsKingSelectionDecisionAllowed(base.Kingdom);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x0008DEEA File Offset: 0x0008C0EA
		public override int GetProposalInfluenceCost()
		{
			return Campaign.Current.Models.DiplomacyModel.GetInfluenceCostOfProposingWar(base.ProposerClan);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x0008DF06 File Offset: 0x0008C106
		public override TextObject GetGeneralTitle()
		{
			TextObject textObject = new TextObject("{=ZYSGp5vO}King of {KINGDOM_NAME}", null);
			textObject.SetTextVariable("KINGDOM_NAME", base.Kingdom.Name);
			return textObject;
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0008DF2A File Offset: 0x0008C12A
		public override TextObject GetSupportTitle()
		{
			TextObject textObject = new TextObject("{=B0uKPW9S}Vote for the next ruler of {KINGDOM_NAME}", null);
			textObject.SetTextVariable("KINGDOM_NAME", base.Kingdom.Name);
			return textObject;
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x0008DF4E File Offset: 0x0008C14E
		public override TextObject GetChooseTitle()
		{
			TextObject textObject = new TextObject("{=L0Oxzkfw}Choose the next ruler of {KINGDOM_NAME}", null);
			textObject.SetTextVariable("KINGDOM_NAME", base.Kingdom.Name);
			return textObject;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x0008DF72 File Offset: 0x0008C172
		public override TextObject GetSupportDescription()
		{
			TextObject textObject = new TextObject("{=XGuDyJMZ}{KINGDOM_NAME} will decide who will bear the crown as the next ruler. You can pick your stance regarding this decision.", null);
			textObject.SetTextVariable("KINGDOM_NAME", base.Kingdom.Name);
			return textObject;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x0008DF96 File Offset: 0x0008C196
		public override TextObject GetChooseDescription()
		{
			TextObject textObject = new TextObject("{=L0Oxzkfw}Choose the next ruler of {KINGDOM_NAME}", null);
			textObject.SetTextVariable("KINGDOM_NAME", base.Kingdom.Name);
			return textObject;
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x0008DFBA File Offset: 0x0008C1BA
		public override bool IsKingsVoteAllowed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x0008DFBD File Offset: 0x0008C1BD
		protected override bool CanProposerClanChangeOpinion()
		{
			return true;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x0008DFC0 File Offset: 0x0008C1C0
		public override float CalculateMeritOfOutcome(DecisionOutcome candidateOutcome)
		{
			float num = 1f;
			foreach (Clan clan in base.Kingdom.Clans)
			{
				if (clan.Leader != Hero.MainHero)
				{
					num += this.CalculateMeritOfOutcomeForClan(clan, candidateOutcome);
				}
			}
			return num;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x0008E030 File Offset: 0x0008C230
		public float CalculateMeritOfOutcomeForClan(Clan clan, DecisionOutcome candidateOutcome)
		{
			float num = 0f;
			Hero king = ((KingSelectionKingdomDecision.KingSelectionDecisionOutcome)candidateOutcome).King;
			if (king.Clan == base.Kingdom.RulingClan)
			{
				if (clan.Leader.GetTraitLevel(DefaultTraits.Authoritarian) > 0)
				{
					num += 3f;
				}
				else if (clan.Leader.GetTraitLevel(DefaultTraits.Oligarchic) > 0)
				{
					num += 2f;
				}
				else
				{
					num += 1f;
				}
			}
			List<float> list = (from t in base.Kingdom.Clans
			select Campaign.Current.Models.DiplomacyModel.GetClanStrength(t) into t
			orderby t descending
			select t).ToList<float>();
			int num2 = 6;
			float num3 = (float)num2 / (list[0] - list[list.Count - 1]);
			float num4 = (float)num2 / 2f - num3 * list[0];
			float num5 = Campaign.Current.Models.DiplomacyModel.GetClanStrength(king.Clan) * num3 + num4;
			num += num5;
			return MathF.Clamp(num, -3f, 8f);
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0008E161 File Offset: 0x0008C361
		public override IEnumerable<DecisionOutcome> DetermineInitialCandidates()
		{
			Dictionary<Clan, float> dictionary = new Dictionary<Clan, float>();
			foreach (Clan clan in base.Kingdom.Clans)
			{
				if (Campaign.Current.Models.DiplomacyModel.IsClanEligibleToBecomeRuler(clan) && clan != this._clanToExclude)
				{
					dictionary.Add(clan, Campaign.Current.Models.DiplomacyModel.GetClanStrength(clan));
				}
			}
			IEnumerable<KeyValuePair<Clan, float>> enumerable = (from t in dictionary
			orderby t.Value descending
			select t).Take(3);
			foreach (KeyValuePair<Clan, float> keyValuePair in enumerable)
			{
				yield return new KingSelectionKingdomDecision.KingSelectionDecisionOutcome(keyValuePair.Key.Leader);
			}
			IEnumerator<KeyValuePair<Clan, float>> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0008E171 File Offset: 0x0008C371
		public override Clan DetermineChooser()
		{
			return base.Kingdom.RulingClan;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0008E17E File Offset: 0x0008C37E
		public override float DetermineSupport(Clan clan, DecisionOutcome possibleOutcome)
		{
			return this.CalculateMeritOfOutcomeForClan(clan, possibleOutcome) * 10f;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0008E190 File Offset: 0x0008C390
		public override void DetermineSponsors(MBReadOnlyList<DecisionOutcome> possibleOutcomes)
		{
			foreach (DecisionOutcome decisionOutcome in possibleOutcomes)
			{
				decisionOutcome.SetSponsor(((KingSelectionKingdomDecision.KingSelectionDecisionOutcome)decisionOutcome).King.Clan);
			}
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0008E1EC File Offset: 0x0008C3EC
		public override void ApplyChosenOutcome(DecisionOutcome chosenOutcome)
		{
			Hero king = ((KingSelectionKingdomDecision.KingSelectionDecisionOutcome)chosenOutcome).King;
			if (king != king.Clan.Leader)
			{
				ChangeClanLeaderAction.ApplyWithSelectedNewLeader(king.Clan, king);
			}
			ChangeRulingClanAction.Apply(base.Kingdom, king.Clan);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0008E230 File Offset: 0x0008C430
		public override TextObject GetSecondaryEffects()
		{
			return new TextObject("{=!}All supporters gains some relation with each other.", null);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0008E23D File Offset: 0x0008C43D
		public override void ApplySecondaryEffects(MBReadOnlyList<DecisionOutcome> possibleOutcomes, DecisionOutcome chosenOutcome)
		{
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0008E240 File Offset: 0x0008C440
		public override TextObject GetChosenOutcomeText(DecisionOutcome chosenOutcome, KingdomDecision.SupportStatus supportStatus, bool isShortVersion = false)
		{
			TextObject textObject = new TextObject("{=JQligd8z}The council of the {KINGDOM} has chosen {KING.NAME} as the new ruler.", null);
			textObject.SetTextVariable("KINGDOM", base.Kingdom.Name);
			StringHelpers.SetCharacterProperties("KING", ((KingSelectionKingdomDecision.KingSelectionDecisionOutcome)chosenOutcome).King.CharacterObject, textObject, false);
			return textObject;
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0008E28E File Offset: 0x0008C48E
		public override DecisionOutcome GetQueriedDecisionOutcome(MBReadOnlyList<DecisionOutcome> possibleOutcomes)
		{
			return (from k in possibleOutcomes
			orderby k.Merit descending
			select k).FirstOrDefault<DecisionOutcome>();
		}

		// Token: 0x04000A5A RID: 2650
		[SaveableField(1)]
		private Clan _clanToExclude;

		// Token: 0x0200058C RID: 1420
		public class KingSelectionDecisionOutcome : DecisionOutcome
		{
			// Token: 0x06004603 RID: 17923 RVA: 0x0014B088 File Offset: 0x00149288
			internal static void AutoGeneratedStaticCollectObjectsKingSelectionDecisionOutcome(object o, List<object> collectedObjects)
			{
				((KingSelectionKingdomDecision.KingSelectionDecisionOutcome)o).AutoGeneratedInstanceCollectObjects(collectedObjects);
			}

			// Token: 0x06004604 RID: 17924 RVA: 0x0014B096 File Offset: 0x00149296
			protected override void AutoGeneratedInstanceCollectObjects(List<object> collectedObjects)
			{
				base.AutoGeneratedInstanceCollectObjects(collectedObjects);
				collectedObjects.Add(this.King);
			}

			// Token: 0x06004605 RID: 17925 RVA: 0x0014B0AB File Offset: 0x001492AB
			internal static object AutoGeneratedGetMemberValueKing(object o)
			{
				return ((KingSelectionKingdomDecision.KingSelectionDecisionOutcome)o).King;
			}

			// Token: 0x06004606 RID: 17926 RVA: 0x0014B0B8 File Offset: 0x001492B8
			public KingSelectionDecisionOutcome(Hero king)
			{
				this.King = king;
			}

			// Token: 0x06004607 RID: 17927 RVA: 0x0014B0C8 File Offset: 0x001492C8
			public override TextObject GetDecisionTitle()
			{
				TextObject textObject = new TextObject("{=4G3Aeqna}{KING.NAME}", null);
				StringHelpers.SetCharacterProperties("KING", this.King.CharacterObject, textObject, false);
				return textObject;
			}

			// Token: 0x06004608 RID: 17928 RVA: 0x0014B0FC File Offset: 0x001492FC
			public override TextObject GetDecisionDescription()
			{
				TextObject textObject = new TextObject("{=FTjKWm8s}{KING.NAME} should rule us", null);
				StringHelpers.SetCharacterProperties("KING", this.King.CharacterObject, textObject, false);
				return textObject;
			}

			// Token: 0x06004609 RID: 17929 RVA: 0x0014B12E File Offset: 0x0014932E
			public override string GetDecisionLink()
			{
				return null;
			}

			// Token: 0x0600460A RID: 17930 RVA: 0x0014B131 File Offset: 0x00149331
			public override ImageIdentifier GetDecisionImageIdentifier()
			{
				return null;
			}

			// Token: 0x04001728 RID: 5928
			[SaveableField(100)]
			public readonly Hero King;
		}
	}
}
