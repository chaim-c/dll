using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200000A RID: 10
	internal class ScriptingInterfaceOfICamera : ICamera
	{
		// Token: 0x0600005C RID: 92 RVA: 0x0000CEEB File Offset: 0x0000B0EB
		public bool CheckEntityVisibility(UIntPtr cameraPointer, UIntPtr entityPointer)
		{
			return ScriptingInterfaceOfICamera.call_CheckEntityVisibilityDelegate(cameraPointer, entityPointer);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000CEF9 File Offset: 0x0000B0F9
		public void ConstructCameraFromPositionElevationBearing(Vec3 position, float elevation, float bearing, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfICamera.call_ConstructCameraFromPositionElevationBearingDelegate(position, elevation, bearing, ref outFrame);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000CF0C File Offset: 0x0000B10C
		public Camera CreateCamera()
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfICamera.call_CreateCameraDelegate();
			Camera result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Camera(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000CF55 File Offset: 0x0000B155
		public bool EnclosesPoint(UIntPtr cameraPointer, Vec3 pointInWorldSpace)
		{
			return ScriptingInterfaceOfICamera.call_EnclosesPointDelegate(cameraPointer, pointInWorldSpace);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000CF63 File Offset: 0x0000B163
		public void FillParametersFrom(UIntPtr cameraPointer, UIntPtr otherCameraPointer)
		{
			ScriptingInterfaceOfICamera.call_FillParametersFromDelegate(cameraPointer, otherCameraPointer);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000CF71 File Offset: 0x0000B171
		public float GetAspectRatio(UIntPtr cameraPointer)
		{
			return ScriptingInterfaceOfICamera.call_GetAspectRatioDelegate(cameraPointer);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000CF80 File Offset: 0x0000B180
		public GameEntity GetEntity(UIntPtr cameraPointer)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfICamera.call_GetEntityDelegate(cameraPointer);
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000CFCA File Offset: 0x0000B1CA
		public float GetFar(UIntPtr cameraPointer)
		{
			return ScriptingInterfaceOfICamera.call_GetFarDelegate(cameraPointer);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000CFD7 File Offset: 0x0000B1D7
		public float GetFovHorizontal(UIntPtr cameraPointer)
		{
			return ScriptingInterfaceOfICamera.call_GetFovHorizontalDelegate(cameraPointer);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		public float GetFovVertical(UIntPtr cameraPointer)
		{
			return ScriptingInterfaceOfICamera.call_GetFovVerticalDelegate(cameraPointer);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000CFF1 File Offset: 0x0000B1F1
		public void GetFrame(UIntPtr cameraPointer, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfICamera.call_GetFrameDelegate(cameraPointer, ref outFrame);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000CFFF File Offset: 0x0000B1FF
		public float GetHorizontalFov(UIntPtr cameraPointer)
		{
			return ScriptingInterfaceOfICamera.call_GetHorizontalFovDelegate(cameraPointer);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000D00C File Offset: 0x0000B20C
		public float GetNear(UIntPtr cameraPointer)
		{
			return ScriptingInterfaceOfICamera.call_GetNearDelegate(cameraPointer);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000D01C File Offset: 0x0000B21C
		public void GetNearPlanePoints(UIntPtr cameraPointer, Vec3[] nearPlanePoints)
		{
			PinnedArrayData<Vec3> pinnedArrayData = new PinnedArrayData<Vec3>(nearPlanePoints, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfICamera.call_GetNearPlanePointsDelegate(cameraPointer, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000D050 File Offset: 0x0000B250
		public void GetNearPlanePointsStatic(ref MatrixFrame cameraFrame, float verticalFov, float aspectRatioXY, float newDNear, float newDFar, Vec3[] nearPlanePoints)
		{
			PinnedArrayData<Vec3> pinnedArrayData = new PinnedArrayData<Vec3>(nearPlanePoints, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfICamera.call_GetNearPlanePointsStaticDelegate(ref cameraFrame, verticalFov, aspectRatioXY, newDNear, newDFar, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000D088 File Offset: 0x0000B288
		public void GetViewProjMatrix(UIntPtr cameraPointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfICamera.call_GetViewProjMatrixDelegate(cameraPointer, ref frame);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000D096 File Offset: 0x0000B296
		public void LookAt(UIntPtr cameraPointer, Vec3 position, Vec3 target, Vec3 upVector)
		{
			ScriptingInterfaceOfICamera.call_LookAtDelegate(cameraPointer, position, target, upVector);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000D0A7 File Offset: 0x0000B2A7
		public void Release(UIntPtr cameraPointer)
		{
			ScriptingInterfaceOfICamera.call_ReleaseDelegate(cameraPointer);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		public void ReleaseCameraEntity(UIntPtr cameraPointer)
		{
			ScriptingInterfaceOfICamera.call_ReleaseCameraEntityDelegate(cameraPointer);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000D0C1 File Offset: 0x0000B2C1
		public void RenderFrustrum(UIntPtr cameraPointer)
		{
			ScriptingInterfaceOfICamera.call_RenderFrustrumDelegate(cameraPointer);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000D0CE File Offset: 0x0000B2CE
		public void ScreenSpaceRayProjection(UIntPtr cameraPointer, Vec2 screenPosition, ref Vec3 rayBegin, ref Vec3 rayEnd)
		{
			ScriptingInterfaceOfICamera.call_ScreenSpaceRayProjectionDelegate(cameraPointer, screenPosition, ref rayBegin, ref rayEnd);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000D0DF File Offset: 0x0000B2DF
		public void SetEntity(UIntPtr cameraPointer, UIntPtr entityId)
		{
			ScriptingInterfaceOfICamera.call_SetEntityDelegate(cameraPointer, entityId);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000D0ED File Offset: 0x0000B2ED
		public void SetFovHorizontal(UIntPtr cameraPointer, float horizontalFov, float aspectRatio, float newDNear, float newDFar)
		{
			ScriptingInterfaceOfICamera.call_SetFovHorizontalDelegate(cameraPointer, horizontalFov, aspectRatio, newDNear, newDFar);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000D100 File Offset: 0x0000B300
		public void SetFovVertical(UIntPtr cameraPointer, float verticalFov, float aspectRatio, float newDNear, float newDFar)
		{
			ScriptingInterfaceOfICamera.call_SetFovVerticalDelegate(cameraPointer, verticalFov, aspectRatio, newDNear, newDFar);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000D113 File Offset: 0x0000B313
		public void SetFrame(UIntPtr cameraPointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfICamera.call_SetFrameDelegate(cameraPointer, ref frame);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000D121 File Offset: 0x0000B321
		public void SetPosition(UIntPtr cameraPointer, Vec3 position)
		{
			ScriptingInterfaceOfICamera.call_SetPositionDelegate(cameraPointer, position);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000D130 File Offset: 0x0000B330
		public void SetViewVolume(UIntPtr cameraPointer, bool perspective, float dLeft, float dRight, float dBottom, float dTop, float dNear, float dFar)
		{
			ScriptingInterfaceOfICamera.call_SetViewVolumeDelegate(cameraPointer, perspective, dLeft, dRight, dBottom, dTop, dNear, dFar);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000D154 File Offset: 0x0000B354
		public void ViewportPointToWorldRay(UIntPtr cameraPointer, ref Vec3 rayBegin, ref Vec3 rayEnd, Vec3 viewportPoint)
		{
			ScriptingInterfaceOfICamera.call_ViewportPointToWorldRayDelegate(cameraPointer, ref rayBegin, ref rayEnd, viewportPoint);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000D165 File Offset: 0x0000B365
		public Vec3 WorldPointToViewportPoint(UIntPtr cameraPointer, ref Vec3 worldPoint)
		{
			return ScriptingInterfaceOfICamera.call_WorldPointToViewportPointDelegate(cameraPointer, ref worldPoint);
		}

		// Token: 0x04000008 RID: 8
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000009 RID: 9
		public static ScriptingInterfaceOfICamera.CheckEntityVisibilityDelegate call_CheckEntityVisibilityDelegate;

		// Token: 0x0400000A RID: 10
		public static ScriptingInterfaceOfICamera.ConstructCameraFromPositionElevationBearingDelegate call_ConstructCameraFromPositionElevationBearingDelegate;

		// Token: 0x0400000B RID: 11
		public static ScriptingInterfaceOfICamera.CreateCameraDelegate call_CreateCameraDelegate;

		// Token: 0x0400000C RID: 12
		public static ScriptingInterfaceOfICamera.EnclosesPointDelegate call_EnclosesPointDelegate;

		// Token: 0x0400000D RID: 13
		public static ScriptingInterfaceOfICamera.FillParametersFromDelegate call_FillParametersFromDelegate;

		// Token: 0x0400000E RID: 14
		public static ScriptingInterfaceOfICamera.GetAspectRatioDelegate call_GetAspectRatioDelegate;

		// Token: 0x0400000F RID: 15
		public static ScriptingInterfaceOfICamera.GetEntityDelegate call_GetEntityDelegate;

		// Token: 0x04000010 RID: 16
		public static ScriptingInterfaceOfICamera.GetFarDelegate call_GetFarDelegate;

		// Token: 0x04000011 RID: 17
		public static ScriptingInterfaceOfICamera.GetFovHorizontalDelegate call_GetFovHorizontalDelegate;

		// Token: 0x04000012 RID: 18
		public static ScriptingInterfaceOfICamera.GetFovVerticalDelegate call_GetFovVerticalDelegate;

		// Token: 0x04000013 RID: 19
		public static ScriptingInterfaceOfICamera.GetFrameDelegate call_GetFrameDelegate;

		// Token: 0x04000014 RID: 20
		public static ScriptingInterfaceOfICamera.GetHorizontalFovDelegate call_GetHorizontalFovDelegate;

		// Token: 0x04000015 RID: 21
		public static ScriptingInterfaceOfICamera.GetNearDelegate call_GetNearDelegate;

		// Token: 0x04000016 RID: 22
		public static ScriptingInterfaceOfICamera.GetNearPlanePointsDelegate call_GetNearPlanePointsDelegate;

		// Token: 0x04000017 RID: 23
		public static ScriptingInterfaceOfICamera.GetNearPlanePointsStaticDelegate call_GetNearPlanePointsStaticDelegate;

		// Token: 0x04000018 RID: 24
		public static ScriptingInterfaceOfICamera.GetViewProjMatrixDelegate call_GetViewProjMatrixDelegate;

		// Token: 0x04000019 RID: 25
		public static ScriptingInterfaceOfICamera.LookAtDelegate call_LookAtDelegate;

		// Token: 0x0400001A RID: 26
		public static ScriptingInterfaceOfICamera.ReleaseDelegate call_ReleaseDelegate;

		// Token: 0x0400001B RID: 27
		public static ScriptingInterfaceOfICamera.ReleaseCameraEntityDelegate call_ReleaseCameraEntityDelegate;

		// Token: 0x0400001C RID: 28
		public static ScriptingInterfaceOfICamera.RenderFrustrumDelegate call_RenderFrustrumDelegate;

		// Token: 0x0400001D RID: 29
		public static ScriptingInterfaceOfICamera.ScreenSpaceRayProjectionDelegate call_ScreenSpaceRayProjectionDelegate;

		// Token: 0x0400001E RID: 30
		public static ScriptingInterfaceOfICamera.SetEntityDelegate call_SetEntityDelegate;

		// Token: 0x0400001F RID: 31
		public static ScriptingInterfaceOfICamera.SetFovHorizontalDelegate call_SetFovHorizontalDelegate;

		// Token: 0x04000020 RID: 32
		public static ScriptingInterfaceOfICamera.SetFovVerticalDelegate call_SetFovVerticalDelegate;

		// Token: 0x04000021 RID: 33
		public static ScriptingInterfaceOfICamera.SetFrameDelegate call_SetFrameDelegate;

		// Token: 0x04000022 RID: 34
		public static ScriptingInterfaceOfICamera.SetPositionDelegate call_SetPositionDelegate;

		// Token: 0x04000023 RID: 35
		public static ScriptingInterfaceOfICamera.SetViewVolumeDelegate call_SetViewVolumeDelegate;

		// Token: 0x04000024 RID: 36
		public static ScriptingInterfaceOfICamera.ViewportPointToWorldRayDelegate call_ViewportPointToWorldRayDelegate;

		// Token: 0x04000025 RID: 37
		public static ScriptingInterfaceOfICamera.WorldPointToViewportPointDelegate call_WorldPointToViewportPointDelegate;

		// Token: 0x02000078 RID: 120
		// (Invoke) Token: 0x060006E3 RID: 1763
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckEntityVisibilityDelegate(UIntPtr cameraPointer, UIntPtr entityPointer);

		// Token: 0x02000079 RID: 121
		// (Invoke) Token: 0x060006E7 RID: 1767
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ConstructCameraFromPositionElevationBearingDelegate(Vec3 position, float elevation, float bearing, ref MatrixFrame outFrame);

		// Token: 0x0200007A RID: 122
		// (Invoke) Token: 0x060006EB RID: 1771
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateCameraDelegate();

		// Token: 0x0200007B RID: 123
		// (Invoke) Token: 0x060006EF RID: 1775
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool EnclosesPointDelegate(UIntPtr cameraPointer, Vec3 pointInWorldSpace);

		// Token: 0x0200007C RID: 124
		// (Invoke) Token: 0x060006F3 RID: 1779
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FillParametersFromDelegate(UIntPtr cameraPointer, UIntPtr otherCameraPointer);

		// Token: 0x0200007D RID: 125
		// (Invoke) Token: 0x060006F7 RID: 1783
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetAspectRatioDelegate(UIntPtr cameraPointer);

		// Token: 0x0200007E RID: 126
		// (Invoke) Token: 0x060006FB RID: 1787
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer GetEntityDelegate(UIntPtr cameraPointer);

		// Token: 0x0200007F RID: 127
		// (Invoke) Token: 0x060006FF RID: 1791
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetFarDelegate(UIntPtr cameraPointer);

		// Token: 0x02000080 RID: 128
		// (Invoke) Token: 0x06000703 RID: 1795
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetFovHorizontalDelegate(UIntPtr cameraPointer);

		// Token: 0x02000081 RID: 129
		// (Invoke) Token: 0x06000707 RID: 1799
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetFovVerticalDelegate(UIntPtr cameraPointer);

		// Token: 0x02000082 RID: 130
		// (Invoke) Token: 0x0600070B RID: 1803
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetFrameDelegate(UIntPtr cameraPointer, ref MatrixFrame outFrame);

		// Token: 0x02000083 RID: 131
		// (Invoke) Token: 0x0600070F RID: 1807
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetHorizontalFovDelegate(UIntPtr cameraPointer);

		// Token: 0x02000084 RID: 132
		// (Invoke) Token: 0x06000713 RID: 1811
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetNearDelegate(UIntPtr cameraPointer);

		// Token: 0x02000085 RID: 133
		// (Invoke) Token: 0x06000717 RID: 1815
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetNearPlanePointsDelegate(UIntPtr cameraPointer, IntPtr nearPlanePoints);

		// Token: 0x02000086 RID: 134
		// (Invoke) Token: 0x0600071B RID: 1819
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetNearPlanePointsStaticDelegate(ref MatrixFrame cameraFrame, float verticalFov, float aspectRatioXY, float newDNear, float newDFar, IntPtr nearPlanePoints);

		// Token: 0x02000087 RID: 135
		// (Invoke) Token: 0x0600071F RID: 1823
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetViewProjMatrixDelegate(UIntPtr cameraPointer, ref MatrixFrame frame);

		// Token: 0x02000088 RID: 136
		// (Invoke) Token: 0x06000723 RID: 1827
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void LookAtDelegate(UIntPtr cameraPointer, Vec3 position, Vec3 target, Vec3 upVector);

		// Token: 0x02000089 RID: 137
		// (Invoke) Token: 0x06000727 RID: 1831
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseDelegate(UIntPtr cameraPointer);

		// Token: 0x0200008A RID: 138
		// (Invoke) Token: 0x0600072B RID: 1835
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseCameraEntityDelegate(UIntPtr cameraPointer);

		// Token: 0x0200008B RID: 139
		// (Invoke) Token: 0x0600072F RID: 1839
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RenderFrustrumDelegate(UIntPtr cameraPointer);

		// Token: 0x0200008C RID: 140
		// (Invoke) Token: 0x06000733 RID: 1843
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ScreenSpaceRayProjectionDelegate(UIntPtr cameraPointer, Vec2 screenPosition, ref Vec3 rayBegin, ref Vec3 rayEnd);

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x06000737 RID: 1847
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEntityDelegate(UIntPtr cameraPointer, UIntPtr entityId);

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x0600073B RID: 1851
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFovHorizontalDelegate(UIntPtr cameraPointer, float horizontalFov, float aspectRatio, float newDNear, float newDFar);

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x0600073F RID: 1855
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFovVerticalDelegate(UIntPtr cameraPointer, float verticalFov, float aspectRatio, float newDNear, float newDFar);

		// Token: 0x02000090 RID: 144
		// (Invoke) Token: 0x06000743 RID: 1859
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameDelegate(UIntPtr cameraPointer, ref MatrixFrame frame);

		// Token: 0x02000091 RID: 145
		// (Invoke) Token: 0x06000747 RID: 1863
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetPositionDelegate(UIntPtr cameraPointer, Vec3 position);

		// Token: 0x02000092 RID: 146
		// (Invoke) Token: 0x0600074B RID: 1867
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetViewVolumeDelegate(UIntPtr cameraPointer, [MarshalAs(UnmanagedType.U1)] bool perspective, float dLeft, float dRight, float dBottom, float dTop, float dNear, float dFar);

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x0600074F RID: 1871
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ViewportPointToWorldRayDelegate(UIntPtr cameraPointer, ref Vec3 rayBegin, ref Vec3 rayEnd, Vec3 viewportPoint);

		// Token: 0x02000094 RID: 148
		// (Invoke) Token: 0x06000753 RID: 1875
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 WorldPointToViewportPointDelegate(UIntPtr cameraPointer, ref Vec3 worldPoint);
	}
}
