using System;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000194 RID: 404
	public static class BannerlordTableauManager
	{
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x0004EE76 File Offset: 0x0004D076
		public static Scene[] TableauCharacterScenes
		{
			get
			{
				return BannerlordTableauManager._tableauCharacterScenes;
			}
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0004EE7D File Offset: 0x0004D07D
		public static void RequestCharacterTableauRender(int characterCodeId, string path, GameEntity poseEntity, Camera cameraObject, int tableauType)
		{
			MBAPI.IMBBannerlordTableauManager.RequestCharacterTableauRender(characterCodeId, path, poseEntity.Pointer, cameraObject.Pointer, tableauType);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0004EE99 File Offset: 0x0004D099
		public static void ClearManager()
		{
			BannerlordTableauManager._tableauCharacterScenes = null;
			BannerlordTableauManager.RequestCallback = null;
			BannerlordTableauManager._isTableauRenderSystemInitialized = false;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0004EEAD File Offset: 0x0004D0AD
		public static void InitializeCharacterTableauRenderSystem()
		{
			if (!BannerlordTableauManager._isTableauRenderSystemInitialized)
			{
				MBAPI.IMBBannerlordTableauManager.InitializeCharacterTableauRenderSystem();
				BannerlordTableauManager._isTableauRenderSystemInitialized = true;
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0004EEC6 File Offset: 0x0004D0C6
		public static int GetNumberOfPendingTableauRequests()
		{
			return MBAPI.IMBBannerlordTableauManager.GetNumberOfPendingTableauRequests();
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0004EED2 File Offset: 0x0004D0D2
		[MBCallback]
		internal static void RequestCharacterTableauSetup(int characterCodeId, Scene scene, GameEntity poseEntity)
		{
			BannerlordTableauManager.RequestCallback(characterCodeId, scene, poseEntity);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0004EEE1 File Offset: 0x0004D0E1
		[MBCallback]
		internal static void RegisterCharacterTableauScene(Scene scene, int type)
		{
			BannerlordTableauManager.TableauCharacterScenes[type] = scene;
		}

		// Token: 0x040006ED RID: 1773
		private static Scene[] _tableauCharacterScenes = new Scene[5];

		// Token: 0x040006EE RID: 1774
		private static bool _isTableauRenderSystemInitialized = false;

		// Token: 0x040006EF RID: 1775
		public static BannerlordTableauManager.RequestCharacterTableauSetupDelegate RequestCallback;

		// Token: 0x020004BE RID: 1214
		// (Invoke) Token: 0x06003716 RID: 14102
		public delegate void RequestCharacterTableauSetupDelegate(int characterCodeId, Scene scene, GameEntity poseEntity);
	}
}
