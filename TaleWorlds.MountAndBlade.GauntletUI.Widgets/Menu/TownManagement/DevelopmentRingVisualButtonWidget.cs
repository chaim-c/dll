using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement
{
	// Token: 0x020000FE RID: 254
	public class DevelopmentRingVisualButtonWidget : ButtonWidget
	{
		// Token: 0x06000D6C RID: 3436 RVA: 0x000255C3 File Offset: 0x000237C3
		public DevelopmentRingVisualButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x000255CC File Offset: 0x000237CC
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!base.IsSelected)
			{
				this.SetState(base.ParentWidget.CurrentState);
				return;
			}
			this.SetState("Selected");
		}
	}
}
