using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001A9 RID: 425
	[ScriptingInterfaceBase]
	internal interface IMBNetwork
	{
		// Token: 0x06001739 RID: 5945
		[EngineMethod("get_multiplayer_disabled", false)]
		bool GetMultiplayerDisabled();

		// Token: 0x0600173A RID: 5946
		[EngineMethod("is_dedicated_server", false)]
		bool IsDedicatedServer();

		// Token: 0x0600173B RID: 5947
		[EngineMethod("initialize_server_side", false)]
		void InitializeServerSide(int port);

		// Token: 0x0600173C RID: 5948
		[EngineMethod("initialize_client_side", false)]
		void InitializeClientSide(string serverAddress, int port, int sessionKey, int playerIndex);

		// Token: 0x0600173D RID: 5949
		[EngineMethod("terminate_server_side", false)]
		void TerminateServerSide();

		// Token: 0x0600173E RID: 5950
		[EngineMethod("terminate_client_side", false)]
		void TerminateClientSide();

		// Token: 0x0600173F RID: 5951
		[EngineMethod("server_ping", false)]
		void ServerPing(string serverAddress, int port);

		// Token: 0x06001740 RID: 5952
		[EngineMethod("add_peer_to_disconnect", false)]
		void AddPeerToDisconnect(int peer);

		// Token: 0x06001741 RID: 5953
		[EngineMethod("prepare_new_udp_session", false)]
		void PrepareNewUdpSession(int player, int sessionKey);

		// Token: 0x06001742 RID: 5954
		[EngineMethod("get_active_udp_sessions_ip_address", false)]
		string GetActiveUdpSessionsIpAddress();

		// Token: 0x06001743 RID: 5955
		[EngineMethod("can_add_new_players_on_server", false)]
		bool CanAddNewPlayersOnServer(int numPlayers);

		// Token: 0x06001744 RID: 5956
		[EngineMethod("add_new_player_on_server", false)]
		int AddNewPlayerOnServer(bool serverPlayer);

		// Token: 0x06001745 RID: 5957
		[EngineMethod("add_new_bot_on_server", false)]
		int AddNewBotOnServer();

		// Token: 0x06001746 RID: 5958
		[EngineMethod("remove_bot_on_server", false)]
		void RemoveBotOnServer(int botPlayerIndex);

		// Token: 0x06001747 RID: 5959
		[EngineMethod("reset_mission_data", false)]
		void ResetMissionData();

		// Token: 0x06001748 RID: 5960
		[EngineMethod("begin_broadcast_module_event", false)]
		void BeginBroadcastModuleEvent();

		// Token: 0x06001749 RID: 5961
		[EngineMethod("end_broadcast_module_event", false)]
		void EndBroadcastModuleEvent(int broadcastFlags, int targetPlayer, bool isReliable);

		// Token: 0x0600174A RID: 5962
		[EngineMethod("elapsed_time_since_last_udp_packet_arrived", false)]
		double ElapsedTimeSinceLastUdpPacketArrived();

		// Token: 0x0600174B RID: 5963
		[EngineMethod("begin_module_event_as_client", false)]
		void BeginModuleEventAsClient(bool isReliable);

		// Token: 0x0600174C RID: 5964
		[EngineMethod("end_module_event_as_client", false)]
		void EndModuleEventAsClient(bool isReliable);

		// Token: 0x0600174D RID: 5965
		[EngineMethod("read_int_from_packet", false)]
		bool ReadIntFromPacket(ref CompressionInfo.Integer compressionInfo, out int output);

		// Token: 0x0600174E RID: 5966
		[EngineMethod("read_uint_from_packet", false)]
		bool ReadUintFromPacket(ref CompressionInfo.UnsignedInteger compressionInfo, out uint output);

		// Token: 0x0600174F RID: 5967
		[EngineMethod("read_long_from_packet", false)]
		bool ReadLongFromPacket(ref CompressionInfo.LongInteger compressionInfo, out long output);

		// Token: 0x06001750 RID: 5968
		[EngineMethod("read_ulong_from_packet", false)]
		bool ReadUlongFromPacket(ref CompressionInfo.UnsignedLongInteger compressionInfo, out ulong output);

		// Token: 0x06001751 RID: 5969
		[EngineMethod("read_float_from_packet", false)]
		bool ReadFloatFromPacket(ref CompressionInfo.Float compressionInfo, out float output);

		// Token: 0x06001752 RID: 5970
		[EngineMethod("read_string_from_packet", false)]
		string ReadStringFromPacket(ref bool bufferReadValid);

		// Token: 0x06001753 RID: 5971
		[EngineMethod("write_int_to_packet", false)]
		void WriteIntToPacket(int value, ref CompressionInfo.Integer compressionInfo);

		// Token: 0x06001754 RID: 5972
		[EngineMethod("write_uint_to_packet", false)]
		void WriteUintToPacket(uint value, ref CompressionInfo.UnsignedInteger compressionInfo);

		// Token: 0x06001755 RID: 5973
		[EngineMethod("write_long_to_packet", false)]
		void WriteLongToPacket(long value, ref CompressionInfo.LongInteger compressionInfo);

		// Token: 0x06001756 RID: 5974
		[EngineMethod("write_ulong_to_packet", false)]
		void WriteUlongToPacket(ulong value, ref CompressionInfo.UnsignedLongInteger compressionInfo);

		// Token: 0x06001757 RID: 5975
		[EngineMethod("write_float_to_packet", false)]
		void WriteFloatToPacket(float value, ref CompressionInfo.Float compressionInfo);

		// Token: 0x06001758 RID: 5976
		[EngineMethod("write_string_to_packet", false)]
		void WriteStringToPacket(string value);

		// Token: 0x06001759 RID: 5977
		[EngineMethod("read_byte_array_from_packet", false)]
		int ReadByteArrayFromPacket(byte[] buffer, int offset, int bufferCapacity, ref bool bufferReadValid);

		// Token: 0x0600175A RID: 5978
		[EngineMethod("write_byte_array_to_packet", false)]
		void WriteByteArrayToPacket(byte[] value, int offset, int size);

		// Token: 0x0600175B RID: 5979
		[EngineMethod("set_server_bandwidth_limit_in_mbps", false)]
		void SetServerBandwidthLimitInMbps(double value);

		// Token: 0x0600175C RID: 5980
		[EngineMethod("set_server_tick_rate", false)]
		void SetServerTickRate(double value);

		// Token: 0x0600175D RID: 5981
		[EngineMethod("reset_debug_variables", false)]
		void ResetDebugVariables();

		// Token: 0x0600175E RID: 5982
		[EngineMethod("print_debug_stats", false)]
		void PrintDebugStats();

		// Token: 0x0600175F RID: 5983
		[EngineMethod("get_average_packet_loss_ratio", false)]
		float GetAveragePacketLossRatio();

		// Token: 0x06001760 RID: 5984
		[EngineMethod("get_debug_uploads_in_bits", false)]
		void GetDebugUploadsInBits(ref GameNetwork.DebugNetworkPacketStatisticsStruct networkStatisticsStruct, ref GameNetwork.DebugNetworkPositionCompressionStatisticsStruct posStatisticsStruct);

		// Token: 0x06001761 RID: 5985
		[EngineMethod("reset_debug_uploads", false)]
		void ResetDebugUploads();

		// Token: 0x06001762 RID: 5986
		[EngineMethod("print_replication_table_statistics", false)]
		void PrintReplicationTableStatistics();

		// Token: 0x06001763 RID: 5987
		[EngineMethod("clear_replication_table_statistics", false)]
		void ClearReplicationTableStatistics();

		// Token: 0x06001764 RID: 5988
		[EngineMethod("set_server_frame_rate", false)]
		void SetServerFrameRate(double limit);
	}
}
