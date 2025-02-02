using System;
using TaleWorlds.GauntletUI;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.Radial
{
	// Token: 0x020000DD RID: 221
	public class MissionRadialCircleActionSelectorWidget : CircleActionSelectorWidget
	{
		// Token: 0x06000B77 RID: 2935 RVA: 0x0001FF4A File Offset: 0x0001E14A
		public MissionRadialCircleActionSelectorWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0001FF54 File Offset: 0x0001E154
		protected override void OnSelectedIndexChanged(int selectedIndex)
		{
			base.OnSelectedIndexChanged(selectedIndex);
			for (int i = 0; i < base.Children.Count; i++)
			{
				MissionRadialButtonWidget missionRadialButtonWidget;
				if ((missionRadialButtonWidget = (base.Children[i] as MissionRadialButtonWidget)) != null)
				{
					if (i == selectedIndex)
					{
						missionRadialButtonWidget.ExecuteFocused();
					}
					else
					{
						missionRadialButtonWidget.ExecuteUnfocused();
					}
				}
			}
		}
	}
}
