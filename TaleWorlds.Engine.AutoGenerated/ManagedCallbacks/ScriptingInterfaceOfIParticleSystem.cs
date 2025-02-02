using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace ManagedCallbacks
{
	// Token: 0x0200001E RID: 30
	internal class ScriptingInterfaceOfIParticleSystem : IParticleSystem
	{
		// Token: 0x06000312 RID: 786 RVA: 0x00012194 File Offset: 0x00010394
		public ParticleSystem CreateParticleSystemAttachedToBone(int runtimeId, UIntPtr skeletonPtr, sbyte boneIndex, ref MatrixFrame boneLocalFrame)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIParticleSystem.call_CreateParticleSystemAttachedToBoneDelegate(runtimeId, skeletonPtr, boneIndex, ref boneLocalFrame);
			ParticleSystem result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new ParticleSystem(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000121E4 File Offset: 0x000103E4
		public ParticleSystem CreateParticleSystemAttachedToEntity(int runtimeId, UIntPtr entityPtr, ref MatrixFrame boneLocalFrame)
		{
			NativeObjectPointer nativeObjectPointer = ScriptingInterfaceOfIParticleSystem.call_CreateParticleSystemAttachedToEntityDelegate(runtimeId, entityPtr, ref boneLocalFrame);
			ParticleSystem result = null;
			if (nativeObjectPointer.Pointer != UIntPtr.Zero)
			{
				result = new ParticleSystem(nativeObjectPointer.Pointer);
				LibraryApplicationInterface.IManaged.DecreaseReferenceCount(nativeObjectPointer.Pointer);
			}
			return result;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00012230 File Offset: 0x00010430
		public void GetLocalFrame(UIntPtr pointer, ref MatrixFrame frame)
		{
			ScriptingInterfaceOfIParticleSystem.call_GetLocalFrameDelegate(pointer, ref frame);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00012240 File Offset: 0x00010440
		public int GetRuntimeIdByName(string particleSystemName)
		{
			byte[] array = null;
			if (particleSystemName != null)
			{
				int byteCount = ScriptingInterfaceOfIParticleSystem._utf8.GetByteCount(particleSystemName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIParticleSystem._utf8.GetBytes(particleSystemName, 0, particleSystemName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIParticleSystem.call_GetRuntimeIdByNameDelegate(array);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001229A File Offset: 0x0001049A
		public void Restart(UIntPtr psysPointer)
		{
			ScriptingInterfaceOfIParticleSystem.call_RestartDelegate(psysPointer);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000122A7 File Offset: 0x000104A7
		public void SetEnable(UIntPtr psysPointer, bool enable)
		{
			ScriptingInterfaceOfIParticleSystem.call_SetEnableDelegate(psysPointer, enable);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000122B5 File Offset: 0x000104B5
		public void SetLocalFrame(UIntPtr pointer, ref MatrixFrame newFrame)
		{
			ScriptingInterfaceOfIParticleSystem.call_SetLocalFrameDelegate(pointer, ref newFrame);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000122C4 File Offset: 0x000104C4
		public void SetParticleEffectByName(UIntPtr pointer, string effectName)
		{
			byte[] array = null;
			if (effectName != null)
			{
				int byteCount = ScriptingInterfaceOfIParticleSystem._utf8.GetByteCount(effectName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIParticleSystem._utf8.GetBytes(effectName, 0, effectName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIParticleSystem.call_SetParticleEffectByNameDelegate(pointer, array);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001231F File Offset: 0x0001051F
		public void SetRuntimeEmissionRateMultiplier(UIntPtr pointer, float multiplier)
		{
			ScriptingInterfaceOfIParticleSystem.call_SetRuntimeEmissionRateMultiplierDelegate(pointer, multiplier);
		}

		// Token: 0x040002A9 RID: 681
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040002AA RID: 682
		public static ScriptingInterfaceOfIParticleSystem.CreateParticleSystemAttachedToBoneDelegate call_CreateParticleSystemAttachedToBoneDelegate;

		// Token: 0x040002AB RID: 683
		public static ScriptingInterfaceOfIParticleSystem.CreateParticleSystemAttachedToEntityDelegate call_CreateParticleSystemAttachedToEntityDelegate;

		// Token: 0x040002AC RID: 684
		public static ScriptingInterfaceOfIParticleSystem.GetLocalFrameDelegate call_GetLocalFrameDelegate;

		// Token: 0x040002AD RID: 685
		public static ScriptingInterfaceOfIParticleSystem.GetRuntimeIdByNameDelegate call_GetRuntimeIdByNameDelegate;

		// Token: 0x040002AE RID: 686
		public static ScriptingInterfaceOfIParticleSystem.RestartDelegate call_RestartDelegate;

		// Token: 0x040002AF RID: 687
		public static ScriptingInterfaceOfIParticleSystem.SetEnableDelegate call_SetEnableDelegate;

		// Token: 0x040002B0 RID: 688
		public static ScriptingInterfaceOfIParticleSystem.SetLocalFrameDelegate call_SetLocalFrameDelegate;

		// Token: 0x040002B1 RID: 689
		public static ScriptingInterfaceOfIParticleSystem.SetParticleEffectByNameDelegate call_SetParticleEffectByNameDelegate;

		// Token: 0x040002B2 RID: 690
		public static ScriptingInterfaceOfIParticleSystem.SetRuntimeEmissionRateMultiplierDelegate call_SetRuntimeEmissionRateMultiplierDelegate;

		// Token: 0x02000305 RID: 773
		// (Invoke) Token: 0x06001117 RID: 4375
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateParticleSystemAttachedToBoneDelegate(int runtimeId, UIntPtr skeletonPtr, sbyte boneIndex, ref MatrixFrame boneLocalFrame);

		// Token: 0x02000306 RID: 774
		// (Invoke) Token: 0x0600111B RID: 4379
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate NativeObjectPointer CreateParticleSystemAttachedToEntityDelegate(int runtimeId, UIntPtr entityPtr, ref MatrixFrame boneLocalFrame);

		// Token: 0x02000307 RID: 775
		// (Invoke) Token: 0x0600111F RID: 4383
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetLocalFrameDelegate(UIntPtr pointer, ref MatrixFrame frame);

		// Token: 0x02000308 RID: 776
		// (Invoke) Token: 0x06001123 RID: 4387
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetRuntimeIdByNameDelegate(byte[] particleSystemName);

		// Token: 0x02000309 RID: 777
		// (Invoke) Token: 0x06001127 RID: 4391
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RestartDelegate(UIntPtr psysPointer);

		// Token: 0x0200030A RID: 778
		// (Invoke) Token: 0x0600112B RID: 4395
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetEnableDelegate(UIntPtr psysPointer, [MarshalAs(UnmanagedType.U1)] bool enable);

		// Token: 0x0200030B RID: 779
		// (Invoke) Token: 0x0600112F RID: 4399
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetLocalFrameDelegate(UIntPtr pointer, ref MatrixFrame newFrame);

		// Token: 0x0200030C RID: 780
		// (Invoke) Token: 0x06001133 RID: 4403
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetParticleEffectByNameDelegate(UIntPtr pointer, byte[] effectName);

		// Token: 0x0200030D RID: 781
		// (Invoke) Token: 0x06001137 RID: 4407
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRuntimeEmissionRateMultiplierDelegate(UIntPtr pointer, float multiplier);
	}
}
