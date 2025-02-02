using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000036 RID: 54
	public abstract class WidgetComponent
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x0000F1E5 File Offset: 0x0000D3E5
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x0000F1ED File Offset: 0x0000D3ED
		public Widget Target { get; private set; }

		// Token: 0x060003AA RID: 938 RVA: 0x0000F1F6 File Offset: 0x0000D3F6
		protected WidgetComponent(Widget target)
		{
			this.Target = target;
		}
	}
}
