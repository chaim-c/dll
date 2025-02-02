using System;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Pawns
{
	// Token: 0x020000CC RID: 204
	public class PawnTablut : PawnBase
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000A52 RID: 2642 RVA: 0x0004C862 File Offset: 0x0004AA62
		public override bool IsPlaced
		{
			get
			{
				return this.X >= 0 && this.X < 9 && this.Y >= 0 && this.Y < 9;
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0004C88C File Offset: 0x0004AA8C
		public PawnTablut(GameEntity entity, bool playerOne) : base(entity, playerOne)
		{
			this.X = -1;
			this.Y = -1;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0004C8A4 File Offset: 0x0004AAA4
		public override void Reset()
		{
			base.Reset();
			this.X = -1;
			this.Y = -1;
		}

		// Token: 0x040003F4 RID: 1012
		public int X;

		// Token: 0x040003F5 RID: 1013
		public int Y;
	}
}
