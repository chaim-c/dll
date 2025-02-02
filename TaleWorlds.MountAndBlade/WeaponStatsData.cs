using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200037D RID: 893
	[EngineStruct("Weapon_stats_data", false)]
	public struct WeaponStatsData
	{
		// Token: 0x04001503 RID: 5379
		public MatrixFrame WeaponFrame;

		// Token: 0x04001504 RID: 5380
		public Vec3 RotationSpeed;

		// Token: 0x04001505 RID: 5381
		public ulong WeaponFlags;

		// Token: 0x04001506 RID: 5382
		public uint Properties;

		// Token: 0x04001507 RID: 5383
		public int WeaponClass;

		// Token: 0x04001508 RID: 5384
		public int AmmoClass;

		// Token: 0x04001509 RID: 5385
		public int ItemUsageIndex;

		// Token: 0x0400150A RID: 5386
		public int ThrustSpeed;

		// Token: 0x0400150B RID: 5387
		public int SwingSpeed;

		// Token: 0x0400150C RID: 5388
		public int MissileSpeed;

		// Token: 0x0400150D RID: 5389
		public int ShieldArmor;

		// Token: 0x0400150E RID: 5390
		public int ThrustDamage;

		// Token: 0x0400150F RID: 5391
		public int SwingDamage;

		// Token: 0x04001510 RID: 5392
		public int DefendSpeed;

		// Token: 0x04001511 RID: 5393
		public int Accuracy;

		// Token: 0x04001512 RID: 5394
		public int WeaponLength;

		// Token: 0x04001513 RID: 5395
		public float WeaponBalance;

		// Token: 0x04001514 RID: 5396
		public float SweetSpot;

		// Token: 0x04001515 RID: 5397
		public short MaxDataValue;

		// Token: 0x04001516 RID: 5398
		public short ReloadPhaseCount;

		// Token: 0x04001517 RID: 5399
		public int ThrustDamageType;

		// Token: 0x04001518 RID: 5400
		public int SwingDamageType;
	}
}
