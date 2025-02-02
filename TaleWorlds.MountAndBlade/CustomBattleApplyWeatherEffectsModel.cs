using System;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001F0 RID: 496
	public class CustomBattleApplyWeatherEffectsModel : ApplyWeatherEffectsModel
	{
		// Token: 0x06001BF1 RID: 7153 RVA: 0x00060AA0 File Offset: 0x0005ECA0
		public override void ApplyWeatherEffects()
		{
			Scene scene = Mission.Current.Scene;
			if (scene != null)
			{
				bool flag = scene.GetRainDensity() > 0f;
				bool flag2 = scene.GetSnowDensity() > 0f;
				bool flag3 = flag || flag2;
				bool flag4 = scene.GetFog() > 0f;
				Mission.Current.SetBowMissileSpeedModifier(flag3 ? 0.9f : 1f);
				Mission.Current.SetCrossbowMissileSpeedModifier(flag3 ? 0.9f : 1f);
				Mission.Current.SetMissileRangeModifier(flag4 ? 0.8f : 1f);
			}
		}
	}
}
