using System;
using TaleWorlds.DotNet;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000191 RID: 401
	[EngineStruct("Hit_particle_result_data", false)]
	public struct HitParticleResultData
	{
		// Token: 0x06001447 RID: 5191 RVA: 0x0004DA2C File Offset: 0x0004BC2C
		public void Reset()
		{
			this.StartHitParticleIndex = -1;
			this.ContinueHitParticleIndex = -1;
			this.EndHitParticleIndex = -1;
		}

		// Token: 0x0400063B RID: 1595
		public int StartHitParticleIndex;

		// Token: 0x0400063C RID: 1596
		public int ContinueHitParticleIndex;

		// Token: 0x0400063D RID: 1597
		public int EndHitParticleIndex;
	}
}
