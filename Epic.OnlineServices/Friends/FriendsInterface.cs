using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000464 RID: 1124
	public sealed class FriendsInterface : Handle
	{
		// Token: 0x06001CC3 RID: 7363 RVA: 0x0002A84C File Offset: 0x00028A4C
		public FriendsInterface()
		{
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0002A856 File Offset: 0x00028A56
		public FriendsInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0002A864 File Offset: 0x00028A64
		public void AcceptInvite(ref AcceptInviteOptions options, object clientData, OnAcceptInviteCallback completionDelegate)
		{
			AcceptInviteOptionsInternal acceptInviteOptionsInternal = default(AcceptInviteOptionsInternal);
			acceptInviteOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnAcceptInviteCallbackInternal onAcceptInviteCallbackInternal = new OnAcceptInviteCallbackInternal(FriendsInterface.OnAcceptInviteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onAcceptInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Friends_AcceptInvite(base.InnerHandle, ref acceptInviteOptionsInternal, zero, onAcceptInviteCallbackInternal);
			Helper.Dispose<AcceptInviteOptionsInternal>(ref acceptInviteOptionsInternal);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0002A8C0 File Offset: 0x00028AC0
		public ulong AddNotifyFriendsUpdate(ref AddNotifyFriendsUpdateOptions options, object clientData, OnFriendsUpdateCallback friendsUpdateHandler)
		{
			AddNotifyFriendsUpdateOptionsInternal addNotifyFriendsUpdateOptionsInternal = default(AddNotifyFriendsUpdateOptionsInternal);
			addNotifyFriendsUpdateOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnFriendsUpdateCallbackInternal onFriendsUpdateCallbackInternal = new OnFriendsUpdateCallbackInternal(FriendsInterface.OnFriendsUpdateCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, friendsUpdateHandler, onFriendsUpdateCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_Friends_AddNotifyFriendsUpdate(base.InnerHandle, ref addNotifyFriendsUpdateOptionsInternal, zero, onFriendsUpdateCallbackInternal);
			Helper.Dispose<AddNotifyFriendsUpdateOptionsInternal>(ref addNotifyFriendsUpdateOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x0002A92C File Offset: 0x00028B2C
		public EpicAccountId GetFriendAtIndex(ref GetFriendAtIndexOptions options)
		{
			GetFriendAtIndexOptionsInternal getFriendAtIndexOptionsInternal = default(GetFriendAtIndexOptionsInternal);
			getFriendAtIndexOptionsInternal.Set(ref options);
			IntPtr from = Bindings.EOS_Friends_GetFriendAtIndex(base.InnerHandle, ref getFriendAtIndexOptionsInternal);
			Helper.Dispose<GetFriendAtIndexOptionsInternal>(ref getFriendAtIndexOptionsInternal);
			EpicAccountId result;
			Helper.Get<EpicAccountId>(from, out result);
			return result;
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x0002A970 File Offset: 0x00028B70
		public int GetFriendsCount(ref GetFriendsCountOptions options)
		{
			GetFriendsCountOptionsInternal getFriendsCountOptionsInternal = default(GetFriendsCountOptionsInternal);
			getFriendsCountOptionsInternal.Set(ref options);
			int result = Bindings.EOS_Friends_GetFriendsCount(base.InnerHandle, ref getFriendsCountOptionsInternal);
			Helper.Dispose<GetFriendsCountOptionsInternal>(ref getFriendsCountOptionsInternal);
			return result;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0002A9AC File Offset: 0x00028BAC
		public FriendsStatus GetStatus(ref GetStatusOptions options)
		{
			GetStatusOptionsInternal getStatusOptionsInternal = default(GetStatusOptionsInternal);
			getStatusOptionsInternal.Set(ref options);
			FriendsStatus result = Bindings.EOS_Friends_GetStatus(base.InnerHandle, ref getStatusOptionsInternal);
			Helper.Dispose<GetStatusOptionsInternal>(ref getStatusOptionsInternal);
			return result;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0002A9E8 File Offset: 0x00028BE8
		public void QueryFriends(ref QueryFriendsOptions options, object clientData, OnQueryFriendsCallback completionDelegate)
		{
			QueryFriendsOptionsInternal queryFriendsOptionsInternal = default(QueryFriendsOptionsInternal);
			queryFriendsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryFriendsCallbackInternal onQueryFriendsCallbackInternal = new OnQueryFriendsCallbackInternal(FriendsInterface.OnQueryFriendsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryFriendsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Friends_QueryFriends(base.InnerHandle, ref queryFriendsOptionsInternal, zero, onQueryFriendsCallbackInternal);
			Helper.Dispose<QueryFriendsOptionsInternal>(ref queryFriendsOptionsInternal);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x0002AA44 File Offset: 0x00028C44
		public void RejectInvite(ref RejectInviteOptions options, object clientData, OnRejectInviteCallback completionDelegate)
		{
			RejectInviteOptionsInternal rejectInviteOptionsInternal = default(RejectInviteOptionsInternal);
			rejectInviteOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnRejectInviteCallbackInternal onRejectInviteCallbackInternal = new OnRejectInviteCallbackInternal(FriendsInterface.OnRejectInviteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onRejectInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Friends_RejectInvite(base.InnerHandle, ref rejectInviteOptionsInternal, zero, onRejectInviteCallbackInternal);
			Helper.Dispose<RejectInviteOptionsInternal>(ref rejectInviteOptionsInternal);
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x0002AA9E File Offset: 0x00028C9E
		public void RemoveNotifyFriendsUpdate(ulong notificationId)
		{
			Bindings.EOS_Friends_RemoveNotifyFriendsUpdate(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x0002AAB8 File Offset: 0x00028CB8
		public void SendInvite(ref SendInviteOptions options, object clientData, OnSendInviteCallback completionDelegate)
		{
			SendInviteOptionsInternal sendInviteOptionsInternal = default(SendInviteOptionsInternal);
			sendInviteOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSendInviteCallbackInternal onSendInviteCallbackInternal = new OnSendInviteCallbackInternal(FriendsInterface.OnSendInviteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onSendInviteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_Friends_SendInvite(base.InnerHandle, ref sendInviteOptionsInternal, zero, onSendInviteCallbackInternal);
			Helper.Dispose<SendInviteOptionsInternal>(ref sendInviteOptionsInternal);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x0002AB14 File Offset: 0x00028D14
		[MonoPInvokeCallback(typeof(OnAcceptInviteCallbackInternal))]
		internal static void OnAcceptInviteCallbackInternalImplementation(ref AcceptInviteCallbackInfoInternal data)
		{
			OnAcceptInviteCallback onAcceptInviteCallback;
			AcceptInviteCallbackInfo acceptInviteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<AcceptInviteCallbackInfoInternal, OnAcceptInviteCallback, AcceptInviteCallbackInfo>(ref data, out onAcceptInviteCallback, out acceptInviteCallbackInfo);
			if (flag)
			{
				onAcceptInviteCallback(ref acceptInviteCallbackInfo);
			}
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x0002AB3C File Offset: 0x00028D3C
		[MonoPInvokeCallback(typeof(OnFriendsUpdateCallbackInternal))]
		internal static void OnFriendsUpdateCallbackInternalImplementation(ref OnFriendsUpdateInfoInternal data)
		{
			OnFriendsUpdateCallback onFriendsUpdateCallback;
			OnFriendsUpdateInfo onFriendsUpdateInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnFriendsUpdateInfoInternal, OnFriendsUpdateCallback, OnFriendsUpdateInfo>(ref data, out onFriendsUpdateCallback, out onFriendsUpdateInfo);
			if (flag)
			{
				onFriendsUpdateCallback(ref onFriendsUpdateInfo);
			}
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x0002AB64 File Offset: 0x00028D64
		[MonoPInvokeCallback(typeof(OnQueryFriendsCallbackInternal))]
		internal static void OnQueryFriendsCallbackInternalImplementation(ref QueryFriendsCallbackInfoInternal data)
		{
			OnQueryFriendsCallback onQueryFriendsCallback;
			QueryFriendsCallbackInfo queryFriendsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryFriendsCallbackInfoInternal, OnQueryFriendsCallback, QueryFriendsCallbackInfo>(ref data, out onQueryFriendsCallback, out queryFriendsCallbackInfo);
			if (flag)
			{
				onQueryFriendsCallback(ref queryFriendsCallbackInfo);
			}
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x0002AB8C File Offset: 0x00028D8C
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

		// Token: 0x06001CD2 RID: 7378 RVA: 0x0002ABB4 File Offset: 0x00028DB4
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

		// Token: 0x04000CBD RID: 3261
		public const int AcceptinviteApiLatest = 1;

		// Token: 0x04000CBE RID: 3262
		public const int AddnotifyfriendsupdateApiLatest = 1;

		// Token: 0x04000CBF RID: 3263
		public const int GetfriendatindexApiLatest = 1;

		// Token: 0x04000CC0 RID: 3264
		public const int GetfriendscountApiLatest = 1;

		// Token: 0x04000CC1 RID: 3265
		public const int GetstatusApiLatest = 1;

		// Token: 0x04000CC2 RID: 3266
		public const int QueryfriendsApiLatest = 1;

		// Token: 0x04000CC3 RID: 3267
		public const int RejectinviteApiLatest = 1;

		// Token: 0x04000CC4 RID: 3268
		public const int SendinviteApiLatest = 1;
	}
}
