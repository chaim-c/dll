using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI.ExtraWidgets.Graph
{
	// Token: 0x02000015 RID: 21
	public class GraphLinePointWidget : BrushWidget
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006D12 File Offset: 0x00004F12
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00006D1A File Offset: 0x00004F1A
		public float HorizontalValue { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006D23 File Offset: 0x00004F23
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00006D2B File Offset: 0x00004F2B
		public float VerticalValue { get; set; }

		// Token: 0x06000121 RID: 289 RVA: 0x00006D34 File Offset: 0x00004F34
		public GraphLinePointWidget(UIContext context) : base(context)
		{
		}
	}
}
