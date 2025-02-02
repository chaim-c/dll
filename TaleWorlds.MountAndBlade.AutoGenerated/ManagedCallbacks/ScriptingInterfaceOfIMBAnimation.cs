using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x0200000C RID: 12
	internal class ScriptingInterfaceOfIMBAnimation : IMBAnimation
	{
		// Token: 0x060001AE RID: 430 RVA: 0x0000A1CA File Offset: 0x000083CA
		public int AnimationIndexOfActionCode(int actionSetNo, int actionIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_AnimationIndexOfActionCodeDelegate(actionSetNo, actionIndex);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000A1D8 File Offset: 0x000083D8
		public bool CheckAnimationClipExists(int actionSetNo, int actionIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_CheckAnimationClipExistsDelegate(actionSetNo, actionIndex);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000A1E6 File Offset: 0x000083E6
		public float GetActionAnimationDuration(int actionSetNo, int actionIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetActionAnimationDurationDelegate(actionSetNo, actionIndex);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public float GetActionBlendOutStartProgress(int actionSetNo, int actionIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetActionBlendOutStartProgressDelegate(actionSetNo, actionIndex);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000A204 File Offset: 0x00008404
		public int GetActionCodeWithName(string name)
		{
			byte[] array = null;
			if (name != null)
			{
				int byteCount = ScriptingInterfaceOfIMBAnimation._utf8.GetByteCount(name);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBAnimation._utf8.GetBytes(name, 0, name.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBAnimation.call_GetActionCodeWithNameDelegate(array);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000A25E File Offset: 0x0000845E
		public string GetActionNameWithCode(int index)
		{
			if (ScriptingInterfaceOfIMBAnimation.call_GetActionNameWithCodeDelegate(index) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000A275 File Offset: 0x00008475
		public Agent.ActionCodeType GetActionType(int actionIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetActionTypeDelegate(actionIndex);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000A282 File Offset: 0x00008482
		public float GetAnimationBlendInPeriod(int animationIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetAnimationBlendInPeriodDelegate(animationIndex);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000A28F File Offset: 0x0000848F
		public int GetAnimationContinueToAction(int actionSetNo, int actionIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetAnimationContinueToActionDelegate(actionSetNo, actionIndex);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000A29D File Offset: 0x0000849D
		public float GetAnimationDuration(int animationIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetAnimationDurationDelegate(animationIndex);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A2AA File Offset: 0x000084AA
		public AnimFlags GetAnimationFlags(int actionSetNo, int actionIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetAnimationFlagsDelegate(actionSetNo, actionIndex);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000A2B8 File Offset: 0x000084B8
		public string GetAnimationName(int actionSetNo, int actionIndex)
		{
			if (ScriptingInterfaceOfIMBAnimation.call_GetAnimationNameDelegate(actionSetNo, actionIndex) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000A2D0 File Offset: 0x000084D0
		public float GetAnimationParameter1(int animationIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetAnimationParameter1Delegate(animationIndex);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000A2DD File Offset: 0x000084DD
		public float GetAnimationParameter2(int animationIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetAnimationParameter2Delegate(animationIndex);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A2EA File Offset: 0x000084EA
		public float GetAnimationParameter3(int animationIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetAnimationParameter3Delegate(animationIndex);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A2F7 File Offset: 0x000084F7
		public Vec3 GetDisplacementVector(int actionSetNo, int actionIndex)
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetDisplacementVectorDelegate(actionSetNo, actionIndex);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A305 File Offset: 0x00008505
		public string GetIDWithIndex(int index)
		{
			if (ScriptingInterfaceOfIMBAnimation.call_GetIDWithIndexDelegate(index) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000A31C File Offset: 0x0000851C
		public int GetIndexWithID(string id)
		{
			byte[] array = null;
			if (id != null)
			{
				int byteCount = ScriptingInterfaceOfIMBAnimation._utf8.GetByteCount(id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBAnimation._utf8.GetBytes(id, 0, id.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBAnimation.call_GetIndexWithIDDelegate(array);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000A376 File Offset: 0x00008576
		public int GetNumActionCodes()
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetNumActionCodesDelegate();
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000A382 File Offset: 0x00008582
		public int GetNumAnimations()
		{
			return ScriptingInterfaceOfIMBAnimation.call_GetNumAnimationsDelegate();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A38E File Offset: 0x0000858E
		public bool IsAnyAnimationLoadingFromDisk()
		{
			return ScriptingInterfaceOfIMBAnimation.call_IsAnyAnimationLoadingFromDiskDelegate();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000A39A File Offset: 0x0000859A
		public void PrefetchAnimationClip(int actionSetNo, int actionIndex)
		{
			ScriptingInterfaceOfIMBAnimation.call_PrefetchAnimationClipDelegate(actionSetNo, actionIndex);
		}

		// Token: 0x04000148 RID: 328
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000149 RID: 329
		public static ScriptingInterfaceOfIMBAnimation.AnimationIndexOfActionCodeDelegate call_AnimationIndexOfActionCodeDelegate;

		// Token: 0x0400014A RID: 330
		public static ScriptingInterfaceOfIMBAnimation.CheckAnimationClipExistsDelegate call_CheckAnimationClipExistsDelegate;

		// Token: 0x0400014B RID: 331
		public static ScriptingInterfaceOfIMBAnimation.GetActionAnimationDurationDelegate call_GetActionAnimationDurationDelegate;

		// Token: 0x0400014C RID: 332
		public static ScriptingInterfaceOfIMBAnimation.GetActionBlendOutStartProgressDelegate call_GetActionBlendOutStartProgressDelegate;

		// Token: 0x0400014D RID: 333
		public static ScriptingInterfaceOfIMBAnimation.GetActionCodeWithNameDelegate call_GetActionCodeWithNameDelegate;

		// Token: 0x0400014E RID: 334
		public static ScriptingInterfaceOfIMBAnimation.GetActionNameWithCodeDelegate call_GetActionNameWithCodeDelegate;

		// Token: 0x0400014F RID: 335
		public static ScriptingInterfaceOfIMBAnimation.GetActionTypeDelegate call_GetActionTypeDelegate;

		// Token: 0x04000150 RID: 336
		public static ScriptingInterfaceOfIMBAnimation.GetAnimationBlendInPeriodDelegate call_GetAnimationBlendInPeriodDelegate;

		// Token: 0x04000151 RID: 337
		public static ScriptingInterfaceOfIMBAnimation.GetAnimationContinueToActionDelegate call_GetAnimationContinueToActionDelegate;

		// Token: 0x04000152 RID: 338
		public static ScriptingInterfaceOfIMBAnimation.GetAnimationDurationDelegate call_GetAnimationDurationDelegate;

		// Token: 0x04000153 RID: 339
		public static ScriptingInterfaceOfIMBAnimation.GetAnimationFlagsDelegate call_GetAnimationFlagsDelegate;

		// Token: 0x04000154 RID: 340
		public static ScriptingInterfaceOfIMBAnimation.GetAnimationNameDelegate call_GetAnimationNameDelegate;

		// Token: 0x04000155 RID: 341
		public static ScriptingInterfaceOfIMBAnimation.GetAnimationParameter1Delegate call_GetAnimationParameter1Delegate;

		// Token: 0x04000156 RID: 342
		public static ScriptingInterfaceOfIMBAnimation.GetAnimationParameter2Delegate call_GetAnimationParameter2Delegate;

		// Token: 0x04000157 RID: 343
		public static ScriptingInterfaceOfIMBAnimation.GetAnimationParameter3Delegate call_GetAnimationParameter3Delegate;

		// Token: 0x04000158 RID: 344
		public static ScriptingInterfaceOfIMBAnimation.GetDisplacementVectorDelegate call_GetDisplacementVectorDelegate;

		// Token: 0x04000159 RID: 345
		public static ScriptingInterfaceOfIMBAnimation.GetIDWithIndexDelegate call_GetIDWithIndexDelegate;

		// Token: 0x0400015A RID: 346
		public static ScriptingInterfaceOfIMBAnimation.GetIndexWithIDDelegate call_GetIndexWithIDDelegate;

		// Token: 0x0400015B RID: 347
		public static ScriptingInterfaceOfIMBAnimation.GetNumActionCodesDelegate call_GetNumActionCodesDelegate;

		// Token: 0x0400015C RID: 348
		public static ScriptingInterfaceOfIMBAnimation.GetNumAnimationsDelegate call_GetNumAnimationsDelegate;

		// Token: 0x0400015D RID: 349
		public static ScriptingInterfaceOfIMBAnimation.IsAnyAnimationLoadingFromDiskDelegate call_IsAnyAnimationLoadingFromDiskDelegate;

		// Token: 0x0400015E RID: 350
		public static ScriptingInterfaceOfIMBAnimation.PrefetchAnimationClipDelegate call_PrefetchAnimationClipDelegate;

		// Token: 0x020001B0 RID: 432
		// (Invoke) Token: 0x06000965 RID: 2405
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AnimationIndexOfActionCodeDelegate(int actionSetNo, int actionIndex);

		// Token: 0x020001B1 RID: 433
		// (Invoke) Token: 0x06000969 RID: 2409
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CheckAnimationClipExistsDelegate(int actionSetNo, int actionIndex);

		// Token: 0x020001B2 RID: 434
		// (Invoke) Token: 0x0600096D RID: 2413
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetActionAnimationDurationDelegate(int actionSetNo, int actionIndex);

		// Token: 0x020001B3 RID: 435
		// (Invoke) Token: 0x06000971 RID: 2417
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetActionBlendOutStartProgressDelegate(int actionSetNo, int actionIndex);

		// Token: 0x020001B4 RID: 436
		// (Invoke) Token: 0x06000975 RID: 2421
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetActionCodeWithNameDelegate(byte[] name);

		// Token: 0x020001B5 RID: 437
		// (Invoke) Token: 0x06000979 RID: 2425
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetActionNameWithCodeDelegate(int index);

		// Token: 0x020001B6 RID: 438
		// (Invoke) Token: 0x0600097D RID: 2429
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Agent.ActionCodeType GetActionTypeDelegate(int actionIndex);

		// Token: 0x020001B7 RID: 439
		// (Invoke) Token: 0x06000981 RID: 2433
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetAnimationBlendInPeriodDelegate(int animationIndex);

		// Token: 0x020001B8 RID: 440
		// (Invoke) Token: 0x06000985 RID: 2437
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAnimationContinueToActionDelegate(int actionSetNo, int actionIndex);

		// Token: 0x020001B9 RID: 441
		// (Invoke) Token: 0x06000989 RID: 2441
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetAnimationDurationDelegate(int animationIndex);

		// Token: 0x020001BA RID: 442
		// (Invoke) Token: 0x0600098D RID: 2445
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate AnimFlags GetAnimationFlagsDelegate(int actionSetNo, int actionIndex);

		// Token: 0x020001BB RID: 443
		// (Invoke) Token: 0x06000991 RID: 2449
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAnimationNameDelegate(int actionSetNo, int actionIndex);

		// Token: 0x020001BC RID: 444
		// (Invoke) Token: 0x06000995 RID: 2453
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetAnimationParameter1Delegate(int animationIndex);

		// Token: 0x020001BD RID: 445
		// (Invoke) Token: 0x06000999 RID: 2457
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetAnimationParameter2Delegate(int animationIndex);

		// Token: 0x020001BE RID: 446
		// (Invoke) Token: 0x0600099D RID: 2461
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetAnimationParameter3Delegate(int animationIndex);

		// Token: 0x020001BF RID: 447
		// (Invoke) Token: 0x060009A1 RID: 2465
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate Vec3 GetDisplacementVectorDelegate(int actionSetNo, int actionIndex);

		// Token: 0x020001C0 RID: 448
		// (Invoke) Token: 0x060009A5 RID: 2469
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetIDWithIndexDelegate(int index);

		// Token: 0x020001C1 RID: 449
		// (Invoke) Token: 0x060009A9 RID: 2473
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetIndexWithIDDelegate(byte[] id);

		// Token: 0x020001C2 RID: 450
		// (Invoke) Token: 0x060009AD RID: 2477
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumActionCodesDelegate();

		// Token: 0x020001C3 RID: 451
		// (Invoke) Token: 0x060009B1 RID: 2481
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumAnimationsDelegate();

		// Token: 0x020001C4 RID: 452
		// (Invoke) Token: 0x060009B5 RID: 2485
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsAnyAnimationLoadingFromDiskDelegate();

		// Token: 0x020001C5 RID: 453
		// (Invoke) Token: 0x060009B9 RID: 2489
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PrefetchAnimationClipDelegate(int actionSetNo, int actionIndex);
	}
}
