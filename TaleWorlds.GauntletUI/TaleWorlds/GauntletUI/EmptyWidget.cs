using System;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.GauntletUI
{
	// Token: 0x02000038 RID: 56
	internal class EmptyWidget : Widget
	{
		// Token: 0x060003B7 RID: 951 RVA: 0x0000F4FF File Offset: 0x0000D6FF
		public EmptyWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000F508 File Offset: 0x0000D708
		protected override void OnUpdate(float dt)
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000F50A File Offset: 0x0000D70A
		protected override void OnParallelUpdate(float dt)
		{
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000F50C File Offset: 0x0000D70C
		protected override void OnLateUpdate(float dt)
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000F50E File Offset: 0x0000D70E
		public override void UpdateBrushes(float dt)
		{
		}
	}
}
