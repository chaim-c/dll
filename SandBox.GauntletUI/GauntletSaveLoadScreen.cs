using System;
using SandBox.View;
using SandBox.ViewModelCollection.SaveLoad;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.LinQuick;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI
{
	// Token: 0x0200000E RID: 14
	[OverrideView(typeof(SaveLoadScreen))]
	public class GauntletSaveLoadScreen : ScreenBase
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00006FA3 File Offset: 0x000051A3
		public GauntletSaveLoadScreen(bool isSaving)
		{
			this._isSaving = isSaving;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006FB4 File Offset: 0x000051B4
		protected override void OnInitialize()
		{
			base.OnInitialize();
			bool isCampaignMapOnStack = GameStateManager.Current.GameStates.FirstOrDefaultQ((GameState s) => s is MapState) != null;
			this._dataSource = new SaveLoadVM(this._isSaving, isCampaignMapOnStack);
			this._dataSource.SetDeleteInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Delete"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			if (Game.Current != null)
			{
				Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
			}
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._spriteCategory = spriteData.SpriteCategories["ui_saveload"];
			this._spriteCategory.Load(resourceContext, uiresourceDepot);
			this._gauntletLayer = new GauntletLayer(1, "GauntletLayer", true);
			this._gauntletLayer.LoadMovie("SaveLoadScreen", this._dataSource);
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.IsFocusLayer = true;
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			ScreenManager.TrySetFocus(this._gauntletLayer);
			base.AddLayer(this._gauntletLayer);
			if (BannerlordConfig.ForceVSyncInMenus)
			{
				Utilities.SetForceVsync(true);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000713C File Offset: 0x0000533C
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (!this._dataSource.IsBusyWithAnAction)
			{
				if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
				{
					this._dataSource.ExecuteDone();
					UISoundsHelper.PlayUISound("event:/ui/panels/next");
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyPressed("Confirm") && !this._gauntletLayer.IsFocusedOnInput())
				{
					this._dataSource.ExecuteLoadSave();
					UISoundsHelper.PlayUISound("event:/ui/panels/next");
					return;
				}
				if (this._gauntletLayer.Input.IsHotKeyPressed("Delete") && !this._gauntletLayer.IsFocusedOnInput())
				{
					this._dataSource.DeleteSelectedSave();
					UISoundsHelper.PlayUISound("event:/ui/panels/next");
				}
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00007200 File Offset: 0x00005400
		protected override void OnFinalize()
		{
			base.OnFinalize();
			if (Game.Current != null)
			{
				Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
			}
			base.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._spriteCategory.Unload();
			Utilities.SetForceVsync(false);
		}

		// Token: 0x04000048 RID: 72
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000049 RID: 73
		private SaveLoadVM _dataSource;

		// Token: 0x0400004A RID: 74
		private SpriteCategory _spriteCategory;

		// Token: 0x0400004B RID: 75
		private readonly bool _isSaving;
	}
}
