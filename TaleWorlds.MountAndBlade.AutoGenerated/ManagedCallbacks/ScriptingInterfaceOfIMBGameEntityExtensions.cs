﻿using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;
using TaleWorlds.MountAndBlade.AutoGenerated;

namespace ManagedCallbacks
{
	// Token: 0x02000015 RID: 21
	internal class ScriptingInterfaceOfIMBGameEntityExtensions : IMBGameEntityExtensions
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000ACD4 File Offset: 0x00008ED4
		public GameEntity CreateFromWeapon(UIntPtr scenePointer, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, int weaponStatsDataLength, in WeaponData ammoWeaponData, WeaponStatsData[] ammoWeaponStatsData, int ammoWeaponStatsDataLength, bool showHolsterWithWeapon)
		{
			WeaponDataAsNative weaponDataAsNative = new WeaponDataAsNative(weaponData);
			PinnedArrayData<WeaponStatsData> pinnedArrayData = new PinnedArrayData<WeaponStatsData>(weaponStatsData, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			WeaponDataAsNative weaponDataAsNative2 = new WeaponDataAsNative(ammoWeaponData);
			PinnedArrayData<WeaponStatsData> pinnedArrayData2 = new PinnedArrayData<WeaponStatsData>(ammoWeaponStatsData, false);
			IntPtr pointer2 = pinnedArrayData2.Pointer;
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMBGameEntityExtensions.call_CreateFromWeaponDelegate(scenePointer, weaponDataAsNative, pointer, weaponStatsDataLength, weaponDataAsNative2, pointer2, ammoWeaponStatsDataLength, showHolsterWithWeapon);
			pinnedArrayData.Dispose();
			pinnedArrayData2.Dispose();
			GameEntity result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new GameEntity(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000AD7F File Offset: 0x00008F7F
		public void FadeIn(UIntPtr entityPointer, bool resetAlpha)
		{
			ScriptingInterfaceOfIMBGameEntityExtensions.call_FadeInDelegate(entityPointer, resetAlpha);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000AD8D File Offset: 0x00008F8D
		public void FadeOut(UIntPtr entityPointer, float interval, bool isRemovingFromScene)
		{
			ScriptingInterfaceOfIMBGameEntityExtensions.call_FadeOutDelegate(entityPointer, interval, isRemovingFromScene);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000AD9C File Offset: 0x00008F9C
		public void HideIfNotFadingOut(UIntPtr entityPointer)
		{
			ScriptingInterfaceOfIMBGameEntityExtensions.call_HideIfNotFadingOutDelegate(entityPointer);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000ADC0 File Offset: 0x00008FC0
		GameEntity IMBGameEntityExtensions.CreateFromWeapon(UIntPtr scenePointer, in WeaponData weaponData, WeaponStatsData[] weaponStatsData, int weaponStatsDataLength, in WeaponData ammoWeaponData, WeaponStatsData[] ammoWeaponStatsData, int ammoWeaponStatsDataLength, bool showHolsterWithWeapon)
		{
			return this.CreateFromWeapon(scenePointer, weaponData, weaponStatsData, weaponStatsDataLength, ammoWeaponData, ammoWeaponStatsData, ammoWeaponStatsDataLength, showHolsterWithWeapon);
		}

		// Token: 0x040001A4 RID: 420
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040001A5 RID: 421
		public static ScriptingInterfaceOfIMBGameEntityExtensions.CreateFromWeaponDelegate call_CreateFromWeaponDelegate;

		// Token: 0x040001A6 RID: 422
		public static ScriptingInterfaceOfIMBGameEntityExtensions.FadeInDelegate call_FadeInDelegate;

		// Token: 0x040001A7 RID: 423
		public static ScriptingInterfaceOfIMBGameEntityExtensions.FadeOutDelegate call_FadeOutDelegate;

		// Token: 0x040001A8 RID: 424
		public static ScriptingInterfaceOfIMBGameEntityExtensions.HideIfNotFadingOutDelegate call_HideIfNotFadingOutDelegate;

		// Token: 0x02000203 RID: 515
		// (Invoke) Token: 0x06000AB1 RID: 2737
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateFromWeaponDelegate(UIntPtr scenePointer, in WeaponDataAsNative weaponData, IntPtr weaponStatsData, int weaponStatsDataLength, in WeaponDataAsNative ammoWeaponData, IntPtr ammoWeaponStatsData, int ammoWeaponStatsDataLength, [MarshalAs(UnmanagedType.U1)] bool showHolsterWithWeapon);

		// Token: 0x02000204 RID: 516
		// (Invoke) Token: 0x06000AB5 RID: 2741
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FadeInDelegate(UIntPtr entityPointer, [MarshalAs(UnmanagedType.U1)] bool resetAlpha);

		// Token: 0x02000205 RID: 517
		// (Invoke) Token: 0x06000AB9 RID: 2745
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FadeOutDelegate(UIntPtr entityPointer, float interval, [MarshalAs(UnmanagedType.U1)] bool isRemovingFromScene);

		// Token: 0x02000206 RID: 518
		// (Invoke) Token: 0x06000ABD RID: 2749
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void HideIfNotFadingOutDelegate(UIntPtr entityPointer);
	}
}
