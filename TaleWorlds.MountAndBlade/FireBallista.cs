using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000334 RID: 820
	public class FireBallista : Ballista
	{
		// Token: 0x06002C48 RID: 11336 RVA: 0x000AD8EA File Offset: 0x000ABAEA
		public override SiegeEngineType GetSiegeEngineType()
		{
			return DefaultSiegeEngineTypes.FireBallista;
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000AD8F4 File Offset: 0x000ABAF4
		public override float ProcessTargetValue(float baseValue, TargetFlags flags)
		{
			if (flags.HasAnyFlag(TargetFlags.NotAThreat))
			{
				return -1000f;
			}
			if (flags.HasAnyFlag(TargetFlags.None))
			{
				baseValue *= 1.5f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsSiegeEngine))
			{
				baseValue *= 2.5f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsStructure))
			{
				baseValue *= 0.1f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsFlammable))
			{
				baseValue *= 2f;
			}
			if (flags.HasAnyFlag(TargetFlags.DebugThreat))
			{
				baseValue *= 1000f;
			}
			return baseValue;
		}
	}
}
