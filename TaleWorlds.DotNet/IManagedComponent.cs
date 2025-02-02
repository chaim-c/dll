using System;

namespace TaleWorlds.DotNet
{
	// Token: 0x02000016 RID: 22
	public interface IManagedComponent
	{
		// Token: 0x0600005C RID: 92
		void OnCustomCallbackMethodPassed(string name, Delegate method);

		// Token: 0x0600005D RID: 93
		void OnStart();

		// Token: 0x0600005E RID: 94
		void OnApplicationTick(float dt);
	}
}
