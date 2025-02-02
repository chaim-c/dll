using System;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x020003B8 RID: 952
	public class PartiesSellLootCampaignBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003A30 RID: 14896 RVA: 0x001120B7 File Offset: 0x001102B7
		public override void RegisterEvents()
		{
			CampaignEvents.SettlementEntered.AddNonSerializedListener(this, new Action<MobileParty, Settlement, Hero>(this.OnSettlementEntered));
		}

		// Token: 0x06003A31 RID: 14897 RVA: 0x001120D0 File Offset: 0x001102D0
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x06003A32 RID: 14898 RVA: 0x001120D4 File Offset: 0x001102D4
		public void OnSettlementEntered(MobileParty mobileParty, Settlement settlement, Hero hero)
		{
			if (Campaign.Current.GameStarted && mobileParty != null && !FactionManager.IsAtWarAgainstFaction(mobileParty.MapFaction, settlement.MapFaction) && !mobileParty.IsMainParty && mobileParty.IsLordParty && !mobileParty.IsDisbanding && settlement.IsTown)
			{
				int gold = settlement.SettlementComponent.Gold;
				for (int i = 0; i < mobileParty.ItemRoster.Count; i++)
				{
					ItemRosterElement subject = mobileParty.ItemRoster[i];
					ItemObject item = subject.EquipmentElement.Item;
					ItemModifier itemModifier = subject.EquipmentElement.ItemModifier;
					int amount = subject.Amount;
					if (!item.IsFood && (item.ItemType != ItemObject.ItemTypeEnum.Horse || !item.HorseComponent.IsRideable || itemModifier != null || item.HorseComponent.IsPackAnimal))
					{
						int itemPrice = settlement.Town.GetItemPrice(subject.EquipmentElement, mobileParty, true);
						int num = (itemPrice * amount < gold) ? amount : (gold / itemPrice);
						if (num > 0)
						{
							SellItemsAction.Apply(mobileParty.Party, settlement.Party, subject, num, settlement);
						}
					}
				}
			}
		}
	}
}
