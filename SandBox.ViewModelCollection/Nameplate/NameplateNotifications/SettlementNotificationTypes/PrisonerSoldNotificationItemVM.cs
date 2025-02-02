using System;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;

namespace SandBox.ViewModelCollection.Nameplate.NameplateNotifications.SettlementNotificationTypes
{
	// Token: 0x02000021 RID: 33
	public class PrisonerSoldNotificationItemVM : SettlementNotificationItemBaseVM
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000DD34 File Offset: 0x0000BF34
		public MobileParty Party { get; private set; }

		// Token: 0x060002D1 RID: 721 RVA: 0x0000DD40 File Offset: 0x0000BF40
		public PrisonerSoldNotificationItemVM(Action<SettlementNotificationItemBaseVM> onRemove, MobileParty party, TroopRoster prisoners, int createdTick) : base(onRemove, createdTick)
		{
			this._prisonersAmount = prisoners.TotalManCount;
			base.Text = SandBoxUIHelper.GetPrisonersSoldNotificationText(this._prisonersAmount);
			this.Party = party;
			base.CharacterName = ((party.LeaderHero != null) ? party.LeaderHero.Name.ToString() : party.Name.ToString());
			base.CharacterVisual = new ImageIdentifierVM(SandBoxUIHelper.GetCharacterCode(PartyBaseHelper.GetVisualPartyLeader(party.Party), false));
			base.RelationType = 0;
			base.CreatedTick = createdTick;
			if (party.LeaderHero != null)
			{
				base.RelationType = (party.LeaderHero.Clan.IsAtWarWith(Hero.MainHero.Clan) ? -1 : 1);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000DDFE File Offset: 0x0000BFFE
		public void AddNewPrisoners(TroopRoster newPrisoners)
		{
			this._prisonersAmount += newPrisoners.Count;
			base.Text = SandBoxUIHelper.GetPrisonersSoldNotificationText(this._prisonersAmount);
		}

		// Token: 0x0400016F RID: 367
		private int _prisonersAmount;
	}
}
