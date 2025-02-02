using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x02000016 RID: 22
	internal class ScriptingInterfaceOfILight : ILight
	{
		// Token: 0x0600020B RID: 523 RVA: 0x00010564 File Offset: 0x0000E764
		public Light CreatePointLight(float lightRadius)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfILight.call_CreatePointLightDelegate(lightRadius);
			Light result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Light(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000105AE File Offset: 0x0000E7AE
		public void EnableShadow(UIntPtr lightpointer, bool shadowEnabled)
		{
			ScriptingInterfaceOfILight.call_EnableShadowDelegate(lightpointer, shadowEnabled);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000105BC File Offset: 0x0000E7BC
		public void GetFrame(UIntPtr lightPointer, out MatrixFrame result)
		{
			ScriptingInterfaceOfILight.call_GetFrameDelegate(lightPointer, out result);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000105CA File Offset: 0x0000E7CA
		public float GetIntensity(UIntPtr lightPointer)
		{
			return ScriptingInterfaceOfILight.call_GetIntensityDelegate(lightPointer);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000105D7 File Offset: 0x0000E7D7
		public Vec3 GetLightColor(UIntPtr lightpointer)
		{
			return ScriptingInterfaceOfILight.call_GetLightColorDelegate(lightpointer);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000105E4 File Offset: 0x0000E7E4
		public float GetRadius(UIntPtr lightpointer)
		{
			return ScriptingInterfaceOfILight.call_GetRadiusDelegate(lightpointer);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000105F1 File Offset: 0x0000E7F1
		public bool IsShadowEnabled(UIntPtr lightpointer)
		{
			return ScriptingInterfaceOfILight.call_IsShadowEnabledDelegate(lightpointer);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000105FE File Offset: 0x0000E7FE
		public void Release(UIntPtr lightpointer)
		{
			ScriptingInterfaceOfILight.call_ReleaseDelegate(lightpointer);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0001060B File Offset: 0x0000E80B
		public void SetFrame(UIntPtr lightPointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfILight.call_SetFrameDelegate(lightPointer, ref frame);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00010619 File Offset: 0x0000E819
		public void SetIntensity(UIntPtr lightPointer, float value)
		{
			ScriptingInterfaceOfILight.call_SetIntensityDelegate(lightPointer, value);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00010627 File Offset: 0x0000E827
		public void SetLightColor(UIntPtr lightpointer, Vec3 color)
		{
			ScriptingInterfaceOfILight.call_SetLightColorDelegate(lightpointer, color);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00010635 File Offset: 0x0000E835
		public void SetLightFlicker(UIntPtr lightpointer, float magnitude, float interval)
		{
			ScriptingInterfaceOfILight.call_SetLightFlickerDelegate(lightpointer, magnitude, interval);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00010644 File Offset: 0x0000E844
		public void SetRadius(UIntPtr lightpointer, float radius)
		{
			ScriptingInterfaceOfILight.call_SetRadiusDelegate(lightpointer, radius);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00010652 File Offset: 0x0000E852
		public void SetShadows(UIntPtr lightPointer, int shadowType)
		{
			ScriptingInterfaceOfILight.call_SetShadowsDelegate(lightPointer, shadowType);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00010660 File Offset: 0x0000E860
		public void SetVisibility(UIntPtr lightpointer, bool value)
		{
			ScriptingInterfaceOfILight.call_SetVisibilityDelegate(lightpointer, value);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0001066E File Offset: 0x0000E86E
		public void SetVolumetricProperties(UIntPtr lightpointer, bool volumelightenabled, float volumeparameter)
		{
			ScriptingInterfaceOfILight.call_SetVolumetricPropertiesDelegate(lightpointer, volumelightenabled, volumeparameter);
		}

		// Token: 0x040001AA RID: 426
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040001AB RID: 427
		public static ScriptingInterfaceOfILight.CreatePointLightDelegate call_CreatePointLightDelegate;

		// Token: 0x040001AC RID: 428
		public static ScriptingInterfaceOfILight.EnableShadowDelegate call_EnableShadowDelegate;

		// Token: 0x040001AD RID: 429
		public static ScriptingInterfaceOfILight.GetFrameDelegate call_GetFrameDelegate;

		// Token: 0x040001AE RID: 430
		public static ScriptingInterfaceOfILight.GetIntensityDelegate call_GetIntensityDelegate;

		// Token: 0x040001AF RID: 431
		public static ScriptingInterfaceOfILight.GetLightColorDelegate call_GetLightColorDelegate;

		// Token: 0x040001B0 RID: 432
		public static ScriptingInterfaceOfILight.GetRadiusDelegate call_GetRadiusDelegate;

		// Token: 0x040001B1 RID: 433
		public static ScriptingInterfaceOfILight.IsShadowEnabledDelegate call_IsShadowEnabledDelegate;

		// Token: 0x040001B2 RID: 434
		public static ScriptingInterfaceOfILight.ReleaseDelegate call_ReleaseDelegate;

		// Token: 0x040001B3 RID: 435
		public static ScriptingInterfaceOfILight.SetFrameDelegate call_SetFrameDelegate;

		// Token: 0x040001B4 RID: 436
		public static ScriptingInterfaceOfILight.SetIntensityDelegate call_SetIntensityDelegate;

		// Token: 0x040001B5 RID: 437
		public static ScriptingInterfaceOfILight.SetLightColorDelegate call_SetLightColorDelegate;

		// Token: 0x040001B6 RID: 438
		public static ScriptingInterfaceOfILight.SetLightFlickerDelegate call_SetLightFlickerDelegate;

		// Token: 0x040001B7 RID: 439
		public static ScriptingInterfaceOfILight.SetRadiusDelegate call_SetRadiusDelegate;

		// Token: 0x040001B8 RID: 440
		public static ScriptingInterfaceOfILight.SetShadowsDelegate call_SetShadowsDelegate;

		// Token: 0x040001B9 RID: 441
		public static ScriptingInterfaceOfILight.SetVisibilityDelegate call_SetVisibilityDelegate;

		// Token: 0x040001BA RID: 442
		public static ScriptingInterfaceOfILight.SetVolumetricPropertiesDelegate call_SetVolumetricPropertiesDelegate;

		// Token: 0x0200020E RID: 526
		// (Invoke) Token: 0x06000D3B RID: 3387
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreatePointLightDelegate(float lightRadius);

		// Token: 0x0200020F RID: 527
		// (Invoke) Token: 0x06000D3F RID: 3391
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EnableShadowDelegate(UIntPtr lightpointer, [MarshalAs(UnmanagedType.U1)] bool shadowEnabled);

		// Token: 0x02000210 RID: 528
		// (Invoke) Token: 0x06000D43 RID: 3395
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetFrameDelegate(UIntPtr lightPointer, out MatrixFrame result);

		// Token: 0x02000211 RID: 529
		// (Invoke) Token: 0x06000D47 RID: 3399
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetIntensityDelegate(UIntPtr lightPointer);

		// Token: 0x02000212 RID: 530
		// (Invoke) Token: 0x06000D4B RID: 3403
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetLightColorDelegate(UIntPtr lightpointer);

		// Token: 0x02000213 RID: 531
		// (Invoke) Token: 0x06000D4F RID: 3407
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetRadiusDelegate(UIntPtr lightpointer);

		// Token: 0x02000214 RID: 532
		// (Invoke) Token: 0x06000D53 RID: 3411
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsShadowEnabledDelegate(UIntPtr lightpointer);

		// Token: 0x02000215 RID: 533
		// (Invoke) Token: 0x06000D57 RID: 3415
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ReleaseDelegate(UIntPtr lightpointer);

		// Token: 0x02000216 RID: 534
		// (Invoke) Token: 0x06000D5B RID: 3419
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFrameDelegate(UIntPtr lightPointer, ref MatrixFrame frame);

		// Token: 0x02000217 RID: 535
		// (Invoke) Token: 0x06000D5F RID: 3423
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetIntensityDelegate(UIntPtr lightPointer, float value);

		// Token: 0x02000218 RID: 536
		// (Invoke) Token: 0x06000D63 RID: 3427
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLightColorDelegate(UIntPtr lightpointer, Vec3 color);

		// Token: 0x02000219 RID: 537
		// (Invoke) Token: 0x06000D67 RID: 3431
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLightFlickerDelegate(UIntPtr lightpointer, float magnitude, float interval);

		// Token: 0x0200021A RID: 538
		// (Invoke) Token: 0x06000D6B RID: 3435
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRadiusDelegate(UIntPtr lightpointer, float radius);

		// Token: 0x0200021B RID: 539
		// (Invoke) Token: 0x06000D6F RID: 3439
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetShadowsDelegate(UIntPtr lightPointer, int shadowType);

		// Token: 0x0200021C RID: 540
		// (Invoke) Token: 0x06000D73 RID: 3443
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVisibilityDelegate(UIntPtr lightpointer, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x0200021D RID: 541
		// (Invoke) Token: 0x06000D77 RID: 3447
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetVolumetricPropertiesDelegate(UIntPtr lightpointer, [MarshalAs(UnmanagedType.U1)] bool volumelightenabled, float volumeparameter);
	}
}
