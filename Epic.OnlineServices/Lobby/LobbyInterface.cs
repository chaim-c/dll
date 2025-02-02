using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200037C RID: 892
	public sealed class LobbyInterface : Handle
	{
		// Token: 0x06001774 RID: 6004 RVA: 0x00022F44 File Offset: 0x00021144
		public LobbyInterface()
		{
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x00022F4E File Offset: 0x0002114E
		public LobbyInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x00022F5C File Offset: 0x0002115C
		public ulong AddNotifyJoinLobbyAccepted(ref AddNotifyJoinLobbyAcceptedOptions options, object clientData, OnJoinLobbyAcceptedCallback notificationFn)
		{
			AddNotifyJoinLobbyAcceptedOptionsInternal addNotifyJoinLobbyAcceptedOptionsInternal = default(AddNotifyJoinLobbyAcceptedOptionsInternal);
			addNotifyJoinLobbyAcceptedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnJoinLobbyAcceptedCallbackInternal onJoinLobbyAcceptedCallbackInternal = new OnJoinLobbyAcceptedCallbackInternal(LobbyInterface.OnJoinLobbyAcceptedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onJoinLobbyAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyJoinLobbyAccepted(base.InnerHandle, ref addNotifyJoinLobbyAcceptedOptionsInternal, zero, onJoinLobbyAcceptedCallbackInternal);
			Helper.Dispose<AddNotifyJoinLobbyAcceptedOptionsInternal>(ref addNotifyJoinLobbyAcceptedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00022FC8 File Offset: 0x000211C8
		public ulong AddNotifyLobbyInviteAccepted(ref AddNotifyLobbyInviteAcceptedOptions options, object clientData, OnLobbyInviteAcceptedCallback notificationFn)
		{
			AddNotifyLobbyInviteAcceptedOptionsInternal addNotifyLobbyInviteAcceptedOptionsInternal = default(AddNotifyLobbyInviteAcceptedOptionsInternal);
			addNotifyLobbyInviteAcceptedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLobbyInviteAcceptedCallbackInternal onLobbyInviteAcceptedCallbackInternal = new OnLobbyInviteAcceptedCallbackInternal(LobbyInterface.OnLobbyInviteAcceptedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onLobbyInviteAcceptedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyInviteAccepted(base.InnerHandle, ref addNotifyLobbyInviteAcceptedOptionsInternal, zero, onLobbyInviteAcceptedCallbackInternal);
			Helper.Dispose<AddNotifyLobbyInviteAcceptedOptionsInternal>(ref addNotifyLobbyInviteAcceptedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00023034 File Offset: 0x00021234
		public ulong AddNotifyLobbyInviteReceived(ref AddNotifyLobbyInviteReceivedOptions options, object clientData, OnLobbyInviteReceivedCallback notificationFn)
		{
			AddNotifyLobbyInviteReceivedOptionsInternal addNotifyLobbyInviteReceivedOptionsInternal = default(AddNotifyLobbyInviteReceivedOptionsInternal);
			addNotifyLobbyInviteReceivedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLobbyInviteReceivedCallbackInternal onLobbyInviteReceivedCallbackInternal = new OnLobbyInviteReceivedCallbackInternal(LobbyInterface.OnLobbyInviteReceivedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onLobbyInviteReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyInviteReceived(base.InnerHandle, ref addNotifyLobbyInviteReceivedOptionsInternal, zero, onLobbyInviteReceivedCallbackInternal);
			Helper.Dispose<AddNotifyLobbyInviteReceivedOptionsInternal>(ref addNotifyLobbyInviteReceivedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x000230A0 File Offset: 0x000212A0
		public ulong AddNotifyLobbyInviteRejected(ref AddNotifyLobbyInviteRejectedOptions options, object clientData, OnLobbyInviteRejectedCallback notificationFn)
		{
			AddNotifyLobbyInviteRejectedOptionsInternal addNotifyLobbyInviteRejectedOptionsInternal = default(AddNotifyLobbyInviteRejectedOptionsInternal);
			addNotifyLobbyInviteRejectedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLobbyInviteRejectedCallbackInternal onLobbyInviteRejectedCallbackInternal = new OnLobbyInviteRejectedCallbackInternal(LobbyInterface.OnLobbyInviteRejectedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onLobbyInviteRejectedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyInviteRejected(base.InnerHandle, ref addNotifyLobbyInviteRejectedOptionsInternal, zero, onLobbyInviteRejectedCallbackInternal);
			Helper.Dispose<AddNotifyLobbyInviteRejectedOptionsInternal>(ref addNotifyLobbyInviteRejectedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0002310C File Offset: 0x0002130C
		public ulong AddNotifyLobbyMemberStatusReceived(ref AddNotifyLobbyMemberStatusReceivedOptions options, object clientData, OnLobbyMemberStatusReceivedCallback notificationFn)
		{
			AddNotifyLobbyMemberStatusReceivedOptionsInternal addNotifyLobbyMemberStatusReceivedOptionsInternal = default(AddNotifyLobbyMemberStatusReceivedOptionsInternal);
			addNotifyLobbyMemberStatusReceivedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLobbyMemberStatusReceivedCallbackInternal onLobbyMemberStatusReceivedCallbackInternal = new OnLobbyMemberStatusReceivedCallbackInternal(LobbyInterface.OnLobbyMemberStatusReceivedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onLobbyMemberStatusReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyMemberStatusReceived(base.InnerHandle, ref addNotifyLobbyMemberStatusReceivedOptionsInternal, zero, onLobbyMemberStatusReceivedCallbackInternal);
			Helper.Dispose<AddNotifyLobbyMemberStatusReceivedOptionsInternal>(ref addNotifyLobbyMemberStatusReceivedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00023178 File Offset: 0x00021378
		public ulong AddNotifyLobbyMemberUpdateReceived(ref AddNotifyLobbyMemberUpdateReceivedOptions options, object clientData, OnLobbyMemberUpdateReceivedCallback notificationFn)
		{
			AddNotifyLobbyMemberUpdateReceivedOptionsInternal addNotifyLobbyMemberUpdateReceivedOptionsInternal = default(AddNotifyLobbyMemberUpdateReceivedOptionsInternal);
			addNotifyLobbyMemberUpdateReceivedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLobbyMemberUpdateReceivedCallbackInternal onLobbyMemberUpdateReceivedCallbackInternal = new OnLobbyMemberUpdateReceivedCallbackInternal(LobbyInterface.OnLobbyMemberUpdateReceivedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onLobbyMemberUpdateReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyMemberUpdateReceived(base.InnerHandle, ref addNotifyLobbyMemberUpdateReceivedOptionsInternal, zero, onLobbyMemberUpdateReceivedCallbackInternal);
			Helper.Dispose<AddNotifyLobbyMemberUpdateReceivedOptionsInternal>(ref addNotifyLobbyMemberUpdateReceivedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000231E4 File Offset: 0x000213E4
		public ulong AddNotifyLobbyUpdateReceived(ref AddNotifyLobbyUpdateReceivedOptions options, object clientData, OnLobbyUpdateReceivedCallback notificationFn)
		{
			AddNotifyLobbyUpdateReceivedOptionsInternal addNotifyLobbyUpdateReceivedOptionsInternal = default(AddNotifyLobbyUpdateReceivedOptionsInternal);
			addNotifyLobbyUpdateReceivedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLobbyUpdateReceivedCallbackInternal onLobbyUpdateReceivedCallbackInternal = new OnLobbyUpdateReceivedCallbackInternal(LobbyInterface.OnLobbyUpdateReceivedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onLobbyUpdateReceivedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyLobbyUpdateReceived(base.InnerHandle, ref addNotifyLobbyUpdateReceivedOptionsInternal, zero, onLobbyUpdateReceivedCallbackInternal);
			Helper.Dispose<AddNotifyLobbyUpdateReceivedOptionsInternal>(ref addNotifyLobbyUpdateReceivedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00023250 File Offset: 0x00021450
		public ulong AddNotifyRTCRoomConnectionChanged(ref AddNotifyRTCRoomConnectionChangedOptions options, object clientData, OnRTCRoomConnectionChangedCallback notificationFn)
		{
			AddNotifyRTCRoomConnectionChangedOptionsInternal addNotifyRTCRoomConnectionChangedOptionsInternal = default(AddNotifyRTCRoomConnectionChangedOptionsInternal);
			addNotifyRTCRoomConnectionChangedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnRTCRoomConnectionChangedCallbackInternal onRTCRoomConnectionChangedCallbackInternal = new OnRTCRoomConnectionChangedCallbackInternal(LobbyInterface.OnRTCRoomConnectionChangedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onRTCRoomConnectionChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifyRTCRoomConnectionChanged(base.InnerHandle, ref addNotifyRTCRoomConnectionChangedOptionsInternal, zero, onRTCRoomConnectionChangedCallbackInternal);
			Helper.Dispose<AddNotifyRTCRoomConnectionChangedOptionsInternal>(ref addNotifyRTCRoomConnectionChangedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000232BC File Offset: 0x000214BC
		public ulong AddNotifySendLobbyNativeInviteRequested(ref AddNotifySendLobbyNativeInviteRequestedOptions options, object clientData, OnSendLobbyNativeInviteRequestedCallback notificationFn)
		{
			AddNotifySendLobbyNativeInviteRequestedOptionsInternal addNotifySendLobbyNativeInviteRequestedOptionsInternal = default(AddNotifySendLobbyNativeInviteRequestedOptionsInternal);
			addNotifySendLobbyNativeInviteRequestedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSendLobbyNativeInviteRequestedCallbackInternal onSendLobbyNativeInviteRequestedCallbackInternal = new OnSendLobbyNativeInviteRequestedCallbackInternal(LobbyInterface.OnSendLobbyNativeInviteRequestedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onSendLobbyNativeInviteRequestedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Lobby_AddNotifySendLobbyNativeInviteRequested(base.InnerHandle, ref addNotifySendLobbyNativeInviteRequestedOptionsInternal, zero, onSendLobbyNativeInviteRequestedCallbackInternal);
			Helper.Dispose<AddNotifySendLobbyNativeInviteRequestedOptionsInternal>(ref addNotifySendLobbyNativeInviteRequestedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00023328 File Offset: 0x00021528
		public Result CopyLobbyDetailsHandle(ref CopyLobbyDetailsHandleOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			CopyLobbyDetailsHandleOptionsInternal copyLobbyDetailsHandleOptionsInternal = default(CopyLobbyDetailsHandleOptionsInternal);
			copyLobbyDetailsHandleOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_CopyLobbyDetailsHandle(base.InnerHandle, ref copyLobbyDetailsHandleOptionsInternal, ref zero);
			Helper.Dispose<CopyLobbyDetailsHandleOptionsInternal>(ref copyLobbyDetailsHandleOptionsInternal);
			Helper.Get<LobbyDetails>(zero, out outLobbyDetailsHandle);
			return result;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00023374 File Offset: 0x00021574
		public Result CopyLobbyDetailsHandleByInviteId(ref CopyLobbyDetailsHandleByInviteIdOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			CopyLobbyDetailsHandleByInviteIdOptionsInternal copyLobbyDetailsHandleByInviteIdOptionsInternal = default(CopyLobbyDetailsHandleByInviteIdOptionsInternal);
			copyLobbyDetailsHandleByInviteIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_CopyLobbyDetailsHandleByInviteId(base.InnerHandle, ref copyLobbyDetailsHandleByInviteIdOptionsInternal, ref zero);
			Helper.Dispose<CopyLobbyDetailsHandleByInviteIdOptionsInternal>(ref copyLobbyDetailsHandleByInviteIdOptionsInternal);
			Helper.Get<LobbyDetails>(zero, out outLobbyDetailsHandle);
			return result;
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000233C0 File Offset: 0x000215C0
		public Result CopyLobbyDetailsHandleByUiEventId(ref CopyLobbyDetailsHandleByUiEventIdOptions options, out LobbyDetails outLobbyDetailsHandle)
		{
			CopyLobbyDetailsHandleByUiEventIdOptionsInternal copyLobbyDetailsHandleByUiEventIdOptionsInternal = default(CopyLobbyDetailsHandleByUiEventIdOptionsInternal);
			copyLobbyDetailsHandleByUiEventIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_CopyLobbyDetailsHandleByUiEventId(base.InnerHandle, ref copyLobbyDetailsHandleByUiEventIdOptionsInternal, ref zero);
			Helper.Dispose<CopyLobbyDetailsHandleByUiEventIdOptionsInternal>(ref copyLobbyDetailsHandleByUiEventIdOptionsInternal);
			Helper.Get<LobbyDetails>(zero, out outLobbyDetailsHandle);
			return result;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0002340C File Offset: 0x0002160C
		public void CreateLobby(ref CreateLobbyOptions options, object clientData, OnCreateLobbyCallback completionDelegate)
		{
			CreateLobbyOptionsInternal createLobbyOptionsInternal = default(CreateLobbyOptionsInternal);
			createLobbyOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnCreateLobbyCallbackInternal onCreateLobbyCallbackInternal = new OnCreateLobbyCallbackInternal(LobbyInterface.OnCreateLobbyCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onCreateLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_CreateLobby(base.InnerHandle, ref createLobbyOptionsInternal, zero, onCreateLobbyCallbackInternal);
			Helper.Dispose<CreateLobbyOptionsInternal>(ref createLobbyOptionsInternal);
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x00023468 File Offset: 0x00021668
		public Result CreateLobbySearch(ref CreateLobbySearchOptions options, out LobbySearch outLobbySearchHandle)
		{
			CreateLobbySearchOptionsInternal createLobbySearchOptionsInternal = default(CreateLobbySearchOptionsInternal);
			createLobbySearchOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_CreateLobbySearch(base.InnerHandle, ref createLobbySearchOptionsInternal, ref zero);
			Helper.Dispose<CreateLobbySearchOptionsInternal>(ref createLobbySearchOptionsInternal);
			Helper.Get<LobbySearch>(zero, out outLobbySearchHandle);
			return result;
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000234B4 File Offset: 0x000216B4
		public void DestroyLobby(ref DestroyLobbyOptions options, object clientData, OnDestroyLobbyCallback completionDelegate)
		{
			DestroyLobbyOptionsInternal destroyLobbyOptionsInternal = default(DestroyLobbyOptionsInternal);
			destroyLobbyOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnDestroyLobbyCallbackInternal onDestroyLobbyCallbackInternal = new OnDestroyLobbyCallbackInternal(LobbyInterface.OnDestroyLobbyCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onDestroyLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_DestroyLobby(base.InnerHandle, ref destroyLobbyOptionsInternal, zero, onDestroyLobbyCallbackInternal);
			Helper.Dispose<DestroyLobbyOptionsInternal>(ref destroyLobbyOptionsInternal);
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x00023510 File Offset: 0x00021710
		public uint GetInviteCount(ref GetInviteCountOptions options)
		{
			GetInviteCountOptionsInternal getInviteCountOptionsInternal = default(GetInviteCountOptionsInternal);
			getInviteCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_Lobby_GetInviteCount(base.InnerHandle, ref getInviteCountOptionsInternal);
			Helper.Dispose<GetInviteCountOptionsInternal>(ref getInviteCountOptionsInternal);
			return result;
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0002354C File Offset: 0x0002174C
		public Result GetInviteIdByIndex(ref GetInviteIdByIndexOptions options, out Utf8String outBuffer)
		{
			GetInviteIdByIndexOptionsInternal getInviteIdByIndexOptionsInternal = default(GetInviteIdByIndexOptionsInternal);
			getInviteIdByIndexOptionsInternal.Set(ref options);
			int size = 65;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Lobby_GetInviteIdByIndex(base.InnerHandle, ref getInviteIdByIndexOptionsInternal, intPtr, ref size);
			Helper.Dispose<GetInviteIdByIndexOptionsInternal>(ref getInviteIdByIndexOptionsInternal);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x000235A8 File Offset: 0x000217A8
		public Result GetRTCRoomName(ref GetRTCRoomNameOptions options, out Utf8String outBuffer)
		{
			GetRTCRoomNameOptionsInternal getRTCRoomNameOptionsInternal = default(GetRTCRoomNameOptionsInternal);
			getRTCRoomNameOptionsInternal.Set(ref options);
			uint size = 256U;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Lobby_GetRTCRoomName(base.InnerHandle, ref getRTCRoomNameOptionsInternal, intPtr, ref size);
			Helper.Dispose<GetRTCRoomNameOptionsInternal>(ref getRTCRoomNameOptionsInternal);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00023604 File Offset: 0x00021804
		public void HardMuteMember(ref HardMuteMemberOptions options, object clientData, OnHardMuteMemberCallback completionDelegate)
		{
			HardMuteMemberOptionsInternal hardMuteMemberOptionsInternal = default(HardMuteMemberOptionsInternal);
			hardMuteMemberOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnHardMuteMemberCallbackInternal onHardMuteMemberCallbackInternal = new OnHardMuteMemberCallbackInternal(LobbyInterface.OnHardMuteMemberCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onHardMuteMemberCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_HardMuteMember(base.InnerHandle, ref hardMuteMemberOptionsInternal, zero, onHardMuteMemberCallbackInternal);
			Helper.Dispose<HardMuteMemberOptionsInternal>(ref hardMuteMemberOptionsInternal);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00023660 File Offset: 0x00021860
		public Result IsRTCRoomConnected(ref IsRTCRoomConnectedOptions options, out bool bOutIsConnected)
		{
			IsRTCRoomConnectedOptionsInternal isRTCRoomConnectedOptionsInternal = default(IsRTCRoomConnectedOptionsInternal);
			isRTCRoomConnectedOptionsInternal.Set(ref options);
			int from = 0;
			Result result = Bindings.EOS_Lobby_IsRTCRoomConnected(base.InnerHandle, ref isRTCRoomConnectedOptionsInternal, ref from);
			Helper.Dispose<IsRTCRoomConnectedOptionsInternal>(ref isRTCRoomConnectedOptionsInternal);
			Helper.Get(from, out bOutIsConnected);
			return result;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x000236A8 File Offset: 0x000218A8
		public void JoinLobby(ref JoinLobbyOptions options, object clientData, OnJoinLobbyCallback completionDelegate)
		{
			JoinLobbyOptionsInternal joinLobbyOptionsInternal = default(JoinLobbyOptionsInternal);
			joinLobbyOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnJoinLobbyCallbackInternal onJoinLobbyCallbackInternal = new OnJoinLobbyCallbackInternal(LobbyInterface.OnJoinLobbyCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onJoinLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_JoinLobby(base.InnerHandle, ref joinLobbyOptionsInternal, zero, onJoinLobbyCallbackInternal);
			Helper.Dispose<JoinLobbyOptionsInternal>(ref joinLobbyOptionsInternal);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00023704 File Offset: 0x00021904
		public void JoinLobbyById(ref JoinLobbyByIdOptions options, object clientData, OnJoinLobbyByIdCallback completionDelegate)
		{
			JoinLobbyByIdOptionsInternal joinLobbyByIdOptionsInternal = default(JoinLobbyByIdOptionsInternal);
			joinLobbyByIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnJoinLobbyByIdCallbackInternal onJoinLobbyByIdCallbackInternal = new OnJoinLobbyByIdCallbackInternal(LobbyInterface.OnJoinLobbyByIdCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onJoinLobbyByIdCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_JoinLobbyById(base.InnerHandle, ref joinLobbyByIdOptionsInternal, zero, onJoinLobbyByIdCallbackInternal);
			Helper.Dispose<JoinLobbyByIdOptionsInternal>(ref joinLobbyByIdOptionsInternal);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00023760 File Offset: 0x00021960
		public void KickMember(ref KickMemberOptions options, object clientData, OnKickMemberCallback completionDelegate)
		{
			KickMemberOptionsInternal kickMemberOptionsInternal = default(KickMemberOptionsInternal);
			kickMemberOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnKickMemberCallbackInternal onKickMemberCallbackInternal = new OnKickMemberCallbackInternal(LobbyInterface.OnKickMemberCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onKickMemberCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_KickMember(base.InnerHandle, ref kickMemberOptionsInternal, zero, onKickMemberCallbackInternal);
			Helper.Dispose<KickMemberOptionsInternal>(ref kickMemberOptionsInternal);
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x000237BC File Offset: 0x000219BC
		public void LeaveLobby(ref LeaveLobbyOptions options, object clientData, OnLeaveLobbyCallback completionDelegate)
		{
			LeaveLobbyOptionsInternal leaveLobbyOptionsInternal = default(LeaveLobbyOptionsInternal);
			leaveLobbyOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLeaveLobbyCallbackInternal onLeaveLobbyCallbackInternal = new OnLeaveLobbyCallbackInternal(LobbyInterface.OnLeaveLobbyCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onLeaveLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_LeaveLobby(base.InnerHandle, ref leaveLobbyOptionsInternal, zero, onLeaveLobbyCallbackInternal);
			Helper.Dispose<LeaveLobbyOptionsInternal>(ref leaveLobbyOptionsInternal);
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00023818 File Offset: 0x00021A18
		public void PromoteMember(ref PromoteMemberOptions options, object clientData, OnPromoteMemberCallback completionDelegate)
		{
			PromoteMemberOptionsInternal promoteMemberOptionsInternal = default(PromoteMemberOptionsInternal);
			promoteMemberOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnPromoteMemberCallbackInternal onPromoteMemberCallbackInternal = new OnPromoteMemberCallbackInternal(LobbyInterface.OnPromoteMemberCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onPromoteMemberCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_PromoteMember(base.InnerHandle, ref promoteMemberOptionsInternal, zero, onPromoteMemberCallbackInternal);
			Helper.Dispose<PromoteMemberOptionsInternal>(ref promoteMemberOptionsInternal);
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00023874 File Offset: 0x00021A74
		public void QueryInvites(ref QueryInvitesOptions options, object clientData, OnQueryInvitesCallback completionDelegate)
		{
			QueryInvitesOptionsInternal queryInvitesOptionsInternal = default(QueryInvitesOptionsInternal);
			queryInvitesOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryInvitesCallbackInternal onQueryInvitesCallbackInternal = new OnQueryInvitesCallbackInternal(LobbyInterface.OnQueryInvitesCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryInvitesCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_QueryInvites(base.InnerHandle, ref queryInvitesOptionsInternal, zero, onQueryInvitesCallbackInternal);
			Helper.Dispose<QueryInvitesOptionsInternal>(ref queryInvitesOptionsInternal);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x000238D0 File Offset: 0x00021AD0
		public void RejectInvite(ref RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
		{
			RejectInviteOptionsInternal rejectInviteOptionsInternal = default(RejectInviteOptionsInternal);
			rejectInviteOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = new OnRejectInviteCallbackInternal(LobbyInterface.OnRejectInviteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onRejectInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_RejectInvite(base.InnerHandle, ref rejectInviteOptionsInternal, zero, onRejectInviteCallbackInternal);
			Helper.Dispose<RejectInviteOptionsInternal>(ref rejectInviteOptionsInternal);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0002392A File Offset: 0x00021B2A
		public void RemoveNotifyJoinLobbyAccepted(ulong inId)
		{
			Bindings.EOS_Lobby_RemoveNotifyJoinLobbyAccepted(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00023941 File Offset: 0x00021B41
		public void RemoveNotifyLobbyInviteAccepted(ulong inId)
		{
			Bindings.EOS_Lobby_RemoveNotifyLobbyInviteAccepted(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00023958 File Offset: 0x00021B58
		public void RemoveNotifyLobbyInviteReceived(ulong inId)
		{
			Bindings.EOS_Lobby_RemoveNotifyLobbyInviteReceived(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0002396F File Offset: 0x00021B6F
		public void RemoveNotifyLobbyInviteRejected(ulong inId)
		{
			Bindings.EOS_Lobby_RemoveNotifyLobbyInviteRejected(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x00023986 File Offset: 0x00021B86
		public void RemoveNotifyLobbyMemberStatusReceived(ulong inId)
		{
			Bindings.EOS_Lobby_RemoveNotifyLobbyMemberStatusReceived(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0002399D File Offset: 0x00021B9D
		public void RemoveNotifyLobbyMemberUpdateReceived(ulong inId)
		{
			Bindings.EOS_Lobby_RemoveNotifyLobbyMemberUpdateReceived(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x000239B4 File Offset: 0x00021BB4
		public void RemoveNotifyLobbyUpdateReceived(ulong inId)
		{
			Bindings.EOS_Lobby_RemoveNotifyLobbyUpdateReceived(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000239CB File Offset: 0x00021BCB
		public void RemoveNotifyRTCRoomConnectionChanged(ulong inId)
		{
			Bindings.EOS_Lobby_RemoveNotifyRTCRoomConnectionChanged(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000239E2 File Offset: 0x00021BE2
		public void RemoveNotifySendLobbyNativeInviteRequested(ulong inId)
		{
			Bindings.EOS_Lobby_RemoveNotifySendLobbyNativeInviteRequested(base.InnerHandle, inId);
			Helper.RemoveCallbackByNotificationId(inId);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000239FC File Offset: 0x00021BFC
		public void SendInvite(ref SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
		{
			SendInviteOptionsInternal sendInviteOptionsInternal = default(SendInviteOptionsInternal);
			sendInviteOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSendInviteCallbackInternal onSendInviteCallbackInternal = new OnSendInviteCallbackInternal(LobbyInterface.OnSendInviteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onSendInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_SendInvite(base.InnerHandle, ref sendInviteOptionsInternal, zero, onSendInviteCallbackInternal);
			Helper.Dispose<SendInviteOptionsInternal>(ref sendInviteOptionsInternal);
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00023A58 File Offset: 0x00021C58
		public void UpdateLobby(ref UpdateLobbyOptions options, object clientData, OnUpdateLobbyCallback completionDelegate)
		{
			UpdateLobbyOptionsInternal updateLobbyOptionsInternal = default(UpdateLobbyOptionsInternal);
			updateLobbyOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUpdateLobbyCallbackInternal onUpdateLobbyCallbackInternal = new OnUpdateLobbyCallbackInternal(LobbyInterface.OnUpdateLobbyCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUpdateLobbyCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Lobby_UpdateLobby(base.InnerHandle, ref updateLobbyOptionsInternal, zero, onUpdateLobbyCallbackInternal);
			Helper.Dispose<UpdateLobbyOptionsInternal>(ref updateLobbyOptionsInternal);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00023AB4 File Offset: 0x00021CB4
		public Result UpdateLobbyModification(ref UpdateLobbyModificationOptions options, out LobbyModification outLobbyModificationHandle)
		{
			UpdateLobbyModificationOptionsInternal updateLobbyModificationOptionsInternal = default(UpdateLobbyModificationOptionsInternal);
			updateLobbyModificationOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_Lobby_UpdateLobbyModification(base.InnerHandle, ref updateLobbyModificationOptionsInternal, ref zero);
			Helper.Dispose<UpdateLobbyModificationOptionsInternal>(ref updateLobbyModificationOptionsInternal);
			Helper.Get<LobbyModification>(zero, out outLobbyModificationHandle);
			return result;
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00023B00 File Offset: 0x00021D00
		[MonoPInvokeCallback(typeof(OnCreateLobbyCallbackInternal))]
		internal static void OnCreateLobbyCallbackInternalImplementation(ref CreateLobbyCallbackInfoInternal data)
		{
			OnCreateLobbyCallback onCreateLobbyCallback;
			CreateLobbyCallbackInfo createLobbyCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<CreateLobbyCallbackInfoInternal, OnCreateLobbyCallback, CreateLobbyCallbackInfo>(ref data, out onCreateLobbyCallback, out createLobbyCallbackInfo);
			if (flag)
			{
				onCreateLobbyCallback(ref createLobbyCallbackInfo);
			}
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00023B28 File Offset: 0x00021D28
		[MonoPInvokeCallback(typeof(OnDestroyLobbyCallbackInternal))]
		internal static void OnDestroyLobbyCallbackInternalImplementation(ref DestroyLobbyCallbackInfoInternal data)
		{
			OnDestroyLobbyCallback onDestroyLobbyCallback;
			DestroyLobbyCallbackInfo destroyLobbyCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<DestroyLobbyCallbackInfoInternal, OnDestroyLobbyCallback, DestroyLobbyCallbackInfo>(ref data, out onDestroyLobbyCallback, out destroyLobbyCallbackInfo);
			if (flag)
			{
				onDestroyLobbyCallback(ref destroyLobbyCallbackInfo);
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00023B50 File Offset: 0x00021D50
		[MonoPInvokeCallback(typeof(OnHardMuteMemberCallbackInternal))]
		internal static void OnHardMuteMemberCallbackInternalImplementation(ref HardMuteMemberCallbackInfoInternal data)
		{
			OnHardMuteMemberCallback onHardMuteMemberCallback;
			HardMuteMemberCallbackInfo hardMuteMemberCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<HardMuteMemberCallbackInfoInternal, OnHardMuteMemberCallback, HardMuteMemberCallbackInfo>(ref data, out onHardMuteMemberCallback, out hardMuteMemberCallbackInfo);
			if (flag)
			{
				onHardMuteMemberCallback(ref hardMuteMemberCallbackInfo);
			}
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00023B78 File Offset: 0x00021D78
		[MonoPInvokeCallback(typeof(OnJoinLobbyAcceptedCallbackInternal))]
		internal static void OnJoinLobbyAcceptedCallbackInternalImplementation(ref JoinLobbyAcceptedCallbackInfoInternal data)
		{
			OnJoinLobbyAcceptedCallback onJoinLobbyAcceptedCallback;
			JoinLobbyAcceptedCallbackInfo joinLobbyAcceptedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<JoinLobbyAcceptedCallbackInfoInternal, OnJoinLobbyAcceptedCallback, JoinLobbyAcceptedCallbackInfo>(ref data, out onJoinLobbyAcceptedCallback, out joinLobbyAcceptedCallbackInfo);
			if (flag)
			{
				onJoinLobbyAcceptedCallback(ref joinLobbyAcceptedCallbackInfo);
			}
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x00023BA0 File Offset: 0x00021DA0
		[MonoPInvokeCallback(typeof(OnJoinLobbyByIdCallbackInternal))]
		internal static void OnJoinLobbyByIdCallbackInternalImplementation(ref JoinLobbyByIdCallbackInfoInternal data)
		{
			OnJoinLobbyByIdCallback onJoinLobbyByIdCallback;
			JoinLobbyByIdCallbackInfo joinLobbyByIdCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<JoinLobbyByIdCallbackInfoInternal, OnJoinLobbyByIdCallback, JoinLobbyByIdCallbackInfo>(ref data, out onJoinLobbyByIdCallback, out joinLobbyByIdCallbackInfo);
			if (flag)
			{
				onJoinLobbyByIdCallback(ref joinLobbyByIdCallbackInfo);
			}
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x00023BC8 File Offset: 0x00021DC8
		[MonoPInvokeCallback(typeof(OnJoinLobbyCallbackInternal))]
		internal static void OnJoinLobbyCallbackInternalImplementation(ref JoinLobbyCallbackInfoInternal data)
		{
			OnJoinLobbyCallback onJoinLobbyCallback;
			JoinLobbyCallbackInfo joinLobbyCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<JoinLobbyCallbackInfoInternal, OnJoinLobbyCallback, JoinLobbyCallbackInfo>(ref data, out onJoinLobbyCallback, out joinLobbyCallbackInfo);
			if (flag)
			{
				onJoinLobbyCallback(ref joinLobbyCallbackInfo);
			}
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x00023BF0 File Offset: 0x00021DF0
		[MonoPInvokeCallback(typeof(OnKickMemberCallbackInternal))]
		internal static void OnKickMemberCallbackInternalImplementation(ref KickMemberCallbackInfoInternal data)
		{
			OnKickMemberCallback onKickMemberCallback;
			KickMemberCallbackInfo kickMemberCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<KickMemberCallbackInfoInternal, OnKickMemberCallback, KickMemberCallbackInfo>(ref data, out onKickMemberCallback, out kickMemberCallbackInfo);
			if (flag)
			{
				onKickMemberCallback(ref kickMemberCallbackInfo);
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x00023C18 File Offset: 0x00021E18
		[MonoPInvokeCallback(typeof(OnLeaveLobbyCallbackInternal))]
		internal static void OnLeaveLobbyCallbackInternalImplementation(ref LeaveLobbyCallbackInfoInternal data)
		{
			OnLeaveLobbyCallback onLeaveLobbyCallback;
			LeaveLobbyCallbackInfo leaveLobbyCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LeaveLobbyCallbackInfoInternal, OnLeaveLobbyCallback, LeaveLobbyCallbackInfo>(ref data, out onLeaveLobbyCallback, out leaveLobbyCallbackInfo);
			if (flag)
			{
				onLeaveLobbyCallback(ref leaveLobbyCallbackInfo);
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x00023C40 File Offset: 0x00021E40
		[MonoPInvokeCallback(typeof(OnLobbyInviteAcceptedCallbackInternal))]
		internal static void OnLobbyInviteAcceptedCallbackInternalImplementation(ref LobbyInviteAcceptedCallbackInfoInternal data)
		{
			OnLobbyInviteAcceptedCallback onLobbyInviteAcceptedCallback;
			LobbyInviteAcceptedCallbackInfo lobbyInviteAcceptedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LobbyInviteAcceptedCallbackInfoInternal, OnLobbyInviteAcceptedCallback, LobbyInviteAcceptedCallbackInfo>(ref data, out onLobbyInviteAcceptedCallback, out lobbyInviteAcceptedCallbackInfo);
			if (flag)
			{
				onLobbyInviteAcceptedCallback(ref lobbyInviteAcceptedCallbackInfo);
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x00023C68 File Offset: 0x00021E68
		[MonoPInvokeCallback(typeof(OnLobbyInviteReceivedCallbackInternal))]
		internal static void OnLobbyInviteReceivedCallbackInternalImplementation(ref LobbyInviteReceivedCallbackInfoInternal data)
		{
			OnLobbyInviteReceivedCallback onLobbyInviteReceivedCallback;
			LobbyInviteReceivedCallbackInfo lobbyInviteReceivedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LobbyInviteReceivedCallbackInfoInternal, OnLobbyInviteReceivedCallback, LobbyInviteReceivedCallbackInfo>(ref data, out onLobbyInviteReceivedCallback, out lobbyInviteReceivedCallbackInfo);
			if (flag)
			{
				onLobbyInviteReceivedCallback(ref lobbyInviteReceivedCallbackInfo);
			}
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x00023C90 File Offset: 0x00021E90
		[MonoPInvokeCallback(typeof(OnLobbyInviteRejectedCallbackInternal))]
		internal static void OnLobbyInviteRejectedCallbackInternalImplementation(ref LobbyInviteRejectedCallbackInfoInternal data)
		{
			OnLobbyInviteRejectedCallback onLobbyInviteRejectedCallback;
			LobbyInviteRejectedCallbackInfo lobbyInviteRejectedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LobbyInviteRejectedCallbackInfoInternal, OnLobbyInviteRejectedCallback, LobbyInviteRejectedCallbackInfo>(ref data, out onLobbyInviteRejectedCallback, out lobbyInviteRejectedCallbackInfo);
			if (flag)
			{
				onLobbyInviteRejectedCallback(ref lobbyInviteRejectedCallbackInfo);
			}
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x00023CB8 File Offset: 0x00021EB8
		[MonoPInvokeCallback(typeof(OnLobbyMemberStatusReceivedCallbackInternal))]
		internal static void OnLobbyMemberStatusReceivedCallbackInternalImplementation(ref LobbyMemberStatusReceivedCallbackInfoInternal data)
		{
			OnLobbyMemberStatusReceivedCallback onLobbyMemberStatusReceivedCallback;
			LobbyMemberStatusReceivedCallbackInfo lobbyMemberStatusReceivedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LobbyMemberStatusReceivedCallbackInfoInternal, OnLobbyMemberStatusReceivedCallback, LobbyMemberStatusReceivedCallbackInfo>(ref data, out onLobbyMemberStatusReceivedCallback, out lobbyMemberStatusReceivedCallbackInfo);
			if (flag)
			{
				onLobbyMemberStatusReceivedCallback(ref lobbyMemberStatusReceivedCallbackInfo);
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x00023CE0 File Offset: 0x00021EE0
		[MonoPInvokeCallback(typeof(OnLobbyMemberUpdateReceivedCallbackInternal))]
		internal static void OnLobbyMemberUpdateReceivedCallbackInternalImplementation(ref LobbyMemberUpdateReceivedCallbackInfoInternal data)
		{
			OnLobbyMemberUpdateReceivedCallback onLobbyMemberUpdateReceivedCallback;
			LobbyMemberUpdateReceivedCallbackInfo lobbyMemberUpdateReceivedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LobbyMemberUpdateReceivedCallbackInfoInternal, OnLobbyMemberUpdateReceivedCallback, LobbyMemberUpdateReceivedCallbackInfo>(ref data, out onLobbyMemberUpdateReceivedCallback, out lobbyMemberUpdateReceivedCallbackInfo);
			if (flag)
			{
				onLobbyMemberUpdateReceivedCallback(ref lobbyMemberUpdateReceivedCallbackInfo);
			}
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x00023D08 File Offset: 0x00021F08
		[MonoPInvokeCallback(typeof(OnLobbyUpdateReceivedCallbackInternal))]
		internal static void OnLobbyUpdateReceivedCallbackInternalImplementation(ref LobbyUpdateReceivedCallbackInfoInternal data)
		{
			OnLobbyUpdateReceivedCallback onLobbyUpdateReceivedCallback;
			LobbyUpdateReceivedCallbackInfo lobbyUpdateReceivedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LobbyUpdateReceivedCallbackInfoInternal, OnLobbyUpdateReceivedCallback, LobbyUpdateReceivedCallbackInfo>(ref data, out onLobbyUpdateReceivedCallback, out lobbyUpdateReceivedCallbackInfo);
			if (flag)
			{
				onLobbyUpdateReceivedCallback(ref lobbyUpdateReceivedCallbackInfo);
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x00023D30 File Offset: 0x00021F30
		[MonoPInvokeCallback(typeof(OnPromoteMemberCallbackInternal))]
		internal static void OnPromoteMemberCallbackInternalImplementation(ref PromoteMemberCallbackInfoInternal data)
		{
			OnPromoteMemberCallback onPromoteMemberCallback;
			PromoteMemberCallbackInfo promoteMemberCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<PromoteMemberCallbackInfoInternal, OnPromoteMemberCallback, PromoteMemberCallbackInfo>(ref data, out onPromoteMemberCallback, out promoteMemberCallbackInfo);
			if (flag)
			{
				onPromoteMemberCallback(ref promoteMemberCallbackInfo);
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x00023D58 File Offset: 0x00021F58
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

		// Token: 0x060017AD RID: 6061 RVA: 0x00023D80 File Offset: 0x00021F80
		[MonoPInvokeCallback(typeof(OnRTCRoomConnectionChangedCallbackInternal))]
		internal static void OnRTCRoomConnectionChangedCallbackInternalImplementation(ref RTCRoomConnectionChangedCallbackInfoInternal data)
		{
			OnRTCRoomConnectionChangedCallback onRTCRoomConnectionChangedCallback;
			RTCRoomConnectionChangedCallbackInfo rtcroomConnectionChangedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<RTCRoomConnectionChangedCallbackInfoInternal, OnRTCRoomConnectionChangedCallback, RTCRoomConnectionChangedCallbackInfo>(ref data, out onRTCRoomConnectionChangedCallback, out rtcroomConnectionChangedCallbackInfo);
			if (flag)
			{
				onRTCRoomConnectionChangedCallback(ref rtcroomConnectionChangedCallbackInfo);
			}
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x00023DA8 File Offset: 0x00021FA8
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

		// Token: 0x060017AF RID: 6063 RVA: 0x00023DD0 File Offset: 0x00021FD0
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

		// Token: 0x060017B0 RID: 6064 RVA: 0x00023DF8 File Offset: 0x00021FF8
		[MonoPInvokeCallback(typeof(OnSendLobbyNativeInviteRequestedCallbackInternal))]
		internal static void OnSendLobbyNativeInviteRequestedCallbackInternalImplementation(ref SendLobbyNativeInviteRequestedCallbackInfoInternal data)
		{
			OnSendLobbyNativeInviteRequestedCallback onSendLobbyNativeInviteRequestedCallback;
			SendLobbyNativeInviteRequestedCallbackInfo sendLobbyNativeInviteRequestedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SendLobbyNativeInviteRequestedCallbackInfoInternal, OnSendLobbyNativeInviteRequestedCallback, SendLobbyNativeInviteRequestedCallbackInfo>(ref data, out onSendLobbyNativeInviteRequestedCallback, out sendLobbyNativeInviteRequestedCallbackInfo);
			if (flag)
			{
				onSendLobbyNativeInviteRequestedCallback(ref sendLobbyNativeInviteRequestedCallbackInfo);
			}
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00023E20 File Offset: 0x00022020
		[MonoPInvokeCallback(typeof(OnUpdateLobbyCallbackInternal))]
		internal static void OnUpdateLobbyCallbackInternalImplementation(ref UpdateLobbyCallbackInfoInternal data)
		{
			OnUpdateLobbyCallback onUpdateLobbyCallback;
			UpdateLobbyCallbackInfo updateLobbyCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UpdateLobbyCallbackInfoInternal, OnUpdateLobbyCallback, UpdateLobbyCallbackInfo>(ref data, out onUpdateLobbyCallback, out updateLobbyCallbackInfo);
			if (flag)
			{
				onUpdateLobbyCallback(ref updateLobbyCallbackInfo);
			}
		}

		// Token: 0x04000AAB RID: 2731
		public const int AddnotifyjoinlobbyacceptedApiLatest = 1;

		// Token: 0x04000AAC RID: 2732
		public const int AddnotifylobbyinviteacceptedApiLatest = 1;

		// Token: 0x04000AAD RID: 2733
		public const int AddnotifylobbyinvitereceivedApiLatest = 1;

		// Token: 0x04000AAE RID: 2734
		public const int AddnotifylobbyinviterejectedApiLatest = 1;

		// Token: 0x04000AAF RID: 2735
		public const int AddnotifylobbymemberstatusreceivedApiLatest = 1;

		// Token: 0x04000AB0 RID: 2736
		public const int AddnotifylobbymemberupdatereceivedApiLatest = 1;

		// Token: 0x04000AB1 RID: 2737
		public const int AddnotifylobbyupdatereceivedApiLatest = 1;

		// Token: 0x04000AB2 RID: 2738
		public const int AddnotifyrtcroomconnectionchangedApiLatest = 2;

		// Token: 0x04000AB3 RID: 2739
		public const int AddnotifysendlobbynativeinviterequestedApiLatest = 1;

		// Token: 0x04000AB4 RID: 2740
		public const int AttributeApiLatest = 1;

		// Token: 0x04000AB5 RID: 2741
		public const int AttributedataApiLatest = 1;

		// Token: 0x04000AB6 RID: 2742
		public const int CopylobbydetailshandleApiLatest = 1;

		// Token: 0x04000AB7 RID: 2743
		public const int CopylobbydetailshandlebyinviteidApiLatest = 1;

		// Token: 0x04000AB8 RID: 2744
		public const int CopylobbydetailshandlebyuieventidApiLatest = 1;

		// Token: 0x04000AB9 RID: 2745
		public const int CreatelobbyApiLatest = 8;

		// Token: 0x04000ABA RID: 2746
		public const int CreatelobbysearchApiLatest = 1;

		// Token: 0x04000ABB RID: 2747
		public const int DestroylobbyApiLatest = 1;

		// Token: 0x04000ABC RID: 2748
		public const int GetinvitecountApiLatest = 1;

		// Token: 0x04000ABD RID: 2749
		public const int GetinviteidbyindexApiLatest = 1;

		// Token: 0x04000ABE RID: 2750
		public const int GetrtcroomnameApiLatest = 1;

		// Token: 0x04000ABF RID: 2751
		public const int HardmutememberApiLatest = 1;

		// Token: 0x04000AC0 RID: 2752
		public const int InviteidMaxLength = 64;

		// Token: 0x04000AC1 RID: 2753
		public const int IsrtcroomconnectedApiLatest = 1;

		// Token: 0x04000AC2 RID: 2754
		public const int JoinlobbyApiLatest = 3;

		// Token: 0x04000AC3 RID: 2755
		public const int JoinlobbybyidApiLatest = 1;

		// Token: 0x04000AC4 RID: 2756
		public const int KickmemberApiLatest = 1;

		// Token: 0x04000AC5 RID: 2757
		public const int LeavelobbyApiLatest = 1;

		// Token: 0x04000AC6 RID: 2758
		public const int LocalrtcoptionsApiLatest = 1;

		// Token: 0x04000AC7 RID: 2759
		public const int MaxLobbies = 16;

		// Token: 0x04000AC8 RID: 2760
		public const int MaxLobbyMembers = 64;

		// Token: 0x04000AC9 RID: 2761
		public const int MaxLobbyidoverrideLength = 60;

		// Token: 0x04000ACA RID: 2762
		public const int MaxSearchResults = 200;

		// Token: 0x04000ACB RID: 2763
		public const int MinLobbyidoverrideLength = 4;

		// Token: 0x04000ACC RID: 2764
		public const int PromotememberApiLatest = 1;

		// Token: 0x04000ACD RID: 2765
		public const int QueryinvitesApiLatest = 1;

		// Token: 0x04000ACE RID: 2766
		public const int RejectinviteApiLatest = 1;

		// Token: 0x04000ACF RID: 2767
		public static readonly Utf8String SearchBucketId = "bucket";

		// Token: 0x04000AD0 RID: 2768
		public static readonly Utf8String SearchMincurrentmembers = "mincurrentmembers";

		// Token: 0x04000AD1 RID: 2769
		public static readonly Utf8String SearchMinslotsavailable = "minslotsavailable";

		// Token: 0x04000AD2 RID: 2770
		public const int SendinviteApiLatest = 1;

		// Token: 0x04000AD3 RID: 2771
		public const int UpdatelobbyApiLatest = 1;

		// Token: 0x04000AD4 RID: 2772
		public const int UpdatelobbymodificationApiLatest = 1;
	}
}
