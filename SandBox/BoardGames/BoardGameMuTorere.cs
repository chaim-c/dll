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
	// Token: 0x020000BC RID: 188
	public class BoardGameMuTorere : BoardGameBase
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x000471AA File Offset: 0x000453AA
		public override int TileCount
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x000471AE File Offset: 0x000453AE
		protected override bool RotateBoard
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000471B1 File Offset: 0x000453B1
		protected override bool PreMovementStagePresent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x000471B4 File Offset: 0x000453B4
		protected override bool DiceRollRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000471B7 File Offset: 0x000453B7
		public BoardGameMuTorere(MissionBoardGameLogic mission, PlayerTurn startingPlayer) : base(mission, new TextObject("{=5siAbi69}Mu Torere", null), startingPlayer)
		{
			this.PawnUnselectedFactor = 4288711820U;
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x000471D8 File Offset: 0x000453D8
		public override void InitializeUnits()
		{
			base.PlayerOneUnits.Clear();
			base.PlayerTwoUnits.Clear();
			List<PawnBase> list = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerOneUnits : base.PlayerTwoUnits;
			for (int i = 0; i < 4; i++)
			{
				GameEntity entity = Mission.Current.Scene.FindEntityWithTag("player_one_unit_" + i);
				list.Add(base.InitializeUnit(new PawnMuTorere(entity, base.PlayerWhoStarted == PlayerTurn.PlayerOne)));
			}
			List<PawnBase> list2 = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerTwoUnits : base.PlayerOneUnits;
			for (int j = 0; j < 4; j++)
			{
				GameEntity entity2 = Mission.Current.Scene.FindEntityWithTag("player_two_unit_" + j);
				list2.Add(base.InitializeUnit(new PawnMuTorere(entity2, base.PlayerWhoStarted > PlayerTurn.PlayerOne)));
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x000472C0 File Offset: 0x000454C0
		public override void InitializeTiles()
		{
			if (base.Tiles == null)
			{
				base.Tiles = new TileBase[this.TileCount];
			}
			int x;
			IEnumerable<GameEntity> source = from x in this.BoardEntity.GetChildren()
			where x.Tags.Any((string t) => t.Contains("tile_"))
			select x;
			IEnumerable<GameEntity> source2 = from x in this.BoardEntity.GetChildren()
			where x.Tags.Any((string t) => t.Contains("decal_"))
			select x;
			int num;
			for (x = 0; x < this.TileCount; x = num)
			{
				GameEntity entity = source.Single((GameEntity e) => e.HasTag("tile_" + x));
				BoardGameDecal firstScriptOfType = source2.Single((GameEntity e) => e.HasTag("decal_" + x)).GetFirstScriptOfType<BoardGameDecal>();
				num = x;
				int xLeft;
				int xRight;
				if (num != 0)
				{
					if (num != 1)
					{
						if (num != 8)
						{
							xLeft = x - 1;
							xRight = x + 1;
						}
						else
						{
							xLeft = 7;
							xRight = 1;
						}
					}
					else
					{
						xLeft = 8;
						xRight = 2;
					}
				}
				else
				{
					xRight = (xLeft = -1);
				}
				base.Tiles[x] = new TileMuTorere(entity, firstScriptOfType, x, xLeft, xRight);
				num = x + 1;
			}
		}

		// Token: 0x06000974 RID: 2420 RVA: 0x0004740A File Offset: 0x0004560A
		public override void InitializeCapturedUnitsZones()
		{
		}

		// Token: 0x06000975 RID: 2421 RVA: 0x0004740C File Offset: 0x0004560C
		public override void InitializeSound()
		{
			PawnBase.PawnMoveSoundCodeID = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/minigame/move_stone");
			PawnBase.PawnSelectSoundCodeID = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/minigame/pick_stone");
			PawnBase.PawnTapSoundCodeID = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/minigame/drop_wood");
			PawnBase.PawnRemoveSoundCodeID = SoundEvent.GetEventIdFromString("event:/mission/movement/foley/minigame/out_stone");
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x0004744A File Offset: 0x0004564A
		public override void Reset()
		{
			base.Reset();
			this.PreplaceUnits();
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x00047458 File Offset: 0x00045658
		public override List<Move> CalculateValidMoves(PawnBase pawn)
		{
			List<Move> list = new List<Move>();
			PawnMuTorere pawnMuTorere = pawn as PawnMuTorere;
			if (pawnMuTorere != null)
			{
				TileMuTorere tileMuTorere = this.FindAvailableTile() as TileMuTorere;
				if (pawnMuTorere.X == 0)
				{
					Move item;
					item.Unit = pawn;
					item.GoalTile = tileMuTorere;
					list.Add(item);
				}
				else if (tileMuTorere.X != 0)
				{
					if (pawnMuTorere.X == tileMuTorere.XLeftTile || pawnMuTorere.X == tileMuTorere.XRightTile)
					{
						Move item2;
						item2.Unit = pawn;
						item2.GoalTile = tileMuTorere;
						list.Add(item2);
					}
				}
				else
				{
					TileMuTorere tileMuTorere2 = this.FindTileByCoordinate(pawnMuTorere.X);
					PawnBase pawnOnTile = base.Tiles[tileMuTorere2.XLeftTile].PawnOnTile;
					PawnBase pawnOnTile2 = base.Tiles[tileMuTorere2.XRightTile].PawnOnTile;
					if (pawnOnTile.PlayerOne != pawnMuTorere.PlayerOne || pawnOnTile2.PlayerOne != pawnMuTorere.PlayerOne)
					{
						Move item3;
						item3.Unit = pawn;
						item3.GoalTile = tileMuTorere;
						list.Add(item3);
					}
				}
			}
			return list;
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00047554 File Offset: 0x00045754
		protected override PawnBase SelectPawn(PawnBase pawn)
		{
			if (base.PlayerTurn == PlayerTurn.PlayerOne)
			{
				if (pawn.PlayerOne)
				{
					this.SelectedUnit = pawn;
				}
			}
			else if (base.AIOpponent == null && !pawn.PlayerOne)
			{
				this.SelectedUnit = pawn;
			}
			return pawn;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x00047588 File Offset: 0x00045788
		protected override void MovePawnToTileDelayed(PawnBase pawn, TileBase tile, bool instantMove, bool displayMessage, float delay)
		{
			base.MovePawnToTileDelayed(pawn, tile, instantMove, displayMessage, delay);
			TileMuTorere tileMuTorere = tile as TileMuTorere;
			PawnMuTorere pawnMuTorere = pawn as PawnMuTorere;
			if (tileMuTorere.PawnOnTile == null && pawnMuTorere != null)
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
				if (pawnMuTorere.X != -1)
				{
					base.Tiles[pawnMuTorere.X].PawnOnTile = null;
				}
				tileMuTorere.PawnOnTile = pawnMuTorere;
				pawnMuTorere.MovingToDifferentTile = (pawnMuTorere.X != tileMuTorere.X);
				pawnMuTorere.X = tileMuTorere.X;
				Vec3 globalPosition = tileMuTorere.Entity.GlobalPosition;
				pawnMuTorere.AddGoalPosition(globalPosition);
				pawnMuTorere.MovePawnToGoalPositionsDelayed(instantMove, 0.6f, this.JustStoppedDraggingUnit, delay);
				if (pawnMuTorere == this.SelectedUnit)
				{
					this.SelectedUnit = null;
				}
			}
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00047680 File Offset: 0x00045880
		protected override void SwitchPlayerTurn()
		{
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

		// Token: 0x0600097B RID: 2427 RVA: 0x000476B4 File Offset: 0x000458B4
		protected override bool CheckGameEnded()
		{
			bool result = false;
			List<List<Move>> list = this.CalculateAllValidMoves((base.PlayerTurn == PlayerTurn.PlayerOne) ? BoardGameSide.Player : BoardGameSide.AI);
			if (base.GetTotalMovesAvailable(ref list) <= 0)
			{
				if (base.PlayerTurn == PlayerTurn.PlayerOne)
				{
					base.OnDefeat("str_boardgame_defeat_message");
					this.ReadyToPlay = false;
					result = true;
				}
				else if (base.PlayerTurn == PlayerTurn.PlayerTwo)
				{
					base.OnVictory("str_boardgame_victory_message");
					this.ReadyToPlay = false;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0004771D File Offset: 0x0004591D
		protected override void OnAfterBoardSetUp()
		{
			this.ReadyToPlay = true;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00047728 File Offset: 0x00045928
		public TileMuTorere FindTileByCoordinate(int x)
		{
			TileMuTorere result = null;
			for (int i = 0; i < this.TileCount; i++)
			{
				TileMuTorere tileMuTorere = base.Tiles[i] as TileMuTorere;
				if (tileMuTorere.X == x)
				{
					result = tileMuTorere;
				}
			}
			return result;
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00047764 File Offset: 0x00045964
		public BoardGameMuTorere.BoardInformation TakePawnsSnapshot()
		{
			BoardGameMuTorere.PawnInformation[] array = new BoardGameMuTorere.PawnInformation[base.PlayerOneUnits.Count + base.PlayerTwoUnits.Count];
			TileBaseInformation[] array2 = new TileBaseInformation[this.TileCount];
			int num = 0;
			foreach (PawnBase pawnBase in ((base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerOneUnits : base.PlayerTwoUnits))
			{
				PawnMuTorere pawnMuTorere = (PawnMuTorere)pawnBase;
				BoardGameMuTorere.PawnInformation pawnInformation = new BoardGameMuTorere.PawnInformation(pawnMuTorere.X);
				array[num++] = pawnInformation;
			}
			foreach (PawnBase pawnBase2 in ((base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerTwoUnits : base.PlayerOneUnits))
			{
				PawnMuTorere pawnMuTorere2 = (PawnMuTorere)pawnBase2;
				BoardGameMuTorere.PawnInformation pawnInformation2 = new BoardGameMuTorere.PawnInformation(pawnMuTorere2.X);
				array[num++] = pawnInformation2;
			}
			for (int i = 0; i < this.TileCount; i++)
			{
				array2[i] = new TileBaseInformation(ref base.Tiles[i].PawnOnTile);
			}
			return new BoardGameMuTorere.BoardInformation(ref array, ref array2);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000478B4 File Offset: 0x00045AB4
		public void UndoMove(ref BoardGameMuTorere.BoardInformation board)
		{
			int num = 0;
			foreach (PawnBase pawnBase in ((base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerOneUnits : base.PlayerTwoUnits))
			{
				((PawnMuTorere)pawnBase).X = board.PawnInformation[num++].X;
			}
			foreach (PawnBase pawnBase2 in ((base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerTwoUnits : base.PlayerOneUnits))
			{
				((PawnMuTorere)pawnBase2).X = board.PawnInformation[num++].X;
			}
			for (int i = 0; i < this.TileCount; i++)
			{
				base.Tiles[i].PawnOnTile = board.TileInformation[i].PawnOnTile;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000479C8 File Offset: 0x00045BC8
		public void AIMakeMove(Move move)
		{
			TileMuTorere tileMuTorere = move.GoalTile as TileMuTorere;
			PawnMuTorere pawnMuTorere = move.Unit as PawnMuTorere;
			base.Tiles[pawnMuTorere.X].PawnOnTile = null;
			tileMuTorere.PawnOnTile = pawnMuTorere;
			pawnMuTorere.X = tileMuTorere.X;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00047A14 File Offset: 0x00045C14
		public TileBase FindAvailableTile()
		{
			foreach (TileBase tileBase in base.Tiles)
			{
				if (tileBase.PawnOnTile == null)
				{
					return tileBase;
				}
			}
			return null;
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00047A48 File Offset: 0x00045C48
		private void PreplaceUnits()
		{
			List<PawnBase> list = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerOneUnits : base.PlayerTwoUnits;
			List<PawnBase> list2 = (base.PlayerWhoStarted == PlayerTurn.PlayerOne) ? base.PlayerTwoUnits : base.PlayerOneUnits;
			for (int i = 0; i < 4; i++)
			{
				this.MovePawnToTileDelayed(list[i], base.Tiles[i + 1], false, false, 0.15f * (float)(i + 1) + 0.25f);
				this.MovePawnToTileDelayed(list2[i], base.Tiles[8 - i], false, false, 0.15f * (float)(i + 1) + 0.5f);
			}
		}

		// Token: 0x040003A0 RID: 928
		public const int WhitePawnCount = 4;

		// Token: 0x040003A1 RID: 929
		public const int BlackPawnCount = 4;

		// Token: 0x020001AF RID: 431
		public struct BoardInformation
		{
			// Token: 0x060010F3 RID: 4339 RVA: 0x00064731 File Offset: 0x00062931
			public BoardInformation(ref BoardGameMuTorere.PawnInformation[] pawns, ref TileBaseInformation[] tiles)
			{
				this.PawnInformation = pawns;
				this.TileInformation = tiles;
			}

			// Token: 0x0400073C RID: 1852
			public readonly BoardGameMuTorere.PawnInformation[] PawnInformation;

			// Token: 0x0400073D RID: 1853
			public readonly TileBaseInformation[] TileInformation;
		}

		// Token: 0x020001B0 RID: 432
		public struct PawnInformation
		{
			// Token: 0x060010F4 RID: 4340 RVA: 0x00064743 File Offset: 0x00062943
			public PawnInformation(int x)
			{
				this.X = x;
			}

			// Token: 0x0400073E RID: 1854
			public readonly int X;
		}
	}
}
