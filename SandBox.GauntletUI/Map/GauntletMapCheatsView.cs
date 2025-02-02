using System;
using System.Collections.Generic;
using SandBox.View.Map;
using SandBox.ViewModelCollection.Map.Cheat;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000027 RID: 39
	[OverrideView(typeof(MapCheatsView))]
	public class GauntletMapCheatsView : MapCheatsView
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0000B1B0 File Offset: 0x000093B0
		protected override void CreateLayout()
		{
			base.CreateLayout();
			IEnumerable<GameplayCheatBase> mapCheatList = GameplayCheatsManager.GetMapCheatList();
			this._dataSource = new GameplayCheatsVM(new Action(this.HandleClose), mapCheatList);
			this.InitializeKeyVisuals();
			base.Layer = new GauntletLayer(4500, "GauntletLayer", false);
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			this._layerAsGauntletLayer.LoadMovie("MapCheats", this._dataSource);
			this._layerAsGauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._layerAsGauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._layerAsGauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._layerAsGauntletLayer);
			base.MapScreen.AddLayer(this._layerAsGauntletLayer);
			base.MapScreen.SetIsMapCheatsActive(true);
			Campaign.Current.TimeControlMode = CampaignTimeControlMode.Stop;
			Campaign.Current.SetTimeControlModeLock(true);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000B29C File Offset: 0x0000949C
		protected override void OnFinalize()
		{
			base.OnFinalize();
			base.MapScreen.RemoveLayer(base.Layer);
			GameplayCheatsVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._layerAsGauntletLayer = null;
			base.Layer = null;
			this._dataSource = null;
			base.MapScreen.SetIsMapCheatsActive(false);
			Campaign.Current.SetTimeControlModeLock(false);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B2FD File Offset: 0x000094FD
		private void HandleClose()
		{
			base.MapScreen.CloseGameplayCheats();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000B30A File Offset: 0x0000950A
		protected override bool IsEscaped()
		{
			return true;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000B30D File Offset: 0x0000950D
		protected override void OnIdleTick(float dt)
		{
			base.OnIdleTick(dt);
			this.HandleInput();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B31C File Offset: 0x0000951C
		protected override void OnMenuModeTick(float dt)
		{
			base.OnMenuModeTick(dt);
			this.HandleInput();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B32B File Offset: 0x0000952B
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this.HandleInput();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B33A File Offset: 0x0000953A
		private void HandleInput()
		{
			if (base.Layer.Input.IsHotKeyReleased("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				GameplayCheatsVM dataSource = this._dataSource;
				if (dataSource == null)
				{
					return;
				}
				dataSource.ExecuteClose();
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B36D File Offset: 0x0000956D
		private void InitializeKeyVisuals()
		{
			this._dataSource.SetCloseInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
		}

		// Token: 0x040000AD RID: 173
		protected GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000AE RID: 174
		protected GameplayCheatsVM _dataSource;
	}
}
