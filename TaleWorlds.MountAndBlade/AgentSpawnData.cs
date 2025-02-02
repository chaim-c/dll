using System;
using TaleWorlds.DotNet;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000102 RID: 258
	[EngineStruct("Agent_spawn_data", false)]
	public struct AgentSpawnData
	{
		// Token: 0x040002B8 RID: 696
		public int HitPoints;

		// Token: 0x040002B9 RID: 697
		public int MonsterUsageIndex;

		// Token: 0x040002BA RID: 698
		public int Weight;

		// Token: 0x040002BB RID: 699
		public float StandingChestHeight;

		// Token: 0x040002BC RID: 700
		public float StandingPelvisHeight;

		// Token: 0x040002BD RID: 701
		public float StandingEyeHeight;

		// Token: 0x040002BE RID: 702
		public float CrouchEyeHeight;

		// Token: 0x040002BF RID: 703
		public float MountedEyeHeight;

		// Token: 0x040002C0 RID: 704
		public float RiderEyeHeightAdder;

		// Token: 0x040002C1 RID: 705
		public float JumpAcceleration;

		// Token: 0x040002C2 RID: 706
		public Vec3 EyeOffsetWrtHead;

		// Token: 0x040002C3 RID: 707
		public Vec3 FirstPersonCameraOffsetWrtHead;

		// Token: 0x040002C4 RID: 708
		public float RiderCameraHeightAdder;

		// Token: 0x040002C5 RID: 709
		public float RiderBodyCapsuleHeightAdder;

		// Token: 0x040002C6 RID: 710
		public float RiderBodyCapsuleForwardAdder;

		// Token: 0x040002C7 RID: 711
		public float ArmLength;

		// Token: 0x040002C8 RID: 712
		public float ArmWeight;

		// Token: 0x040002C9 RID: 713
		public float JumpSpeedLimit;

		// Token: 0x040002CA RID: 714
		public float RelativeSpeedLimitForCharge;
	}
}
