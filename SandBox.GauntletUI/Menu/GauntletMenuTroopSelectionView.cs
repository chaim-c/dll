using System;
using SandBox.View.Map;
using SandBox.View.Menu;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Roster;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TroopSelection;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Menu
{
	// Token: 0x02000020 RID: 32
	[OverrideView(typeof(MenuTroopSelectionView))]
	public class GauntletMenuTroopSelectionView : MenuView
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00009DA0 File Offset: 0x00007FA0
		public GauntletMenuTroopSelectionView(TroopRoster fullRoster, TroopRoster initialSelections, Func<CharacterObject, bool> changeChangeStatusOfTroop, Action<TroopRoster> onDone, int maxSelectableTroopCount, int minSelectableTroopCount)
		{
			this._onDone = onDone;
			this._fullRoster = fullRoster;
			this._initialSelections = initialSelections;
			this._changeChangeStatusOfTroop = changeChangeStatusOfTroop;
			this._maxSelectableTroopCount = maxSelectableTroopCount;
			this._minSelectableTroopCount = minSelectableTroopCount;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00009DD8 File Offset: 0x00007FD8
		protected override void OnInitialize()
		{
			base.OnInitialize();
			this._dataSource = new GameMenuTroopSelectionVM(this._fullRoster, this._initialSelections, this._changeChangeStatusOfTroop, new Action<TroopRoster>(this.OnDone), this._maxSelectableTroopCount, this._minSelectableTroopCount)
			{
				IsEnabled = true
			};
			this._dataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._dataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._dataSource.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
			base.Layer = new GauntletLayer(206, "GauntletLayer", false)
			{
				Name = "MenuTroopSelection"
			};
			this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
			base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
			this._movie = this._layerAsGauntletLayer.LoadMovie("GameMenuTroopSelection", this._dataSource);
			base.Layer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._layerAsGauntletLayer);
			base.MenuViewContext.AddLayer(base.Layer);
			MapScreen mapScreen;
			if ((mapScreen = (ScreenManager.TopScreen as MapScreen)) != null)
			{
				mapScreen.SetIsInHideoutTroopManage(true);
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009F55 File Offset: 0x00008155
		private void OnDone(TroopRoster obj)
		{
			MapScreen.Instance.SetIsInHideoutTroopManage(false);
			base.MenuViewContext.CloseTroopSelection();
			Action<TroopRoster> onDone = this._onDone;
			if (onDone == null)
			{
				return;
			}
			onDone.DynamicInvokeWithLog(new object[]
			{
				obj
			});
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009F88 File Offset: 0x00008188
		protected override void OnFinalize()
		{
			base.Layer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(base.Layer);
			this._dataSource.OnFinalize();
			this._dataSource = null;
			this._layerAsGauntletLayer.ReleaseMovie(this._movie);
			base.MenuViewContext.RemoveLayer(base.Layer);
			this._movie = null;
			base.Layer = null;
			this._layerAsGauntletLayer = null;
			MapScreen.Instance.SetIsInHideoutTroopManage(false);
			base.OnFinalize();
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A008 File Offset: 0x00008208
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			if (this._dataSource != null)
			{
				this._dataSource.IsFiveStackModifierActive = base.Layer.Input.IsHotKeyDown("FiveStackModifier");
				this._dataSource.IsEntireStackModifierActive = base.Layer.Input.IsHotKeyDown("EntireStackModifier");
			}
			ScreenLayer layer = base.Layer;
			if (layer != null && layer.Input.IsHotKeyPressed("Exit"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				this._dataSource.ExecuteCancel();
			}
			else
			{
				ScreenLayer layer2 = base.Layer;
				if (layer2 != null && layer2.Input.IsHotKeyPressed("Confirm") && this._dataSource.IsDoneEnabled)
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._dataSource.ExecuteDone();
				}
				else
				{
					ScreenLayer layer3 = base.Layer;
					if (layer3 != null && layer3.Input.IsHotKeyPressed("Reset"))
					{
						UISoundsHelper.PlayUISound("event:/ui/default");
						this._dataSource.ExecuteReset();
					}
				}
			}
			GameMenuTroopSelectionVM dataSource = this._dataSource;
			if (dataSource != null && !dataSource.IsEnabled)
			{
				base.MenuViewContext.CloseTroopSelection();
			}
		}

		// Token: 0x04000084 RID: 132
		private readonly Action<TroopRoster> _onDone;

		// Token: 0x04000085 RID: 133
		private readonly TroopRoster _fullRoster;

		// Token: 0x04000086 RID: 134
		private readonly TroopRoster _initialSelections;

		// Token: 0x04000087 RID: 135
		private readonly Func<CharacterObject, bool> _changeChangeStatusOfTroop;

		// Token: 0x04000088 RID: 136
		private readonly int _maxSelectableTroopCount;

		// Token: 0x04000089 RID: 137
		private readonly int _minSelectableTroopCount;

		// Token: 0x0400008A RID: 138
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x0400008B RID: 139
		private GameMenuTroopSelectionVM _dataSource;

		// Token: 0x0400008C RID: 140
		private IGauntletMovie _movie;
	}
}
