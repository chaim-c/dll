using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000140 RID: 320
	public interface IFormationUnit
	{
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000F76 RID: 3958
		IFormationArrangement Formation { get; }

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000F77 RID: 3959
		// (set) Token: 0x06000F78 RID: 3960
		int FormationFileIndex { get; set; }

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000F79 RID: 3961
		// (set) Token: 0x06000F7A RID: 3962
		int FormationRankIndex { get; set; }

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000F7B RID: 3963
		IFormationUnit FollowedUnit { get; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000F7C RID: 3964
		bool IsShieldUsageEncouraged { get; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000F7D RID: 3965
		bool IsPlayerUnit { get; }
	}
}
