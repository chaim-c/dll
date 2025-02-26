﻿using System;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F8 RID: 504
	public class DefaultItemPickupModel : ItemPickupModel
	{
		// Token: 0x06001C13 RID: 7187 RVA: 0x000615AC File Offset: 0x0005F7AC
		public override float GetItemScoreForAgent(SpawnedItemEntity item, Agent agent)
		{
			if (!item.WeaponCopy.Item.ItemFlags.HasAnyFlag(ItemFlags.CannotBePickedUp))
			{
				WeaponClass weaponClass = item.WeaponCopy.Item.PrimaryWeapon.WeaponClass;
				if (MissionGameModels.Current.BattleBannerBearersModel.IsFormationBanner(agent.Formation, item))
				{
					return 120f;
				}
				if (agent.HadSameTypeOfConsumableOrShieldOnSpawn(weaponClass))
				{
					switch (weaponClass)
					{
					case WeaponClass.Arrow:
						return 80f;
					case WeaponClass.Bolt:
						return 80f;
					case WeaponClass.Stone:
						return 20f;
					case WeaponClass.Boulder:
						return -1f;
					case WeaponClass.ThrowingAxe:
						return 60f;
					case WeaponClass.ThrowingKnife:
						return 50f;
					case WeaponClass.Javelin:
						return 70f;
					case WeaponClass.SmallShield:
					case WeaponClass.LargeShield:
						return 100f;
					}
					throw new MBException("This pickable item not scored: " + weaponClass.ToString());
				}
			}
			return 0f;
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x000616B4 File Offset: 0x0005F8B4
		public override bool IsItemAvailableForAgent(SpawnedItemEntity item, Agent agent, EquipmentIndex slotToPickUp)
		{
			if (!agent.CanReachAndUseObject(item, 0f) || !agent.ObjectHasVacantPosition(item) || item.HasAIMovingTo)
			{
				return false;
			}
			WeaponClass weaponClass = item.WeaponCopy.Item.PrimaryWeapon.WeaponClass;
			switch (weaponClass)
			{
			case WeaponClass.Arrow:
			case WeaponClass.Bolt:
			case WeaponClass.ThrowingAxe:
			case WeaponClass.ThrowingKnife:
			case WeaponClass.Javelin:
				if (item.WeaponCopy.Amount > 0 && !agent.Equipment[slotToPickUp].IsEmpty && agent.Equipment[slotToPickUp].Item.PrimaryWeapon.WeaponClass == weaponClass && (int)agent.Equipment[slotToPickUp].Amount <= agent.Equipment[slotToPickUp].ModifiedMaxAmount >> 1)
				{
					return true;
				}
				break;
			case WeaponClass.SmallShield:
			case WeaponClass.LargeShield:
				return agent.Equipment[slotToPickUp].IsEmpty && agent.HasLostShield();
			case WeaponClass.Banner:
				return agent.Equipment[slotToPickUp].IsEmpty;
			}
			return false;
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x000617F8 File Offset: 0x0005F9F8
		public override bool IsAgentEquipmentSuitableForPickUpAvailability(Agent agent)
		{
			if (agent.HasLostShield())
			{
				return true;
			}
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.ExtraWeaponSlot; equipmentIndex++)
			{
				MissionWeapon missionWeapon = agent.Equipment[equipmentIndex];
				if (!missionWeapon.IsEmpty && missionWeapon.IsAnyConsumable() && (int)missionWeapon.Amount <= missionWeapon.ModifiedMaxAmount >> 1)
				{
					return true;
				}
			}
			BattleBannerBearersModel battleBannerBearersModel = MissionGameModels.Current.BattleBannerBearersModel;
			return battleBannerBearersModel != null && battleBannerBearersModel.IsBannerSearchingAgent(agent);
		}
	}
}
