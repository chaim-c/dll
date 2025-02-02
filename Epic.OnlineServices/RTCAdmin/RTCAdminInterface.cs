using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x0200020A RID: 522
	public sealed class RTCAdminInterface : Handle
	{
		// Token: 0x06000EB3 RID: 3763 RVA: 0x00015C04 File Offset: 0x00013E04
		public RTCAdminInterface()
		{
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00015C0E File Offset: 0x00013E0E
		public RTCAdminInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x00015C1C File Offset: 0x00013E1C
		public Result CopyUserTokenByIndex(ref CopyUserTokenByIndexOptions options, out UserToken? outUserToken)
		{
			CopyUserTokenByIndexOptionsInternal copyUserTokenByIndexOptionsInternal = default(CopyUserTokenByIndexOptionsInternal);
			copyUserTokenByIndexOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_RTCAdmin_CopyUserTokenByIndex(base.InnerHandle, ref copyUserTokenByIndexOptionsInternal, ref zero);
			Helper.Dispose<CopyUserTokenByIndexOptionsInternal>(ref copyUserTokenByIndexOptionsInternal);
			Helper.Get<UserTokenInternal, UserToken>(zero, out outUserToken);
			bool flag = outUserToken != null;
			if (flag)
			{
				Bindings.EOS_RTCAdmin_UserToken_Release(zero);
			}
			return result;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00015C7C File Offset: 0x00013E7C
		public Result CopyUserTokenByUserId(ref CopyUserTokenByUserIdOptions options, out UserToken? outUserToken)
		{
			CopyUserTokenByUserIdOptionsInternal copyUserTokenByUserIdOptionsInternal = default(CopyUserTokenByUserIdOptionsInternal);
			copyUserTokenByUserIdOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			Result result = Bindings.EOS_RTCAdmin_CopyUserTokenByUserId(base.InnerHandle, ref copyUserTokenByUserIdOptionsInternal, ref zero);
			Helper.Dispose<CopyUserTokenByUserIdOptionsInternal>(ref copyUserTokenByUserIdOptionsInternal);
			Helper.Get<UserTokenInternal, UserToken>(zero, out outUserToken);
			bool flag = outUserToken != null;
			if (flag)
			{
				Bindings.EOS_RTCAdmin_UserToken_Release(zero);
			}
			return result;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00015CDC File Offset: 0x00013EDC
		public void Kick(ref KickOptions options, object clientData, OnKickCompleteCallback completionDelegate)
		{
			KickOptionsInternal kickOptionsInternal = default(KickOptionsInternal);
			kickOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnKickCompleteCallbackInternal onKickCompleteCallbackInternal = new OnKickCompleteCallbackInternal(RTCAdminInterface.OnKickCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onKickCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAdmin_Kick(base.InnerHandle, ref kickOptionsInternal, zero, onKickCompleteCallbackInternal);
			Helper.Dispose<KickOptionsInternal>(ref kickOptionsInternal);
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x00015D38 File Offset: 0x00013F38
		public void QueryJoinRoomToken(ref QueryJoinRoomTokenOptions options, object clientData, OnQueryJoinRoomTokenCompleteCallback completionDelegate)
		{
			QueryJoinRoomTokenOptionsInternal queryJoinRoomTokenOptionsInternal = default(QueryJoinRoomTokenOptionsInternal);
			queryJoinRoomTokenOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnQueryJoinRoomTokenCompleteCallbackInternal onQueryJoinRoomTokenCompleteCallbackInternal = new OnQueryJoinRoomTokenCompleteCallbackInternal(RTCAdminInterface.OnQueryJoinRoomTokenCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onQueryJoinRoomTokenCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAdmin_QueryJoinRoomToken(base.InnerHandle, ref queryJoinRoomTokenOptionsInternal, zero, onQueryJoinRoomTokenCompleteCallbackInternal);
			Helper.Dispose<QueryJoinRoomTokenOptionsInternal>(ref queryJoinRoomTokenOptionsInternal);
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x00015D94 File Offset: 0x00013F94
		public void SetParticipantHardMute(ref SetParticipantHardMuteOptions options, object clientData, OnSetParticipantHardMuteCompleteCallback completionDelegate)
		{
			SetParticipantHardMuteOptionsInternal setParticipantHardMuteOptionsInternal = default(SetParticipantHardMuteOptionsInternal);
			setParticipantHardMuteOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnSetParticipantHardMuteCompleteCallbackInternal onSetParticipantHardMuteCompleteCallbackInternal = new OnSetParticipantHardMuteCompleteCallbackInternal(RTCAdminInterface.OnSetParticipantHardMuteCompleteCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onSetParticipantHardMuteCompleteCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAdmin_SetParticipantHardMute(base.InnerHandle, ref setParticipantHardMuteOptionsInternal, zero, onSetParticipantHardMuteCompleteCallbackInternal);
			Helper.Dispose<SetParticipantHardMuteOptionsInternal>(ref setParticipantHardMuteOptionsInternal);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x00015DF0 File Offset: 0x00013FF0
		[MonoPInvokeCallback(typeof(OnKickCompleteCallbackInternal))]
		internal static void OnKickCompleteCallbackInternalImplementation(ref KickCompleteCallbackInfoInternal data)
		{
			OnKickCompleteCallback onKickCompleteCallback;
			KickCompleteCallbackInfo kickCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<KickCompleteCallbackInfoInternal, OnKickCompleteCallback, KickCompleteCallbackInfo>(ref data, out onKickCompleteCallback, out kickCompleteCallbackInfo);
			if (flag)
			{
				onKickCompleteCallback(ref kickCompleteCallbackInfo);
			}
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x00015E18 File Offset: 0x00014018
		[MonoPInvokeCallback(typeof(OnQueryJoinRoomTokenCompleteCallbackInternal))]
		internal static void OnQueryJoinRoomTokenCompleteCallbackInternalImplementation(ref QueryJoinRoomTokenCompleteCallbackInfoInternal data)
		{
			OnQueryJoinRoomTokenCompleteCallback onQueryJoinRoomTokenCompleteCallback;
			QueryJoinRoomTokenCompleteCallbackInfo queryJoinRoomTokenCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<QueryJoinRoomTokenCompleteCallbackInfoInternal, OnQueryJoinRoomTokenCompleteCallback, QueryJoinRoomTokenCompleteCallbackInfo>(ref data, out onQueryJoinRoomTokenCompleteCallback, out queryJoinRoomTokenCompleteCallbackInfo);
			if (flag)
			{
				onQueryJoinRoomTokenCompleteCallback(ref queryJoinRoomTokenCompleteCallbackInfo);
			}
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00015E40 File Offset: 0x00014040
		[MonoPInvokeCallback(typeof(OnSetParticipantHardMuteCompleteCallbackInternal))]
		internal static void OnSetParticipantHardMuteCompleteCallbackInternalImplementation(ref SetParticipantHardMuteCompleteCallbackInfoInternal data)
		{
			OnSetParticipantHardMuteCompleteCallback onSetParticipantHardMuteCompleteCallback;
			SetParticipantHardMuteCompleteCallbackInfo setParticipantHardMuteCompleteCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<SetParticipantHardMuteCompleteCallbackInfoInternal, OnSetParticipantHardMuteCompleteCallback, SetParticipantHardMuteCompleteCallbackInfo>(ref data, out onSetParticipantHardMuteCompleteCallback, out setParticipantHardMuteCompleteCallbackInfo);
			if (flag)
			{
				onSetParticipantHardMuteCompleteCallback(ref setParticipantHardMuteCompleteCallbackInfo);
			}
		}

		// Token: 0x04000694 RID: 1684
		public const int CopyusertokenbyindexApiLatest = 2;

		// Token: 0x04000695 RID: 1685
		public const int CopyusertokenbyuseridApiLatest = 2;

		// Token: 0x04000696 RID: 1686
		public const int KickApiLatest = 1;

		// Token: 0x04000697 RID: 1687
		public const int QueryjoinroomtokenApiLatest = 2;

		// Token: 0x04000698 RID: 1688
		public const int SetparticipanthardmuteApiLatest = 1;

		// Token: 0x04000699 RID: 1689
		public const int UsertokenApiLatest = 1;
	}
}
