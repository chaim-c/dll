using System;
using System.Runtime.InteropServices;
using TaleWorlds.Core;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000249 RID: 585
	[EngineStruct("Killing_blow", false)]
	public struct KillingBlow
	{
		// Token: 0x06001F69 RID: 8041 RVA: 0x0006F464 File Offset: 0x0006D664
		public KillingBlow(Blow b, Vec3 ragdollImpulsePoint, Vec3 ragdollImpulseAmount, int deathAction, int weaponItemKind, Agent.KillInfo overrideKillInfo = Agent.KillInfo.Invalid)
		{
			this.RagdollImpulseLocalPoint = ragdollImpulsePoint;
			this.RagdollImpulseAmount = ragdollImpulseAmount;
			this.DeathAction = deathAction;
			this.OverrideKillInfo = overrideKillInfo;
			this.DamageType = b.DamageType;
			this.AttackType = b.AttackType;
			this.OwnerId = b.OwnerId;
			this.VictimBodyPart = b.VictimBodyPart;
			this.WeaponClass = (int)b.WeaponRecord.WeaponClass;
			this.BlowPosition = b.GlobalPosition;
			this.WeaponRecordWeaponFlags = b.WeaponRecord.WeaponFlags;
			this.WeaponItemKind = weaponItemKind;
			this.InflictedDamage = b.InflictedDamage;
			this.IsMissile = b.IsMissile;
			this.IsValid = true;
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0006F515 File Offset: 0x0006D715
		public bool IsHeadShot()
		{
			return this.VictimBodyPart == BoneBodyPartType.Head;
		}

		// Token: 0x04000B90 RID: 2960
		public Vec3 RagdollImpulseLocalPoint;

		// Token: 0x04000B91 RID: 2961
		public Vec3 RagdollImpulseAmount;

		// Token: 0x04000B92 RID: 2962
		public int DeathAction;

		// Token: 0x04000B93 RID: 2963
		public DamageTypes DamageType;

		// Token: 0x04000B94 RID: 2964
		public AgentAttackType AttackType;

		// Token: 0x04000B95 RID: 2965
		public int OwnerId;

		// Token: 0x04000B96 RID: 2966
		public BoneBodyPartType VictimBodyPart;

		// Token: 0x04000B97 RID: 2967
		public int WeaponClass;

		// Token: 0x04000B98 RID: 2968
		public Agent.KillInfo OverrideKillInfo;

		// Token: 0x04000B99 RID: 2969
		public Vec3 BlowPosition;

		// Token: 0x04000B9A RID: 2970
		public WeaponFlags WeaponRecordWeaponFlags;

		// Token: 0x04000B9B RID: 2971
		public int WeaponItemKind;

		// Token: 0x04000B9C RID: 2972
		public int InflictedDamage;

		// Token: 0x04000B9D RID: 2973
		[MarshalAs(UnmanagedType.U1)]
		public bool IsMissile;

		// Token: 0x04000B9E RID: 2974
		[MarshalAs(UnmanagedType.U1)]
		public bool IsValid;
	}
}
