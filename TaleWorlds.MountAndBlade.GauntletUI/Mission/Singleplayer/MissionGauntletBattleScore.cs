using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection.Scoreboard;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x0200002C RID: 44
	[OverrideView(typeof(MissionBattleScoreUIHandler))]
	public class MissionGauntletBattleScore : MissionView
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000BC45 File Offset: 0x00009E45
		public ScoreboardBaseVM DataSource
		{
			get
			{
				return this._dataSource;
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000BC4D File Offset: 0x00009E4D
		public MissionGauntletBattleScore(ScoreboardBaseVM scoreboardVM)
		{
			this._dataSource = scoreboardVM;
			this.ViewOrderPriority = 15;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000BC64 File Offset: 0x00009E64
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			base.Mission.IsFriendlyMission = false;
			this._dataSource.Initialize(base.MissionScreen, base.Mission, null, new Action<bool>(this.ToggleScoreboard));
			this._isSiegeScoreboard = base.Mission.HasMissionBehavior<SiegeDeploymentMissionController>();
			this.CreateView();
			this._dataSource.SetShortcuts(new ScoreboardHotkeys
			{
				ShowMouseHotkey = HotKeyManager.GetCategory("ScoreboardHotKeyCategory").GetGameKey(35),
				ShowScoreboardHotkey = HotKeyManager.GetCategory("Generic").GetGameKey(4),
				DoneInputKey = HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"),
				FastForwardKey = HotKeyManager.GetCategory("ScoreboardHotKeyCategory").GetHotKey("ToggleFastForward")
			});
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000BD38 File Offset: 0x00009F38
		private void CreateView()
		{
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "GauntletLayer", false);
			this._gauntletLayer.LoadMovie("SPScoreboard", this._dataSource);
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			this._gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("ScoreboardHotKeyCategory"));
			GameKeyContext category = HotKeyManager.GetCategory("ScoreboardHotKeyCategory");
			if (!base.MissionScreen.SceneLayer.Input.IsCategoryRegistered(category))
			{
				base.MissionScreen.SceneLayer.Input.RegisterHotKeyCategory(category);
			}
			base.MissionScreen.AddLayer(this._gauntletLayer);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000BE0C File Offset: 0x0000A00C
		public override void OnMissionScreenFinalize()
		{
			base.Mission.OnMainAgentChanged -= this.Mission_OnMainAgentChanged;
			base.MissionScreen.GetSpectatedCharacter = null;
			base.OnMissionScreenFinalize();
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._dataSource.OnFinalize();
			this._dataSource = null;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000BE6C File Offset: 0x0000A06C
		public override bool OnEscape()
		{
			if (this._dataSource.ShowScoreboard)
			{
				this.OnClose();
				return true;
			}
			return base.OnEscape();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000BE89 File Offset: 0x0000A089
		public override void EarlyStart()
		{
			base.EarlyStart();
			base.Mission.OnMainAgentChanged += this.Mission_OnMainAgentChanged;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000BEA8 File Offset: 0x0000A0A8
		public override void OnDeploymentFinished()
		{
			this._isPreparationEnded = true;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000BEB1 File Offset: 0x0000A0B1
		private void Mission_OnMainAgentChanged(object sender, PropertyChangedEventArgs e)
		{
			if (base.Mission.MainAgent == null)
			{
				this._dataSource.OnMainHeroDeath();
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000BECC File Offset: 0x0000A0CC
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			this._dataSource.Tick(dt);
			if (MissionGauntletBattleScore._forceScoreboardToggle || this._dataSource.IsOver || this._dataSource.IsMainCharacterDead || TaleWorlds.InputSystem.Input.IsGamepadActive)
			{
				bool flag = this.CanOpenScoreboard() && (base.Mission.InputManager.IsGameKeyPressed(4) || this._gauntletLayer.Input.IsGameKeyPressed(4));
				if (flag && !this._dataSource.ShowScoreboard)
				{
					IBattleEndLogic battleEndLogic = base.Mission.MissionBehaviors.FirstOrDefault((MissionBehavior behavior) => behavior is IBattleEndLogic) as IBattleEndLogic;
					if (battleEndLogic != null)
					{
						battleEndLogic.SetNotificationDisabled(true);
					}
					this._toOpen = true;
				}
				if (flag && this._dataSource.ShowScoreboard)
				{
					IBattleEndLogic battleEndLogic2 = base.Mission.MissionBehaviors.FirstOrDefault((MissionBehavior behavior) => behavior is IBattleEndLogic) as IBattleEndLogic;
					if (battleEndLogic2 != null)
					{
						battleEndLogic2.SetNotificationDisabled(false);
					}
					this.OnClose();
				}
			}
			else
			{
				bool flag2 = this.CanOpenScoreboard() && (base.Mission.InputManager.IsHotKeyDown("HoldShow") || this._gauntletLayer.Input.IsHotKeyDown("HoldShow"));
				if (flag2 && !this._dataSource.ShowScoreboard)
				{
					IBattleEndLogic battleEndLogic3 = base.Mission.MissionBehaviors.FirstOrDefault((MissionBehavior behavior) => behavior is IBattleEndLogic) as IBattleEndLogic;
					if (battleEndLogic3 != null)
					{
						battleEndLogic3.SetNotificationDisabled(true);
					}
					this._toOpen = true;
				}
				if (!flag2 && this._dataSource.ShowScoreboard)
				{
					IBattleEndLogic battleEndLogic4 = base.Mission.MissionBehaviors.FirstOrDefault((MissionBehavior behavior) => behavior is IBattleEndLogic) as IBattleEndLogic;
					if (battleEndLogic4 != null)
					{
						battleEndLogic4.SetNotificationDisabled(false);
					}
					this.OnClose();
				}
			}
			if (this._toOpen && base.MissionScreen.SetDisplayDialog(true))
			{
				this.OnOpen();
			}
			if (this._dataSource.IsMainCharacterDead && !this._dataSource.IsOver && (base.Mission.InputManager.IsHotKeyReleased("ToggleFastForward") || this._gauntletLayer.Input.IsHotKeyReleased("ToggleFastForward")))
			{
				this._dataSource.IsFastForwarding = !this._dataSource.IsFastForwarding;
				this._dataSource.ExecuteFastForwardAction();
			}
			if (this._dataSource.IsOver && this._dataSource.ShowScoreboard && (base.Mission.InputManager.IsHotKeyPressed("Confirm") || this._gauntletLayer.Input.IsHotKeyPressed("Confirm")))
			{
				this._dataSource.ExecuteQuitAction();
			}
			if (this._dataSource.ShowScoreboard && !base.DebugInput.IsControlDown() && base.DebugInput.IsHotKeyPressed("ShowHighlightsSummary"))
			{
				HighlightsController missionBehavior = base.Mission.GetMissionBehavior<HighlightsController>();
				if (missionBehavior != null)
				{
					missionBehavior.ShowSummary();
				}
			}
			bool flag3 = base.Mission.InputManager.IsGameKeyPressed(35) || this._gauntletLayer.Input.IsGameKeyPressed(35);
			if (this._dataSource.ShowScoreboard && !this._isMouseEnabled && flag3)
			{
				this.SetMouseState(true);
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000C251 File Offset: 0x0000A451
		private bool CanOpenScoreboard()
		{
			return !base.MissionScreen.IsRadialMenuActive && !base.MissionScreen.IsPhotoModeEnabled && !base.Mission.IsOrderMenuOpen;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000C27D File Offset: 0x0000A47D
		private void ToggleScoreboard(bool value)
		{
			if (value)
			{
				this._toOpen = true;
				return;
			}
			this.OnClose();
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000C290 File Offset: 0x0000A490
		private void OnOpen()
		{
			this._toOpen = false;
			if (this._dataSource.ShowScoreboard || (this._isSiegeScoreboard && !this._isPreparationEnded))
			{
				base.MissionScreen.SetDisplayDialog(false);
				return;
			}
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(false, InputUsageMask.All);
			this._dataSource.ShowScoreboard = true;
			base.MissionScreen.SetCameraLockState(true);
			if (this._dataSource.IsOver || this._dataSource.IsMainCharacterDead)
			{
				this.SetMouseState(true);
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000C31C File Offset: 0x0000A51C
		private void OnClose()
		{
			if (!this._dataSource.ShowScoreboard)
			{
				return;
			}
			base.MissionScreen.SetDisplayDialog(false);
			this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
			this._dataSource.ShowScoreboard = false;
			base.MissionScreen.SetCameraLockState(false);
			this.SetMouseState(false);
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x0000C374 File Offset: 0x0000A574
		private void SetMouseState(bool isEnabled)
		{
			this._gauntletLayer.IsFocusLayer = isEnabled;
			if (isEnabled)
			{
				this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
				ScreenManager.TrySetFocus(this._gauntletLayer);
			}
			else
			{
				ScreenManager.TryLoseFocus(this._gauntletLayer);
			}
			ScoreboardBaseVM dataSource = this._dataSource;
			if (dataSource != null)
			{
				dataSource.SetMouseState(isEnabled);
			}
			this._isMouseEnabled = isEnabled;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000C3D3 File Offset: 0x0000A5D3
		public override void OnPhotoModeActivated()
		{
			base.OnPhotoModeActivated();
			this._gauntletLayer.UIContext.ContextAlpha = 0f;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public override void OnPhotoModeDeactivated()
		{
			base.OnPhotoModeDeactivated();
			this._gauntletLayer.UIContext.ContextAlpha = 1f;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000C410 File Offset: 0x0000A610
		[CommandLineFunctionality.CommandLineArgumentFunction("force_toggle", "scoreboard")]
		public static string ForceScoreboardToggle(List<string> args)
		{
			int num;
			if (args.Count == 1 && int.TryParse(args[0], out num) && (num == 0 || num == 1))
			{
				MissionGauntletBattleScore._forceScoreboardToggle = (num == 1);
				return "Force Scoreboard Toggle is: " + (MissionGauntletBattleScore._forceScoreboardToggle ? "ON" : "OFF");
			}
			return "Format is: scoreboard.force_toggle 0-1";
		}

		// Token: 0x04000118 RID: 280
		private ScoreboardBaseVM _dataSource;

		// Token: 0x04000119 RID: 281
		private GauntletLayer _gauntletLayer;

		// Token: 0x0400011A RID: 282
		private bool _isPreparationEnded;

		// Token: 0x0400011B RID: 283
		private bool _isSiegeScoreboard;

		// Token: 0x0400011C RID: 284
		private bool _toOpen;

		// Token: 0x0400011D RID: 285
		private bool _isMouseEnabled;

		// Token: 0x0400011E RID: 286
		private static bool _forceScoreboardToggle;
	}
}
