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
	// Token: 0x0200001F RID: 31
	internal class ScriptingInterfaceOfIMBSoundEvent : IMBSoundEvent
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000C7E4 File Offset: 0x0000A9E4
		public int CreateEventFromExternalFile(string programmerSoundEventName, string filePath, UIntPtr scene)
		{
			byte[] array = null;
			if (programmerSoundEventName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBSoundEvent._utf8.GetByteCount(programmerSoundEventName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBSoundEvent._utf8.GetBytes(programmerSoundEventName, 0, programmerSoundEventName.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (filePath != null)
			{
				int byteCount2 = ScriptingInterfaceOfIMBSoundEvent._utf8.GetByteCount(filePath);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIMBSoundEvent._utf8.GetBytes(filePath, 0, filePath.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			return ScriptingInterfaceOfIMBSoundEvent.call_CreateEventFromExternalFileDelegate(array, array2, scene);
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000C884 File Offset: 0x0000AA84
		public int CreateEventFromSoundBuffer(string programmerSoundEventName, byte[] soundBuffer, UIntPtr scene)
		{
			byte[] array = null;
			if (programmerSoundEventName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBSoundEvent._utf8.GetByteCount(programmerSoundEventName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBSoundEvent._utf8.GetBytes(programmerSoundEventName, 0, programmerSoundEventName.Length, array, 0);
				array[byteCount] = 0;
			}
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(soundBuffer, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray soundBuffer2 = new ManagedArray(pointer, (soundBuffer != null) ? soundBuffer.Length : 0);
			int result = ScriptingInterfaceOfIMBSoundEvent.call_CreateEventFromSoundBufferDelegate(array, soundBuffer2, scene);
			pinnedArrayData.Dispose();
			return result;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000C90D File Offset: 0x0000AB0D
		public bool PlaySound(int fmodEventIndex, ref Vec3 position)
		{
			return ScriptingInterfaceOfIMBSoundEvent.call_PlaySoundDelegate(fmodEventIndex, ref position);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000C91B File Offset: 0x0000AB1B
		public bool PlaySoundWithIntParam(int fmodEventIndex, int paramIndex, float paramVal, ref Vec3 position)
		{
			return ScriptingInterfaceOfIMBSoundEvent.call_PlaySoundWithIntParamDelegate(fmodEventIndex, paramIndex, paramVal, ref position);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000C92C File Offset: 0x0000AB2C
		public bool PlaySoundWithParam(int soundCodeId, SoundEventParameter parameter, ref Vec3 position)
		{
			return ScriptingInterfaceOfIMBSoundEvent.call_PlaySoundWithParamDelegate(soundCodeId, parameter, ref position);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000C93C File Offset: 0x0000AB3C
		public bool PlaySoundWithStrParam(int fmodEventIndex, string paramName, float paramVal, ref Vec3 position)
		{
			byte[] array = null;
			if (paramName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBSoundEvent._utf8.GetByteCount(paramName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBSoundEvent._utf8.GetBytes(paramName, 0, paramName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBSoundEvent.call_PlaySoundWithStrParamDelegate(fmodEventIndex, array, paramVal, ref position);
		}

		// Token: 0x04000289 RID: 649
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400028A RID: 650
		public static ScriptingInterfaceOfIMBSoundEvent.CreateEventFromExternalFileDelegate call_CreateEventFromExternalFileDelegate;

		// Token: 0x0400028B RID: 651
		public static ScriptingInterfaceOfIMBSoundEvent.CreateEventFromSoundBufferDelegate call_CreateEventFromSoundBufferDelegate;

		// Token: 0x0400028C RID: 652
		public static ScriptingInterfaceOfIMBSoundEvent.PlaySoundDelegate call_PlaySoundDelegate;

		// Token: 0x0400028D RID: 653
		public static ScriptingInterfaceOfIMBSoundEvent.PlaySoundWithIntParamDelegate call_PlaySoundWithIntParamDelegate;

		// Token: 0x0400028E RID: 654
		public static ScriptingInterfaceOfIMBSoundEvent.PlaySoundWithParamDelegate call_PlaySoundWithParamDelegate;

		// Token: 0x0400028F RID: 655
		public static ScriptingInterfaceOfIMBSoundEvent.PlaySoundWithStrParamDelegate call_PlaySoundWithStrParamDelegate;

		// Token: 0x020002DE RID: 734
		// (Invoke) Token: 0x06000E1D RID: 3613
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int CreateEventFromExternalFileDelegate(byte[] programmerSoundEventName, byte[] filePath, UIntPtr scene);

		// Token: 0x020002DF RID: 735
		// (Invoke) Token: 0x06000E21 RID: 3617
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int CreateEventFromSoundBufferDelegate(byte[] programmerSoundEventName, ManagedArray soundBuffer, UIntPtr scene);

		// Token: 0x020002E0 RID: 736
		// (Invoke) Token: 0x06000E25 RID: 3621
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool PlaySoundDelegate(int fmodEventIndex, ref Vec3 position);

		// Token: 0x020002E1 RID: 737
		// (Invoke) Token: 0x06000E29 RID: 3625
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool PlaySoundWithIntParamDelegate(int fmodEventIndex, int paramIndex, float paramVal, ref Vec3 position);

		// Token: 0x020002E2 RID: 738
		// (Invoke) Token: 0x06000E2D RID: 3629
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool PlaySoundWithParamDelegate(int soundCodeId, SoundEventParameter parameter, ref Vec3 position);

		// Token: 0x020002E3 RID: 739
		// (Invoke) Token: 0x06000E31 RID: 3633
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool PlaySoundWithStrParamDelegate(int fmodEventIndex, byte[] paramName, float paramVal, ref Vec3 position);
	}
}
