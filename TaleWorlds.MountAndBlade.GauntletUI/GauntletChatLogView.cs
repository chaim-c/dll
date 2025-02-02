using System;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.Screens;
using TaleWorlds.MountAndBlade.ViewModelCollection.Multiplayer;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.GauntletUI
{
	// Token: 0x02000004 RID: 4
	public class GauntletChatLogView : GlobalLayer
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000319D File Offset: 0x0000139D
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000031A4 File Offset: 0x000013A4
		public static GauntletChatLogView Current { get; private set; }

		// Token: 0x0600001F RID: 31 RVA: 0x000031AC File Offset: 0x000013AC
		public GauntletChatLogView()
		{
			this._dataSource = new MPChatVM();
			this._dataSource.SetGetKeyTextFromKeyIDFunc(new Func<TextObject>(this.GetToggleChatKeyText));
			this._dataSource.SetGetCycleChannelKeyTextFunc(new Func<TextObject>(this.GetCycleChannelsKeyText));
			this._dataSource.SetGetSendMessageKeyTextFunc(new Func<TextObject>(this.GetSendMessageKeyText));
			this._dataSource.SetGetCancelSendingKeyTextFunc(new Func<TextObject>(this.GetCancelSendingKeyText));
			GauntletLayer gauntletLayer = new GauntletLayer(300, "GauntletLayer", false);
			this._movie = gauntletLayer.LoadMovie("SPChatLog", this._dataSource);
			gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
			gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
			gauntletLayer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("ChatLogHotKeyCategory"));
			base.Layer = gauntletLayer;
			this._chatLogMessageManager = new ChatLogMessageManager(this._dataSource);
			MessageManager.SetMessageManager(this._chatLogMessageManager);
			ManagedOptions.OnManagedOptionChanged = (ManagedOptions.OnManagedOptionChangedDelegate)Delegate.Combine(ManagedOptions.OnManagedOptionChanged, new ManagedOptions.OnManagedOptionChangedDelegate(this.OnManagedOptionsChanged));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000032DE File Offset: 0x000014DE
		public static void Initialize()
		{
			if (GauntletChatLogView.Current == null)
			{
				GauntletChatLogView.Current = new GauntletChatLogView();
				ScreenManager.AddGlobalLayer(GauntletChatLogView.Current, false);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000032FC File Offset: 0x000014FC
		private void OnManagedOptionsChanged(ManagedOptions.ManagedOptionsType changedManagedOptionsType)
		{
			bool flag = changedManagedOptionsType == ManagedOptions.ManagedOptionsType.HideBattleUI && Mission.Current != null && BannerlordConfig.HideBattleUI;
			bool flag2 = changedManagedOptionsType == ManagedOptions.ManagedOptionsType.EnableSingleplayerChatBox && !GameNetwork.IsMultiplayer && !BannerlordConfig.EnableSingleplayerChatBox;
			bool flag3 = changedManagedOptionsType == ManagedOptions.ManagedOptionsType.EnableMultiplayerChatBox && GameNetwork.IsMultiplayer && !BannerlordConfig.EnableMultiplayerChatBox;
			if (flag || flag2 || flag3)
			{
				this._dataSource.Clear();
				this.CloseChat();
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003364 File Offset: 0x00001564
		private void CloseChat()
		{
			if (this._dataSource.IsInspectingMessages)
			{
				this._dataSource.StopInspectingMessages();
				ScreenManager.TryLoseFocus(base.Layer);
				return;
			}
			if (this._dataSource.IsTypingText)
			{
				this._dataSource.StopTyping(true);
				ScreenManager.TryLoseFocus(base.Layer);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000033BC File Offset: 0x000015BC
		protected override void OnTick(float dt)
		{
			if (!this._isEnabled)
			{
				this.CloseChat();
				return;
			}
			base.OnTick(dt);
			if (this._dataSource.IsChatAllowedByOptions())
			{
				this._chatLogMessageManager.Update();
			}
			this._dataSource.UpdateObjects(Game.Current, Mission.Current);
			this._dataSource.Tick(dt);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003418 File Offset: 0x00001618
		protected override void OnLateTick(float dt)
		{
			base.OnLateTick(dt);
			MPChatVM dataSource = this._dataSource;
			if (dataSource != null && dataSource.IsChatAllowedByOptions())
			{
				this.HandleInput();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000343C File Offset: 0x0000163C
		private void HandleInput()
		{
			bool flag = false;
			bool flag2 = true;
			this._isTeamChatAvailable = true;
			InputContext inputContext = null;
			if (ScreenManager.TopScreen is MissionScreen)
			{
				MissionScreen missionScreen = (MissionScreen)ScreenManager.TopScreen;
				if (missionScreen.SceneLayer != null)
				{
					flag = true;
					inputContext = missionScreen.SceneLayer.Input;
				}
			}
			else if (ScreenManager.TopScreen is IGauntletChatLogHandlerScreen)
			{
				((IGauntletChatLogHandlerScreen)ScreenManager.TopScreen).TryUpdateChatLogLayerParameters(ref this._isTeamChatAvailable, ref flag, ref inputContext);
			}
			else if (ScreenManager.TopScreen is GauntletInitialScreen)
			{
				flag = false;
			}
			else
			{
				ScreenLayer screenLayer = null;
				ScreenBase topScreen = ScreenManager.TopScreen;
				if (((topScreen != null) ? topScreen.Layers : null) != null)
				{
					for (int i = 0; i < ScreenManager.TopScreen.Layers.Count; i++)
					{
						if (ScreenManager.TopScreen.Layers[i]._categoryId == "SceneLayer")
						{
							screenLayer = ScreenManager.TopScreen.Layers[i];
							break;
						}
					}
				}
				if (screenLayer != null)
				{
					flag = true;
					flag2 = true;
					inputContext = screenLayer.Input;
				}
				this._dataSource.ShowHideShowHint = (screenLayer != null);
			}
			GauntletLayer gauntletLayer;
			if ((gauntletLayer = (ScreenManager.FocusedLayer as GauntletLayer)) != null && gauntletLayer != base.Layer && gauntletLayer.UIContext.EventManager.FocusedWidget is EditableTextWidget)
			{
				flag = false;
			}
			bool flag3 = false;
			bool flag4 = false;
			if (flag)
			{
				if (inputContext != null && !inputContext.IsCategoryRegistered(HotKeyManager.GetCategory("ChatLogHotKeyCategory")))
				{
					inputContext.RegisterHotKeyCategory(HotKeyManager.GetCategory("ChatLogHotKeyCategory"));
				}
				if (flag2)
				{
					if (inputContext != null && inputContext.IsGameKeyReleased(6) && this._canFocusWhileInMission)
					{
						this._dataSource.TypeToChannelAll(true);
						flag3 = true;
					}
					else if (inputContext != null && inputContext.IsGameKeyReleased(7) && this._canFocusWhileInMission && this._isTeamChatAvailable)
					{
						this._dataSource.TypeToChannelTeam(true);
						flag3 = true;
					}
					if (base.Layer.Input.IsHotKeyReleased("ToggleEscapeMenu") || base.Layer.Input.IsHotKeyReleased("Exit"))
					{
						bool isGamepadActive = Input.IsGamepadActive;
						this._dataSource.StopTyping(isGamepadActive);
						flag4 = true;
					}
					else if (base.Layer.Input.IsGameKeyReleased(8) || base.Layer.Input.IsHotKeyReleased("FinalizeChatAlternative") || base.Layer.Input.IsHotKeyReleased("SendMessage"))
					{
						if ((Input.IsGamepadActive && base.Layer.Input.IsHotKeyReleased("SendMessage")) || !Input.IsGamepadActive)
						{
							this._dataSource.SendCurrentlyTypedMessage();
						}
						this._dataSource.StopTyping(false);
						flag4 = true;
					}
					if (inputContext != null && (inputContext.IsGameKeyDownAndReleased(8) || inputContext.IsHotKeyDownAndReleased("FinalizeChatAlternative")) && this._canFocusWhileInMission)
					{
						if (this._dataSource.ActiveChannelType == ChatChannelType.NaN)
						{
							this._dataSource.TypeToChannelAll(true);
						}
						else
						{
							this._dataSource.StartTyping();
						}
						flag3 = true;
					}
					if (base.Layer.Input.IsHotKeyReleased("CycleChatTypes"))
					{
						if (this._dataSource.ActiveChannelType == ChatChannelType.Team)
						{
							this._dataSource.TypeToChannelAll(false);
						}
						else if (this._dataSource.ActiveChannelType == ChatChannelType.All && this._isTeamChatAvailable)
						{
							this._dataSource.TypeToChannelTeam(false);
						}
					}
				}
				else if (inputContext != null && (inputContext.IsGameKeyReleased(8) || inputContext.IsHotKeyReleased("FinalizeChatAlternative")) && this._canFocusWhileInMission)
				{
					if (!this._dataSource.IsInspectingMessages)
					{
						this._dataSource.StartInspectingMessages();
						flag3 = true;
					}
					else
					{
						this._dataSource.StopInspectingMessages();
						flag4 = true;
					}
				}
			}
			else
			{
				bool flag5 = this._dataSource.IsTypingText || this._dataSource.IsInspectingMessages;
				if (this._dataSource.IsTypingText)
				{
					this._dataSource.StopTyping(false);
				}
				else if (this._dataSource.IsInspectingMessages)
				{
					this._dataSource.StopInspectingMessages();
				}
				if (flag5)
				{
					base.Layer.InputRestrictions.ResetInputRestrictions();
					flag4 = true;
				}
			}
			if (flag3)
			{
				this.UpdateFocusLayer();
				ScreenManager.TrySetFocus(base.Layer);
			}
			else if (flag4)
			{
				this.UpdateFocusLayer();
				ScreenManager.TryLoseFocus(base.Layer);
			}
			if (flag3 || flag4)
			{
				MissionScreen missionScreen2 = ScreenManager.TopScreen as MissionScreen;
				if (missionScreen2 != null && missionScreen2.SceneLayer != null)
				{
					missionScreen2.Mission.GetMissionBehavior<MissionMainAgentController>().IsChatOpen = (flag3 && !flag4);
				}
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000038AC File Offset: 0x00001AAC
		private void UpdateFocusLayer()
		{
			if (this._dataSource.IsTypingText || this._dataSource.IsInspectingMessages)
			{
				if (this._dataSource.IsTypingText && !base.Layer.IsFocusLayer)
				{
					base.Layer.IsFocusLayer = true;
				}
				base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
				return;
			}
			base.Layer.IsFocusLayer = false;
			base.Layer.InputRestrictions.ResetInputRestrictions();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003928 File Offset: 0x00001B28
		public void SetCanFocusWhileInMission(bool canFocusInMission)
		{
			this._canFocusWhileInMission = canFocusInMission;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003931 File Offset: 0x00001B31
		public void OnSupportedFeaturesReceived(SupportedFeatures supportedFeatures)
		{
			this.SetEnabled(supportedFeatures.SupportsFeatures(Features.TextChat));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003941 File Offset: 0x00001B41
		public void SetEnabled(bool isEnabled)
		{
			if (this._isEnabled != isEnabled)
			{
				this._isEnabled = isEnabled;
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00003954 File Offset: 0x00001B54
		public void LoadMovie(bool forMultiplayer)
		{
			if (this._movie != null)
			{
				GauntletLayer gauntletLayer = base.Layer as GauntletLayer;
				if (gauntletLayer != null)
				{
					gauntletLayer.ReleaseMovie(this._movie);
				}
			}
			if (forMultiplayer)
			{
				Game game = Game.Current;
				if (game != null)
				{
					game.GetGameHandler<ChatBox>().InitializeForMultiplayer();
				}
				GauntletLayer gauntletLayer2 = base.Layer as GauntletLayer;
				this._movie = ((gauntletLayer2 != null) ? gauntletLayer2.LoadMovie("MPChatLog", this._dataSource) : null);
				this._dataSource.SetMessageHistoryCapacity(100);
				return;
			}
			this.SetEnabled(true);
			Game game2 = Game.Current;
			if (game2 != null)
			{
				game2.GetGameHandler<ChatBox>().InitializeForSinglePlayer();
			}
			GauntletLayer gauntletLayer3 = base.Layer as GauntletLayer;
			this._movie = ((gauntletLayer3 != null) ? gauntletLayer3.LoadMovie("SPChatLog", this._dataSource) : null);
			this._dataSource.ChatBoxSizeX = BannerlordConfig.ChatBoxSizeX;
			this._dataSource.ChatBoxSizeY = BannerlordConfig.ChatBoxSizeY;
			this._dataSource.SetMessageHistoryCapacity(250);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003A48 File Offset: 0x00001C48
		private TextObject GetToggleChatKeyText()
		{
			if (Input.IsGamepadActive)
			{
				Game game = Game.Current;
				if (game == null)
				{
					return null;
				}
				GameTextManager gameTextManager = game.GameTextManager;
				if (gameTextManager == null)
				{
					return null;
				}
				return gameTextManager.GetHotKeyGameTextFromKeyID("controllerloption");
			}
			else
			{
				Game game2 = Game.Current;
				if (game2 == null)
				{
					return null;
				}
				GameTextManager gameTextManager2 = game2.GameTextManager;
				if (gameTextManager2 == null)
				{
					return null;
				}
				return gameTextManager2.GetHotKeyGameTextFromKeyID("enter");
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00003A9D File Offset: 0x00001C9D
		private TextObject GetCycleChannelsKeyText()
		{
			Game game = Game.Current;
			TextObject textObject;
			if (game == null)
			{
				textObject = null;
			}
			else
			{
				GameTextManager gameTextManager = game.GameTextManager;
				textObject = ((gameTextManager != null) ? gameTextManager.GetHotKeyGameText("ChatLogHotKeyCategory", "CycleChatTypes") : null);
			}
			return textObject ?? TextObject.Empty;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003ACF File Offset: 0x00001CCF
		private TextObject GetSendMessageKeyText()
		{
			Game game = Game.Current;
			TextObject textObject;
			if (game == null)
			{
				textObject = null;
			}
			else
			{
				GameTextManager gameTextManager = game.GameTextManager;
				textObject = ((gameTextManager != null) ? gameTextManager.GetHotKeyGameText("ChatLogHotKeyCategory", "SendMessage") : null);
			}
			return textObject ?? TextObject.Empty;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003B01 File Offset: 0x00001D01
		private TextObject GetCancelSendingKeyText()
		{
			Game game = Game.Current;
			TextObject textObject;
			if (game == null)
			{
				textObject = null;
			}
			else
			{
				GameTextManager gameTextManager = game.GameTextManager;
				textObject = ((gameTextManager != null) ? gameTextManager.GetHotKeyGameText("GenericPanelGameKeyCategory", "Exit") : null);
			}
			return textObject ?? TextObject.Empty;
		}

		// Token: 0x04000024 RID: 36
		private MPChatVM _dataSource;

		// Token: 0x04000025 RID: 37
		private ChatLogMessageManager _chatLogMessageManager;

		// Token: 0x04000026 RID: 38
		private bool _canFocusWhileInMission = true;

		// Token: 0x04000027 RID: 39
		private bool _isTeamChatAvailable;

		// Token: 0x04000028 RID: 40
		private IGauntletMovie _movie;

		// Token: 0x04000029 RID: 41
		private bool _isEnabled = true;

		// Token: 0x0400002A RID: 42
		private const int MaxHistoryCountForSingleplayer = 250;

		// Token: 0x0400002B RID: 43
		private const int MaxHistoryCountForMultiplayer = 100;
	}
}
