using System;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Pawns
{
	// Token: 0x020000CB RID: 203
	public class PawnSeega : PawnBase
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0004C758 File Offset: 0x0004A958
		public override bool IsPlaced
		{
			get
			{
				return this.X >= 0 && this.X < BoardGameSeega.BoardWidth && this.Y >= 0 && this.Y < BoardGameSeega.BoardHeight;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0004C788 File Offset: 0x0004A988
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x0004C790 File Offset: 0x0004A990
		public bool MovedThisTurn { get; private set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000A4A RID: 2634 RVA: 0x0004C799 File Offset: 0x0004A999
		// (set) Token: 0x06000A4B RID: 2635 RVA: 0x0004C7A1 File Offset: 0x0004A9A1
		public int PrevX
		{
			get
			{
				return this._prevX;
			}
			set
			{
				this._prevX = value;
				if (value >= 0)
				{
					this.MovedThisTurn = true;
					return;
				}
				this.MovedThisTurn = false;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0004C7BD File Offset: 0x0004A9BD
		// (set) Token: 0x06000A4D RID: 2637 RVA: 0x0004C7C5 File Offset: 0x0004A9C5
		public int PrevY
		{
			get
			{
				return this._prevY;
			}
			set
			{
				this._prevY = value;
				if (value >= 0)
				{
					this.MovedThisTurn = true;
					return;
				}
				this.MovedThisTurn = false;
			}
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0004C7E1 File Offset: 0x0004A9E1
		public PawnSeega(GameEntity entity, bool playerOne) : base(entity, playerOne)
		{
			this.X = -1;
			this.Y = -1;
			this.PrevX = -1;
			this.PrevY = -1;
			this.MovedThisTurn = false;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0004C80E File Offset: 0x0004AA0E
		public override void Reset()
		{
			base.Reset();
			this.X = -1;
			this.Y = -1;
			this.PrevX = -1;
			this.PrevY = -1;
			this.MovedThisTurn = false;
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0004C839 File Offset: 0x0004AA39
		public void UpdateMoveBackAvailable()
		{
			if (this.MovedThisTurn)
			{
				this.MovedThisTurn = false;
				return;
			}
			this.PrevX = -1;
			this.PrevY = -1;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0004C859 File Offset: 0x0004AA59
		public void AISetMovedThisTurn(bool moved)
		{
			this.MovedThisTurn = moved;
		}

		// Token: 0x040003EF RID: 1007
		public int X;

		// Token: 0x040003F0 RID: 1008
		public int Y;

		// Token: 0x040003F1 RID: 1009
		private int _prevX;

		// Token: 0x040003F2 RID: 1010
		private int _prevY;
	}
}
