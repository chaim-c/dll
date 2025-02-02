using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using TaleWorlds.DotNet;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade;

namespace ManagedCallbacks
{
	// Token: 0x0200001B RID: 27
	internal class ScriptingInterfaceOfIMBNetwork : IMBNetwork
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000C01F File Offset: 0x0000A21F
		public int AddNewBotOnServer()
		{
			return ScriptingInterfaceOfIMBNetwork.call_AddNewBotOnServerDelegate();
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000C02B File Offset: 0x0000A22B
		public int AddNewPlayerOnServer(bool serverPlayer)
		{
			return ScriptingInterfaceOfIMBNetwork.call_AddNewPlayerOnServerDelegate(serverPlayer);
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C038 File Offset: 0x0000A238
		public void AddPeerToDisconnect(int peer)
		{
			ScriptingInterfaceOfIMBNetwork.call_AddPeerToDisconnectDelegate(peer);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000C045 File Offset: 0x0000A245
		public void BeginBroadcastModuleEvent()
		{
			ScriptingInterfaceOfIMBNetwork.call_BeginBroadcastModuleEventDelegate();
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C051 File Offset: 0x0000A251
		public void BeginModuleEventAsClient(bool isReliable)
		{
			ScriptingInterfaceOfIMBNetwork.call_BeginModuleEventAsClientDelegate(isReliable);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C05E File Offset: 0x0000A25E
		public bool CanAddNewPlayersOnServer(int numPlayers)
		{
			return ScriptingInterfaceOfIMBNetwork.call_CanAddNewPlayersOnServerDelegate(numPlayers);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000C06B File Offset: 0x0000A26B
		public void ClearReplicationTableStatistics()
		{
			ScriptingInterfaceOfIMBNetwork.call_ClearReplicationTableStatisticsDelegate();
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000C077 File Offset: 0x0000A277
		public double ElapsedTimeSinceLastUdpPacketArrived()
		{
			return ScriptingInterfaceOfIMBNetwork.call_ElapsedTimeSinceLastUdpPacketArrivedDelegate();
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000C083 File Offset: 0x0000A283
		public void EndBroadcastModuleEvent(int broadcastFlags, int targetPlayer, bool isReliable)
		{
			ScriptingInterfaceOfIMBNetwork.call_EndBroadcastModuleEventDelegate(broadcastFlags, targetPlayer, isReliable);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000C092 File Offset: 0x0000A292
		public void EndModuleEventAsClient(bool isReliable)
		{
			ScriptingInterfaceOfIMBNetwork.call_EndModuleEventAsClientDelegate(isReliable);
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000C09F File Offset: 0x0000A29F
		public string GetActiveUdpSessionsIpAddress()
		{
			if (ScriptingInterfaceOfIMBNetwork.call_GetActiveUdpSessionsIpAddressDelegate() != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000C0B5 File Offset: 0x0000A2B5
		public float GetAveragePacketLossRatio()
		{
			return ScriptingInterfaceOfIMBNetwork.call_GetAveragePacketLossRatioDelegate();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
		public void GetDebugUploadsInBits(ref GameNetwork.DebugNetworkPacketStatisticsStruct networkStatisticsStruct, ref GameNetwork.DebugNetworkPositionCompressionStatisticsStruct posStatisticsStruct)
		{
			ScriptingInterfaceOfIMBNetwork.call_GetDebugUploadsInBitsDelegate(ref networkStatisticsStruct, ref posStatisticsStruct);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000C0CF File Offset: 0x0000A2CF
		public bool GetMultiplayerDisabled()
		{
			return ScriptingInterfaceOfIMBNetwork.call_GetMultiplayerDisabledDelegate();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C0DC File Offset: 0x0000A2DC
		public void InitializeClientSide(string serverAddress, int port, int sessionKey, int playerIndex)
		{
			byte[] array = null;
			if (serverAddress != null)
			{
				int byteCount = ScriptingInterfaceOfIMBNetwork._utf8.GetByteCount(serverAddress);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBNetwork._utf8.GetBytes(serverAddress, 0, serverAddress.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBNetwork.call_InitializeClientSideDelegate(array, port, sessionKey, playerIndex);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000C13A File Offset: 0x0000A33A
		public void InitializeServerSide(int port)
		{
			ScriptingInterfaceOfIMBNetwork.call_InitializeServerSideDelegate(port);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C147 File Offset: 0x0000A347
		public bool IsDedicatedServer()
		{
			return ScriptingInterfaceOfIMBNetwork.call_IsDedicatedServerDelegate();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000C153 File Offset: 0x0000A353
		public void PrepareNewUdpSession(int player, int sessionKey)
		{
			ScriptingInterfaceOfIMBNetwork.call_PrepareNewUdpSessionDelegate(player, sessionKey);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000C161 File Offset: 0x0000A361
		public void PrintDebugStats()
		{
			ScriptingInterfaceOfIMBNetwork.call_PrintDebugStatsDelegate();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000C16D File Offset: 0x0000A36D
		public void PrintReplicationTableStatistics()
		{
			ScriptingInterfaceOfIMBNetwork.call_PrintReplicationTableStatisticsDelegate();
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000C17C File Offset: 0x0000A37C
		public int ReadByteArrayFromPacket(byte[] buffer, int offset, int bufferCapacity, ref bool bufferReadValid)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(buffer, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray buffer2 = new ManagedArray(pointer, (buffer != null) ? buffer.Length : 0);
			int result = ScriptingInterfaceOfIMBNetwork.call_ReadByteArrayFromPacketDelegate(buffer2, offset, bufferCapacity, ref bufferReadValid);
			pinnedArrayData.Dispose();
			return result;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
		public bool ReadFloatFromPacket(ref CompressionInfo.Float compressionInfo, out float output)
		{
			return ScriptingInterfaceOfIMBNetwork.call_ReadFloatFromPacketDelegate(ref compressionInfo, out output);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000C1CF File Offset: 0x0000A3CF
		public bool ReadIntFromPacket(ref CompressionInfo.Integer compressionInfo, out int output)
		{
			return ScriptingInterfaceOfIMBNetwork.call_ReadIntFromPacketDelegate(ref compressionInfo, out output);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000C1DD File Offset: 0x0000A3DD
		public bool ReadLongFromPacket(ref CompressionInfo.LongInteger compressionInfo, out long output)
		{
			return ScriptingInterfaceOfIMBNetwork.call_ReadLongFromPacketDelegate(ref compressionInfo, out output);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000C1EB File Offset: 0x0000A3EB
		public string ReadStringFromPacket(ref bool bufferReadValid)
		{
			if (ScriptingInterfaceOfIMBNetwork.call_ReadStringFromPacketDelegate(ref bufferReadValid) != 1)
			{
				return null;
			}
			return Managed.ReturnValueFromEngine;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000C202 File Offset: 0x0000A402
		public bool ReadUintFromPacket(ref CompressionInfo.UnsignedInteger compressionInfo, out uint output)
		{
			return ScriptingInterfaceOfIMBNetwork.call_ReadUintFromPacketDelegate(ref compressionInfo, out output);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000C210 File Offset: 0x0000A410
		public bool ReadUlongFromPacket(ref CompressionInfo.UnsignedLongInteger compressionInfo, out ulong output)
		{
			return ScriptingInterfaceOfIMBNetwork.call_ReadUlongFromPacketDelegate(ref compressionInfo, out output);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000C21E File Offset: 0x0000A41E
		public void RemoveBotOnServer(int botPlayerIndex)
		{
			ScriptingInterfaceOfIMBNetwork.call_RemoveBotOnServerDelegate(botPlayerIndex);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000C22B File Offset: 0x0000A42B
		public void ResetDebugUploads()
		{
			ScriptingInterfaceOfIMBNetwork.call_ResetDebugUploadsDelegate();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000C237 File Offset: 0x0000A437
		public void ResetDebugVariables()
		{
			ScriptingInterfaceOfIMBNetwork.call_ResetDebugVariablesDelegate();
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000C243 File Offset: 0x0000A443
		public void ResetMissionData()
		{
			ScriptingInterfaceOfIMBNetwork.call_ResetMissionDataDelegate();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000C250 File Offset: 0x0000A450
		public void ServerPing(string serverAddress, int port)
		{
			byte[] array = null;
			if (serverAddress != null)
			{
				int byteCount = ScriptingInterfaceOfIMBNetwork._utf8.GetByteCount(serverAddress);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBNetwork._utf8.GetBytes(serverAddress, 0, serverAddress.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBNetwork.call_ServerPingDelegate(array, port);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000C2AB File Offset: 0x0000A4AB
		public void SetServerBandwidthLimitInMbps(double value)
		{
			ScriptingInterfaceOfIMBNetwork.call_SetServerBandwidthLimitInMbpsDelegate(value);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000C2B8 File Offset: 0x0000A4B8
		public void SetServerFrameRate(double limit)
		{
			ScriptingInterfaceOfIMBNetwork.call_SetServerFrameRateDelegate(limit);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000C2C5 File Offset: 0x0000A4C5
		public void SetServerTickRate(double value)
		{
			ScriptingInterfaceOfIMBNetwork.call_SetServerTickRateDelegate(value);
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000C2D2 File Offset: 0x0000A4D2
		public void TerminateClientSide()
		{
			ScriptingInterfaceOfIMBNetwork.call_TerminateClientSideDelegate();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000C2DE File Offset: 0x0000A4DE
		public void TerminateServerSide()
		{
			ScriptingInterfaceOfIMBNetwork.call_TerminateServerSideDelegate();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C2EC File Offset: 0x0000A4EC
		public void WriteByteArrayToPacket(byte[] value, int offset, int size)
		{
			PinnedArrayData<byte> pinnedArrayData = new PinnedArrayData<byte>(value, false);
			IntPtr pointer = pinnedArrayData.Pointer;
			ManagedArray value2 = new ManagedArray(pointer, (value != null) ? value.Length : 0);
			ScriptingInterfaceOfIMBNetwork.call_WriteByteArrayToPacketDelegate(value2, offset, size);
			pinnedArrayData.Dispose();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000C32F File Offset: 0x0000A52F
		public void WriteFloatToPacket(float value, ref CompressionInfo.Float compressionInfo)
		{
			ScriptingInterfaceOfIMBNetwork.call_WriteFloatToPacketDelegate(value, ref compressionInfo);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000C33D File Offset: 0x0000A53D
		public void WriteIntToPacket(int value, ref CompressionInfo.Integer compressionInfo)
		{
			ScriptingInterfaceOfIMBNetwork.call_WriteIntToPacketDelegate(value, ref compressionInfo);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000C34B File Offset: 0x0000A54B
		public void WriteLongToPacket(long value, ref CompressionInfo.LongInteger compressionInfo)
		{
			ScriptingInterfaceOfIMBNetwork.call_WriteLongToPacketDelegate(value, ref compressionInfo);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000C35C File Offset: 0x0000A55C
		public void WriteStringToPacket(string value)
		{
			byte[] array = null;
			if (value != null)
			{
				int byteCount = ScriptingInterfaceOfIMBNetwork._utf8.GetByteCount(value);
				array = ((byteCount < 1024) ? CallbackStringBufferManager.StringBuffer0 : new byte[byteCount + 1]);
				ScriptingInterfaceOfIMBNetwork._utf8.GetBytes(value, 0, value.Length, array, 0);
				array[byteCount] = 0;
			}
			ScriptingInterfaceOfIMBNetwork.call_WriteStringToPacketDelegate(array);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C3B6 File Offset: 0x0000A5B6
		public void WriteUintToPacket(uint value, ref CompressionInfo.UnsignedInteger compressionInfo)
		{
			ScriptingInterfaceOfIMBNetwork.call_WriteUintToPacketDelegate(value, ref compressionInfo);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public void WriteUlongToPacket(ulong value, ref CompressionInfo.UnsignedLongInteger compressionInfo)
		{
			ScriptingInterfaceOfIMBNetwork.call_WriteUlongToPacketDelegate(value, ref compressionInfo);
		}

		// Token: 0x04000239 RID: 569
		private static readonly Encoding _utf8 = Encoding.UTF8;

		// Token: 0x0400023A RID: 570
		public static ScriptingInterfaceOfIMBNetwork.AddNewBotOnServerDelegate call_AddNewBotOnServerDelegate;

		// Token: 0x0400023B RID: 571
		public static ScriptingInterfaceOfIMBNetwork.AddNewPlayerOnServerDelegate call_AddNewPlayerOnServerDelegate;

		// Token: 0x0400023C RID: 572
		public static ScriptingInterfaceOfIMBNetwork.AddPeerToDisconnectDelegate call_AddPeerToDisconnectDelegate;

		// Token: 0x0400023D RID: 573
		public static ScriptingInterfaceOfIMBNetwork.BeginBroadcastModuleEventDelegate call_BeginBroadcastModuleEventDelegate;

		// Token: 0x0400023E RID: 574
		public static ScriptingInterfaceOfIMBNetwork.BeginModuleEventAsClientDelegate call_BeginModuleEventAsClientDelegate;

		// Token: 0x0400023F RID: 575
		public static ScriptingInterfaceOfIMBNetwork.CanAddNewPlayersOnServerDelegate call_CanAddNewPlayersOnServerDelegate;

		// Token: 0x04000240 RID: 576
		public static ScriptingInterfaceOfIMBNetwork.ClearReplicationTableStatisticsDelegate call_ClearReplicationTableStatisticsDelegate;

		// Token: 0x04000241 RID: 577
		public static ScriptingInterfaceOfIMBNetwork.ElapsedTimeSinceLastUdpPacketArrivedDelegate call_ElapsedTimeSinceLastUdpPacketArrivedDelegate;

		// Token: 0x04000242 RID: 578
		public static ScriptingInterfaceOfIMBNetwork.EndBroadcastModuleEventDelegate call_EndBroadcastModuleEventDelegate;

		// Token: 0x04000243 RID: 579
		public static ScriptingInterfaceOfIMBNetwork.EndModuleEventAsClientDelegate call_EndModuleEventAsClientDelegate;

		// Token: 0x04000244 RID: 580
		public static ScriptingInterfaceOfIMBNetwork.GetActiveUdpSessionsIpAddressDelegate call_GetActiveUdpSessionsIpAddressDelegate;

		// Token: 0x04000245 RID: 581
		public static ScriptingInterfaceOfIMBNetwork.GetAveragePacketLossRatioDelegate call_GetAveragePacketLossRatioDelegate;

		// Token: 0x04000246 RID: 582
		public static ScriptingInterfaceOfIMBNetwork.GetDebugUploadsInBitsDelegate call_GetDebugUploadsInBitsDelegate;

		// Token: 0x04000247 RID: 583
		public static ScriptingInterfaceOfIMBNetwork.GetMultiplayerDisabledDelegate call_GetMultiplayerDisabledDelegate;

		// Token: 0x04000248 RID: 584
		public static ScriptingInterfaceOfIMBNetwork.InitializeClientSideDelegate call_InitializeClientSideDelegate;

		// Token: 0x04000249 RID: 585
		public static ScriptingInterfaceOfIMBNetwork.InitializeServerSideDelegate call_InitializeServerSideDelegate;

		// Token: 0x0400024A RID: 586
		public static ScriptingInterfaceOfIMBNetwork.IsDedicatedServerDelegate call_IsDedicatedServerDelegate;

		// Token: 0x0400024B RID: 587
		public static ScriptingInterfaceOfIMBNetwork.PrepareNewUdpSessionDelegate call_PrepareNewUdpSessionDelegate;

		// Token: 0x0400024C RID: 588
		public static ScriptingInterfaceOfIMBNetwork.PrintDebugStatsDelegate call_PrintDebugStatsDelegate;

		// Token: 0x0400024D RID: 589
		public static ScriptingInterfaceOfIMBNetwork.PrintReplicationTableStatisticsDelegate call_PrintReplicationTableStatisticsDelegate;

		// Token: 0x0400024E RID: 590
		public static ScriptingInterfaceOfIMBNetwork.ReadByteArrayFromPacketDelegate call_ReadByteArrayFromPacketDelegate;

		// Token: 0x0400024F RID: 591
		public static ScriptingInterfaceOfIMBNetwork.ReadFloatFromPacketDelegate call_ReadFloatFromPacketDelegate;

		// Token: 0x04000250 RID: 592
		public static ScriptingInterfaceOfIMBNetwork.ReadIntFromPacketDelegate call_ReadIntFromPacketDelegate;

		// Token: 0x04000251 RID: 593
		public static ScriptingInterfaceOfIMBNetwork.ReadLongFromPacketDelegate call_ReadLongFromPacketDelegate;

		// Token: 0x04000252 RID: 594
		public static ScriptingInterfaceOfIMBNetwork.ReadStringFromPacketDelegate call_ReadStringFromPacketDelegate;

		// Token: 0x04000253 RID: 595
		public static ScriptingInterfaceOfIMBNetwork.ReadUintFromPacketDelegate call_ReadUintFromPacketDelegate;

		// Token: 0x04000254 RID: 596
		public static ScriptingInterfaceOfIMBNetwork.ReadUlongFromPacketDelegate call_ReadUlongFromPacketDelegate;

		// Token: 0x04000255 RID: 597
		public static ScriptingInterfaceOfIMBNetwork.RemoveBotOnServerDelegate call_RemoveBotOnServerDelegate;

		// Token: 0x04000256 RID: 598
		public static ScriptingInterfaceOfIMBNetwork.ResetDebugUploadsDelegate call_ResetDebugUploadsDelegate;

		// Token: 0x04000257 RID: 599
		public static ScriptingInterfaceOfIMBNetwork.ResetDebugVariablesDelegate call_ResetDebugVariablesDelegate;

		// Token: 0x04000258 RID: 600
		public static ScriptingInterfaceOfIMBNetwork.ResetMissionDataDelegate call_ResetMissionDataDelegate;

		// Token: 0x04000259 RID: 601
		public static ScriptingInterfaceOfIMBNetwork.ServerPingDelegate call_ServerPingDelegate;

		// Token: 0x0400025A RID: 602
		public static ScriptingInterfaceOfIMBNetwork.SetServerBandwidthLimitInMbpsDelegate call_SetServerBandwidthLimitInMbpsDelegate;

		// Token: 0x0400025B RID: 603
		public static ScriptingInterfaceOfIMBNetwork.SetServerFrameRateDelegate call_SetServerFrameRateDelegate;

		// Token: 0x0400025C RID: 604
		public static ScriptingInterfaceOfIMBNetwork.SetServerTickRateDelegate call_SetServerTickRateDelegate;

		// Token: 0x0400025D RID: 605
		public static ScriptingInterfaceOfIMBNetwork.TerminateClientSideDelegate call_TerminateClientSideDelegate;

		// Token: 0x0400025E RID: 606
		public static ScriptingInterfaceOfIMBNetwork.TerminateServerSideDelegate call_TerminateServerSideDelegate;

		// Token: 0x0400025F RID: 607
		public static ScriptingInterfaceOfIMBNetwork.WriteByteArrayToPacketDelegate call_WriteByteArrayToPacketDelegate;

		// Token: 0x04000260 RID: 608
		public static ScriptingInterfaceOfIMBNetwork.WriteFloatToPacketDelegate call_WriteFloatToPacketDelegate;

		// Token: 0x04000261 RID: 609
		public static ScriptingInterfaceOfIMBNetwork.WriteIntToPacketDelegate call_WriteIntToPacketDelegate;

		// Token: 0x04000262 RID: 610
		public static ScriptingInterfaceOfIMBNetwork.WriteLongToPacketDelegate call_WriteLongToPacketDelegate;

		// Token: 0x04000263 RID: 611
		public static ScriptingInterfaceOfIMBNetwork.WriteStringToPacketDelegate call_WriteStringToPacketDelegate;

		// Token: 0x04000264 RID: 612
		public static ScriptingInterfaceOfIMBNetwork.WriteUintToPacketDelegate call_WriteUintToPacketDelegate;

		// Token: 0x04000265 RID: 613
		public static ScriptingInterfaceOfIMBNetwork.WriteUlongToPacketDelegate call_WriteUlongToPacketDelegate;

		// Token: 0x02000292 RID: 658
		// (Invoke) Token: 0x06000CED RID: 3309
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddNewBotOnServerDelegate();

		// Token: 0x02000293 RID: 659
		// (Invoke) Token: 0x06000CF1 RID: 3313
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int AddNewPlayerOnServerDelegate([MarshalAs(UnmanagedType.U1)] bool serverPlayer);

		// Token: 0x02000294 RID: 660
		// (Invoke) Token: 0x06000CF5 RID: 3317
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void AddPeerToDisconnectDelegate(int peer);

		// Token: 0x02000295 RID: 661
		// (Invoke) Token: 0x06000CF9 RID: 3321
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BeginBroadcastModuleEventDelegate();

		// Token: 0x02000296 RID: 662
		// (Invoke) Token: 0x06000CFD RID: 3325
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void BeginModuleEventAsClientDelegate([MarshalAs(UnmanagedType.U1)] bool isReliable);

		// Token: 0x02000297 RID: 663
		// (Invoke) Token: 0x06000D01 RID: 3329
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool CanAddNewPlayersOnServerDelegate(int numPlayers);

		// Token: 0x02000298 RID: 664
		// (Invoke) Token: 0x06000D05 RID: 3333
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ClearReplicationTableStatisticsDelegate();

		// Token: 0x02000299 RID: 665
		// (Invoke) Token: 0x06000D09 RID: 3337
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate double ElapsedTimeSinceLastUdpPacketArrivedDelegate();

		// Token: 0x0200029A RID: 666
		// (Invoke) Token: 0x06000D0D RID: 3341
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EndBroadcastModuleEventDelegate(int broadcastFlags, int targetPlayer, [MarshalAs(UnmanagedType.U1)] bool isReliable);

		// Token: 0x0200029B RID: 667
		// (Invoke) Token: 0x06000D11 RID: 3345
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void EndModuleEventAsClientDelegate([MarshalAs(UnmanagedType.U1)] bool isReliable);

		// Token: 0x0200029C RID: 668
		// (Invoke) Token: 0x06000D15 RID: 3349
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int GetActiveUdpSessionsIpAddressDelegate();

		// Token: 0x0200029D RID: 669
		// (Invoke) Token: 0x06000D19 RID: 3353
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate float GetAveragePacketLossRatioDelegate();

		// Token: 0x0200029E RID: 670
		// (Invoke) Token: 0x06000D1D RID: 3357
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void GetDebugUploadsInBitsDelegate(ref GameNetwork.DebugNetworkPacketStatisticsStruct networkStatisticsStruct, ref GameNetwork.DebugNetworkPositionCompressionStatisticsStruct posStatisticsStruct);

		// Token: 0x0200029F RID: 671
		// (Invoke) Token: 0x06000D21 RID: 3361
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool GetMultiplayerDisabledDelegate();

		// Token: 0x020002A0 RID: 672
		// (Invoke) Token: 0x06000D25 RID: 3365
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InitializeClientSideDelegate(byte[] serverAddress, int port, int sessionKey, int playerIndex);

		// Token: 0x020002A1 RID: 673
		// (Invoke) Token: 0x06000D29 RID: 3369
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void InitializeServerSideDelegate(int port);

		// Token: 0x020002A2 RID: 674
		// (Invoke) Token: 0x06000D2D RID: 3373
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool IsDedicatedServerDelegate();

		// Token: 0x020002A3 RID: 675
		// (Invoke) Token: 0x06000D31 RID: 3377
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PrepareNewUdpSessionDelegate(int player, int sessionKey);

		// Token: 0x020002A4 RID: 676
		// (Invoke) Token: 0x06000D35 RID: 3381
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PrintDebugStatsDelegate();

		// Token: 0x020002A5 RID: 677
		// (Invoke) Token: 0x06000D39 RID: 3385
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void PrintReplicationTableStatisticsDelegate();

		// Token: 0x020002A6 RID: 678
		// (Invoke) Token: 0x06000D3D RID: 3389
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int ReadByteArrayFromPacketDelegate(ManagedArray buffer, int offset, int bufferCapacity, [MarshalAs(UnmanagedType.U1)] ref bool bufferReadValid);

		// Token: 0x020002A7 RID: 679
		// (Invoke) Token: 0x06000D41 RID: 3393
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ReadFloatFromPacketDelegate(ref CompressionInfo.Float compressionInfo, out float output);

		// Token: 0x020002A8 RID: 680
		// (Invoke) Token: 0x06000D45 RID: 3397
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ReadIntFromPacketDelegate(ref CompressionInfo.Integer compressionInfo, out int output);

		// Token: 0x020002A9 RID: 681
		// (Invoke) Token: 0x06000D49 RID: 3401
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ReadLongFromPacketDelegate(ref CompressionInfo.LongInteger compressionInfo, out long output);

		// Token: 0x020002AA RID: 682
		// (Invoke) Token: 0x06000D4D RID: 3405
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate int ReadStringFromPacketDelegate([MarshalAs(UnmanagedType.U1)] ref bool bufferReadValid);

		// Token: 0x020002AB RID: 683
		// (Invoke) Token: 0x06000D51 RID: 3409
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ReadUintFromPacketDelegate(ref CompressionInfo.UnsignedInteger compressionInfo, out uint output);

		// Token: 0x020002AC RID: 684
		// (Invoke) Token: 0x06000D55 RID: 3413
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		[return: MarshalAs(UnmanagedType.U1)]
		public delegate bool ReadUlongFromPacketDelegate(ref CompressionInfo.UnsignedLongInteger compressionInfo, out ulong output);

		// Token: 0x020002AD RID: 685
		// (Invoke) Token: 0x06000D59 RID: 3417
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void RemoveBotOnServerDelegate(int botPlayerIndex);

		// Token: 0x020002AE RID: 686
		// (Invoke) Token: 0x06000D5D RID: 3421
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResetDebugUploadsDelegate();

		// Token: 0x020002AF RID: 687
		// (Invoke) Token: 0x06000D61 RID: 3425
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResetDebugVariablesDelegate();

		// Token: 0x020002B0 RID: 688
		// (Invoke) Token: 0x06000D65 RID: 3429
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ResetMissionDataDelegate();

		// Token: 0x020002B1 RID: 689
		// (Invoke) Token: 0x06000D69 RID: 3433
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void ServerPingDelegate(byte[] serverAddress, int port);

		// Token: 0x020002B2 RID: 690
		// (Invoke) Token: 0x06000D6D RID: 3437
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetServerBandwidthLimitInMbpsDelegate(double value);

		// Token: 0x020002B3 RID: 691
		// (Invoke) Token: 0x06000D71 RID: 3441
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetServerFrameRateDelegate(double limit);

		// Token: 0x020002B4 RID: 692
		// (Invoke) Token: 0x06000D75 RID: 3445
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void SetServerTickRateDelegate(double value);

		// Token: 0x020002B5 RID: 693
		// (Invoke) Token: 0x06000D79 RID: 3449
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TerminateClientSideDelegate();

		// Token: 0x020002B6 RID: 694
		// (Invoke) Token: 0x06000D7D RID: 3453
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void TerminateServerSideDelegate();

		// Token: 0x020002B7 RID: 695
		// (Invoke) Token: 0x06000D81 RID: 3457
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WriteByteArrayToPacketDelegate(ManagedArray value, int offset, int size);

		// Token: 0x020002B8 RID: 696
		// (Invoke) Token: 0x06000D85 RID: 3461
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WriteFloatToPacketDelegate(float value, ref CompressionInfo.Float compressionInfo);

		// Token: 0x020002B9 RID: 697
		// (Invoke) Token: 0x06000D89 RID: 3465
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WriteIntToPacketDelegate(int value, ref CompressionInfo.Integer compressionInfo);

		// Token: 0x020002BA RID: 698
		// (Invoke) Token: 0x06000D8D RID: 3469
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WriteLongToPacketDelegate(long value, ref CompressionInfo.LongInteger compressionInfo);

		// Token: 0x020002BB RID: 699
		// (Invoke) Token: 0x06000D91 RID: 3473
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WriteStringToPacketDelegate(byte[] value);

		// Token: 0x020002BC RID: 700
		// (Invoke) Token: 0x06000D95 RID: 3477
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WriteUintToPacketDelegate(uint value, ref CompressionInfo.UnsignedInteger compressionInfo);

		// Token: 0x020002BD RID: 701
		// (Invoke) Token: 0x06000D99 RID: 3481
		[UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
		[SuppressUnmanagedCodeSecurity]
		[MonoNativeFunctionWrapper]
		public delegate void WriteUlongToPacketDelegate(ulong value, ref CompressionInfo.UnsignedLongInteger compressionInfo);
	}
}
