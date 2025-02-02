using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000335 RID: 821
	public class FireMangonel : Mangonel
	{
		// Token: 0x06002C4B RID: 11339 RVA: 0x000AD978 File Offset: 0x000ABB78
		public override SiegeEngineType GetSiegeEngineType()
		{
			if (this._defaultSide != BattleSideEnum.Attacker)
			{
				return DefaultSiegeEngineTypes.FireCatapult;
			}
			return DefaultSiegeEngineTypes.FireOnager;
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000AD990 File Offset: 0x000ABB90
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
				baseValue *= 12f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsStructure))
			{
				baseValue *= 1.5f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsSmall))
			{
				baseValue *= 8f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsMoving))
			{
				baseValue *= 12f;
			}
			if (flags.HasAnyFlag(TargetFlags.DebugThreat))
			{
				baseValue *= 10f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsSiegeTower))
			{
				baseValue *= 12f;
			}
			if (flags.HasAnyFlag(TargetFlags.IsFlammable))
			{
				baseValue *= 100f;
			}
			return baseValue;
		}
	}
}
