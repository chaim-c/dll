using System;
using System.Runtime.InteropServices;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200018A RID: 394
	[EngineStruct("Animation_system_bone_data_biped", false)]
	[Serializable]
	public struct AnimationSystemBoneDataBiped
	{
		// Token: 0x040005C7 RID: 1479
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public sbyte[] RagdollStationaryCheckBoneIndices;

		// Token: 0x040005C8 RID: 1480
		public sbyte RagdollStationaryCheckBoneCount;

		// Token: 0x040005C9 RID: 1481
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
		public sbyte[] MoveAdderBoneIndices;

		// Token: 0x040005CA RID: 1482
		public sbyte MoveAdderBoneCount;

		// Token: 0x040005CB RID: 1483
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public sbyte[] SplashDecalBoneIndices;

		// Token: 0x040005CC RID: 1484
		public sbyte SplashDecalBoneCount;

		// Token: 0x040005CD RID: 1485
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public sbyte[] BloodBurstBoneIndices;

		// Token: 0x040005CE RID: 1486
		public sbyte BloodBurstBoneCount;

		// Token: 0x040005CF RID: 1487
		public sbyte MainHandBoneIndex;

		// Token: 0x040005D0 RID: 1488
		public sbyte OffHandBoneIndex;

		// Token: 0x040005D1 RID: 1489
		public sbyte MainHandItemBoneIndex;

		// Token: 0x040005D2 RID: 1490
		public sbyte OffHandItemBoneIndex;

		// Token: 0x040005D3 RID: 1491
		public sbyte MainHandItemSecondaryBoneIndex;

		// Token: 0x040005D4 RID: 1492
		public sbyte OffHandItemSecondaryBoneIndex;

		// Token: 0x040005D5 RID: 1493
		public sbyte OffHandShoulderBoneIndex;

		// Token: 0x040005D6 RID: 1494
		public sbyte HandNumBonesForIk;

		// Token: 0x040005D7 RID: 1495
		public sbyte PrimaryFootBoneIndex;

		// Token: 0x040005D8 RID: 1496
		public sbyte SecondaryFootBoneIndex;

		// Token: 0x040005D9 RID: 1497
		public sbyte RightFootIkEndEffectorBoneIndex;

		// Token: 0x040005DA RID: 1498
		public sbyte LeftFootIkEndEffectorBoneIndex;

		// Token: 0x040005DB RID: 1499
		public sbyte RightFootIkTipBoneIndex;

		// Token: 0x040005DC RID: 1500
		public sbyte LeftFootIkTipBoneIndex;

		// Token: 0x040005DD RID: 1501
		public sbyte FootNumBonesForIk;
	}
}
