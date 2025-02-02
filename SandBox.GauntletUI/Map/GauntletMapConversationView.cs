using System;
using System.Collections.Generic;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.BarterSystem;
using TaleWorlds.CampaignSystem.ComponentInterfaces;
using TaleWorlds.CampaignSystem.Conversation;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements.Locations;
using TaleWorlds.CampaignSystem.ViewModelCollection.Barter;
using TaleWorlds.CampaignSystem.ViewModelCollection.Conversation;
using TaleWorlds.CampaignSystem.ViewModelCollection.Map.MapConversation;
using TaleWorlds.Core;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.View;
using TaleWorlds.ScreenSystem;
using TaleWorlds.TwoDimension;

namespace SandBox.GauntletUI.Map
{
	// Token: 0x02000028 RID: 40
	[OverrideView(typeof(MapConversationView))]
	public class GauntletMapConversationView : MapConversationView, IConversationStateHandler
	{
		// Token: 0x0600016F RID: 367 RVA: 0x0000B390 File Offset: 0x00009590
		public GauntletMapConversationView(ConversationCharacterData playerCharacterData, ConversationCharacterData conversationPartnerData)
		{
			this._conversationStateQueue = new Queue<GauntletMapConversationView.ConversationStates>();
			this._playerCharacterData = new ConversationCharacterData?(playerCharacterData);
			this._conversationPartnerData = new ConversationCharacterData?(conversationPartnerData);
			this._barter = Campaign.Current.BarterManager;
			BarterManager barter = this._barter;
			barter.BarterBegin = (BarterManager.BarterBeginEventDelegate)Delegate.Combine(barter.BarterBegin, new BarterManager.BarterBeginEventDelegate(this.OnBarterBegin));
			BarterManager barter2 = this._barter;
			barter2.Closed = (BarterManager.BarterCloseEventDelegate)Delegate.Combine(barter2.Closed, new BarterManager.BarterCloseEventDelegate(this.OnBarterClosed));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B424 File Offset: 0x00009624
		private void OnBarterClosed()
		{
			this._layerAsGauntletLayer.ReleaseMovie(this._barterMovie);
			this._barterCategory.Unload();
			this._barterDataSource = null;
			this._isBarterActive = false;
			this._dataSource.IsBarterActive = false;
			BarterItemVM.IsFiveStackModifierActive = false;
			BarterItemVM.IsEntireStackModifierActive = false;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B474 File Offset: 0x00009674
		private void OnBarterBegin(BarterData args)
		{
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._barterCategory = spriteData.SpriteCategories["ui_barter"];
			this._barterCategory.Load(resourceContext, uiresourceDepot);
			this._barterDataSource = new BarterVM(args);
			this._barterDataSource.SetResetInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Reset"));
			this._barterDataSource.SetDoneInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Confirm"));
			this._barterDataSource.SetCancelInputKey(HotKeyManager.GetCategory("GenericPanelGameKeyCategory").GetHotKey("Exit"));
			this._barterMovie = this._layerAsGauntletLayer.LoadMovie("BarterScreen", this._barterDataSource);
			this._isBarterActive = true;
			this._dataSource.IsBarterActive = true;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000B550 File Offset: 0x00009750
		protected override void CreateLayout()
		{
			base.CreateLayout();
			SpriteData spriteData = UIResourceManager.SpriteData;
			TwoDimensionEngineResourceContext resourceContext = UIResourceManager.ResourceContext;
			ResourceDepot uiresourceDepot = UIResourceManager.UIResourceDepot;
			this._conversationCategory = spriteData.SpriteCategories["ui_conversation"];
			this._conversationCategory.Load(resourceContext, uiresourceDepot);
			Campaign.Current.ConversationManager.Handler = this;
			Game.Current.GameStateManager.RegisterActiveStateDisableRequest(this);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B5B8 File Offset: 0x000097B8
		private void OnContinue()
		{
			MapConversationVM dataSource = this._dataSource;
			bool flag;
			if (dataSource == null)
			{
				flag = false;
			}
			else
			{
				MissionConversationVM dialogController = dataSource.DialogController;
				int? num = (dialogController != null) ? new int?(dialogController.AnswerList.Count) : null;
				int num2 = 0;
				flag = (num.GetValueOrDefault() <= num2 & num != null);
			}
			if (flag && !this._isBarterActive && this._isConversationInstalled)
			{
				((IConversationStateHandler)this).ExecuteConversationContinue();
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000B625 File Offset: 0x00009825
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			this.Tick();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000B634 File Offset: 0x00009834
		protected override void OnIdleTick(float dt)
		{
			base.OnIdleTick(dt);
			this.Tick();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000B643 File Offset: 0x00009843
		protected override bool IsEscaped()
		{
			return !this._isConversationInstalled;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000B64E File Offset: 0x0000984E
		protected override bool IsOpeningEscapeMenuOnFocusChangeAllowed()
		{
			return true;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000B654 File Offset: 0x00009854
		private void OnAfterConversationInstalled()
		{
			if (this._conversationStateQueue.Count > 0)
			{
				GauntletMapConversationView.ConversationStates conversationStates = this._conversationStateQueue.Peek();
				if (conversationStates == GauntletMapConversationView.ConversationStates.OnActivate || conversationStates == GauntletMapConversationView.ConversationStates.OnContinue)
				{
					this.ProcessConversationState(this._conversationStateQueue.Dequeue());
					this.OnAfterConversationInstalled();
				}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000B69C File Offset: 0x0000989C
		private void Tick()
		{
			if (this._conversationStateQueue.Count > 0)
			{
				GauntletMapConversationView.ConversationStates state = this._conversationStateQueue.Dequeue();
				this.ProcessConversationState(state);
			}
			if (this._isConversationInstalled && ScreenManager.TopScreen == base.MapScreen && ScreenManager.FocusedLayer != base.Layer)
			{
				ScreenManager.TrySetFocus(base.Layer);
			}
			MapConversationVM dataSource = this._dataSource;
			bool flag;
			if (dataSource == null)
			{
				flag = false;
			}
			else
			{
				MissionConversationVM dialogController = dataSource.DialogController;
				int? num = (dialogController != null) ? new int?(dialogController.AnswerList.Count) : null;
				int num2 = 0;
				flag = (num.GetValueOrDefault() <= num2 & num != null);
			}
			if (flag && !this._isBarterActive && this._isConversationInstalled && this.IsReleasedInGauntletLayer("ContinueKey"))
			{
				UISoundsHelper.PlayUISound("event:/ui/default");
				((IConversationStateHandler)this).ExecuteConversationContinue();
			}
			if (this._barterDataSource != null)
			{
				if (this.IsReleasedInGauntletLayer("Exit"))
				{
					UISoundsHelper.PlayUISound("event:/ui/default");
					this._barterDataSource.ExecuteCancel();
				}
				else
				{
					if (this.IsReleasedInGauntletLayer("Confirm"))
					{
						BarterVM barterDataSource = this._barterDataSource;
						if (barterDataSource != null && !barterDataSource.IsOfferDisabled)
						{
							UISoundsHelper.PlayUISound("event:/ui/default");
							this._barterDataSource.ExecuteOffer();
							goto IL_195;
						}
					}
					if (this.IsReleasedInGauntletLayer("Reset"))
					{
						UISoundsHelper.PlayUISound("event:/ui/default");
						this._barterDataSource.ExecuteReset();
					}
				}
			}
			else if (this.IsReleasedInGauntletLayer("ToggleEscapeMenu"))
			{
				MapScreen mapScreen = base.MapScreen;
				if (mapScreen != null && mapScreen.IsEscapeMenuOpened)
				{
					base.MapScreen.CloseEscapeMenu();
				}
				else
				{
					MapScreen mapScreen2 = base.MapScreen;
					if (mapScreen2 != null)
					{
						mapScreen2.OpenEscapeMenu();
					}
				}
			}
			IL_195:
			BarterItemVM.IsFiveStackModifierActive = this.IsDownInGauntletLayer("FiveStackModifier");
			BarterItemVM.IsEntireStackModifierActive = this.IsDownInGauntletLayer("EntireStackModifier");
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000B85E File Offset: 0x00009A5E
		protected override void OnMenuModeTick(float dt)
		{
			base.OnMenuModeTick(dt);
			this.Tick();
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000B870 File Offset: 0x00009A70
		protected override void OnMapConversationUpdate(ConversationCharacterData playerConversationData, ConversationCharacterData partnerConversationData)
		{
			base.OnMapConversationUpdate(playerConversationData, partnerConversationData);
			float timeOfDay = CampaignTime.Now.CurrentHourInDay * 1f;
			MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(MobileParty.MainParty.Position2D);
			bool isCurrentTerrainUnderSnow = weatherEventInPosition == MapWeatherModel.WeatherEvent.Snowy || weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard;
			bool isInside = false;
			if (partnerConversationData.Character.HeroObject != null)
			{
				LocationComplex locationComplex = LocationComplex.Current;
				string text;
				if (locationComplex == null)
				{
					text = null;
				}
				else
				{
					Location locationOfCharacter = locationComplex.GetLocationOfCharacter(partnerConversationData.Character.HeroObject);
					text = ((locationOfCharacter != null) ? locationOfCharacter.StringId : null);
				}
				string a = text;
				isInside = (Hero.MainHero.CurrentSettlement != null && (a == "lordshall" || a == "tavern"));
			}
			MapConversationTableauData mapConversationTableauData = MapConversationTableauData.CreateFrom(playerConversationData, partnerConversationData, Campaign.Current.MapSceneWrapper.GetFaceTerrainType(MobileParty.MainParty.CurrentNavigationFace), timeOfDay, isCurrentTerrainUnderSnow, Hero.MainHero.CurrentSettlement, isInside, weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain, weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard);
			if (!GauntletMapConversationView.IsSame(mapConversationTableauData, this._tableauData))
			{
				this._dataSource.TableauData = mapConversationTableauData;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000B97E File Offset: 0x00009B7E
		private bool IsReleasedInGauntletLayer(string hotKeyID)
		{
			ScreenLayer layer = base.Layer;
			return layer != null && layer.Input.IsHotKeyReleased(hotKeyID);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000B997 File Offset: 0x00009B97
		private bool IsDownInGauntletLayer(string hotKeyID)
		{
			ScreenLayer layer = base.Layer;
			return layer != null && layer.Input.IsHotKeyDown(hotKeyID);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		private void OnClose()
		{
			MapState mapState = Game.Current.GameStateManager.LastOrDefault<MapState>();
			if (mapState == null)
			{
				return;
			}
			mapState.OnMapConversationOver();
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000B9CC File Offset: 0x00009BCC
		protected override void OnFinalize()
		{
			base.OnFinalize();
			base.Layer.IsFocusLayer = false;
			ScreenManager.TryLoseFocus(base.Layer);
			BarterManager barter = this._barter;
			barter.BarterBegin = (BarterManager.BarterBeginEventDelegate)Delegate.Remove(barter.BarterBegin, new BarterManager.BarterBeginEventDelegate(this.OnBarterBegin));
			BarterManager barter2 = this._barter;
			barter2.Closed = (BarterManager.BarterCloseEventDelegate)Delegate.Remove(barter2.Closed, new BarterManager.BarterCloseEventDelegate(this.OnBarterClosed));
			this._dataSource.OnFinalize();
			BarterVM barterDataSource = this._barterDataSource;
			if (barterDataSource != null)
			{
				barterDataSource.OnFinalize();
			}
			base.MapScreen.RemoveLayer(base.Layer);
			SpriteCategory conversationCategory = this._conversationCategory;
			if (conversationCategory != null)
			{
				conversationCategory.Unload();
			}
			base.Layer = null;
			this._barterMovie = null;
			this._dataSource = null;
			this._barterDataSource = null;
			Campaign.Current.ConversationManager.Handler = null;
			Game.Current.GameStateManager.UnregisterActiveStateDisableRequest(this);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		private string GetContinueKeyText()
		{
			if (Input.IsGamepadActive)
			{
				GameTexts.SetVariable("CONSOLE_KEY_NAME", HyperlinkTexts.GetKeyHyperlinkText(HotKeyManager.GetHotKeyId("ConversationHotKeyCategory", "ContinueKey")));
				return GameTexts.FindText("str_click_to_continue_console", null).ToString();
			}
			return GameTexts.FindText("str_click_to_continue", null).ToString();
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000BB14 File Offset: 0x00009D14
		private void ProcessConversationState(GauntletMapConversationView.ConversationStates state)
		{
			switch (state)
			{
			case GauntletMapConversationView.ConversationStates.OnInstall:
				this.CreateConversationTableau();
				this.OnAfterConversationInstalled();
				return;
			case GauntletMapConversationView.ConversationStates.OnUninstall:
				this.UninstallConversation();
				return;
			case GauntletMapConversationView.ConversationStates.OnActivate:
				break;
			case GauntletMapConversationView.ConversationStates.OnDeactivate:
				MBInformationManager.HideInformations();
				return;
			case GauntletMapConversationView.ConversationStates.OnContinue:
				this._dataSource.DialogController.OnConversationContinue();
				return;
			case GauntletMapConversationView.ConversationStates.ExecuteContinue:
				this._dataSource.DialogController.ExecuteContinue();
				break;
			default:
				return;
			}
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000BB7C File Offset: 0x00009D7C
		private void CreateConversationTableau()
		{
			float timeOfDay = CampaignTime.Now.CurrentHourInDay * 1f;
			MapWeatherModel.WeatherEvent weatherEventInPosition = Campaign.Current.Models.MapWeatherModel.GetWeatherEventInPosition(MobileParty.MainParty.Position2D);
			bool isCurrentTerrainUnderSnow = weatherEventInPosition == MapWeatherModel.WeatherEvent.Snowy || weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard;
			bool isInside = false;
			if (this._conversationPartnerData.Value.Character.HeroObject != null)
			{
				LocationComplex locationComplex = LocationComplex.Current;
				string text;
				if (locationComplex == null)
				{
					text = null;
				}
				else
				{
					Location locationOfCharacter = locationComplex.GetLocationOfCharacter(this._conversationPartnerData.Value.Character.HeroObject);
					text = ((locationOfCharacter != null) ? locationOfCharacter.StringId : null);
				}
				string a = text;
				isInside = (Hero.MainHero.CurrentSettlement != null && (a == "lordshall" || a == "tavern"));
			}
			this._tableauData = MapConversationTableauData.CreateFrom(this._playerCharacterData.Value, this._conversationPartnerData.Value, Campaign.Current.MapSceneWrapper.GetFaceTerrainType(MobileParty.MainParty.CurrentNavigationFace), timeOfDay, isCurrentTerrainUnderSnow, Hero.MainHero.CurrentSettlement, isInside, weatherEventInPosition == MapWeatherModel.WeatherEvent.HeavyRain, weatherEventInPosition == MapWeatherModel.WeatherEvent.Blizzard);
			this._dataSource.TableauData = this._tableauData;
			this._dataSource.IsTableauEnabled = true;
			this._layerAsGauntletLayer.GamepadNavigationContext.GainNavigationAfterFrames(1, null);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000BCC1 File Offset: 0x00009EC1
		private void UninstallConversation()
		{
			if (this._isConversationInstalled)
			{
				this.OnClose();
				this._isConversationInstalled = false;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		void IConversationStateHandler.OnConversationInstall()
		{
			if (!this._isConversationInstalled)
			{
				base.CreateConversationMission();
				this._dataSource = new MapConversationVM(new Action(this.OnContinue), new Func<string>(this.GetContinueKeyText));
				base.Layer = new GauntletLayer(205, "GauntletLayer", false);
				this._layerAsGauntletLayer = (base.Layer as GauntletLayer);
				this._layerAsGauntletLayer.LoadMovie("MapConversation", this._dataSource);
				base.Layer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.All);
				base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("Generic"));
				base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericPanelGameKeyCategory"));
				base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("GenericCampaignPanelsGameKeyCategory"));
				base.Layer.Input.RegisterHotKeyCategory(HotKeyManager.GetCategory("ConversationHotKeyCategory"));
				base.MapScreen.AddLayer(base.Layer);
				base.Layer.IsFocusLayer = true;
				ScreenManager.TrySetFocus(base.Layer);
				this._conversationStateQueue.Enqueue(GauntletMapConversationView.ConversationStates.OnInstall);
				this._isConversationInstalled = true;
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000BE0C File Offset: 0x0000A00C
		void IConversationStateHandler.OnConversationUninstall()
		{
			this._conversationStateQueue.Enqueue(GauntletMapConversationView.ConversationStates.OnUninstall);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000BE1A File Offset: 0x0000A01A
		void IConversationStateHandler.OnConversationActivate()
		{
			if (this._conversationStateQueue.Count > 0)
			{
				this._conversationStateQueue.Enqueue(GauntletMapConversationView.ConversationStates.OnActivate);
				return;
			}
			this.ProcessConversationState(GauntletMapConversationView.ConversationStates.OnActivate);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000BE3E File Offset: 0x0000A03E
		void IConversationStateHandler.OnConversationDeactivate()
		{
			if (this._conversationStateQueue.Count > 0)
			{
				this._conversationStateQueue.Enqueue(GauntletMapConversationView.ConversationStates.OnDeactivate);
				return;
			}
			this.ProcessConversationState(GauntletMapConversationView.ConversationStates.OnDeactivate);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000BE62 File Offset: 0x0000A062
		void IConversationStateHandler.OnConversationContinue()
		{
			if (this._conversationStateQueue.Count > 0)
			{
				this._conversationStateQueue.Enqueue(GauntletMapConversationView.ConversationStates.OnContinue);
				return;
			}
			this.ProcessConversationState(GauntletMapConversationView.ConversationStates.OnContinue);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000BE86 File Offset: 0x0000A086
		void IConversationStateHandler.ExecuteConversationContinue()
		{
			if (this._conversationStateQueue.Count > 0)
			{
				this._conversationStateQueue.Enqueue(GauntletMapConversationView.ConversationStates.ExecuteContinue);
				return;
			}
			this.ProcessConversationState(GauntletMapConversationView.ConversationStates.ExecuteContinue);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		private static bool IsSame(MapConversationTableauData first, MapConversationTableauData second)
		{
			return first != null && second != null && (GauntletMapConversationView.IsSame(first.PlayerCharacterData, second.PlayerCharacterData) && GauntletMapConversationView.IsSame(first.ConversationPartnerData, second.ConversationPartnerData) && first.ConversationTerrainType == second.ConversationTerrainType && first.IsCurrentTerrainUnderSnow == second.IsCurrentTerrainUnderSnow) && first.TimeOfDay == second.TimeOfDay;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000BF14 File Offset: 0x0000A114
		private static bool IsSame(ConversationCharacterData first, ConversationCharacterData second)
		{
			return first.Character == second.Character && first.NoHorse == second.NoHorse && first.NoWeapon == second.NoWeapon && first.Party == second.Party && first.SpawnedAfterFight == second.SpawnedAfterFight && first.IsCivilianEquipmentRequiredForLeader == second.IsCivilianEquipmentRequiredForLeader;
		}

		// Token: 0x040000AF RID: 175
		private GauntletLayer _layerAsGauntletLayer;

		// Token: 0x040000B0 RID: 176
		private MapConversationVM _dataSource;

		// Token: 0x040000B1 RID: 177
		private SpriteCategory _conversationCategory;

		// Token: 0x040000B2 RID: 178
		private MapConversationTableauData _tableauData;

		// Token: 0x040000B3 RID: 179
		private bool _isBarterActive;

		// Token: 0x040000B4 RID: 180
		private Queue<GauntletMapConversationView.ConversationStates> _conversationStateQueue;

		// Token: 0x040000B5 RID: 181
		private ConversationCharacterData? _playerCharacterData;

		// Token: 0x040000B6 RID: 182
		private ConversationCharacterData? _conversationPartnerData;

		// Token: 0x040000B7 RID: 183
		private bool _isConversationInstalled;

		// Token: 0x040000B8 RID: 184
		private BarterManager _barter;

		// Token: 0x040000B9 RID: 185
		private SpriteCategory _barterCategory;

		// Token: 0x040000BA RID: 186
		private BarterVM _barterDataSource;

		// Token: 0x040000BB RID: 187
		private IGauntletMovie _barterMovie;

		// Token: 0x02000048 RID: 72
		private enum ConversationStates
		{
			// Token: 0x0400019F RID: 415
			OnInstall,
			// Token: 0x040001A0 RID: 416
			OnUninstall,
			// Token: 0x040001A1 RID: 417
			OnActivate,
			// Token: 0x040001A2 RID: 418
			OnDeactivate,
			// Token: 0x040001A3 RID: 419
			OnContinue,
			// Token: 0x040001A4 RID: 420
			ExecuteContinue
		}
	}
}
