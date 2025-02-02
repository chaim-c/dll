using System;
using SandBox.View.Map;
using SandBox.ViewModelCollection;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000025 RID: 37
	[OverrideView(typeof(BattleSimulationMapView))]
	public class GauntletMapBattleSimulationView : MapView
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000ADBB File Offset: 0x00008FBB
		public GauntletMapBattleSimulationView(BattleSimulation battleSimulation)
		{
			this._battleSimulation = battleSimulation;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000ADCC File Offset: 0x00008FCC
		protected override void CreateLayout()
		{
			base.CreateLayout();
			this._dataSource = new SPScoreboardVM(this._battleSimulation);
			this._dataSource.Initialize(null, null, new Action(base.MapState.EndBattleSimulation), null);
			this._dataSource.SetShortcuts(new ScoreboardHotkeys
			{
				ShowMouseHotkey = null,
				ShowScoreboardHotkey = null,
				DoneInputKey = HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"),
				FastForwardKey = HotKeyManager.GetCategory("ScoreboardHotKeyCategory").GetHotKey("ToggleFastForward")
			});
			base.Layer = new GauntletLayer(101, "GauntletLayer", false);
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("ScoreboardHotKeyCategory"));
			this._gauntletMovie = this._layerAsGauntletLayer.LoadMovie("SPScoreboard", this._dataSource);
			this._dataSource.ExecutePlayAction();
			base.Layer.IsFocusLayer = true;
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			base.MapScreen.AddLayer(base.Layer);
			ScreenManager.TrySetFocus(base.Layer);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000AF24 File Offset: 0x00009124
		protected override void OnFinalize()
		{
			this._dataSource.OnFinalize();
			base.MapScreen.RemoveLayer(base.Layer);
			base.Layer.IsFocusLayer = false;
			base.Layer.InputRestrictions.ResetInputRestrictions();
			ScreenManager.TryLoseFocus(base.Layer);
			this._layerAsGauntletLayer = null;
			base.Layer = null;
			this._gauntletMovie = null;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AF8C File Offset: 0x0000918C
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this._dataSource != null && base.Layer != null)
			{
				if (!this._dataSource.IsOver && base.Layer.Input.IsHotKeyReleased("ToggleFastForward"))
				{
					this._dataSource.IsFastForwarding = !this._dataSource.IsFastForwarding;
					this._dataSource.ExecuteFastForwardAction();
					return;
				}
				if (this._dataSource.IsOver && this._dataSource.ShowScoreboard && base.Layer.Input.IsHotKeyPressed("Confirm"))
				{
					this._dataSource.ExecuteQuitAction();
				}
			}
		}

		// Token: 0x040000A7 RID: 167
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000A8 RID: 168
		private IGauntletMovie _gauntletMovie;

		// Token: 0x040000A9 RID: 169
		private SPScoreboardVM _dataSource;

		// Token: 0x040000AA RID: 170
		private readonly BattleSimulation _battleSimulation;
	}
}
