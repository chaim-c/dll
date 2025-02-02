using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000373 RID: 883
	public class RandomTimer : Timer
	{
		// Token: 0x06003096 RID: 12438 RVA: 0x000C9B07 File Offset: 0x000C7D07
		public RandomTimer(float gameTime, float durationMin, float durationMax) : base(gameTime, MBRandom.RandomFloatRanged(durationMin, durationMax), true)
		{
			this.durationMin = durationMin;
			this.durationMax = durationMax;
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x000C9B28 File Offset: 0x000C7D28
		public override bool Check(float gameTime)
		{
			bool result = false;
			bool flag;
			do
			{
				flag = base.Check(gameTime);
				if (flag)
				{
					this.RecomputeDuration();
					result = true;
				}
			}
			while (flag);
			return result;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000C9B4E File Offset: 0x000C7D4E
		public void ChangeDuration(float min, float max)
		{
			this.durationMin = min;
			this.durationMax = max;
			this.RecomputeDuration();
		}

		// Token: 0x06003099 RID: 12441 RVA: 0x000C9B64 File Offset: 0x000C7D64
		public void RecomputeDuration()
		{
			base.Duration = MBRandom.RandomFloatRanged(this.durationMin, this.durationMax);
		}

		// Token: 0x040014BA RID: 5306
		private float durationMin;

		// Token: 0x040014BB RID: 5307
		private float durationMax;
	}
}
