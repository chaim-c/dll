using System;
using SandBox.BoardGames.Pawns;

namespace SandBox.BoardGames
{
	// Token: 0x020000B8 RID: 184
	public struct TileBaseInformation
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x00043F3B File Offset: 0x0004213B
		public TileBaseInformation(ref PawnBase pawnOnTile)
		{
			this.PawnOnTile = pawnOnTile;
		}

		// Token: 0x04000358 RID: 856
		public PawnBase PawnOnTile;
	}
}
