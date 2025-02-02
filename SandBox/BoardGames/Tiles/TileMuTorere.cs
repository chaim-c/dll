using System;
using SandBox.BoardGames.Objects;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Tiles
{
	// Token: 0x020000C4 RID: 196
	public class TileMuTorere : Tile1D
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x0004B91A File Offset: 0x00049B1A
		public int XLeftTile { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x0004B922 File Offset: 0x00049B22
		public int XRightTile { get; }

		// Token: 0x060009F6 RID: 2550 RVA: 0x0004B92A File Offset: 0x00049B2A
		public TileMuTorere(GameEntity entity, BoardGameDecal decal, int x, int xLeft, int xRight) : base(entity, decal, x)
		{
			this.XLeftTile = xLeft;
			this.XRightTile = xRight;
		}
	}
}
