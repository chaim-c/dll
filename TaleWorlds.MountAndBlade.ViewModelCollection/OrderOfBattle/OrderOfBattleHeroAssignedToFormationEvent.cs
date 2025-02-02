using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.MountAndBlade.ViewModelCollection.OrderOfBattle
{
	// Token: 0x02000033 RID: 51
	public class OrderOfBattleHeroAssignedToFormationEvent : EventBase
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000146FA File Offset: 0x000128FA
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00014702 File Offset: 0x00012902
		public Agent AssignedHero { get; private set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001470B File Offset: 0x0001290B
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00014713 File Offset: 0x00012913
		public Formation AssignedFormation { get; private set; }

		// Token: 0x06000473 RID: 1139 RVA: 0x0001471C File Offset: 0x0001291C
		public OrderOfBattleHeroAssignedToFormationEvent(Agent assignedHero, Formation assignedFormation)
		{
			this.AssignedHero = assignedHero;
			this.AssignedFormation = assignedFormation;
		}
	}
}
