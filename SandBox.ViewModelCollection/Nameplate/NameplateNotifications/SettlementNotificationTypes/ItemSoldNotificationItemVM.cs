using System;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace SandBox.ViewModelCollection.Nameplate.NameplateNotifications.SettlementNotificationTypes
{
	// Token: 0x02000020 RID: 32
	public class ItemSoldNotificationItemVM : SettlementNotificationItemBaseVM
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		public ItemRosterElement Item { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000DBD8 File Offset: 0x0000BDD8
		public PartyBase ReceiverParty { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000DBE0 File Offset: 0x0000BDE0
		public PartyBase PayerParty { get; }

		// Token: 0x060002CD RID: 717 RVA: 0x0000DBE8 File Offset: 0x0000BDE8
		public ItemSoldNotificationItemVM(Action<SettlementNotificationItemBaseVM> onRemove, PartyBase receiverParty, PartyBase payerParty, ItemRosterElement item, int number, int createdTick) : base(onRemove, createdTick)
		{
			this.Item = item;
			this.ReceiverParty = receiverParty;
			this.PayerParty = payerParty;
			this._number = number;
			this._heroParty = (receiverParty.IsSettlement ? payerParty : receiverParty);
			base.Text = SandBoxUIHelper.GetItemSoldNotificationText(this.Item, this._number, this._number < 0);
			base.CharacterName = ((this._heroParty.LeaderHero != null) ? this._heroParty.LeaderHero.Name.ToString() : this._heroParty.Name.ToString());
			CharacterObject visualPartyLeader = PartyBaseHelper.GetVisualPartyLeader(this._heroParty);
			base.CharacterVisual = new ImageIdentifierVM(SandBoxUIHelper.GetCharacterCode(visualPartyLeader, false));
			base.RelationType = 0;
			base.CreatedTick = createdTick;
			if (this._heroParty.LeaderHero != null)
			{
				base.RelationType = (this._heroParty.LeaderHero.Clan.IsAtWarWith(Hero.MainHero.Clan) ? -1 : 1);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000DCED File Offset: 0x0000BEED
		public void AddNewTransaction(int amount)
		{
			this._number += amount;
			if (this._number == 0)
			{
				base.ExecuteRemove();
				return;
			}
			base.Text = SandBoxUIHelper.GetItemSoldNotificationText(this.Item, this._number, this._number < 0);
		}

		// Token: 0x0400016C RID: 364
		private int _number;

		// Token: 0x0400016D RID: 365
		private PartyBase _heroParty;
	}
}
