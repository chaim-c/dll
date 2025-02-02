using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200034B RID: 843
	public class StandingPointWithWeaponRequirement : StandingPoint
	{
		// Token: 0x06002E1D RID: 11805 RVA: 0x000BBCE0 File Offset: 0x000B9EE0
		public StandingPointWithWeaponRequirement()
		{
			this.AutoSheathWeapons = false;
			this._requiredWeaponClass1 = WeaponClass.Undefined;
			this._requiredWeaponClass2 = WeaponClass.Undefined;
			this._hasAlternative = base.HasAlternative();
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x000BBD09 File Offset: 0x000B9F09
		protected internal override void OnInit()
		{
			base.OnInit();
		}

		// Token: 0x06002E1F RID: 11807 RVA: 0x000BBD11 File Offset: 0x000B9F11
		public void InitRequiredWeaponClasses(WeaponClass requiredWeaponClass1, WeaponClass requiredWeaponClass2 = WeaponClass.Undefined)
		{
			this._requiredWeaponClass1 = requiredWeaponClass1;
			this._requiredWeaponClass2 = requiredWeaponClass2;
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x000BBD21 File Offset: 0x000B9F21
		public void InitRequiredWeapon(ItemObject weapon)
		{
			this._requiredWeapon = weapon;
		}

		// Token: 0x06002E21 RID: 11809 RVA: 0x000BBD2A File Offset: 0x000B9F2A
		public void InitGivenWeapon(ItemObject weapon)
		{
			this._givenWeapon = weapon;
		}

		// Token: 0x06002E22 RID: 11810 RVA: 0x000BBD34 File Offset: 0x000B9F34
		public override bool IsDisabledForAgent(Agent agent)
		{
			EquipmentIndex wieldedItemIndex = agent.GetWieldedItemIndex(Agent.HandIndex.MainHand);
			if (this._requiredWeapon != null)
			{
				if (wieldedItemIndex != EquipmentIndex.None && agent.Equipment[wieldedItemIndex].Item == this._requiredWeapon)
				{
					return base.IsDisabledForAgent(agent);
				}
			}
			else if (this._givenWeapon != null)
			{
				if (wieldedItemIndex == EquipmentIndex.None || agent.Equipment[wieldedItemIndex].Item != this._givenWeapon)
				{
					return base.IsDisabledForAgent(agent);
				}
			}
			else if ((this._requiredWeaponClass1 != WeaponClass.Undefined || this._requiredWeaponClass2 != WeaponClass.Undefined) && wieldedItemIndex != EquipmentIndex.None)
			{
				for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumAllWeaponSlots; equipmentIndex++)
				{
					if (!agent.Equipment[equipmentIndex].IsEmpty && (agent.Equipment[equipmentIndex].CurrentUsageItem.WeaponClass == this._requiredWeaponClass1 || agent.Equipment[equipmentIndex].CurrentUsageItem.WeaponClass == this._requiredWeaponClass2) && (!agent.Equipment[equipmentIndex].CurrentUsageItem.IsConsumable || agent.Equipment[equipmentIndex].Amount < agent.Equipment[equipmentIndex].ModifiedMaxAmount || equipmentIndex == EquipmentIndex.ExtraWeaponSlot))
					{
						return base.IsDisabledForAgent(agent);
					}
				}
			}
			return true;
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x000BBE8B File Offset: 0x000BA08B
		public void SetHasAlternative(bool hasAlternative)
		{
			this._hasAlternative = hasAlternative;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x000BBE94 File Offset: 0x000BA094
		public override bool HasAlternative()
		{
			return this._hasAlternative;
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x000BBE9C File Offset: 0x000BA09C
		public void SetUsingBattleSide(BattleSideEnum side)
		{
			this.StandingPointSide = side;
		}

		// Token: 0x04001337 RID: 4919
		private ItemObject _requiredWeapon;

		// Token: 0x04001338 RID: 4920
		private ItemObject _givenWeapon;

		// Token: 0x04001339 RID: 4921
		private WeaponClass _requiredWeaponClass1;

		// Token: 0x0400133A RID: 4922
		private WeaponClass _requiredWeaponClass2;

		// Token: 0x0400133B RID: 4923
		private bool _hasAlternative;
	}
}
