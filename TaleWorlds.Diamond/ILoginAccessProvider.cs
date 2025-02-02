using System;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000012 RID: 18
	public interface ILoginAccessProvider
	{
		// Token: 0x06000053 RID: 83
		void Initialize(string preferredUserName, PlatformInitParams initParams);

		// Token: 0x06000054 RID: 84
		string GetUserName();

		// Token: 0x06000055 RID: 85
		PlayerId GetPlayerId();

		// Token: 0x06000056 RID: 86
		AccessObjectResult CreateAccessObject();
	}
}
