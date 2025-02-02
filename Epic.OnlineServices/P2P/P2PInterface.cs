using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002D9 RID: 729
	public sealed class P2PInterface : Handle
	{
		// Token: 0x0600139C RID: 5020 RVA: 0x0001CD40 File Offset: 0x0001AF40
		public P2PInterface()
		{
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0001CD4A File Offset: 0x0001AF4A
		public P2PInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0001CD58 File Offset: 0x0001AF58
		public Result AcceptConnection(ref AcceptConnectionOptions options)
		{
			AcceptConnectionOptionsInternal acceptConnectionOptionsInternal = default(AcceptConnectionOptionsInternal);
			acceptConnectionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_P2P_AcceptConnection(base.InnerHandle, ref acceptConnectionOptionsInternal);
			Helper.Dispose<AcceptConnectionOptionsInternal>(ref acceptConnectionOptionsInternal);
			return result;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0001CD94 File Offset: 0x0001AF94
		public ulong AddNotifyIncomingPacketQueueFull(ref AddNotifyIncomingPacketQueueFullOptions options, object clientData, OnIncomingPacketQueueFullCallback incomingPacketQueueFullHandler)
		{
			AddNotifyIncomingPacketQueueFullOptionsInternal addNotifyIncomingPacketQueueFullOptionsInternal = default(AddNotifyIncomingPacketQueueFullOptionsInternal);
			addNotifyIncomingPacketQueueFullOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnIncomingPacketQueueFullCallbackInternal onIncomingPacketQueueFullCallbackInternal = new OnIncomingPacketQueueFullCallbackInternal(P2PInterface.OnIncomingPacketQueueFullCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, incomingPacketQueueFullHandler, onIncomingPacketQueueFullCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_P2P_AddNotifyIncomingPacketQueueFull(base.InnerHandle, ref addNotifyIncomingPacketQueueFullOptionsInternal, zero, onIncomingPacketQueueFullCallbackInternal);
			Helper.Dispose<AddNotifyIncomingPacketQueueFullOptionsInternal>(ref addNotifyIncomingPacketQueueFullOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0001CE00 File Offset: 0x0001B000
		public ulong AddNotifyPeerConnectionClosed(ref AddNotifyPeerConnectionClosedOptions options, object clientData, OnRemoteConnectionClosedCallback connectionClosedHandler)
		{
			AddNotifyPeerConnectionClosedOptionsInternal addNotifyPeerConnectionClosedOptionsInternal = default(AddNotifyPeerConnectionClosedOptionsInternal);
			addNotifyPeerConnectionClosedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnRemoteConnectionClosedCallbackInternal onRemoteConnectionClosedCallbackInternal = new OnRemoteConnectionClosedCallbackInternal(P2PInterface.OnRemoteConnectionClosedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, connectionClosedHandler, onRemoteConnectionClosedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionClosed(base.InnerHandle, ref addNotifyPeerConnectionClosedOptionsInternal, zero, onRemoteConnectionClosedCallbackInternal);
			Helper.Dispose<AddNotifyPeerConnectionClosedOptionsInternal>(ref addNotifyPeerConnectionClosedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0001CE6C File Offset: 0x0001B06C
		public ulong AddNotifyPeerConnectionEstablished(ref AddNotifyPeerConnectionEstablishedOptions options, object clientData, OnPeerConnectionEstablishedCallback connectionEstablishedHandler)
		{
			AddNotifyPeerConnectionEstablishedOptionsInternal addNotifyPeerConnectionEstablishedOptionsInternal = default(AddNotifyPeerConnectionEstablishedOptionsInternal);
			addNotifyPeerConnectionEstablishedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnPeerConnectionEstablishedCallbackInternal onPeerConnectionEstablishedCallbackInternal = new OnPeerConnectionEstablishedCallbackInternal(P2PInterface.OnPeerConnectionEstablishedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, connectionEstablishedHandler, onPeerConnectionEstablishedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionEstablished(base.InnerHandle, ref addNotifyPeerConnectionEstablishedOptionsInternal, zero, onPeerConnectionEstablishedCallbackInternal);
			Helper.Dispose<AddNotifyPeerConnectionEstablishedOptionsInternal>(ref addNotifyPeerConnectionEstablishedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0001CED8 File Offset: 0x0001B0D8
		public ulong AddNotifyPeerConnectionInterrupted(ref AddNotifyPeerConnectionInterruptedOptions options, object clientData, OnPeerConnectionInterruptedCallback connectionInterruptedHandler)
		{
			AddNotifyPeerConnectionInterruptedOptionsInternal addNotifyPeerConnectionInterruptedOptionsInternal = default(AddNotifyPeerConnectionInterruptedOptionsInternal);
			addNotifyPeerConnectionInterruptedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnPeerConnectionInterruptedCallbackInternal onPeerConnectionInterruptedCallbackInternal = new OnPeerConnectionInterruptedCallbackInternal(P2PInterface.OnPeerConnectionInterruptedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, connectionInterruptedHandler, onPeerConnectionInterruptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionInterrupted(base.InnerHandle, ref addNotifyPeerConnectionInterruptedOptionsInternal, zero, onPeerConnectionInterruptedCallbackInternal);
			Helper.Dispose<AddNotifyPeerConnectionInterruptedOptionsInternal>(ref addNotifyPeerConnectionInterruptedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0001CF44 File Offset: 0x0001B144
		public ulong AddNotifyPeerConnectionRequest(ref AddNotifyPeerConnectionRequestOptions options, object clientData, OnIncomingConnectionRequestCallback connectionRequestHandler)
		{
			AddNotifyPeerConnectionRequestOptionsInternal addNotifyPeerConnectionRequestOptionsInternal = default(AddNotifyPeerConnectionRequestOptionsInternal);
			addNotifyPeerConnectionRequestOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnIncomingConnectionRequestCallbackInternal onIncomingConnectionRequestCallbackInternal = new OnIncomingConnectionRequestCallbackInternal(P2PInterface.OnIncomingConnectionRequestCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, connectionRequestHandler, onIncomingConnectionRequestCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_P2P_AddNotifyPeerConnectionRequest(base.InnerHandle, ref addNotifyPeerConnectionRequestOptionsInternal, zero, onIncomingConnectionRequestCallbackInternal);
			Helper.Dispose<AddNotifyPeerConnectionRequestOptionsInternal>(ref addNotifyPeerConnectionRequestOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060013A4 RID: 5028 RVA: 0x0001CFB0 File Offset: 0x0001B1B0
		public Result ClearPacketQueue(ref ClearPacketQueueOptions options)
		{
			ClearPacketQueueOptionsInternal clearPacketQueueOptionsInternal = default(ClearPacketQueueOptionsInternal);
			clearPacketQueueOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_P2P_ClearPacketQueue(base.InnerHandle, ref clearPacketQueueOptionsInternal);
			Helper.Dispose<ClearPacketQueueOptionsInternal>(ref clearPacketQueueOptionsInternal);
			return result;
		}

		// Token: 0x060013A5 RID: 5029 RVA: 0x0001CFEC File Offset: 0x0001B1EC
		public Result CloseConnection(ref CloseConnectionOptions options)
		{
			CloseConnectionOptionsInternal closeConnectionOptionsInternal = default(CloseConnectionOptionsInternal);
			closeConnectionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_P2P_CloseConnection(base.InnerHandle, ref closeConnectionOptionsInternal);
			Helper.Dispose<CloseConnectionOptionsInternal>(ref closeConnectionOptionsInternal);
			return result;
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0001D028 File Offset: 0x0001B228
		public Result CloseConnections(ref CloseConnectionsOptions options)
		{
			CloseConnectionsOptionsInternal closeConnectionsOptionsInternal = default(CloseConnectionsOptionsInternal);
			closeConnectionsOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_P2P_CloseConnections(base.InnerHandle, ref closeConnectionsOptionsInternal);
			Helper.Dispose<CloseConnectionsOptionsInternal>(ref closeConnectionsOptionsInternal);
			return result;
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0001D064 File Offset: 0x0001B264
		public Result GetNATType(ref GetNATTypeOptions options, out NATType outNATType)
		{
			GetNATTypeOptionsInternal getNATTypeOptionsInternal = default(GetNATTypeOptionsInternal);
			getNATTypeOptionsInternal.Set(ref options);
			outNATType = Helper.GetDefault<NATType>();
			Result result = Bindings.EOS_P2P_GetNATType(base.InnerHandle, ref getNATTypeOptionsInternal, ref outNATType);
			Helper.Dispose<GetNATTypeOptionsInternal>(ref getNATTypeOptionsInternal);
			return result;
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0001D0A8 File Offset: 0x0001B2A8
		public Result GetNextReceivedPacketSize(ref GetNextReceivedPacketSizeOptions options, out uint outPacketSizeBytes)
		{
			GetNextReceivedPacketSizeOptionsInternal getNextReceivedPacketSizeOptionsInternal = default(GetNextReceivedPacketSizeOptionsInternal);
			getNextReceivedPacketSizeOptionsInternal.Set(ref options);
			outPacketSizeBytes = Helper.GetDefault<uint>();
			Result result = Bindings.EOS_P2P_GetNextReceivedPacketSize(base.InnerHandle, ref getNextReceivedPacketSizeOptionsInternal, ref outPacketSizeBytes);
			Helper.Dispose<GetNextReceivedPacketSizeOptionsInternal>(ref getNextReceivedPacketSizeOptionsInternal);
			return result;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0001D0EC File Offset: 0x0001B2EC
		public Result GetPacketQueueInfo(ref GetPacketQueueInfoOptions options, out PacketQueueInfo outPacketQueueInfo)
		{
			GetPacketQueueInfoOptionsInternal getPacketQueueInfoOptionsInternal = default(GetPacketQueueInfoOptionsInternal);
			getPacketQueueInfoOptionsInternal.Set(ref options);
			PacketQueueInfoInternal @default = Helper.GetDefault<PacketQueueInfoInternal>();
			Result result = Bindings.EOS_P2P_GetPacketQueueInfo(base.InnerHandle, ref getPacketQueueInfoOptionsInternal, ref @default);
			Helper.Dispose<GetPacketQueueInfoOptionsInternal>(ref getPacketQueueInfoOptionsInternal);
			Helper.Get<PacketQueueInfoInternal, PacketQueueInfo>(ref @default, out outPacketQueueInfo);
			return result;
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0001D138 File Offset: 0x0001B338
		public Result GetPortRange(ref GetPortRangeOptions options, out ushort outPort, out ushort outNumAdditionalPortsToTry)
		{
			GetPortRangeOptionsInternal getPortRangeOptionsInternal = default(GetPortRangeOptionsInternal);
			getPortRangeOptionsInternal.Set(ref options);
			outPort = Helper.GetDefault<ushort>();
			outNumAdditionalPortsToTry = Helper.GetDefault<ushort>();
			Result result = Bindings.EOS_P2P_GetPortRange(base.InnerHandle, ref getPortRangeOptionsInternal, ref outPort, ref outNumAdditionalPortsToTry);
			Helper.Dispose<GetPortRangeOptionsInternal>(ref getPortRangeOptionsInternal);
			return result;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0001D184 File Offset: 0x0001B384
		public Result GetRelayControl(ref GetRelayControlOptions options, out RelayControl outRelayControl)
		{
			GetRelayControlOptionsInternal getRelayControlOptionsInternal = default(GetRelayControlOptionsInternal);
			getRelayControlOptionsInternal.Set(ref options);
			outRelayControl = Helper.GetDefault<RelayControl>();
			Result result = Bindings.EOS_P2P_GetRelayControl(base.InnerHandle, ref getRelayControlOptionsInternal, ref outRelayControl);
			Helper.Dispose<GetRelayControlOptionsInternal>(ref getRelayControlOptionsInternal);
			return result;
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0001D1C8 File Offset: 0x0001B3C8
		public void QueryNATType(ref QueryNATTypeOptions options, object clientData, OnQueryNATTypeCompleteCallback completionDelegate)
		{
			QueryNATTypeOptionsInternal queryNATTypeOptionsInternal = default(QueryNATTypeOptionsInternal);
			queryNATTypeOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryNATTypeCompleteCallbackInternal onQueryNATTypeCompleteCallbackInternal = new OnQueryNATTypeCompleteCallbackInternal(P2PInterface.OnQueryNATTypeCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryNATTypeCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_P2P_QueryNATType(base.InnerHandle, ref queryNATTypeOptionsInternal, zero, onQueryNATTypeCompleteCallbackInternal);
			Helper.Dispose<QueryNATTypeOptionsInternal>(ref queryNATTypeOptionsInternal);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0001D224 File Offset: 0x0001B424
		public Result ReceivePacket(ref ReceivePacketOptions options, out ProductUserId outPeerId, out SocketId outSocketId, out byte outChannel, ArraySegment<byte> outData, out uint outBytesWritten)
		{
			ReceivePacketOptionsInternal receivePacketOptionsInternal = default(ReceivePacketOptionsInternal);
			receivePacketOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			SocketIdInternal @default = Helper.GetDefault<SocketIdInternal>();
			outChannel = Helper.GetDefault<byte>();
			outBytesWritten = 0U;
			IntPtr outData2 = Helper.AddPinnedBuffer(outData);
			Result result = Bindings.EOS_P2P_ReceivePacket(base.InnerHandle, ref receivePacketOptionsInternal, ref zero, ref @default, ref outChannel, outData2, ref outBytesWritten);
			Helper.Dispose<ReceivePacketOptionsInternal>(ref receivePacketOptionsInternal);
			Helper.Get<ProductUserId>(zero, out outPeerId);
			Helper.Get<SocketIdInternal, SocketId>(ref @default, out outSocketId);
			Helper.Dispose(ref outData2);
			return result;
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0001D2A4 File Offset: 0x0001B4A4
		public void RemoveNotifyIncomingPacketQueueFull(ulong notificationId)
		{
			Bindings.EOS_P2P_RemoveNotifyIncomingPacketQueueFull(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0001D2BB File Offset: 0x0001B4BB
		public void RemoveNotifyPeerConnectionClosed(ulong notificationId)
		{
			Bindings.EOS_P2P_RemoveNotifyPeerConnectionClosed(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0001D2D2 File Offset: 0x0001B4D2
		public void RemoveNotifyPeerConnectionEstablished(ulong notificationId)
		{
			Bindings.EOS_P2P_RemoveNotifyPeerConnectionEstablished(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0001D2E9 File Offset: 0x0001B4E9
		public void RemoveNotifyPeerConnectionInterrupted(ulong notificationId)
		{
			Bindings.EOS_P2P_RemoveNotifyPeerConnectionInterrupted(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0001D300 File Offset: 0x0001B500
		public void RemoveNotifyPeerConnectionRequest(ulong notificationId)
		{
			Bindings.EOS_P2P_RemoveNotifyPeerConnectionRequest(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0001D318 File Offset: 0x0001B518
		public Result SendPacket(ref SendPacketOptions options)
		{
			SendPacketOptionsInternal sendPacketOptionsInternal = default(SendPacketOptionsInternal);
			sendPacketOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_P2P_SendPacket(base.InnerHandle, ref sendPacketOptionsInternal);
			Helper.Dispose<SendPacketOptionsInternal>(ref sendPacketOptionsInternal);
			return result;
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0001D354 File Offset: 0x0001B554
		public Result SetPacketQueueSize(ref SetPacketQueueSizeOptions options)
		{
			SetPacketQueueSizeOptionsInternal setPacketQueueSizeOptionsInternal = default(SetPacketQueueSizeOptionsInternal);
			setPacketQueueSizeOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_P2P_SetPacketQueueSize(base.InnerHandle, ref setPacketQueueSizeOptionsInternal);
			Helper.Dispose<SetPacketQueueSizeOptionsInternal>(ref setPacketQueueSizeOptionsInternal);
			return result;
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0001D390 File Offset: 0x0001B590
		public Result SetPortRange(ref SetPortRangeOptions options)
		{
			SetPortRangeOptionsInternal setPortRangeOptionsInternal = default(SetPortRangeOptionsInternal);
			setPortRangeOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_P2P_SetPortRange(base.InnerHandle, ref setPortRangeOptionsInternal);
			Helper.Dispose<SetPortRangeOptionsInternal>(ref setPortRangeOptionsInternal);
			return result;
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0001D3CC File Offset: 0x0001B5CC
		public Result SetRelayControl(ref SetRelayControlOptions options)
		{
			SetRelayControlOptionsInternal setRelayControlOptionsInternal = default(SetRelayControlOptionsInternal);
			setRelayControlOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_P2P_SetRelayControl(base.InnerHandle, ref setRelayControlOptionsInternal);
			Helper.Dispose<SetRelayControlOptionsInternal>(ref setRelayControlOptionsInternal);
			return result;
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0001D408 File Offset: 0x0001B608
		[MonoPInvokeCallback(typeof(OnIncomingConnectionRequestCallbackInternal))]
		internal static void OnIncomingConnectionRequestCallbackInternalImplementation(ref OnIncomingConnectionRequestInfoInternal data)
		{
			OnIncomingConnectionRequestCallback onIncomingConnectionRequestCallback;
			OnIncomingConnectionRequestInfo onIncomingConnectionRequestInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnIncomingConnectionRequestInfoInternal, OnIncomingConnectionRequestCallback, OnIncomingConnectionRequestInfo>(ref data, out onIncomingConnectionRequestCallback, out onIncomingConnectionRequestInfo);
			if (flag)
			{
				onIncomingConnectionRequestCallback(ref onIncomingConnectionRequestInfo);
			}
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0001D430 File Offset: 0x0001B630
		[MonoPInvokeCallback(typeof(OnIncomingPacketQueueFullCallbackInternal))]
		internal static void OnIncomingPacketQueueFullCallbackInternalImplementation(ref OnIncomingPacketQueueFullInfoInternal data)
		{
			OnIncomingPacketQueueFullCallback onIncomingPacketQueueFullCallback;
			OnIncomingPacketQueueFullInfo onIncomingPacketQueueFullInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnIncomingPacketQueueFullInfoInternal, OnIncomingPacketQueueFullCallback, OnIncomingPacketQueueFullInfo>(ref data, out onIncomingPacketQueueFullCallback, out onIncomingPacketQueueFullInfo);
			if (flag)
			{
				onIncomingPacketQueueFullCallback(ref onIncomingPacketQueueFullInfo);
			}
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0001D458 File Offset: 0x0001B658
		[MonoPInvokeCallback(typeof(OnPeerConnectionEstablishedCallbackInternal))]
		internal static void OnPeerConnectionEstablishedCallbackInternalImplementation(ref OnPeerConnectionEstablishedInfoInternal data)
		{
			OnPeerConnectionEstablishedCallback onPeerConnectionEstablishedCallback;
			OnPeerConnectionEstablishedInfo onPeerConnectionEstablishedInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnPeerConnectionEstablishedInfoInternal, OnPeerConnectionEstablishedCallback, OnPeerConnectionEstablishedInfo>(ref data, out onPeerConnectionEstablishedCallback, out onPeerConnectionEstablishedInfo);
			if (flag)
			{
				onPeerConnectionEstablishedCallback(ref onPeerConnectionEstablishedInfo);
			}
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0001D480 File Offset: 0x0001B680
		[MonoPInvokeCallback(typeof(OnPeerConnectionInterruptedCallbackInternal))]
		internal static void OnPeerConnectionInterruptedCallbackInternalImplementation(ref OnPeerConnectionInterruptedInfoInternal data)
		{
			OnPeerConnectionInterruptedCallback onPeerConnectionInterruptedCallback;
			OnPeerConnectionInterruptedInfo onPeerConnectionInterruptedInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnPeerConnectionInterruptedInfoInternal, OnPeerConnectionInterruptedCallback, OnPeerConnectionInterruptedInfo>(ref data, out onPeerConnectionInterruptedCallback, out onPeerConnectionInterruptedInfo);
			if (flag)
			{
				onPeerConnectionInterruptedCallback(ref onPeerConnectionInterruptedInfo);
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0001D4A8 File Offset: 0x0001B6A8
		[MonoPInvokeCallback(typeof(OnQueryNATTypeCompleteCallbackInternal))]
		internal static void OnQueryNATTypeCompleteCallbackInternalImplementation(ref OnQueryNATTypeCompleteInfoInternal data)
		{
			OnQueryNATTypeCompleteCallback onQueryNATTypeCompleteCallback;
			OnQueryNATTypeCompleteInfo onQueryNATTypeCompleteInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnQueryNATTypeCompleteInfoInternal, OnQueryNATTypeCompleteCallback, OnQueryNATTypeCompleteInfo>(ref data, out onQueryNATTypeCompleteCallback, out onQueryNATTypeCompleteInfo);
			if (flag)
			{
				onQueryNATTypeCompleteCallback(ref onQueryNATTypeCompleteInfo);
			}
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0001D4D0 File Offset: 0x0001B6D0
		[MonoPInvokeCallback(typeof(OnRemoteConnectionClosedCallbackInternal))]
		internal static void OnRemoteConnectionClosedCallbackInternalImplementation(ref OnRemoteConnectionClosedInfoInternal data)
		{
			OnRemoteConnectionClosedCallback onRemoteConnectionClosedCallback;
			OnRemoteConnectionClosedInfo onRemoteConnectionClosedInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnRemoteConnectionClosedInfoInternal, OnRemoteConnectionClosedCallback, OnRemoteConnectionClosedInfo>(ref data, out onRemoteConnectionClosedCallback, out onRemoteConnectionClosedInfo);
			if (flag)
			{
				onRemoteConnectionClosedCallback(ref onRemoteConnectionClosedInfo);
			}
		}

		// Token: 0x040008B3 RID: 2227
		public const int AcceptconnectionApiLatest = 1;

		// Token: 0x040008B4 RID: 2228
		public const int AddnotifyincomingpacketqueuefullApiLatest = 1;

		// Token: 0x040008B5 RID: 2229
		public const int AddnotifypeerconnectionclosedApiLatest = 1;

		// Token: 0x040008B6 RID: 2230
		public const int AddnotifypeerconnectionestablishedApiLatest = 1;

		// Token: 0x040008B7 RID: 2231
		public const int AddnotifypeerconnectioninterruptedApiLatest = 1;

		// Token: 0x040008B8 RID: 2232
		public const int AddnotifypeerconnectionrequestApiLatest = 1;

		// Token: 0x040008B9 RID: 2233
		public const int ClearpacketqueueApiLatest = 1;

		// Token: 0x040008BA RID: 2234
		public const int CloseconnectionApiLatest = 1;

		// Token: 0x040008BB RID: 2235
		public const int CloseconnectionsApiLatest = 1;

		// Token: 0x040008BC RID: 2236
		public const int GetnattypeApiLatest = 1;

		// Token: 0x040008BD RID: 2237
		public const int GetnextreceivedpacketsizeApiLatest = 2;

		// Token: 0x040008BE RID: 2238
		public const int GetpacketqueueinfoApiLatest = 1;

		// Token: 0x040008BF RID: 2239
		public const int GetportrangeApiLatest = 1;

		// Token: 0x040008C0 RID: 2240
		public const int GetrelaycontrolApiLatest = 1;

		// Token: 0x040008C1 RID: 2241
		public const int MaxConnections = 32;

		// Token: 0x040008C2 RID: 2242
		public const int MaxPacketSize = 1170;

		// Token: 0x040008C3 RID: 2243
		public const int MaxQueueSizeUnlimited = 0;

		// Token: 0x040008C4 RID: 2244
		public const int QuerynattypeApiLatest = 1;

		// Token: 0x040008C5 RID: 2245
		public const int ReceivepacketApiLatest = 2;

		// Token: 0x040008C6 RID: 2246
		public const int SendpacketApiLatest = 3;

		// Token: 0x040008C7 RID: 2247
		public const int SetpacketqueuesizeApiLatest = 1;

		// Token: 0x040008C8 RID: 2248
		public const int SetportrangeApiLatest = 1;

		// Token: 0x040008C9 RID: 2249
		public const int SetrelaycontrolApiLatest = 1;

		// Token: 0x040008CA RID: 2250
		public const int SocketidApiLatest = 1;

		// Token: 0x040008CB RID: 2251
		public const int SocketidSocketnameSize = 33;
	}
}
