using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000177 RID: 375
	public static class QueryLibrary
	{
		// Token: 0x060012FA RID: 4858 RVA: 0x00047BFB File Offset: 0x00045DFB
		public static bool IsInfantry(Agent a)
		{
			return !a.HasMount && !a.IsRangedCached;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00047C10 File Offset: 0x00045E10
		public static bool IsInfantryWithoutBanner(Agent a)
		{
			return a.Banner == null && !a.HasMount && !a.IsRangedCached;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x00047C2D File Offset: 0x00045E2D
		public static bool HasShield(Agent a)
		{
			return a.HasShieldCached;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x00047C35 File Offset: 0x00045E35
		public static bool IsRanged(Agent a)
		{
			return !a.HasMount && a.IsRangedCached;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00047C47 File Offset: 0x00045E47
		public static bool IsRangedWithoutBanner(Agent a)
		{
			return a.Banner == null && !a.HasMount && a.IsRangedCached;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00047C61 File Offset: 0x00045E61
		public static bool IsCavalry(Agent a)
		{
			return a.HasMount && !a.IsRangedCached;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00047C76 File Offset: 0x00045E76
		public static bool IsCavalryWithoutBanner(Agent a)
		{
			return a.Banner == null && a.HasMount && !a.IsRangedCached;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00047C93 File Offset: 0x00045E93
		public static bool IsRangedCavalry(Agent a)
		{
			return a.HasMount && a.IsRangedCached;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00047CA5 File Offset: 0x00045EA5
		public static bool IsRangedCavalryWithoutBanner(Agent a)
		{
			return a.Banner == null && a.HasMount && a.IsRangedCached;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00047CBF File Offset: 0x00045EBF
		public static bool HasSpear(Agent a)
		{
			return a.HasSpearCached;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00047CC7 File Offset: 0x00045EC7
		public static bool HasThrown(Agent a)
		{
			return a.HasThrownCached;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00047CCF File Offset: 0x00045ECF
		public static bool IsHeavy(Agent a)
		{
			return MissionGameModels.Current.AgentStatCalculateModel.HasHeavyArmor(a);
		}
	}
}
