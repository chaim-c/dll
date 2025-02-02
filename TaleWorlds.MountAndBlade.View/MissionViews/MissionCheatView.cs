using System;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x0200004A RID: 74
	[DefaultView]
	public abstract class MissionCheatView : MissionView
	{
		// Token: 0x06000341 RID: 833
		public abstract bool GetIsCheatsAvailable();

		// Token: 0x06000342 RID: 834
		public abstract void InitializeScreen();

		// Token: 0x06000343 RID: 835
		public abstract void FinalizeScreen();
	}
}
