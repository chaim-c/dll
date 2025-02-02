using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.View
{
	// Token: 0x0200001F RID: 31
	public static class WeaponComponentViewExtensions
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x00007DC0 File Offset: 0x00005FC0
		public static MetaMesh GetFlyingMeshCopy(this WeaponComponentData weaponComponentData, ItemObject item)
		{
			if (item.WeaponDesign != null)
			{
				if (!weaponComponentData.IsRangedWeapon || !weaponComponentData.IsConsumable)
				{
					return null;
				}
				MetaMesh weaponMesh = CraftedDataViewManager.GetCraftedDataView(item.WeaponDesign).WeaponMesh;
				if (!(weaponMesh != null))
				{
					return null;
				}
				return weaponMesh.CreateCopy();
			}
			else
			{
				if (!string.IsNullOrEmpty(item.FlyingMeshName))
				{
					return MetaMesh.GetCopy(item.FlyingMeshName, true, false);
				}
				return null;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00007E2C File Offset: 0x0000602C
		public static MetaMesh GetFlyingMeshIfExists(this WeaponComponentData weaponComponentData, ItemObject item)
		{
			if (item.WeaponDesign != null && weaponComponentData.IsRangedWeapon && weaponComponentData.IsConsumable)
			{
				return CraftedDataViewManager.GetCraftedDataView(item.WeaponDesign).WeaponMesh;
			}
			return null;
		}
	}
}
