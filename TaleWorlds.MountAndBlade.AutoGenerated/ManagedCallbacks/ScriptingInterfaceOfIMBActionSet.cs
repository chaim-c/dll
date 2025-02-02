using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000009 RID: 9
	internal class ScriptingInterfaceOfIMBActionSet : IMBActionSet
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000088B4 File Offset: 0x00006AB4
		public bool AreActionsAlternatives(int index, int actionNo1, int actionNo2)
		{
			return ScriptingInterfaceOfIMBActionSet.call_AreActionsAlternativesDelegate(index, actionNo1, actionNo2);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000088C3 File Offset: 0x00006AC3
		public string GetAnimationName(int index, int actionNo)
		{
			if (ScriptingInterfaceOfIMBActionSet.call_GetAnimationNameDelegate(index, actionNo) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000088DC File Offset: 0x00006ADC
		public bool GetBoneHasParentBone(string actionSetId, sbyte boneIndex)
		{
			byte[] array = null;
			if (actionSetId != null)
			{
				int byteCount = ScriptingInterfaceOfIMBActionSet._utf8.GetByteCount(actionSetId);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBActionSet._utf8.GetBytes(actionSetId, 0, actionSetId.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBActionSet.call_GetBoneHasParentBoneDelegate(array, boneIndex);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00008938 File Offset: 0x00006B38
		public sbyte GetBoneIndexWithId(string actionSetId, string boneId)
		{
			byte[] array = null;
			if (actionSetId != null)
			{
				int byteCount = ScriptingInterfaceOfIMBActionSet._utf8.GetByteCount(actionSetId);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBActionSet._utf8.GetBytes(actionSetId, 0, actionSetId.Length, array, 0);
				array[byteCount] = 0;
			}
			byte[] array2 = null;
			if (boneId != null)
			{
				int byteCount2 = ScriptingInterfaceOfIMBActionSet._utf8.GetByteCount(boneId);
				array2 = ((byteCount2 < 1024) ? CallbackStringBufferManager.StringBuffer1 : new byte[byteCount2 + 1]);
				ScriptingInterfaceOfIMBActionSet._utf8.GetBytes(boneId, 0, boneId.Length, array2, 0);
				array2[byteCount2] = 0;
			}
			return ScriptingInterfaceOfIMBActionSet.call_GetBoneIndexWithIdDelegate(array, array2);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000089D8 File Offset: 0x00006BD8
		public int GetIndexWithID(string id)
		{
			byte[] array = null;
			if (id != null)
			{
				int byteCount = ScriptingInterfaceOfIMBActionSet._utf8.GetByteCount(id);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBActionSet._utf8.GetBytes(id, 0, id.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBActionSet.call_GetIndexWithIDDelegate(array);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00008A32 File Offset: 0x00006C32
		public string GetNameWithIndex(int index)
		{
			if (ScriptingInterfaceOfIMBActionSet.call_GetNameWithIndexDelegate(index) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00008A49 File Offset: 0x00006C49
		public int GetNumberOfActionSets()
		{
			return ScriptingInterfaceOfIMBActionSet.call_GetNumberOfActionSetsDelegate();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00008A55 File Offset: 0x00006C55
		public int GetNumberOfMonsterUsageSets()
		{
			return ScriptingInterfaceOfIMBActionSet.call_GetNumberOfMonsterUsageSetsDelegate();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00008A61 File Offset: 0x00006C61
		public string GetSkeletonName(int index)
		{
			if (ScriptingInterfaceOfIMBActionSet.call_GetSkeletonNameDelegate(index) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x04000022 RID: 34
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000023 RID: 35
		public static ScriptingInterfaceOfIMBActionSet.AreActionsAlternativesDelegate call_AreActionsAlternativesDelegate;

		// Token: 0x04000024 RID: 36
		public static ScriptingInterfaceOfIMBActionSet.GetAnimationNameDelegate call_GetAnimationNameDelegate;

		// Token: 0x04000025 RID: 37
		public static ScriptingInterfaceOfIMBActionSet.GetBoneHasParentBoneDelegate call_GetBoneHasParentBoneDelegate;

		// Token: 0x04000026 RID: 38
		public static ScriptingInterfaceOfIMBActionSet.GetBoneIndexWithIdDelegate call_GetBoneIndexWithIdDelegate;

		// Token: 0x04000027 RID: 39
		public static ScriptingInterfaceOfIMBActionSet.GetIndexWithIDDelegate call_GetIndexWithIDDelegate;

		// Token: 0x04000028 RID: 40
		public static ScriptingInterfaceOfIMBActionSet.GetNameWithIndexDelegate call_GetNameWithIndexDelegate;

		// Token: 0x04000029 RID: 41
		public static ScriptingInterfaceOfIMBActionSet.GetNumberOfActionSetsDelegate call_GetNumberOfActionSetsDelegate;

		// Token: 0x0400002A RID: 42
		public static ScriptingInterfaceOfIMBActionSet.GetNumberOfMonsterUsageSetsDelegate call_GetNumberOfMonsterUsageSetsDelegate;

		// Token: 0x0400002B RID: 43
		public static ScriptingInterfaceOfIMBActionSet.GetSkeletonNameDelegate call_GetSkeletonNameDelegate;

		// Token: 0x0200008D RID: 141
		// (Invoke) Token: 0x060004D9 RID: 1241
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool AreActionsAlternativesDelegate(int index, int actionNo1, int actionNo2);

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x060004DD RID: 1245
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetAnimationNameDelegate(int index, int actionNo);

		// Token: 0x0200008F RID: 143
		// (Invoke) Token: 0x060004E1 RID: 1249
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetBoneHasParentBoneDelegate(byte[] actionSetId, sbyte boneIndex);

		// Token: 0x02000090 RID: 144
		// (Invoke) Token: 0x060004E5 RID: 1253
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate sbyte GetBoneIndexWithIdDelegate(byte[] actionSetId, byte[] boneId);

		// Token: 0x02000091 RID: 145
		// (Invoke) Token: 0x060004E9 RID: 1257
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetIndexWithIDDelegate(byte[] id);

		// Token: 0x02000092 RID: 146
		// (Invoke) Token: 0x060004ED RID: 1261
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNameWithIndexDelegate(int index);

		// Token: 0x02000093 RID: 147
		// (Invoke) Token: 0x060004F1 RID: 1265
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumberOfActionSetsDelegate();

		// Token: 0x02000094 RID: 148
		// (Invoke) Token: 0x060004F5 RID: 1269
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetNumberOfMonsterUsageSetsDelegate();

		// Token: 0x02000095 RID: 149
		// (Invoke) Token: 0x060004F9 RID: 1273
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetSkeletonNameDelegate(int index);
	}
}
