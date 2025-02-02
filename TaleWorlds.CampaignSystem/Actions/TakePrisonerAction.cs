using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x0200045A RID: 1114
	public static class TakePrisonerAction
	{
		// Token: 0x06004120 RID: 16672 RVA: 0x00141C64 File Offset: 0x0013FE64
		private static void ApplyInternal(PartyBase capturerParty, Hero prisonerCharacter, bool isEventCalled = true)
		{
			if (prisonerCharacter.PartyBelongedTo != null)
			{
				if (prisonerCharacter.PartyBelongedTo.LeaderHero == prisonerCharacter)
				{
					prisonerCharacter.PartyBelongedTo.RemovePartyLeader();
				}
				prisonerCharacter.PartyBelongedTo.MemberRoster.RemoveTroop(prisonerCharacter.CharacterObject, 1, default(UniqueTroopDescriptor), 0);
			}
			prisonerCharacter.CaptivityStartTime = CampaignTime.Now;
			prisonerCharacter.ChangeState(Hero.CharacterStates.Prisoner);
			capturerParty.AddPrisoner(prisonerCharacter.CharacterObject, 1);
			if (prisonerCharacter == Hero.MainHero)
			{
				PlayerCaptivity.StartCaptivity(capturerParty);
			}
			if (capturerParty.IsSettlement && prisonerCharacter.StayingInSettlement != null)
			{
				prisonerCharacter.StayingInSettlement = null;
			}
			if (isEventCalled)
			{
				CampaignEventDispatcher.Instance.OnHeroPrisonerTaken(capturerParty, prisonerCharacter);
			}
		}

		// Token: 0x06004121 RID: 16673 RVA: 0x00141D07 File Offset: 0x0013FF07
		public static void Apply(PartyBase capturerParty, Hero prisonerCharacter)
		{
			TakePrisonerAction.ApplyInternal(capturerParty, prisonerCharacter, true);
		}

		// Token: 0x06004122 RID: 16674 RVA: 0x00141D14 File Offset: 0x0013FF14
		public static void ApplyByTakenFromPartyScreen(FlattenedTroopRoster roster)
		{
			foreach (FlattenedTroopRosterElement flattenedTroopRosterElement in roster)
			{
				if (flattenedTroopRosterElement.Troop.IsHero)
				{
					TakePrisonerAction.ApplyInternal(PartyBase.MainParty, flattenedTroopRosterElement.Troop.HeroObject, true);
				}
			}
			CampaignEventDispatcher.Instance.OnPrisonerTaken(roster);
		}
	}
}
