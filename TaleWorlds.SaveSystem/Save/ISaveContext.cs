using System;
using TaleWorlds.SaveSystem.Definition;

namespace TaleWorlds.SaveSystem.Save
{
	// Token: 0x02000029 RID: 41
	internal interface ISaveContext
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600016B RID: 363
		DefinitionContext DefinitionContext { get; }

		// Token: 0x0600016C RID: 364
		int AddOrGetStringId(string text);

		// Token: 0x0600016D RID: 365
		int GetObjectId(object target);

		// Token: 0x0600016E RID: 366
		int GetContainerId(object target);

		// Token: 0x0600016F RID: 367
		int GetStringId(string target);
	}
}
