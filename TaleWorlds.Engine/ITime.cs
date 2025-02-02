using System;
using TaleWorlds.Library;

namespace TaleWorlds.Engine
{
	// Token: 0x0200003B RID: 59
	[ApplicationInterfaceBase]
	internal interface ITime
	{
		// Token: 0x0600053C RID: 1340
		[EngineMethod("get_application_time", false)]
		float GetApplicationTime();
	}
}
