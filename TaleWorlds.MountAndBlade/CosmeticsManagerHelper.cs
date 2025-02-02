using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.Diamond.Cosmetics;
using TaleWorlds.MountAndBlade.Diamond.Cosmetics.CosmeticTypes;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000201 RID: 513
	public static class CosmeticsManagerHelper
	{
		// Token: 0x06001C63 RID: 7267 RVA: 0x0006306C File Offset: 0x0006126C
		public static Dictionary<int, List<int>> GetUsedIndicesFromIds(Dictionary<string, List<string>> usedCosmetics)
		{
			Dictionary<int, List<int>> dictionary = new Dictionary<int, List<int>>();
			MBReadOnlyList<MultiplayerClassDivisions.MPHeroClass> objectTypeList = MBObjectManager.Instance.GetObjectTypeList<MultiplayerClassDivisions.MPHeroClass>();
			foreach (KeyValuePair<string, List<string>> keyValuePair in usedCosmetics)
			{
				int num = -1;
				for (int i = 0; i < objectTypeList.Count; i++)
				{
					if (objectTypeList[i].StringId == keyValuePair.Key)
					{
						num = i;
						break;
					}
				}
				if (num != -1)
				{
					List<int> list = new List<int>();
					foreach (string b in keyValuePair.Value)
					{
						int num2 = -1;
						for (int j = 0; j < CosmeticsManager.CosmeticElementsList.Count; j++)
						{
							if (CosmeticsManager.CosmeticElementsList[j].Id == b)
							{
								num2 = j;
								break;
							}
						}
						if (num2 >= 0)
						{
							list.Add(num2);
						}
					}
					if (list.Count > 0)
					{
						dictionary.Add(num, list);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001C64 RID: 7268 RVA: 0x000631B0 File Offset: 0x000613B0
		public static ActionIndexCache GetSuitableTauntAction(Agent agent, int tauntIndex)
		{
			if (agent.Equipment == null)
			{
				return ActionIndexCache.act_none;
			}
			WeaponComponentData currentUsageItem = agent.WieldedWeapon.CurrentUsageItem;
			WeaponComponentData currentUsageItem2 = agent.WieldedOffhandWeapon.CurrentUsageItem;
			return ActionIndexCache.Create(TauntUsageManager.GetAction(tauntIndex, agent.GetIsLeftStance(), !agent.HasMount, currentUsageItem, currentUsageItem2));
		}

		// Token: 0x06001C65 RID: 7269 RVA: 0x00063208 File Offset: 0x00061408
		public static TauntUsageManager.TauntUsage.TauntUsageFlag GetActionNotUsableReason(Agent agent, int tauntIndex)
		{
			WeaponComponentData currentUsageItem = agent.WieldedWeapon.CurrentUsageItem;
			WeaponComponentData currentUsageItem2 = agent.WieldedOffhandWeapon.CurrentUsageItem;
			return TauntUsageManager.GetIsActionNotSuitableReason(tauntIndex, agent.GetIsLeftStance(), !agent.HasMount, currentUsageItem, currentUsageItem2);
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0006324C File Offset: 0x0006144C
		public static string GetSuitableTauntActionForEquipment(Equipment equipment, TauntCosmeticElement taunt)
		{
			if (equipment == null)
			{
				return null;
			}
			EquipmentIndex equipmentIndex;
			EquipmentIndex equipmentIndex2;
			bool flag;
			equipment.GetInitialWeaponIndicesToEquip(out equipmentIndex, out equipmentIndex2, out flag, Equipment.InitialWeaponEquipPreference.Any);
			WeaponComponentData weaponComponentData;
			if (equipmentIndex == EquipmentIndex.None)
			{
				weaponComponentData = null;
			}
			else
			{
				ItemObject item = equipment[equipmentIndex].Item;
				weaponComponentData = ((item != null) ? item.PrimaryWeapon : null);
			}
			WeaponComponentData mainHandWeapon = weaponComponentData;
			WeaponComponentData weaponComponentData2;
			if (equipmentIndex2 == EquipmentIndex.None)
			{
				weaponComponentData2 = null;
			}
			else
			{
				ItemObject item2 = equipment[equipmentIndex2].Item;
				weaponComponentData2 = ((item2 != null) ? item2.PrimaryWeapon : null);
			}
			WeaponComponentData offhandWeapon = weaponComponentData2;
			return TauntUsageManager.GetAction(TauntUsageManager.GetIndexOfAction(taunt.Id), false, true, mainHandWeapon, offhandWeapon);
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x000632C9 File Offset: 0x000614C9
		public static bool IsWeaponClassOneHanded(WeaponClass weaponClass)
		{
			return weaponClass == WeaponClass.OneHandedAxe || weaponClass == WeaponClass.OneHandedPolearm || weaponClass == WeaponClass.OneHandedSword;
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x000632DA File Offset: 0x000614DA
		public static bool IsWeaponClassTwoHanded(WeaponClass weaponClass)
		{
			return weaponClass == WeaponClass.TwoHandedAxe || weaponClass == WeaponClass.TwoHandedMace || weaponClass == WeaponClass.TwoHandedPolearm || weaponClass == WeaponClass.TwoHandedSword;
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x000632EF File Offset: 0x000614EF
		public static bool IsWeaponClassShield(WeaponClass weaponClass)
		{
			return weaponClass == WeaponClass.LargeShield || weaponClass == WeaponClass.SmallShield;
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x000632FD File Offset: 0x000614FD
		public static bool IsWeaponClassBow(WeaponClass weaponClass)
		{
			return weaponClass == WeaponClass.Bow;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x00063304 File Offset: 0x00061504
		public static bool IsWeaponClassCrossbow(WeaponClass weaponClass)
		{
			return weaponClass == WeaponClass.Crossbow;
		}

		// Token: 0x06001C6C RID: 7276 RVA: 0x0006330C File Offset: 0x0006150C
		public static WeaponClass[] GetComplimentaryWeaponClasses(WeaponClass weaponClass)
		{
			switch (weaponClass)
			{
			case WeaponClass.OneHandedSword:
			case WeaponClass.OneHandedAxe:
			case WeaponClass.Mace:
			case WeaponClass.Pick:
			case WeaponClass.OneHandedPolearm:
			case WeaponClass.LowGripPolearm:
			case WeaponClass.Stone:
			case WeaponClass.ThrowingAxe:
			case WeaponClass.ThrowingKnife:
			case WeaponClass.Javelin:
				return new WeaponClass[]
				{
					WeaponClass.SmallShield,
					WeaponClass.LargeShield
				};
			case WeaponClass.Arrow:
				return new WeaponClass[]
				{
					WeaponClass.Bow
				};
			case WeaponClass.Bolt:
				return new WeaponClass[]
				{
					WeaponClass.Crossbow
				};
			case WeaponClass.Bow:
				return new WeaponClass[]
				{
					WeaponClass.Arrow
				};
			case WeaponClass.Crossbow:
				return new WeaponClass[]
				{
					WeaponClass.Bolt
				};
			case WeaponClass.SmallShield:
			case WeaponClass.LargeShield:
			{
				WeaponClass[] array = new WeaponClass[4];
				RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.895DDC90B779053B1FA2B08E339BED64E96C6AC9).FieldHandle);
				return array;
			}
			}
			return new WeaponClass[0];
		}
	}
}
