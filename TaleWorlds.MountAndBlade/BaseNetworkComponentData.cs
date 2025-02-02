using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002E4 RID: 740
	public class BaseNetworkComponentData : UdpNetworkComponent
	{
		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002882 RID: 10370 RVA: 0x0009C3F1 File Offset: 0x0009A5F1
		// (set) Token: 0x06002883 RID: 10371 RVA: 0x0009C3F9 File Offset: 0x0009A5F9
		public int CurrentBattleIndex { get; private set; }

		// Token: 0x06002884 RID: 10372 RVA: 0x0009C402 File Offset: 0x0009A602
		public void UpdateCurrentBattleIndex(int currentBattleIndex)
		{
			this.CurrentBattleIndex = currentBattleIndex;
		}

		// Token: 0x04000F50 RID: 3920
		public const float MaxIntermissionStateTime = 240f;
	}
}
