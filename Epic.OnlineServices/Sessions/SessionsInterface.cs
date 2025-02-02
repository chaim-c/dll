using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000157 RID: 343
	public sealed class SessionsInterface : Handle
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x0000E2F9 File Offset: 0x0000C4F9
		public SessionsInterface()
		{
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0000E303 File Offset: 0x0000C503
		public SessionsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0000E310 File Offset: 0x0000C510
		public ulong AddNotifyJoinSessionAccepted(ref AddNotifyJoinSessionAcceptedOptions options, object clientData, OnJoinSessionAcceptedCallback notificationFn)
		{
			AddNotifyJoinSessionAcceptedOptionsInternal addNotifyJoinSessionAcceptedOptionsInternal = default(AddNotifyJoinSessionAcceptedOptionsInternal);
			addNotifyJoinSessionAcceptedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnJoinSessionAcceptedCallbackInternal onJoinSessionAcceptedCallbackInternal = new OnJoinSessionAcceptedCallbackInternal(SessionsInterface.OnJoinSessionAcceptedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onJoinSessionAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Sessions_AddNotifyJoinSessionAccepted(base.InnerHandle, ref addNotifyJoinSessionAcceptedOptionsInternal, zero, onJoinSessionAcceptedCallbackInternal);
			Helper.Dispose<AddNotifyJoinSessionAcceptedOptionsInternal>(ref addNotifyJoinSessionAcceptedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0000E37C File Offset: 0x0000C57C
		public ulong AddNotifySessionInviteAccepted(ref AddNotifySessionInviteAcceptedOptions options, object clientData, OnSessionInviteAcceptedCallback notificationFn)
		{
			AddNotifySessionInviteAcceptedOptionsInternal addNotifySessionInviteAcceptedOptionsInternal = default(AddNotifySessionInviteAcceptedOptionsInternal);
			addNotifySessionInviteAcceptedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSessionInviteAcceptedCallbackInternal onSessionInviteAcceptedCallbackInternal = new OnSessionInviteAcceptedCallbackInternal(SessionsInterface.OnSessionInviteAcceptedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onSessionInviteAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Sessions_AddNotifySessionInviteAccepted(base.InnerHandle, ref addNotifySessionInviteAcceptedOptionsInternal, zero, onSessionInviteAcceptedCallbackInternal);
			Helper.Dispose<AddNotifySessionInviteAcceptedOptionsInternal>(ref addNotifySessionInviteAcceptedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0000E3E8 File Offset: 0x0000C5E8
		public ulong AddNotifySessionInviteReceived(ref AddNotifySessionInviteReceivedOptions options, object clientData, OnSessionInviteReceivedCallback notificationFn)
		{
			AddNotifySessionInviteReceivedOptionsInternal addNotifySessionInviteReceivedOptionsInternal = default(AddNotifySessionInviteReceivedOptionsInternal);
			addNotifySessionInviteReceivedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSessionInviteReceivedCallbackInternal onSessionInviteReceivedCallbackInternal = new OnSessionInviteReceivedCallbackInternal(SessionsInterface.OnSessionInviteReceivedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onSessionInviteReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Sessions_AddNotifySessionInviteReceived(base.InnerHandle, ref addNotifySessionInviteReceivedOptionsInternal, zero, onSessionInviteReceivedCallbackInternal);
			Helper.Dispose<AddNotifySessionInviteReceivedOptionsInternal>(ref addNotifySessionInviteReceivedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0000E454 File Offset: 0x0000C654
		public Result CopyActiveSessionHandle(ref CopyActiveSessionHandleOptions options, out ActiveSession outSessionHandle)
		{
			CopyActiveSessionHandleOptionsInternal copyActiveSessionHandleOptionsInternal = default(CopyActiveSessionHandleOptionsInternal);
			copyActiveSessionHandleOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CopyActiveSessionHandle(base.InnerHandle, ref copyActiveSessionHandleOptionsInternal, ref zero);
			Helper.Dispose<CopyActiveSessionHandleOptionsInternal>(ref copyActiveSessionHandleOptionsInternal);
			Helper.Get<ActiveSession>(zero, out outSessionHandle);
			return result;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0000E4A0 File Offset: 0x0000C6A0
		public Result CopySessionHandleByInviteId(ref CopySessionHandleByInviteIdOptions options, out SessionDetails outSessionHandle)
		{
			CopySessionHandleByInviteIdOptionsInternal copySessionHandleByInviteIdOptionsInternal = default(CopySessionHandleByInviteIdOptionsInternal);
			copySessionHandleByInviteIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CopySessionHandleByInviteId(base.InnerHandle, ref copySessionHandleByInviteIdOptionsInternal, ref zero);
			Helper.Dispose<CopySessionHandleByInviteIdOptionsInternal>(ref copySessionHandleByInviteIdOptionsInternal);
			Helper.Get<SessionDetails>(zero, out outSessionHandle);
			return result;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		public Result CopySessionHandleByUiEventId(ref CopySessionHandleByUiEventIdOptions options, out SessionDetails outSessionHandle)
		{
			CopySessionHandleByUiEventIdOptionsInternal copySessionHandleByUiEventIdOptionsInternal = default(CopySessionHandleByUiEventIdOptionsInternal);
			copySessionHandleByUiEventIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CopySessionHandleByUiEventId(base.InnerHandle, ref copySessionHandleByUiEventIdOptionsInternal, ref zero);
			Helper.Dispose<CopySessionHandleByUiEventIdOptionsInternal>(ref copySessionHandleByUiEventIdOptionsInternal);
			Helper.Get<SessionDetails>(zero, out outSessionHandle);
			return result;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x0000E538 File Offset: 0x0000C738
		public Result CopySessionHandleForPresence(ref CopySessionHandleForPresenceOptions options, out SessionDetails outSessionHandle)
		{
			CopySessionHandleForPresenceOptionsInternal copySessionHandleForPresenceOptionsInternal = default(CopySessionHandleForPresenceOptionsInternal);
			copySessionHandleForPresenceOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CopySessionHandleForPresence(base.InnerHandle, ref copySessionHandleForPresenceOptionsInternal, ref zero);
			Helper.Dispose<CopySessionHandleForPresenceOptionsInternal>(ref copySessionHandleForPresenceOptionsInternal);
			Helper.Get<SessionDetails>(zero, out outSessionHandle);
			return result;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000E584 File Offset: 0x0000C784
		public Result CreateSessionModification(ref CreateSessionModificationOptions options, out SessionModification outSessionModificationHandle)
		{
			CreateSessionModificationOptionsInternal createSessionModificationOptionsInternal = default(CreateSessionModificationOptionsInternal);
			createSessionModificationOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CreateSessionModification(base.InnerHandle, ref createSessionModificationOptionsInternal, ref zero);
			Helper.Dispose<CreateSessionModificationOptionsInternal>(ref createSessionModificationOptionsInternal);
			Helper.Get<SessionModification>(zero, out outSessionModificationHandle);
			return result;
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		public Result CreateSessionSearch(ref CreateSessionSearchOptions options, out SessionSearch outSessionSearchHandle)
		{
			CreateSessionSearchOptionsInternal createSessionSearchOptionsInternal = default(CreateSessionSearchOptionsInternal);
			createSessionSearchOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_CreateSessionSearch(base.InnerHandle, ref createSessionSearchOptionsInternal, ref zero);
			Helper.Dispose<CreateSessionSearchOptionsInternal>(ref createSessionSearchOptionsInternal);
			Helper.Get<SessionSearch>(zero, out outSessionSearchHandle);
			return result;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0000E61C File Offset: 0x0000C81C
		public void DestroySession(ref DestroySessionOptions options, object clientData, OnDestroySessionCallback completionDelegate)
		{
			DestroySessionOptionsInternal destroySessionOptionsInternal = default(DestroySessionOptionsInternal);
			destroySessionOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnDestroySessionCallbackInternal onDestroySessionCallbackInternal = new OnDestroySessionCallbackInternal(SessionsInterface.OnDestroySessionCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onDestroySessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_DestroySession(base.InnerHandle, ref destroySessionOptionsInternal, zero, onDestroySessionCallbackInternal);
			Helper.Dispose<DestroySessionOptionsInternal>(ref destroySessionOptionsInternal);
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000E678 File Offset: 0x0000C878
		public Result DumpSessionState(ref DumpSessionStateOptions options)
		{
			DumpSessionStateOptionsInternal dumpSessionStateOptionsInternal = default(DumpSessionStateOptionsInternal);
			dumpSessionStateOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_Sessions_DumpSessionState(base.InnerHandle, ref dumpSessionStateOptionsInternal);
			Helper.Dispose<DumpSessionStateOptionsInternal>(ref dumpSessionStateOptionsInternal);
			return result;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0000E6B4 File Offset: 0x0000C8B4
		public void EndSession(ref EndSessionOptions options, object clientData, OnEndSessionCallback completionDelegate)
		{
			EndSessionOptionsInternal endSessionOptionsInternal = default(EndSessionOptionsInternal);
			endSessionOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnEndSessionCallbackInternal onEndSessionCallbackInternal = new OnEndSessionCallbackInternal(SessionsInterface.OnEndSessionCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onEndSessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_EndSession(base.InnerHandle, ref endSessionOptionsInternal, zero, onEndSessionCallbackInternal);
			Helper.Dispose<EndSessionOptionsInternal>(ref endSessionOptionsInternal);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0000E710 File Offset: 0x0000C910
		public uint GetInviteCount(ref GetInviteCountOptions options)
		{
			GetInviteCountOptionsInternal getInviteCountOptionsInternal = default(GetInviteCountOptionsInternal);
			getInviteCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Sessions_GetInviteCount(base.InnerHandle, ref getInviteCountOptionsInternal);
			Helper.Dispose<GetInviteCountOptionsInternal>(ref getInviteCountOptionsInternal);
			return result;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0000E74C File Offset: 0x0000C94C
		public Result GetInviteIdByIndex(ref GetInviteIdByIndexOptions options, out Utf8String outBuffer)
		{
			GetInviteIdByIndexOptionsInternal getInviteIdByIndexOptionsInternal = default(GetInviteIdByIndexOptionsInternal);
			getInviteIdByIndexOptionsInternal.Set(ref options);
			int size = 65;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Sessions_GetInviteIdByIndex(base.InnerHandle, ref getInviteIdByIndexOptionsInternal, intPtr, ref size);
			Helper.Dispose<GetInviteIdByIndexOptionsInternal>(ref getInviteIdByIndexOptionsInternal);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0000E7A8 File Offset: 0x0000C9A8
		public Result IsUserInSession(ref IsUserInSessionOptions options)
		{
			IsUserInSessionOptionsInternal isUserInSessionOptionsInternal = default(IsUserInSessionOptionsInternal);
			isUserInSessionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_Sessions_IsUserInSession(base.InnerHandle, ref isUserInSessionOptionsInternal);
			Helper.Dispose<IsUserInSessionOptionsInternal>(ref isUserInSessionOptionsInternal);
			return result;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0000E7E4 File Offset: 0x0000C9E4
		public void JoinSession(ref JoinSessionOptions options, object clientData, OnJoinSessionCallback completionDelegate)
		{
			JoinSessionOptionsInternal joinSessionOptionsInternal = default(JoinSessionOptionsInternal);
			joinSessionOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnJoinSessionCallbackInternal onJoinSessionCallbackInternal = new OnJoinSessionCallbackInternal(SessionsInterface.OnJoinSessionCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onJoinSessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_JoinSession(base.InnerHandle, ref joinSessionOptionsInternal, zero, onJoinSessionCallbackInternal);
			Helper.Dispose<JoinSessionOptionsInternal>(ref joinSessionOptionsInternal);
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0000E840 File Offset: 0x0000CA40
		public void QueryInvites(ref QueryInvitesOptions options, object clientData, OnQueryInvitesCallback completionDelegate)
		{
			QueryInvitesOptionsInternal queryInvitesOptionsInternal = default(QueryInvitesOptionsInternal);
			queryInvitesOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryInvitesCallbackInternal onQueryInvitesCallbackInternal = new OnQueryInvitesCallbackInternal(SessionsInterface.OnQueryInvitesCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryInvitesCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_QueryInvites(base.InnerHandle, ref queryInvitesOptionsInternal, zero, onQueryInvitesCallbackInternal);
			Helper.Dispose<QueryInvitesOptionsInternal>(ref queryInvitesOptionsInternal);
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0000E89C File Offset: 0x0000CA9C
		public void RegisterPlayers(ref RegisterPlayersOptions options, object clientData, OnRegisterPlayersCallback completionDelegate)
		{
			RegisterPlayersOptionsInternal registerPlayersOptionsInternal = default(RegisterPlayersOptionsInternal);
			registerPlayersOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnRegisterPlayersCallbackInternal onRegisterPlayersCallbackInternal = new OnRegisterPlayersCallbackInternal(SessionsInterface.OnRegisterPlayersCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onRegisterPlayersCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_RegisterPlayers(base.InnerHandle, ref registerPlayersOptionsInternal, zero, onRegisterPlayersCallbackInternal);
			Helper.Dispose<RegisterPlayersOptionsInternal>(ref registerPlayersOptionsInternal);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		public void RejectInvite(ref RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
		{
			RejectInviteOptionsInternal rejectInviteOptionsInternal = default(RejectInviteOptionsInternal);
			rejectInviteOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = new OnRejectInviteCallbackInternal(SessionsInterface.OnRejectInviteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onRejectInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_RejectInvite(base.InnerHandle, ref rejectInviteOptionsInternal, zero, onRejectInviteCallbackInternal);
			Helper.Dispose<RejectInviteOptionsInternal>(ref rejectInviteOptionsInternal);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0000E952 File Offset: 0x0000CB52
		public void RemoveNotifyJoinSessionAccepted(ulong inId)
		{
			Bindings.EOS_Sessions_RemoveNotifyJoinSessionAccepted(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000E969 File Offset: 0x0000CB69
		public void RemoveNotifySessionInviteAccepted(ulong inId)
		{
			Bindings.EOS_Sessions_RemoveNotifySessionInviteAccepted(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0000E980 File Offset: 0x0000CB80
		public void RemoveNotifySessionInviteReceived(ulong inId)
		{
			Bindings.EOS_Sessions_RemoveNotifySessionInviteReceived(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0000E998 File Offset: 0x0000CB98
		public void SendInvite(ref SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
		{
			SendInviteOptionsInternal sendInviteOptionsInternal = default(SendInviteOptionsInternal);
			sendInviteOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSendInviteCallbackInternal onSendInviteCallbackInternal = new OnSendInviteCallbackInternal(SessionsInterface.OnSendInviteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onSendInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_SendInvite(base.InnerHandle, ref sendInviteOptionsInternal, zero, onSendInviteCallbackInternal);
			Helper.Dispose<SendInviteOptionsInternal>(ref sendInviteOptionsInternal);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		public void StartSession(ref StartSessionOptions options, object clientData, OnStartSessionCallback completionDelegate)
		{
			StartSessionOptionsInternal startSessionOptionsInternal = default(StartSessionOptionsInternal);
			startSessionOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnStartSessionCallbackInternal onStartSessionCallbackInternal = new OnStartSessionCallbackInternal(SessionsInterface.OnStartSessionCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onStartSessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_StartSession(base.InnerHandle, ref startSessionOptionsInternal, zero, onStartSessionCallbackInternal);
			Helper.Dispose<StartSessionOptionsInternal>(ref startSessionOptionsInternal);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0000EA50 File Offset: 0x0000CC50
		public void UnregisterPlayers(ref UnregisterPlayersOptions options, object clientData, OnUnregisterPlayersCallback completionDelegate)
		{
			UnregisterPlayersOptionsInternal unregisterPlayersOptionsInternal = default(UnregisterPlayersOptionsInternal);
			unregisterPlayersOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUnregisterPlayersCallbackInternal onUnregisterPlayersCallbackInternal = new OnUnregisterPlayersCallbackInternal(SessionsInterface.OnUnregisterPlayersCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUnregisterPlayersCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_UnregisterPlayers(base.InnerHandle, ref unregisterPlayersOptionsInternal, zero, onUnregisterPlayersCallbackInternal);
			Helper.Dispose<UnregisterPlayersOptionsInternal>(ref unregisterPlayersOptionsInternal);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		public void UpdateSession(ref UpdateSessionOptions options, object clientData, OnUpdateSessionCallback completionDelegate)
		{
			UpdateSessionOptionsInternal updateSessionOptionsInternal = default(UpdateSessionOptionsInternal);
			updateSessionOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUpdateSessionCallbackInternal onUpdateSessionCallbackInternal = new OnUpdateSessionCallbackInternal(SessionsInterface.OnUpdateSessionCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUpdateSessionCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Sessions_UpdateSession(base.InnerHandle, ref updateSessionOptionsInternal, zero, onUpdateSessionCallbackInternal);
			Helper.Dispose<UpdateSessionOptionsInternal>(ref updateSessionOptionsInternal);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x0000EB08 File Offset: 0x0000CD08
		public Result UpdateSessionModification(ref UpdateSessionModificationOptions options, out SessionModification outSessionModificationHandle)
		{
			UpdateSessionModificationOptionsInternal updateSessionModificationOptionsInternal = default(UpdateSessionModificationOptionsInternal);
			updateSessionModificationOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Sessions_UpdateSessionModification(base.InnerHandle, ref updateSessionModificationOptionsInternal, ref zero);
			Helper.Dispose<UpdateSessionModificationOptionsInternal>(ref updateSessionModificationOptionsInternal);
			Helper.Get<SessionModification>(zero, out outSessionModificationHandle);
			return result;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0000EB54 File Offset: 0x0000CD54
		[MonoPInvokeCallback(typeof(OnDestroySessionCallbackInternal))]
		internal static void OnDestroySessionCallbackInternalImplementation(ref DestroySessionCallbackInfoInternal data)
		{
			OnDestroySessionCallback onDestroySessionCallback;
			DestroySessionCallbackInfo destroySessionCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<DestroySessionCallbackInfoInternal, OnDestroySessionCallback, DestroySessionCallbackInfo>(ref data, out onDestroySessionCallback, out destroySessionCallbackInfo);
			if (flag)
			{
				onDestroySessionCallback(ref destroySessionCallbackInfo);
			}
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0000EB7C File Offset: 0x0000CD7C
		[MonoPInvokeCallback(typeof(OnEndSessionCallbackInternal))]
		internal static void OnEndSessionCallbackInternalImplementation(ref EndSessionCallbackInfoInternal data)
		{
			OnEndSessionCallback onEndSessionCallback;
			EndSessionCallbackInfo endSessionCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<EndSessionCallbackInfoInternal, OnEndSessionCallback, EndSessionCallbackInfo>(ref data, out onEndSessionCallback, out endSessionCallbackInfo);
			if (flag)
			{
				onEndSessionCallback(ref endSessionCallbackInfo);
			}
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0000EBA4 File Offset: 0x0000CDA4
		[MonoPInvokeCallback(typeof(OnJoinSessionAcceptedCallbackInternal))]
		internal static void OnJoinSessionAcceptedCallbackInternalImplementation(ref JoinSessionAcceptedCallbackInfoInternal data)
		{
			OnJoinSessionAcceptedCallback onJoinSessionAcceptedCallback;
			JoinSessionAcceptedCallbackInfo joinSessionAcceptedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<JoinSessionAcceptedCallbackInfoInternal, OnJoinSessionAcceptedCallback, JoinSessionAcceptedCallbackInfo>(ref data, out onJoinSessionAcceptedCallback, out joinSessionAcceptedCallbackInfo);
			if (flag)
			{
				onJoinSessionAcceptedCallback(ref joinSessionAcceptedCallbackInfo);
			}
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0000EBCC File Offset: 0x0000CDCC
		[MonoPInvokeCallback(typeof(OnJoinSessionCallbackInternal))]
		internal static void OnJoinSessionCallbackInternalImplementation(ref JoinSessionCallbackInfoInternal data)
		{
			OnJoinSessionCallback onJoinSessionCallback;
			JoinSessionCallbackInfo joinSessionCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<JoinSessionCallbackInfoInternal, OnJoinSessionCallback, JoinSessionCallbackInfo>(ref data, out onJoinSessionCallback, out joinSessionCallbackInfo);
			if (flag)
			{
				onJoinSessionCallback(ref joinSessionCallbackInfo);
			}
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		[MonoPInvokeCallback(typeof(OnQueryInvitesCallbackInternal))]
		internal static void OnQueryInvitesCallbackInternalImplementation(ref QueryInvitesCallbackInfoInternal data)
		{
			OnQueryInvitesCallback onQueryInvitesCallback;
			QueryInvitesCallbackInfo queryInvitesCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryInvitesCallbackInfoInternal, OnQueryInvitesCallback, QueryInvitesCallbackInfo>(ref data, out onQueryInvitesCallback, out queryInvitesCallbackInfo);
			if (flag)
			{
				onQueryInvitesCallback(ref queryInvitesCallbackInfo);
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000EC1C File Offset: 0x0000CE1C
		[MonoPInvokeCallback(typeof(OnRegisterPlayersCallbackInternal))]
		internal static void OnRegisterPlayersCallbackInternalImplementation(ref RegisterPlayersCallbackInfoInternal data)
		{
			OnRegisterPlayersCallback onRegisterPlayersCallback;
			RegisterPlayersCallbackInfo registerPlayersCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<RegisterPlayersCallbackInfoInternal, OnRegisterPlayersCallback, RegisterPlayersCallbackInfo>(ref data, out onRegisterPlayersCallback, out registerPlayersCallbackInfo);
			if (flag)
			{
				onRegisterPlayersCallback(ref registerPlayersCallbackInfo);
			}
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0000EC44 File Offset: 0x0000CE44
		[MonoPInvokeCallback(typeof(OnRejectInviteCallbackInternal))]
		internal static void OnRejectInviteCallbackInternalImplementation(ref RejectInviteCallbackInfoInternal data)
		{
			OnRejectInviteCallback onRejectInviteCallback;
			RejectInviteCallbackInfo rejectInviteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<RejectInviteCallbackInfoInternal, OnRejectInviteCallback, RejectInviteCallbackInfo>(ref data, out onRejectInviteCallback, out rejectInviteCallbackInfo);
			if (flag)
			{
				onRejectInviteCallback(ref rejectInviteCallbackInfo);
			}
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		[MonoPInvokeCallback(typeof(OnSendInviteCallbackInternal))]
		internal static void OnSendInviteCallbackInternalImplementation(ref SendInviteCallbackInfoInternal data)
		{
			OnSendInviteCallback onSendInviteCallback;
			SendInviteCallbackInfo sendInviteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SendInviteCallbackInfoInternal, OnSendInviteCallback, SendInviteCallbackInfo>(ref data, out onSendInviteCallback, out sendInviteCallbackInfo);
			if (flag)
			{
				onSendInviteCallback(ref sendInviteCallbackInfo);
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0000EC94 File Offset: 0x0000CE94
		[MonoPInvokeCallback(typeof(OnSessionInviteAcceptedCallbackInternal))]
		internal static void OnSessionInviteAcceptedCallbackInternalImplementation(ref SessionInviteAcceptedCallbackInfoInternal data)
		{
			OnSessionInviteAcceptedCallback onSessionInviteAcceptedCallback;
			SessionInviteAcceptedCallbackInfo sessionInviteAcceptedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SessionInviteAcceptedCallbackInfoInternal, OnSessionInviteAcceptedCallback, SessionInviteAcceptedCallbackInfo>(ref data, out onSessionInviteAcceptedCallback, out sessionInviteAcceptedCallbackInfo);
			if (flag)
			{
				onSessionInviteAcceptedCallback(ref sessionInviteAcceptedCallbackInfo);
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0000ECBC File Offset: 0x0000CEBC
		[MonoPInvokeCallback(typeof(OnSessionInviteReceivedCallbackInternal))]
		internal static void OnSessionInviteReceivedCallbackInternalImplementation(ref SessionInviteReceivedCallbackInfoInternal data)
		{
			OnSessionInviteReceivedCallback onSessionInviteReceivedCallback;
			SessionInviteReceivedCallbackInfo sessionInviteReceivedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SessionInviteReceivedCallbackInfoInternal, OnSessionInviteReceivedCallback, SessionInviteReceivedCallbackInfo>(ref data, out onSessionInviteReceivedCallback, out sessionInviteReceivedCallbackInfo);
			if (flag)
			{
				onSessionInviteReceivedCallback(ref sessionInviteReceivedCallbackInfo);
			}
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		[MonoPInvokeCallback(typeof(OnStartSessionCallbackInternal))]
		internal static void OnStartSessionCallbackInternalImplementation(ref StartSessionCallbackInfoInternal data)
		{
			OnStartSessionCallback onStartSessionCallback;
			StartSessionCallbackInfo startSessionCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<StartSessionCallbackInfoInternal, OnStartSessionCallback, StartSessionCallbackInfo>(ref data, out onStartSessionCallback, out startSessionCallbackInfo);
			if (flag)
			{
				onStartSessionCallback(ref startSessionCallbackInfo);
			}
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		[MonoPInvokeCallback(typeof(OnUnregisterPlayersCallbackInternal))]
		internal static void OnUnregisterPlayersCallbackInternalImplementation(ref UnregisterPlayersCallbackInfoInternal data)
		{
			OnUnregisterPlayersCallback onUnregisterPlayersCallback;
			UnregisterPlayersCallbackInfo unregisterPlayersCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UnregisterPlayersCallbackInfoInternal, OnUnregisterPlayersCallback, UnregisterPlayersCallbackInfo>(ref data, out onUnregisterPlayersCallback, out unregisterPlayersCallbackInfo);
			if (flag)
			{
				onUnregisterPlayersCallback(ref unregisterPlayersCallbackInfo);
			}
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0000ED34 File Offset: 0x0000CF34
		[MonoPInvokeCallback(typeof(OnUpdateSessionCallbackInternal))]
		internal static void OnUpdateSessionCallbackInternalImplementation(ref UpdateSessionCallbackInfoInternal data)
		{
			OnUpdateSessionCallback onUpdateSessionCallback;
			UpdateSessionCallbackInfo updateSessionCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UpdateSessionCallbackInfoInternal, OnUpdateSessionCallback, UpdateSessionCallbackInfo>(ref data, out onUpdateSessionCallback, out updateSessionCallbackInfo);
			if (flag)
			{
				onUpdateSessionCallback(ref updateSessionCallbackInfo);
			}
		}

		// Token: 0x0400047B RID: 1147
		public const int AddnotifyjoinsessionacceptedApiLatest = 1;

		// Token: 0x0400047C RID: 1148
		public const int AddnotifysessioninviteacceptedApiLatest = 1;

		// Token: 0x0400047D RID: 1149
		public const int AddnotifysessioninvitereceivedApiLatest = 1;

		// Token: 0x0400047E RID: 1150
		public const int AttributedataApiLatest = 1;

		// Token: 0x0400047F RID: 1151
		public const int CopyactivesessionhandleApiLatest = 1;

		// Token: 0x04000480 RID: 1152
		public const int CopysessionhandlebyinviteidApiLatest = 1;

		// Token: 0x04000481 RID: 1153
		public const int CopysessionhandlebyuieventidApiLatest = 1;

		// Token: 0x04000482 RID: 1154
		public const int CopysessionhandleforpresenceApiLatest = 1;

		// Token: 0x04000483 RID: 1155
		public const int CreatesessionmodificationApiLatest = 4;

		// Token: 0x04000484 RID: 1156
		public const int CreatesessionsearchApiLatest = 1;

		// Token: 0x04000485 RID: 1157
		public const int DestroysessionApiLatest = 1;

		// Token: 0x04000486 RID: 1158
		public const int DumpsessionstateApiLatest = 1;

		// Token: 0x04000487 RID: 1159
		public const int EndsessionApiLatest = 1;

		// Token: 0x04000488 RID: 1160
		public const int GetinvitecountApiLatest = 1;

		// Token: 0x04000489 RID: 1161
		public const int GetinviteidbyindexApiLatest = 1;

		// Token: 0x0400048A RID: 1162
		public const int InviteidMaxLength = 64;

		// Token: 0x0400048B RID: 1163
		public const int IsuserinsessionApiLatest = 1;

		// Token: 0x0400048C RID: 1164
		public const int JoinsessionApiLatest = 2;

		// Token: 0x0400048D RID: 1165
		public const int MaxSearchResults = 200;

		// Token: 0x0400048E RID: 1166
		public const int Maxregisteredplayers = 1000;

		// Token: 0x0400048F RID: 1167
		public const int QueryinvitesApiLatest = 1;

		// Token: 0x04000490 RID: 1168
		public const int RegisterplayersApiLatest = 2;

		// Token: 0x04000491 RID: 1169
		public const int RejectinviteApiLatest = 1;

		// Token: 0x04000492 RID: 1170
		public static readonly Utf8String SearchBucketId = "bucket";

		// Token: 0x04000493 RID: 1171
		public static readonly Utf8String SearchEmptyServersOnly = "emptyonly";

		// Token: 0x04000494 RID: 1172
		public static readonly Utf8String SearchMinslotsavailable = "minslotsavailable";

		// Token: 0x04000495 RID: 1173
		public static readonly Utf8String SearchNonemptyServersOnly = "nonemptyonly";

		// Token: 0x04000496 RID: 1174
		public const int SendinviteApiLatest = 1;

		// Token: 0x04000497 RID: 1175
		public const int SessionattributeApiLatest = 1;

		// Token: 0x04000498 RID: 1176
		public const int SessionattributedataApiLatest = 1;

		// Token: 0x04000499 RID: 1177
		public const int StartsessionApiLatest = 1;

		// Token: 0x0400049A RID: 1178
		public const int UnregisterplayersApiLatest = 2;

		// Token: 0x0400049B RID: 1179
		public const int UpdatesessionApiLatest = 1;

		// Token: 0x0400049C RID: 1180
		public const int UpdatesessionmodificationApiLatest = 1;
	}
}
