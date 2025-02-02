using System;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001C9 RID: 457
	public static class MBMapScene
	{
		// Token: 0x06001A2C RID: 6700 RVA: 0x0005C55C File Offset: 0x0005A75C
		public static Vec2 GetAccessiblePointNearPosition(Scene mapScene, Vec2 position, float radius)
		{
			return MBAPI.IMBMapScene.GetAccessiblePointNearPosition(mapScene.Pointer, position, radius).AsVec2;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0005C583 File Offset: 0x0005A783
		public static void RemoveZeroCornerBodies(Scene mapScene)
		{
			MBAPI.IMBMapScene.RemoveZeroCornerBodies(mapScene.Pointer);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0005C595 File Offset: 0x0005A795
		public static void LoadAtmosphereData(Scene mapScene)
		{
			MBAPI.IMBMapScene.LoadAtmosphereData(mapScene.Pointer);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0005C5A7 File Offset: 0x0005A7A7
		public static void GetFaceIndexForMultiplePositions(Scene mapScene, int movedPartyCount, float[] positionArray, PathFaceRecord[] resultArray, bool check_if_disabled, bool check_height)
		{
			MBAPI.IMBMapScene.GetFaceIndexForMultiplePositions(mapScene.Pointer, movedPartyCount, positionArray, resultArray, check_if_disabled, check_height);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0005C5C0 File Offset: 0x0005A7C0
		public static void TickStepSound(Scene mapScene, MBAgentVisuals visuals, int terrainType, int soundType)
		{
			MBAPI.IMBMapScene.TickStepSound(mapScene.Pointer, visuals.Pointer, terrainType, soundType);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0005C5DA File Offset: 0x0005A7DA
		public static void TickAmbientSounds(Scene mapScene, int terrainType)
		{
			MBAPI.IMBMapScene.TickAmbientSounds(mapScene.Pointer, terrainType);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0005C5ED File Offset: 0x0005A7ED
		public static bool GetMouseVisible()
		{
			return MBAPI.IMBMapScene.GetMouseVisible();
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0005C5F9 File Offset: 0x0005A7F9
		public static bool GetApplyRainColorGrade()
		{
			return MBMapScene.ApplyRainColorGrade;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0005C600 File Offset: 0x0005A800
		public static void SendMouseKeyEvent(int mouseKeyId, bool isDown)
		{
			MBAPI.IMBMapScene.SendMouseKeyEvent(mouseKeyId, isDown);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0005C60E File Offset: 0x0005A80E
		public static void SetMousePos(int posX, int posY)
		{
			MBAPI.IMBMapScene.SetMousePos(posX, posY);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0005C61C File Offset: 0x0005A81C
		public static void TickVisuals(Scene mapScene, float tod, Mesh[] tickedMapMeshes)
		{
			for (int i = 0; i < tickedMapMeshes.Length; i++)
			{
				MBMapScene._tickedMapMeshesCachedArray[i] = tickedMapMeshes[i].Pointer;
			}
			MBAPI.IMBMapScene.TickVisuals(mapScene.Pointer, tod, MBMapScene._tickedMapMeshesCachedArray, tickedMapMeshes.Length);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0005C65F File Offset: 0x0005A85F
		public static void ValidateTerrainSoundIds()
		{
			MBAPI.IMBMapScene.ValidateTerrainSoundIds();
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0005C66B File Offset: 0x0005A86B
		public static void GetGlobalIlluminationOfString(Scene mapScene, string value)
		{
			MBAPI.IMBMapScene.SetPoliticalColor(mapScene.Pointer, value);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0005C67E File Offset: 0x0005A87E
		public static void GetColorGradeGridData(Scene mapScene, byte[] gridData, string textureName)
		{
			MBAPI.IMBMapScene.GetColorGradeGridData(mapScene.Pointer, gridData, textureName);
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0005C694 File Offset: 0x0005A894
		public static void GetBattleSceneIndexMap(Scene mapScene, ref byte[] indexData, ref int width, ref int height)
		{
			MBAPI.IMBMapScene.GetBattleSceneIndexMapResolution(mapScene.Pointer, ref width, ref height);
			int num = width * height * 2;
			if (indexData == null || indexData.Length != num)
			{
				indexData = new byte[num];
			}
			MBAPI.IMBMapScene.GetBattleSceneIndexMap(mapScene.Pointer, indexData);
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0005C6E0 File Offset: 0x0005A8E0
		public static void SetFrameForAtmosphere(Scene mapScene, float tod, float cameraElevation, bool forceLoadTextures)
		{
			MBAPI.IMBMapScene.SetFrameForAtmosphere(mapScene.Pointer, tod, cameraElevation, forceLoadTextures);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0005C6F5 File Offset: 0x0005A8F5
		public static void SetTerrainDynamicParams(Scene mapScene, Vec3 dynamic_params)
		{
			MBAPI.IMBMapScene.SetTerrainDynamicParams(mapScene.Pointer, dynamic_params);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0005C708 File Offset: 0x0005A908
		public static void SetSeasonTimeFactor(Scene mapScene, float seasonTimeFactor)
		{
			MBAPI.IMBMapScene.SetSeasonTimeFactor(mapScene.Pointer, seasonTimeFactor);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0005C71B File Offset: 0x0005A91B
		public static float GetSeasonTimeFactor(Scene mapScene)
		{
			return MBAPI.IMBMapScene.GetSeasonTimeFactor(mapScene.Pointer);
		}

		// Token: 0x040007F5 RID: 2037
		public static bool ApplyRainColorGrade;

		// Token: 0x040007F6 RID: 2038
		private static UIntPtr[] _tickedMapMeshesCachedArray = new UIntPtr[1024];
	}
}
