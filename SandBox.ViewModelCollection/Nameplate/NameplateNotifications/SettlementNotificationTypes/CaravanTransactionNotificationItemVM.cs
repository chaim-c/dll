using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Core;

namespace SandBox.ViewModelCollection.Nameplate.NameplateNotifications.SettlementNotificationTypes
{
	// Token: 0x0200001E RID: 30
	public class CaravanTransactionNotificationItemVM : SettlementNotificationItemBaseVM
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000D87A File Offset: 0x0000BA7A
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000D882 File Offset: 0x0000BA82
		public MobileParty CaravanParty { get; private set; }

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D88C File Offset: 0x0000BA8C
		public CaravanTransactionNotificationItemVM(Action<SettlementNotificationItemBaseVM> onRemove, MobileParty caravanParty, List<ValueTuple<EquipmentElement, int>> items, int createdTick) : base(onRemove, createdTick)
		{
			this._items = items;
			List<ValueTuple<EquipmentElement, int>> list = this._items.DistinctBy((ValueTuple<EquipmentElement, int> i) => i.Item1).ToList<ValueTuple<EquipmentElement, int>>();
			if (list.Count < this._items.Count)
			{
				this._items = list;
			}
			this.CaravanParty = caravanParty;
			if (items.Count > 0 && items[0].Item2 > 0)
			{
				this._isSelling = true;
			}
			base.Text = SandBoxUIHelper.GetItemsTradedNotificationText(items, this._isSelling);
			Hero leaderHero = caravanParty.LeaderHero;
			base.CharacterName = (((leaderHero != null) ? leaderHero.Name.ToString() : null) ?? caravanParty.Name.ToString());
			if (caravanParty.Party.Owner != null)
			{
				base.CharacterVisual = new ImageIdentifierVM(SandBoxUIHelper.GetCharacterCode(this.CaravanParty.Party.Owner.CharacterObject, false));
			}
			else
			{
				CharacterObject visualPartyLeader = PartyBaseHelper.GetVisualPartyLeader(this.CaravanParty.Party);
				if (visualPartyLeader != null)
				{
					base.CharacterVisual = new ImageIdentifierVM(SandBoxUIHelper.GetCharacterCode(visualPartyLeader, false));
				}
			}
			base.RelationType = 0;
			if (caravanParty != null)
			{
				IFaction mapFaction = caravanParty.MapFaction;
				base.RelationType = ((mapFaction != null && mapFaction.IsAtWarWith(Hero.MainHero.Clan)) ? -1 : 1);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
		public void AddNewItems(List<ValueTuple<EquipmentElement, int>> newItems)
		{
			CaravanTransactionNotificationItemVM.<>c__DisplayClass7_0 CS$<>8__locals1 = new CaravanTransactionNotificationItemVM.<>c__DisplayClass7_0();
			CS$<>8__locals1.newItems = newItems;
			int i;
			int j;
			for (i = 0; i < CS$<>8__locals1.newItems.Count; i = j + 1)
			{
				ValueTuple<EquipmentElement, int> valueTuple = this._items.FirstOrDefault((ValueTuple<EquipmentElement, int> t) => t.Item1.Equals(CS$<>8__locals1.newItems[i].Item1));
				if (!valueTuple.Item1.IsEmpty)
				{
					int index = this._items.IndexOf(valueTuple);
					valueTuple.Item2 += CS$<>8__locals1.newItems[i].Item2;
					this._items[index] = valueTuple;
				}
				else
				{
					this._items.Add(new ValueTuple<EquipmentElement, int>(CS$<>8__locals1.newItems[i].Item1, CS$<>8__locals1.newItems[i].Item2));
				}
				j = i;
			}
			base.Text = SandBoxUIHelper.GetItemsTradedNotificationText(this._items, this._isSelling);
		}

		// Token: 0x04000167 RID: 359
		private List<ValueTuple<EquipmentElement, int>> _items;

		// Token: 0x04000168 RID: 360
		private bool _isSelling;
	}
}
