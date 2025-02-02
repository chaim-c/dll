using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E2 RID: 482
	[EngineStruct("Blow", false)]
	public struct Blow
	{
		// Token: 0x06001AEE RID: 6894 RVA: 0x0005D52D File Offset: 0x0005B72D
		public Blow(int ownerId)
		{
			this = default(Blow);
			this.OwnerId = ownerId;
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x0005D53D File Offset: 0x0005B73D
		public bool IsMissile
		{
			get
			{
				return this.WeaponRecord.IsMissile;
			}
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x0005D54A File Offset: 0x0005B74A
		public bool IsBlowCrit(int maxHitPointsOfVictim)
		{
			return (float)this.InflictedDamage > (float)maxHitPointsOfVictim * 0.5f;
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x0005D55D File Offset: 0x0005B75D
		public bool IsBlowLow(int maxHitPointsOfVictim)
		{
			return (float)this.InflictedDamage <= (float)maxHitPointsOfVictim * 0.1f;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x0005D573 File Offset: 0x0005B773
		public bool IsHeadShot()
		{
			return this.VictimBodyPart == BoneBodyPartType.Head;
		}

		// Token: 0x04000862 RID: 2146
		public BlowWeaponRecord WeaponRecord;

		// Token: 0x04000863 RID: 2147
		public Vec3 GlobalPosition;

		// Token: 0x04000864 RID: 2148
		public Vec3 Direction;

		// Token: 0x04000865 RID: 2149
		public Vec3 SwingDirection;

		// Token: 0x04000866 RID: 2150
		public int InflictedDamage;

		// Token: 0x04000867 RID: 2151
		public int SelfInflictedDamage;

		// Token: 0x04000868 RID: 2152
		public float BaseMagnitude;

		// Token: 0x04000869 RID: 2153
		public float DefenderStunPeriod;

		// Token: 0x0400086A RID: 2154
		public float AttackerStunPeriod;

		// Token: 0x0400086B RID: 2155
		public float AbsorbedByArmor;

		// Token: 0x0400086C RID: 2156
		public float MovementSpeedDamageModifier;

		// Token: 0x0400086D RID: 2157
		public StrikeType StrikeType;

		// Token: 0x0400086E RID: 2158
		public AgentAttackType AttackType;

		// Token: 0x0400086F RID: 2159
		[CustomEngineStructMemberData("blow_flags")]
		public BlowFlags BlowFlag;

		// Token: 0x04000870 RID: 2160
		public int OwnerId;

		// Token: 0x04000871 RID: 2161
		public sbyte BoneIndex;

		// Token: 0x04000872 RID: 2162
		public BoneBodyPartType VictimBodyPart;

		// Token: 0x04000873 RID: 2163
		public DamageTypes DamageType;

		// Token: 0x04000874 RID: 2164
		[MarshalAs(UnmanagedType.U1)]
		public bool NoIgnore;

		// Token: 0x04000875 RID: 2165
		[MarshalAs(UnmanagedType.U1)]
		public bool DamageCalculated;

		// Token: 0x04000876 RID: 2166
		[MarshalAs(UnmanagedType.U1)]
		public bool IsFallDamage;

		// Token: 0x04000877 RID: 2167
		public float DamagedPercentage;
	}
}
