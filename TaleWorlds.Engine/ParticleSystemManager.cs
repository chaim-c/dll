using System;

namespace TaleWorlds.Engine
{
	// Token: 0x02000070 RID: 112
	public sealed class ParticleSystemManager
	{
		// Token: 0x060008B7 RID: 2231 RVA: 0x00008B35 File Offset: 0x00006D35
		public static int GetRuntimeIdByName(string particleSystemName)
		{
			return EngineApplicationInterface.IParticleSystem.GetRuntimeIdByName(particleSystemName);
		}
	}
}
