using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000023 RID: 35
	internal class ScriptingInterfaceOfISceneView : ISceneView
	{
		// Token: 0x06000442 RID: 1090 RVA: 0x00014786 File Offset: 0x00012986
		public void AddClearTask(UIntPtr ptr, bool clearOnlySceneview)
		{
			ScriptingInterfaceOfISceneView.call_AddClearTaskDelegate(ptr, clearOnlySceneview);
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00014794 File Offset: 0x00012994
		public bool CheckSceneReadyToRender(UIntPtr ptr)
		{
			return ScriptingInterfaceOfISceneView.call_CheckSceneReadyToRenderDelegate(ptr);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x000147A1 File Offset: 0x000129A1
		public void ClearAll(UIntPtr pointer, bool clear_scene, bool remove_terrain)
		{
			ScriptingInterfaceOfISceneView.call_ClearAllDelegate(pointer, clear_scene, remove_terrain);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x000147B0 File Offset: 0x000129B0
		public SceneView CreateSceneView()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfISceneView.call_CreateSceneViewDelegate();
			SceneView result = NativeObject.CreateNativeObjectWrapper<SceneView>(nativeObjectPointer);
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000147F0 File Offset: 0x000129F0
		public void DoNotClear(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfISceneView.call_DoNotClearDelegate(pointer, value);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00014800 File Offset: 0x00012A00
		public Scene GetScene(UIntPtr ptr)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfISceneView.call_GetSceneDelegate(ptr);
			Scene result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Scene(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001484A File Offset: 0x00012A4A
		public bool ProjectedMousePositionOnGround(UIntPtr pointer, out Vec3 groundPosition, out Vec3 groundNormal, bool mouseVisible, BodyFlags excludeBodyOwnerFlags, bool checkOccludedSurface)
		{
			return ScriptingInterfaceOfISceneView.call_ProjectedMousePositionOnGroundDelegate(pointer, out groundPosition, out groundNormal, mouseVisible, excludeBodyOwnerFlags, checkOccludedSurface);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00014860 File Offset: 0x00012A60
		public bool RayCastForClosestEntityOrTerrain(UIntPtr ptr, ref Vec3 sourcePoint, ref Vec3 targetPoint, float rayThickness, ref float collisionDistance, ref Vec3 closestPoint, ref UIntPtr entityIndex, BodyFlags bodyExcludeFlags)
		{
			return ScriptingInterfaceOfISceneView.call_RayCastForClosestEntityOrTerrainDelegate(ptr, ref sourcePoint, ref targetPoint, rayThickness, ref collisionDistance, ref closestPoint, ref entityIndex, bodyExcludeFlags);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00014884 File Offset: 0x00012A84
		public bool ReadyToRender(UIntPtr pointer)
		{
			return ScriptingInterfaceOfISceneView.call_ReadyToRenderDelegate(pointer);
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00014891 File Offset: 0x00012A91
		public Vec2 ScreenPointToViewportPoint(UIntPtr ptr, float position_x, float position_y)
		{
			return ScriptingInterfaceOfISceneView.call_ScreenPointToViewportPointDelegate(ptr, position_x, position_y);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000148A0 File Offset: 0x00012AA0
		public void SetAcceptGlobalDebugRenderObjects(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetAcceptGlobalDebugRenderObjectsDelegate(ptr, value);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x000148AE File Offset: 0x00012AAE
		public void SetCamera(UIntPtr ptr, UIntPtr cameraPtr)
		{
			ScriptingInterfaceOfISceneView.call_SetCameraDelegate(ptr, cameraPtr);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x000148BC File Offset: 0x00012ABC
		public void SetCleanScreenUntilLoadingDone(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetCleanScreenUntilLoadingDoneDelegate(pointer, value);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x000148CA File Offset: 0x00012ACA
		public void SetClearAndDisableAfterSucessfullRender(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetClearAndDisableAfterSucessfullRenderDelegate(pointer, value);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000148D8 File Offset: 0x00012AD8
		public void SetClearGbuffer(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetClearGbufferDelegate(pointer, value);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000148E6 File Offset: 0x00012AE6
		public void SetFocusedShadowmap(UIntPtr ptr, bool enable, ref Vec3 center, float radius)
		{
			ScriptingInterfaceOfISceneView.call_SetFocusedShadowmapDelegate(ptr, enable, ref center, radius);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x000148F7 File Offset: 0x00012AF7
		public void SetForceShaderCompilation(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetForceShaderCompilationDelegate(ptr, value);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00014905 File Offset: 0x00012B05
		public void SetPointlightResolutionMultiplier(UIntPtr pointer, float value)
		{
			ScriptingInterfaceOfISceneView.call_SetPointlightResolutionMultiplierDelegate(pointer, value);
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00014913 File Offset: 0x00012B13
		public void SetPostfxConfigParams(UIntPtr ptr, int value)
		{
			ScriptingInterfaceOfISceneView.call_SetPostfxConfigParamsDelegate(ptr, value);
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x00014921 File Offset: 0x00012B21
		public void SetPostfxFromConfig(UIntPtr ptr)
		{
			ScriptingInterfaceOfISceneView.call_SetPostfxFromConfigDelegate(ptr);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001492E File Offset: 0x00012B2E
		public void SetRenderWithPostfx(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetRenderWithPostfxDelegate(ptr, value);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001493C File Offset: 0x00012B3C
		public void SetResolutionScaling(UIntPtr ptr, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetResolutionScalingDelegate(ptr, value);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001494A File Offset: 0x00012B4A
		public void SetScene(UIntPtr ptr, UIntPtr scenePtr)
		{
			ScriptingInterfaceOfISceneView.call_SetSceneDelegate(ptr, scenePtr);
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00014958 File Offset: 0x00012B58
		public void SetSceneUsesContour(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetSceneUsesContourDelegate(pointer, value);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00014966 File Offset: 0x00012B66
		public void SetSceneUsesShadows(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetSceneUsesShadowsDelegate(pointer, value);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00014974 File Offset: 0x00012B74
		public void SetSceneUsesSkybox(UIntPtr pointer, bool value)
		{
			ScriptingInterfaceOfISceneView.call_SetSceneUsesSkyboxDelegate(pointer, value);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00014982 File Offset: 0x00012B82
		public void SetShadowmapResolutionMultiplier(UIntPtr pointer, float value)
		{
			ScriptingInterfaceOfISceneView.call_SetShadowmapResolutionMultiplierDelegate(pointer, value);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00014990 File Offset: 0x00012B90
		public void TranslateMouse(UIntPtr pointer, ref Vec3 worldMouseNear, ref Vec3 worldMouseFar, float maxDistance)
		{
			ScriptingInterfaceOfISceneView.call_TranslateMouseDelegate(pointer, ref worldMouseNear, ref worldMouseFar, maxDistance);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000149A1 File Offset: 0x00012BA1
		public Vec2 WorldPointToScreenPoint(UIntPtr ptr, Vec3 position)
		{
			return ScriptingInterfaceOfISceneView.call_WorldPointToScreenPointDelegate(ptr, position);
		}

		// Token: 0x040003D4 RID: 980
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040003D5 RID: 981
		public static ScriptingInterfaceOfISceneView.AddClearTaskDelegate call_AddClearTaskDelegate;

		// Token: 0x040003D6 RID: 982
		public static ScriptingInterfaceOfISceneView.CheckSceneReadyToRenderDelegate call_CheckSceneReadyToRenderDelegate;

		// Token: 0x040003D7 RID: 983
		public static ScriptingInterfaceOfISceneView.ClearAllDelegate call_ClearAllDelegate;

		// Token: 0x040003D8 RID: 984
		public static ScriptingInterfaceOfISceneView.CreateSceneViewDelegate call_CreateSceneViewDelegate;

		// Token: 0x040003D9 RID: 985
		public static ScriptingInterfaceOfISceneView.DoNotClearDelegate call_DoNotClearDelegate;

		// Token: 0x040003DA RID: 986
		public static ScriptingInterfaceOfISceneView.GetSceneDelegate call_GetSceneDelegate;

		// Token: 0x040003DB RID: 987
		public static ScriptingInterfaceOfISceneView.ProjectedMousePositionOnGroundDelegate call_ProjectedMousePositionOnGroundDelegate;

		// Token: 0x040003DC RID: 988
		public static ScriptingInterfaceOfISceneView.RayCastForClosestEntityOrTerrainDelegate call_RayCastForClosestEntityOrTerrainDelegate;

		// Token: 0x040003DD RID: 989
		public static ScriptingInterfaceOfISceneView.ReadyToRenderDelegate call_ReadyToRenderDelegate;

		// Token: 0x040003DE RID: 990
		public static ScriptingInterfaceOfISceneView.ScreenPointToViewportPointDelegate call_ScreenPointToViewportPointDelegate;

		// Token: 0x040003DF RID: 991
		public static ScriptingInterfaceOfISceneView.SetAcceptGlobalDebugRenderObjectsDelegate call_SetAcceptGlobalDebugRenderObjectsDelegate;

		// Token: 0x040003E0 RID: 992
		public static ScriptingInterfaceOfISceneView.SetCameraDelegate call_SetCameraDelegate;

		// Token: 0x040003E1 RID: 993
		public static ScriptingInterfaceOfISceneView.SetCleanScreenUntilLoadingDoneDelegate call_SetCleanScreenUntilLoadingDoneDelegate;

		// Token: 0x040003E2 RID: 994
		public static ScriptingInterfaceOfISceneView.SetClearAndDisableAfterSucessfullRenderDelegate call_SetClearAndDisableAfterSucessfullRenderDelegate;

		// Token: 0x040003E3 RID: 995
		public static ScriptingInterfaceOfISceneView.SetClearGbufferDelegate call_SetClearGbufferDelegate;

		// Token: 0x040003E4 RID: 996
		public static ScriptingInterfaceOfISceneView.SetFocusedShadowmapDelegate call_SetFocusedShadowmapDelegate;

		// Token: 0x040003E5 RID: 997
		public static ScriptingInterfaceOfISceneView.SetForceShaderCompilationDelegate call_SetForceShaderCompilationDelegate;

		// Token: 0x040003E6 RID: 998
		public static ScriptingInterfaceOfISceneView.SetPointlightResolutionMultiplierDelegate call_SetPointlightResolutionMultiplierDelegate;

		// Token: 0x040003E7 RID: 999
		public static ScriptingInterfaceOfISceneView.SetPostfxConfigParamsDelegate call_SetPostfxConfigParamsDelegate;

		// Token: 0x040003E8 RID: 1000
		public static ScriptingInterfaceOfISceneView.SetPostfxFromConfigDelegate call_SetPostfxFromConfigDelegate;

		// Token: 0x040003E9 RID: 1001
		public static ScriptingInterfaceOfISceneView.SetRenderWithPostfxDelegate call_SetRenderWithPostfxDelegate;

		// Token: 0x040003EA RID: 1002
		public static ScriptingInterfaceOfISceneView.SetResolutionScalingDelegate call_SetResolutionScalingDelegate;

		// Token: 0x040003EB RID: 1003
		public static ScriptingInterfaceOfISceneView.SetSceneDelegate call_SetSceneDelegate;

		// Token: 0x040003EC RID: 1004
		public static ScriptingInterfaceOfISceneView.SetSceneUsesContourDelegate call_SetSceneUsesContourDelegate;

		// Token: 0x040003ED RID: 1005
		public static ScriptingInterfaceOfISceneView.SetSceneUsesShadowsDelegate call_SetSceneUsesShadowsDelegate;

		// Token: 0x040003EE RID: 1006
		public static ScriptingInterfaceOfISceneView.SetSceneUsesSkyboxDelegate call_SetSceneUsesSkyboxDelegate;

		// Token: 0x040003EF RID: 1007
		public static ScriptingInterfaceOfISceneView.SetShadowmapResolutionMultiplierDelegate call_SetShadowmapResolutionMultiplierDelegate;

		// Token: 0x040003F0 RID: 1008
		public static ScriptingInterfaceOfISceneView.TranslateMouseDelegate call_TranslateMouseDelegate;

		// Token: 0x040003F1 RID: 1009
		public static ScriptingInterfaceOfISceneView.WorldPointToScreenPointDelegate call_WorldPointToScreenPointDelegate;

		// Token: 0x0200042B RID: 1067
		// (Invoke) Token: 0x060015AF RID: 5551
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddClearTaskDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool clearOnlySceneview);

		// Token: 0x0200042C RID: 1068
		// (Invoke) Token: 0x060015B3 RID: 5555
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckSceneReadyToRenderDelegate(UIntPtr ptr);

		// Token: 0x0200042D RID: 1069
		// (Invoke) Token: 0x060015B7 RID: 5559
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearAllDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool clear_scene, [MarshalAs(UnmanagedType.U1)] bool remove_terrain);

		// Token: 0x0200042E RID: 1070
		// (Invoke) Token: 0x060015BB RID: 5563
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateSceneViewDelegate();

		// Token: 0x0200042F RID: 1071
		// (Invoke) Token: 0x060015BF RID: 5567
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void DoNotClearDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000430 RID: 1072
		// (Invoke) Token: 0x060015C3 RID: 5571
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetSceneDelegate(UIntPtr ptr);

		// Token: 0x02000431 RID: 1073
		// (Invoke) Token: 0x060015C7 RID: 5575
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ProjectedMousePositionOnGroundDelegate(UIntPtr pointer, out Vec3 groundPosition, out Vec3 groundNormal, [MarshalAs(UnmanagedType.U1)] bool mouseVisible, BodyFlags excludeBodyOwnerFlags, [MarshalAs(UnmanagedType.U1)] bool checkOccludedSurface);

		// Token: 0x02000432 RID: 1074
		// (Invoke) Token: 0x060015CB RID: 5579
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool RayCastForClosestEntityOrTerrainDelegate(UIntPtr ptr, ref Vec3 sourcePoint, ref Vec3 targetPoint, float rayThickness, ref float collisionDistance, ref Vec3 closestPoint, ref UIntPtr entityIndex, BodyFlags bodyExcludeFlags);

		// Token: 0x02000433 RID: 1075
		// (Invoke) Token: 0x060015CF RID: 5583
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ReadyToRenderDelegate(UIntPtr pointer);

		// Token: 0x02000434 RID: 1076
		// (Invoke) Token: 0x060015D3 RID: 5587
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec2 ScreenPointToViewportPointDelegate(UIntPtr ptr, float position_x, float position_y);

		// Token: 0x02000435 RID: 1077
		// (Invoke) Token: 0x060015D7 RID: 5591
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAcceptGlobalDebugRenderObjectsDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000436 RID: 1078
		// (Invoke) Token: 0x060015DB RID: 5595
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCameraDelegate(UIntPtr ptr, UIntPtr cameraPtr);

		// Token: 0x02000437 RID: 1079
		// (Invoke) Token: 0x060015DF RID: 5599
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetCleanScreenUntilLoadingDoneDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000438 RID: 1080
		// (Invoke) Token: 0x060015E3 RID: 5603
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetClearAndDisableAfterSucessfullRenderDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000439 RID: 1081
		// (Invoke) Token: 0x060015E7 RID: 5607
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetClearGbufferDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x0200043A RID: 1082
		// (Invoke) Token: 0x060015EB RID: 5611
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFocusedShadowmapDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool enable, ref Vec3 center, float radius);

		// Token: 0x0200043B RID: 1083
		// (Invoke) Token: 0x060015EF RID: 5615
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetForceShaderCompilationDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x0200043C RID: 1084
		// (Invoke) Token: 0x060015F3 RID: 5619
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPointlightResolutionMultiplierDelegate(UIntPtr pointer, float value);

		// Token: 0x0200043D RID: 1085
		// (Invoke) Token: 0x060015F7 RID: 5623
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPostfxConfigParamsDelegate(UIntPtr ptr, int value);

		// Token: 0x0200043E RID: 1086
		// (Invoke) Token: 0x060015FB RID: 5627
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPostfxFromConfigDelegate(UIntPtr ptr);

		// Token: 0x0200043F RID: 1087
		// (Invoke) Token: 0x060015FF RID: 5631
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRenderWithPostfxDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000440 RID: 1088
		// (Invoke) Token: 0x06001603 RID: 5635
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetResolutionScalingDelegate(UIntPtr ptr, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000441 RID: 1089
		// (Invoke) Token: 0x06001607 RID: 5639
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSceneDelegate(UIntPtr ptr, UIntPtr scenePtr);

		// Token: 0x02000442 RID: 1090
		// (Invoke) Token: 0x0600160B RID: 5643
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSceneUsesContourDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000443 RID: 1091
		// (Invoke) Token: 0x0600160F RID: 5647
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSceneUsesShadowsDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000444 RID: 1092
		// (Invoke) Token: 0x06001613 RID: 5651
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSceneUsesSkyboxDelegate(UIntPtr pointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x02000445 RID: 1093
		// (Invoke) Token: 0x06001617 RID: 5655
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetShadowmapResolutionMultiplierDelegate(UIntPtr pointer, float value);

		// Token: 0x02000446 RID: 1094
		// (Invoke) Token: 0x0600161B RID: 5659
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TranslateMouseDelegate(UIntPtr pointer, ref Vec3 worldMouseNear, ref Vec3 worldMouseFar, float maxDistance);

		// Token: 0x02000447 RID: 1095
		// (Invoke) Token: 0x0600161F RID: 5663
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec2 WorldPointToScreenPointDelegate(UIntPtr ptr, Vec3 position);
	}
}
