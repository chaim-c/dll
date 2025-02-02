using System;
using SandBox.BoardGames.Pawns;
using SandBox.BoardGames.Tiles;

namespace SandBox.BoardGames
{
	// Token: 0x020000B9 RID: 185
	public struct Move
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00043F45 File Offset: 0x00042145
		public bool IsValid
		{
			get
			{
				return this.Unit != null && this.GoalTile != null;
			}
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00043F5A File Offset: 0x0004215A
		public Move(PawnBase unit, TileBase goalTile)
		{
			this.Unit = unit;
			this.GoalTile = goalTile;
		}

		// Token: 0x04000359 RID: 857
		public static readonly Move Invalid = new Move
		{
			Unit = null,
			GoalTile = null
		};

		// Token: 0x0400035A RID: 858
		public PawnBase Unit;

		// Token: 0x0400035B RID: 859
		public TileBase GoalTile;
	}
}
