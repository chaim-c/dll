using System;
using System.Collections.Generic;
using Helpers;
using SandBox.BoardGames.MissionLogics;
using SandBox.BoardGames.Pawns;

namespace SandBox.BoardGames.AI
{
	// Token: 0x020000D4 RID: 212
	public class BoardGameAIPuluc : BoardGameAIBase
	{
		// Token: 0x06000AB8 RID: 2744 RVA: 0x0004E2EE File Offset: 0x0004C4EE
		public BoardGameAIPuluc(BoardGameHelper.AIDifficulty difficulty, MissionBoardGameLogic boardGameHandler) : base(difficulty, boardGameHandler)
		{
			this._board = (base.BoardGameHandler.Board as BoardGamePuluc);
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x0004E328 File Offset: 0x0004C528
		protected override void InitializeDifficulty()
		{
			switch (base.Difficulty)
			{
			case BoardGameHelper.AIDifficulty.Easy:
				this.MaxDepth = 3;
				return;
			case BoardGameHelper.AIDifficulty.Normal:
				this.MaxDepth = 5;
				return;
			case BoardGameHelper.AIDifficulty.Hard:
				this.MaxDepth = 7;
				return;
			default:
				return;
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0004E368 File Offset: 0x0004C568
		public override Move CalculateMovementStageMove()
		{
			Move result;
			result.GoalTile = null;
			result.Unit = null;
			if (this._board.IsReady)
			{
				this.ExpectiMax(this.MaxDepth, BoardGameSide.AI, false, ref result);
			}
			if (!base.AbortRequested)
			{
				bool isValid = result.IsValid;
			}
			return result;
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0004E3B4 File Offset: 0x0004C5B4
		private float ExpectiMax(int depth, BoardGameSide side, bool chanceNode, ref Move bestMove)
		{
			float num;
			if (depth == 0)
			{
				num = (float)this.Evaluation();
				if (side == BoardGameSide.Player)
				{
					num = -num;
				}
			}
			else if (chanceNode)
			{
				num = 0f;
				for (int i = 0; i < 5; i++)
				{
					int lastDice = this._board.LastDice;
					this._board.ForceDice((i == 0) ? 5 : i);
					num += this._diceProbabilities[i] * this.ExpectiMax(depth - 1, side, false, ref bestMove);
					this._board.ForceDice(lastDice);
				}
			}
			else
			{
				BoardGamePuluc.BoardInformation boardInformation = this._board.TakeBoardSnapshot();
				List<List<Move>> list = this._board.CalculateAllValidMoves(side);
				if (this._board.HasMovesAvailable(ref list))
				{
					num = float.MinValue;
					using (List<List<Move>>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							List<Move> list2 = enumerator.Current;
							if (list2 != null)
							{
								foreach (Move move in list2)
								{
									this._board.AIMakeMove(move);
									BoardGameSide side2 = (side == BoardGameSide.AI) ? BoardGameSide.Player : BoardGameSide.AI;
									float num2 = -this.ExpectiMax(depth - 1, side2, true, ref bestMove);
									this._board.UndoMove(ref boardInformation);
									if (num < num2)
									{
										num = num2;
										if (depth == this.MaxDepth)
										{
											bestMove = move;
										}
									}
								}
							}
						}
						return num;
					}
				}
				num = (float)this.Evaluation();
				if (side == BoardGameSide.Player)
				{
					num = -num;
				}
			}
			return num;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0004E548 File Offset: 0x0004C748
		private int Evaluation()
		{
			return 20 * (this._board.GetPlayerTwoUnitsAlive() - this._board.GetPlayerOneUnitsAlive()) + 5 * (this.GetUnitsBeingCaptured(true) - this.GetUnitsBeingCaptured(false)) + (this.GetUnitsInPlay(false) - this.GetUnitsInPlay(true));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0004E588 File Offset: 0x0004C788
		private int GetUnitsInSpawn(bool playerOne)
		{
			int num = 0;
			using (List<PawnBase>.Enumerator enumerator = (playerOne ? this._board.PlayerOneUnits : this._board.PlayerTwoUnits).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((PawnPuluc)enumerator.Current).IsInSpawn)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0004E5FC File Offset: 0x0004C7FC
		private int GetUnitsBeingCaptured(bool playerOne)
		{
			int num = 0;
			using (List<PawnBase>.Enumerator enumerator = (playerOne ? this._board.PlayerOneUnits : this._board.PlayerTwoUnits).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!((PawnPuluc)enumerator.Current).IsTopPawn)
					{
						num++;
					}
				}
			}
			return num;
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0004E670 File Offset: 0x0004C870
		private int GetUnitsInPlay(bool playerOne)
		{
			int num = 0;
			foreach (PawnBase pawnBase in (playerOne ? this._board.PlayerOneUnits : this._board.PlayerTwoUnits))
			{
				PawnPuluc pawnPuluc = (PawnPuluc)pawnBase;
				if (pawnPuluc.InPlay && pawnPuluc.IsTopPawn)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x04000417 RID: 1047
		private readonly BoardGamePuluc _board;

		// Token: 0x04000418 RID: 1048
		private readonly float[] _diceProbabilities = new float[]
		{
			0.0625f,
			0.25f,
			0.375f,
			0.25f,
			0.0625f
		};
	}
}
