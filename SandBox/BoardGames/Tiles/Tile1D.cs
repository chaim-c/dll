using System;
using SandBox.BoardGames.Objects;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Tiles
{
	// Token: 0x020000C1 RID: 193
	public class Tile1D : TileBase
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060009E9 RID: 2537 RVA: 0x0004B843 File Offset: 0x00049A43
		public int X { get; }

		// Token: 0x060009EA RID: 2538 RVA: 0x0004B84B File Offset: 0x00049A4B
		public Tile1D(GameEntity entity, BoardGameDecal decal, int x) : base(entity, decal)
		{
			this.X = x;
		}
	}
}
