using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000017 RID: 23
	internal class ScriptingInterfaceOfIMBMapScene : IMBMapScene
	{
		// Token: 0x06000224 RID: 548 RVA: 0x0000B042 File Offset: 0x00009242
		public Vec3 GetAccessiblePointNearPosition(UIntPtr scenePointer, Vec2 position, float radius)
		{
			return ScriptingInterfaceOfIMBMapScene.call_GetAccessiblePointNearPositionDelegate(scenePointer, position, radius);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000B054 File Offset: 0x00009254
		public void GetBattleSceneIndexMap(UIntPtr scenePointer, byte[] indexData)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(indexData, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray indexData2 = new ManagedArray(pointer, (indexData != null) ? indexData.Length : 0);
			ScriptingInterfaceOfIMBMapScene.call_GetBattleSceneIndexMapDelegate(scenePointer, indexData2);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000B096 File Offset: 0x00009296
		public void GetBattleSceneIndexMapResolution(UIntPtr scenePointer, ref int width, ref int height)
		{
			ScriptingInterfaceOfIMBMapScene.call_GetBattleSceneIndexMapResolutionDelegate(scenePointer, ref width, ref height);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000B0A8 File Offset: 0x000092A8
		public void GetColorGradeGridData(UIntPtr scenePointer, byte[] snowData, string textureName)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(snowData, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray snowData2 = new ManagedArray(pointer, (snowData != null) ? snowData.Length : 0);
			byte[] array = null;
			if (textureName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBMapScene._utf8.GetByteCount(textureName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBMapScene._utf8.GetBytes(textureName, 0, textureName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBMapScene.call_GetColorGradeGridDataDelegate(scenePointer, snowData2, array);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000B134 File Offset: 0x00009334
		public void GetFaceIndexForMultiplePositions(UIntPtr scenePointer, int movedPartyCount, float[] positionArray, PathFaceRecord[] resultArray, bool check_if_disabled, bool check_height)
		{
			PinnedArrayData<float> pinnedArrayData = new PinnedArrayData<float>(positionArray, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			PinnedArrayData<PathFaceRecord> pinnedArrayData2 = new PinnedArrayData<PathFaceRecord>(resultArray, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			ScriptingInterfaceOfIMBMapScene.call_GetFaceIndexForMultiplePositionsDelegate(scenePointer, movedPartyCount, pointer, pointer2, check_if_disabled, check_height);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000B184 File Offset: 0x00009384
		public bool GetMouseVisible()
		{
			return ScriptingInterfaceOfIMBMapScene.call_GetMouseVisibleDelegate();
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000B190 File Offset: 0x00009390
		public float GetSeasonTimeFactor(UIntPtr scenePointer)
		{
			return ScriptingInterfaceOfIMBMapScene.call_GetSeasonTimeFactorDelegate(scenePointer);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000B19D File Offset: 0x0000939D
		public void LoadAtmosphereData(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIMBMapScene.call_LoadAtmosphereDataDelegate(scenePointer);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000B1AA File Offset: 0x000093AA
		public void RemoveZeroCornerBodies(UIntPtr scenePointer)
		{
			ScriptingInterfaceOfIMBMapScene.call_RemoveZeroCornerBodiesDelegate(scenePointer);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000B1B7 File Offset: 0x000093B7
		public void SendMouseKeyEvent(int keyId, bool isDown)
		{
			ScriptingInterfaceOfIMBMapScene.call_SendMouseKeyEventDelegate(keyId, isDown);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000B1C5 File Offset: 0x000093C5
		public void SetFrameForAtmosphere(UIntPtr scenePointer, float tod, float cameraElevation, bool forceLoadTextures)
		{
			ScriptingInterfaceOfIMBMapScene.call_SetFrameForAtmosphereDelegate(scenePointer, tod, cameraElevation, forceLoadTextures);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000B1D6 File Offset: 0x000093D6
		public void SetMousePos(int posX, int posY)
		{
			ScriptingInterfaceOfIMBMapScene.call_SetMousePosDelegate(posX, posY);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000B1E4 File Offset: 0x000093E4
		public void SetMouseVisible(bool value)
		{
			ScriptingInterfaceOfIMBMapScene.call_SetMouseVisibleDelegate(value);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000B1F4 File Offset: 0x000093F4
		public void SetPoliticalColor(UIntPtr scenePointer, string value)
		{
			byte[] array = null;
			if (value != null)
			{
				int byteCount = ScriptingInterfaceOfIMBMapScene._utf8.GetByteCount(value);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBMapScene._utf8.GetBytes(value, 0, value.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBMapScene.call_SetPoliticalColorDelegate(scenePointer, array);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000B24F File Offset: 0x0000944F
		public void SetSeasonTimeFactor(UIntPtr scenePointer, float seasonTimeFactor)
		{
			ScriptingInterfaceOfIMBMapScene.call_SetSeasonTimeFactorDelegate(scenePointer, seasonTimeFactor);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000B25D File Offset: 0x0000945D
		public void SetTerrainDynamicParams(UIntPtr scenePointer, Vec3 dynamic_params)
		{
			ScriptingInterfaceOfIMBMapScene.call_SetTerrainDynamicParamsDelegate(scenePointer, dynamic_params);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000B26B File Offset: 0x0000946B
		public void TickAmbientSounds(UIntPtr scenePointer, int terrainType)
		{
			ScriptingInterfaceOfIMBMapScene.call_TickAmbientSoundsDelegate(scenePointer, terrainType);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000B279 File Offset: 0x00009479
		public void TickStepSound(UIntPtr scenePointer, UIntPtr visualsPointer, int faceIndexterrainType, int soundType)
		{
			ScriptingInterfaceOfIMBMapScene.call_TickStepSoundDelegate(scenePointer, visualsPointer, faceIndexterrainType, soundType);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000B28C File Offset: 0x0000948C
		public void TickVisuals(UIntPtr scenePointer, float tod, UIntPtr[] ticked_map_meshes, int tickedMapMeshesCount)
		{
			PinnedArrayData<UIntPtr> pinnedArrayData = new PinnedArrayData<UIntPtr>(ticked_map_meshes, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIMBMapScene.call_TickVisualsDelegate(scenePointer, tod, pointer, tickedMapMeshesCount);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000B2C0 File Offset: 0x000094C0
		public void ValidateTerrainSoundIds()
		{
			ScriptingInterfaceOfIMBMapScene.call_ValidateTerrainSoundIdsDelegate();
		}

		// Token: 0x040001B2 RID: 434
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040001B3 RID: 435
		public static ScriptingInterfaceOfIMBMapScene.GetAccessiblePointNearPositionDelegate call_GetAccessiblePointNearPositionDelegate;

		// Token: 0x040001B4 RID: 436
		public static ScriptingInterfaceOfIMBMapScene.GetBattleSceneIndexMapDelegate call_GetBattleSceneIndexMapDelegate;

		// Token: 0x040001B5 RID: 437
		public static ScriptingInterfaceOfIMBMapScene.GetBattleSceneIndexMapResolutionDelegate call_GetBattleSceneIndexMapResolutionDelegate;

		// Token: 0x040001B6 RID: 438
		public static ScriptingInterfaceOfIMBMapScene.GetColorGradeGridDataDelegate call_GetColorGradeGridDataDelegate;

		// Token: 0x040001B7 RID: 439
		public static ScriptingInterfaceOfIMBMapScene.GetFaceIndexForMultiplePositionsDelegate call_GetFaceIndexForMultiplePositionsDelegate;

		// Token: 0x040001B8 RID: 440
		public static ScriptingInterfaceOfIMBMapScene.GetMouseVisibleDelegate call_GetMouseVisibleDelegate;

		// Token: 0x040001B9 RID: 441
		public static ScriptingInterfaceOfIMBMapScene.GetSeasonTimeFactorDelegate call_GetSeasonTimeFactorDelegate;

		// Token: 0x040001BA RID: 442
		public static ScriptingInterfaceOfIMBMapScene.LoadAtmosphereDataDelegate call_LoadAtmosphereDataDelegate;

		// Token: 0x040001BB RID: 443
		public static ScriptingInterfaceOfIMBMapScene.RemoveZeroCornerBodiesDelegate call_RemoveZeroCornerBodiesDelegate;

		// Token: 0x040001BC RID: 444
		public static ScriptingInterfaceOfIMBMapScene.SendMouseKeyEventDelegate call_SendMouseKeyEventDelegate;

		// Token: 0x040001BD RID: 445
		public static ScriptingInterfaceOfIMBMapScene.SetFrameForAtmosphereDelegate call_SetFrameForAtmosphereDelegate;

		// Token: 0x040001BE RID: 446
		public static ScriptingInterfaceOfIMBMapScene.SetMousePosDelegate call_SetMousePosDelegate;

		// Token: 0x040001BF RID: 447
		public static ScriptingInterfaceOfIMBMapScene.SetMouseVisibleDelegate call_SetMouseVisibleDelegate;

		// Token: 0x040001C0 RID: 448
		public static ScriptingInterfaceOfIMBMapScene.SetPoliticalColorDelegate call_SetPoliticalColorDelegate;

		// Token: 0x040001C1 RID: 449
		public static ScriptingInterfaceOfIMBMapScene.SetSeasonTimeFactorDelegate call_SetSeasonTimeFactorDelegate;

		// Token: 0x040001C2 RID: 450
		public static ScriptingInterfaceOfIMBMapScene.SetTerrainDynamicParamsDelegate call_SetTerrainDynamicParamsDelegate;

		// Token: 0x040001C3 RID: 451
		public static ScriptingInterfaceOfIMBMapScene.TickAmbientSoundsDelegate call_TickAmbientSoundsDelegate;

		// Token: 0x040001C4 RID: 452
		public static ScriptingInterfaceOfIMBMapScene.TickStepSoundDelegate call_TickStepSoundDelegate;

		// Token: 0x040001C5 RID: 453
		public static ScriptingInterfaceOfIMBMapScene.TickVisualsDelegate call_TickVisualsDelegate;

		// Token: 0x040001C6 RID: 454
		public static ScriptingInterfaceOfIMBMapScene.ValidateTerrainSoundIdsDelegate call_ValidateTerrainSoundIdsDelegate;

		// Token: 0x0200020F RID: 527
		// (Invoke) Token: 0x06000AE1 RID: 2785
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetAccessiblePointNearPositionDelegate(UIntPtr scenePointer, Vec2 position, float radius);

		// Token: 0x02000210 RID: 528
		// (Invoke) Token: 0x06000AE5 RID: 2789
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBattleSceneIndexMapDelegate(UIntPtr scenePointer, ManagedArray indexData);

		// Token: 0x02000211 RID: 529
		// (Invoke) Token: 0x06000AE9 RID: 2793
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBattleSceneIndexMapResolutionDelegate(UIntPtr scenePointer, ref int width, ref int height);

		// Token: 0x02000212 RID: 530
		// (Invoke) Token: 0x06000AED RID: 2797
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetColorGradeGridDataDelegate(UIntPtr scenePointer, ManagedArray snowData, byte[] textureName);

		// Token: 0x02000213 RID: 531
		// (Invoke) Token: 0x06000AF1 RID: 2801
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetFaceIndexForMultiplePositionsDelegate(UIntPtr scenePointer, int movedPartyCount, IntPtr positionArray, IntPtr resultArray, [MarshalAs(UnmanagedType.U1)] bool check_if_disabled, [MarshalAs(UnmanagedType.U1)] bool check_height);

		// Token: 0x02000214 RID: 532
		// (Invoke) Token: 0x06000AF5 RID: 2805
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetMouseVisibleDelegate();

		// Token: 0x02000215 RID: 533
		// (Invoke) Token: 0x06000AF9 RID: 2809
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetSeasonTimeFactorDelegate(UIntPtr scenePointer);

		// Token: 0x02000216 RID: 534
		// (Invoke) Token: 0x06000AFD RID: 2813
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LoadAtmosphereDataDelegate(UIntPtr scenePointer);

		// Token: 0x02000217 RID: 535
		// (Invoke) Token: 0x06000B01 RID: 2817
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveZeroCornerBodiesDelegate(UIntPtr scenePointer);

		// Token: 0x02000218 RID: 536
		// (Invoke) Token: 0x06000B05 RID: 2821
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SendMouseKeyEventDelegate(int keyId, [MarshalAs(UnmanagedType.U1)] bool isDown);

		// Token: 0x02000219 RID: 537
		// (Invoke) Token: 0x06000B09 RID: 2825
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameForAtmosphereDelegate(UIntPtr scenePointer, float tod, float cameraElevation, [MarshalAs(UnmanagedType.U1)] bool forceLoadTextures);

		// Token: 0x0200021A RID: 538
		// (Invoke) Token: 0x06000B0D RID: 2829
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMousePosDelegate(int posX, int posY);

		// Token: 0x0200021B RID: 539
		// (Invoke) Token: 0x06000B11 RID: 2833
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMouseVisibleDelegate([MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x0200021C RID: 540
		// (Invoke) Token: 0x06000B15 RID: 2837
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPoliticalColorDelegate(UIntPtr scenePointer, byte[] value);

		// Token: 0x0200021D RID: 541
		// (Invoke) Token: 0x06000B19 RID: 2841
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSeasonTimeFactorDelegate(UIntPtr scenePointer, float seasonTimeFactor);

		// Token: 0x0200021E RID: 542
		// (Invoke) Token: 0x06000B1D RID: 2845
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTerrainDynamicParamsDelegate(UIntPtr scenePointer, Vec3 dynamic_params);

		// Token: 0x0200021F RID: 543
		// (Invoke) Token: 0x06000B21 RID: 2849
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TickAmbientSoundsDelegate(UIntPtr scenePointer, int terrainType);

		// Token: 0x02000220 RID: 544
		// (Invoke) Token: 0x06000B25 RID: 2853
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TickStepSoundDelegate(UIntPtr scenePointer, UIntPtr visualsPointer, int faceIndexterrainType, int soundType);

		// Token: 0x02000221 RID: 545
		// (Invoke) Token: 0x06000B29 RID: 2857
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TickVisualsDelegate(UIntPtr scenePointer, float tod, IntPtr ticked_map_meshes, int tickedMapMeshesCount);

		// Token: 0x02000222 RID: 546
		// (Invoke) Token: 0x06000B2D RID: 2861
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ValidateTerrainSoundIdsDelegate();
	}
}
