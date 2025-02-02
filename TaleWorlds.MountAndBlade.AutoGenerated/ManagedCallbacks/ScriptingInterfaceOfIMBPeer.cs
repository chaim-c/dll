using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x0200001C RID: 28
	internal class ScriptingInterfaceOfIMBPeer : IMBPeer
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x0000C3E6 File Offset: 0x0000A5E6
		public void BeginModuleEvent(int index, bool isReliable)
		{
			ScriptingInterfaceOfIMBPeer.call_BeginModuleEventDelegate(index, isReliable);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000C3F4 File Offset: 0x0000A5F4
		public void EndModuleEvent(bool isReliable)
		{
			ScriptingInterfaceOfIMBPeer.call_EndModuleEventDelegate(isReliable);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000C401 File Offset: 0x0000A601
		public double GetAverageLossPercent(int index)
		{
			return ScriptingInterfaceOfIMBPeer.call_GetAverageLossPercentDelegate(index);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000C40E File Offset: 0x0000A60E
		public double GetAveragePingInMilliseconds(int index)
		{
			return ScriptingInterfaceOfIMBPeer.call_GetAveragePingInMillisecondsDelegate(index);
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000C41B File Offset: 0x0000A61B
		public uint GetHost(int index)
		{
			return ScriptingInterfaceOfIMBPeer.call_GetHostDelegate(index);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000C428 File Offset: 0x0000A628
		public bool GetIsSynchronized(int index)
		{
			return ScriptingInterfaceOfIMBPeer.call_GetIsSynchronizedDelegate(index);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000C435 File Offset: 0x0000A635
		public ushort GetPort(int index)
		{
			return ScriptingInterfaceOfIMBPeer.call_GetPortDelegate(index);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000C442 File Offset: 0x0000A642
		public uint GetReversedHost(int index)
		{
			return ScriptingInterfaceOfIMBPeer.call_GetReversedHostDelegate(index);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000C44F File Offset: 0x0000A64F
		public bool IsActive(int index)
		{
			return ScriptingInterfaceOfIMBPeer.call_IsActiveDelegate(index);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000C45C File Offset: 0x0000A65C
		public void SendExistingObjects(int index, UIntPtr missionPointer)
		{
			ScriptingInterfaceOfIMBPeer.call_SendExistingObjectsDelegate(index, missionPointer);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000C46A File Offset: 0x0000A66A
		public void SetControlledAgent(int index, UIntPtr missionPointer, int agentIndex)
		{
			ScriptingInterfaceOfIMBPeer.call_SetControlledAgentDelegate(index, missionPointer, agentIndex);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000C479 File Offset: 0x0000A679
		public void SetIsSynchronized(int index, bool value)
		{
			ScriptingInterfaceOfIMBPeer.call_SetIsSynchronizedDelegate(index, value);
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0000C487 File Offset: 0x0000A687
		public void SetRelevantGameOptions(int index, bool sendMeBloodEvents, bool sendMeSoundEvents)
		{
			ScriptingInterfaceOfIMBPeer.call_SetRelevantGameOptionsDelegate(index, sendMeBloodEvents, sendMeSoundEvents);
		}

		// Token: 0x060002ED RID: 749 RVA: 0x0000C496 File Offset: 0x0000A696
		public void SetTeam(int index, int teamIndex)
		{
			ScriptingInterfaceOfIMBPeer.call_SetTeamDelegate(index, teamIndex);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		public void SetUserData(int index, MBNetworkPeer data)
		{
			ScriptingInterfaceOfIMBPeer.call_SetUserDataDelegate(index, (data != null) ? data.GetManagedId() : 0);
		}

		// Token: 0x04000266 RID: 614
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x04000267 RID: 615
		public static ScriptingInterfaceOfIMBPeer.BeginModuleEventDelegate call_BeginModuleEventDelegate;

		// Token: 0x04000268 RID: 616
		public static ScriptingInterfaceOfIMBPeer.EndModuleEventDelegate call_EndModuleEventDelegate;

		// Token: 0x04000269 RID: 617
		public static ScriptingInterfaceOfIMBPeer.GetAverageLossPercentDelegate call_GetAverageLossPercentDelegate;

		// Token: 0x0400026A RID: 618
		public static ScriptingInterfaceOfIMBPeer.GetAveragePingInMillisecondsDelegate call_GetAveragePingInMillisecondsDelegate;

		// Token: 0x0400026B RID: 619
		public static ScriptingInterfaceOfIMBPeer.GetHostDelegate call_GetHostDelegate;

		// Token: 0x0400026C RID: 620
		public static ScriptingInterfaceOfIMBPeer.GetIsSynchronizedDelegate call_GetIsSynchronizedDelegate;

		// Token: 0x0400026D RID: 621
		public static ScriptingInterfaceOfIMBPeer.GetPortDelegate call_GetPortDelegate;

		// Token: 0x0400026E RID: 622
		public static ScriptingInterfaceOfIMBPeer.GetReversedHostDelegate call_GetReversedHostDelegate;

		// Token: 0x0400026F RID: 623
		public static ScriptingInterfaceOfIMBPeer.IsActiveDelegate call_IsActiveDelegate;

		// Token: 0x04000270 RID: 624
		public static ScriptingInterfaceOfIMBPeer.SendExistingObjectsDelegate call_SendExistingObjectsDelegate;

		// Token: 0x04000271 RID: 625
		public static ScriptingInterfaceOfIMBPeer.SetControlledAgentDelegate call_SetControlledAgentDelegate;

		// Token: 0x04000272 RID: 626
		public static ScriptingInterfaceOfIMBPeer.SetIsSynchronizedDelegate call_SetIsSynchronizedDelegate;

		// Token: 0x04000273 RID: 627
		public static ScriptingInterfaceOfIMBPeer.SetRelevantGameOptionsDelegate call_SetRelevantGameOptionsDelegate;

		// Token: 0x04000274 RID: 628
		public static ScriptingInterfaceOfIMBPeer.SetTeamDelegate call_SetTeamDelegate;

		// Token: 0x04000275 RID: 629
		public static ScriptingInterfaceOfIMBPeer.SetUserDataDelegate call_SetUserDataDelegate;

		// Token: 0x020002BE RID: 702
		// (Invoke) Token: 0x06000D9D RID: 3485
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BeginModuleEventDelegate(int index, [MarshalAs(UnmanagedType.U1)] bool isReliable);

		// Token: 0x020002BF RID: 703
		// (Invoke) Token: 0x06000DA1 RID: 3489
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EndModuleEventDelegate([MarshalAs(UnmanagedType.U1)] bool isReliable);

		// Token: 0x020002C0 RID: 704
		// (Invoke) Token: 0x06000DA5 RID: 3493
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate double GetAverageLossPercentDelegate(int index);

		// Token: 0x020002C1 RID: 705
		// (Invoke) Token: 0x06000DA9 RID: 3497
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate double GetAveragePingInMillisecondsDelegate(int index);

		// Token: 0x020002C2 RID: 706
		// (Invoke) Token: 0x06000DAD RID: 3501
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetHostDelegate(int index);

		// Token: 0x020002C3 RID: 707
		// (Invoke) Token: 0x06000DB1 RID: 3505
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetIsSynchronizedDelegate(int index);

		// Token: 0x020002C4 RID: 708
		// (Invoke) Token: 0x06000DB5 RID: 3509
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate ushort GetPortDelegate(int index);

		// Token: 0x020002C5 RID: 709
		// (Invoke) Token: 0x06000DB9 RID: 3513
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate uint GetReversedHostDelegate(int index);

		// Token: 0x020002C6 RID: 710
		// (Invoke) Token: 0x06000DBD RID: 3517
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsActiveDelegate(int index);

		// Token: 0x020002C7 RID: 711
		// (Invoke) Token: 0x06000DC1 RID: 3521
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SendExistingObjectsDelegate(int index, UIntPtr missionPointer);

		// Token: 0x020002C8 RID: 712
		// (Invoke) Token: 0x06000DC5 RID: 3525
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetControlledAgentDelegate(int index, UIntPtr missionPointer, int agentIndex);

		// Token: 0x020002C9 RID: 713
		// (Invoke) Token: 0x06000DC9 RID: 3529
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetIsSynchronizedDelegate(int index, [MarshalAs(UnmanagedType.U1)] bool value);

		// Token: 0x020002CA RID: 714
		// (Invoke) Token: 0x06000DCD RID: 3533
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetRelevantGameOptionsDelegate(int index, [MarshalAs(UnmanagedType.U1)] bool sendMeBloodEvents, [MarshalAs(UnmanagedType.U1)] bool sendMeSoundEvents);

		// Token: 0x020002CB RID: 715
		// (Invoke) Token: 0x06000DD1 RID: 3537
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetTeamDelegate(int index, int teamIndex);

		// Token: 0x020002CC RID: 716
		// (Invoke) Token: 0x06000DD5 RID: 3541
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetUserDataDelegate(int index, int data);
	}
}
