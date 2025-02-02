using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000190 RID: 400
	[EngineStruct("Attack_collision_data", false)]
	public struct AttackCollisionData
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x0004D6C0 File Offset: 0x0004B8C0
		public bool AttackBlockedWithShield
		{
			get
			{
				return this._attackBlockedWithShield;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x0004D6C8 File Offset: 0x0004B8C8
		public bool CorrectSideShieldBlock
		{
			get
			{
				return this._correctSideShieldBlock;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x0004D6D0 File Offset: 0x0004B8D0
		public bool IsAlternativeAttack
		{
			get
			{
				return this._isAlternativeAttack;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001417 RID: 5143 RVA: 0x0004D6D8 File Offset: 0x0004B8D8
		public bool IsColliderAgent
		{
			get
			{
				return this._isColliderAgent;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0004D6E0 File Offset: 0x0004B8E0
		public bool CollidedWithShieldOnBack
		{
			get
			{
				return this._collidedWithShieldOnBack;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001419 RID: 5145 RVA: 0x0004D6E8 File Offset: 0x0004B8E8
		public bool IsMissile
		{
			get
			{
				return this._isMissile;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600141A RID: 5146 RVA: 0x0004D6F0 File Offset: 0x0004B8F0
		public bool MissileBlockedWithWeapon
		{
			get
			{
				return this._missileBlockedWithWeapon;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x0600141B RID: 5147 RVA: 0x0004D6F8 File Offset: 0x0004B8F8
		public bool MissileHasPhysics
		{
			get
			{
				return this._missileHasPhysics;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600141C RID: 5148 RVA: 0x0004D700 File Offset: 0x0004B900
		public bool EntityExists
		{
			get
			{
				return this._entityExists;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x0600141D RID: 5149 RVA: 0x0004D708 File Offset: 0x0004B908
		public bool ThrustTipHit
		{
			get
			{
				return this._thrustTipHit;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0004D710 File Offset: 0x0004B910
		public bool MissileGoneUnderWater
		{
			get
			{
				return this._missileGoneUnderWater;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x0600141F RID: 5151 RVA: 0x0004D718 File Offset: 0x0004B918
		public bool MissileGoneOutOfBorder
		{
			get
			{
				return this._missileGoneOutOfBorder;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x0004D720 File Offset: 0x0004B920
		public bool IsHorseCharge
		{
			get
			{
				return this.ChargeVelocity > 0f;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001421 RID: 5153 RVA: 0x0004D72F File Offset: 0x0004B92F
		public bool IsFallDamage
		{
			get
			{
				return this.FallSpeed > 0f;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x0004D73E File Offset: 0x0004B93E
		public CombatCollisionResult CollisionResult
		{
			get
			{
				return (CombatCollisionResult)this._collisionResult;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0004D746 File Offset: 0x0004B946
		public int AffectorWeaponSlotOrMissileIndex { get; }

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x0004D74E File Offset: 0x0004B94E
		public int StrikeType { get; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0004D756 File Offset: 0x0004B956
		public int DamageType { get; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0004D75E File Offset: 0x0004B95E
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0004D766 File Offset: 0x0004B966
		public sbyte CollisionBoneIndex { get; private set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001428 RID: 5160 RVA: 0x0004D76F File Offset: 0x0004B96F
		public BoneBodyPartType VictimHitBodyPart { get; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0004D777 File Offset: 0x0004B977
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x0004D77F File Offset: 0x0004B97F
		public sbyte AttackBoneIndex { get; private set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0004D788 File Offset: 0x0004B988
		public Agent.UsageDirection AttackDirection { get; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x0004D790 File Offset: 0x0004B990
		// (set) Token: 0x0600142D RID: 5165 RVA: 0x0004D798 File Offset: 0x0004B998
		public int PhysicsMaterialIndex { get; private set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x0600142E RID: 5166 RVA: 0x0004D7A1 File Offset: 0x0004B9A1
		// (set) Token: 0x0600142F RID: 5167 RVA: 0x0004D7A9 File Offset: 0x0004B9A9
		public CombatHitResultFlags CollisionHitResultFlags { get; private set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001430 RID: 5168 RVA: 0x0004D7B2 File Offset: 0x0004B9B2
		public float AttackProgress { get; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0004D7BA File Offset: 0x0004B9BA
		public float CollisionDistanceOnWeapon { get; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x0004D7C2 File Offset: 0x0004B9C2
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x0004D7CA File Offset: 0x0004B9CA
		public float AttackerStunPeriod { get; set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001434 RID: 5172 RVA: 0x0004D7D3 File Offset: 0x0004B9D3
		// (set) Token: 0x06001435 RID: 5173 RVA: 0x0004D7DB File Offset: 0x0004B9DB
		public float DefenderStunPeriod { get; set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001436 RID: 5174 RVA: 0x0004D7E4 File Offset: 0x0004B9E4
		public float MissileTotalDamage { get; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0004D7EC File Offset: 0x0004B9EC
		public float MissileStartingBaseSpeed { get; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001438 RID: 5176 RVA: 0x0004D7F4 File Offset: 0x0004B9F4
		public float ChargeVelocity { get; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001439 RID: 5177 RVA: 0x0004D7FC File Offset: 0x0004B9FC
		// (set) Token: 0x0600143A RID: 5178 RVA: 0x0004D804 File Offset: 0x0004BA04
		public float FallSpeed { get; private set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600143B RID: 5179 RVA: 0x0004D80D File Offset: 0x0004BA0D
		public Vec3 WeaponRotUp { get; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x0004D815 File Offset: 0x0004BA15
		public Vec3 WeaponBlowDir
		{
			get
			{
				return this._weaponBlowDir;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600143D RID: 5181 RVA: 0x0004D81D File Offset: 0x0004BA1D
		// (set) Token: 0x0600143E RID: 5182 RVA: 0x0004D825 File Offset: 0x0004BA25
		public Vec3 CollisionGlobalPosition { get; private set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600143F RID: 5183 RVA: 0x0004D82E File Offset: 0x0004BA2E
		public Vec3 MissileVelocity { get; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x0004D836 File Offset: 0x0004BA36
		public Vec3 MissileStartingPosition { get; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001441 RID: 5185 RVA: 0x0004D83E File Offset: 0x0004BA3E
		public Vec3 VictimAgentCurVelocity { get; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x0004D846 File Offset: 0x0004BA46
		public Vec3 CollisionGlobalNormal { get; }

		// Token: 0x06001443 RID: 5187 RVA: 0x0004D84E File Offset: 0x0004BA4E
		public void SetCollisionBoneIndexForAreaDamage(sbyte boneIndex)
		{
			this.CollisionBoneIndex = boneIndex;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0004D857 File Offset: 0x0004BA57
		public void UpdateCollisionPositionAndBoneForReflect(int inflictedDamage, Vec3 position, sbyte boneIndex)
		{
			this.InflictedDamage = inflictedDamage;
			this.CollisionGlobalPosition = position;
			this.AttackBoneIndex = boneIndex;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0004D870 File Offset: 0x0004BA70
		private AttackCollisionData(bool attackBlockedWithShield, bool correctSideShieldBlock, bool isAlternativeAttack, bool isColliderAgent, bool collidedWithShieldOnBack, bool isMissile, bool missileBlockedWithWeapon, bool missileHasPhysics, bool entityExists, bool thrustTipHit, bool missileGoneUnderWater, bool missileGoneOutOfBorder, CombatCollisionResult collisionResult, int affectorWeaponSlotOrMissileIndex, int StrikeType, int DamageType, sbyte CollisionBoneIndex, BoneBodyPartType VictimHitBodyPart, sbyte AttackBoneIndex, Agent.UsageDirection AttackDirection, int PhysicsMaterialIndex, CombatHitResultFlags CollisionHitResultFlags, float AttackProgress, float CollisionDistanceOnWeapon, float AttackerStunPeriod, float DefenderStunPeriod, float MissileTotalDamage, float MissileStartingBaseSpeed, float ChargeVelocity, float FallSpeed, Vec3 WeaponRotUp, Vec3 weaponBlowDir, Vec3 CollisionGlobalPosition, Vec3 MissileVelocity, Vec3 MissileStartingPosition, Vec3 VictimAgentCurVelocity, Vec3 GroundNormal)
		{
			this._attackBlockedWithShield = attackBlockedWithShield;
			this._correctSideShieldBlock = correctSideShieldBlock;
			this._isAlternativeAttack = isAlternativeAttack;
			this._isColliderAgent = isColliderAgent;
			this._collidedWithShieldOnBack = collidedWithShieldOnBack;
			this._isMissile = isMissile;
			this._missileBlockedWithWeapon = missileBlockedWithWeapon;
			this._missileHasPhysics = missileHasPhysics;
			this._entityExists = entityExists;
			this._thrustTipHit = thrustTipHit;
			this._missileGoneUnderWater = missileGoneUnderWater;
			this._missileGoneOutOfBorder = missileGoneOutOfBorder;
			this._collisionResult = (int)collisionResult;
			this.AffectorWeaponSlotOrMissileIndex = affectorWeaponSlotOrMissileIndex;
			this.StrikeType = StrikeType;
			this.DamageType = DamageType;
			this.CollisionBoneIndex = CollisionBoneIndex;
			this.VictimHitBodyPart = VictimHitBodyPart;
			this.AttackBoneIndex = AttackBoneIndex;
			this.AttackDirection = AttackDirection;
			this.PhysicsMaterialIndex = PhysicsMaterialIndex;
			this.CollisionHitResultFlags = CollisionHitResultFlags;
			this.AttackProgress = AttackProgress;
			this.CollisionDistanceOnWeapon = CollisionDistanceOnWeapon;
			this.AttackerStunPeriod = AttackerStunPeriod;
			this.DefenderStunPeriod = DefenderStunPeriod;
			this.MissileTotalDamage = MissileTotalDamage;
			this.MissileStartingBaseSpeed = MissileStartingBaseSpeed;
			this.ChargeVelocity = ChargeVelocity;
			this.FallSpeed = FallSpeed;
			this.WeaponRotUp = WeaponRotUp;
			this._weaponBlowDir = weaponBlowDir;
			this.CollisionGlobalPosition = CollisionGlobalPosition;
			this.MissileVelocity = MissileVelocity;
			this.MissileStartingPosition = MissileStartingPosition;
			this.VictimAgentCurVelocity = VictimAgentCurVelocity;
			this.CollisionGlobalNormal = GroundNormal;
			this.BaseMagnitude = 0f;
			this.MovementSpeedDamageModifier = 0f;
			this.AbsorbedByArmor = 0;
			this.InflictedDamage = 0;
			this.SelfInflictedDamage = 0;
			this.IsShieldBroken = false;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0004D9D4 File Offset: 0x0004BBD4
		public static AttackCollisionData GetAttackCollisionDataForDebugPurpose(bool _attackBlockedWithShield, bool _correctSideShieldBlock, bool _isAlternativeAttack, bool _isColliderAgent, bool _collidedWithShieldOnBack, bool _isMissile, bool _isMissileBlockedWithWeapon, bool _missileHasPhysics, bool _entityExists, bool _thrustTipHit, bool _missileGoneUnderWater, bool _missileGoneOutOfBorder, CombatCollisionResult collisionResult, int affectorWeaponSlotOrMissileIndex, int StrikeType, int DamageType, sbyte CollisionBoneIndex, BoneBodyPartType VictimHitBodyPart, sbyte AttackBoneIndex, Agent.UsageDirection AttackDirection, int PhysicsMaterialIndex, CombatHitResultFlags CollisionHitResultFlags, float AttackProgress, float CollisionDistanceOnWeapon, float AttackerStunPeriod, float DefenderStunPeriod, float MissileTotalDamage, float MissileInitialSpeed, float ChargeVelocity, float FallSpeed, Vec3 WeaponRotUp, Vec3 _weaponBlowDir, Vec3 CollisionGlobalPosition, Vec3 MissileVelocity, Vec3 MissileStartingPosition, Vec3 VictimAgentCurVelocity, Vec3 GroundNormal)
		{
			return new AttackCollisionData(_attackBlockedWithShield, _correctSideShieldBlock, _isAlternativeAttack, _isColliderAgent, _collidedWithShieldOnBack, _isMissile, _isMissileBlockedWithWeapon, _missileHasPhysics, _entityExists, _thrustTipHit, _missileGoneUnderWater, _missileGoneOutOfBorder, collisionResult, affectorWeaponSlotOrMissileIndex, StrikeType, DamageType, CollisionBoneIndex, VictimHitBodyPart, AttackBoneIndex, AttackDirection, PhysicsMaterialIndex, CollisionHitResultFlags, AttackProgress, CollisionDistanceOnWeapon, AttackerStunPeriod, DefenderStunPeriod, MissileTotalDamage, MissileInitialSpeed, ChargeVelocity, FallSpeed, WeaponRotUp, _weaponBlowDir, CollisionGlobalPosition, MissileVelocity, MissileStartingPosition, VictimAgentCurVelocity, GroundNormal);
		}

		// Token: 0x04000610 RID: 1552
		[MarshalAs(UnmanagedType.U1)]
		private bool _attackBlockedWithShield;

		// Token: 0x04000611 RID: 1553
		[MarshalAs(UnmanagedType.U1)]
		private bool _correctSideShieldBlock;

		// Token: 0x04000612 RID: 1554
		[MarshalAs(UnmanagedType.U1)]
		private bool _isAlternativeAttack;

		// Token: 0x04000613 RID: 1555
		[MarshalAs(UnmanagedType.U1)]
		private bool _isColliderAgent;

		// Token: 0x04000614 RID: 1556
		[MarshalAs(UnmanagedType.U1)]
		private bool _collidedWithShieldOnBack;

		// Token: 0x04000615 RID: 1557
		[MarshalAs(UnmanagedType.U1)]
		private bool _isMissile;

		// Token: 0x04000616 RID: 1558
		[MarshalAs(UnmanagedType.U1)]
		private bool _missileBlockedWithWeapon;

		// Token: 0x04000617 RID: 1559
		[MarshalAs(UnmanagedType.U1)]
		private bool _missileHasPhysics;

		// Token: 0x04000618 RID: 1560
		[MarshalAs(UnmanagedType.U1)]
		private bool _entityExists;

		// Token: 0x04000619 RID: 1561
		[MarshalAs(UnmanagedType.U1)]
		private bool _thrustTipHit;

		// Token: 0x0400061A RID: 1562
		[MarshalAs(UnmanagedType.U1)]
		private bool _missileGoneUnderWater;

		// Token: 0x0400061B RID: 1563
		[MarshalAs(UnmanagedType.U1)]
		private bool _missileGoneOutOfBorder;

		// Token: 0x0400061C RID: 1564
		private int _collisionResult;

		// Token: 0x0400062F RID: 1583
		private Vec3 _weaponBlowDir;

		// Token: 0x04000635 RID: 1589
		[CustomEngineStructMemberData(true)]
		public float BaseMagnitude;

		// Token: 0x04000636 RID: 1590
		[CustomEngineStructMemberData(true)]
		public float MovementSpeedDamageModifier;

		// Token: 0x04000637 RID: 1591
		[CustomEngineStructMemberData(true)]
		public int AbsorbedByArmor;

		// Token: 0x04000638 RID: 1592
		[CustomEngineStructMemberData(true)]
		public int InflictedDamage;

		// Token: 0x04000639 RID: 1593
		[CustomEngineStructMemberData(true)]
		public int SelfInflictedDamage;

		// Token: 0x0400063A RID: 1594
		[CustomEngineStructMemberData(true)]
		[MarshalAs(UnmanagedType.U1)]
		public bool IsShieldBroken;
	}
}
