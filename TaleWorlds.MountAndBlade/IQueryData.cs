using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000175 RID: 373
	public interface IQueryData
	{
		// Token: 0x060012ED RID: 4845
		void Expire();

		// Token: 0x060012EE RID: 4846
		void Evaluate(float currentTime);

		// Token: 0x060012EF RID: 4847
		void SetSyncGroup(IQueryData[] syncGroup);
	}
}
