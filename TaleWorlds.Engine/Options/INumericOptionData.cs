using System;

namespace TaleWorlds.Engine.Options
{
	// Token: 0x0200009D RID: 157
	public interface INumericOptionData : IOptionData
	{
		// Token: 0x06000BDB RID: 3035
		float GetMinValue();

		// Token: 0x06000BDC RID: 3036
		float GetMaxValue();

		// Token: 0x06000BDD RID: 3037
		bool GetIsDiscrete();

		// Token: 0x06000BDE RID: 3038
		int GetDiscreteIncrementInterval();

		// Token: 0x06000BDF RID: 3039
		bool GetShouldUpdateContinuously();
	}
}
