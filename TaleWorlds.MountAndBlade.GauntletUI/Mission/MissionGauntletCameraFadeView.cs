using System;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission
{
	// Token: 0x02000023 RID: 35
	[DefaultView]
	public class MissionGauntletCameraFadeView : MissionView
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00008C40 File Offset: 0x00006E40
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._dataSource = new BindingListFloatItem(0f);
			this._layer = new GauntletLayer(100000, "GauntletLayer", false);
			this._layer.LoadMovie("CameraFade", this._dataSource);
			base.MissionScreen.AddLayer(this._layer);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008CA1 File Offset: 0x00006EA1
		public override void AfterStart()
		{
			base.AfterStart();
			this._controller = base.Mission.GetMissionBehavior<MissionCameraFadeView>();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008CBA File Offset: 0x00006EBA
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (this._dataSource != null && this._controller != null)
			{
				this._dataSource.Item = this._controller.FadeAlpha;
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00008CE9 File Offset: 0x00006EE9
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			base.MissionScreen.RemoveLayer(this._layer);
			this._controller = null;
			this._dataSource = null;
			this._layer = null;
		}

		// Token: 0x040000CB RID: 203
		private GauntletLayer _layer;

		// Token: 0x040000CC RID: 204
		private BindingListFloatItem _dataSource;

		// Token: 0x040000CD RID: 205
		private MissionCameraFadeView _controller;
	}
}
