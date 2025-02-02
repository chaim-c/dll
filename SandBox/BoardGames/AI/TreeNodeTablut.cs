using System;
using System.Collections.Generic;
using System.Linq;
using SandBox.BoardGames.Pawns;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace SandBox.BoardGames.AI
{
	// Token: 0x020000D7 RID: 215
	public class TreeNodeTablut
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0004EF47 File Offset: 0x0004D147
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0004EF4F File Offset: 0x0004D14F
		public Move OpeningMove { get; private set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0004EF58 File Offset: 0x0004D158
		private bool IsLeaf
		{
			get
			{
				return this._children == null;
			}
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0004EF63 File Offset: 0x0004D163
		public TreeNodeTablut(BoardGameSide side, int depth)
		{
			this._side = side;
			this._depth = depth;
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0004EF79 File Offset: 0x0004D179
		public static TreeNodeTablut CreateTreeAndReturnRootNode(BoardGameTablut.BoardInformation initialBoardState, int maxDepth)
		{
			TreeNodeTablut.MaxDepth = maxDepth;
			return new TreeNodeTablut(BoardGameSide.Player, 0)
			{
				_boardState = initialBoardState
			};
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0004EF90 File Offset: 0x0004D190
		public TreeNodeTablut GetChildWithBestScore()
		{
			TreeNodeTablut result = null;
			if (!this.IsLeaf)
			{
				float num = float.MinValue;
				foreach (TreeNodeTablut treeNodeTablut in this._children)
				{
					if (treeNodeTablut._visits > 0)
					{
						float num2 = (float)treeNodeTablut._wins / (float)treeNodeTablut._visits;
						if (num2 > num)
						{
							result = treeNodeTablut;
							num = num2;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0004F014 File Offset: 0x0004D214
		public void SelectAction()
		{
			TreeNodeTablut treeNodeTablut = this;
			while (!treeNodeTablut.IsLeaf)
			{
				treeNodeTablut = treeNodeTablut.Select();
			}
			TreeNodeTablut.ExpandResult expandResult = treeNodeTablut.Expand();
			if (expandResult == TreeNodeTablut.ExpandResult.NeedsToBeSimulated)
			{
				if (treeNodeTablut._children != null)
				{
					treeNodeTablut = treeNodeTablut.Select();
				}
				BoardGameTablut.State state = this.Simulate(ref treeNodeTablut);
				if (state != BoardGameTablut.State.Aborted)
				{
					BoardGameSide winner = (state == BoardGameTablut.State.AIWon) ? BoardGameSide.AI : BoardGameSide.Player;
					treeNodeTablut.BackPropagate(winner);
					return;
				}
			}
			else
			{
				BoardGameSide winner2 = (expandResult == TreeNodeTablut.ExpandResult.AIWon) ? BoardGameSide.AI : BoardGameSide.Player;
				treeNodeTablut.BackPropagate(winner2);
			}
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0004F080 File Offset: 0x0004D280
		private TreeNodeTablut Select()
		{
			TreeNodeTablut treeNodeTablut = null;
			if (!this.IsLeaf)
			{
				double num = double.MinValue;
				foreach (TreeNodeTablut treeNodeTablut2 in this._children)
				{
					if (treeNodeTablut2._visits == 0)
					{
						treeNodeTablut = treeNodeTablut2;
						break;
					}
					double num2 = (double)treeNodeTablut2._wins / (double)treeNodeTablut2._visits + (double)(1.5f * MathF.Sqrt(MathF.Log((float)this._visits) / (float)treeNodeTablut2._visits));
					if (num2 > num)
					{
						treeNodeTablut = treeNodeTablut2;
						num = num2;
					}
				}
				if (treeNodeTablut != null && treeNodeTablut._boardState.PawnInformation == null)
				{
					BoardGameAITablut.Board.UndoMove(ref treeNodeTablut._parent._boardState);
					BoardGameAITablut.Board.AIMakeMove(treeNodeTablut.OpeningMove);
					treeNodeTablut._boardState = BoardGameAITablut.Board.TakeBoardSnapshot();
				}
			}
			return treeNodeTablut;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0004F174 File Offset: 0x0004D374
		private TreeNodeTablut.ExpandResult Expand()
		{
			TreeNodeTablut.ExpandResult result = TreeNodeTablut.ExpandResult.NeedsToBeSimulated;
			if (this._depth < TreeNodeTablut.MaxDepth)
			{
				BoardGameAITablut.Board.UndoMove(ref this._boardState);
				BoardGameTablut.State state = BoardGameAITablut.Board.CheckGameState();
				if (state == BoardGameTablut.State.InProgress)
				{
					BoardGameSide side = (this._side == BoardGameSide.Player) ? BoardGameSide.AI : BoardGameSide.Player;
					List<List<Move>> list = BoardGameAITablut.Board.CalculateAllValidMoves(side);
					int totalMovesAvailable = BoardGameAITablut.Board.GetTotalMovesAvailable(ref list);
					if (totalMovesAvailable > 0)
					{
						this._children = new List<TreeNodeTablut>(totalMovesAvailable);
						using (List<List<Move>>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								List<Move> list2 = enumerator.Current;
								foreach (Move openingMove in list2)
								{
									TreeNodeTablut treeNodeTablut = new TreeNodeTablut(side, this._depth + 1);
									treeNodeTablut.OpeningMove = openingMove;
									treeNodeTablut._parent = this;
									this._children.Add(treeNodeTablut);
								}
							}
							return result;
						}
					}
					Debug.FailedAssert("No available moves left but the game is in progress", "C:\\Develop\\MB3\\Source\\Bannerlord\\SandBox\\BoardGames\\AI\\TreeNodeTablut.cs", "Expand", 203);
				}
				else if (state != BoardGameTablut.State.Aborted)
				{
					if (state == BoardGameTablut.State.AIWon)
					{
						result = TreeNodeTablut.ExpandResult.AIWon;
					}
					else
					{
						result = TreeNodeTablut.ExpandResult.PlayerWon;
					}
				}
			}
			return result;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0004F2BC File Offset: 0x0004D4BC
		private BoardGameTablut.State Simulate(ref TreeNodeTablut tn)
		{
			BoardGameAITablut.Board.UndoMove(ref tn._boardState);
			BoardGameTablut.State state = BoardGameAITablut.Board.CheckGameState();
			BoardGameSide side = tn._side;
			while (state == BoardGameTablut.State.InProgress)
			{
				List<PawnBase> list = (tn._side == BoardGameSide.Player) ? BoardGameAITablut.Board.PlayerOneUnits : BoardGameAITablut.Board.PlayerTwoUnits;
				int count = list.Count;
				int num = 3;
				PawnBase pawnBase;
				bool flag;
				do
				{
					pawnBase = list[MBRandom.RandomInt(count)];
					flag = BoardGameAITablut.Board.HasAvailableMoves(pawnBase as PawnTablut);
					num--;
				}
				while (!flag && num > 0);
				if (!flag)
				{
					pawnBase = (from x in list
					orderby MBRandom.RandomInt()
					select x).FirstOrDefault((PawnBase x) => BoardGameAITablut.Board.HasAvailableMoves(x as PawnTablut));
					flag = (pawnBase != null);
				}
				if (flag)
				{
					Move randomAvailableMove = BoardGameAITablut.Board.GetRandomAvailableMove(pawnBase as PawnTablut);
					BoardGameAITablut.Board.AIMakeMove(randomAvailableMove);
					state = BoardGameAITablut.Board.CheckGameState();
				}
				else if (tn._side == BoardGameSide.Player)
				{
					state = BoardGameTablut.State.AIWon;
				}
				else
				{
					state = BoardGameTablut.State.PlayerWon;
				}
				tn._side = ((tn._side == BoardGameSide.Player) ? BoardGameSide.AI : BoardGameSide.Player);
			}
			tn._side = side;
			return state;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0004F40C File Offset: 0x0004D60C
		private void BackPropagate(BoardGameSide winner)
		{
			for (TreeNodeTablut treeNodeTablut = this; treeNodeTablut != null; treeNodeTablut = treeNodeTablut._parent)
			{
				treeNodeTablut._visits++;
				if (winner == treeNodeTablut._side)
				{
					treeNodeTablut._wins++;
				}
			}
		}

		// Token: 0x0400041D RID: 1053
		private const float UCTConstant = 1.5f;

		// Token: 0x0400041E RID: 1054
		private static int MaxDepth;

		// Token: 0x0400041F RID: 1055
		private readonly int _depth;

		// Token: 0x04000420 RID: 1056
		private BoardGameTablut.BoardInformation _boardState;

		// Token: 0x04000421 RID: 1057
		private TreeNodeTablut _parent;

		// Token: 0x04000422 RID: 1058
		private List<TreeNodeTablut> _children;

		// Token: 0x04000423 RID: 1059
		private BoardGameSide _side;

		// Token: 0x04000424 RID: 1060
		private int _visits;

		// Token: 0x04000425 RID: 1061
		private int _wins;

		// Token: 0x020001C7 RID: 455
		private enum ExpandResult
		{
			// Token: 0x0400078A RID: 1930
			NeedsToBeSimulated,
			// Token: 0x0400078B RID: 1931
			AIWon,
			// Token: 0x0400078C RID: 1932
			PlayerWon
		}
	}
}
