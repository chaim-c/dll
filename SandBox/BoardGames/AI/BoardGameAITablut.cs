using System;
using Helpers;
using SandBox.BoardGames.MissionLogics;

namespace SandBox.BoardGames.AI
{
	// Token: 0x020000D6 RID: 214
	public class BoardGameAITablut : BoardGameAIBase
	{
		// Token: 0x06000ACA RID: 2762 RVA: 0x0004EE2A File Offset: 0x0004D02A
		public BoardGameAITablut(BoardGameHelper.AIDifficulty difficulty, MissionBoardGameLogic boardGameHandler) : base(difficulty, boardGameHandler)
		{
			BoardGameAITablut.Board = (base.BoardGameHandler.Board as BoardGameTablut);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0004EE4C File Offset: 0x0004D04C
		public override Move CalculateMovementStageMove()
		{
			Move openingMove;
			openingMove.GoalTile = null;
			openingMove.Unit = null;
			if (BoardGameAITablut.Board.IsReady)
			{
				BoardGameTablut.BoardInformation initialBoardState = BoardGameAITablut.Board.TakeBoardSnapshot();
				TreeNodeTablut treeNodeTablut = TreeNodeTablut.CreateTreeAndReturnRootNode(initialBoardState, this.MaxDepth);
				int num = 0;
				while (num < this._sampleCount && !base.AbortRequested)
				{
					treeNodeTablut.SelectAction();
					num++;
				}
				if (!base.AbortRequested)
				{
					BoardGameAITablut.Board.UndoMove(ref initialBoardState);
					TreeNodeTablut childWithBestScore = treeNodeTablut.GetChildWithBestScore();
					if (childWithBestScore != null)
					{
						openingMove = childWithBestScore.OpeningMove;
					}
				}
			}
			if (!base.AbortRequested)
			{
				bool isValid = openingMove.IsValid;
			}
			return openingMove;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0004EEE8 File Offset: 0x0004D0E8
		protected override void InitializeDifficulty()
		{
			switch (base.Difficulty)
			{
			case BoardGameHelper.AIDifficulty.Easy:
				this.MaxDepth = 3;
				this._sampleCount = 30000;
				return;
			case BoardGameHelper.AIDifficulty.Normal:
				this.MaxDepth = 4;
				this._sampleCount = 47000;
				return;
			case BoardGameHelper.AIDifficulty.Hard:
				this.MaxDepth = 5;
				this._sampleCount = 64000;
				return;
			default:
				return;
			}
		}

		// Token: 0x0400041B RID: 1051
		public static BoardGameTablut Board;

		// Token: 0x0400041C RID: 1052
		private int _sampleCount;
	}
}
