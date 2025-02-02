using System;

namespace TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer
{
	// Token: 0x02000061 RID: 97
	public class DeploymentView : MissionView
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x0002225D File Offset: 0x0002045D
		public override void AfterStart()
		{
			base.AfterStart();
			this._deploymentHandler = base.Mission.GetMissionBehavior<DeploymentHandler>();
			this.CreateWidgets();
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0002227C File Offset: 0x0002047C
		public override void OnRemoveBehavior()
		{
			this.RemoveWidgets();
			base.OnRemoveBehavior();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0002228A File Offset: 0x0002048A
		protected virtual void CreateWidgets()
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0002228C File Offset: 0x0002048C
		protected virtual void RemoveWidgets()
		{
		}

		// Token: 0x040002A6 RID: 678
		private DeploymentHandler _deploymentHandler;
	}
}
