using System;
using System.Collections.Generic;
using Helpers;
using SandBox.BoardGames.MissionLogics;
using SandBox.BoardGames.Pawns;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SandBox.BoardGames.AI
{
	// Token: 0x020000D5 RID: 213
	public class BoardGameAISeega : BoardGameAIBase
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
		public BoardGameAISeega(BoardGameHelper.AIDifficulty difficulty, MissionBoardGameLogic boardGameHandler) : base(difficulty, boardGameHandler)
		{
			this._board = (base.BoardGameHandler.Board as BoardGameSeega);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0004E728 File Offset: 0x0004C928
		protected override void InitializeDifficulty()
		{
			switch (base.Difficulty)
			{
			case BoardGameHelper.AIDifficulty.Easy:
				this.MaxDepth = 2;
				return;
			case BoardGameHelper.AIDifficulty.Normal:
				this.MaxDepth = 3;
				return;
			case BoardGameHelper.AIDifficulty.Hard:
				this.MaxDepth = 4;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0004E768 File Offset: 0x0004C968
		public override Move CalculateMovementStageMove()
		{
			Move result = Move.Invalid;
			if (this._board.IsReady)
			{
				List<List<Move>> list = this._board.CalculateAllValidMoves(BoardGameSide.AI);
				if (!this._board.HasMovesAvailable(ref list))
				{
					IEnumerable<KeyValuePair<PawnBase, int>> blockingPawns = this._board.GetBlockingPawns(false);
					InformationManager.DisplayMessage(new InformationMessage(new TextObject("{=1bzdDYoO}All AI pawns blocked. Removing one of the player's pawns to make a move", null).ToString()));
					PawnBase key = blockingPawns.MaxBy((KeyValuePair<PawnBase, int> x) => x.Value).Key;
					this._board.SetPawnCaptured(key, false);
					list = this._board.CalculateAllValidMoves(BoardGameSide.AI);
				}
				BoardGameSeega.BoardInformation boardInformation = this._board.TakeBoardSnapshot();
				if (this._board.HasMovesAvailable(ref list))
				{
					int num = int.MinValue;
					foreach (List<Move> list2 in list)
					{
						if (base.AbortRequested)
						{
							break;
						}
						foreach (Move move in list2)
						{
							if (base.AbortRequested)
							{
								break;
							}
							this._board.AIMakeMove(move);
							int num2 = -this.NegaMax(this.MaxDepth, -1, -2147483647, int.MaxValue);
							this._board.UndoMove(ref boardInformation);
							if (num2 > num)
							{
								result = move;
								num = num2;
							}
						}
					}
				}
			}
			if (!base.AbortRequested)
			{
				bool isValid = result.IsValid;
			}
			return result;
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0004E91C File Offset: 0x0004CB1C
		public override bool WantsToForfeit()
		{
			if (!this.MayForfeit)
			{
				return false;
			}
			int playerOneUnitsAlive = this._board.GetPlayerOneUnitsAlive();
			int playerTwoUnitsAlive = this._board.GetPlayerTwoUnitsAlive();
			int num = (base.Difficulty == BoardGameHelper.AIDifficulty.Hard) ? 2 : 1;
			if (playerTwoUnitsAlive <= 7 && playerOneUnitsAlive >= playerTwoUnitsAlive + (num + playerTwoUnitsAlive / 2))
			{
				this.MayForfeit = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0004E974 File Offset: 0x0004CB74
		public override Move CalculatePreMovementStageMove()
		{
			Move invalid = Move.Invalid;
			foreach (PawnBase pawnBase in this._board.PlayerTwoUnits)
			{
				PawnSeega pawnSeega = (PawnSeega)pawnBase;
				if (!pawnSeega.IsPlaced && !pawnSeega.Moving)
				{
					while (!invalid.IsValid)
					{
						if (base.AbortRequested)
						{
							break;
						}
						int x = MBRandom.RandomInt(0, 5);
						int y = MBRandom.RandomInt(0, 5);
						if (this._board.GetTile(x, y).PawnOnTile == null && !this._board.GetTile(x, y).Entity.HasTag("obstructed_at_start"))
						{
							invalid.Unit = pawnSeega;
							invalid.GoalTile = this._board.GetTile(x, y);
						}
					}
					break;
				}
			}
			return invalid;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0004EA64 File Offset: 0x0004CC64
		private int NegaMax(int depth, int color, int alpha, int beta)
		{
			int num = int.MinValue;
			if (depth == 0)
			{
				return color * this.Evaluation();
			}
			foreach (PawnBase pawnBase in ((color == 1) ? this._board.PlayerTwoUnits : this._board.PlayerOneUnits))
			{
				((PawnSeega)pawnBase).UpdateMoveBackAvailable();
			}
			List<List<Move>> list = this._board.CalculateAllValidMoves((color == 1) ? BoardGameSide.AI : BoardGameSide.Player);
			if (!this._board.HasMovesAvailable(ref list))
			{
				return color * this.Evaluation();
			}
			BoardGameSeega.BoardInformation boardInformation = this._board.TakeBoardSnapshot();
			foreach (List<Move> list2 in list)
			{
				if (list2 != null)
				{
					foreach (Move move in list2)
					{
						this._board.AIMakeMove(move);
						num = MathF.Max(-this.NegaMax(depth - 1, -color, -beta, -alpha), num);
						alpha = MathF.Max(alpha, num);
						this._board.UndoMove(ref boardInformation);
						if (alpha >= beta && color == 1)
						{
							return alpha;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0004EBE4 File Offset: 0x0004CDE4
		private int Evaluation()
		{
			float num = MBRandom.RandomFloat;
			switch (base.Difficulty)
			{
			case BoardGameHelper.AIDifficulty.Easy:
				num = num * 0.7f + 0.5f;
				break;
			case BoardGameHelper.AIDifficulty.Normal:
				num = num * 0.5f + 0.65f;
				break;
			case BoardGameHelper.AIDifficulty.Hard:
				num = num * 0.35f + 0.75f;
				break;
			}
			return (int)((float)(20 * (this._board.GetPlayerTwoUnitsAlive() - this._board.GetPlayerOneUnitsAlive()) + (this.GetPlacementScore(false) - this.GetPlacementScore(true)) + 2 * (this.GetSurroundedScore(false) - this.GetSurroundedScore(true))) * num);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0004EC80 File Offset: 0x0004CE80
		private int GetPlacementScore(bool player)
		{
			int num = 0;
			foreach (PawnBase pawnBase in (player ? this._board.PlayerOneUnits : this._board.PlayerTwoUnits))
			{
				PawnSeega pawnSeega = (PawnSeega)pawnBase;
				if (pawnSeega.IsPlaced)
				{
					num += this._boardValues[pawnSeega.X, pawnSeega.Y];
				}
			}
			return num;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0004ED0C File Offset: 0x0004CF0C
		private int GetSurroundedScore(bool player)
		{
			int num = 0;
			foreach (PawnBase pawnBase in (player ? this._board.PlayerOneUnits : this._board.PlayerTwoUnits))
			{
				PawnSeega pawnSeega = (PawnSeega)pawnBase;
				if (pawnSeega.IsPlaced)
				{
					num += this.GetAmountSurroundingThisPawn(pawnSeega);
				}
			}
			return num;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0004ED88 File Offset: 0x0004CF88
		private int GetAmountSurroundingThisPawn(PawnSeega pawn)
		{
			int num = 0;
			int x = pawn.X;
			int y = pawn.Y;
			if (x > 0 && this._board.GetTile(x - 1, y).PawnOnTile != null)
			{
				num++;
			}
			if (y > 0 && this._board.GetTile(x, y - 1).PawnOnTile != null)
			{
				num++;
			}
			if (x < BoardGameSeega.BoardWidth - 1 && this._board.GetTile(x + 1, y).PawnOnTile != null)
			{
				num++;
			}
			if (y < BoardGameSeega.BoardHeight - 1 && this._board.GetTile(x, y + 1).PawnOnTile != null)
			{
				num++;
			}
			return num;
		}

		// Token: 0x04000419 RID: 1049
		private readonly BoardGameSeega _board;

		// Token: 0x0400041A RID: 1050
		private readonly int[,] _boardValues = new int[,]
		{
			{
				3,
				2,
				2,
				2,
				3
			},
			{
				2,
				1,
				1,
				1,
				2
			},
			{
				2,
				1,
				3,
				1,
				2
			},
			{
				2,
				1,
				1,
				1,
				2
			},
			{
				3,
				2,
				2,
				2,
				3
			}
		};
	}
}
