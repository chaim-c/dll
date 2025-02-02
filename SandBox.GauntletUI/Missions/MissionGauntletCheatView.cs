using System;
using System.Collections.Generic;
using SandBox.ViewModelCollection.Map.Cheat;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.ScreenSystem;

namespace SandBox.GauntletUI.Missions
{
	// Token: 0x02000016 RID: 22
	[OverrideView(typeof(MissionCheatView))]
	public class MissionGauntletCheatView : MissionCheatView
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000080C2 File Offset: 0x000062C2
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			this.FinalizeScreen();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000080D0 File Offset: 0x000062D0
		public override bool GetIsCheatsAvailable()
		{
			return true;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000080D4 File Offset: 0x000062D4
		public override void InitializeScreen()
		{
			if (this._isActive)
			{
				return;
			}
			this._isActive = true;
			IEnumerable<GameplayCheatBase> missionCheatList = GameplayCheatsManager.GetMissionCheatList();
			this._dataSource = new GameplayCheatsVM(new Action(this.FinalizeScreen), missionCheatList);
			this.InitializeKeyVisuals();
			this._gauntletLayer = new GauntletLayer(4500, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("MapCheats", this._dataSource);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._gauntletLayer.IsFocusLayer = true;
			ScreenManager.TrySetFocus(this._gauntletLayer);
			base.MissionScreen.AddLayer(this._gauntletLayer);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008198 File Offset: 0x00006398
		public override void FinalizeScreen()
		{
			if (!this._isActive)
			{
				return;
			}
			this._isActive = false;
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			GameplayCheatsVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.OnFinalize();
			}
			this._gauntletLayer = null;
			this._dataSource = null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000081E5 File Offset: 0x000063E5
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			if (this._isActive)
			{
				this.HandleInput();
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000081FC File Offset: 0x000063FC
		private void HandleInput()
		{
			if (this._gauntletLayer.Input.IsHotKeyReleased("Exit"))
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

		// Token: 0x060000E7 RID: 231 RVA: 0x0000822F File Offset: 0x0000642F
		private void InitializeKeyVisuals()
		{
			this._dataSource.SetCloseInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
		}

		// Token: 0x04000061 RID: 97
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000062 RID: 98
		private GameplayCheatsVM _dataSource;

		// Token: 0x04000063 RID: 99
		private bool _isActive;
	}
}
