using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.GameOver
{
	// Token: 0x02000145 RID: 325
	public class GameOverScreenWidget : Widget
	{
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x0600112D RID: 4397 RVA: 0x0003009E File Offset: 0x0002E29E
		// (set) Token: 0x0600112E RID: 4398 RVA: 0x000300A6 File Offset: 0x0002E2A6
		public BrushWidget ConceptVisualWidget { get; set; }

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x000300AF File Offset: 0x0002E2AF
		// (set) Token: 0x06001130 RID: 4400 RVA: 0x000300B7 File Offset: 0x0002E2B7
		public BrushWidget BannerBrushWidget { get; set; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001131 RID: 4401 RVA: 0x000300C0 File Offset: 0x0002E2C0
		// (set) Token: 0x06001132 RID: 4402 RVA: 0x000300C8 File Offset: 0x0002E2C8
		public BrushWidget BannerFrameBrushWidget1 { get; set; }

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x000300D1 File Offset: 0x0002E2D1
		// (set) Token: 0x06001134 RID: 4404 RVA: 0x000300D9 File Offset: 0x0002E2D9
		public BrushWidget BannerFrameBrushWidget2 { get; set; }

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001135 RID: 4405 RVA: 0x000300E2 File Offset: 0x0002E2E2
		// (set) Token: 0x06001136 RID: 4406 RVA: 0x000300EA File Offset: 0x0002E2EA
		public string GameOverReason { get; set; }

		// Token: 0x06001137 RID: 4407 RVA: 0x000300F3 File Offset: 0x0002E2F3
		public GameOverScreenWidget(UIContext context) : base(context)
		{
			base.EventManager.AddLateUpdateAction(this, new Action<float>(this.OnManualLateUpdate), 4);
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x00030118 File Offset: 0x0002E318
		private void OnManualLateUpdate(float obj)
		{
			if (this.ConceptVisualWidget != null)
			{
				this.ConceptVisualWidget.Brush = base.Context.GetBrush("GameOver.Mask." + this.GameOverReason);
			}
			if (this.BannerBrushWidget != null)
			{
				this.BannerBrushWidget.Brush = base.Context.GetBrush("GameOver.Banner." + this.GameOverReason);
			}
			if (this.BannerFrameBrushWidget1 != null)
			{
				this.BannerFrameBrushWidget1.Brush = base.Context.GetBrush("GameOver.Banner.Frame." + this.GameOverReason);
			}
			if (this.BannerFrameBrushWidget2 != null)
			{
				this.BannerFrameBrushWidget2.Brush = base.Context.GetBrush("GameOver.Banner.Frame." + this.GameOverReason);
			}
		}
	}
}
