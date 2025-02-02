using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Source.Missions;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.MountAndBlade.ViewModelCollection.EscapeMenu;

namespace TaleWorlds.MountAndBlade.GauntletUI.Mission.Singleplayer
{
	// Token: 0x02000033 RID: 51
	[OverrideView(typeof(MissionSingleplayerEscapeMenu))]
	public class MissionGauntletSingleplayerEscapeMenu : MissionGauntletEscapeMenuBase
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0000DB64 File Offset: 0x0000BD64
		public MissionGauntletSingleplayerEscapeMenu(bool isIronmanMode) : base("EscapeMenu")
		{
			this._isIronmanMode = isIronmanMode;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000DB78 File Offset: 0x0000BD78
		public override void OnMissionScreenInitialize()
		{
			base.OnMissionScreenInitialize();
			this._missionOptionsComponent = base.Mission.GetMissionBehavior<MissionOptionsComponent>();
			this.DataSource = new EscapeMenuVM(null, null);
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000DBC9 File Offset: 0x0000BDC9
		public override void OnMissionScreenFinalize()
		{
			base.OnMissionScreenFinalize();
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Remove(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionChanged));
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
		private void OnManagedOptionChanged(ManagedOptions.ManagedOptionsType changedManagedOptionsType)
		{
			if (changedManagedOptionsType == ManagedOptions.ManagedOptionsType.HideBattleUI)
			{
				EscapeMenuVM dataSource = this.DataSource;
				if (dataSource == null)
				{
					return;
				}
				dataSource.RefreshItems(this.GetEscapeMenuItems());
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000DC10 File Offset: 0x0000BE10
		public override void OnFocusChangeOnGameWindow(bool focusGained)
		{
			base.OnFocusChangeOnGameWindow(focusGained);
			if (!focusGained && BannerlordConfig.StopGameOnFocusLost && base.MissionScreen.IsOpeningEscapeMenuOnFocusChangeAllowed() && !GameStateManager.Current.ActiveStateDisabledByUser && !LoadingWindow.IsLoadingWindowActive && !base.IsActive)
			{
				this.OnEscape();
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000DC5D File Offset: 0x0000BE5D
		public override void OnSceneRenderingStarted()
		{
			base.OnSceneRenderingStarted();
			if (base.MissionScreen.IsFocusLost)
			{
				this.OnFocusChangeOnGameWindow(false);
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000DC7C File Offset: 0x0000BE7C
		protected override List<EscapeMenuItemVM> GetEscapeMenuItems()
		{
			TextObject ironmanDisabledReason = GameTexts.FindText("str_pause_menu_disabled_hint", "IronmanMode");
			List<EscapeMenuItemVM> list = new List<EscapeMenuItemVM>();
			list.Add(new EscapeMenuItemVM(new TextObject("{=e139gKZc}Return to the Game", null), delegate(object o)
			{
				this.OnEscapeMenuToggled(false);
			}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), true));
			list.Add(new EscapeMenuItemVM(new TextObject("{=NqarFr4P}Options", null), delegate(object o)
			{
				this.OnEscapeMenuToggled(false);
				MissionOptionsComponent missionOptionsComponent = this._missionOptionsComponent;
				if (missionOptionsComponent == null)
				{
					return;
				}
				missionOptionsComponent.OnAddOptionsUIHandler();
			}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), false));
			if (BannerlordConfig.HideBattleUI)
			{
				list.Add(new EscapeMenuItemVM(new TextObject("{=asCeKZXx}Re-enable Battle UI", null), delegate(object o)
				{
					ManagedOptions.SetConfig(ManagedOptions.ManagedOptionsType.HideBattleUI, 0f);
					ManagedOptions.SaveConfig();
					this.DataSource.RefreshItems(this.GetEscapeMenuItems());
				}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), false));
			}
			if (TaleWorlds.InputSystem.Input.IsGamepadActive)
			{
				MissionCheatView missionBehavior = base.Mission.GetMissionBehavior<MissionCheatView>();
				if (missionBehavior != null && missionBehavior.GetIsCheatsAvailable())
				{
					list.Add(new EscapeMenuItemVM(new TextObject("{=WA6Sk6cH}Cheat Menu", null), delegate(object o)
					{
						this.MissionScreen.Mission.GetMissionBehavior<MissionCheatView>().InitializeScreen();
					}, null, () => new Tuple<bool, TextObject>(false, TextObject.Empty), false));
				}
			}
			list.Add(new EscapeMenuItemVM(new TextObject("{=VklN5Wm6}Photo Mode", null), delegate(object o)
			{
				this.OnEscapeMenuToggled(false);
				this.MissionScreen.SetPhotoModeEnabled(true);
				this.Mission.IsInPhotoMode = true;
				InformationManager.ClearAllMessages();
			}, null, () => this.GetIsPhotoModeDisabled(), false));
			Action <>9__12;
			list.Add(new EscapeMenuItemVM(new TextObject("{=RamV6yLM}Exit to Main Menu", null), delegate(object o)
			{
				Game game = Game.Current;
				if (!(((game != null) ? game.GameType : null) is EditorGame))
				{
					Game game2 = Game.Current;
					if (!(((game2 != null) ? game2.GameType.GetType().Name : null) == "CustomGame"))
					{
						string titleText = GameTexts.FindText("str_exit", null).ToString();
						string text = GameTexts.FindText("str_mission_exit_query", null).ToString();
						bool isAffirmativeOptionShown = true;
						bool isNegativeOptionShown = true;
						string affirmativeText = GameTexts.FindText("str_yes", null).ToString();
						string negativeText = GameTexts.FindText("str_no", null).ToString();
						Action affirmativeAction = new Action(this.OnExitToMainMenu);
						Action negativeAction;
						if ((negativeAction = <>9__12) == null)
						{
							negativeAction = (<>9__12 = delegate()
							{
								this.OnEscapeMenuToggled(false);
							});
						}
						InformationManager.ShowInquiry(new InquiryData(titleText, text, isAffirmativeOptionShown, isNegativeOptionShown, affirmativeText, negativeText, affirmativeAction, negativeAction, "", 0f, null, null, null), false, false);
						return;
					}
				}
				this.OnExitToMainMenu();
			}, null, () => new Tuple<bool, TextObject>(this._isIronmanMode, ironmanDisabledReason), false));
			return list;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000DE48 File Offset: 0x0000C048
		private Tuple<bool, TextObject> GetIsPhotoModeDisabled()
		{
			if (base.MissionScreen.IsDeploymentActive)
			{
				return new Tuple<bool, TextObject>(true, new TextObject("{=rZSjkCpw}Cannot use photo mode during deployment.", null));
			}
			if (base.MissionScreen.IsConversationActive)
			{
				return new Tuple<bool, TextObject>(true, new TextObject("{=ImQnhIQ5}Cannot use photo mode during conversation.", null));
			}
			if (base.MissionScreen.IsPhotoModeEnabled)
			{
				return new Tuple<bool, TextObject>(true, new TextObject("{=79bODbwZ}Photo mode is already active.", null));
			}
			if (Module.CurrentModule.IsOnlyCoreContentEnabled)
			{
				return new Tuple<bool, TextObject>(true, new TextObject("{=V8BXjyYq}Disabled during installation.", null));
			}
			return new Tuple<bool, TextObject>(false, TextObject.Empty);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000DEDB File Offset: 0x0000C0DB
		private void OnExitToMainMenu()
		{
			base.OnEscapeMenuToggled(false);
			InformationManager.HideInquiry();
			MBGameManager.EndGame();
		}

		// Token: 0x0400014C RID: 332
		private MissionOptionsComponent _missionOptionsComponent;

		// Token: 0x0400014D RID: 333
		private bool _isIronmanMode;
	}
}
