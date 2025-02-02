using System;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000440 RID: 1088
	public static class EndCaptivityAction
	{
		// Token: 0x060040AF RID: 16559 RVA: 0x0013ECB4 File Offset: 0x0013CEB4
		private static void ApplyInternal(Hero prisoner, EndCaptivityDetail detail, Hero facilitatior = null)
		{
			PartyBase partyBelongedToAsPrisoner = prisoner.PartyBelongedToAsPrisoner;
			IFaction capturerFaction = (partyBelongedToAsPrisoner != null) ? partyBelongedToAsPrisoner.MapFaction : null;
			if (prisoner == Hero.MainHero)
			{
				PlayerCaptivity.EndCaptivity();
				if (facilitatior != null && detail != EndCaptivityDetail.Death)
				{
					StringHelpers.SetCharacterProperties("FACILITATOR", facilitatior.CharacterObject, null, false);
					MBInformationManager.AddQuickInformation(new TextObject("{=xPuSASof}{FACILITATOR.NAME} paid a ransom and freed you from captivity.", null), 0, null, "");
				}
				CampaignEventDispatcher.Instance.OnHeroPrisonerReleased(prisoner, partyBelongedToAsPrisoner, capturerFaction, detail);
				return;
			}
			if (detail == EndCaptivityDetail.Death)
			{
				prisoner.StayingInSettlement = null;
			}
			if (partyBelongedToAsPrisoner != null && partyBelongedToAsPrisoner.PrisonRoster.Contains(prisoner.CharacterObject))
			{
				partyBelongedToAsPrisoner.PrisonRoster.RemoveTroop(prisoner.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
			}
			if (detail != EndCaptivityDetail.Death)
			{
				if (detail <= EndCaptivityDetail.ReleasedByChoice || detail == EndCaptivityDetail.ReleasedByCompensation)
				{
					prisoner.ChangeState(Hero.CharacterStates.Released);
					if (prisoner.IsPlayerCompanion && detail != EndCaptivityDetail.Ransom)
					{
						MakeHeroFugitiveAction.Apply(prisoner);
					}
				}
				else
				{
					MakeHeroFugitiveAction.Apply(prisoner);
				}
				Settlement currentSettlement = prisoner.CurrentSettlement;
				if (currentSettlement != null)
				{
					currentSettlement.AddHeroWithoutParty(prisoner);
				}
				CampaignEventDispatcher.Instance.OnHeroPrisonerReleased(prisoner, partyBelongedToAsPrisoner, capturerFaction, detail);
			}
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x0013EDAB File Offset: 0x0013CFAB
		public static void ApplyByReleasedAfterBattle(Hero character)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedAfterBattle, null);
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x0013EDB5 File Offset: 0x0013CFB5
		public static void ApplyByRansom(Hero character, Hero facilitator)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.Ransom, facilitator);
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x0013EDBF File Offset: 0x0013CFBF
		public static void ApplyByPeace(Hero character, Hero facilitator = null)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedAfterPeace, facilitator);
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x0013EDC9 File Offset: 0x0013CFC9
		public static void ApplyByEscape(Hero character, Hero facilitator = null)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedAfterEscape, facilitator);
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x0013EDD3 File Offset: 0x0013CFD3
		public static void ApplyByDeath(Hero character)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.Death, null);
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x0013EDE0 File Offset: 0x0013CFE0
		public static void ApplyByReleasedByChoice(FlattenedTroopRoster troopRoster)
		{
			foreach (FlattenedTroopRosterElement flattenedTroopRosterElement in troopRoster)
			{
				if (flattenedTroopRosterElement.Troop.IsHero)
				{
					EndCaptivityAction.ApplyInternal(flattenedTroopRosterElement.Troop.HeroObject, EndCaptivityDetail.ReleasedByChoice, null);
				}
			}
			CampaignEventDispatcher.Instance.OnPrisonerReleased(troopRoster);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x0013EE50 File Offset: 0x0013D050
		public static void ApplyByReleasedByChoice(Hero character, Hero facilitator = null)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedByChoice, facilitator);
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x0013EE5A File Offset: 0x0013D05A
		public static void ApplyByReleasedByCompensation(Hero character)
		{
			EndCaptivityAction.ApplyInternal(character, EndCaptivityDetail.ReleasedByCompensation, null);
		}
	}
}
