using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E4 RID: 484
	[EngineStruct("Blow_weapon_record", false)]
	public struct BlowWeaponRecord
	{
		// Token: 0x06001AF3 RID: 6899 RVA: 0x0005D580 File Offset: 0x0005B780
		public void FillAsMeleeBlow(ItemObject item, WeaponComponentData weaponComponentData, int affectorWeaponSlot, sbyte weaponAttachBoneIndex)
		{
			this._isMissile = false;
			if (weaponComponentData != null)
			{
				this.ItemFlags = item.ItemFlags;
				this.WeaponFlags = weaponComponentData.WeaponFlags;
				this.WeaponClass = weaponComponentData.WeaponClass;
				this.BoneNoToAttach = weaponAttachBoneIndex;
				this.AffectorWeaponSlotOrMissileIndex = affectorWeaponSlot;
				this.Weight = item.Weight;
				this._isMaterialMetal = weaponComponentData.PhysicsMaterial.Contains("metal");
				return;
			}
			this._isMaterialMetal = false;
			this.AffectorWeaponSlotOrMissileIndex = -1;
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x0005D5FC File Offset: 0x0005B7FC
		public void FillAsMissileBlow(ItemObject item, WeaponComponentData weaponComponentData, int missileIndex, sbyte weaponAttachBoneIndex, Vec3 startingPosition, Vec3 currentPosition, Vec3 velocity)
		{
			this._isMissile = true;
			this.StartingPosition = startingPosition;
			this.CurrentPosition = currentPosition;
			this.Velocity = velocity;
			this.ItemFlags = item.ItemFlags;
			this.WeaponFlags = weaponComponentData.WeaponFlags;
			this.WeaponClass = weaponComponentData.WeaponClass;
			this.BoneNoToAttach = weaponAttachBoneIndex;
			this.AffectorWeaponSlotOrMissileIndex = missileIndex;
			this.Weight = item.Weight;
			this._isMaterialMetal = weaponComponentData.PhysicsMaterial.Contains("metal");
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x0005D67D File Offset: 0x0005B87D
		public bool HasWeapon()
		{
			return this.AffectorWeaponSlotOrMissileIndex >= 0;
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001AF6 RID: 6902 RVA: 0x0005D68B File Offset: 0x0005B88B
		public bool IsMissile
		{
			get
			{
				return this._isMissile;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x0005D693 File Offset: 0x0005B893
		public bool IsShield
		{
			get
			{
				return !this.WeaponFlags.HasAnyFlag(WeaponFlags.WeaponMask) && this.WeaponFlags.HasAllFlags(WeaponFlags.HasHitPoints | WeaponFlags.CanBlockRanged);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001AF8 RID: 6904 RVA: 0x0005D6B7 File Offset: 0x0005B8B7
		public bool IsRanged
		{
			get
			{
				return this.WeaponFlags.HasAnyFlag(WeaponFlags.RangedWeapon);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x0005D6C6 File Offset: 0x0005B8C6
		public bool IsAmmo
		{
			get
			{
				return !this.WeaponFlags.HasAnyFlag(WeaponFlags.WeaponMask) && this.WeaponFlags.HasAnyFlag(WeaponFlags.Consumable);
			}
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x0005D6EC File Offset: 0x0005B8EC
		public int GetHitSound(bool isOwnerHumanoid, bool isCriticalBlow, bool isLowBlow, bool isNonTipThrust, AgentAttackType attackType, DamageTypes damageType)
		{
			int result;
			if (this.HasWeapon())
			{
				if (this.IsRanged || this.IsAmmo)
				{
					switch (this.WeaponClass)
					{
					case WeaponClass.Stone:
						if (isCriticalBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatThrowingStoneHigh;
						}
						else if (isLowBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatThrowingStoneLow;
						}
						else
						{
							result = CombatSoundContainer.SoundCodeMissionCombatThrowingStoneMed;
						}
						break;
					case WeaponClass.Boulder:
						if (isCriticalBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatBoulderHigh;
						}
						else if (isLowBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatBoulderLow;
						}
						else
						{
							result = CombatSoundContainer.SoundCodeMissionCombatBoulderMed;
						}
						break;
					case WeaponClass.ThrowingAxe:
						if (isCriticalBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatThrowingAxeHigh;
						}
						else if (isLowBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatThrowingAxeLow;
						}
						else
						{
							result = CombatSoundContainer.SoundCodeMissionCombatThrowingAxeMed;
						}
						break;
					case WeaponClass.ThrowingKnife:
						if (isCriticalBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatThrowingDaggerHigh;
						}
						else if (isLowBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatThrowingDaggerLow;
						}
						else
						{
							result = CombatSoundContainer.SoundCodeMissionCombatThrowingDaggerMed;
						}
						break;
					case WeaponClass.Javelin:
						if (isCriticalBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatMissileHigh;
						}
						else if (isLowBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatMissileLow;
						}
						else
						{
							result = CombatSoundContainer.SoundCodeMissionCombatMissileMed;
						}
						break;
					default:
						if (isCriticalBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatMissileHigh;
						}
						else if (isLowBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatMissileLow;
						}
						else
						{
							result = CombatSoundContainer.SoundCodeMissionCombatMissileMed;
						}
						break;
					}
				}
				else if (this.IsShield)
				{
					if (this._isMaterialMetal)
					{
						result = CombatSoundContainer.SoundCodeMissionCombatMetalShieldBash;
					}
					else
					{
						result = CombatSoundContainer.SoundCodeMissionCombatWoodShieldBash;
					}
				}
				else if (attackType == AgentAttackType.Bash)
				{
					result = CombatSoundContainer.SoundCodeMissionCombatBluntLow;
				}
				else
				{
					if (isNonTipThrust)
					{
						damageType = DamageTypes.Blunt;
					}
					switch (damageType)
					{
					case DamageTypes.Cut:
						if (isCriticalBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatCutHigh;
						}
						else if (isLowBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatCutLow;
						}
						else
						{
							result = CombatSoundContainer.SoundCodeMissionCombatCutMed;
						}
						break;
					case DamageTypes.Pierce:
						if (isCriticalBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatPierceHigh;
						}
						else if (isLowBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatPierceLow;
						}
						else
						{
							result = CombatSoundContainer.SoundCodeMissionCombatPierceMed;
						}
						break;
					case DamageTypes.Blunt:
						if (isCriticalBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatBluntHigh;
						}
						else if (isLowBlow)
						{
							result = CombatSoundContainer.SoundCodeMissionCombatBluntLow;
						}
						else
						{
							result = CombatSoundContainer.SoundCodeMissionCombatBluntMed;
						}
						break;
					default:
						result = CombatSoundContainer.SoundCodeMissionCombatBluntMed;
						Debug.FailedAssert("false", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\BlowWeaponRecord.cs", "GetHitSound", 247);
						break;
					}
				}
			}
			else if (!isOwnerHumanoid)
			{
				result = CombatSoundContainer.SoundCodeMissionCombatChargeDamage;
			}
			else if (attackType == AgentAttackType.Kick)
			{
				result = CombatSoundContainer.SoundCodeMissionCombatKick;
			}
			else if (isCriticalBlow)
			{
				result = CombatSoundContainer.SoundCodeMissionCombatPunchHigh;
			}
			else if (isLowBlow)
			{
				result = CombatSoundContainer.SoundCodeMissionCombatPunchLow;
			}
			else
			{
				result = CombatSoundContainer.SoundCodeMissionCombatPunchMed;
			}
			return result;
		}

		// Token: 0x04000882 RID: 2178
		public Vec3 StartingPosition;

		// Token: 0x04000883 RID: 2179
		public Vec3 CurrentPosition;

		// Token: 0x04000884 RID: 2180
		public Vec3 Velocity;

		// Token: 0x04000885 RID: 2181
		public ItemFlags ItemFlags;

		// Token: 0x04000886 RID: 2182
		public WeaponFlags WeaponFlags;

		// Token: 0x04000887 RID: 2183
		public WeaponClass WeaponClass;

		// Token: 0x04000888 RID: 2184
		public sbyte BoneNoToAttach;

		// Token: 0x04000889 RID: 2185
		public int AffectorWeaponSlotOrMissileIndex;

		// Token: 0x0400088A RID: 2186
		public float Weight;

		// Token: 0x0400088B RID: 2187
		[CustomEngineStructMemberData(true)]
		[MarshalAs(UnmanagedType.U1)]
		private bool _isMissile;

		// Token: 0x0400088C RID: 2188
		[MarshalAs(UnmanagedType.U1)]
		private bool _isMaterialMetal;
	}
}
