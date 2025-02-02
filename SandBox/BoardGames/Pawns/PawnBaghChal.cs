using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.BoardGames.Pawns
{
	// Token: 0x020000C6 RID: 198
	public class PawnBaghChal : PawnBase
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x0004BA95 File Offset: 0x00049C95
		public override bool IsPlaced
		{
			get
			{
				return this.X >= 0 && this.X < BoardGameBaghChal.BoardWidth && this.Y >= 0 && this.Y < BoardGameBaghChal.BoardHeight;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0004BAC5 File Offset: 0x00049CC5
		public MatrixFrame InitialFrame { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x0004BACD File Offset: 0x00049CCD
		public bool IsTiger { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x0004BAD5 File Offset: 0x00049CD5
		public bool IsGoat
		{
			get
			{
				return !this.IsTiger;
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0004BAE0 File Offset: 0x00049CE0
		public PawnBaghChal(GameEntity entity, bool playerOne, bool isTiger) : base(entity, playerOne)
		{
			this.X = -1;
			this.Y = -1;
			this.PrevX = -1;
			this.PrevY = -1;
			this.IsTiger = isTiger;
			this.InitialFrame = base.Entity.GetFrame();
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0004BB1E File Offset: 0x00049D1E
		public override void Reset()
		{
			base.Reset();
			this.X = -1;
			this.Y = -1;
			this.PrevX = -1;
			this.PrevY = -1;
		}

		// Token: 0x040003C6 RID: 966
		public int X;

		// Token: 0x040003C7 RID: 967
		public int Y;

		// Token: 0x040003C8 RID: 968
		public int PrevX;

		// Token: 0x040003C9 RID: 969
		public int PrevY;
	}
}
