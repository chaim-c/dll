using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x0200001E RID: 30
	internal class ScriptingInterfaceOfIMBSkeletonExtensions : IMBSkeletonExtensions
	{
		// Token: 0x060002F6 RID: 758 RVA: 0x0000C50C File Offset: 0x0000A70C
		public Skeleton CreateAgentSkeleton(string skeletonName, bool isHumanoid, int actionSetIndex, string monsterUsageSetName, ref AnimationSystemData animationSystemData)
		{
			byte[] array = null;
			if (skeletonName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBSkeletonExtensions._utf8.GetByteCount(skeletonName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBSkeletonExtensions._utf8.GetBytes(skeletonName, 0, skeletonName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (monsterUsageSetName != null)
			{
				int byteCount2 = ScriptingInterfaceOfIMBSkeletonExtensions._utf8.GetByteCount(monsterUsageSetName);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIMBSkeletonExtensions._utf8.GetBytes(monsterUsageSetName, 0, monsterUsageSetName.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMBSkeletonExtensions.call_CreateAgentSkeletonDelegate(array, isHumanoid, actionSetIndex, array2, ref animationSystemData);
			Skeleton result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Skeleton(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public Skeleton CreateSimpleSkeleton(string skeletonName)
		{
			byte[] array = null;
			if (skeletonName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBSkeletonExtensions._utf8.GetByteCount(skeletonName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBSkeletonExtensions._utf8.GetBytes(skeletonName, 0, skeletonName.Length, array, 0);
				array[byteCount] = 0;
			}
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMBSkeletonExtensions.call_CreateSimpleSkeletonDelegate(array);
			Skeleton result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Skeleton(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000C678 File Offset: 0x0000A878
		public Skeleton CreateWithActionSet(ref AnimationSystemData animationSystemData)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIMBSkeletonExtensions.call_CreateWithActionSetDelegate(ref animationSystemData);
			Skeleton result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new Skeleton(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
		public bool DoesActionContinueWithCurrentActionAtChannel(UIntPtr skeletonPointer, int actionChannelNo, int actionIndex)
		{
			return ScriptingInterfaceOfIMBSkeletonExtensions.call_DoesActionContinueWithCurrentActionAtChannelDelegate(skeletonPointer, actionChannelNo, actionIndex);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000C6D1 File Offset: 0x0000A8D1
		public int GetActionAtChannel(UIntPtr skeletonPointer, int channelNo)
		{
			return ScriptingInterfaceOfIMBSkeletonExtensions.call_GetActionAtChannelDelegate(skeletonPointer, channelNo);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000C6DF File Offset: 0x0000A8DF
		public void GetBoneEntitialFrame(UIntPtr skeletonPointer, sbyte bone, bool useBoneMapping, bool forceToUpdate, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIMBSkeletonExtensions.call_GetBoneEntitialFrameDelegate(skeletonPointer, bone, useBoneMapping, forceToUpdate, ref outFrame);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000C6F2 File Offset: 0x0000A8F2
		public void GetBoneEntitialFrameAtAnimationProgress(UIntPtr skeletonPointer, sbyte boneIndex, int animationIndex, float progress, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIMBSkeletonExtensions.call_GetBoneEntitialFrameAtAnimationProgressDelegate(skeletonPointer, boneIndex, animationIndex, progress, ref outFrame);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000C705 File Offset: 0x0000A905
		public string GetSkeletonFaceAnimationName(UIntPtr entityId)
		{
			if (ScriptingInterfaceOfIMBSkeletonExtensions.call_GetSkeletonFaceAnimationNameDelegate(entityId) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000C71C File Offset: 0x0000A91C
		public float GetSkeletonFaceAnimationTime(UIntPtr entityId)
		{
			return ScriptingInterfaceOfIMBSkeletonExtensions.call_GetSkeletonFaceAnimationTimeDelegate(entityId);
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000C729 File Offset: 0x0000A929
		public void SetAgentActionChannel(UIntPtr skeletonPointer, int actionChannelNo, int actionIndex, float channelParameter, float blendPeriodOverride, bool forceFaceMorphRestart)
		{
			ScriptingInterfaceOfIMBSkeletonExtensions.call_SetAgentActionChannelDelegate(skeletonPointer, actionChannelNo, actionIndex, channelParameter, blendPeriodOverride, forceFaceMorphRestart);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000C73E File Offset: 0x0000A93E
		public void SetAnimationAtChannel(UIntPtr skeletonPointer, int animationIndex, int channelNo, float animationSpeedMultiplier, float blendInPeriod, float startProgress)
		{
			ScriptingInterfaceOfIMBSkeletonExtensions.call_SetAnimationAtChannelDelegate(skeletonPointer, animationIndex, channelNo, animationSpeedMultiplier, blendInPeriod, startProgress);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000C754 File Offset: 0x0000A954
		public void SetFacialAnimationOfChannel(UIntPtr skeletonPointer, int channel, string facialAnimationName, bool playSound, bool loop)
		{
			byte[] array = null;
			if (facialAnimationName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBSkeletonExtensions._utf8.GetByteCount(facialAnimationName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBSkeletonExtensions._utf8.GetBytes(facialAnimationName, 0, facialAnimationName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBSkeletonExtensions.call_SetFacialAnimationOfChannelDelegate(skeletonPointer, channel, array, playSound, loop);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public void SetSkeletonFaceAnimationTime(UIntPtr entityId, float time)
		{
			ScriptingInterfaceOfIMBSkeletonExtensions.call_SetSkeletonFaceAnimationTimeDelegate(entityId, time);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000C7C2 File Offset: 0x0000A9C2
		public void TickActionChannels(UIntPtr skeletonPointer)
		{
			ScriptingInterfaceOfIMBSkeletonExtensions.call_TickActionChannelsDelegate(skeletonPointer);
		}

		// Token: 0x0400027A RID: 634
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400027B RID: 635
		public static ScriptingInterfaceOfIMBSkeletonExtensions.CreateAgentSkeletonDelegate call_CreateAgentSkeletonDelegate;

		// Token: 0x0400027C RID: 636
		public static ScriptingInterfaceOfIMBSkeletonExtensions.CreateSimpleSkeletonDelegate call_CreateSimpleSkeletonDelegate;

		// Token: 0x0400027D RID: 637
		public static ScriptingInterfaceOfIMBSkeletonExtensions.CreateWithActionSetDelegate call_CreateWithActionSetDelegate;

		// Token: 0x0400027E RID: 638
		public static ScriptingInterfaceOfIMBSkeletonExtensions.DoesActionContinueWithCurrentActionAtChannelDelegate call_DoesActionContinueWithCurrentActionAtChannelDelegate;

		// Token: 0x0400027F RID: 639
		public static ScriptingInterfaceOfIMBSkeletonExtensions.GetActionAtChannelDelegate call_GetActionAtChannelDelegate;

		// Token: 0x04000280 RID: 640
		public static ScriptingInterfaceOfIMBSkeletonExtensions.GetBoneEntitialFrameDelegate call_GetBoneEntitialFrameDelegate;

		// Token: 0x04000281 RID: 641
		public static ScriptingInterfaceOfIMBSkeletonExtensions.GetBoneEntitialFrameAtAnimationProgressDelegate call_GetBoneEntitialFrameAtAnimationProgressDelegate;

		// Token: 0x04000282 RID: 642
		public static ScriptingInterfaceOfIMBSkeletonExtensions.GetSkeletonFaceAnimationNameDelegate call_GetSkeletonFaceAnimationNameDelegate;

		// Token: 0x04000283 RID: 643
		public static ScriptingInterfaceOfIMBSkeletonExtensions.GetSkeletonFaceAnimationTimeDelegate call_GetSkeletonFaceAnimationTimeDelegate;

		// Token: 0x04000284 RID: 644
		public static ScriptingInterfaceOfIMBSkeletonExtensions.SetAgentActionChannelDelegate call_SetAgentActionChannelDelegate;

		// Token: 0x04000285 RID: 645
		public static ScriptingInterfaceOfIMBSkeletonExtensions.SetAnimationAtChannelDelegate call_SetAnimationAtChannelDelegate;

		// Token: 0x04000286 RID: 646
		public static ScriptingInterfaceOfIMBSkeletonExtensions.SetFacialAnimationOfChannelDelegate call_SetFacialAnimationOfChannelDelegate;

		// Token: 0x04000287 RID: 647
		public static ScriptingInterfaceOfIMBSkeletonExtensions.SetSkeletonFaceAnimationTimeDelegate call_SetSkeletonFaceAnimationTimeDelegate;

		// Token: 0x04000288 RID: 648
		public static ScriptingInterfaceOfIMBSkeletonExtensions.TickActionChannelsDelegate call_TickActionChannelsDelegate;

		// Token: 0x020002D0 RID: 720
		// (Invoke) Token: 0x06000DE5 RID: 3557
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateAgentSkeletonDelegate(byte[] skeletonName, [MarshalAs(UnmanagedType.U1)] bool isHumanoid, int actionSetIndex, byte[] monsterUsageSetName, ref AnimationSystemData animationSystemData);

		// Token: 0x020002D1 RID: 721
		// (Invoke) Token: 0x06000DE9 RID: 3561
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateSimpleSkeletonDelegate(byte[] skeletonName);

		// Token: 0x020002D2 RID: 722
		// (Invoke) Token: 0x06000DED RID: 3565
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateWithActionSetDelegate(ref AnimationSystemData animationSystemData);

		// Token: 0x020002D3 RID: 723
		// (Invoke) Token: 0x06000DF1 RID: 3569
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool DoesActionContinueWithCurrentActionAtChannelDelegate(UIntPtr skeletonPointer, int actionChannelNo, int actionIndex);

		// Token: 0x020002D4 RID: 724
		// (Invoke) Token: 0x06000DF5 RID: 3573
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetActionAtChannelDelegate(UIntPtr skeletonPointer, int channelNo);

		// Token: 0x020002D5 RID: 725
		// (Invoke) Token: 0x06000DF9 RID: 3577
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneEntitialFrameDelegate(UIntPtr skeletonPointer, sbyte bone, [MarshalAs(UnmanagedType.U1)] bool useBoneMapping, [MarshalAs(UnmanagedType.U1)] bool forceToUpdate, ref MatrixFrame outFrame);

		// Token: 0x020002D6 RID: 726
		// (Invoke) Token: 0x06000DFD RID: 3581
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetBoneEntitialFrameAtAnimationProgressDelegate(UIntPtr skeletonPointer, sbyte boneIndex, int animationIndex, float progress, ref MatrixFrame outFrame);

		// Token: 0x020002D7 RID: 727
		// (Invoke) Token: 0x06000E01 RID: 3585
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSkeletonFaceAnimationNameDelegate(UIntPtr entityId);

		// Token: 0x020002D8 RID: 728
		// (Invoke) Token: 0x06000E05 RID: 3589
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetSkeletonFaceAnimationTimeDelegate(UIntPtr entityId);

		// Token: 0x020002D9 RID: 729
		// (Invoke) Token: 0x06000E09 RID: 3593
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAgentActionChannelDelegate(UIntPtr skeletonPointer, int actionChannelNo, int actionIndex, float channelParameter, float blendPeriodOverride, [MarshalAs(UnmanagedType.U1)] bool forceFaceMorphRestart);

		// Token: 0x020002DA RID: 730
		// (Invoke) Token: 0x06000E0D RID: 3597
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetAnimationAtChannelDelegate(UIntPtr skeletonPointer, int animationIndex, int channelNo, float animationSpeedMultiplier, float blendInPeriod, float startProgress);

		// Token: 0x020002DB RID: 731
		// (Invoke) Token: 0x06000E11 RID: 3601
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetFacialAnimationOfChannelDelegate(UIntPtr skeletonPointer, int channel, byte[] facialAnimationName, [MarshalAs(UnmanagedType.U1)] bool playSound, [MarshalAs(UnmanagedType.U1)] bool loop);

		// Token: 0x020002DC RID: 732
		// (Invoke) Token: 0x06000E15 RID: 3605
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetSkeletonFaceAnimationTimeDelegate(UIntPtr entityId, float time);

		// Token: 0x020002DD RID: 733
		// (Invoke) Token: 0x06000E19 RID: 3609
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TickActionChannelsDelegate(UIntPtr skeletonPointer);
	}
}
