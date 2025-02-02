using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200033C RID: 828
	public class PathLastNodeFixer : UsableMissionObjectComponent
	{
		// Token: 0x06002CA6 RID: 11430 RVA: 0x000B051C File Offset: 0x000AE71C
		protected internal override void OnEditorTick(float dt)
		{
			base.OnEditorTick(dt);
			Path pathWithName = this._scene.GetPathWithName(this.PathHolder.PathEntity);
			this.Update(pathWithName);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000B054E File Offset: 0x000AE74E
		protected internal override void OnAdded(Scene scene)
		{
			base.OnAdded(scene);
			this._scene = scene;
			this.Update();
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x000B0564 File Offset: 0x000AE764
		public void Update()
		{
			Path pathWithName = this._scene.GetPathWithName(this.PathHolder.PathEntity);
			this.Update(pathWithName);
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x000B058F File Offset: 0x000AE78F
		private void Update(Path path)
		{
			path != null;
		}

		// Token: 0x04001207 RID: 4615
		public IPathHolder PathHolder;

		// Token: 0x04001208 RID: 4616
		private Scene _scene;
	}
}
