using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ModuleManager;
using TaleWorlds.ScreenSystem;

namespace TaleWorlds.MountAndBlade.View.Screens
{
	// Token: 0x02000034 RID: 52
	[GameStateScreen(typeof(EditorState))]
	public class SceneEditorScreen : ScreenBase, IGameStateListener
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00016E91 File Offset: 0x00015091
		public SceneEditorScreen(EditorState editorState)
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00016E9C File Offset: 0x0001509C
		protected override void OnInitialize()
		{
			base.OnInitialize();
			SceneEditorLayer sceneEditorLayer = new SceneEditorLayer("SceneEditorLayer");
			sceneEditorLayer.InputRestrictions.SetInputRestrictions(true, InputUsageMask.Invalid);
			base.AddLayer(sceneEditorLayer);
			ManagedParameters.Instance.Initialize(ModuleHelper.GetXmlPath("Native", "managed_core_parameters"));
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00016EE7 File Offset: 0x000150E7
		protected override void OnActivate()
		{
			base.OnActivate();
			MouseManager.ActivateMouseCursor(CursorType.System);
			MBEditor.ActivateSceneEditorPresentation();
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00016EFA File Offset: 0x000150FA
		protected override void OnDeactivate()
		{
			MBEditor.DeactivateSceneEditorPresentation();
			MouseManager.ActivateMouseCursor(CursorType.Default);
			base.OnDeactivate();
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00016F0D File Offset: 0x0001510D
		protected override void OnFrameTick(float dt)
		{
			base.OnFrameTick(dt);
			MBEditor.TickSceneEditorPresentation(dt);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00016F1C File Offset: 0x0001511C
		void IGameStateListener.OnActivate()
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00016F1E File Offset: 0x0001511E
		void IGameStateListener.OnDeactivate()
		{
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00016F20 File Offset: 0x00015120
		void IGameStateListener.OnInitialize()
		{
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00016F22 File Offset: 0x00015122
		void IGameStateListener.OnFinalize()
		{
		}
	}
}
