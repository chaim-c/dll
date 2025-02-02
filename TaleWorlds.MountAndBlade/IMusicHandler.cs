using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001D0 RID: 464
	public interface IMusicHandler
	{
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001A66 RID: 6758
		bool IsPausable { get; }

		// Token: 0x06001A67 RID: 6759
		void OnUpdated(float dt);
	}
}
