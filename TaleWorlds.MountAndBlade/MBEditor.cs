using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.ObjectSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C1 RID: 449
	public class MBEditor
	{
		// Token: 0x060019D3 RID: 6611 RVA: 0x0005B9D4 File Offset: 0x00059BD4
		[MBCallback]
		internal static void SetEditorScene(Scene scene)
		{
			if (MBEditor._editorScene != null)
			{
				if (MBEditor._agentRendererSceneController != null)
				{
					MBAgentRendererSceneController.DestructAgentRendererSceneController(MBEditor._editorScene, MBEditor._agentRendererSceneController, false);
				}
				MBEditor._editorScene.ClearAll();
			}
			MBEditor._editorScene = scene;
			MBEditor._agentRendererSceneController = MBAgentRendererSceneController.CreateNewAgentRendererSceneController(MBEditor._editorScene, 32);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x0005BA26 File Offset: 0x00059C26
		[MBCallback]
		internal static void CloseEditorScene()
		{
			if (MBEditor._agentRendererSceneController != null)
			{
				MBAgentRendererSceneController.DestructAgentRendererSceneController(MBEditor._editorScene, MBEditor._agentRendererSceneController, false);
			}
			MBEditor._agentRendererSceneController = null;
			MBEditor._editorScene = null;
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x0005BA4B File Offset: 0x00059C4B
		[MBCallback]
		internal static void DestroyEditor(Scene scene)
		{
			MBAgentRendererSceneController.DestructAgentRendererSceneController(MBEditor._editorScene, MBEditor._agentRendererSceneController, false);
			MBEditor._editorScene.ClearAll();
			MBEditor._editorScene = null;
			MBEditor._agentRendererSceneController = null;
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060019D6 RID: 6614 RVA: 0x0005BA73 File Offset: 0x00059C73
		public static bool IsEditModeOn
		{
			get
			{
				return MBAPI.IMBEditor.IsEditMode();
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0005BA7F File Offset: 0x00059C7F
		public static bool EditModeEnabled
		{
			get
			{
				return MBAPI.IMBEditor.IsEditModeEnabled();
			}
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x0005BA8B File Offset: 0x00059C8B
		public static void UpdateSceneTree()
		{
			MBAPI.IMBEditor.UpdateSceneTree();
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x0005BA97 File Offset: 0x00059C97
		public static bool IsEntitySelected(GameEntity entity)
		{
			return MBAPI.IMBEditor.IsEntitySelected(entity.Pointer);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0005BAA9 File Offset: 0x00059CA9
		public static void RenderEditorMesh(MetaMesh mesh, MatrixFrame frame)
		{
			MBAPI.IMBEditor.RenderEditorMesh(mesh.Pointer, ref frame);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0005BABD File Offset: 0x00059CBD
		public static void EnterEditMode(SceneView sceneView, MatrixFrame initialCameraFrame, float initialCameraElevation, float initialCameraBearing)
		{
			MBAPI.IMBEditor.EnterEditMode(sceneView.Pointer, ref initialCameraFrame, initialCameraElevation, initialCameraBearing);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x0005BAD3 File Offset: 0x00059CD3
		public static void TickEditMode(float dt)
		{
			MBAPI.IMBEditor.TickEditMode(dt);
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x0005BAE0 File Offset: 0x00059CE0
		public static void LeaveEditMode()
		{
			MBAPI.IMBEditor.LeaveEditMode();
			MBAgentRendererSceneController.DestructAgentRendererSceneController(MBEditor._editorScene, MBEditor._agentRendererSceneController, false);
			MBEditor._agentRendererSceneController = null;
			MBEditor._editorScene = null;
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x0005BB08 File Offset: 0x00059D08
		public static void EnterEditMissionMode(Mission mission)
		{
			MBAPI.IMBEditor.EnterEditMissionMode(mission.Pointer);
			MBEditor._isEditorMissionOn = true;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0005BB20 File Offset: 0x00059D20
		public static void LeaveEditMissionMode()
		{
			MBAPI.IMBEditor.LeaveEditMissionMode();
			MBEditor._isEditorMissionOn = false;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x0005BB32 File Offset: 0x00059D32
		public static bool IsEditorMissionOn()
		{
			return MBEditor._isEditorMissionOn && MBEditor.IsEditModeOn;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x0005BB44 File Offset: 0x00059D44
		public static void ActivateSceneEditorPresentation()
		{
			Monster.GetBoneIndexWithId = new Func<string, string, sbyte>(MBActionSet.GetBoneIndexWithId);
			Monster.GetBoneHasParentBone = new Func<string, sbyte, bool>(MBActionSet.GetBoneHasParentBone);
			MBObjectManager.Init();
			MBObjectManager.Instance.RegisterType<Monster>("Monster", "Monsters", 2U, true, false);
			MBObjectManager.Instance.LoadXML("Monsters", true);
			MBAPI.IMBEditor.ActivateSceneEditorPresentation();
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x0005BBAA File Offset: 0x00059DAA
		public static void DeactivateSceneEditorPresentation()
		{
			MBAPI.IMBEditor.DeactivateSceneEditorPresentation();
			MBObjectManager.Instance.Destroy();
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x0005BBC0 File Offset: 0x00059DC0
		public static void TickSceneEditorPresentation(float dt)
		{
			MBAPI.IMBEditor.TickSceneEditorPresentation(dt);
			LoadingWindow.DisableGlobalLoadingWindow();
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x0005BBD2 File Offset: 0x00059DD2
		public static SceneView GetEditorSceneView()
		{
			return MBAPI.IMBEditor.GetEditorSceneView();
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x0005BBDE File Offset: 0x00059DDE
		public static bool HelpersEnabled()
		{
			return MBAPI.IMBEditor.HelpersEnabled();
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x0005BBEA File Offset: 0x00059DEA
		public static bool BorderHelpersEnabled()
		{
			return MBAPI.IMBEditor.BorderHelpersEnabled();
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x0005BBF6 File Offset: 0x00059DF6
		public static void ZoomToPosition(Vec3 pos)
		{
			MBAPI.IMBEditor.ZoomToPosition(pos);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x0005BC03 File Offset: 0x00059E03
		public static bool IsReplayManagerReplaying()
		{
			return MBAPI.IMBEditor.IsReplayManagerReplaying();
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x0005BC0F File Offset: 0x00059E0F
		public static bool IsReplayManagerRendering()
		{
			return MBAPI.IMBEditor.IsReplayManagerRendering();
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x0005BC1B File Offset: 0x00059E1B
		public static bool IsReplayManagerRecording()
		{
			return MBAPI.IMBEditor.IsReplayManagerRecording();
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x0005BC27 File Offset: 0x00059E27
		public static void AddEditorWarning(string msg)
		{
			MBAPI.IMBEditor.AddEditorWarning(msg);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x0005BC34 File Offset: 0x00059E34
		public static void AddEntityWarning(GameEntity entityId, string msg)
		{
			MBAPI.IMBEditor.AddEntityWarning(entityId.Pointer, msg);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x0005BC47 File Offset: 0x00059E47
		public static string GetAllPrefabsAndChildWithTag(string tag)
		{
			return MBAPI.IMBEditor.GetAllPrefabsAndChildWithTag(tag);
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x0005BC54 File Offset: 0x00059E54
		public static void ExitEditMode()
		{
			MBAPI.IMBEditor.ExitEditMode();
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0005BC60 File Offset: 0x00059E60
		public static void SetUpgradeLevelVisibility(List<string> levels)
		{
			string text = "";
			for (int i = 0; i < levels.Count - 1; i++)
			{
				text = text + levels[i] + "|";
			}
			text += levels[levels.Count - 1];
			MBAPI.IMBEditor.SetUpgradeLevelVisibility(text);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0005BCB9 File Offset: 0x00059EB9
		public static void SetLevelVisibility(List<string> levels)
		{
		}

		// Token: 0x040007E6 RID: 2022
		public static Scene _editorScene;

		// Token: 0x040007E7 RID: 2023
		private static MBAgentRendererSceneController _agentRendererSceneController;

		// Token: 0x040007E8 RID: 2024
		public static bool _isEditorMissionOn;
	}
}
