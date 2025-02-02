using System;
using SandBox.BoardGames.Objects;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Tiles
{
	// Token: 0x020000C2 RID: 194
	public class Tile2D : TileBase
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x0004B85C File Offset: 0x00049A5C
		public int X { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x0004B864 File Offset: 0x00049A64
		public int Y { get; }

		// Token: 0x060009ED RID: 2541 RVA: 0x0004B86C File Offset: 0x00049A6C
		public Tile2D(GameEntity entity, BoardGameDecal decal, int x, int y) : base(entity, decal)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
