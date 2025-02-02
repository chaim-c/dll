using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000024 RID: 36
	internal class ScriptingInterfaceOfIMBWorld : IMBWorld
	{
		// Token: 0x0600032B RID: 811 RVA: 0x0000CC71 File Offset: 0x0000AE71
		public void CheckResourceModifications()
		{
			ScriptingInterfaceOfIMBWorld.call_CheckResourceModificationsDelegate();
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000CC7D File Offset: 0x0000AE7D
		public void FixSkeletons()
		{
			ScriptingInterfaceOfIMBWorld.call_FixSkeletonsDelegate();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000CC89 File Offset: 0x0000AE89
		public int GetGameType()
		{
			return ScriptingInterfaceOfIMBWorld.call_GetGameTypeDelegate();
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000CC95 File Offset: 0x0000AE95
		public float GetGlobalTime(MBCommon.TimeType timeType)
		{
			return ScriptingInterfaceOfIMBWorld.call_GetGlobalTimeDelegate(timeType);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000CCA2 File Offset: 0x0000AEA2
		public string GetLastMessages()
		{
			if (ScriptingInterfaceOfIMBWorld.call_GetLastMessagesDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000CCB8 File Offset: 0x0000AEB8
		public void PauseGame()
		{
			ScriptingInterfaceOfIMBWorld.call_PauseGameDelegate();
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000CCC4 File Offset: 0x0000AEC4
		public void SetBodyUsed(string bodyName)
		{
			byte[] array = null;
			if (bodyName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBWorld._utf8.GetByteCount(bodyName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBWorld._utf8.GetBytes(bodyName, 0, bodyName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBWorld.call_SetBodyUsedDelegate(array);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000CD1E File Offset: 0x0000AF1E
		public void SetGameType(int gameType)
		{
			ScriptingInterfaceOfIMBWorld.call_SetGameTypeDelegate(gameType);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		public void SetMaterialUsed(string materialName)
		{
			byte[] array = null;
			if (materialName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBWorld._utf8.GetByteCount(materialName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBWorld._utf8.GetBytes(materialName, 0, materialName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBWorld.call_SetMaterialUsedDelegate(array);
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000CD88 File Offset: 0x0000AF88
		public void SetMeshUsed(string meshName)
		{
			byte[] array = null;
			if (meshName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBWorld._utf8.GetByteCount(meshName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBWorld._utf8.GetBytes(meshName, 0, meshName.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBWorld.call_SetMeshUsedDelegate(array);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000CDE2 File Offset: 0x0000AFE2
		public void UnpauseGame()
		{
			ScriptingInterfaceOfIMBWorld.call_UnpauseGameDelegate();
		}

		// Token: 0x040002A9 RID: 681
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040002AA RID: 682
		public static ScriptingInterfaceOfIMBWorld.CheckResourceModificationsDelegate call_CheckResourceModificationsDelegate;

		// Token: 0x040002AB RID: 683
		public static ScriptingInterfaceOfIMBWorld.FixSkeletonsDelegate call_FixSkeletonsDelegate;

		// Token: 0x040002AC RID: 684
		public static ScriptingInterfaceOfIMBWorld.GetGameTypeDelegate call_GetGameTypeDelegate;

		// Token: 0x040002AD RID: 685
		public static ScriptingInterfaceOfIMBWorld.GetGlobalTimeDelegate call_GetGlobalTimeDelegate;

		// Token: 0x040002AE RID: 686
		public static ScriptingInterfaceOfIMBWorld.GetLastMessagesDelegate call_GetLastMessagesDelegate;

		// Token: 0x040002AF RID: 687
		public static ScriptingInterfaceOfIMBWorld.PauseGameDelegate call_PauseGameDelegate;

		// Token: 0x040002B0 RID: 688
		public static ScriptingInterfaceOfIMBWorld.SetBodyUsedDelegate call_SetBodyUsedDelegate;

		// Token: 0x040002B1 RID: 689
		public static ScriptingInterfaceOfIMBWorld.SetGameTypeDelegate call_SetGameTypeDelegate;

		// Token: 0x040002B2 RID: 690
		public static ScriptingInterfaceOfIMBWorld.SetMaterialUsedDelegate call_SetMaterialUsedDelegate;

		// Token: 0x040002B3 RID: 691
		public static ScriptingInterfaceOfIMBWorld.SetMeshUsedDelegate call_SetMeshUsedDelegate;

		// Token: 0x040002B4 RID: 692
		public static ScriptingInterfaceOfIMBWorld.UnpauseGameDelegate call_UnpauseGameDelegate;

		// Token: 0x020002F9 RID: 761
		// (Invoke) Token: 0x06000E89 RID: 3721
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void CheckResourceModificationsDelegate();

		// Token: 0x020002FA RID: 762
		// (Invoke) Token: 0x06000E8D RID: 3725
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void FixSkeletonsDelegate();

		// Token: 0x020002FB RID: 763
		// (Invoke) Token: 0x06000E91 RID: 3729
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetGameTypeDelegate();

		// Token: 0x020002FC RID: 764
		// (Invoke) Token: 0x06000E95 RID: 3733
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetGlobalTimeDelegate(MBCommon.TimeType timeType);

		// Token: 0x020002FD RID: 765
		// (Invoke) Token: 0x06000E99 RID: 3737
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetLastMessagesDelegate();

		// Token: 0x020002FE RID: 766
		// (Invoke) Token: 0x06000E9D RID: 3741
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PauseGameDelegate();

		// Token: 0x020002FF RID: 767
		// (Invoke) Token: 0x06000EA1 RID: 3745
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetBodyUsedDelegate(byte[] bodyName);

		// Token: 0x02000300 RID: 768
		// (Invoke) Token: 0x06000EA5 RID: 3749
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetGameTypeDelegate(int gameType);

		// Token: 0x02000301 RID: 769
		// (Invoke) Token: 0x06000EA9 RID: 3753
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMaterialUsedDelegate(byte[] materialName);

		// Token: 0x02000302 RID: 770
		// (Invoke) Token: 0x06000EAD RID: 3757
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetMeshUsedDelegate(byte[] meshName);

		// Token: 0x02000303 RID: 771
		// (Invoke) Token: 0x06000EB1 RID: 3761
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void UnpauseGameDelegate();
	}
}
