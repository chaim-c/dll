using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.MountAndBlade.View.MissionViews
{
	// Token: 0x02000052 RID: 82
	public class MissionPlayerMovementFlagsChangeEvent : EventBase
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600039C RID: 924 RVA: 0x0002004D File Offset: 0x0001E24D
		// (set) Token: 0x0600039D RID: 925 RVA: 0x00020055 File Offset: 0x0001E255
		public Agent.MovementControlFlag MovementFlag { get; private set; }

		// Token: 0x0600039E RID: 926 RVA: 0x0002005E File Offset: 0x0001E25E
		public MissionPlayerMovementFlagsChangeEvent(Agent.MovementControlFlag movementFlag)
		{
			this.MovementFlag = movementFlag;
		}
	}
}
