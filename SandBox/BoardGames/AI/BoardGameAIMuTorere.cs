using System;
using System.Collections.Generic;
using Helpers;
using SandBox.BoardGames.MissionLogics;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.BoardGames.AI
{
	// Token: 0x020000D3 RID: 211
	public class BoardGameAIMuTorere : BoardGameAIBase
	{
		// Token: 0x06000AB2 RID: 2738 RVA: 0x0004DFAE File Offset: 0x0004C1AE
		public BoardGameAIMuTorere(BoardGameHelper.AIDifficulty difficulty, MissionBoardGameLogic boardGameHandler) : base(difficulty, boardGameHandler)
		{
			this._board = (base.BoardGameHandler.Board as BoardGameMuTorere);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0004DFD0 File Offset: 0x0004C1D0
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

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0004E010 File Offset: 0x0004C210
		public override Move CalculateMovementStageMove()
		{
			Move result;
			result.GoalTile = null;
			result.Unit = null;
			if (this._board.IsReady)
			{
				List<List<Move>> list = this._board.CalculateAllValidMoves(BoardGameSide.AI);
				BoardGameMuTorere.BoardInformation boardInformation = this._board.TakePawnsSnapshot();
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
							int num2 = -this.NegaMax(this.MaxDepth, -1);
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

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0004E13C File Offset: 0x0004C33C
		private int NegaMax(int depth, int color)
		{
			int num = int.MinValue;
			if (depth == 0)
			{
				return color * this.Evaluation() * ((this._board.PlayerWhoStarted == PlayerTurn.PlayerOne) ? 1 : -1);
			}
			BoardGameMuTorere.BoardInformation boardInformation = this._board.TakePawnsSnapshot();
			List<List<Move>> list = this._board.CalculateAllValidMoves((color == 1) ? BoardGameSide.AI : BoardGameSide.Player);
			if (!this._board.HasMovesAvailable(ref list))
			{
				return color * this.Evaluation();
			}
			foreach (List<Move> list2 in list)
			{
				foreach (Move move in list2)
				{
					this._board.AIMakeMove(move);
					num = MathF.Max(num, -this.NegaMax(depth - 1, -color));
					this._board.UndoMove(ref boardInformation);
				}
			}
			return num;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0004E244 File Offset: 0x0004C444
		private int Evaluation()
		{
			float num = MBRandom.RandomFloat;
			switch (base.Difficulty)
			{
			case BoardGameHelper.AIDifficulty.Easy:
				num = num * 2f - 1f;
				break;
			case BoardGameHelper.AIDifficulty.Normal:
				num = num * 1.7f - 0.7f;
				break;
			case BoardGameHelper.AIDifficulty.Hard:
				num = num * 1.4f - 0.4f;
				break;
			}
			return (int)(num * 100f * (float)(this.CanMove(false) - this.CanMove(true)));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0004E2BC File Offset: 0x0004C4BC
		private int CanMove(bool playerOne)
		{
			List<List<Move>> list = this._board.CalculateAllValidMoves(playerOne ? BoardGameSide.Player : BoardGameSide.AI);
			if (!this._board.HasMovesAvailable(ref list))
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x04000416 RID: 1046
		private readonly BoardGameMuTorere _board;
	}
}
