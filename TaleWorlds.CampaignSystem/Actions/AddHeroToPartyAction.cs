using System;
using Helpers;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Actions
{
	// Token: 0x02000424 RID: 1060
	public static class AddHeroToPartyAction
	{
		// Token: 0x06004041 RID: 16449 RVA: 0x0013C5E0 File Offset: 0x0013A7E0
		private static void ApplyInternal(Hero hero, MobileParty newParty, bool showNotification = true)
		{
			PartyBase partyBase = (hero.PartyBelongedTo != null) ? hero.PartyBelongedTo.Party : ((hero.CurrentSettlement != null) ? hero.CurrentSettlement.Party : null);
			if (partyBase != null)
			{
				if (partyBase.IsSettlement && partyBase.Settlement.Notables.IndexOf(hero) >= 0)
				{
					hero.StayingInSettlement = null;
				}
				else
				{
					partyBase.MemberRoster.AddToCounts(hero.CharacterObject, -1, false, 0, 0, true, -1);
				}
			}
			if (hero.GovernorOf != null)
			{
				ChangeGovernorAction.RemoveGovernorOf(hero);
			}
			newParty.AddElementToMemberRoster(hero.CharacterObject, 1, false);
			CampaignEventDispatcher.Instance.OnHeroJoinedParty(hero, newParty);
			if (showNotification && newParty == MobileParty.MainParty && hero.IsPlayerCompanion)
			{
				TextObject textObject = GameTexts.FindText("str_companion_added", null);
				StringHelpers.SetCharacterProperties("COMPANION", hero.CharacterObject, textObject, false);
				MBInformationManager.AddQuickInformation(textObject, 0, null, "");
			}
		}

		// Token: 0x06004042 RID: 16450 RVA: 0x0013C6C1 File Offset: 0x0013A8C1
		public static void Apply(Hero hero, MobileParty party, bool showNotification = true)
		{
			AddHeroToPartyAction.ApplyInternal(hero, party, showNotification);
		}
	}
}
