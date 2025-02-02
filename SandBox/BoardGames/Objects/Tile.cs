using System;
using TaleWorlds.Engine;

namespace SandBox.BoardGames.Objects
{
	// Token: 0x020000CE RID: 206
	public class Tile : ScriptComponentBehavior
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x0004C8E6 File Offset: 0x0004AAE6
		protected override void OnInit()
		{
			base.OnInit();
			base.GameEntity.RemoveMultiMesh(base.GameEntity.GetMetaMesh(0));
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0004C906 File Offset: 0x0004AB06
		public void SetVisibility(bool visible)
		{
			base.GameEntity.SetVisibilityExcludeParents(visible);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0004C914 File Offset: 0x0004AB14
		protected override bool MovesEntity()
		{
			return false;
		}

		// Token: 0x040003F6 RID: 1014
		public MetaMesh TileMesh;
	}
}
