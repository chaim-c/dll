using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000214 RID: 532
	public class EditorGame : GameType
	{
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x00066B6D File Offset: 0x00064D6D
		public static EditorGame Current
		{
			get
			{
				return Game.Current.GameType as EditorGame;
			}
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00066B88 File Offset: 0x00064D88
		protected override void OnInitialize()
		{
			Game currentGame = base.CurrentGame;
			IGameStarter gameStarter = new BasicGameStarter();
			this.InitializeGameModels(gameStarter);
			base.GameManager.InitializeGameStarter(currentGame, gameStarter);
			base.GameManager.OnGameStart(base.CurrentGame, gameStarter);
			MBObjectManager objectManager = currentGame.ObjectManager;
			currentGame.SetBasicModels(gameStarter.Models);
			currentGame.CreateGameManager();
			base.GameManager.BeginGameStart(base.CurrentGame);
			currentGame.InitializeDefaultGameObjects();
			currentGame.LoadBasicFiles();
			this.LoadCustomGameXmls();
			objectManager.UnregisterNonReadyObjects();
			currentGame.SetDefaultEquipments(new Dictionary<string, Equipment>());
			objectManager.UnregisterNonReadyObjects();
			base.GameManager.OnNewCampaignStart(base.CurrentGame, null);
			base.GameManager.OnAfterCampaignStart(base.CurrentGame);
			base.GameManager.OnGameInitializationFinished(base.CurrentGame);
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x00066C50 File Offset: 0x00064E50
		private void InitializeGameModels(IGameStarter basicGameStarter)
		{
			basicGameStarter.AddModel(new CustomBattleAgentStatCalculateModel());
			basicGameStarter.AddModel(new CustomAgentApplyDamageModel());
			basicGameStarter.AddModel(new CustomBattleApplyWeatherEffectsModel());
			basicGameStarter.AddModel(new CustomBattleMoraleModel());
			basicGameStarter.AddModel(new CustomBattleInitializationModel());
			basicGameStarter.AddModel(new CustomBattleSpawnModel());
			basicGameStarter.AddModel(new DefaultAgentDecideKilledOrUnconsciousModel());
			basicGameStarter.AddModel(new DefaultRidingModel());
			basicGameStarter.AddModel(new DefaultStrikeMagnitudeModel());
			basicGameStarter.AddModel(new CustomBattleBannerBearersModel());
			basicGameStarter.AddModel(new DefaultFormationArrangementModel());
			basicGameStarter.AddModel(new DefaultDamageParticleModel());
			basicGameStarter.AddModel(new DefaultItemPickupModel());
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00066CEC File Offset: 0x00064EEC
		private void LoadCustomGameXmls()
		{
			base.ObjectManager.LoadXML("Items", false);
			base.ObjectManager.LoadXML("EquipmentRosters", false);
			base.ObjectManager.LoadXML("NPCCharacters", false);
			base.ObjectManager.LoadXML("SPCultures", false);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00066D3D File Offset: 0x00064F3D
		protected override void BeforeRegisterTypes(MBObjectManager objectManager)
		{
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x00066D3F File Offset: 0x00064F3F
		protected override void OnRegisterTypes(MBObjectManager objectManager)
		{
			objectManager.RegisterType<BasicCharacterObject>("NPCCharacter", "NPCCharacters", 43U, true, false);
			objectManager.RegisterType<BasicCultureObject>("Culture", "SPCultures", 17U, true, false);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00066D69 File Offset: 0x00064F69
		protected override void DoLoadingForGameType(GameTypeLoadingStates gameTypeLoadingState, out GameTypeLoadingStates nextState)
		{
			nextState = GameTypeLoadingStates.None;
			switch (gameTypeLoadingState)
			{
			case GameTypeLoadingStates.InitializeFirstStep:
				base.CurrentGame.Initialize();
				nextState = GameTypeLoadingStates.WaitSecondStep;
				return;
			case GameTypeLoadingStates.WaitSecondStep:
				nextState = GameTypeLoadingStates.LoadVisualsThirdState;
				return;
			case GameTypeLoadingStates.LoadVisualsThirdState:
				nextState = GameTypeLoadingStates.PostInitializeFourthState;
				break;
			case GameTypeLoadingStates.PostInitializeFourthState:
				break;
			default:
				return;
			}
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x00066D9B File Offset: 0x00064F9B
		public override void OnDestroy()
		{
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00066D9D File Offset: 0x00064F9D
		public override void OnStateChanged(GameState oldState)
		{
		}
	}
}
