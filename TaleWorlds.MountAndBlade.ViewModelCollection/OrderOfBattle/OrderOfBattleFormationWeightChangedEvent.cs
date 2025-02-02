using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x02000035 RID: 53
	public class OrderOfBattleFormationWeightChangedEvent : EventBase
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00014752 File Offset: 0x00012952
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0001475A File Offset: 0x0001295A
		public Formation Formation { get; private set; }

		// Token: 0x06000479 RID: 1145 RVA: 0x00014763 File Offset: 0x00012963
		public OrderOfBattleFormationWeightChangedEvent(Formation formation)
		{
			this.Formation = formation;
		}
	}
}
