using System;
using SandBox.BoardGames.Objects;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace SandBox.BoardGames.Tiles
{
	// Token: 0x020000C5 RID: 197
	public class TilePuluc : Tile1D
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x0004B945 File Offset: 0x00049B45
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x0004B94D File Offset: 0x00049B4D
		public Vec3 PosLeft { get; private set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x0004B956 File Offset: 0x00049B56
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0004B95E File Offset: 0x00049B5E
		public Vec3 PosLeftMid { get; private set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0004B967 File Offset: 0x00049B67
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x0004B96F File Offset: 0x00049B6F
		public Vec3 PosRight { get; private set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0004B978 File Offset: 0x00049B78
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x0004B980 File Offset: 0x00049B80
		public Vec3 PosRightMid { get; private set; }

		// Token: 0x060009FF RID: 2559 RVA: 0x0004B989 File Offset: 0x00049B89
		public TilePuluc(GameEntity entity, BoardGameDecal decal, int x) : base(entity, decal, x)
		{
			this.UpdateTilePosition();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0004B99C File Offset: 0x00049B9C
		public void UpdateTilePosition()
		{
			MatrixFrame globalFrame = base.Entity.GetGlobalFrame();
			MetaMesh tileMesh = base.Entity.GetFirstScriptOfType<Tile>().TileMesh;
			Vec3 vec = tileMesh.GetBoundingBox().max - tileMesh.GetBoundingBox().min;
			Mat3 mat = globalFrame.rotation.TransformToParent(tileMesh.Frame.rotation);
			Vec3 v = mat.TransformToParent(new Vec3(0f, vec.y / 6f, 0f, -1f));
			Vec3 v2 = mat.TransformToParent(new Vec3(0f, vec.y / 3f, 0f, -1f));
			Vec3 globalPosition = base.Entity.GlobalPosition;
			this.PosLeft = globalPosition + v2;
			this.PosLeftMid = globalPosition + v;
			this.PosRight = globalPosition - v2;
			this.PosRightMid = globalPosition - v;
		}
	}
}
