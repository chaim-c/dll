using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.Radial
{
	// Token: 0x020000DC RID: 220
	public class MissionRadialButtonWidget : ButtonWidget
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0001FEFB File Offset: 0x0001E0FB
		public MissionRadialButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0001FF04 File Offset: 0x0001E104
		public void ExecuteFocused()
		{
			if (base.IsDisabled)
			{
				this.SetState("DisabledSelected");
			}
			base.EventFired("OnFocused", Array.Empty<object>());
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0001FF29 File Offset: 0x0001E129
		public void ExecuteUnfocused()
		{
			if (base.IsDisabled)
			{
				this.SetState("Disabled");
				return;
			}
			this.SetState("Default");
		}
	}
}
