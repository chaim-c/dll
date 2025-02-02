using System;
using TaleWorlds.Engine;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.Screens
{
	// Token: 0x02000035 RID: 53
	public class SceneEditorLayer : ScreenLayer
	{
		// Token: 0x0600026A RID: 618 RVA: 0x00016F24 File Offset: 0x00015124
		public SceneEditorLayer(string categoryId = "SceneEditorLayer") : base(-100, categoryId)
		{
			base.Name = "SceneEditorLayer";
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00016F3A File Offset: 0x0001513A
		protected override void OnActivate()
		{
			base.OnActivate();
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00016F42 File Offset: 0x00015142
		protected override void Tick(float dt)
		{
			base.Tick(dt);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00016F4B File Offset: 0x0001514B
		protected override void OnDeactivate()
		{
			base.OnDeactivate();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00016F54 File Offset: 0x00015154
		protected override void RefreshGlobalOrder(ref int currentOrder)
		{
			SceneView editorSceneView = MBEditor.GetEditorSceneView();
			if (editorSceneView != null)
			{
				editorSceneView.SetRenderOrder(currentOrder);
				currentOrder++;
			}
		}
	}
}
