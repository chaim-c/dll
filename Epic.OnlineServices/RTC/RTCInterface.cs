using System;
using Epic.OnlineServices.RTCAudio;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000194 RID: 404
	public sealed class RTCInterface : Handle
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x000112C9 File Offset: 0x0000F4C9
		public RTCInterface()
		{
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000112D3 File Offset: 0x0000F4D3
		public RTCInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000112E0 File Offset: 0x0000F4E0
		public ulong AddNotifyDisconnected(ref AddNotifyDisconnectedOptions options, object clientData, OnDisconnectedCallback completionDelegate)
		{
			AddNotifyDisconnectedOptionsInternal addNotifyDisconnectedOptionsInternal = default(AddNotifyDisconnectedOptionsInternal);
			addNotifyDisconnectedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnDisconnectedCallbackInternal onDisconnectedCallbackInternal = new OnDisconnectedCallbackInternal(RTCInterface.OnDisconnectedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onDisconnectedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTC_AddNotifyDisconnected(base.InnerHandle, ref addNotifyDisconnectedOptionsInternal, zero, onDisconnectedCallbackInternal);
			Helper.Dispose<AddNotifyDisconnectedOptionsInternal>(ref addNotifyDisconnectedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x0001134C File Offset: 0x0000F54C
		public ulong AddNotifyParticipantStatusChanged(ref AddNotifyParticipantStatusChangedOptions options, object clientData, OnParticipantStatusChangedCallback completionDelegate)
		{
			AddNotifyParticipantStatusChangedOptionsInternal addNotifyParticipantStatusChangedOptionsInternal = default(AddNotifyParticipantStatusChangedOptionsInternal);
			addNotifyParticipantStatusChangedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnParticipantStatusChangedCallbackInternal onParticipantStatusChangedCallbackInternal = new OnParticipantStatusChangedCallbackInternal(RTCInterface.OnParticipantStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onParticipantStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTC_AddNotifyParticipantStatusChanged(base.InnerHandle, ref addNotifyParticipantStatusChangedOptionsInternal, zero, onParticipantStatusChangedCallbackInternal);
			Helper.Dispose<AddNotifyParticipantStatusChangedOptionsInternal>(ref addNotifyParticipantStatusChangedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public void BlockParticipant(ref BlockParticipantOptions options, object clientData, OnBlockParticipantCallback completionDelegate)
		{
			BlockParticipantOptionsInternal blockParticipantOptionsInternal = default(BlockParticipantOptionsInternal);
			blockParticipantOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnBlockParticipantCallbackInternal onBlockParticipantCallbackInternal = new OnBlockParticipantCallbackInternal(RTCInterface.OnBlockParticipantCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onBlockParticipantCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTC_BlockParticipant(base.InnerHandle, ref blockParticipantOptionsInternal, zero, onBlockParticipantCallbackInternal);
			Helper.Dispose<BlockParticipantOptionsInternal>(ref blockParticipantOptionsInternal);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00011414 File Offset: 0x0000F614
		public RTCAudioInterface GetAudioInterface()
		{
			IntPtr from = Bindings.EOS_RTC_GetAudioInterface(base.InnerHandle);
			RTCAudioInterface result;
			Helper.Get<RTCAudioInterface>(from, out result);
			return result;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0001143C File Offset: 0x0000F63C
		public void JoinRoom(ref JoinRoomOptions options, object clientData, OnJoinRoomCallback completionDelegate)
		{
			JoinRoomOptionsInternal joinRoomOptionsInternal = default(JoinRoomOptionsInternal);
			joinRoomOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnJoinRoomCallbackInternal onJoinRoomCallbackInternal = new OnJoinRoomCallbackInternal(RTCInterface.OnJoinRoomCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onJoinRoomCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTC_JoinRoom(base.InnerHandle, ref joinRoomOptionsInternal, zero, onJoinRoomCallbackInternal);
			Helper.Dispose<JoinRoomOptionsInternal>(ref joinRoomOptionsInternal);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00011498 File Offset: 0x0000F698
		public void LeaveRoom(ref LeaveRoomOptions options, object clientData, OnLeaveRoomCallback completionDelegate)
		{
			LeaveRoomOptionsInternal leaveRoomOptionsInternal = default(LeaveRoomOptionsInternal);
			leaveRoomOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnLeaveRoomCallbackInternal onLeaveRoomCallbackInternal = new OnLeaveRoomCallbackInternal(RTCInterface.OnLeaveRoomCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onLeaveRoomCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTC_LeaveRoom(base.InnerHandle, ref leaveRoomOptionsInternal, zero, onLeaveRoomCallbackInternal);
			Helper.Dispose<LeaveRoomOptionsInternal>(ref leaveRoomOptionsInternal);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000114F2 File Offset: 0x0000F6F2
		public void RemoveNotifyDisconnected(ulong notificationId)
		{
			Bindings.EOS_RTC_RemoveNotifyDisconnected(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00011509 File Offset: 0x0000F709
		public void RemoveNotifyParticipantStatusChanged(ulong notificationId)
		{
			Bindings.EOS_RTC_RemoveNotifyParticipantStatusChanged(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00011520 File Offset: 0x0000F720
		public Result SetRoomSetting(ref SetRoomSettingOptions options)
		{
			SetRoomSettingOptionsInternal setRoomSettingOptionsInternal = default(SetRoomSettingOptionsInternal);
			setRoomSettingOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_RTC_SetRoomSetting(base.InnerHandle, ref setRoomSettingOptionsInternal);
			Helper.Dispose<SetRoomSettingOptionsInternal>(ref setRoomSettingOptionsInternal);
			return result;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0001155C File Offset: 0x0000F75C
		public Result SetSetting(ref SetSettingOptions options)
		{
			SetSettingOptionsInternal setSettingOptionsInternal = default(SetSettingOptionsInternal);
			setSettingOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_RTC_SetSetting(base.InnerHandle, ref setSettingOptionsInternal);
			Helper.Dispose<SetSettingOptionsInternal>(ref setSettingOptionsInternal);
			return result;
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00011598 File Offset: 0x0000F798
		[MonoPInvokeCallback(typeof(OnBlockParticipantCallbackInternal))]
		internal static void OnBlockParticipantCallbackInternalImplementation(ref BlockParticipantCallbackInfoInternal data)
		{
			OnBlockParticipantCallback onBlockParticipantCallback;
			BlockParticipantCallbackInfo blockParticipantCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<BlockParticipantCallbackInfoInternal, OnBlockParticipantCallback, BlockParticipantCallbackInfo>(ref data, out onBlockParticipantCallback, out blockParticipantCallbackInfo);
			if (flag)
			{
				onBlockParticipantCallback(ref blockParticipantCallbackInfo);
			}
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000115C0 File Offset: 0x0000F7C0
		[MonoPInvokeCallback(typeof(OnDisconnectedCallbackInternal))]
		internal static void OnDisconnectedCallbackInternalImplementation(ref DisconnectedCallbackInfoInternal data)
		{
			OnDisconnectedCallback onDisconnectedCallback;
			DisconnectedCallbackInfo disconnectedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<DisconnectedCallbackInfoInternal, OnDisconnectedCallback, DisconnectedCallbackInfo>(ref data, out onDisconnectedCallback, out disconnectedCallbackInfo);
			if (flag)
			{
				onDisconnectedCallback(ref disconnectedCallbackInfo);
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000115E8 File Offset: 0x0000F7E8
		[MonoPInvokeCallback(typeof(OnJoinRoomCallbackInternal))]
		internal static void OnJoinRoomCallbackInternalImplementation(ref JoinRoomCallbackInfoInternal data)
		{
			OnJoinRoomCallback onJoinRoomCallback;
			JoinRoomCallbackInfo joinRoomCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<JoinRoomCallbackInfoInternal, OnJoinRoomCallback, JoinRoomCallbackInfo>(ref data, out onJoinRoomCallback, out joinRoomCallbackInfo);
			if (flag)
			{
				onJoinRoomCallback(ref joinRoomCallbackInfo);
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00011610 File Offset: 0x0000F810
		[MonoPInvokeCallback(typeof(OnLeaveRoomCallbackInternal))]
		internal static void OnLeaveRoomCallbackInternalImplementation(ref LeaveRoomCallbackInfoInternal data)
		{
			OnLeaveRoomCallback onLeaveRoomCallback;
			LeaveRoomCallbackInfo leaveRoomCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<LeaveRoomCallbackInfoInternal, OnLeaveRoomCallback, LeaveRoomCallbackInfo>(ref data, out onLeaveRoomCallback, out leaveRoomCallbackInfo);
			if (flag)
			{
				onLeaveRoomCallback(ref leaveRoomCallbackInfo);
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x00011638 File Offset: 0x0000F838
		[MonoPInvokeCallback(typeof(OnParticipantStatusChangedCallbackInternal))]
		internal static void OnParticipantStatusChangedCallbackInternalImplementation(ref ParticipantStatusChangedCallbackInfoInternal data)
		{
			OnParticipantStatusChangedCallback onParticipantStatusChangedCallback;
			ParticipantStatusChangedCallbackInfo participantStatusChangedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<ParticipantStatusChangedCallbackInfoInternal, OnParticipantStatusChangedCallback, ParticipantStatusChangedCallbackInfo>(ref data, out onParticipantStatusChangedCallback, out participantStatusChangedCallbackInfo);
			if (flag)
			{
				onParticipantStatusChangedCallback(ref participantStatusChangedCallbackInfo);
			}
		}

		// Token: 0x04000545 RID: 1349
		public const int AddnotifydisconnectedApiLatest = 1;

		// Token: 0x04000546 RID: 1350
		public const int AddnotifyparticipantstatuschangedApiLatest = 1;

		// Token: 0x04000547 RID: 1351
		public const int BlockparticipantApiLatest = 1;

		// Token: 0x04000548 RID: 1352
		public const int JoinroomApiLatest = 1;

		// Token: 0x04000549 RID: 1353
		public const int LeaveroomApiLatest = 1;

		// Token: 0x0400054A RID: 1354
		public const int ParticipantmetadataApiLatest = 1;

		// Token: 0x0400054B RID: 1355
		public const int ParticipantmetadataKeyMaxcharcount = 256;

		// Token: 0x0400054C RID: 1356
		public const int ParticipantmetadataValueMaxcharcount = 256;

		// Token: 0x0400054D RID: 1357
		public const int SetroomsettingApiLatest = 1;

		// Token: 0x0400054E RID: 1358
		public const int SetsettingApiLatest = 1;
	}
}
