using System;
using System.Collections.Generic;
using SandBox.BoardGames.AI;
using SandBox.BoardGames.MissionLogics;
using SandBox.BoardGames.Pawns;
using SandBox.BoardGames.Tiles;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.InputSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.BoardGames
{
	// Token: 0x020000BA RID: 186
	public abstract class BoardGameBase
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060008F2 RID: 2290
		public abstract int TileCount { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060008F3 RID: 2291
		protected abstract bool RotateBoard { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060008F4 RID: 2292
		protected abstract bool PreMovementStagePresent { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060008F5 RID: 2293
		protected abstract bool DiceRollRequired { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00043F97 File Offset: 0x00042197
		protected virtual int UnitsToPlacePerTurnInPreMovementStage
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00043F9A File Offset: 0x0004219A
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00043FA2 File Offset: 0x000421A2
		protected virtual PawnBase SelectedUnit
		{
			get
			{
				return this._selectedUnit;
			}
			set
			{
				this.OnBeforeSelectedUnitChanged(this._selectedUnit, value);
				this._selectedUnit = value;
				this.OnAfterSelectedUnitChanged();
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00043FBE File Offset: 0x000421BE
		public TextObject Name { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060008FA RID: 2298 RVA: 0x00043FC6 File Offset: 0x000421C6
		// (set) Token: 0x060008FB RID: 2299 RVA: 0x00043FCE File Offset: 0x000421CE
		public bool InPreMovementStage { get; protected set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060008FC RID: 2300 RVA: 0x00043FD7 File Offset: 0x000421D7
		// (set) Token: 0x060008FD RID: 2301 RVA: 0x00043FDF File Offset: 0x000421DF
		public TileBase[] Tiles { get; protected set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060008FE RID: 2302 RVA: 0x00043FE8 File Offset: 0x000421E8
		// (set) Token: 0x060008FF RID: 2303 RVA: 0x00043FF0 File Offset: 0x000421F0
		public List<PawnBase> PlayerOneUnits { get; protected set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000900 RID: 2304 RVA: 0x00043FF9 File Offset: 0x000421F9
		// (set) Token: 0x06000901 RID: 2305 RVA: 0x00044001 File Offset: 0x00042201
		public List<PawnBase> PlayerTwoUnits { get; protected set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x0004400A File Offset: 0x0004220A
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x00044012 File Offset: 0x00042212
		public int LastDice { get; protected set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x0004401B File Offset: 0x0004221B
		public bool IsReady
		{
			get
			{
				return this.ReadyToPlay && !this.SettingUpBoard;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00044030 File Offset: 0x00042230
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x00044038 File Offset: 0x00042238
		public PlayerTurn PlayerWhoStarted { get; private set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00044041 File Offset: 0x00042241
		// (set) Token: 0x06000908 RID: 2312 RVA: 0x00044049 File Offset: 0x00042249
		public GameOverEnum GameOverInfo { get; private set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00044052 File Offset: 0x00042252
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0004405A File Offset: 0x0004225A
		public PlayerTurn PlayerTurn { get; protected set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x00044063 File Offset: 0x00042263
		protected IInputContext InputManager
		{
			get
			{
				return this.MissionHandler.Mission.InputManager;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00044075 File Offset: 0x00042275
		protected List<PawnBase> PawnSelectFilter { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x0004407D File Offset: 0x0004227D
		protected BoardGameAIBase AIOpponent
		{
			get
			{
				return this.MissionHandler.AIOpponent;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0004408A File Offset: 0x0004228A
		private bool DiceRolled
		{
			get
			{
				return this.LastDice != -1;
			}
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00044098 File Offset: 0x00042298
		protected BoardGameBase(MissionBoardGameLogic mission, TextObject name, PlayerTurn startingPlayer)
		{
			this.Name = name;
			this.MissionHandler = mission;
			this.SetStartingPlayer(startingPlayer);
			this.PlayerOnePool = new CapturedPawnsPool();
			this.PlayerTwoPool = new CapturedPawnsPool();
			this.PlayerOneUnits = new List<PawnBase>();
			this.PlayerTwoUnits = new List<PawnBase>();
			this.PawnSelectFilter = new List<PawnBase>();
		}

		// Token: 0x06000910 RID: 2320
		public abstract void InitializeUnits();

		// Token: 0x06000911 RID: 2321
		public abstract void InitializeTiles();

		// Token: 0x06000912 RID: 2322
		public abstract void InitializeSound();

		// Token: 0x06000913 RID: 2323
		public abstract List<Move> CalculateValidMoves(PawnBase pawn);

		// Token: 0x06000914 RID: 2324
		protected abstract PawnBase SelectPawn(PawnBase pawn);

		// Token: 0x06000915 RID: 2325
		protected abstract bool CheckGameEnded();

		// Token: 0x06000916 RID: 2326
		protected abstract void OnAfterBoardSetUp();

		// Token: 0x06000917 RID: 2327 RVA: 0x00044117 File Offset: 0x00042317
		protected virtual void OnAfterBoardRotated()
		{
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x00044119 File Offset: 0x00042319
		protected virtual void OnBeforeEndTurn()
		{
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0004411B File Offset: 0x0004231B
		public virtual void RollDice()
		{
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0004411D File Offset: 0x0004231D
		protected virtual void UpdateAllTilesPositions()
		{
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0004411F File Offset: 0x0004231F
		public virtual void InitializeDiceBoard()
		{
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x00044124 File Offset: 0x00042324
		public virtual void Reset()
		{
			this.PlayerOnePool.PawnCount = 0;
			this.PlayerTwoPool.PawnCount = 0;
			this.ClearValidMoves();
			this.SelectedUnit = null;
			this.PawnSelectFilter.Clear();
			this.GameOverInfo = GameOverEnum.GameStillInProgress;
			this._draggingSelectedUnit = false;
			this.JustStoppedDraggingUnit = false;
			this._draggingTimer = 0f;
			BoardGameAIBase aiopponent = this.MissionHandler.AIOpponent;
			if (aiopponent != null)
			{
				aiopponent.ResetThinking();
			}
			this.ReadyToPlay = false;
			this._firstTickAfterReady = true;
			this._rotationCompleted = !this.RotateBoard;
			this.SettingUpBoard = true;
			this.UnfocusAllPawns();
			for (int i = 0; i < this.TileCount; i++)
			{
				this.Tiles[i].Reset();
			}
			this.MovesLeftToEndTurn = (this.PreMovementStagePresent ? this.UnitsToPlacePerTurnInPreMovementStage : 1);
			this.LastDice = -1;
			this._waitingAIForfeitResponse = false;
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x00044204 File Offset: 0x00042404
		protected virtual void OnPawnArrivesGoalPosition(PawnBase pawn, Vec3 prevPos, Vec3 currentPos)
		{
			if (this.IsReady && pawn.IsPlaced && !pawn.Captured && pawn.MovingToDifferentTile)
			{
				this.MovesLeftToEndTurn--;
			}
			pawn.MovingToDifferentTile = false;
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0004423B File Offset: 0x0004243B
		protected virtual void HandlePreMovementStage(float dt)
		{
			Debug.FailedAssert("HandlePreMovementStage is not implemented for " + this.MissionHandler.CurrentBoardGame, "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\BoardGames\\BoardGameBase.cs", "HandlePreMovementStage", 288);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0004426C File Offset: 0x0004246C
		public virtual void InitializeCapturedUnitsZones()
		{
			this.PlayerOnePool.Entity = Mission.Current.Scene.FindEntityWithTag((this.PlayerWhoStarted == PlayerTurn.PlayerOne) ? "captured_pawns_pool_1" : "captured_pawns_pool_2");
			this.PlayerOnePool.PawnCount = 0;
			this.PlayerTwoPool.Entity = Mission.Current.Scene.FindEntityWithTag((this.PlayerWhoStarted == PlayerTurn.PlayerOne) ? "captured_pawns_pool_2" : "captured_pawns_pool_1");
			this.PlayerTwoPool.PawnCount = 0;
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x000442ED File Offset: 0x000424ED
		protected virtual void HandlePreMovementStageAI(Move move)
		{
			Debug.FailedAssert("HandlePreMovementStageAI is not implemented for " + this.MissionHandler.CurrentBoardGame, "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\BoardGames\\BoardGameBase.cs", "HandlePreMovementStageAI", 306);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0004431D File Offset: 0x0004251D
		public virtual void SetPawnCaptured(PawnBase pawn, bool fake = false)
		{
			pawn.Captured = true;
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x00044328 File Offset: 0x00042528
		public virtual List<List<Move>> CalculateAllValidMoves(BoardGameSide side)
		{
			List<List<Move>> list = new List<List<Move>>(100);
			foreach (PawnBase pawn in ((side == BoardGameSide.AI) ? this.PlayerTwoUnits : this.PlayerOneUnits))
			{
				list.Add(this.CalculateValidMoves(pawn));
			}
			return list;
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00044398 File Offset: 0x00042598
		protected virtual void SwitchPlayerTurn()
		{
			this.MissionHandler.Handler.SwitchTurns();
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x000443AA File Offset: 0x000425AA
		protected virtual void MovePawnToTile(PawnBase pawn, TileBase tile, bool instantMove = false, bool displayMessage = true)
		{
			this.MovePawnToTileDelayed(pawn, tile, instantMove, displayMessage, 0f);
		}

		// Token: 0x06000925 RID: 2341 RVA: 0x000443BC File Offset: 0x000425BC
		protected virtual void MovePawnToTileDelayed(PawnBase pawn, TileBase tile, bool instantMove, bool displayMessage, float delay)
		{
			this.ClearValidMoves();
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x000443C4 File Offset: 0x000425C4
		protected virtual void OnAfterDiceRollAnimation()
		{
			if (this.LastDice != -1)
			{
				this.MissionHandler.Handler.DiceRoll(this.LastDice);
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x000443E5 File Offset: 0x000425E5
		public void SetUserRay(Vec3 rayBegin, Vec3 rayEnd)
		{
			this._userRayBegin = rayBegin;
			this._userRayEnd = rayEnd;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000443F8 File Offset: 0x000425F8
		public void SetStartingPlayer(PlayerTurn player)
		{
			this.HasToMovePawnsAcross = (this.PlayerWhoStarted != player);
			if (player == PlayerTurn.PlayerOne)
			{
				this._rotationTarget = 0f;
			}
			else if (player == PlayerTurn.PlayerTwo)
			{
				this._rotationTarget = 3.1415927f;
			}
			else
			{
				Debug.FailedAssert("Unexpected starting player caught: " + player, "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\BoardGames\\BoardGameBase.cs", "SetStartingPlayer", 376);
			}
			this.PlayerWhoStarted = player;
			this.PlayerTurn = player;
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0004446C File Offset: 0x0004266C
		public void SetGameOverInfo(GameOverEnum info)
		{
			this.GameOverInfo = info;
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x00044478 File Offset: 0x00042678
		public bool HasMovesAvailable(ref List<List<Move>> moves)
		{
			foreach (List<Move> list in moves)
			{
				if (list != null && list.Count > 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000444D4 File Offset: 0x000426D4
		public int GetTotalMovesAvailable(ref List<List<Move>> moves)
		{
			int num = 0;
			foreach (List<Move> list in moves)
			{
				if (list != null)
				{
					num += list.Count;
				}
			}
			return num;
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0004452C File Offset: 0x0004272C
		public void PlayDiceRollSound()
		{
			Vec3 globalPosition = this.DiceBoard.GlobalPosition;
			this.MissionHandler.Mission.MakeSound(this.DiceRollSoundCodeID, globalPosition, true, false, -1, -1);
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x00044560 File Offset: 0x00042760
		public int GetPlayerOneUnitsAlive()
		{
			int num = 0;
			using (List<PawnBase>.Enumerator enumerator = this.PlayerOneUnits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Captured)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x000445BC File Offset: 0x000427BC
		public int GetPlayerTwoUnitsAlive()
		{
			int num = 0;
			using (List<PawnBase>.Enumerator enumerator = this.PlayerTwoUnits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Captured)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x00044618 File Offset: 0x00042818
		public int GetPlayerOneUnitsDead()
		{
			int num = 0;
			using (List<PawnBase>.Enumerator enumerator = this.PlayerOneUnits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Captured)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x00044674 File Offset: 0x00042874
		public int GetPlayerTwoUnitsDead()
		{
			int num = 0;
			using (List<PawnBase>.Enumerator enumerator = this.PlayerTwoUnits.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Captured)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000446D0 File Offset: 0x000428D0
		public void Initialize()
		{
			this.BoardEntity = Mission.Current.Scene.FindEntityWithTag("boardgame");
			this.InitializeUnits();
			this.InitializeTiles();
			this.InitializeCapturedUnitsZones();
			this.InitializeDiceBoard();
			this.InitializeSound();
			this.Reset();
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00044710 File Offset: 0x00042910
		protected void RemovePawnFromBoard(PawnBase pawn, float speed, bool instantMove = false)
		{
			CapturedPawnsPool capturedPawnsPool = pawn.PlayerOne ? this.PlayerOnePool : this.PlayerTwoPool;
			IEnumerable<GameEntity> children = capturedPawnsPool.Entity.GetChildren();
			GameEntity gameEntity = null;
			foreach (GameEntity gameEntity2 in children)
			{
				if (gameEntity2.HasTag("pawn_" + capturedPawnsPool.PawnCount))
				{
					gameEntity = gameEntity2;
					break;
				}
			}
			capturedPawnsPool.PawnCount++;
			Vec3 origin = gameEntity.GetGlobalFrame().origin;
			float num = pawn.Entity.GlobalPosition.z - origin.z;
			float num2 = 0.001f;
			if (num > num2)
			{
				Vec3 goal = origin;
				goal.z = pawn.Entity.GlobalPosition.z;
				pawn.AddGoalPosition(goal);
			}
			else if (num < -num2)
			{
				Vec3 globalPosition = pawn.Entity.GlobalPosition;
				globalPosition.z = origin.z;
				pawn.AddGoalPosition(globalPosition);
			}
			pawn.AddGoalPosition(origin);
			pawn.MovePawnToGoalPositions(instantMove, speed, false);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00044838 File Offset: 0x00042A38
		public bool Tick(float dt)
		{
			foreach (PawnBase pawnBase in this.PlayerOneUnits)
			{
				pawnBase.Tick(dt);
			}
			foreach (PawnBase pawnBase2 in this.PlayerTwoUnits)
			{
				pawnBase2.Tick(dt);
			}
			for (int i = 0; i < this.TileCount; i++)
			{
				this.Tiles[i].Tick(dt);
			}
			if (this.MovingPawnPresent() || !this.DoneSettingUpBoard() || !this.ReadyToPlay)
			{
				return false;
			}
			if (this._firstTickAfterReady)
			{
				this._firstTickAfterReady = false;
				this.MissionHandler.Handler.Activate();
			}
			if (this.IsReady)
			{
				if (this._draggingSelectedUnit)
				{
					Vec3 userRayBegin = this._userRayBegin;
					Vec3 userRayEnd = this._userRayEnd;
					Vec3 globalPosition = this.SelectedUnit.Entity.GlobalPosition;
					float length = (userRayEnd - userRayBegin).Length;
					float num = (globalPosition - userRayBegin).Length / length;
					Vec3 vecTo = new Vec3(userRayBegin.x + (userRayEnd.x - userRayBegin.x) * num, userRayBegin.y + (userRayEnd.y - userRayBegin.y) * num, this.SelectedUnit.PosBeforeMoving.z + 0.05f, -1f);
					Vec3 pawnAtPosition = MBMath.Lerp(globalPosition, vecTo, 1f, 0.005f);
					this.SelectedUnit.SetPawnAtPosition(pawnAtPosition);
				}
				if (this.DiceRollAnimationRunning)
				{
					if (this.DiceRollAnimationTimer < 1f)
					{
						this.DiceRollAnimationTimer += dt;
					}
					else
					{
						this.DiceRollAnimationRunning = false;
						this.OnAfterDiceRollAnimation();
					}
				}
				if (this.MovesLeftToEndTurn == 0)
				{
					this.EndTurn();
				}
				else
				{
					this.UpdateTurn(dt);
				}
				this.CheckSwitchPlayerTurn();
				return true;
			}
			return false;
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00044A4C File Offset: 0x00042C4C
		public void ForceDice(int value)
		{
			this.LastDice = value;
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00044A55 File Offset: 0x00042C55
		protected PawnBase InitializeUnit(PawnBase pawnToInit)
		{
			pawnToInit.OnArrivedIntermediateGoalPosition = new Action<PawnBase, Vec3, Vec3>(this.OnPawnArrivesGoalPosition);
			pawnToInit.OnArrivedFinalGoalPosition = new Action<PawnBase, Vec3, Vec3>(this.OnPawnArrivesGoalPosition);
			return pawnToInit;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x00044A80 File Offset: 0x00042C80
		protected Move HandlePlayerInput(float dt)
		{
			Move result = new Move(null, null);
			if (this.InputManager.IsHotKeyPressed("BoardGamePawnSelect") && !this._draggingSelectedUnit)
			{
				this.JustStoppedDraggingUnit = false;
				PawnBase hoveredPawnIfAny = this.GetHoveredPawnIfAny();
				TileBase hoveredTileIfAny = this.GetHoveredTileIfAny();
				if (hoveredPawnIfAny != null)
				{
					if (this.PawnSelectFilter.Count == 0 || this.PawnSelectFilter.Contains(hoveredPawnIfAny))
					{
						PawnBase selectedUnit = this.SelectedUnit;
						PawnBase pawnBase = this.SelectPawn(hoveredPawnIfAny);
						if (pawnBase.PlayerOne == (this.PlayerTurn == PlayerTurn.PlayerOne) || !pawnBase.PlayerOne == (this.PlayerTurn == PlayerTurn.PlayerTwo))
						{
							if (this.SelectedUnit != null && this.SelectedUnit == selectedUnit)
							{
								this._deselectUnit = true;
							}
						}
						else if (hoveredTileIfAny == null)
						{
							this.SelectedUnit = null;
						}
					}
				}
				else if (hoveredTileIfAny == null)
				{
					this.SelectedUnit = null;
				}
			}
			else if (this.SelectedUnit != null && this.InputManager.IsHotKeyReleased("BoardGamePawnDeselect"))
			{
				if (this._draggingSelectedUnit)
				{
					this._draggingSelectedUnit = false;
					this.JustStoppedDraggingUnit = true;
				}
				else if (this._deselectUnit)
				{
					PawnBase hoveredPawnIfAny2 = this.GetHoveredPawnIfAny();
					if (hoveredPawnIfAny2 != null && hoveredPawnIfAny2 == this.SelectedUnit)
					{
						this.SelectedUnit = null;
						this._deselectUnit = false;
					}
				}
				if (this._validMoves != null)
				{
					this.SelectedUnit.DisableCollisionBody();
					TileBase hoveredTileIfAny2 = this.GetHoveredTileIfAny();
					if (hoveredTileIfAny2 != null && (hoveredTileIfAny2.PawnOnTile == null || hoveredTileIfAny2.PawnOnTile != this.SelectedUnit))
					{
						foreach (Move move in this._validMoves)
						{
							if (hoveredTileIfAny2.Entity == move.GoalTile.Entity)
							{
								result = move;
							}
						}
					}
					this.SelectedUnit.EnableCollisionBody();
				}
				if (!result.IsValid && this.SelectedUnit != null && this.JustStoppedDraggingUnit)
				{
					this.SelectedUnit.ClearGoalPositions();
					this.SelectedUnit.AddGoalPosition(this.SelectedUnit.PosBeforeMoving);
					this.SelectedUnit.MovePawnToGoalPositions(false, 0.8f, false);
				}
				this._draggingTimer = 0f;
			}
			if (this.SelectedUnit != null && this.InputManager.IsHotKeyDown("BoardGameDragPreview"))
			{
				this._draggingTimer += dt;
				if (this._draggingTimer >= 0.2f)
				{
					this._draggingSelectedUnit = true;
					this._deselectUnit = false;
				}
			}
			return result;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00044D10 File Offset: 0x00042F10
		protected PawnBase GetHoveredPawnIfAny()
		{
			PawnBase pawnBase = null;
			float num;
			GameEntity gameEntity;
			Mission.Current.Scene.RayCastForClosestEntityOrTerrain(this._userRayBegin, this._userRayEnd, out num, out gameEntity, 0.01f, BodyFlags.CommonFocusRayCastExcludeFlags);
			if (gameEntity != null)
			{
				foreach (PawnBase pawnBase2 in this.PlayerOneUnits)
				{
					if (pawnBase2.Entity.Name.Equals(gameEntity.Name))
					{
						pawnBase = pawnBase2;
						break;
					}
				}
				if (pawnBase == null)
				{
					foreach (PawnBase pawnBase3 in this.PlayerTwoUnits)
					{
						if (pawnBase3.Entity.Name.Equals(gameEntity.Name))
						{
							pawnBase = pawnBase3;
							break;
						}
					}
				}
			}
			return pawnBase;
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00044E14 File Offset: 0x00043014
		protected TileBase GetHoveredTileIfAny()
		{
			TileBase result = null;
			float num;
			GameEntity gameEntity;
			Mission.Current.Scene.RayCastForClosestEntityOrTerrain(this._userRayBegin, this._userRayEnd, out num, out gameEntity, 0.01f, BodyFlags.CommonFocusRayCastExcludeFlags);
			if (gameEntity != null)
			{
				for (int i = 0; i < this.TileCount; i++)
				{
					TileBase tileBase = this.Tiles[i];
					if (tileBase.Entity.Name.Equals(gameEntity.Name))
					{
						result = tileBase;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00044E90 File Offset: 0x00043090
		protected void CheckSwitchPlayerTurn()
		{
			if (this.PlayerTurn == PlayerTurn.PlayerOneWaiting || this.PlayerTurn == PlayerTurn.PlayerTwoWaiting)
			{
				bool flag = false;
				using (List<PawnBase>.Enumerator enumerator = this.PlayerOneUnits.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Moving)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					using (List<PawnBase>.Enumerator enumerator = this.PlayerTwoUnits.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.Moving)
							{
								flag = true;
								break;
							}
						}
					}
				}
				if (!flag)
				{
					this.SwitchPlayerTurn();
				}
			}
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00044F50 File Offset: 0x00043150
		protected void OnVictory(string message = "str_boardgame_victory_message")
		{
			this.MissionHandler.PlayerOneWon(message);
		}

		// Token: 0x0600093B RID: 2363 RVA: 0x00044F5E File Offset: 0x0004315E
		protected void OnAfterEndTurn()
		{
			this.ClearValidMoves();
			this.CheckGameEnded();
			this.MovesLeftToEndTurn = (this.InPreMovementStage ? this.UnitsToPlacePerTurnInPreMovementStage : 1);
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x00044F84 File Offset: 0x00043184
		protected void OnDefeat(string message = "str_boardgame_defeat_message")
		{
			this.MissionHandler.PlayerTwoWon(message);
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x00044F92 File Offset: 0x00043192
		protected void OnDraw(string message = "str_boardgame_draw_message")
		{
			this.MissionHandler.GameWasDraw(message);
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x00044FA0 File Offset: 0x000431A0
		private void OnBeforeSelectedUnitChanged(PawnBase oldSelectedUnit, PawnBase newSelectedUnit)
		{
			if (oldSelectedUnit != null)
			{
				oldSelectedUnit.Entity.GetMetaMesh(0).SetFactor1Linear(this.PawnUnselectedFactor);
			}
			if (newSelectedUnit != null)
			{
				newSelectedUnit.Entity.GetMetaMesh(0).SetFactor1Linear(this.PawnSelectedFactor);
			}
			this.ClearValidMoves();
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x00044FDC File Offset: 0x000431DC
		protected void EndTurn()
		{
			this.OnBeforeEndTurn();
			this.SwitchToWaiting();
			this.OnAfterEndTurn();
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x00044FF0 File Offset: 0x000431F0
		protected void ClearValidMoves()
		{
			this.HideAllValidTiles();
			if (this._validMoves != null)
			{
				this._validMoves.Clear();
				this._validMoves = null;
			}
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x00045014 File Offset: 0x00043214
		private void OnAfterSelectedUnitChanged()
		{
			if (this.SelectedUnit != null)
			{
				List<Move> list = this.CalculateValidMoves(this.SelectedUnit);
				if (list != null && list.Count > 0)
				{
					this._validMoves = list;
				}
				if (this.SelectedUnit.PlayerOne || this.MissionHandler.AIOpponent == null)
				{
					this.SelectedUnit.PlayPawnSelectSound();
					this.ShowAllValidTiles();
				}
			}
		}

		// Token: 0x06000942 RID: 2370 RVA: 0x00045074 File Offset: 0x00043274
		private void UpdateTurn(float dt)
		{
			if (this.PlayerTurn == PlayerTurn.PlayerOne || (this.PlayerTurn == PlayerTurn.PlayerTwo && this.AIOpponent == null))
			{
				if (this.InPreMovementStage)
				{
					this.HandlePreMovementStage(dt);
					return;
				}
				if (!this.DiceRollRequired || this.DiceRolled)
				{
					Move move = this.HandlePlayerInput(dt);
					if (move.IsValid)
					{
						this.MovePawnToTile(move.Unit, move.GoalTile, false, true);
						return;
					}
				}
			}
			else if (this.PlayerTurn == PlayerTurn.PlayerTwo && this.AIOpponent != null && !this._waitingAIForfeitResponse)
			{
				if (this.AIOpponent.WantsToForfeit())
				{
					this.OnAIWantsForfeit();
				}
				if (this.DiceRollRequired && !this.DiceRolled)
				{
					this.RollDice();
				}
				this.AIOpponent.UpdateThinkingAboutMove(dt);
				if (this.AIOpponent.CanMakeMove())
				{
					this.SelectedUnit = this.AIOpponent.RecentMoveCalculated.Unit;
					if (this.SelectedUnit != null)
					{
						if (this.InPreMovementStage)
						{
							this.HandlePreMovementStageAI(this.AIOpponent.RecentMoveCalculated);
						}
						else
						{
							TileBase goalTile = this.AIOpponent.RecentMoveCalculated.GoalTile;
							this.MovePawnToTile(this.SelectedUnit, goalTile, false, true);
						}
					}
					else
					{
						MBInformationManager.AddQuickInformation(GameTexts.FindText("str_boardgame_no_available_moves_opponent", null), 0, null, "");
						this.EndTurn();
					}
					this.AIOpponent.ResetThinking();
				}
			}
		}

		// Token: 0x06000943 RID: 2371 RVA: 0x000451D0 File Offset: 0x000433D0
		private bool DoneSettingUpBoard()
		{
			bool result = !this.SettingUpBoard;
			if (this.SettingUpBoard)
			{
				if (this._rotationApplied != this._rotationTarget && this.RotateBoard)
				{
					float value = this._rotationTarget - this._rotationApplied;
					float num = 0.05f;
					float num2 = MathF.Clamp(value, -num, num);
					MatrixFrame globalFrame = this.BoardEntity.GetGlobalFrame();
					globalFrame.rotation.RotateAboutUp(num2);
					this.BoardEntity.SetGlobalFrame(globalFrame);
					this._rotationApplied += num2;
					if (MathF.Abs(this._rotationTarget - this._rotationApplied) <= 1E-05f)
					{
						this._rotationApplied = this._rotationTarget;
						this.UpdateAllPawnsPositions();
						this.UpdateAllTilesPositions();
						return result;
					}
				}
				else
				{
					if (!this._rotationCompleted)
					{
						this._rotationCompleted = true;
						this.OnAfterBoardRotated();
						return result;
					}
					this.SettingUpBoard = false;
					this.OnAfterBoardSetUp();
				}
			}
			return result;
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x000452B4 File Offset: 0x000434B4
		protected void HideAllValidTiles()
		{
			if (this._validMoves != null && this._validMoves.Count > 0)
			{
				foreach (Move move in this._validMoves)
				{
					move.GoalTile.SetVisibility(false);
				}
			}
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00045320 File Offset: 0x00043520
		protected void ShowAllValidTiles()
		{
			if (this._validMoves != null && this._validMoves.Count > 0)
			{
				foreach (Move move in this._validMoves)
				{
					move.GoalTile.SetVisibility(true);
				}
			}
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0004538C File Offset: 0x0004358C
		private void UnfocusAllPawns()
		{
			if (this.PlayerOneUnits != null)
			{
				foreach (PawnBase pawnBase in this.PlayerOneUnits)
				{
					pawnBase.Entity.GetMetaMesh(0).SetFactor1Linear(this.PawnUnselectedFactor);
				}
			}
			if (this.PlayerTwoUnits != null)
			{
				foreach (PawnBase pawnBase2 in this.PlayerTwoUnits)
				{
					pawnBase2.Entity.GetMetaMesh(0).SetFactor1Linear(this.PawnUnselectedFactor);
				}
			}
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00045450 File Offset: 0x00043650
		private bool MovingPawnPresent()
		{
			bool flag = false;
			foreach (PawnBase pawnBase in this.PlayerOneUnits)
			{
				if (pawnBase.Moving || pawnBase.HasAnyGoalPosition)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				foreach (PawnBase pawnBase2 in this.PlayerTwoUnits)
				{
					if (pawnBase2.Moving || pawnBase2.HasAnyGoalPosition)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00045508 File Offset: 0x00043708
		private void SwitchToWaiting()
		{
			if (this.PlayerTurn == PlayerTurn.PlayerOne)
			{
				this.PlayerTurn = PlayerTurn.PlayerOneWaiting;
			}
			else if (this.PlayerTurn == PlayerTurn.PlayerTwo)
			{
				this.PlayerTurn = PlayerTurn.PlayerTwoWaiting;
			}
			this.JustStoppedDraggingUnit = false;
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00045534 File Offset: 0x00043734
		protected void OnAIWantsForfeit()
		{
			if (!this._waitingAIForfeitResponse)
			{
				this._waitingAIForfeitResponse = true;
				InformationManager.ShowInquiry(new InquiryData(GameTexts.FindText("str_boardgame", null).ToString(), GameTexts.FindText("str_boardgame_forfeit_question", null).ToString(), true, true, GameTexts.FindText("str_accept", null).ToString(), GameTexts.FindText("str_reject", null).ToString(), new Action(this.OnAIForfeitAccepted), new Action(this.OnAIForfeitRejected), "", 0f, null, null, null), false, false);
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x000455C4 File Offset: 0x000437C4
		private void UpdateAllPawnsPositions()
		{
			foreach (PawnBase pawnBase in this.PlayerOneUnits)
			{
				pawnBase.UpdatePawnPosition();
			}
			foreach (PawnBase pawnBase2 in this.PlayerTwoUnits)
			{
				pawnBase2.UpdatePawnPosition();
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x00045654 File Offset: 0x00043854
		private void OnAIForfeitAccepted()
		{
			this.MissionHandler.AIForfeitGame();
			this._waitingAIForfeitResponse = false;
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x00045668 File Offset: 0x00043868
		private void OnAIForfeitRejected()
		{
			this._waitingAIForfeitResponse = false;
		}

		// Token: 0x0400035C RID: 860
		public const string StringBoardGame = "str_boardgame";

		// Token: 0x0400035D RID: 861
		public const string StringForfeitQuestion = "str_boardgame_forfeit_question";

		// Token: 0x0400035E RID: 862
		public const string StringMovePiecePlayer = "str_boardgame_move_piece_player";

		// Token: 0x0400035F RID: 863
		public const string StringMovePieceOpponent = "str_boardgame_move_piece_opponent";

		// Token: 0x04000360 RID: 864
		public const string StringCapturePiecePlayer = "str_boardgame_capture_piece_player";

		// Token: 0x04000361 RID: 865
		public const string StringCapturePieceOpponent = "str_boardgame_capture_piece_opponent";

		// Token: 0x04000362 RID: 866
		public const string StringVictoryMessage = "str_boardgame_victory_message";

		// Token: 0x04000363 RID: 867
		public const string StringDefeatMessage = "str_boardgame_defeat_message";

		// Token: 0x04000364 RID: 868
		public const string StringDrawMessage = "str_boardgame_draw_message";

		// Token: 0x04000365 RID: 869
		public const string StringNoAvailableMovesPlayer = "str_boardgame_no_available_moves_player";

		// Token: 0x04000366 RID: 870
		public const string StringNoAvailableMovesOpponent = "str_boardgame_no_available_moves_opponent";

		// Token: 0x04000367 RID: 871
		public const string StringSeegaBarrierByP1DrawMessage = "str_boardgame_seega_barrier_by_player_one_draw_message";

		// Token: 0x04000368 RID: 872
		public const string StringSeegaBarrierByP2DrawMessage = "str_boardgame_seega_barrier_by_player_two_draw_message";

		// Token: 0x04000369 RID: 873
		public const string StringSeegaBarrierByP1VictoryMessage = "str_boardgame_seega_barrier_by_player_one_victory_message";

		// Token: 0x0400036A RID: 874
		public const string StringSeegaBarrierByP2VictoryMessage = "str_boardgame_seega_barrier_by_player_two_victory_message";

		// Token: 0x0400036B RID: 875
		public const string StringSeegaBarrierByP1DefeatMessage = "str_boardgame_seega_barrier_by_player_one_defeat_message";

		// Token: 0x0400036C RID: 876
		public const string StringSeegaBarrierByP2DefeatMessage = "str_boardgame_seega_barrier_by_player_two_defeat_message";

		// Token: 0x0400036D RID: 877
		public const string StringRollDicePlayer = "str_boardgame_roll_dice_player";

		// Token: 0x0400036E RID: 878
		public const string StringRollDiceOpponent = "str_boardgame_roll_dice_opponent";

		// Token: 0x0400036F RID: 879
		protected const int InvalidDice = -1;

		// Token: 0x04000370 RID: 880
		protected const float DelayBeforeMovingAnyPawn = 0.25f;

		// Token: 0x04000371 RID: 881
		protected const float DelayBetweenPawnMovementsBegin = 0.15f;

		// Token: 0x04000372 RID: 882
		private const float DiceRollAnimationDuration = 1f;

		// Token: 0x04000373 RID: 883
		private const float DraggingDuration = 0.2f;

		// Token: 0x04000374 RID: 884
		private const int UnitsToPlacePerTurnInMovementStage = 1;

		// Token: 0x04000375 RID: 885
		protected uint PawnSelectedFactor = uint.MaxValue;

		// Token: 0x04000376 RID: 886
		protected uint PawnUnselectedFactor = 4282203453U;

		// Token: 0x04000377 RID: 887
		protected MissionBoardGameLogic MissionHandler;

		// Token: 0x04000378 RID: 888
		protected GameEntity BoardEntity;

		// Token: 0x04000379 RID: 889
		protected GameEntity DiceBoard;

		// Token: 0x0400037A RID: 890
		protected bool JustStoppedDraggingUnit;

		// Token: 0x0400037B RID: 891
		protected CapturedPawnsPool PlayerOnePool;

		// Token: 0x0400037C RID: 892
		protected bool ReadyToPlay;

		// Token: 0x0400037D RID: 893
		protected CapturedPawnsPool PlayerTwoPool;

		// Token: 0x0400037E RID: 894
		protected bool SettingUpBoard = true;

		// Token: 0x0400037F RID: 895
		protected bool HasToMovePawnsAcross;

		// Token: 0x04000380 RID: 896
		protected float DiceRollAnimationTimer;

		// Token: 0x04000381 RID: 897
		protected int MovesLeftToEndTurn;

		// Token: 0x04000382 RID: 898
		protected bool DiceRollAnimationRunning;

		// Token: 0x04000383 RID: 899
		protected int DiceRollSoundCodeID;

		// Token: 0x04000384 RID: 900
		private List<Move> _validMoves;

		// Token: 0x04000385 RID: 901
		private PawnBase _selectedUnit;

		// Token: 0x04000386 RID: 902
		private Vec3 _userRayBegin;

		// Token: 0x04000387 RID: 903
		private Vec3 _userRayEnd;

		// Token: 0x04000388 RID: 904
		private float _draggingTimer;

		// Token: 0x04000389 RID: 905
		private bool _draggingSelectedUnit;

		// Token: 0x0400038A RID: 906
		private float _rotationApplied;

		// Token: 0x0400038B RID: 907
		private float _rotationTarget;

		// Token: 0x0400038C RID: 908
		private bool _rotationCompleted;

		// Token: 0x0400038D RID: 909
		private bool _deselectUnit;

		// Token: 0x0400038E RID: 910
		private bool _firstTickAfterReady = true;

		// Token: 0x0400038F RID: 911
		private bool _waitingAIForfeitResponse;
	}
}
