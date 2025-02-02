using System;
using System.Linq;
using Helpers;
using SandBox.BoardGames.AI;
using SandBox.Conversation;
using SandBox.Conversation.MissionLogics;
using SandBox.Objects.Usables;
using SandBox.Source.Missions.AgentBehaviors;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.Source.Missions.Handlers;

namespace SandBox.BoardGames.MissionLogics
{
	// Token: 0x020000CF RID: 207
	public class MissionBoardGameLogic : MissionLogic
	{
		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000A5D RID: 2653 RVA: 0x0004C920 File Offset: 0x0004AB20
		// (remove) Token: 0x06000A5E RID: 2654 RVA: 0x0004C958 File Offset: 0x0004AB58
		public event Action GameStarted;

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000A5F RID: 2655 RVA: 0x0004C990 File Offset: 0x0004AB90
		// (remove) Token: 0x06000A60 RID: 2656 RVA: 0x0004C9C8 File Offset: 0x0004ABC8
		public event Action GameEnded;

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000A61 RID: 2657 RVA: 0x0004C9FD File Offset: 0x0004ABFD
		// (set) Token: 0x06000A62 RID: 2658 RVA: 0x0004CA05 File Offset: 0x0004AC05
		public BoardGameBase Board { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0004CA0E File Offset: 0x0004AC0E
		// (set) Token: 0x06000A64 RID: 2660 RVA: 0x0004CA16 File Offset: 0x0004AC16
		public BoardGameAIBase AIOpponent { get; private set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x0004CA1F File Offset: 0x0004AC1F
		public bool IsOpposingAgentMovingToPlayingChair
		{
			get
			{
				return BoardGameAgentBehavior.IsAgentMovingToChair(this.OpposingAgent);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x0004CA2C File Offset: 0x0004AC2C
		// (set) Token: 0x06000A67 RID: 2663 RVA: 0x0004CA34 File Offset: 0x0004AC34
		public bool IsGameInProgress { get; private set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x0004CA3D File Offset: 0x0004AC3D
		public BoardGameHelper.BoardGameState BoardGameFinalState
		{
			get
			{
				return this._boardGameState;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x0004CA45 File Offset: 0x0004AC45
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x0004CA4D File Offset: 0x0004AC4D
		public CultureObject.BoardGameType CurrentBoardGame { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0004CA56 File Offset: 0x0004AC56
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x0004CA5E File Offset: 0x0004AC5E
		public BoardGameHelper.AIDifficulty Difficulty { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x0004CA67 File Offset: 0x0004AC67
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x0004CA6F File Offset: 0x0004AC6F
		public int BetAmount { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x0004CA78 File Offset: 0x0004AC78
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0004CA80 File Offset: 0x0004AC80
		public Agent OpposingAgent { get; private set; }

		// Token: 0x06000A71 RID: 2673 RVA: 0x0004CA8C File Offset: 0x0004AC8C
		public override void AfterStart()
		{
			base.AfterStart();
			this._opposingChair = base.Mission.Scene.FindEntityWithTag("gambler_npc").CollectObjects<Chair>().FirstOrDefault<Chair>();
			this._playerChair = base.Mission.Scene.FindEntityWithTag("gambler_player").CollectObjects<Chair>().FirstOrDefault<Chair>();
			foreach (StandingPoint standingPoint in this._opposingChair.StandingPoints)
			{
				standingPoint.IsDisabledForPlayers = true;
			}
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0004CB34 File Offset: 0x0004AD34
		public void SetStartingPlayer(bool playerOneStarts)
		{
			this._startingPlayer = (playerOneStarts ? PlayerTurn.PlayerOne : PlayerTurn.PlayerTwo);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0004CB43 File Offset: 0x0004AD43
		public void StartBoardGame()
		{
			this._startingBoardGame = true;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0004CB4C File Offset: 0x0004AD4C
		private void BoardGameInit(CultureObject.BoardGameType game)
		{
			if (this.Board == null)
			{
				switch (game)
				{
				case CultureObject.BoardGameType.Seega:
					this.Board = new BoardGameSeega(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAISeega(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.Puluc:
					this.Board = new BoardGamePuluc(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAIPuluc(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.Konane:
					this.Board = new BoardGameKonane(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAIKonane(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.MuTorere:
					this.Board = new BoardGameMuTorere(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAIMuTorere(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.Tablut:
					this.Board = new BoardGameTablut(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAITablut(this.Difficulty, this);
					break;
				case CultureObject.BoardGameType.BaghChal:
					this.Board = new BoardGameBaghChal(this, this._startingPlayer);
					this.AIOpponent = new BoardGameAIBaghChal(this.Difficulty, this);
					break;
				default:
					Debug.FailedAssert("[DEBUG]No board with this name was found.", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\BoardGames\\MissionLogics\\MissionBoardGameLogic.cs", "BoardGameInit", 122);
					break;
				}
				this.Board.Initialize();
				if (this.AIOpponent != null)
				{
					this.AIOpponent.Initialize();
				}
			}
			else
			{
				this.Board.SetStartingPlayer(this._startingPlayer);
				this.Board.InitializeUnits();
				this.Board.InitializeCapturedUnitsZones();
				this.Board.Reset();
				if (this.AIOpponent != null)
				{
					this.AIOpponent.SetDifficulty(this.Difficulty);
					this.AIOpponent.Initialize();
				}
			}
			if (this.Handler != null)
			{
				this.Handler.Install();
			}
			this._boardGameState = BoardGameHelper.BoardGameState.None;
			this.IsGameInProgress = true;
			this._isTavernGame = (CampaignMission.Current.Location == Settlement.CurrentSettlement.LocationComplex.GetLocationWithId("tavern"));
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x0004CD48 File Offset: 0x0004AF48
		public override void OnMissionTick(float dt)
		{
			if (base.Mission.IsInPhotoMode)
			{
				return;
			}
			if (this._startingBoardGame)
			{
				this._startingBoardGame = false;
				this.BoardGameInit(this.CurrentBoardGame);
				Action gameStarted = this.GameStarted;
				if (gameStarted == null)
				{
					return;
				}
				gameStarted();
				return;
			}
			else
			{
				if (this.IsGameInProgress)
				{
					this.Board.Tick(dt);
					return;
				}
				if (this.OpposingAgent != null && this.OpposingAgent.IsHero && Hero.OneToOneConversationHero == null && this.CheckIfBothSidesAreSitting())
				{
					this.StartBoardGame();
				}
				return;
			}
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0004CDD0 File Offset: 0x0004AFD0
		public void DetectOpposingAgent()
		{
			foreach (Agent agent in Mission.Current.Agents)
			{
				if (agent == ConversationMission.OneToOneConversationAgent)
				{
					this.OpposingAgent = agent;
					if (agent.IsHero)
					{
						BoardGameAgentBehavior.AddTargetChair(this.OpposingAgent, this._opposingChair);
					}
					AgentNavigator agentNavigator = this.OpposingAgent.GetComponent<CampaignAgentComponent>().AgentNavigator;
					this._specialTagCacheOfOpposingHero = agentNavigator.SpecialTargetTag;
					agentNavigator.SpecialTargetTag = "gambler_npc";
					break;
				}
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0004CE74 File Offset: 0x0004B074
		public bool CheckIfBothSidesAreSitting()
		{
			return Agent.Main != null && this.OpposingAgent != null && this._playerChair.IsAgentFullySitting(Agent.Main) && this._opposingChair.IsAgentFullySitting(this.OpposingAgent);
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0004CEAC File Offset: 0x0004B0AC
		public void PlayerOneWon(string message = "str_boardgame_victory_message")
		{
			Agent opposingAgent = this.OpposingAgent;
			this.SetGameOver(GameOverEnum.PlayerOneWon);
			this.ShowInquiry(message, opposingAgent);
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0004CED0 File Offset: 0x0004B0D0
		public void PlayerTwoWon(string message = "str_boardgame_defeat_message")
		{
			Agent opposingAgent = this.OpposingAgent;
			this.SetGameOver(GameOverEnum.PlayerTwoWon);
			this.ShowInquiry(message, opposingAgent);
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x0004CEF4 File Offset: 0x0004B0F4
		public void GameWasDraw(string message = "str_boardgame_draw_message")
		{
			Agent opposingAgent = this.OpposingAgent;
			this.SetGameOver(GameOverEnum.Draw);
			this.ShowInquiry(message, opposingAgent);
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x0004CF18 File Offset: 0x0004B118
		private void ShowInquiry(string message, Agent conversationAgent)
		{
			InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_boardgame", null).ToString(), GameTexts.FindText(message, null).ToString(), true, false, GameTexts.FindText("str_ok", null).ToString(), "", delegate()
			{
				this.StartConversationWithOpponentAfterGameEnd(conversationAgent);
			}, null, "", 0f, null, null, null), false, false);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x0004CF92 File Offset: 0x0004B192
		private void StartConversationWithOpponentAfterGameEnd(Agent conversationAgent)
		{
			MissionConversationLogic.Current.StartConversation(conversationAgent, false, false);
			this._boardGameState = BoardGameHelper.BoardGameState.None;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0004CFA8 File Offset: 0x0004B1A8
		public void SetGameOver(GameOverEnum gameOverInfo)
		{
			base.Mission.MainAgent.ClearTargetFrame();
			if (this.Handler != null && gameOverInfo != GameOverEnum.PlayerCanceledTheGame)
			{
				this.Handler.Uninstall();
			}
			Hero opposingHero = this.OpposingAgent.IsHero ? ((CharacterObject)this.OpposingAgent.Character).HeroObject : null;
			switch (gameOverInfo)
			{
			case GameOverEnum.PlayerOneWon:
				this._boardGameState = BoardGameHelper.BoardGameState.Win;
				break;
			case GameOverEnum.PlayerTwoWon:
				this._boardGameState = BoardGameHelper.BoardGameState.Loss;
				break;
			case GameOverEnum.Draw:
				this._boardGameState = BoardGameHelper.BoardGameState.Draw;
				break;
			case GameOverEnum.PlayerCanceledTheGame:
				this._boardGameState = BoardGameHelper.BoardGameState.None;
				break;
			}
			if (gameOverInfo != GameOverEnum.PlayerCanceledTheGame)
			{
				CampaignEventDispatcher.Instance.OnPlayerBoardGameOver(opposingHero, this._boardGameState);
			}
			Action gameEnded = this.GameEnded;
			if (gameEnded != null)
			{
				gameEnded();
			}
			BoardGameAgentBehavior.RemoveBoardGameBehaviorOfAgent(this.OpposingAgent);
			this.OpposingAgent.GetComponent<CampaignAgentComponent>().AgentNavigator.SpecialTargetTag = this._specialTagCacheOfOpposingHero;
			this.OpposingAgent = null;
			this.IsGameInProgress = false;
			BoardGameAIBase aiopponent = this.AIOpponent;
			if (aiopponent == null)
			{
				return;
			}
			aiopponent.OnSetGameOver();
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0004D0AC File Offset: 0x0004B2AC
		public void ForfeitGame()
		{
			this.Board.SetGameOverInfo(GameOverEnum.PlayerTwoWon);
			Agent opposingAgent = this.OpposingAgent;
			this.SetGameOver(this.Board.GameOverInfo);
			this.StartConversationWithOpponentAfterGameEnd(opposingAgent);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0004D0E4 File Offset: 0x0004B2E4
		public void AIForfeitGame()
		{
			this.Board.SetGameOverInfo(GameOverEnum.PlayerOneWon);
			this.SetGameOver(this.Board.GameOverInfo);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0004D103 File Offset: 0x0004B303
		public void RollDice()
		{
			this.Board.RollDice();
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0004D110 File Offset: 0x0004B310
		public bool RequiresDiceRolling()
		{
			switch (this.CurrentBoardGame)
			{
			case CultureObject.BoardGameType.Seega:
				return false;
			case CultureObject.BoardGameType.Puluc:
				return true;
			case CultureObject.BoardGameType.Konane:
				return false;
			case CultureObject.BoardGameType.MuTorere:
				return false;
			case CultureObject.BoardGameType.Tablut:
				return false;
			case CultureObject.BoardGameType.BaghChal:
				return false;
			default:
				return false;
			}
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0004D151 File Offset: 0x0004B351
		public void SetBetAmount(int bet)
		{
			this.BetAmount = bet;
		}

		// Token: 0x06000A83 RID: 2691 RVA: 0x0004D15A File Offset: 0x0004B35A
		public void SetCurrentDifficulty(BoardGameHelper.AIDifficulty difficulty)
		{
			this.Difficulty = difficulty;
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x0004D163 File Offset: 0x0004B363
		public void SetBoardGame(CultureObject.BoardGameType game)
		{
			this.CurrentBoardGame = game;
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0004D16C File Offset: 0x0004B36C
		public override InquiryData OnEndMissionRequest(out bool canLeave)
		{
			canLeave = true;
			return null;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0004D174 File Offset: 0x0004B374
		public static bool IsBoardGameAvailable()
		{
			Mission mission = Mission.Current;
			MissionBoardGameLogic missionBoardGameLogic = (mission != null) ? mission.GetMissionBehavior<MissionBoardGameLogic>() : null;
			Mission mission2 = Mission.Current;
			return ((mission2 != null) ? mission2.Scene : null) != null && missionBoardGameLogic != null && Mission.Current.Scene.FindEntityWithTag("boardgame") != null && missionBoardGameLogic.OpposingAgent == null;
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0004D1D8 File Offset: 0x0004B3D8
		public static bool IsThereActiveBoardGameWithHero(Hero hero)
		{
			Mission mission = Mission.Current;
			MissionBoardGameLogic missionBoardGameLogic = (mission != null) ? mission.GetMissionBehavior<MissionBoardGameLogic>() : null;
			Mission mission2 = Mission.Current;
			if (((mission2 != null) ? mission2.Scene : null) != null && Mission.Current.Scene.FindEntityWithTag("boardgame") != null && missionBoardGameLogic != null)
			{
				Agent opposingAgent = missionBoardGameLogic.OpposingAgent;
				return ((opposingAgent != null) ? opposingAgent.Character : null) == hero.CharacterObject;
			}
			return false;
		}

		// Token: 0x040003F7 RID: 1015
		private const string BoardGameEntityTag = "boardgame";

		// Token: 0x040003F8 RID: 1016
		private const string SpecialTargetGamblerNpcTag = "gambler_npc";

		// Token: 0x040003FB RID: 1019
		public IBoardGameHandler Handler;

		// Token: 0x040003FC RID: 1020
		private PlayerTurn _startingPlayer = PlayerTurn.PlayerTwo;

		// Token: 0x040003FD RID: 1021
		private Chair _playerChair;

		// Token: 0x040003FE RID: 1022
		private Chair _opposingChair;

		// Token: 0x040003FF RID: 1023
		private string _specialTagCacheOfOpposingHero;

		// Token: 0x04000400 RID: 1024
		private bool _isTavernGame;

		// Token: 0x04000401 RID: 1025
		private bool _startingBoardGame;

		// Token: 0x04000402 RID: 1026
		private BoardGameHelper.BoardGameState _boardGameState;
	}
}
