﻿using System;
using TaleWorlds.CampaignSystem.CharacterDevelopment;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.CampaignSystem.CampaignBehaviors
{
	// Token: 0x0200037A RID: 890
	public class CampaignBattleRecoveryBehavior : CampaignBehaviorBase
	{
		// Token: 0x06003469 RID: 13417 RVA: 0x000DC93D File Offset: 0x000DAB3D
		public override void RegisterEvents()
		{
			CampaignEvents.MapEventEnded.AddNonSerializedListener(this, new Action<MapEvent>(this.OnMapEventEnded));
			CampaignEvents.DailyTickPartyEvent.AddNonSerializedListener(this, new Action<MobileParty>(this.DailyTickParty));
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000DC970 File Offset: 0x000DAB70
		private void DailyTickParty(MobileParty party)
		{
			if (party.HasPerk(DefaultPerks.Medicine.Veterinarian, false) && MBRandom.RandomFloat < DefaultPerks.Medicine.Veterinarian.PrimaryBonus)
			{
				ItemModifier @object = MBObjectManager.Instance.GetObject<ItemModifier>("lame_horse");
				int num = MBRandom.RandomInt(party.ItemRoster.Count);
				for (int i = num; i < party.ItemRoster.Count + num; i++)
				{
					int index = i % party.ItemRoster.Count;
					ItemObject itemAtIndex = party.ItemRoster.GetItemAtIndex(index);
					ItemRosterElement elementCopyAtIndex = party.ItemRoster.GetElementCopyAtIndex(index);
					if (elementCopyAtIndex.EquipmentElement.ItemModifier == @object)
					{
						party.ItemRoster.AddToCounts(elementCopyAtIndex.EquipmentElement, -1);
						party.ItemRoster.Add(new ItemRosterElement(itemAtIndex, 1, null));
						return;
					}
				}
			}
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000DCA41 File Offset: 0x000DAC41
		public override void SyncData(IDataStore dataStore)
		{
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000DCA43 File Offset: 0x000DAC43
		private void OnMapEventEnded(MapEvent mapEvent)
		{
			this.CheckRecoveryForMapEventSide(mapEvent.AttackerSide);
			this.CheckRecoveryForMapEventSide(mapEvent.DefenderSide);
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x000DCA60 File Offset: 0x000DAC60
		private void CheckRecoveryForMapEventSide(MapEventSide mapEventSide)
		{
			if (mapEventSide.MapEvent.EventType == MapEvent.BattleTypes.FieldBattle || mapEventSide.MapEvent.EventType == MapEvent.BattleTypes.Siege || mapEventSide.MapEvent.EventType == MapEvent.BattleTypes.SiegeOutside)
			{
				foreach (MapEventParty mapEventParty in mapEventSide.Parties)
				{
					PartyBase party = mapEventParty.Party;
					if (party.IsMobile)
					{
						MobileParty mobileParty = party.MobileParty;
						foreach (TroopRosterElement troopRosterElement in mapEventParty.WoundedInBattle.GetTroopRoster())
						{
							int index = mapEventParty.WoundedInBattle.FindIndexOfTroop(troopRosterElement.Character);
							int elementNumber = mapEventParty.WoundedInBattle.GetElementNumber(index);
							if (mobileParty.HasPerk(DefaultPerks.Medicine.BattleHardened, false))
							{
								this.GiveTroopXp(troopRosterElement, elementNumber, party, (int)DefaultPerks.Medicine.BattleHardened.PrimaryBonus);
							}
						}
						foreach (TroopRosterElement troopRosterElement2 in mapEventParty.DiedInBattle.GetTroopRoster())
						{
							int index2 = mapEventParty.DiedInBattle.FindIndexOfTroop(troopRosterElement2.Character);
							int elementNumber2 = mapEventParty.DiedInBattle.GetElementNumber(index2);
							if (mobileParty.HasPerk(DefaultPerks.Medicine.Veterinarian, false) && troopRosterElement2.Character.IsMounted)
							{
								this.RecoverMountWithChance(troopRosterElement2, elementNumber2, party);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x000DCC38 File Offset: 0x000DAE38
		private void RecoverMountWithChance(TroopRosterElement troopRosterElement, int count, PartyBase party)
		{
			EquipmentElement equipmentElement = troopRosterElement.Character.Equipment[10];
			if (equipmentElement.Item != null)
			{
				for (int i = 0; i < count; i++)
				{
					if (MBRandom.RandomFloat < DefaultPerks.Medicine.Veterinarian.SecondaryBonus)
					{
						party.ItemRoster.AddToCounts(equipmentElement.Item, 1);
					}
				}
			}
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x000DCC92 File Offset: 0x000DAE92
		private void GiveTroopXp(TroopRosterElement troopRosterElement, int count, PartyBase partyBase, int xp)
		{
			partyBase.MemberRoster.AddXpToTroop(xp * count, troopRosterElement.Character);
		}
	}
}
