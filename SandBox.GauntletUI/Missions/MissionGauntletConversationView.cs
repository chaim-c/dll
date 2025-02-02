using System;
using SandBox.Conversation.MissionLogics;
using SandBox.View.Missions;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.ViewModelCollection.Conversation;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.Engine.Screens;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Mission;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.MountAndBlade.View.MissionViews;
using TaleWorlds.MountAndBlade.View.MissionViews.Singleplayer;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.Missions
{
	// Token: 0x02000017 RID: 23
	[OverrideView(typeof(MissionConversationView))]
	public class MissionGauntletConversationView : MissionView, IConversationStateHandler
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00008250 File Offset: 0x00006450
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00008258 File Offset: 0x00006458
		public MissionConversationLogic ConversationHandler { get; private set; }

		// Token: 0x060000EA RID: 234 RVA: 0x00008261 File Offset: 0x00006461
		public MissionGauntletConversationView()
		{
			this.ViewOrderPriority = 49;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008274 File Offset: 0x00006474
		public override void OnMissionScreenTick(float dt)
		{
			base.OnMissionScreenTick(dt);
			MissionGauntletEscapeMenuBase escapeView = this._escapeView;
			if ((escapeView == null || !escapeView.IsActive) && this._gauntletLayer != null)
			{
				MissionConversationVM dataSource = this._dataSource;
				if (dataSource != null && dataSource.AnswerList.Count <= 0 && base.Mission.Mode != MissionMode.Barter)
				{
					if (!this.IsReleasedInSceneLayer("ContinueClick", false))
					{
						if (!this.IsReleasedInGauntletLayer("ContinueKey", true))
						{
							goto IL_9D;
						}
						MissionConversationVM dataSource2 = this._dataSource;
						if (dataSource2 == null || dataSource2.SelectedAnOptionOrLinkThisFrame)
						{
							goto IL_9D;
						}
					}
					MissionConversationVM dataSource3 = this._dataSource;
					if (dataSource3 != null)
					{
						dataSource3.ExecuteContinue();
					}
				}
				IL_9D:
				if (this._gauntletLayer != null && this.IsGameKeyReleasedInAnyLayer("ToggleEscapeMenu", true))
				{
					base.MissionScreen.OnEscape();
				}
				if (this._dataSource != null)
				{
					this._dataSource.SelectedAnOptionOrLinkThisFrame = false;
				}
				if (base.MissionScreen.SceneLayer.Input.IsKeyDown(InputKey.RightMouseButton))
				{
					GauntletLayer gauntletLayer = this._gauntletLayer;
					if (gauntletLayer == null)
					{
						return;
					}
					gauntletLayer.InputRestrictions.SetMouseVisibility(false);
					return;
				}
				else
				{
					GauntletLayer gauntletLayer2 = this._gauntletLayer;
					if (gauntletLayer2 == null)
					{
						return;
					}
					gauntletLayer2.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
				}
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000083A0 File Offset: 0x000065A0
		public override void OnMissionScreenFinalize()
		{
			Campaign.Current.ConversationManager.Handler = null;
			if (this._dataSource != null)
			{
				MissionConversationVM dataSource = this._dataSource;
				if (dataSource != null)
				{
					dataSource.OnFinalize();
				}
				this._dataSource = null;
			}
			this._gauntletLayer = null;
			this.ConversationHandler = null;
			base.OnMissionScreenFinalize();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000083F1 File Offset: 0x000065F1
		public override void EarlyStart()
		{
			base.EarlyStart();
			this.ConversationHandler = base.Mission.GetMissionBehavior<MissionConversationLogic>();
			this._conversationCameraView = base.Mission.GetMissionBehavior<MissionConversationCameraView>();
			Campaign.Current.ConversationManager.Handler = this;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000842B File Offset: 0x0000662B
		public override void OnMissionScreenActivate()
		{
			base.OnMissionScreenActivate();
			if (this._dataSource != null)
			{
				base.MissionScreen.SetLayerCategoriesStateAndDeactivateOthers(new string[]
				{
					"Conversation",
					"SceneLayer"
				}, true);
				ScreenManager.TrySetFocus(this._gauntletLayer);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00008468 File Offset: 0x00006668
		void IConversationStateHandler.OnConversationInstall()
		{
			base.MissionScreen.SetConversationActive(true);
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._conversationCategory = spriteData.SpriteCategories["ui_conversation"];
			this._conversationCategory.Load(resourceContext, uiresourceDepot);
			this._dataSource = new MissionConversationVM(new Func<string>(this.GetContinueKeyText), false);
			this._gauntletLayer = new GauntletLayer(this.ViewOrderPriority, "Conversation", false);
			this._gauntletLayer.LoadMovie("SPConversation", this._dataSource);
			GameKeyContext category = HotKeyManager.GetCategory("ConversationHotKeyCategory");
			this._gauntletLayer.Input.RegisterHotKeyCategory(category);
			if (!base.MissionScreen.SceneLayer.Input.IsCategoryRegistered(category))
			{
				base.MissionScreen.SceneLayer.Input.RegisterHotKeyCategory(category);
			}
			this._gauntletLayer.IsFocusLayer = true;
			this._gauntletLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
			this._escapeView = base.Mission.GetMissionBehavior<MissionGauntletEscapeMenuBase>();
			base.MissionScreen.AddLayer(this._gauntletLayer);
			base.MissionScreen.SetLayerCategoriesStateAndDeactivateOthers(new string[]
			{
				"Conversation",
				"SceneLayer"
			}, true);
			ScreenManager.TrySetFocus(this._gauntletLayer);
			this._conversationManager = Campaign.Current.ConversationManager;
			InformationManager.ClearAllMessages();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000085C8 File Offset: 0x000067C8
		public override void OnMissionModeChange(MissionMode oldMissionMode, bool atStart)
		{
			base.OnMissionModeChange(oldMissionMode, atStart);
			if (oldMissionMode == MissionMode.Barter && base.Mission.Mode == MissionMode.Conversation)
			{
				ScreenManager.TrySetFocus(this._gauntletLayer);
			}
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000085F0 File Offset: 0x000067F0
		void IConversationStateHandler.OnConversationUninstall()
		{
			base.MissionScreen.SetConversationActive(false);
			if (this._dataSource != null)
			{
				MissionConversationVM dataSource = this._dataSource;
				if (dataSource != null)
				{
					dataSource.OnFinalize();
				}
				this._dataSource = null;
			}
			this._conversationCategory.Unload();
			this._gauntletLayer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(this._gauntletLayer);
			this._gauntletLayer.InputRestrictions.ResetInputRestrictions();
			base.MissionScreen.SetLayerCategoriesStateAndToggleOthers(new string[]
			{
				"Conversation"
			}, false);
			base.MissionScreen.SetLayerCategoriesState(new string[]
			{
				"SceneLayer"
			}, true);
			base.MissionScreen.RemoveLayer(this._gauntletLayer);
			this._gauntletLayer = null;
			this._escapeView = null;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000086B0 File Offset: 0x000068B0
		private string GetContinueKeyText()
		{
			if (TaleWorlds.InputSystem.Input.IsGamepadActive)
			{
				GameTexts.SetVariable("CONSOLE_KEY_NAME", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("ConversationHotKeyCategory", "ContinueKey")));
				return GameTexts.FindText("str_click_to_continue_console", null).ToString();
			}
			return GameTexts.FindText("str_click_to_continue", null).ToString();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00008703 File Offset: 0x00006903
		void IConversationStateHandler.OnConversationActivate()
		{
			base.MissionScreen.SetLayerCategoriesStateAndDeactivateOthers(new string[]
			{
				"Conversation",
				"SceneLayer"
			}, true);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00008727 File Offset: 0x00006927
		void IConversationStateHandler.OnConversationDeactivate()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000872E File Offset: 0x0000692E
		void IConversationStateHandler.OnConversationContinue()
		{
			this._dataSource.OnConversationContinue();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000873B File Offset: 0x0000693B
		void IConversationStateHandler.ExecuteConversationContinue()
		{
			this._dataSource.ExecuteContinue();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008748 File Offset: 0x00006948
		private bool IsGameKeyReleasedInAnyLayer(string hotKeyID, bool isDownAndReleased)
		{
			bool flag = this.IsReleasedInSceneLayer(hotKeyID, isDownAndReleased);
			bool flag2 = this.IsReleasedInGauntletLayer(hotKeyID, isDownAndReleased);
			return flag || flag2;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00008768 File Offset: 0x00006968
		private bool IsReleasedInSceneLayer(string hotKeyID, bool isDownAndReleased)
		{
			if (isDownAndReleased)
			{
				SceneLayer sceneLayer = base.MissionScreen.SceneLayer;
				return sceneLayer != null && sceneLayer.Input.IsHotKeyDownAndReleased(hotKeyID);
			}
			SceneLayer sceneLayer2 = base.MissionScreen.SceneLayer;
			return sceneLayer2 != null && sceneLayer2.Input.IsHotKeyReleased(hotKeyID);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000087A6 File Offset: 0x000069A6
		private bool IsReleasedInGauntletLayer(string hotKeyID, bool isDownAndReleased)
		{
			if (isDownAndReleased)
			{
				GauntletLayer gauntletLayer = this._gauntletLayer;
				return gauntletLayer != null && gauntletLayer.Input.IsHotKeyDownAndReleased(hotKeyID);
			}
			GauntletLayer gauntletLayer2 = this._gauntletLayer;
			return gauntletLayer2 != null && gauntletLayer2.Input.IsHotKeyReleased(hotKeyID);
		}

		// Token: 0x04000064 RID: 100
		private MissionConversationVM _dataSource;

		// Token: 0x04000065 RID: 101
		private GauntletLayer _gauntletLayer;

		// Token: 0x04000066 RID: 102
		private ConversationManager _conversationManager;

		// Token: 0x04000068 RID: 104
		private MissionConversationCameraView _conversationCameraView;

		// Token: 0x04000069 RID: 105
		private MissionGauntletEscapeMenuBase _escapeView;

		// Token: 0x0400006A RID: 106
		private SpriteCategory _conversationCategory;
	}
}
