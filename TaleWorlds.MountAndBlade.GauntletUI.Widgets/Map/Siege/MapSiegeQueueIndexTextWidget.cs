using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Map.Siege
{
	// Token: 0x0200010E RID: 270
	public class MapSiegeQueueIndexTextWidget : TextWidget
	{
		// Token: 0x06000E36 RID: 3638 RVA: 0x00027662 File Offset: 0x00025862
		public MapSiegeQueueIndexTextWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0002766B File Offset: 0x0002586B
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			base.IsVisible = (base.IntText > 0);
		}
	}
}
