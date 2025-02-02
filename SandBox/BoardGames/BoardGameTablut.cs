using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.BoardGames.MissionLogics;
using SandBox.BoardGames.Objects;
using SandBox.BoardGames.Pawns;
using SandBox.BoardGames.Tiles;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade;

namespace SandBox.BoardGames
{
	// Token: 0x020000BF RID: 191
	public class BoardGameTablut : BoardGameBase
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x0004AA3E File Offset: 0x00048C3E
		public override int TileCount
		{
			get
			{
				return 81;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0004AA42 File Offset: 0x00048C42
		protected override bool RotateBoard
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x0004AA45 File Offset: 0x00048C45
		protected override bool PreMovementStagePresent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0004AA48 File Offset: 0x00048C48
		protected override bool DiceRollRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0004AA4B File Offset: 0x00048C4B
		// (set) Token: 0x060009CD RID: 2509 RVA: 0x0004AA53 File Offset: 0x00048C53
		private PawnTablut King { get; set; }

		// Token: 0x060009CE RID: 2510 RVA: 0x0004AA5C File Offset: 0x00048C5C
		public BoardGameTablut(MissionBoardGameLogic mission, PlayerTurn startingPlayer) : base(mission, new TextObject("{=qeKskdiY}Tablut", null), startingPlayer)
		{
			this.SelectedUnit = null;
			this.PawnUnselectedFactor = 4287395960U;
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0004AA83 File Offset: 0x00048C83
		public static bool IsCitadelTile(int tileX, int tileY)
		{
			return tileX == 4 && tileY == 4;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0004AA90 File Offset: 0x00048C90
		public override void InitializeUnits()
		{
			base.PlayerOneUnits.Clear();
			base.PlayerTwoUnits.Clear();
			List<PawnBase> list = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerOneUnits : base.PlayerTwoUnits;
			for (int i = 0; i < 16; i++)
			{
				GameEntity entity = Mission.Current.Scene.FindEntityWithTag("player_one_unit_" + i);
				list.Add(base.InitializeUnit(new PawnTablut(entity, base.PlayerWhoStarted == PlayerTurn.PlayerOne)));
			}
			List<PawnBase> list2 = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerTwoUnits : base.PlayerOneUnits;
			for (int j = 0; j < 9; j++)
			{
				GameEntity entity2 = Mission.Current.Scene.FindEntityWithTag("player_two_unit_" + j);
				list2.Add(base.InitializeUnit(new PawnTablut(entity2, base.PlayerWhoStarted > PlayerTurn.PlayerOne)));
			}
			this.King = (list2[0] as PawnTablut);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0004AB8C File Offset: 0x00048D8C
		public override void InitializeTiles()
		{
			int x;
			IEnumerable<GameEntity> source = from x in this.BoardEntity.GetChildren()
			where x.Tags.Any((string t) => t.Contains("tile_"))
			select x;
			IEnumerable<GameEntity> source2 = from x in this.BoardEntity.GetChildren()
			where x.Tags.Any((string t) => t.Contains("decal_"))
			select x;
			if (base.Tiles == null)
			{
				base.Tiles = new TileBase[this.TileCount];
			}
			int num;
			for (x = 0; x < 9; x = num)
			{
				int y;
				for (y = 0; y < 9; y = num)
				{
					GameEntity entity = source.Single((GameEntity e) => e.HasTag(string.Concat(new object[]
					{
						"tile_",
						x,
						"_",
						y
					})));
					BoardGameDecal firstScriptOfType = source2.Single((GameEntity e) => e.HasTag(string.Concat(new object[]
					{
						"decal_",
						x,
						"_",
						y
					}))).GetFirstScriptOfType<BoardGameDecal>();
					Tile2D tile = new Tile2D(entity, firstScriptOfType, x, y);
					this.SetTile(tile, x, y);
					num = y + 1;
				}
				num = x + 1;
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0004ACD3 File Offset: 0x00048ED3
		public override void InitializeSound()
		{
			PawnBase.PawnMoveSoundCodeID = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/minigame/move_stone");
			PawnBase.PawnSelectSoundCodeID = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/minigame/pick_stone");
			PawnBase.PawnTapSoundCodeID = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/minigame/drop_stone");
			PawnBase.PawnRemoveSoundCodeID = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/minigame/out_stone");
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0004AD11 File Offset: 0x00048F11
		public override void Reset()
		{
			base.Reset();
			if (this._startState.PawnInformation == null)
			{
				this.PreplaceUnits();
				return;
			}
			this.RestoreStartingBoard();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0004AD34 File Offset: 0x00048F34
		public override List<Move> CalculateValidMoves(PawnBase pawn)
		{
			List<Move> list = new List<Move>(16);
			if (pawn.IsPlaced && !pawn.Captured)
			{
				PawnTablut pawnTablut = pawn as PawnTablut;
				int i = pawnTablut.X;
				int j = pawnTablut.Y;
				while (i > 0)
				{
					i--;
					if (!this.AddValidMove(list, pawn, i, j))
					{
						break;
					}
				}
				i = pawnTablut.X;
				while (i < 8)
				{
					i++;
					if (!this.AddValidMove(list, pawn, i, j))
					{
						break;
					}
				}
				i = pawnTablut.X;
				while (j < 8)
				{
					j++;
					if (!this.AddValidMove(list, pawn, i, j))
					{
						break;
					}
				}
				j = pawnTablut.Y;
				while (j > 0)
				{
					j--;
					if (!this.AddValidMove(list, pawn, i, j))
					{
						break;
					}
				}
				j = pawnTablut.Y;
			}
			return list;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0004ADEC File Offset: 0x00048FEC
		public override void SetPawnCaptured(PawnBase pawn, bool fake = false)
		{
			base.SetPawnCaptured(pawn, fake);
			PawnTablut pawnTablut = pawn as PawnTablut;
			this.GetTile(pawnTablut.X, pawnTablut.Y).PawnOnTile = null;
			pawnTablut.X = -1;
			pawnTablut.Y = -1;
			if (!fake)
			{
				base.RemovePawnFromBoard(pawnTablut, 0.6f, false);
			}
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0004AE3E File Offset: 0x0004903E
		protected override void OnAfterBoardSetUp()
		{
			if (this._startState.PawnInformation == null)
			{
				this._startState = this.TakeBoardSnapshot();
			}
			this.ReadyToPlay = true;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0004AE60 File Offset: 0x00049060
		protected override PawnBase SelectPawn(PawnBase pawn)
		{
			if (pawn.PlayerOne == (base.PlayerTurn == PlayerTurn.PlayerOne))
			{
				this.SelectedUnit = pawn;
			}
			return pawn;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0004AE7C File Offset: 0x0004907C
		protected override void MovePawnToTileDelayed(PawnBase pawn, TileBase tile, bool instantMove, bool displayMessage, float delay)
		{
			base.MovePawnToTileDelayed(pawn, tile, instantMove, displayMessage, delay);
			Tile2D tile2D = tile as Tile2D;
			PawnTablut pawnTablut = pawn as PawnTablut;
			if (tile2D.PawnOnTile == null)
			{
				if (displayMessage)
				{
					if (base.PlayerTurn == PlayerTurn.PlayerOne)
					{
						InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_boardgame_move_piece_player", null).ToString()));
					}
					else
					{
						InformationManager.DisplayMessage(new InformationMessage(GameTexts.FindText("str_boardgame_move_piece_opponent", null).ToString()));
					}
				}
				Vec3 globalPosition = pawnTablut.Entity.GlobalPosition;
				Vec3 globalPosition2 = tile2D.Entity.GlobalPosition;
				if (pawnTablut.X != -1 && pawnTablut.Y != -1)
				{
					this.GetTile(pawnTablut.X, pawnTablut.Y).PawnOnTile = null;
				}
				pawnTablut.MovingToDifferentTile = (pawnTablut.X != tile2D.X || pawnTablut.Y != tile2D.Y);
				pawnTablut.X = tile2D.X;
				pawnTablut.Y = tile2D.Y;
				tile2D.PawnOnTile = pawnTablut;
				if (this.SettingUpBoard && globalPosition2.z > globalPosition.z)
				{
					Vec3 goal = globalPosition;
					goal.z += 2f * (globalPosition2.z - globalPosition.z);
					pawnTablut.AddGoalPosition(goal);
					pawnTablut.MovePawnToGoalPositionsDelayed(instantMove, 0.5f, this.JustStoppedDraggingUnit, delay);
				}
				pawnTablut.AddGoalPosition(globalPosition2);
				pawnTablut.MovePawnToGoalPositionsDelayed(instantMove, 0.5f, this.JustStoppedDraggingUnit, delay);
				if (instantMove)
				{
					this.CheckIfPawnCaptures(this.SelectedUnit as PawnTablut, false);
				}
				if (pawnTablut == this.SelectedUnit && instantMove)
				{
					this.SelectedUnit = null;
				}
			}
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0004B014 File Offset: 0x00049214
		protected override void SwitchPlayerTurn()
		{
			if ((base.PlayerTurn == PlayerTurn.PlayerOneWaiting || base.PlayerTurn == PlayerTurn.PlayerTwoWaiting) && this.SelectedUnit != null)
			{
				this.CheckIfPawnCaptures(this.SelectedUnit as PawnTablut, false);
			}
			this.SelectedUnit = null;
			if (base.PlayerTurn == PlayerTurn.PlayerOneWaiting)
			{
				base.PlayerTurn = PlayerTurn.PlayerTwo;
			}
			else if (base.PlayerTurn == PlayerTurn.PlayerTwoWaiting)
			{
				base.PlayerTurn = PlayerTurn.PlayerOne;
			}
			this.CheckGameEnded();
			base.SwitchPlayerTurn();
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0004B084 File Offset: 0x00049284
		protected override bool CheckGameEnded()
		{
			BoardGameTablut.State state = this.CheckGameState();
			bool result = true;
			switch (state)
			{
			case BoardGameTablut.State.InProgress:
				result = false;
				break;
			case BoardGameTablut.State.PlayerWon:
				base.OnVictory("str_boardgame_victory_message");
				this.ReadyToPlay = false;
				break;
			case BoardGameTablut.State.AIWon:
				base.OnDefeat("str_boardgame_defeat_message");
				this.ReadyToPlay = false;
				break;
			}
			return result;
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0004B0E0 File Offset: 0x000492E0
		public bool AIMakeMove(Move move)
		{
			Tile2D tile2D = move.GoalTile as Tile2D;
			PawnTablut pawnTablut = move.Unit as PawnTablut;
			if (tile2D.PawnOnTile == null)
			{
				if (pawnTablut.X != -1 && pawnTablut.Y != -1)
				{
					this.GetTile(pawnTablut.X, pawnTablut.Y).PawnOnTile = null;
				}
				pawnTablut.X = tile2D.X;
				pawnTablut.Y = tile2D.Y;
				tile2D.PawnOnTile = pawnTablut;
				this.CheckIfPawnCaptures(pawnTablut, true);
				return true;
			}
			return false;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0004B164 File Offset: 0x00049364
		public bool HasAvailableMoves(PawnTablut pawn)
		{
			bool result = false;
			if (pawn.IsPlaced && !pawn.Captured)
			{
				int x = pawn.X;
				int y = pawn.Y;
				result = ((x > 0 && this.GetTile(x - 1, y).PawnOnTile == null && !BoardGameTablut.IsCitadelTile(x - 1, y)) || (x < 8 && this.GetTile(x + 1, y).PawnOnTile == null && !BoardGameTablut.IsCitadelTile(x + 1, y)) || (y > 0 && this.GetTile(x, y - 1).PawnOnTile == null && !BoardGameTablut.IsCitadelTile(x, y - 1)) || (y < 8 && this.GetTile(x, y + 1).PawnOnTile == null && !BoardGameTablut.IsCitadelTile(x, y + 1)));
			}
			return result;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0004B220 File Offset: 0x00049420
		public Move GetRandomAvailableMove(PawnTablut pawn)
		{
			List<Move> list = this.CalculateValidMoves(pawn);
			return list[MBRandom.RandomInt(list.Count)];
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0004B23C File Offset: 0x0004943C
		public BoardGameTablut.BoardInformation TakeBoardSnapshot()
		{
			List<PawnBase> list = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerOneUnits : base.PlayerTwoUnits;
			List<PawnBase> list2 = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerTwoUnits : base.PlayerOneUnits;
			BoardGameTablut.PawnInformation[] array = new BoardGameTablut.PawnInformation[25];
			for (int i = 0; i < 25; i++)
			{
				PawnTablut pawnTablut;
				if (i < 16)
				{
					pawnTablut = (list[i] as PawnTablut);
				}
				else
				{
					pawnTablut = (list2[i - 16] as PawnTablut);
				}
				BoardGameTablut.PawnInformation pawnInformation;
				pawnInformation.X = pawnTablut.X;
				pawnInformation.Y = pawnTablut.Y;
				pawnInformation.IsCaptured = pawnTablut.Captured;
				array[i] = pawnInformation;
			}
			PlayerTurn playerTurn = base.PlayerTurn;
			return new BoardGameTablut.BoardInformation(ref array, playerTurn);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0004B2FC File Offset: 0x000494FC
		public void UndoMove(ref BoardGameTablut.BoardInformation board)
		{
			for (int i = 0; i < this.TileCount; i++)
			{
				base.Tiles[i].PawnOnTile = null;
			}
			List<PawnBase> list = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerOneUnits : base.PlayerTwoUnits;
			List<PawnBase> list2 = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerTwoUnits : base.PlayerOneUnits;
			for (int j = 0; j < 25; j++)
			{
				BoardGameTablut.PawnInformation pawnInformation = board.PawnInformation[j];
				PawnTablut pawnTablut;
				if (j < 16)
				{
					pawnTablut = (list[j] as PawnTablut);
				}
				else
				{
					pawnTablut = (list2[j - 16] as PawnTablut);
				}
				pawnTablut.X = pawnInformation.X;
				pawnTablut.Y = pawnInformation.Y;
				pawnTablut.Captured = pawnInformation.IsCaptured;
				if (pawnTablut.IsPlaced)
				{
					this.GetTile(pawnTablut.X, pawnTablut.Y).PawnOnTile = pawnTablut;
				}
			}
			base.PlayerTurn = board.Turn;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0004B3F8 File Offset: 0x000495F8
		public BoardGameTablut.State CheckGameState()
		{
			BoardGameTablut.State result;
			if (!base.AIOpponent.AbortRequested)
			{
				result = BoardGameTablut.State.InProgress;
				if (base.PlayerTurn == PlayerTurn.PlayerOne || base.PlayerTurn == PlayerTurn.PlayerTwo)
				{
					bool flag = base.PlayerWhoStarted == PlayerTurn.PlayerOne;
					if (this.King.Captured)
					{
						result = (flag ? BoardGameTablut.State.PlayerWon : BoardGameTablut.State.AIWon);
					}
					else if (this.King.X == 0 || this.King.X == 8 || this.King.Y == 0 || this.King.Y == 8)
					{
						result = (flag ? BoardGameTablut.State.AIWon : BoardGameTablut.State.PlayerWon);
					}
					else
					{
						bool flag2 = false;
						bool flag3 = base.PlayerTurn == PlayerTurn.PlayerOne;
						List<PawnBase> list = flag3 ? base.PlayerOneUnits : base.PlayerTwoUnits;
						int count = list.Count;
						for (int i = 0; i < count; i++)
						{
							PawnBase pawnBase = list[i];
							if (pawnBase.IsPlaced && !pawnBase.Captured && this.HasAvailableMoves(pawnBase as PawnTablut))
							{
								flag2 = true;
								break;
							}
						}
						if (!flag2)
						{
							result = (flag3 ? BoardGameTablut.State.AIWon : BoardGameTablut.State.PlayerWon);
						}
					}
				}
			}
			else
			{
				result = BoardGameTablut.State.Aborted;
			}
			return result;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0004B50D File Offset: 0x0004970D
		private void SetTile(TileBase tile, int x, int y)
		{
			base.Tiles[y * 9 + x] = tile;
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0004B51D File Offset: 0x0004971D
		private TileBase GetTile(int x, int y)
		{
			return base.Tiles[y * 9 + x];
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0004B52C File Offset: 0x0004972C
		private void PreplaceUnits()
		{
			int[] array = new int[]
			{
				3,
				0,
				4,
				0,
				5,
				0,
				4,
				1,
				0,
				3,
				0,
				4,
				0,
				5,
				1,
				4,
				8,
				3,
				8,
				4,
				8,
				5,
				7,
				4,
				3,
				8,
				4,
				8,
				5,
				8,
				4,
				7
			};
			int[] array2 = new int[]
			{
				4,
				4,
				4,
				3,
				4,
				2,
				5,
				4,
				6,
				4,
				3,
				4,
				2,
				4,
				4,
				5,
				4,
				6
			};
			List<PawnBase> list = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerOneUnits : base.PlayerTwoUnits;
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				int x = array[i * 2];
				int y = array[i * 2 + 1];
				this.MovePawnToTileDelayed(list[i], this.GetTile(x, y), false, false, 0.15f * (float)(i + 1) + 0.25f);
			}
			List<PawnBase> list2 = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerTwoUnits : base.PlayerOneUnits;
			int count2 = list2.Count;
			for (int j = 0; j < count2; j++)
			{
				int x2 = array2[j * 2];
				int y2 = array2[j * 2 + 1];
				this.MovePawnToTileDelayed(list2[j], this.GetTile(x2, y2), false, false, 0.15f * (float)(j + 1) + 0.25f);
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0004B63C File Offset: 0x0004983C
		private void RestoreStartingBoard()
		{
			List<PawnBase> list = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerOneUnits : base.PlayerTwoUnits;
			List<PawnBase> list2 = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerTwoUnits : base.PlayerOneUnits;
			for (int i = 0; i < 25; i++)
			{
				PawnBase pawnBase;
				if (i < 16)
				{
					pawnBase = list[i];
				}
				else
				{
					pawnBase = list2[i - 16];
				}
				BoardGameTablut.PawnInformation pawnInformation = this._startState.PawnInformation[i];
				TileBase tile = this.GetTile(pawnInformation.X, pawnInformation.Y);
				pawnBase.Reset();
				this.MovePawnToTile(pawnBase, tile, false, false);
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0004B6D8 File Offset: 0x000498D8
		private bool AddValidMove(List<Move> moves, PawnBase pawn, int x, int y)
		{
			bool result = false;
			TileBase tile = this.GetTile(x, y);
			if (tile.PawnOnTile == null && !BoardGameTablut.IsCitadelTile(x, y))
			{
				Move item;
				item.Unit = pawn;
				item.GoalTile = tile;
				moves.Add(item);
				result = true;
			}
			return result;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0004B720 File Offset: 0x00049920
		private void CheckIfPawnCapturedEnemyPawn(PawnTablut pawn, bool fake, TileBase victimTile, Tile2D helperTile)
		{
			PawnBase pawnOnTile = victimTile.PawnOnTile;
			if (pawnOnTile != null && pawnOnTile.PlayerOne != pawn.PlayerOne)
			{
				PawnBase pawnOnTile2 = helperTile.PawnOnTile;
				if (pawnOnTile2 != null)
				{
					if (pawnOnTile2.PlayerOne == pawn.PlayerOne)
					{
						this.SetPawnCaptured(pawnOnTile, fake);
						return;
					}
				}
				else if (BoardGameTablut.IsCitadelTile(helperTile.X, helperTile.Y))
				{
					this.SetPawnCaptured(pawnOnTile, fake);
				}
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0004B78C File Offset: 0x0004998C
		private void CheckIfPawnCaptures(PawnTablut pawn, bool fake = false)
		{
			int x = pawn.X;
			int y = pawn.Y;
			if (x > 1)
			{
				this.CheckIfPawnCapturedEnemyPawn(pawn, fake, this.GetTile(x - 1, y), this.GetTile(x - 2, y) as Tile2D);
			}
			if (x < 7)
			{
				this.CheckIfPawnCapturedEnemyPawn(pawn, fake, this.GetTile(x + 1, y), this.GetTile(x + 2, y) as Tile2D);
			}
			if (y > 1)
			{
				this.CheckIfPawnCapturedEnemyPawn(pawn, fake, this.GetTile(x, y - 1), this.GetTile(x, y - 2) as Tile2D);
			}
			if (y < 7)
			{
				this.CheckIfPawnCapturedEnemyPawn(pawn, fake, this.GetTile(x, y + 1), this.GetTile(x, y + 2) as Tile2D);
			}
		}

		// Token: 0x040003AF RID: 943
		public const int BoardWidth = 9;

		// Token: 0x040003B0 RID: 944
		public const int BoardHeight = 9;

		// Token: 0x040003B1 RID: 945
		public const int AttackerPawnCount = 16;

		// Token: 0x040003B2 RID: 946
		public const int DefenderPawnCount = 9;

		// Token: 0x040003B3 RID: 947
		private BoardGameTablut.BoardInformation _startState;

		// Token: 0x020001BD RID: 445
		public struct PawnInformation
		{
			// Token: 0x06001116 RID: 4374 RVA: 0x00064AC1 File Offset: 0x00062CC1
			public PawnInformation(int x, int y, bool captured)
			{
				this.X = x;
				this.Y = y;
				this.IsCaptured = captured;
			}

			// Token: 0x04000768 RID: 1896
			public int X;

			// Token: 0x04000769 RID: 1897
			public int Y;

			// Token: 0x0400076A RID: 1898
			public bool IsCaptured;
		}

		// Token: 0x020001BE RID: 446
		public struct BoardInformation
		{
			// Token: 0x06001117 RID: 4375 RVA: 0x00064AD8 File Offset: 0x00062CD8
			public BoardInformation(ref BoardGameTablut.PawnInformation[] pawns, PlayerTurn turn)
			{
				this.PawnInformation = pawns;
				this.Turn = turn;
			}

			// Token: 0x0400076B RID: 1899
			public readonly BoardGameTablut.PawnInformation[] PawnInformation;

			// Token: 0x0400076C RID: 1900
			public readonly PlayerTurn Turn;
		}

		// Token: 0x020001BF RID: 447
		public enum State
		{
			// Token: 0x0400076E RID: 1902
			InProgress,
			// Token: 0x0400076F RID: 1903
			Aborted,
			// Token: 0x04000770 RID: 1904
			PlayerWon,
			// Token: 0x04000771 RID: 1905
			AIWon
		}
	}
}
