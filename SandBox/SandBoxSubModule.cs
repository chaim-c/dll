using System;
using SandBox.AI;
using SandBox.CampaignBehaviors;
using SandBox.GameComponents;
using SandBox.Issues;
using SandBox.Objects;
using TaleWorlds.CampaignSystem;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.MountAndBlade;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;
using TaleWorlds.SaveSystem.Load;

namespace SandBox
{
	// Token: 0x0200002A RID: 42
	public class SandBoxSubModule : MBSubModuleBase
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00007A58 File Offset: 0x00005C58
		protected override void OnSubModuleLoad()
		{
			base.OnSubModuleLoad();
			Module.CurrentModule.SetEditorMissionTester(new SandBoxEditorMissionTester());
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00007A70 File Offset: 0x00005C70
		protected override void InitializeGameStarter(Game game, IGameStarter gameStarterObject)
		{
			if (game.GameType is Campaign)
			{
				gameStarterObject.AddModel(new SandboxAgentStatCalculateModel());
				gameStarterObject.AddModel(new SandboxStrikeMagnitudeModel());
				gameStarterObject.AddModel(new SandboxAgentApplyDamageModel());
				gameStarterObject.AddModel(new SandboxMissionDifficultyModel());
				gameStarterObject.AddModel(new SandboxApplyWeatherEffectsModel());
				gameStarterObject.AddModel(new SandboxAutoBlockModel());
				gameStarterObject.AddModel(new SandboxAgentDecideKilledOrUnconsciousModel());
				gameStarterObject.AddModel(new SandboxBattleBannerBearersModel());
				gameStarterObject.AddModel(new DefaultFormationArrangementModel());
				gameStarterObject.AddModel(new SandboxBattleMoraleModel());
				gameStarterObject.AddModel(new SandboxBattleInitializationModel());
				gameStarterObject.AddModel(new SandboxBattleSpawnModel());
				gameStarterObject.AddModel(new DefaultDamageParticleModel());
				gameStarterObject.AddModel(new DefaultItemPickupModel());
				CampaignGameStarter campaignGameStarter = gameStarterObject as CampaignGameStarter;
				if (campaignGameStarter != null)
				{
					campaignGameStarter.AddBehavior(new HideoutConversationsCampaignBehavior());
					campaignGameStarter.AddBehavior(new AlleyCampaignBehavior());
					campaignGameStarter.AddBehavior(new CommonTownsfolkCampaignBehavior());
					campaignGameStarter.AddBehavior(new CompanionRolesCampaignBehavior());
					campaignGameStarter.AddBehavior(new DefaultNotificationsCampaignBehavior());
					campaignGameStarter.AddBehavior(new ClanMemberRolesCampaignBehavior());
					campaignGameStarter.AddBehavior(new GuardsCampaignBehavior());
					campaignGameStarter.AddBehavior(new SettlementMusiciansCampaignBehavior());
					campaignGameStarter.AddBehavior(new BoardGameCampaignBehavior());
					campaignGameStarter.AddBehavior(new WorkshopsCharactersCampaignBehavior());
					campaignGameStarter.AddBehavior(new TradersCampaignBehavior());
					campaignGameStarter.AddBehavior(new ArenaMasterCampaignBehavior());
					campaignGameStarter.AddBehavior(new CommonVillagersCampaignBehavior());
					campaignGameStarter.AddBehavior(new HeirSelectionCampaignBehavior());
					campaignGameStarter.AddBehavior(new DefaultCutscenesCampaignBehavior());
					campaignGameStarter.AddBehavior(new RivalGangMovingInIssueBehavior());
					campaignGameStarter.AddBehavior(new RuralNotableInnAndOutIssueBehavior());
					campaignGameStarter.AddBehavior(new FamilyFeudIssueBehavior());
					campaignGameStarter.AddBehavior(new NotableWantsDaughterFoundIssueBehavior());
					campaignGameStarter.AddBehavior(new TheSpyPartyIssueQuestBehavior());
					campaignGameStarter.AddBehavior(new ProdigalSonIssueBehavior());
					campaignGameStarter.AddBehavior(new BarberCampaignBehavior());
					campaignGameStarter.AddBehavior(new SnareTheWealthyIssueBehavior());
					campaignGameStarter.AddBehavior(new RetirementCampaignBehavior());
					campaignGameStarter.AddBehavior(new StatisticsCampaignBehavior());
					campaignGameStarter.AddBehavior(new DumpIntegrityCampaignBehavior());
					campaignGameStarter.AddBehavior(new CaravanConversationsCampaignBehavior());
				}
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007C60 File Offset: 0x00005E60
		public override void OnCampaignStart(Game game, object starterObject)
		{
			Campaign campaign = game.GameType as Campaign;
			if (campaign != null)
			{
				SandBoxManager sandBoxManager = campaign.SandBoxManager;
				sandBoxManager.SandBoxMissionManager = new SandBoxMissionManager();
				sandBoxManager.AgentBehaviorManager = new AgentBehaviorManager();
				sandBoxManager.ModuleManager = new ModuleManager();
				sandBoxManager.SandBoxSaveManager = new SandBoxSaveManager();
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00007CB0 File Offset: 0x00005EB0
		private void OnRegisterTypes()
		{
			MBObjectManager.Instance.RegisterType<InstrumentData>("MusicInstrument", "MusicInstruments", 54U, true, false);
			MBObjectManager.Instance.RegisterType<SettlementMusicData>("MusicTrack", "MusicTracks", 55U, true, false);
			new DefaultMusicInstrumentData();
			MBObjectManager.Instance.LoadXML("MusicInstruments", false);
			MBObjectManager.Instance.LoadXML("MusicTracks", false);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007D14 File Offset: 0x00005F14
		public override void OnGameInitializationFinished(Game game)
		{
			Campaign campaign = game.GameType as Campaign;
			if (campaign != null)
			{
				campaign.CampaignMissionManager = new CampaignMissionManager();
				campaign.MapSceneCreator = new MapSceneCreator();
				campaign.EncyclopediaManager.CreateEncyclopediaPages();
				this.OnRegisterTypes();
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007D57 File Offset: 0x00005F57
		public override void RegisterSubModuleObjects(bool isSavedCampaign)
		{
			Campaign.Current.SandBoxManager.InitializeSandboxXMLs(isSavedCampaign);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007D69 File Offset: 0x00005F69
		public override void AfterRegisterSubModuleObjects(bool isSavedCampaign)
		{
			Campaign.Current.SandBoxManager.InitializeCharactersAfterLoad(isSavedCampaign);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007D7C File Offset: 0x00005F7C
		public override void OnInitialState()
		{
			base.OnInitialState();
			if (Module.CurrentModule.StartupInfo.IsContinueGame && !this._latestSaveLoaded)
			{
				this._latestSaveLoaded = true;
				SaveGameFileInfo[] saveFiles = MBSaveLoad.GetSaveFiles(null);
				if (saveFiles.IsEmpty<SaveGameFileInfo>())
				{
					return;
				}
				SandBoxSaveHelper.TryLoadSave(saveFiles.MaxBy((SaveGameFileInfo s) => s.MetaData.GetCreationTime()), new Action<LoadResult>(this.StartGame), null);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007DF6 File Offset: 0x00005FF6
		private void StartGame(LoadResult loadResult)
		{
			MBGameManager.StartNewGame(new SandBoxGameManager(loadResult));
			MouseManager.ShowCursor(false);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007E0C File Offset: 0x0000600C
		public override void OnGameLoaded(Game game, object starterObject)
		{
			Campaign campaign = game.GameType as Campaign;
			if (campaign != null)
			{
				SandBoxManager sandBoxManager = campaign.SandBoxManager;
				sandBoxManager.SandBoxMissionManager = new SandBoxMissionManager();
				sandBoxManager.AgentBehaviorManager = new AgentBehaviorManager();
				sandBoxManager.ModuleManager = new ModuleManager();
				sandBoxManager.SandBoxSaveManager = new SandBoxSaveManager();
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007E59 File Offset: 0x00006059
		protected override void OnBeforeInitialModuleScreenSetAsRoot()
		{
			base.OnBeforeInitialModuleScreenSetAsRoot();
			if (!this._initialized)
			{
				MBSaveLoad.Initialize(Module.CurrentModule.GlobalTextManager);
				this._initialized = true;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007E7F File Offset: 0x0000607F
		public override void OnConfigChanged()
		{
			if (Campaign.Current != null)
			{
				CampaignEventDispatcher.Instance.OnConfigChanged();
			}
		}

		// Token: 0x04000053 RID: 83
		private bool _initialized;

		// Token: 0x04000054 RID: 84
		private bool _latestSaveLoaded;
	}
}
