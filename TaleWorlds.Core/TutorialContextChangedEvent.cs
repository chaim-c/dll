using System;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.Core
{
	// Token: 0x020000C3 RID: 195
	public class TutorialContextChangedEvent : EventBase
	{
		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00020036 File Offset: 0x0001E236
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x0002003E File Offset: 0x0001E23E
		public TutorialContexts NewContext { get; private set; }

		// Token: 0x060009AF RID: 2479 RVA: 0x00020047 File Offset: 0x0001E247
		public TutorialContextChangedEvent(TutorialContexts newContext)
		{
			this.NewContext = newContext;
		}
	}
}
