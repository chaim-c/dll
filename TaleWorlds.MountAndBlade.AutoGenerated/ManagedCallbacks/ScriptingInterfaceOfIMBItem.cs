using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x02000016 RID: 22
	internal class ScriptingInterfaceOfIMBItem : IMBItem
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public void GetHolsterFrameByIndex(int index, ref MatrixFrame outFrame)
		{
			ScriptingInterfaceOfIMBItem.call_GetHolsterFrameByIndexDelegate(index, ref outFrame);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		public int GetItemHolsterIndex(string itemholstername)
		{
			byte[] array = null;
			if (itemholstername != null)
			{
				int byteCount = ScriptingInterfaceOfIMBItem._utf8.GetByteCount(itemholstername);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBItem._utf8.GetBytes(itemholstername, 0, itemholstername.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBItem.call_GetItemHolsterIndexDelegate(array);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000AE4C File Offset: 0x0000904C
		public bool GetItemIsPassiveUsage(string itemUsageName)
		{
			byte[] array = null;
			if (itemUsageName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBItem._utf8.GetByteCount(itemUsageName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBItem._utf8.GetBytes(itemUsageName, 0, itemUsageName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBItem.call_GetItemIsPassiveUsageDelegate(array);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000AEA8 File Offset: 0x000090A8
		public int GetItemUsageIndex(string itemusagename)
		{
			byte[] array = null;
			if (itemusagename != null)
			{
				int byteCount = ScriptingInterfaceOfIMBItem._utf8.GetByteCount(itemusagename);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBItem._utf8.GetBytes(itemusagename, 0, itemusagename.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBItem.call_GetItemUsageIndexDelegate(array);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000AF04 File Offset: 0x00009104
		public int GetItemUsageReloadActionCode(string itemUsageName, int usageDirection, bool isMounted, int leftHandUsageSetIndex, bool isLeftStance)
		{
			byte[] array = null;
			if (itemUsageName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBItem._utf8.GetByteCount(itemUsageName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBItem._utf8.GetBytes(itemUsageName, 0, itemUsageName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBItem.call_GetItemUsageReloadActionCodeDelegate(array, usageDirection, isMounted, leftHandUsageSetIndex, isLeftStance);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000AF64 File Offset: 0x00009164
		public int GetItemUsageSetFlags(string ItemUsageName)
		{
			byte[] array = null;
			if (ItemUsageName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBItem._utf8.GetByteCount(ItemUsageName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBItem._utf8.GetBytes(ItemUsageName, 0, ItemUsageName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBItem.call_GetItemUsageSetFlagsDelegate(array);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000AFC0 File Offset: 0x000091C0
		public int GetItemUsageStrikeType(string itemUsageName, int usageDirection, bool isMounted, int leftHandUsageSetIndex, bool isLeftStance)
		{
			byte[] array = null;
			if (itemUsageName != null)
			{
				int byteCount = ScriptingInterfaceOfIMBItem._utf8.GetByteCount(itemUsageName);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBItem._utf8.GetBytes(itemUsageName, 0, itemUsageName.Length, array, 0);
				array[byteCount] = 0;
			}
			return ScriptingInterfaceOfIMBItem.call_GetItemUsageStrikeTypeDelegate(array, usageDirection, isMounted, leftHandUsageSetIndex, isLeftStance);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000B020 File Offset: 0x00009220
		public float GetMissileRange(float shot_speed, float z_diff)
		{
			return ScriptingInterfaceOfIMBItem.call_GetMissileRangeDelegate(shot_speed, z_diff);
		}

		// Token: 0x040001A9 RID: 425
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x040001AA RID: 426
		public static ScriptingInterfaceOfIMBItem.GetHolsterFrameByIndexDelegate call_GetHolsterFrameByIndexDelegate;

		// Token: 0x040001AB RID: 427
		public static ScriptingInterfaceOfIMBItem.GetItemHolsterIndexDelegate call_GetItemHolsterIndexDelegate;

		// Token: 0x040001AC RID: 428
		public static ScriptingInterfaceOfIMBItem.GetItemIsPassiveUsageDelegate call_GetItemIsPassiveUsageDelegate;

		// Token: 0x040001AD RID: 429
		public static ScriptingInterfaceOfIMBItem.GetItemUsageIndexDelegate call_GetItemUsageIndexDelegate;

		// Token: 0x040001AE RID: 430
		public static ScriptingInterfaceOfIMBItem.GetItemUsageReloadActionCodeDelegate call_GetItemUsageReloadActionCodeDelegate;

		// Token: 0x040001AF RID: 431
		public static ScriptingInterfaceOfIMBItem.GetItemUsageSetFlagsDelegate call_GetItemUsageSetFlagsDelegate;

		// Token: 0x040001B0 RID: 432
		public static ScriptingInterfaceOfIMBItem.GetItemUsageStrikeTypeDelegate call_GetItemUsageStrikeTypeDelegate;

		// Token: 0x040001B1 RID: 433
		public static ScriptingInterfaceOfIMBItem.GetMissileRangeDelegate call_GetMissileRangeDelegate;

		// Token: 0x02000207 RID: 519
		// (Invoke) Token: 0x06000AC1 RID: 2753
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetHolsterFrameByIndexDelegate(int index, ref MatrixFrame outFrame);

		// Token: 0x02000208 RID: 520
		// (Invoke) Token: 0x06000AC5 RID: 2757
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetItemHolsterIndexDelegate(byte[] itemholstername);

		// Token: 0x02000209 RID: 521
		// (Invoke) Token: 0x06000AC9 RID: 2761
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetItemIsPassiveUsageDelegate(byte[] itemUsageName);

		// Token: 0x0200020A RID: 522
		// (Invoke) Token: 0x06000ACD RID: 2765
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetItemUsageIndexDelegate(byte[] itemusagename);

		// Token: 0x0200020B RID: 523
		// (Invoke) Token: 0x06000AD1 RID: 2769
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetItemUsageReloadActionCodeDelegate(byte[] itemUsageName, int usageDirection, [MarshalAs(UnmanagedType.U1)] bool isMounted, int leftHandUsageSetIndex, [MarshalAs(UnmanagedType.U1)] bool isLeftStance);

		// Token: 0x0200020C RID: 524
		// (Invoke) Token: 0x06000AD5 RID: 2773
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetItemUsageSetFlagsDelegate(byte[] ItemUsageName);

		// Token: 0x0200020D RID: 525
		// (Invoke) Token: 0x06000AD9 RID: 2777
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetItemUsageStrikeTypeDelegate(byte[] itemUsageName, int usageDirection, [MarshalAs(UnmanagedType.U1)] bool isMounted, int leftHandUsageSetIndex, [MarshalAs(UnmanagedType.U1)] bool isLeftStance);

		// Token: 0x0200020E RID: 526
		// (Invoke) Token: 0x06000ADD RID: 2781
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetMissileRangeDelegate(float shot_speed, float z_diff);
	}
}
