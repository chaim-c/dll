using System;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Pawns
{
	// Token: 0x020000C8 RID: 200
	public class PawnKonane : PawnBase
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000A2F RID: 2607 RVA: 0x0004C2BB File Offset: 0x0004A4BB
		public override bool IsPlaced
		{
			get
			{
				return this.X >= 0 && this.X < BoardGameKonane.BoardWidth && this.Y >= 0 && this.Y < BoardGameKonane.BoardHeight;
			}
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0004C2EB File Offset: 0x0004A4EB
		public PawnKonane(GameEntity entity, bool playerOne) : base(entity, playerOne)
		{
			this.X = -1;
			this.Y = -1;
			this.PrevX = -1;
			this.PrevY = -1;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0004C311 File Offset: 0x0004A511
		public override void Reset()
		{
			base.Reset();
			this.X = -1;
			this.Y = -1;
			this.PrevX = -1;
			this.PrevY = -1;
		}

		// Token: 0x040003E2 RID: 994
		public int X;

		// Token: 0x040003E3 RID: 995
		public int Y;

		// Token: 0x040003E4 RID: 996
		public int PrevX;

		// Token: 0x040003E5 RID: 997
		public int PrevY;
	}
}
