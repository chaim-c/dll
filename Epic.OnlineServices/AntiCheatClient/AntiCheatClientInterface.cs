using System;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200061C RID: 1564
	public sealed class AntiCheatClientInterface : Handle
	{
		// Token: 0x060027F5 RID: 10229 RVA: 0x0003B834 File Offset: 0x00039A34
		public AntiCheatClientInterface()
		{
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0003B84A File Offset: 0x00039A4A
		public AntiCheatClientInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x0003B864 File Offset: 0x00039A64
		public Result AddExternalIntegrityCatalog(ref AddExternalIntegrityCatalogOptions options)
		{
			AddExternalIntegrityCatalogOptionsInternal addExternalIntegrityCatalogOptionsInternal = default(AddExternalIntegrityCatalogOptionsInternal);
			addExternalIntegrityCatalogOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatClient_AddExternalIntegrityCatalog(base.InnerHandle, ref addExternalIntegrityCatalogOptionsInternal);
			Helper.Dispose<AddExternalIntegrityCatalogOptionsInternal>(ref addExternalIntegrityCatalogOptionsInternal);
			return result;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x0003B8A0 File Offset: 0x00039AA0
		public ulong AddNotifyClientIntegrityViolated(ref AddNotifyClientIntegrityViolatedOptions options, object clientData, OnClientIntegrityViolatedCallback notificationFn)
		{
			AddNotifyClientIntegrityViolatedOptionsInternal addNotifyClientIntegrityViolatedOptionsInternal = default(AddNotifyClientIntegrityViolatedOptionsInternal);
			addNotifyClientIntegrityViolatedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnClientIntegrityViolatedCallbackInternal onClientIntegrityViolatedCallbackInternal = new OnClientIntegrityViolatedCallbackInternal(AntiCheatClientInterface.OnClientIntegrityViolatedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onClientIntegrityViolatedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatClient_AddNotifyClientIntegrityViolated(base.InnerHandle, ref addNotifyClientIntegrityViolatedOptionsInternal, zero, onClientIntegrityViolatedCallbackInternal);
			Helper.Dispose<AddNotifyClientIntegrityViolatedOptionsInternal>(ref addNotifyClientIntegrityViolatedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x0003B90C File Offset: 0x00039B0C
		public ulong AddNotifyMessageToPeer(ref AddNotifyMessageToPeerOptions options, object clientData, OnMessageToPeerCallback notificationFn)
		{
			AddNotifyMessageToPeerOptionsInternal addNotifyMessageToPeerOptionsInternal = default(AddNotifyMessageToPeerOptionsInternal);
			addNotifyMessageToPeerOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnMessageToPeerCallbackInternal onMessageToPeerCallbackInternal = new OnMessageToPeerCallbackInternal(AntiCheatClientInterface.OnMessageToPeerCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onMessageToPeerCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatClient_AddNotifyMessageToPeer(base.InnerHandle, ref addNotifyMessageToPeerOptionsInternal, zero, onMessageToPeerCallbackInternal);
			Helper.Dispose<AddNotifyMessageToPeerOptionsInternal>(ref addNotifyMessageToPeerOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x0003B978 File Offset: 0x00039B78
		public ulong AddNotifyMessageToServer(ref AddNotifyMessageToServerOptions options, object clientData, OnMessageToServerCallback notificationFn)
		{
			AddNotifyMessageToServerOptionsInternal addNotifyMessageToServerOptionsInternal = default(AddNotifyMessageToServerOptionsInternal);
			addNotifyMessageToServerOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnMessageToServerCallbackInternal onMessageToServerCallbackInternal = new OnMessageToServerCallbackInternal(AntiCheatClientInterface.OnMessageToServerCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onMessageToServerCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatClient_AddNotifyMessageToServer(base.InnerHandle, ref addNotifyMessageToServerOptionsInternal, zero, onMessageToServerCallbackInternal);
			Helper.Dispose<AddNotifyMessageToServerOptionsInternal>(ref addNotifyMessageToServerOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x0003B9E4 File Offset: 0x00039BE4
		public ulong AddNotifyPeerActionRequired(ref AddNotifyPeerActionRequiredOptions options, object clientData, OnPeerActionRequiredCallback notificationFn)
		{
			AddNotifyPeerActionRequiredOptionsInternal addNotifyPeerActionRequiredOptionsInternal = default(AddNotifyPeerActionRequiredOptionsInternal);
			addNotifyPeerActionRequiredOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnPeerActionRequiredCallbackInternal onPeerActionRequiredCallbackInternal = new OnPeerActionRequiredCallbackInternal(AntiCheatClientInterface.OnPeerActionRequiredCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onPeerActionRequiredCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatClient_AddNotifyPeerActionRequired(base.InnerHandle, ref addNotifyPeerActionRequiredOptionsInternal, zero, onPeerActionRequiredCallbackInternal);
			Helper.Dispose<AddNotifyPeerActionRequiredOptionsInternal>(ref addNotifyPeerActionRequiredOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x0003BA50 File Offset: 0x00039C50
		public ulong AddNotifyPeerAuthStatusChanged(ref AddNotifyPeerAuthStatusChangedOptions options, object clientData, OnPeerAuthStatusChangedCallback notificationFn)
		{
			AddNotifyPeerAuthStatusChangedOptionsInternal addNotifyPeerAuthStatusChangedOptionsInternal = default(AddNotifyPeerAuthStatusChangedOptionsInternal);
			addNotifyPeerAuthStatusChangedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnPeerAuthStatusChangedCallbackInternal onPeerAuthStatusChangedCallbackInternal = new OnPeerAuthStatusChangedCallbackInternal(AntiCheatClientInterface.OnPeerAuthStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onPeerAuthStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatClient_AddNotifyPeerAuthStatusChanged(base.InnerHandle, ref addNotifyPeerAuthStatusChangedOptionsInternal, zero, onPeerAuthStatusChangedCallbackInternal);
			Helper.Dispose<AddNotifyPeerAuthStatusChangedOptionsInternal>(ref addNotifyPeerAuthStatusChangedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x0003BABC File Offset: 0x00039CBC
		public Result BeginSession(ref BeginSessionOptions options)
		{
			BeginSessionOptionsInternal beginSessionOptionsInternal = default(BeginSessionOptionsInternal);
			beginSessionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatClient_BeginSession(base.InnerHandle, ref beginSessionOptionsInternal);
			Helper.Dispose<BeginSessionOptionsInternal>(ref beginSessionOptionsInternal);
			return result;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x0003BAF8 File Offset: 0x00039CF8
		public Result EndSession(ref EndSessionOptions options)
		{
			EndSessionOptionsInternal endSessionOptionsInternal = default(EndSessionOptionsInternal);
			endSessionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatClient_EndSession(base.InnerHandle, ref endSessionOptionsInternal);
			Helper.Dispose<EndSessionOptionsInternal>(ref endSessionOptionsInternal);
			return result;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x0003BB34 File Offset: 0x00039D34
		public Result GetProtectMessageOutputLength(ref GetProtectMessageOutputLengthOptions options, out uint outBufferSizeBytes)
		{
			GetProtectMessageOutputLengthOptionsInternal getProtectMessageOutputLengthOptionsInternal = default(GetProtectMessageOutputLengthOptionsInternal);
			getProtectMessageOutputLengthOptionsInternal.Set(ref options);
			outBufferSizeBytes = Helper.GetDefault<uint>();
			Result result = Bindings.EOS_AntiCheatClient_GetProtectMessageOutputLength(base.InnerHandle, ref getProtectMessageOutputLengthOptionsInternal, ref outBufferSizeBytes);
			Helper.Dispose<GetProtectMessageOutputLengthOptionsInternal>(ref getProtectMessageOutputLengthOptionsInternal);
			return result;
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0003BB78 File Offset: 0x00039D78
		public Result PollStatus(ref PollStatusOptions options, out AntiCheatClientViolationType outViolationType, out Utf8String outMessage)
		{
			PollStatusOptionsInternal pollStatusOptionsInternal = default(PollStatusOptionsInternal);
			pollStatusOptionsInternal.Set(ref options);
			outViolationType = Helper.GetDefault<AntiCheatClientViolationType>();
			uint outMessageLength = options.OutMessageLength;
			IntPtr intPtr = Helper.AddAllocation(outMessageLength);
			Result result = Bindings.EOS_AntiCheatClient_PollStatus(base.InnerHandle, ref pollStatusOptionsInternal, ref outViolationType, intPtr);
			Helper.Dispose<PollStatusOptionsInternal>(ref pollStatusOptionsInternal);
			Helper.Get(intPtr, out outMessage);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0003BBDC File Offset: 0x00039DDC
		public Result ProtectMessage(ref ProtectMessageOptions options, ArraySegment<byte> outBuffer, out uint outBytesWritten)
		{
			ProtectMessageOptionsInternal protectMessageOptionsInternal = default(ProtectMessageOptionsInternal);
			protectMessageOptionsInternal.Set(ref options);
			outBytesWritten = 0U;
			IntPtr outBuffer2 = Helper.AddPinnedBuffer(outBuffer);
			Result result = Bindings.EOS_AntiCheatClient_ProtectMessage(base.InnerHandle, ref protectMessageOptionsInternal, outBuffer2, ref outBytesWritten);
			Helper.Dispose<ProtectMessageOptionsInternal>(ref protectMessageOptionsInternal);
			Helper.Dispose(ref outBuffer2);
			return result;
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0003BC2C File Offset: 0x00039E2C
		public Result ReceiveMessageFromPeer(ref ReceiveMessageFromPeerOptions options)
		{
			ReceiveMessageFromPeerOptionsInternal receiveMessageFromPeerOptionsInternal = default(ReceiveMessageFromPeerOptionsInternal);
			receiveMessageFromPeerOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatClient_ReceiveMessageFromPeer(base.InnerHandle, ref receiveMessageFromPeerOptionsInternal);
			Helper.Dispose<ReceiveMessageFromPeerOptionsInternal>(ref receiveMessageFromPeerOptionsInternal);
			return result;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0003BC68 File Offset: 0x00039E68
		public Result ReceiveMessageFromServer(ref ReceiveMessageFromServerOptions options)
		{
			ReceiveMessageFromServerOptionsInternal receiveMessageFromServerOptionsInternal = default(ReceiveMessageFromServerOptionsInternal);
			receiveMessageFromServerOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatClient_ReceiveMessageFromServer(base.InnerHandle, ref receiveMessageFromServerOptionsInternal);
			Helper.Dispose<ReceiveMessageFromServerOptionsInternal>(ref receiveMessageFromServerOptionsInternal);
			return result;
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0003BCA4 File Offset: 0x00039EA4
		public Result RegisterPeer(ref RegisterPeerOptions options)
		{
			RegisterPeerOptionsInternal registerPeerOptionsInternal = default(RegisterPeerOptionsInternal);
			registerPeerOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatClient_RegisterPeer(base.InnerHandle, ref registerPeerOptionsInternal);
			Helper.Dispose<RegisterPeerOptionsInternal>(ref registerPeerOptionsInternal);
			return result;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0003BCDE File Offset: 0x00039EDE
		public void RemoveNotifyClientIntegrityViolated(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyClientIntegrityViolated(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x0003BCF5 File Offset: 0x00039EF5
		public void RemoveNotifyMessageToPeer(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyMessageToPeer(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x0003BD0C File Offset: 0x00039F0C
		public void RemoveNotifyMessageToServer(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyMessageToServer(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x0003BD23 File Offset: 0x00039F23
		public void RemoveNotifyPeerActionRequired(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyPeerActionRequired(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x0003BD3A File Offset: 0x00039F3A
		public void RemoveNotifyPeerAuthStatusChanged(ulong notificationId)
		{
			Bindings.EOS_AntiCheatClient_RemoveNotifyPeerAuthStatusChanged(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x0003BD54 File Offset: 0x00039F54
		public Result UnprotectMessage(ref UnprotectMessageOptions options, ArraySegment<byte> outBuffer, out uint outBytesWritten)
		{
			UnprotectMessageOptionsInternal unprotectMessageOptionsInternal = default(UnprotectMessageOptionsInternal);
			unprotectMessageOptionsInternal.Set(ref options);
			outBytesWritten = 0U;
			IntPtr outBuffer2 = Helper.AddPinnedBuffer(outBuffer);
			Result result = Bindings.EOS_AntiCheatClient_UnprotectMessage(base.InnerHandle, ref unprotectMessageOptionsInternal, outBuffer2, ref outBytesWritten);
			Helper.Dispose<UnprotectMessageOptionsInternal>(ref unprotectMessageOptionsInternal);
			Helper.Dispose(ref outBuffer2);
			return result;
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x0003BDA4 File Offset: 0x00039FA4
		public Result UnregisterPeer(ref UnregisterPeerOptions options)
		{
			UnregisterPeerOptionsInternal unregisterPeerOptionsInternal = default(UnregisterPeerOptionsInternal);
			unregisterPeerOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatClient_UnregisterPeer(base.InnerHandle, ref unregisterPeerOptionsInternal);
			Helper.Dispose<UnregisterPeerOptionsInternal>(ref unregisterPeerOptionsInternal);
			return result;
		}

		// Token: 0x0600280C RID: 10252 RVA: 0x0003BDE0 File Offset: 0x00039FE0
		[MonoPInvokeCallback(typeof(OnClientIntegrityViolatedCallbackInternal))]
		internal static void OnClientIntegrityViolatedCallbackInternalImplementation(ref OnClientIntegrityViolatedCallbackInfoInternal data)
		{
			OnClientIntegrityViolatedCallback onClientIntegrityViolatedCallback;
			OnClientIntegrityViolatedCallbackInfo onClientIntegrityViolatedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnClientIntegrityViolatedCallbackInfoInternal, OnClientIntegrityViolatedCallback, OnClientIntegrityViolatedCallbackInfo>(ref data, out onClientIntegrityViolatedCallback, out onClientIntegrityViolatedCallbackInfo);
			if (flag)
			{
				onClientIntegrityViolatedCallback(ref onClientIntegrityViolatedCallbackInfo);
			}
		}

		// Token: 0x0600280D RID: 10253 RVA: 0x0003BE08 File Offset: 0x0003A008
		[MonoPInvokeCallback(typeof(OnMessageToPeerCallbackInternal))]
		internal static void OnMessageToPeerCallbackInternalImplementation(ref OnMessageToClientCallbackInfoInternal data)
		{
			OnMessageToPeerCallback onMessageToPeerCallback;
			OnMessageToClientCallbackInfo onMessageToClientCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnMessageToClientCallbackInfoInternal, OnMessageToPeerCallback, OnMessageToClientCallbackInfo>(ref data, out onMessageToPeerCallback, out onMessageToClientCallbackInfo);
			if (flag)
			{
				onMessageToPeerCallback(ref onMessageToClientCallbackInfo);
			}
		}

		// Token: 0x0600280E RID: 10254 RVA: 0x0003BE30 File Offset: 0x0003A030
		[MonoPInvokeCallback(typeof(OnMessageToServerCallbackInternal))]
		internal static void OnMessageToServerCallbackInternalImplementation(ref OnMessageToServerCallbackInfoInternal data)
		{
			OnMessageToServerCallback onMessageToServerCallback;
			OnMessageToServerCallbackInfo onMessageToServerCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnMessageToServerCallbackInfoInternal, OnMessageToServerCallback, OnMessageToServerCallbackInfo>(ref data, out onMessageToServerCallback, out onMessageToServerCallbackInfo);
			if (flag)
			{
				onMessageToServerCallback(ref onMessageToServerCallbackInfo);
			}
		}

		// Token: 0x0600280F RID: 10255 RVA: 0x0003BE58 File Offset: 0x0003A058
		[MonoPInvokeCallback(typeof(OnPeerActionRequiredCallbackInternal))]
		internal static void OnPeerActionRequiredCallbackInternalImplementation(ref OnClientActionRequiredCallbackInfoInternal data)
		{
			OnPeerActionRequiredCallback onPeerActionRequiredCallback;
			OnClientActionRequiredCallbackInfo onClientActionRequiredCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnClientActionRequiredCallbackInfoInternal, OnPeerActionRequiredCallback, OnClientActionRequiredCallbackInfo>(ref data, out onPeerActionRequiredCallback, out onClientActionRequiredCallbackInfo);
			if (flag)
			{
				onPeerActionRequiredCallback(ref onClientActionRequiredCallbackInfo);
			}
		}

		// Token: 0x06002810 RID: 10256 RVA: 0x0003BE80 File Offset: 0x0003A080
		[MonoPInvokeCallback(typeof(OnPeerAuthStatusChangedCallbackInternal))]
		internal static void OnPeerAuthStatusChangedCallbackInternalImplementation(ref OnClientAuthStatusChangedCallbackInfoInternal data)
		{
			OnPeerAuthStatusChangedCallback onPeerAuthStatusChangedCallback;
			OnClientAuthStatusChangedCallbackInfo onClientAuthStatusChangedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnClientAuthStatusChangedCallbackInfoInternal, OnPeerAuthStatusChangedCallback, OnClientAuthStatusChangedCallbackInfo>(ref data, out onPeerAuthStatusChangedCallback, out onClientAuthStatusChangedCallbackInfo);
			if (flag)
			{
				onPeerAuthStatusChangedCallback(ref onClientAuthStatusChangedCallbackInfo);
			}
		}

		// Token: 0x040011F4 RID: 4596
		public const int AddexternalintegritycatalogApiLatest = 1;

		// Token: 0x040011F5 RID: 4597
		public const int AddnotifyclientintegrityviolatedApiLatest = 1;

		// Token: 0x040011F6 RID: 4598
		public const int AddnotifymessagetopeerApiLatest = 1;

		// Token: 0x040011F7 RID: 4599
		public const int AddnotifymessagetoserverApiLatest = 1;

		// Token: 0x040011F8 RID: 4600
		public const int AddnotifypeeractionrequiredApiLatest = 1;

		// Token: 0x040011F9 RID: 4601
		public const int AddnotifypeerauthstatuschangedApiLatest = 1;

		// Token: 0x040011FA RID: 4602
		public const int BeginsessionApiLatest = 3;

		// Token: 0x040011FB RID: 4603
		public const int EndsessionApiLatest = 1;

		// Token: 0x040011FC RID: 4604
		public const int GetprotectmessageoutputlengthApiLatest = 1;

		// Token: 0x040011FD RID: 4605
		public IntPtr PeerSelf = (IntPtr)(-1);

		// Token: 0x040011FE RID: 4606
		public const int PollstatusApiLatest = 1;

		// Token: 0x040011FF RID: 4607
		public const int ProtectmessageApiLatest = 1;

		// Token: 0x04001200 RID: 4608
		public const int ReceivemessagefrompeerApiLatest = 1;

		// Token: 0x04001201 RID: 4609
		public const int ReceivemessagefromserverApiLatest = 1;

		// Token: 0x04001202 RID: 4610
		public const int RegisterpeerApiLatest = 3;

		// Token: 0x04001203 RID: 4611
		public const int RegisterpeerMaxAuthenticationtimeout = 120;

		// Token: 0x04001204 RID: 4612
		public const int RegisterpeerMinAuthenticationtimeout = 40;

		// Token: 0x04001205 RID: 4613
		public const int UnprotectmessageApiLatest = 1;

		// Token: 0x04001206 RID: 4614
		public const int UnregisterpeerApiLatest = 1;
	}
}
