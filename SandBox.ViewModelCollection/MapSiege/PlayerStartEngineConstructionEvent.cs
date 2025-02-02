using System;
using TaleWorlds.Core;
using TaleWorlds.Library.EventSystem;

namespace SandBox.ViewModelCollection.MapSiege
{
	// Token: 0x02000035 RID: 53
	public class PlayerStartEngineConstructionEvent : EventBase
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001248D File Offset: 0x0001068D
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x00012495 File Offset: 0x00010695
		public SiegeEngineType Engine { get; private set; }

		// Token: 0x060003FE RID: 1022 RVA: 0x0001249E File Offset: 0x0001069E
		public PlayerStartEngineConstructionEvent(SiegeEngineType engine)
		{
			this.Engine = engine;
		}
	}
}
