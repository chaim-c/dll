﻿using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C5 RID: 453
	public static class MBEquipmentMissionExtensions
	{
		// Token: 0x060019FE RID: 6654 RVA: 0x0005BD24 File Offset: 0x00059F24
		public static SkinMask GetSkinMeshesMask(this Equipment equipment)
		{
			SkinMask skinMask = SkinMask.AllVisible;
			for (EquipmentIndex equipmentIndex = EquipmentIndex.WeaponItemBeginSlot; equipmentIndex < EquipmentIndex.NumEquipmentSetSlots; equipmentIndex++)
			{
				bool flag = equipment[(int)equipmentIndex].Item != null && equipment[(int)equipmentIndex].Item.HasArmorComponent;
				bool flag2 = equipment[(int)equipmentIndex].CosmeticItem != null && equipment[(int)equipmentIndex].CosmeticItem.HasArmorComponent;
				if (((equipmentIndex == EquipmentIndex.NumAllWeaponSlots || equipmentIndex == EquipmentIndex.Body || equipmentIndex == EquipmentIndex.Leg || equipmentIndex == EquipmentIndex.Gloves || equipmentIndex == EquipmentIndex.Cape) && flag) || flag2)
				{
					if (equipment[equipmentIndex].CosmeticItem == null)
					{
						skinMask &= equipment[(int)equipmentIndex].Item.ArmorComponent.MeshesMask;
					}
					else
					{
						skinMask &= equipment[(int)equipmentIndex].CosmeticItem.ArmorComponent.MeshesMask;
					}
				}
			}
			return skinMask;
		}
	}
}
