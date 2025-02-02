using System;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace MBHelpers
{
	// Token: 0x020000DA RID: 218
	public static class BannerHelper
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x0000F0AE File Offset: 0x0000D2AE
		public static void AddBannerBonusForBanner(BannerEffect bannerEffect, BannerComponent bannerComponent, ref FactoredNumber bonuses)
		{
			if (bannerComponent != null && bannerComponent.BannerEffect == bannerEffect)
			{
				BannerHelper.AddBannerEffectToStat(ref bonuses, bannerEffect.IncrementType, bannerComponent.GetBannerEffectBonus());
			}
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		private static void AddBannerEffectToStat(ref FactoredNumber stat, BannerEffect.EffectIncrementType effectIncrementType, float number)
		{
			if (effectIncrementType == BannerEffect.EffectIncrementType.Add)
			{
				stat.Add(number);
				return;
			}
			if (effectIncrementType == BannerEffect.EffectIncrementType.AddFactor)
			{
				stat.AddFactor(number);
			}
		}
	}
}
