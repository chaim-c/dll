using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.GameOver
{
	// Token: 0x02000144 RID: 324
	public class GameOverCategoryIconBrushWidget : BrushWidget
	{
		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00030048 File Offset: 0x0002E248
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x00030050 File Offset: 0x0002E250
		public string CategoryID { get; set; }

		// Token: 0x0600112B RID: 4395 RVA: 0x00030059 File Offset: 0x0002E259
		public GameOverCategoryIconBrushWidget(UIContext context) : base(context)
		{
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.OnManualLateUpdate), 4);
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0003007B File Offset: 0x0002E27B
		private void OnManualLateUpdate(float obj)
		{
			base.Brush = base.Context.GetBrush("GameOver.Category.Visual." + this.CategoryID);
		}
	}
}
