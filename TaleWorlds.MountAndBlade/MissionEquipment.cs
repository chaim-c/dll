using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000250 RID: 592
	public class MissionEquipment
	{
		// Token: 0x06001F90 RID: 8080 RVA: 0x000700FE File Offset: 0x0006E2FE
		public MissionEquipment()
		{
			this._weaponSlots = new MissionWeapon[5];
			this._cache = default(MissionEquipment.MissionEquipmentCache);
			this._cache.Initialize();
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0007012C File Offset: 0x0006E32C
		public MissionEquipment(Equipment spawnEquipment, Banner banner) : this()
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				this._weaponSlots[(int)equipmentIndex] = new MissionWeapon(spawnEquipment[equipmentIndex].Item, spawnEquipment[equipmentIndex].ItemModifier, banner);
			}
		}

		// Token: 0x17000645 RID: 1605
		public MissionWeapon this[int index]
		{
			get
			{
				return this._weaponSlots[index];
			}
			set
			{
				this._weaponSlots[index] = value;
				this._cache.InvalidateOnWeaponSlotUpdated();
			}
		}

		// Token: 0x17000646 RID: 1606
		public MissionWeapon this[EquipmentIndex index]
		{
			get
			{
				return this._weaponSlots[(int)index];
			}
			set
			{
				this[(int)index] = value;
			}
		}

		// Token: 0x06001F96 RID: 8086 RVA: 0x000701BC File Offset: 0x0006E3BC
		public void FillFrom(Equipment sourceEquipment, Banner banner)
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				this[equipmentIndex] = new MissionWeapon(sourceEquipment[equipmentIndex].Item, sourceEquipment[equipmentIndex].ItemModifier, banner);
			}
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x00070200 File Offset: 0x0006E400
		private float CalculateGetTotalWeightOfWeapons()
		{
			float num = 0f;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				MissionWeapon missionWeapon = this[equipmentIndex];
				if (!missionWeapon.IsEmpty)
				{
					if (missionWeapon.CurrentUsageItem.IsShield)
					{
						if (missionWeapon.HitPoints > 0)
						{
							num += missionWeapon.GetWeight();
						}
					}
					else
					{
						num += missionWeapon.GetWeight();
					}
				}
			}
			return num;
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x0007025F File Offset: 0x0006E45F
		public float GetTotalWeightOfWeapons()
		{
			if (!this._cache.IsValid(MissionEquipment.MissionEquipmentCache.CachedFloat.TotalWeightOfWeapons))
			{
				this._cache.UpdateAndMarkValid(MissionEquipment.MissionEquipmentCache.CachedFloat.TotalWeightOfWeapons, this.CalculateGetTotalWeightOfWeapons());
			}
			return this._cache.GetValue(MissionEquipment.MissionEquipmentCache.CachedFloat.TotalWeightOfWeapons);
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x00070290 File Offset: 0x0006E490
		public static EquipmentIndex SelectWeaponPickUpSlot(Agent agentPickingUp, MissionWeapon weaponBeingPickedUp, bool isStuckMissile)
		{
			EquipmentIndex equipmentIndex = EquipmentIndex.None;
			if (weaponBeingPickedUp.Item.ItemFlags.HasAnyFlag(ItemFlags.DropOnWeaponChange | ItemFlags.DropOnAnyAction))
			{
				equipmentIndex = EquipmentIndex.ExtraWeaponSlot;
			}
			else
			{
				Agent.HandIndex handIndex = weaponBeingPickedUp.Item.ItemFlags.HasAnyFlag(ItemFlags.HeldInOffHand) ? Agent.HandIndex.OffHand : Agent.HandIndex.MainHand;
				EquipmentIndex wieldedItemIndex = agentPickingUp.GetWieldedItemIndex(handIndex);
				MissionWeapon missionWeapon = (wieldedItemIndex != EquipmentIndex.None) ? agentPickingUp.Equipment[wieldedItemIndex] : MissionWeapon.Invalid;
				if (isStuckMissile)
				{
					bool flag = false;
					bool flag2 = false;
					bool isConsumable = weaponBeingPickedUp.Item.PrimaryWeapon.IsConsumable;
					if (isConsumable)
					{
						flag = (!missionWeapon.IsEmpty && missionWeapon.IsEqualTo(weaponBeingPickedUp) && missionWeapon.HasEnoughSpaceForAmount((int)weaponBeingPickedUp.Amount));
						flag2 = (!missionWeapon.IsEmpty && missionWeapon.IsSameType(weaponBeingPickedUp) && missionWeapon.HasEnoughSpaceForAmount((int)weaponBeingPickedUp.Amount));
					}
					EquipmentIndex equipmentIndex2 = EquipmentIndex.None;
					EquipmentIndex equipmentIndex3 = EquipmentIndex.None;
					EquipmentIndex equipmentIndex4 = EquipmentIndex.None;
					EquipmentIndex equipmentIndex5 = EquipmentIndex.WeaponItemBeginSlot;
					while (equipmentIndex5 < EquipmentIndex.ExtraWeaponSlot)
					{
						if (!isConsumable)
						{
							goto IL_19B;
						}
						if (equipmentIndex3 != EquipmentIndex.None && !agentPickingUp.Equipment[equipmentIndex5].IsEmpty && agentPickingUp.Equipment[equipmentIndex5].IsEqualTo(weaponBeingPickedUp) && agentPickingUp.Equipment[equipmentIndex5].HasEnoughSpaceForAmount((int)weaponBeingPickedUp.Amount))
						{
							equipmentIndex3 = equipmentIndex5;
						}
						else
						{
							if (equipmentIndex4 != EquipmentIndex.None || agentPickingUp.Equipment[equipmentIndex5].IsEmpty || !agentPickingUp.Equipment[equipmentIndex5].IsSameType(weaponBeingPickedUp) || !agentPickingUp.Equipment[equipmentIndex5].HasEnoughSpaceForAmount((int)weaponBeingPickedUp.Amount))
							{
								goto IL_19B;
							}
							equipmentIndex4 = equipmentIndex5;
						}
						IL_1BC:
						equipmentIndex5++;
						continue;
						IL_19B:
						if (equipmentIndex2 == EquipmentIndex.None && agentPickingUp.Equipment[equipmentIndex5].IsEmpty)
						{
							equipmentIndex2 = equipmentIndex5;
							goto IL_1BC;
						}
						goto IL_1BC;
					}
					if (flag)
					{
						equipmentIndex = wieldedItemIndex;
					}
					else if (equipmentIndex3 != EquipmentIndex.None)
					{
						equipmentIndex = equipmentIndex4;
					}
					else if (flag2)
					{
						equipmentIndex = wieldedItemIndex;
					}
					else if (equipmentIndex4 != EquipmentIndex.None)
					{
						equipmentIndex = equipmentIndex4;
					}
					else if (equipmentIndex2 != EquipmentIndex.None)
					{
						equipmentIndex = equipmentIndex2;
					}
				}
				else
				{
					bool isConsumable2 = weaponBeingPickedUp.Item.PrimaryWeapon.IsConsumable;
					if (isConsumable2 && weaponBeingPickedUp.Amount == 0)
					{
						equipmentIndex = EquipmentIndex.None;
					}
					else
					{
						if (handIndex == Agent.HandIndex.OffHand && wieldedItemIndex != EquipmentIndex.None)
						{
							for (int i = 0; i < 4; i++)
							{
								if (i != (int)wieldedItemIndex && !agentPickingUp.Equipment[i].IsEmpty && agentPickingUp.Equipment[i].Item.ItemFlags.HasAnyFlag(ItemFlags.HeldInOffHand))
								{
									equipmentIndex = wieldedItemIndex;
									break;
								}
							}
						}
						if (equipmentIndex == EquipmentIndex.None && isConsumable2)
						{
							for (EquipmentIndex equipmentIndex6 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex6 < EquipmentIndex.ExtraWeaponSlot; equipmentIndex6++)
							{
								if (!agentPickingUp.Equipment[equipmentIndex6].IsEmpty && agentPickingUp.Equipment[equipmentIndex6].IsSameType(weaponBeingPickedUp) && agentPickingUp.Equipment[equipmentIndex6].Amount < agentPickingUp.Equipment[equipmentIndex6].ModifiedMaxAmount)
								{
									equipmentIndex = equipmentIndex6;
									break;
								}
							}
						}
						if (equipmentIndex == EquipmentIndex.None)
						{
							for (EquipmentIndex equipmentIndex7 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex7 < EquipmentIndex.ExtraWeaponSlot; equipmentIndex7++)
							{
								if (agentPickingUp.Equipment[equipmentIndex7].IsEmpty)
								{
									equipmentIndex = equipmentIndex7;
									break;
								}
							}
						}
						if (equipmentIndex == EquipmentIndex.None)
						{
							for (EquipmentIndex equipmentIndex8 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex8 < EquipmentIndex.ExtraWeaponSlot; equipmentIndex8++)
							{
								if (!agentPickingUp.Equipment[equipmentIndex8].IsEmpty && agentPickingUp.Equipment[equipmentIndex8].IsAnyConsumable() && agentPickingUp.Equipment[equipmentIndex8].Amount == 0)
								{
									equipmentIndex = equipmentIndex8;
									break;
								}
							}
						}
						if (equipmentIndex == EquipmentIndex.None && !missionWeapon.IsEmpty)
						{
							equipmentIndex = wieldedItemIndex;
						}
						if (equipmentIndex == EquipmentIndex.None)
						{
							equipmentIndex = EquipmentIndex.WeaponItemBeginSlot;
						}
					}
				}
			}
			return equipmentIndex;
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x00070654 File Offset: 0x0006E854
		public bool HasAmmo(EquipmentIndex equipmentIndex, out int rangedUsageIndex, out bool hasLoadedAmmo, out bool noAmmoInThisSlot)
		{
			hasLoadedAmmo = false;
			noAmmoInThisSlot = false;
			MissionWeapon missionWeapon = this._weaponSlots[(int)equipmentIndex];
			rangedUsageIndex = missionWeapon.GetRangedUsageIndex();
			if (rangedUsageIndex >= 0)
			{
				if (missionWeapon.Ammo > 0)
				{
					hasLoadedAmmo = true;
					return true;
				}
				noAmmoInThisSlot = (missionWeapon.IsAnyConsumable() && missionWeapon.Amount == 0);
				for (EquipmentIndex equipmentIndex2 = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex2 < EquipmentIndex.NumAllWeaponSlots; equipmentIndex2++)
				{
					MissionWeapon missionWeapon2 = this[(int)equipmentIndex2];
					if (!missionWeapon2.IsEmpty && missionWeapon2.HasAnyUsageWithWeaponClass(missionWeapon.GetWeaponComponentDataForUsage(rangedUsageIndex).AmmoClass) && this[(int)equipmentIndex2].ModifiedMaxAmount > 1 && missionWeapon2.Amount > 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00070700 File Offset: 0x0006E900
		public int GetAmmoAmount(EquipmentIndex weaponIndex)
		{
			if (this[weaponIndex].IsAnyConsumable() && this[weaponIndex].ModifiedMaxAmount <= 1)
			{
				return (int)this[weaponIndex].ModifiedMaxAmount;
			}
			int num = 0;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				if (!this[(int)equipmentIndex].IsEmpty && this[(int)equipmentIndex].CurrentUsageItem.WeaponClass == this[weaponIndex].CurrentUsageItem.AmmoClass && this[(int)equipmentIndex].ModifiedMaxAmount > 1)
				{
					num += (int)this[(int)equipmentIndex].Amount;
				}
			}
			return num;
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x000707B0 File Offset: 0x0006E9B0
		public int GetMaxAmmo(EquipmentIndex weaponIndex)
		{
			if (this[weaponIndex].IsAnyConsumable() && this[weaponIndex].ModifiedMaxAmount <= 1)
			{
				return (int)this[weaponIndex].ModifiedMaxAmount;
			}
			int num = 0;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				if (!this[(int)equipmentIndex].IsEmpty && this[(int)equipmentIndex].CurrentUsageItem.WeaponClass == this[weaponIndex].CurrentUsageItem.AmmoClass && this[(int)equipmentIndex].ModifiedMaxAmount > 1)
				{
					num += (int)this[(int)equipmentIndex].ModifiedMaxAmount;
				}
			}
			return num;
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00070860 File Offset: 0x0006EA60
		public void GetAmmoCountAndIndexOfType(ItemObject.ItemTypeEnum itemType, out int ammoCount, out EquipmentIndex eIndex, EquipmentIndex equippedIndex = EquipmentIndex.None)
		{
			ItemObject.ItemTypeEnum ammoTypeForItemType = ItemObject.GetAmmoTypeForItemType(itemType);
			ItemObject itemObject;
			if (equippedIndex != EquipmentIndex.None)
			{
				itemObject = this[equippedIndex].Item;
				ammoCount = 0;
			}
			else
			{
				itemObject = null;
				ammoCount = -1;
			}
			eIndex = equippedIndex;
			if (ammoTypeForItemType != ItemObject.ItemTypeEnum.Invalid)
			{
				for (EquipmentIndex equipmentIndex = EquipmentIndex.Weapon3; equipmentIndex >= EquipmentIndex.WeaponItemBeginSlot; equipmentIndex--)
				{
					if (!this[equipmentIndex].IsEmpty && this[equipmentIndex].Item.Type == ammoTypeForItemType)
					{
						int amount = (int)this[equipmentIndex].Amount;
						if (amount > 0)
						{
							if (itemObject == null)
							{
								eIndex = equipmentIndex;
								itemObject = this[equipmentIndex].Item;
								ammoCount = amount;
							}
							else if (itemObject.Id == this[equipmentIndex].Item.Id)
							{
								ammoCount += amount;
							}
						}
					}
				}
			}
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00070934 File Offset: 0x0006EB34
		public static bool DoesWeaponFitToSlot(EquipmentIndex slotIndex, MissionWeapon weapon)
		{
			bool result;
			if (weapon.IsEmpty)
			{
				result = true;
			}
			else if (weapon.Item.ItemFlags.HasAnyFlag(ItemFlags.DropOnWeaponChange | ItemFlags.DropOnAnyAction))
			{
				result = (slotIndex == EquipmentIndex.ExtraWeaponSlot);
			}
			else
			{
				result = (slotIndex >= EquipmentIndex.WeaponItemBeginSlot && slotIndex < EquipmentIndex.ExtraWeaponSlot);
			}
			return result;
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0007097C File Offset: 0x0006EB7C
		public void CheckLoadedAmmos()
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				if (!this[equipmentIndex].IsEmpty && this[equipmentIndex].Item.PrimaryWeapon.WeaponClass == WeaponClass.Crossbow)
				{
					int num;
					EquipmentIndex equipmentIndex2;
					this.GetAmmoCountAndIndexOfType(this[equipmentIndex].Item.Type, out num, out equipmentIndex2, EquipmentIndex.None);
					if (equipmentIndex2 != EquipmentIndex.None)
					{
						MissionWeapon ammoWeapon = this._weaponSlots[(int)equipmentIndex2].Consume(MathF.Min(this[equipmentIndex].MaxAmmo, this._weaponSlots[(int)equipmentIndex2].Amount));
						this._weaponSlots[(int)equipmentIndex].ReloadAmmo(ammoWeapon, this._weaponSlots[(int)equipmentIndex].ReloadPhaseCount);
					}
				}
			}
			this._cache.InvalidateOnWeaponAmmoUpdated();
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x00070A56 File Offset: 0x0006EC56
		public void SetUsageIndexOfSlot(EquipmentIndex slotIndex, int usageIndex)
		{
			this._weaponSlots[(int)slotIndex].CurrentUsageIndex = usageIndex;
			this._cache.InvalidateOnWeaponUsageIndexUpdated();
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00070A75 File Offset: 0x0006EC75
		public void SetReloadPhaseOfSlot(EquipmentIndex slotIndex, short reloadPhase)
		{
			this._weaponSlots[(int)slotIndex].ReloadPhase = reloadPhase;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00070A8C File Offset: 0x0006EC8C
		public void SetAmountOfSlot(EquipmentIndex slotIndex, short dataValue, bool addOverflowToMaxAmount = false)
		{
			if (addOverflowToMaxAmount)
			{
				short num = dataValue - this._weaponSlots[(int)slotIndex].Amount;
				if (num > 0)
				{
					this._weaponSlots[(int)slotIndex].AddExtraModifiedMaxValue(num);
				}
			}
			short amount = this._weaponSlots[(int)slotIndex].Amount;
			this._weaponSlots[(int)slotIndex].Amount = dataValue;
			this._cache.InvalidateOnWeaponAmmoUpdated();
			if ((amount != 0 && dataValue == 0) || (amount == 0 && dataValue != 0))
			{
				this._cache.InvalidateOnWeaponAmmoAvailabilityChanged();
			}
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x00070B10 File Offset: 0x0006ED10
		public void SetHitPointsOfSlot(EquipmentIndex slotIndex, short dataValue, bool addOverflowToMaxHitPoints = false)
		{
			if (addOverflowToMaxHitPoints)
			{
				short num = dataValue - this._weaponSlots[(int)slotIndex].HitPoints;
				if (num > 0)
				{
					this._weaponSlots[(int)slotIndex].AddExtraModifiedMaxValue(num);
				}
			}
			this._weaponSlots[(int)slotIndex].HitPoints = dataValue;
			this._cache.InvalidateOnWeaponHitPointsUpdated();
			if (dataValue == 0)
			{
				this._cache.InvalidateOnWeaponDestroyed();
			}
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00070B78 File Offset: 0x0006ED78
		public void SetReloadedAmmoOfSlot(EquipmentIndex slotIndex, EquipmentIndex ammoSlotIndex, short totalAmmo)
		{
			if (ammoSlotIndex == EquipmentIndex.None)
			{
				this._weaponSlots[(int)slotIndex].SetAmmo(MissionWeapon.Invalid);
			}
			else
			{
				MissionWeapon ammo = this._weaponSlots[(int)ammoSlotIndex];
				ammo.Amount = totalAmmo;
				this._weaponSlots[(int)slotIndex].SetAmmo(ammo);
			}
			this._cache.InvalidateOnWeaponAmmoUpdated();
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00070BD3 File Offset: 0x0006EDD3
		public void SetConsumedAmmoOfSlot(EquipmentIndex slotIndex, short count)
		{
			this._weaponSlots[(int)slotIndex].ConsumeAmmo(count);
			this._cache.InvalidateOnWeaponAmmoUpdated();
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x00070BF2 File Offset: 0x0006EDF2
		public void AttachWeaponToWeaponInSlot(EquipmentIndex slotIndex, ref MissionWeapon weapon, ref MatrixFrame attachLocalFrame)
		{
			this._weaponSlots[(int)slotIndex].AttachWeapon(weapon, ref attachLocalFrame);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00070C0C File Offset: 0x0006EE0C
		public bool HasShield()
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				WeaponComponentData currentUsageItem = this._weaponSlots[(int)equipmentIndex].CurrentUsageItem;
				if (currentUsageItem != null && currentUsageItem.IsShield)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00070C48 File Offset: 0x0006EE48
		public bool HasAnyWeapon()
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				if (this._weaponSlots[(int)equipmentIndex].CurrentUsageItem != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00070C78 File Offset: 0x0006EE78
		public bool HasAnyWeaponWithFlags(WeaponFlags flags)
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				WeaponComponentData currentUsageItem = this._weaponSlots[(int)equipmentIndex].CurrentUsageItem;
				if (currentUsageItem != null && currentUsageItem.WeaponFlags.HasAllFlags(flags))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00070CB8 File Offset: 0x0006EEB8
		public ItemObject GetBanner()
		{
			ItemObject result = null;
			MissionWeapon missionWeapon = this._weaponSlots[4];
			ItemObject item = missionWeapon.Item;
			if (item != null && item.IsBannerItem && item.BannerComponent != null)
			{
				result = item;
			}
			return result;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00070CF4 File Offset: 0x0006EEF4
		public bool HasRangedWeapon(WeaponClass requiredAmmoClass = WeaponClass.Undefined)
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				WeaponComponentData currentUsageItem = this._weaponSlots[(int)equipmentIndex].CurrentUsageItem;
				if (currentUsageItem != null && currentUsageItem.IsRangedWeapon && (requiredAmmoClass == WeaponClass.Undefined || currentUsageItem.AmmoClass == requiredAmmoClass))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00070D39 File Offset: 0x0006EF39
		public bool ContainsNonConsumableRangedWeaponWithAmmo()
		{
			if (!this._cache.IsValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsNonConsumableRangedWeaponWithAmmo))
			{
				this.GatherInformationAndUpdateCache();
			}
			return this._cache.GetValue(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsNonConsumableRangedWeaponWithAmmo);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00070D5B File Offset: 0x0006EF5B
		public bool ContainsMeleeWeapon()
		{
			if (!this._cache.IsValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsMeleeWeapon))
			{
				this.GatherInformationAndUpdateCache();
			}
			return this._cache.GetValue(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsMeleeWeapon);
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x00070D7D File Offset: 0x0006EF7D
		public bool ContainsShield()
		{
			if (!this._cache.IsValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsShield))
			{
				this.GatherInformationAndUpdateCache();
			}
			return this._cache.GetValue(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsShield);
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x00070D9F File Offset: 0x0006EF9F
		public bool ContainsSpear()
		{
			if (!this._cache.IsValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsSpear))
			{
				this.GatherInformationAndUpdateCache();
			}
			return this._cache.GetValue(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsSpear);
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00070DC1 File Offset: 0x0006EFC1
		public bool ContainsThrownWeapon()
		{
			if (!this._cache.IsValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsThrownWeapon))
			{
				this.GatherInformationAndUpdateCache();
			}
			return this._cache.GetValue(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsThrownWeapon);
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00070DE4 File Offset: 0x0006EFE4
		private void GatherInformationAndUpdateCache()
		{
			bool value;
			bool value2;
			bool value3;
			bool value4;
			bool value5;
			this.GatherInformation(out value, out value2, out value3, out value4, out value5);
			this._cache.UpdateAndMarkValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsMeleeWeapon, value);
			this._cache.UpdateAndMarkValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsShield, value2);
			this._cache.UpdateAndMarkValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsSpear, value3);
			this._cache.UpdateAndMarkValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsNonConsumableRangedWeaponWithAmmo, value4);
			this._cache.UpdateAndMarkValid(MissionEquipment.MissionEquipmentCache.CachedBool.ContainsThrownWeapon, value5);
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00070E44 File Offset: 0x0006F044
		private void GatherInformation(out bool containsMeleeWeapon, out bool containsShield, out bool containsSpear, out bool containsNonConsumableRangedWeaponWithAmmo, out bool containsThrownWeapon)
		{
			containsMeleeWeapon = false;
			containsShield = false;
			containsSpear = false;
			containsNonConsumableRangedWeaponWithAmmo = false;
			containsThrownWeapon = false;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				bool flag;
				bool flag2;
				bool flag3;
				bool flag4;
				bool flag5;
				WeaponClass weaponClass;
				this._weaponSlots[(int)equipmentIndex].GatherInformationFromWeapon(out flag, out flag2, out flag3, out flag4, out flag5, out weaponClass);
				containsMeleeWeapon = (containsMeleeWeapon || flag);
				containsShield = (containsShield || flag2);
				containsSpear = (containsSpear || flag3);
				containsThrownWeapon = (containsThrownWeapon || flag5);
				if (flag4)
				{
					containsNonConsumableRangedWeaponWithAmmo = (containsNonConsumableRangedWeaponWithAmmo || this.GetAmmoAmount(equipmentIndex) > 0);
				}
			}
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00070EC0 File Offset: 0x0006F0C0
		public void SetGlossMultipliersOfWeaponsRandomly(int seed)
		{
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
			{
				this._weaponSlots[(int)equipmentIndex].SetRandomGlossMultiplier(seed);
			}
		}

		// Token: 0x04000BD2 RID: 3026
		private readonly MissionWeapon[] _weaponSlots;

		// Token: 0x04000BD3 RID: 3027
		private MissionEquipment.MissionEquipmentCache _cache;

		// Token: 0x02000509 RID: 1289
		private struct MissionEquipmentCache
		{
			// Token: 0x06003832 RID: 14386 RVA: 0x000E15AD File Offset: 0x000DF7AD
			public void Initialize()
			{
				this._cachedBool = default(StackArray.StackArray5Bool);
				this._validity = default(StackArray.StackArray6Bool);
			}

			// Token: 0x06003833 RID: 14387 RVA: 0x000E15C7 File Offset: 0x000DF7C7
			public bool IsValid(MissionEquipment.MissionEquipmentCache.CachedBool queriedData)
			{
				return this._validity[(int)queriedData];
			}

			// Token: 0x06003834 RID: 14388 RVA: 0x000E15D8 File Offset: 0x000DF7D8
			public void UpdateAndMarkValid(MissionEquipment.MissionEquipmentCache.CachedBool data, bool value)
			{
				this._cachedBool[(int)data] = value;
				this._validity[(int)data] = true;
			}

			// Token: 0x06003835 RID: 14389 RVA: 0x000E1601 File Offset: 0x000DF801
			public bool GetValue(MissionEquipment.MissionEquipmentCache.CachedBool data)
			{
				return this._cachedBool[(int)data];
			}

			// Token: 0x06003836 RID: 14390 RVA: 0x000E160F File Offset: 0x000DF80F
			public bool IsValid(MissionEquipment.MissionEquipmentCache.CachedFloat queriedData)
			{
				return this._validity[(int)(5 + queriedData)];
			}

			// Token: 0x06003837 RID: 14391 RVA: 0x000E1620 File Offset: 0x000DF820
			public void UpdateAndMarkValid(MissionEquipment.MissionEquipmentCache.CachedFloat data, float value)
			{
				this._cachedFloat = value;
				this._validity[(int)(5 + data)] = true;
			}

			// Token: 0x06003838 RID: 14392 RVA: 0x000E1645 File Offset: 0x000DF845
			public float GetValue(MissionEquipment.MissionEquipmentCache.CachedFloat data)
			{
				return this._cachedFloat;
			}

			// Token: 0x06003839 RID: 14393 RVA: 0x000E1650 File Offset: 0x000DF850
			public void InvalidateOnWeaponSlotUpdated()
			{
				this._validity[0] = false;
				this._validity[1] = false;
				this._validity[2] = false;
				this._validity[3] = false;
				this._validity[4] = false;
				this._validity[5] = false;
			}

			// Token: 0x0600383A RID: 14394 RVA: 0x000E16AB File Offset: 0x000DF8AB
			public void InvalidateOnWeaponUsageIndexUpdated()
			{
			}

			// Token: 0x0600383B RID: 14395 RVA: 0x000E16AD File Offset: 0x000DF8AD
			public void InvalidateOnWeaponAmmoUpdated()
			{
				this._validity[5] = false;
			}

			// Token: 0x0600383C RID: 14396 RVA: 0x000E16BC File Offset: 0x000DF8BC
			public void InvalidateOnWeaponAmmoAvailabilityChanged()
			{
				this._validity[3] = false;
			}

			// Token: 0x0600383D RID: 14397 RVA: 0x000E16CB File Offset: 0x000DF8CB
			public void InvalidateOnWeaponHitPointsUpdated()
			{
				this._validity[5] = false;
			}

			// Token: 0x0600383E RID: 14398 RVA: 0x000E16DA File Offset: 0x000DF8DA
			public void InvalidateOnWeaponDestroyed()
			{
				this._validity[1] = false;
			}

			// Token: 0x04001BD5 RID: 7125
			private const int CachedBoolCount = 5;

			// Token: 0x04001BD6 RID: 7126
			private const int CachedFloatCount = 1;

			// Token: 0x04001BD7 RID: 7127
			private const int TotalCachedCount = 6;

			// Token: 0x04001BD8 RID: 7128
			private float _cachedFloat;

			// Token: 0x04001BD9 RID: 7129
			private StackArray.StackArray5Bool _cachedBool;

			// Token: 0x04001BDA RID: 7130
			private StackArray.StackArray6Bool _validity;

			// Token: 0x02000679 RID: 1657
			public enum CachedBool
			{
				// Token: 0x0400215B RID: 8539
				ContainsMeleeWeapon,
				// Token: 0x0400215C RID: 8540
				ContainsShield,
				// Token: 0x0400215D RID: 8541
				ContainsSpear,
				// Token: 0x0400215E RID: 8542
				ContainsNonConsumableRangedWeaponWithAmmo,
				// Token: 0x0400215F RID: 8543
				ContainsThrownWeapon,
				// Token: 0x04002160 RID: 8544
				Count
			}

			// Token: 0x0200067A RID: 1658
			public enum CachedFloat
			{
				// Token: 0x04002162 RID: 8546
				TotalWeightOfWeapons,
				// Token: 0x04002163 RID: 8547
				Count
			}
		}
	}
}
