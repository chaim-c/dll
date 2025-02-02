using System;
using SandBox.BoardGames.Objects;
using SandBox.BoardGames.Pawns;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.BoardGames.Tiles
{
	// Token: 0x020000C3 RID: 195
	public abstract class TileBase
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0004B885 File Offset: 0x00049A85
		public GameEntity Entity { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0004B88D File Offset: 0x00049A8D
		public BoardGameDecal ValidMoveDecal { get; }

		// Token: 0x060009F0 RID: 2544 RVA: 0x0004B895 File Offset: 0x00049A95
		protected TileBase(GameEntity entity, BoardGameDecal decal)
		{
			this.Entity = entity;
			this.ValidMoveDecal = decal;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0004B8AB File Offset: 0x00049AAB
		public virtual void Reset()
		{
			this.PawnOnTile = null;
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0004B8B4 File Offset: 0x00049AB4
		public void Tick(float dt)
		{
			int num = this._showTile ? 1 : -1;
			this._tileFadeTimer += (float)num * dt * 5f;
			this._tileFadeTimer = MBMath.ClampFloat(this._tileFadeTimer, 0f, 1f);
			this.ValidMoveDecal.SetAlpha(this._tileFadeTimer);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0004B911 File Offset: 0x00049B11
		public void SetVisibility(bool isVisible)
		{
			this._showTile = isVisible;
		}

		// Token: 0x040003BA RID: 954
		public PawnBase PawnOnTile;

		// Token: 0x040003BB RID: 955
		private bool _showTile;

		// Token: 0x040003BC RID: 956
		private float _tileFadeTimer;

		// Token: 0x040003BD RID: 957
		private const float TileFadeDuration = 0.2f;
	}
}
