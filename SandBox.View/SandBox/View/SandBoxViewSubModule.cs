using System;
using System.Collections.Generic;
using SandBox.View.Conversation;
using SandBox.View.Map;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.CampaignSystem.MapEvents;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Buildings;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.CampaignSystem.ViewModelCollection;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Information.RundownTooltip;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.View.Tableaus;
using TaleWorlds.SaveSystem;
using TaleWorlds.SaveSystem.Load;
using TaleWorlds.ScreenSystem;

namespace SandBox.View
{
	// Token: 0x0200000C RID: 12
	public class SandBoxViewSubModule : MBSubModuleBase
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000045D1 File Offset: 0x000027D1
		public static ConversationViewManager ConversationViewManager
		{
			get
			{
				return SandBoxViewSubModule._instance._conversationViewManager;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000045DD File Offset: 0x000027DD
		public static IMapConversationDataProvider MapConversationDataProvider
		{
			get
			{
				return SandBoxViewSubModule._instance._mapConversationDataProvider;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000045E9 File Offset: 0x000027E9
		internal static Dictionary<UIntPtr, PartyVisual> VisualsOfEntities
		{
			get
			{
				return SandBoxViewSubModule._instance._visualsOfEntities;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000045F5 File Offset: 0x000027F5
		internal static Dictionary<UIntPtr, Tuple<MatrixFrame, PartyVisual>> FrameAndVisualOfEngines
		{
			get
			{
				return SandBoxViewSubModule._instance._frameAndVisualOfEngines;
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00004604 File Offset: 0x00002804
		protected override void OnSubModuleLoad()
		{
			base.OnSubModuleLoad();
			SandBoxViewSubModule._instance = this;
			SandBoxSaveHelper.OnStateChange += SandBoxViewSubModule.OnSaveHelperStateChange;
			this.RegisterTooltipTypes();
			Module.CurrentModule.AddInitialStateOption(new InitialStateOption("CampaignResumeGame", new TextObject("{=6mN03uTP}Saved Games", null), 0, delegate()
			{
				ScreenManager.PushScreen(SandBoxViewCreator.CreateSaveLoadScreen(false));
			}, () => this.IsSavedGamesDisabled(), null));
			Module.CurrentModule.AddInitialStateOption(new InitialStateOption("ContinueCampaign", new TextObject("{=0tJ1oarX}Continue Campaign", null), 1, new Action(this.ContinueCampaign), () => this.IsContinueCampaignDisabled(), null));
			Module.CurrentModule.AddInitialStateOption(new InitialStateOption("SandBoxNewGame", new TextObject("{=171fTtIN}SandBox", null), 3, delegate()
			{
				MBGameManager.StartNewGame(new SandBoxGameManager());
			}, () => this.IsSandboxDisabled(), this._sandBoxAchievementsHint));
			Module.CurrentModule.ImguiProfilerTick += this.OnImguiProfilerTick;
			this._mapConversationDataProvider = new DefaultMapConversationDataProvider();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000472B File Offset: 0x0000292B
		protected override void OnSubModuleUnloaded()
		{
			Module.CurrentModule.ImguiProfilerTick -= this.OnImguiProfilerTick;
			SandBoxSaveHelper.OnStateChange -= SandBoxViewSubModule.OnSaveHelperStateChange;
			this.UnregisterTooltipTypes();
			SandBoxViewSubModule._instance = null;
			base.OnSubModuleUnloaded();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00004766 File Offset: 0x00002966
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
			if (!this._isInitialized)
			{
				CampaignOptionsManager.Initialize();
				this._isInitialized = true;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00004782 File Offset: 0x00002982
		public override void OnCampaignStart(Game game, object starterObject)
		{
			base.OnCampaignStart(game, starterObject);
			if (Campaign.Current != null)
			{
				this._conversationViewManager = new ConversationViewManager();
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000479E File Offset: 0x0000299E
		public override void OnGameLoaded(Game game, object initializerObject)
		{
			this._conversationViewManager = new ConversationViewManager();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000047AB File Offset: 0x000029AB
		public override void OnAfterGameInitializationFinished(Game game, object starterObject)
		{
			base.OnAfterGameInitializationFinished(game, starterObject);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000047B5 File Offset: 0x000029B5
		public override void BeginGameStart(Game game)
		{
			base.BeginGameStart(game);
			if (Campaign.Current != null)
			{
				this._visualsOfEntities = new Dictionary<UIntPtr, PartyVisual>();
				this._frameAndVisualOfEngines = new Dictionary<UIntPtr, Tuple<MatrixFrame, PartyVisual>>();
				Campaign.Current.SaveHandler.MainHeroVisualSupplier = new MainHeroSaveVisualSupplier();
				TableauCacheManager.InitializeSandboxValues();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000047F4 File Offset: 0x000029F4
		public override void OnGameEnd(Game game)
		{
			if (this._visualsOfEntities != null)
			{
				foreach (PartyVisual partyVisual in this._visualsOfEntities.Values)
				{
					partyVisual.ReleaseResources();
				}
			}
			this._visualsOfEntities = null;
			this._frameAndVisualOfEngines = null;
			this._conversationViewManager = null;
			if (Campaign.Current != null)
			{
				Campaign.Current.SaveHandler.MainHeroVisualSupplier = null;
				TableauCacheManager.ReleaseSandboxValues();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004884 File Offset: 0x00002A84
		private ValueTuple<bool, TextObject> IsSavedGamesDisabled()
		{
			if (Module.CurrentModule.IsOnlyCoreContentEnabled)
			{
				return new ValueTuple<bool, TextObject>(true, new TextObject("{=V8BXjyYq}Disabled during installation.", null));
			}
			if (MBSaveLoad.NumberOfCurrentSaves == 0)
			{
				return new ValueTuple<bool, TextObject>(true, new TextObject("{=XcVVE1mp}No saved games found.", null));
			}
			return new ValueTuple<bool, TextObject>(false, TextObject.Empty);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000048D4 File Offset: 0x00002AD4
		private ValueTuple<bool, TextObject> IsContinueCampaignDisabled()
		{
			if (Module.CurrentModule.IsOnlyCoreContentEnabled)
			{
				return new ValueTuple<bool, TextObject>(true, new TextObject("{=V8BXjyYq}Disabled during installation.", null));
			}
			if (string.IsNullOrEmpty(BannerlordConfig.LatestSaveGameName))
			{
				return new ValueTuple<bool, TextObject>(true, new TextObject("{=aWMZQKXZ}Save the game at least once to continue", null));
			}
			SaveGameFileInfo saveFileWithName = MBSaveLoad.GetSaveFileWithName(BannerlordConfig.LatestSaveGameName);
			if (saveFileWithName == null)
			{
				return new ValueTuple<bool, TextObject>(true, new TextObject("{=60LTq0tQ}Can't find the save file for the latest save game.", null));
			}
			if (saveFileWithName.IsCorrupted)
			{
				return new ValueTuple<bool, TextObject>(true, new TextObject("{=t6W3UjG0}Save game file appear to be corrupted. Try starting a new campaign or load another one from Saved Games menu.", null));
			}
			return new ValueTuple<bool, TextObject>(false, TextObject.Empty);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00004962 File Offset: 0x00002B62
		private ValueTuple<bool, TextObject> IsSandboxDisabled()
		{
			if (Module.CurrentModule.IsOnlyCoreContentEnabled)
			{
				return new ValueTuple<bool, TextObject>(true, new TextObject("{=V8BXjyYq}Disabled during installation.", null));
			}
			return new ValueTuple<bool, TextObject>(false, TextObject.Empty);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004990 File Offset: 0x00002B90
		private void ContinueCampaign()
		{
			SaveGameFileInfo saveFileWithName = MBSaveLoad.GetSaveFileWithName(BannerlordConfig.LatestSaveGameName);
			if (saveFileWithName != null && !saveFileWithName.IsCorrupted)
			{
				SandBoxSaveHelper.TryLoadSave(saveFileWithName, new Action<LoadResult>(this.StartGame), null);
				return;
			}
			InformationManager.ShowInquiry(new InquiryData(new TextObject("{=oZrVNUOk}Error", null).ToString(), new TextObject("{=t6W3UjG0}Save game file appear to be corrupted. Try starting a new campaign or load another one from Saved Games menu.", null).ToString(), true, false, new TextObject("{=yS7PvrTD}OK", null).ToString(), null, null, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00004A15 File Offset: 0x00002C15
		private void StartGame(LoadResult loadResult)
		{
			MBGameManager.StartNewGame(new SandBoxGameManager(loadResult));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00004A24 File Offset: 0x00002C24
		private void OnImguiProfilerTick()
		{
			if (Campaign.Current == null)
			{
				return;
			}
			List<MobileParty> all = MobileParty.All;
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			foreach (MobileParty mobileParty in all)
			{
				if (mobileParty.IsVisible)
				{
					num++;
				}
				PartyVisual visualOfParty = PartyVisualManager.Current.GetVisualOfParty(mobileParty.Party);
				if (visualOfParty.HumanAgentVisuals != null)
				{
					num2++;
				}
				if (visualOfParty.MountAgentVisuals != null)
				{
					num2++;
				}
				if (visualOfParty.CaravanMountAgentVisuals != null)
				{
					num2++;
				}
				num3++;
			}
			Imgui.BeginMainThreadScope();
			Imgui.Begin("Bannerlord Campaign Statistics");
			Imgui.Columns(2, "", true);
			Imgui.Text("Name");
			Imgui.NextColumn();
			Imgui.Text("Count");
			Imgui.NextColumn();
			Imgui.Separator();
			Imgui.Text("Total Mobile Party");
			Imgui.NextColumn();
			Imgui.Text(num3.ToString());
			Imgui.NextColumn();
			Imgui.Text("Visible Mobile Party");
			Imgui.NextColumn();
			Imgui.Text(num.ToString());
			Imgui.NextColumn();
			Imgui.Text("Total Agent Visuals");
			Imgui.NextColumn();
			Imgui.Text(num2.ToString());
			Imgui.NextColumn();
			Imgui.End();
			Imgui.EndMainThreadScope();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004B6C File Offset: 0x00002D6C
		private void RegisterTooltipTypes()
		{
			InformationManager.RegisterTooltip<List<MobileParty>, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshEncounterTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<Track, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshTrackTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<MapEvent, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshMapEventTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<Army, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshArmyTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<MobileParty, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshMobilePartyTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<Hero, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshHeroTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<Settlement, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshSettlementTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<CharacterObject, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshCharacterTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<WeaponDesignElement, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshCraftingPartTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<InventoryLogic, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshInventoryTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<ItemObject, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshItemTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<Building, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshBuildingTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<Workshop, PropertyBasedTooltipVM>(new Action<PropertyBasedTooltipVM, object[]>(TooltipRefresherCollection.RefreshWorkshopTooltip), "PropertyBasedTooltip");
			InformationManager.RegisterTooltip<ExplainedNumber, RundownTooltipVM>(new Action<RundownTooltipVM, object[]>(TooltipRefresherCollection.RefreshExplainedNumberTooltip), "RundownTooltip");
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004CB0 File Offset: 0x00002EB0
		private void UnregisterTooltipTypes()
		{
			InformationManager.UnregisterTooltip<List<MobileParty>>();
			InformationManager.UnregisterTooltip<Track>();
			InformationManager.UnregisterTooltip<MapEvent>();
			InformationManager.UnregisterTooltip<Army>();
			InformationManager.UnregisterTooltip<MobileParty>();
			InformationManager.UnregisterTooltip<Hero>();
			InformationManager.UnregisterTooltip<Settlement>();
			InformationManager.UnregisterTooltip<CharacterObject>();
			InformationManager.UnregisterTooltip<WeaponDesignElement>();
			InformationManager.UnregisterTooltip<InventoryLogic>();
			InformationManager.UnregisterTooltip<ItemObject>();
			InformationManager.UnregisterTooltip<Building>();
			InformationManager.UnregisterTooltip<Workshop>();
			InformationManager.UnregisterTooltip<ExplainedNumber>();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004D03 File Offset: 0x00002F03
		public static void SetMapConversationDataProvider(IMapConversationDataProvider mapConversationDataProvider)
		{
			SandBoxViewSubModule._instance._mapConversationDataProvider = mapConversationDataProvider;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004D10 File Offset: 0x00002F10
		private static void OnSaveHelperStateChange(SandBoxSaveHelper.SaveHelperState currentState)
		{
			switch (currentState)
			{
			case SandBoxSaveHelper.SaveHelperState.Start:
			case SandBoxSaveHelper.SaveHelperState.LoadGame:
				LoadingWindow.EnableGlobalLoadingWindow();
				return;
			case SandBoxSaveHelper.SaveHelperState.Inquiry:
				LoadingWindow.DisableGlobalLoadingWindow();
				return;
			default:
				Debug.FailedAssert("Undefined save state for listener!", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox.View\\SandBoxViewSubModule.cs", "OnSaveHelperStateChange", 426);
				return;
			}
		}

		// Token: 0x04000013 RID: 19
		private TextObject _sandBoxAchievementsHint = new TextObject("{=j09m7S2E}Achievements are disabled in SandBox mode!", null);

		// Token: 0x04000014 RID: 20
		private bool _isInitialized;

		// Token: 0x04000015 RID: 21
		private ConversationViewManager _conversationViewManager;

		// Token: 0x04000016 RID: 22
		private IMapConversationDataProvider _mapConversationDataProvider;

		// Token: 0x04000017 RID: 23
		private Dictionary<UIntPtr, PartyVisual> _visualsOfEntities;

		// Token: 0x04000018 RID: 24
		private Dictionary<UIntPtr, Tuple<MatrixFrame, PartyVisual>> _frameAndVisualOfEngines;

		// Token: 0x04000019 RID: 25
		private static SandBoxViewSubModule _instance;
	}
}
