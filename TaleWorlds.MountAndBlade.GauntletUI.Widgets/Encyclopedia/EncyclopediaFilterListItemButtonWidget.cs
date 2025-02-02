using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia
{
	// Token: 0x0200014C RID: 332
	public class EncyclopediaFilterListItemButtonWidget : ButtonWidget
	{
		// Token: 0x060011A4 RID: 4516 RVA: 0x00030F6D File Offset: 0x0002F16D
		public EncyclopediaFilterListItemButtonWidget(UIContext context) : base(context)
		{
			base.OverrideDefaultStateSwitchingEnabled = true;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00030F80 File Offset: 0x0002F180
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			if (base.IsDisabled)
			{
				this.SetState("Disabled");
				return;
			}
			if (base.IsHovered)
			{
				this.SetState("Hovered");
				return;
			}
			if (base.IsSelected)
			{
				this.SetState("Selected");
				return;
			}
			if (base.IsPressed)
			{
				this.SetState("Pressed");
				return;
			}
			this.SetState("Default");
		}
	}
}
