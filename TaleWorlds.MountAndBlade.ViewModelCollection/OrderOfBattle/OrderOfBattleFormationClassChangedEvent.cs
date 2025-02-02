using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x02000034 RID: 52
	public class OrderOfBattleFormationClassChangedEvent : EventBase
	{
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00014732 File Offset: 0x00012932
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0001473A File Offset: 0x0001293A
		public Formation Formation { get; private set; }

		// Token: 0x06000476 RID: 1142 RVA: 0x00014743 File Offset: 0x00012943
		public OrderOfBattleFormationClassChangedEvent(Formation formation)
		{
			this.Formation = formation;
		}
	}
}
