using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000022 RID: 34
	internal class ScriptingInterfaceOfIMBVoiceManager : IMBVoiceManager
	{
		// Token: 0x0600031E RID: 798 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		public int GetVoiceDefinitionCountWithMonsterSoundAndCollisionInfoClassName(string className)
		{
			byte[] array = null;
			if (className != null)
			{
				int byteCount = ScriptingInterfaceOfIMBVoiceManager._utf8.GetByteCount(className);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBVoiceManager._utf8.GetBytes(className, 0, className.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBVoiceManager.call_GetVoiceDefinitionCountWithMonsterSoundAndCollisionInfoClassNameDelegate(array);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000CB1C File Offset: 0x0000AD1C
		public void GetVoiceDefinitionListWithMonsterSoundAndCollisionInfoClassName(string className, int[] definitionIndices)
		{
			byte[] array = null;
			if (className != null)
			{
				int byteCount = ScriptingInterfaceOfIMBVoiceManager._utf8.GetByteCount(className);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBVoiceManager._utf8.GetBytes(className, 0, className.Length, array, 0);
				array[byteCount] = 0;
			}
			PinnedArrayData<int> pinnedArrayData = new PinnedArrayData<int>(definitionIndices, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ScriptingInterfaceOfIMBVoiceManager.call_GetVoiceDefinitionListWithMonsterSoundAndCollisionInfoClassNameDelegate(array, pointer);
			pinnedArrayData.Dispose();
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000CB90 File Offset: 0x0000AD90
		public int GetVoiceTypeIndex(string voiceType)
		{
			byte[] array = null;
			if (voiceType != null)
			{
				int byteCount = ScriptingInterfaceOfIMBVoiceManager._utf8.GetByteCount(voiceType);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBVoiceManager._utf8.GetBytes(voiceType, 0, voiceType.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBVoiceManager.call_GetVoiceTypeIndexDelegate(array);
		}

		// Token: 0x0400029E RID: 670
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400029F RID: 671
		public static ScriptingInterfaceOfIMBVoiceManager.GetVoiceDefinitionCountWithMonsterSoundAndCollisionInfoClassNameDelegate call_GetVoiceDefinitionCountWithMonsterSoundAndCollisionInfoClassNameDelegate;

		// Token: 0x040002A0 RID: 672
		public static ScriptingInterfaceOfIMBVoiceManager.GetVoiceDefinitionListWithMonsterSoundAndCollisionInfoClassNameDelegate call_GetVoiceDefinitionListWithMonsterSoundAndCollisionInfoClassNameDelegate;

		// Token: 0x040002A1 RID: 673
		public static ScriptingInterfaceOfIMBVoiceManager.GetVoiceTypeIndexDelegate call_GetVoiceTypeIndexDelegate;

		// Token: 0x020002F0 RID: 752
		// (Invoke) Token: 0x06000E65 RID: 3685
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVoiceDefinitionCountWithMonsterSoundAndCollisionInfoClassNameDelegate(byte[] className);

		// Token: 0x020002F1 RID: 753
		// (Invoke) Token: 0x06000E69 RID: 3689
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetVoiceDefinitionListWithMonsterSoundAndCollisionInfoClassNameDelegate(byte[] className, IntPtr definitionIndices);

		// Token: 0x020002F2 RID: 754
		// (Invoke) Token: 0x06000E6D RID: 3693
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetVoiceTypeIndexDelegate(byte[] voiceType);
	}
}
