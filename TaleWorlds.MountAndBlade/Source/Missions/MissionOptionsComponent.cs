using System;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003B5 RID: 949
	public class MissionOptionsComponent : MissionLogic
	{
		// Token: 0x1400009E RID: 158
		// (add) Token: 0x060032D3 RID: 13011 RVA: 0x000D338C File Offset: 0x000D158C
		// (remove) Token: 0x060032D4 RID: 13012 RVA: 0x000D33C4 File Offset: 0x000D15C4
		public event OnMissionAddOptionsDelegate OnOptionsAdded;

		// Token: 0x060032D5 RID: 13013 RVA: 0x000D33F9 File Offset: 0x000D15F9
		public void OnAddOptionsUIHandler()
		{
			if (this.OnOptionsAdded != null)
			{
				this.OnOptionsAdded();
			}
		}
	}
}
