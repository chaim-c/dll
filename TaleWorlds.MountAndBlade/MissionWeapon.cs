using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002C6 RID: 710
	public struct MissionWeapon
	{
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x00094ECF File Offset: 0x000930CF
		// (set) Token: 0x060026F7 RID: 9975 RVA: 0x00094ED7 File Offset: 0x000930D7
		public ItemObject Item { get; private set; }

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x00094EE0 File Offset: 0x000930E0
		// (set) Token: 0x060026F9 RID: 9977 RVA: 0x00094EE8 File Offset: 0x000930E8
		public ItemModifier ItemModifier { get; private set; }

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060026FA RID: 9978 RVA: 0x00094EF1 File Offset: 0x000930F1
		public int WeaponsCount
		{
			get
			{
				return this._weapons.Count;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x00094EFE File Offset: 0x000930FE
		public WeaponComponentData CurrentUsageItem
		{
			get
			{
				if (this._weapons == null || this._weapons.Count == 0)
				{
					return null;
				}
				return this._weapons[this.CurrentUsageIndex];
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060026FC RID: 9980 RVA: 0x00094F28 File Offset: 0x00093128
		// (set) Token: 0x060026FD RID: 9981 RVA: 0x00094F30 File Offset: 0x00093130
		public short ReloadPhase { get; set; }

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060026FE RID: 9982 RVA: 0x00094F3C File Offset: 0x0009313C
		public short ReloadPhaseCount
		{
			get
			{
				short result = 1;
				if (this.CurrentUsageItem != null)
				{
					result = this.CurrentUsageItem.ReloadPhaseCount;
				}
				return result;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x00094F60 File Offset: 0x00093160
		public bool IsReloading
		{
			get
			{
				return this.ReloadPhase < this.ReloadPhaseCount;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06002700 RID: 9984 RVA: 0x00094F70 File Offset: 0x00093170
		// (set) Token: 0x06002701 RID: 9985 RVA: 0x00094F78 File Offset: 0x00093178
		public Banner Banner { get; private set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06002702 RID: 9986 RVA: 0x00094F81 File Offset: 0x00093181
		// (set) Token: 0x06002703 RID: 9987 RVA: 0x00094F89 File Offset: 0x00093189
		public float GlossMultiplier { get; private set; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06002704 RID: 9988 RVA: 0x00094F92 File Offset: 0x00093192
		public short RawDataForNetwork
		{
			get
			{
				return this._dataValue;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002705 RID: 9989 RVA: 0x00094F9A File Offset: 0x0009319A
		// (set) Token: 0x06002706 RID: 9990 RVA: 0x00094FA2 File Offset: 0x000931A2
		public short HitPoints
		{
			get
			{
				return this._dataValue;
			}
			set
			{
				this._dataValue = value;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002707 RID: 9991 RVA: 0x00094FAB File Offset: 0x000931AB
		// (set) Token: 0x06002708 RID: 9992 RVA: 0x00094FB3 File Offset: 0x000931B3
		public short Amount
		{
			get
			{
				return this._dataValue;
			}
			set
			{
				this._dataValue = value;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002709 RID: 9993 RVA: 0x00094FBC File Offset: 0x000931BC
		public short Ammo
		{
			get
			{
				MissionWeapon.MissionSubWeapon ammoWeapon = this._ammoWeapon;
				if (ammoWeapon == null)
				{
					return 0;
				}
				return ammoWeapon.Value._dataValue;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x00094FD4 File Offset: 0x000931D4
		public MissionWeapon AmmoWeapon
		{
			get
			{
				MissionWeapon.MissionSubWeapon ammoWeapon = this._ammoWeapon;
				if (ammoWeapon == null)
				{
					return MissionWeapon.Invalid;
				}
				return ammoWeapon.Value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x00094FEB File Offset: 0x000931EB
		public short MaxAmmo
		{
			get
			{
				return this._modifiedMaxDataValue;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x00094FF3 File Offset: 0x000931F3
		public short ModifiedMaxAmount
		{
			get
			{
				return this._modifiedMaxDataValue;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x00094FFB File Offset: 0x000931FB
		public short ModifiedMaxHitPoints
		{
			get
			{
				return this._modifiedMaxDataValue;
			}
		}

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x0600270E RID: 9998 RVA: 0x00095003 File Offset: 0x00093203
		public bool IsEmpty
		{
			get
			{
				return this.CurrentUsageItem == null;
			}
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x00095010 File Offset: 0x00093210
		public MissionWeapon(ItemObject item, ItemModifier itemModifier, Banner banner)
		{
			this.Item = item;
			this.ItemModifier = itemModifier;
			this.Banner = banner;
			this.CurrentUsageIndex = 0;
			this._weapons = new List<WeaponComponentData>(1);
			this._modifiedMaxDataValue = 0;
			this._hasAnyConsumableUsage = false;
			if (item != null && item.Weapons != null)
			{
				foreach (WeaponComponentData weaponComponentData in item.Weapons)
				{
					this._weapons.Add(weaponComponentData);
					bool isConsumable = weaponComponentData.IsConsumable;
					if (isConsumable || weaponComponentData.IsRangedWeapon || weaponComponentData.WeaponFlags.HasAnyFlag(WeaponFlags.HasHitPoints))
					{
						this._modifiedMaxDataValue = weaponComponentData.MaxDataValue;
						if (itemModifier != null)
						{
							if (weaponComponentData.WeaponFlags.HasAnyFlag(WeaponFlags.HasHitPoints))
							{
								this._modifiedMaxDataValue = weaponComponentData.GetModifiedMaximumHitPoints(itemModifier);
							}
							else if (isConsumable)
							{
								this._modifiedMaxDataValue = weaponComponentData.GetModifiedStackCount(itemModifier);
							}
						}
					}
					if (isConsumable)
					{
						this._hasAnyConsumableUsage = true;
					}
				}
			}
			this._dataValue = this._modifiedMaxDataValue;
			this.ReloadPhase = 0;
			this._ammoWeapon = null;
			this._attachedWeapons = null;
			this._attachedWeaponFrames = null;
			this.GlossMultiplier = 1f;
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x00095158 File Offset: 0x00093358
		public MissionWeapon(ItemObject primaryItem, ItemModifier itemModifier, Banner banner, short dataValue)
		{
			this = new MissionWeapon(primaryItem, itemModifier, banner);
			this._dataValue = dataValue;
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x0009516B File Offset: 0x0009336B
		public MissionWeapon(ItemObject primaryItem, ItemModifier itemModifier, Banner banner, short dataValue, short reloadPhase, MissionWeapon? ammoWeapon)
		{
			this = new MissionWeapon(primaryItem, itemModifier, banner, dataValue);
			this.ReloadPhase = reloadPhase;
			this._ammoWeapon = ((ammoWeapon != null) ? new MissionWeapon.MissionSubWeapon(ammoWeapon.Value) : null);
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x0009519E File Offset: 0x0009339E
		public TextObject GetModifiedItemName()
		{
			if (this.ItemModifier == null)
			{
				return this.Item.Name;
			}
			TextObject name = this.ItemModifier.Name;
			name.SetTextVariable("ITEMNAME", this.Item.Name);
			return name;
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000951D6 File Offset: 0x000933D6
		public bool IsEqualTo(MissionWeapon other)
		{
			return this.Item == other.Item;
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x000951E7 File Offset: 0x000933E7
		public bool IsSameType(MissionWeapon other)
		{
			return this.Item.PrimaryWeapon.WeaponClass == other.Item.PrimaryWeapon.WeaponClass;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x0009520C File Offset: 0x0009340C
		public float GetWeight()
		{
			float num = this.Item.PrimaryWeapon.IsConsumable ? (this.GetBaseWeight() * (float)this._dataValue) : this.GetBaseWeight();
			MissionWeapon.MissionSubWeapon ammoWeapon = this._ammoWeapon;
			return num + ((ammoWeapon != null) ? ammoWeapon.Value.GetWeight() : 0f);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00095260 File Offset: 0x00093460
		private float GetBaseWeight()
		{
			return this.Item.Weight;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0009526D File Offset: 0x0009346D
		public WeaponComponentData GetWeaponComponentDataForUsage(int usageIndex)
		{
			return this._weapons[usageIndex];
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x0009527B File Offset: 0x0009347B
		public int GetGetModifiedArmorForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedArmor(this.ItemModifier);
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x00095299 File Offset: 0x00093499
		public int GetModifiedThrustDamageForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedThrustDamage(this.ItemModifier);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000952B7 File Offset: 0x000934B7
		public int GetModifiedSwingDamageForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedSwingDamage(this.ItemModifier);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x000952D5 File Offset: 0x000934D5
		public int GetModifiedMissileDamageForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedMissileDamage(this.ItemModifier);
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x000952F3 File Offset: 0x000934F3
		public int GetModifiedThrustSpeedForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedThrustSpeed(this.ItemModifier);
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x00095311 File Offset: 0x00093511
		public int GetModifiedSwingSpeedForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedSwingSpeed(this.ItemModifier);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x0009532F File Offset: 0x0009352F
		public int GetModifiedMissileSpeedForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedMissileSpeed(this.ItemModifier);
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x0009534D File Offset: 0x0009354D
		public int GetModifiedMissileSpeedForUsage(int usageIndex)
		{
			return this._weapons[usageIndex].GetModifiedMissileSpeed(this.ItemModifier);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x00095366 File Offset: 0x00093566
		public int GetModifiedHandlingForCurrentUsage()
		{
			return this._weapons[this.CurrentUsageIndex].GetModifiedHandling(this.ItemModifier);
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x00095384 File Offset: 0x00093584
		public WeaponData GetWeaponData(bool needBatchedVersionForMeshes)
		{
			if (!this.IsEmpty && this.Item.WeaponComponent != null)
			{
				WeaponComponent weaponComponent = this.Item.WeaponComponent;
				WeaponData result = new WeaponData
				{
					WeaponKind = (int)this.Item.Id.InternalValue,
					ItemHolsterIndices = this.Item.GetItemHolsterIndices(),
					ReloadPhase = this.ReloadPhase,
					Difficulty = this.Item.Difficulty,
					BaseWeight = this.GetBaseWeight(),
					HasFlagAnimation = false,
					WeaponFrame = weaponComponent.PrimaryWeapon.Frame,
					ScaleFactor = this.Item.ScaleFactor,
					Inertia = weaponComponent.PrimaryWeapon.Inertia,
					CenterOfMass = weaponComponent.PrimaryWeapon.CenterOfMass,
					CenterOfMass3D = weaponComponent.PrimaryWeapon.CenterOfMass3D,
					HolsterPositionShift = this.Item.HolsterPositionShift,
					TrailParticleName = weaponComponent.PrimaryWeapon.TrailParticleName,
					AmmoOffset = weaponComponent.PrimaryWeapon.AmmoOffset
				};
				string physicsMaterial = weaponComponent.PrimaryWeapon.PhysicsMaterial;
				result.PhysicsMaterialIndex = (string.IsNullOrEmpty(physicsMaterial) ? PhysicsMaterial.InvalidPhysicsMaterial.Index : PhysicsMaterial.GetFromName(physicsMaterial).Index);
				result.FlyingSoundCode = SoundManager.GetEventGlobalIndex(weaponComponent.PrimaryWeapon.FlyingSoundCode);
				result.PassbySoundCode = SoundManager.GetEventGlobalIndex(weaponComponent.PrimaryWeapon.PassbySoundCode);
				result.StickingFrame = weaponComponent.PrimaryWeapon.StickingFrame;
				result.CollisionShape = ((!needBatchedVersionForMeshes || string.IsNullOrEmpty(this.Item.CollisionBodyName)) ? null : PhysicsShape.GetFromResource(this.Item.CollisionBodyName, false));
				result.Shape = ((!needBatchedVersionForMeshes || string.IsNullOrEmpty(this.Item.BodyName)) ? null : PhysicsShape.GetFromResource(this.Item.BodyName, false));
				result.DataValue = this._dataValue;
				result.CurrentUsageIndex = this.CurrentUsageIndex;
				int rangedUsageIndex = this.GetRangedUsageIndex();
				WeaponComponentData weaponComponentData;
				if (this.GetConsumableIfAny(out weaponComponentData))
				{
					result.AirFrictionConstant = ItemObject.GetAirFrictionConstant(weaponComponentData.WeaponClass, weaponComponentData.WeaponFlags);
				}
				else if (rangedUsageIndex >= 0)
				{
					result.AirFrictionConstant = ItemObject.GetAirFrictionConstant(this.GetWeaponComponentDataForUsage(rangedUsageIndex).WeaponClass, this.GetWeaponComponentDataForUsage(rangedUsageIndex).WeaponFlags);
				}
				result.GlossMultiplier = this.GlossMultiplier;
				result.HasLowerHolsterPriority = this.Item.HasLowerHolsterPriority;
				MissionWeapon.OnGetWeaponDataDelegate onGetWeaponDataHandler = MissionWeapon.OnGetWeaponDataHandler;
				if (onGetWeaponDataHandler != null)
				{
					onGetWeaponDataHandler(ref result, this, false, this.Banner, needBatchedVersionForMeshes);
				}
				return result;
			}
			return WeaponData.InvalidWeaponData;
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x0009563C File Offset: 0x0009383C
		public WeaponStatsData[] GetWeaponStatsData()
		{
			WeaponStatsData[] array = new WeaponStatsData[this._weapons.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.GetWeaponStatsDataForUsage(i);
			}
			return array;
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00095678 File Offset: 0x00093878
		public WeaponStatsData GetWeaponStatsDataForUsage(int usageIndex)
		{
			WeaponStatsData result = default(WeaponStatsData);
			WeaponComponentData weaponComponentData = this._weapons[usageIndex];
			result.WeaponClass = (int)weaponComponentData.WeaponClass;
			result.AmmoClass = (int)weaponComponentData.AmmoClass;
			result.Properties = (uint)this.Item.ItemFlags;
			result.WeaponFlags = (ulong)weaponComponentData.WeaponFlags;
			result.ItemUsageIndex = (string.IsNullOrEmpty(weaponComponentData.ItemUsage) ? -1 : weaponComponentData.GetItemUsageIndex());
			result.ThrustSpeed = weaponComponentData.GetModifiedThrustSpeed(this.ItemModifier);
			result.SwingSpeed = weaponComponentData.GetModifiedSwingSpeed(this.ItemModifier);
			result.MissileSpeed = weaponComponentData.GetModifiedMissileSpeed(this.ItemModifier);
			result.ShieldArmor = weaponComponentData.GetModifiedArmor(this.ItemModifier);
			result.Accuracy = weaponComponentData.Accuracy;
			result.WeaponLength = weaponComponentData.WeaponLength;
			result.WeaponBalance = weaponComponentData.WeaponBalance;
			result.ThrustDamage = weaponComponentData.GetModifiedThrustDamage(this.ItemModifier);
			result.ThrustDamageType = (int)weaponComponentData.ThrustDamageType;
			result.SwingDamage = weaponComponentData.GetModifiedSwingDamage(this.ItemModifier);
			result.SwingDamageType = (int)weaponComponentData.SwingDamageType;
			result.DefendSpeed = weaponComponentData.GetModifiedHandling(this.ItemModifier);
			result.SweetSpot = weaponComponentData.SweetSpotReach;
			result.MaxDataValue = this._modifiedMaxDataValue;
			result.WeaponFrame = weaponComponentData.Frame;
			result.RotationSpeed = weaponComponentData.RotationSpeed;
			result.ReloadPhaseCount = weaponComponentData.ReloadPhaseCount;
			return result;
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x000957F8 File Offset: 0x000939F8
		public WeaponData GetAmmoWeaponData(bool needBatchedVersion)
		{
			return this.AmmoWeapon.GetWeaponData(needBatchedVersion);
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x00095814 File Offset: 0x00093A14
		public WeaponStatsData[] GetAmmoWeaponStatsData()
		{
			return this.AmmoWeapon.GetWeaponStatsData();
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x0009582F File Offset: 0x00093A2F
		public int GetAttachedWeaponsCount()
		{
			List<MissionWeapon.MissionSubWeapon> attachedWeapons = this._attachedWeapons;
			if (attachedWeapons == null)
			{
				return 0;
			}
			return attachedWeapons.Count;
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x00095842 File Offset: 0x00093A42
		public MissionWeapon GetAttachedWeapon(int attachmentIndex)
		{
			return this._attachedWeapons[attachmentIndex].Value;
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x00095855 File Offset: 0x00093A55
		public MatrixFrame GetAttachedWeaponFrame(int attachmentIndex)
		{
			return this._attachedWeaponFrames[attachmentIndex];
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x00095863 File Offset: 0x00093A63
		public bool IsShield()
		{
			return this._weapons.Count == 1 && this._weapons[0].IsShield;
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x00095886 File Offset: 0x00093A86
		public bool IsBanner()
		{
			return this._weapons.Count == 1 && this._weapons[0].WeaponClass == WeaponClass.Banner;
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x000958B0 File Offset: 0x00093AB0
		public bool IsAnyAmmo()
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsAmmo)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x0009590C File Offset: 0x00093B0C
		public bool HasAnyUsageWithWeaponClass(WeaponClass weaponClass)
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.WeaponClass == weaponClass)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x00095968 File Offset: 0x00093B68
		public bool HasAnyUsageWithAmmoClass(WeaponClass ammoClass)
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.AmmoClass == ammoClass)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x000959C4 File Offset: 0x00093BC4
		public bool HasAllUsagesWithAnyWeaponFlag(WeaponFlags flags)
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.WeaponFlags.HasAnyFlag(flags))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x00095A24 File Offset: 0x00093C24
		public bool HasAnyUsageWithoutWeaponFlag(WeaponFlags flags)
		{
			using (List<WeaponComponentData>.Enumerator enumerator = this._weapons.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.WeaponFlags.HasAnyFlag(flags))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x00095A84 File Offset: 0x00093C84
		public void GatherInformationFromWeapon(out bool weaponHasMelee, out bool weaponHasShield, out bool weaponHasPolearm, out bool weaponHasNonConsumableRanged, out bool weaponHasThrown, out WeaponClass rangedAmmoClass)
		{
			weaponHasMelee = false;
			weaponHasShield = false;
			weaponHasPolearm = false;
			weaponHasNonConsumableRanged = false;
			weaponHasThrown = false;
			rangedAmmoClass = WeaponClass.Undefined;
			foreach (WeaponComponentData weaponComponentData in this._weapons)
			{
				weaponHasMelee = (weaponHasMelee || weaponComponentData.IsMeleeWeapon);
				weaponHasShield = (weaponHasShield || weaponComponentData.IsShield);
				weaponHasPolearm = weaponComponentData.IsPolearm;
				if (weaponComponentData.IsRangedWeapon)
				{
					weaponHasThrown = weaponComponentData.IsConsumable;
					weaponHasNonConsumableRanged = !weaponHasThrown;
					rangedAmmoClass = weaponComponentData.AmmoClass;
				}
			}
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x00095B30 File Offset: 0x00093D30
		public bool GetConsumableIfAny(out WeaponComponentData consumableWeapon)
		{
			consumableWeapon = null;
			if (this._hasAnyConsumableUsage)
			{
				foreach (WeaponComponentData weaponComponentData in this._weapons)
				{
					if (weaponComponentData.IsConsumable)
					{
						consumableWeapon = weaponComponentData;
						break;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x00095B98 File Offset: 0x00093D98
		public bool IsAnyConsumable()
		{
			return this._hasAnyConsumableUsage;
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x00095BA0 File Offset: 0x00093DA0
		public int GetRangedUsageIndex()
		{
			for (int i = 0; i < this._weapons.Count; i++)
			{
				if (this._weapons[i].IsRangedWeapon)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x00095BDC File Offset: 0x00093DDC
		public MissionWeapon Consume(short count)
		{
			this.Amount -= count;
			return new MissionWeapon(this.Item, this.ItemModifier, this.Banner, count, 0, null);
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x00095C1C File Offset: 0x00093E1C
		public void ConsumeAmmo(short count)
		{
			if (count > 0)
			{
				MissionWeapon value = this._ammoWeapon.Value;
				value.Amount = count;
				this._ammoWeapon = new MissionWeapon.MissionSubWeapon(value);
				return;
			}
			this._ammoWeapon = null;
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x00095C55 File Offset: 0x00093E55
		public void SetAmmo(MissionWeapon ammoWeapon)
		{
			this._ammoWeapon = new MissionWeapon.MissionSubWeapon(ammoWeapon);
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x00095C64 File Offset: 0x00093E64
		public void ReloadAmmo(MissionWeapon ammoWeapon, short reloadPhase)
		{
			if (this._ammoWeapon != null && this._ammoWeapon.Value.Amount >= 0)
			{
				ammoWeapon.Amount += this._ammoWeapon.Value.Amount;
			}
			this._ammoWeapon = new MissionWeapon.MissionSubWeapon(ammoWeapon);
			this.ReloadPhase = reloadPhase;
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x00095CC4 File Offset: 0x00093EC4
		public void AttachWeapon(MissionWeapon attachedWeapon, ref MatrixFrame attachFrame)
		{
			if (this._attachedWeapons == null)
			{
				this._attachedWeapons = new List<MissionWeapon.MissionSubWeapon>();
				this._attachedWeaponFrames = new List<MatrixFrame>();
			}
			this._attachedWeapons.Add(new MissionWeapon.MissionSubWeapon(attachedWeapon));
			this._attachedWeaponFrames.Add(attachFrame);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x00095D11 File Offset: 0x00093F11
		public void RemoveAttachedWeapon(int attachmentIndex)
		{
			this._attachedWeapons.RemoveAt(attachmentIndex);
			this._attachedWeaponFrames.RemoveAt(attachmentIndex);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x00095D2B File Offset: 0x00093F2B
		public bool HasEnoughSpaceForAmount(int amount)
		{
			return (int)(this.ModifiedMaxAmount - this.Amount) >= amount;
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x00095D40 File Offset: 0x00093F40
		public void SetRandomGlossMultiplier(int seed)
		{
			Random random = new Random(seed);
			float glossMultiplier = 1f + (random.NextFloat() * 2f - 1f) * 0.3f;
			this.GlossMultiplier = glossMultiplier;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x00095D7A File Offset: 0x00093F7A
		public void AddExtraModifiedMaxValue(short extraValue)
		{
			this._modifiedMaxDataValue += extraValue;
		}

		// Token: 0x04000E85 RID: 3717
		public const short ReloadPhaseCountMax = 10;

		// Token: 0x04000E86 RID: 3718
		public static MissionWeapon.OnGetWeaponDataDelegate OnGetWeaponDataHandler;

		// Token: 0x04000E87 RID: 3719
		public static readonly MissionWeapon Invalid = new MissionWeapon(null, null, null);

		// Token: 0x04000E8A RID: 3722
		private readonly List<WeaponComponentData> _weapons;

		// Token: 0x04000E8B RID: 3723
		public int CurrentUsageIndex;

		// Token: 0x04000E8C RID: 3724
		private bool _hasAnyConsumableUsage;

		// Token: 0x04000E8D RID: 3725
		private short _dataValue;

		// Token: 0x04000E8E RID: 3726
		private short _modifiedMaxDataValue;

		// Token: 0x04000E92 RID: 3730
		private MissionWeapon.MissionSubWeapon _ammoWeapon;

		// Token: 0x04000E93 RID: 3731
		private List<MissionWeapon.MissionSubWeapon> _attachedWeapons;

		// Token: 0x04000E94 RID: 3732
		private List<MatrixFrame> _attachedWeaponFrames;

		// Token: 0x02000587 RID: 1415
		public struct ImpactSoundModifier
		{
			// Token: 0x04001D79 RID: 7545
			public const string ModifierName = "impactModifier";

			// Token: 0x04001D7A RID: 7546
			public const float None = 0f;

			// Token: 0x04001D7B RID: 7547
			public const float ActiveBlock = 0.1f;

			// Token: 0x04001D7C RID: 7548
			public const float ChamberBlocked = 0.2f;

			// Token: 0x04001D7D RID: 7549
			public const float CrushThrough = 0.3f;
		}

		// Token: 0x02000588 RID: 1416
		private class MissionSubWeapon
		{
			// Token: 0x170009B0 RID: 2480
			// (get) Token: 0x06003A23 RID: 14883 RVA: 0x000E64A9 File Offset: 0x000E46A9
			// (set) Token: 0x06003A24 RID: 14884 RVA: 0x000E64B1 File Offset: 0x000E46B1
			public MissionWeapon Value { get; private set; }

			// Token: 0x06003A25 RID: 14885 RVA: 0x000E64BA File Offset: 0x000E46BA
			public MissionSubWeapon(MissionWeapon subWeapon)
			{
				this.Value = subWeapon;
			}
		}

		// Token: 0x02000589 RID: 1417
		// (Invoke) Token: 0x06003A27 RID: 14887
		public delegate void OnGetWeaponDataDelegate(ref WeaponData weaponData, MissionWeapon weapon, bool isFemale, Banner banner, bool needBatchedVersion);
	}
}
