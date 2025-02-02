using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001D9 RID: 473
	public sealed class RTCAudioInterface : Handle
	{
		// Token: 0x06000D1E RID: 3358 RVA: 0x00013221 File Offset: 0x00011421
		public RTCAudioInterface()
		{
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0001322B File Offset: 0x0001142B
		public RTCAudioInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00013238 File Offset: 0x00011438
		public ulong AddNotifyAudioBeforeRender(ref AddNotifyAudioBeforeRenderOptions options, object clientData, OnAudioBeforeRenderCallback completionDelegate)
		{
			AddNotifyAudioBeforeRenderOptionsInternal addNotifyAudioBeforeRenderOptionsInternal = default(AddNotifyAudioBeforeRenderOptionsInternal);
			addNotifyAudioBeforeRenderOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnAudioBeforeRenderCallbackInternal onAudioBeforeRenderCallbackInternal = new OnAudioBeforeRenderCallbackInternal(RTCAudioInterface.OnAudioBeforeRenderCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onAudioBeforeRenderCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioBeforeRender(base.InnerHandle, ref addNotifyAudioBeforeRenderOptionsInternal, zero, onAudioBeforeRenderCallbackInternal);
			Helper.Dispose<AddNotifyAudioBeforeRenderOptionsInternal>(ref addNotifyAudioBeforeRenderOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000132A4 File Offset: 0x000114A4
		public ulong AddNotifyAudioBeforeSend(ref AddNotifyAudioBeforeSendOptions options, object clientData, OnAudioBeforeSendCallback completionDelegate)
		{
			AddNotifyAudioBeforeSendOptionsInternal addNotifyAudioBeforeSendOptionsInternal = default(AddNotifyAudioBeforeSendOptionsInternal);
			addNotifyAudioBeforeSendOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnAudioBeforeSendCallbackInternal onAudioBeforeSendCallbackInternal = new OnAudioBeforeSendCallbackInternal(RTCAudioInterface.OnAudioBeforeSendCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onAudioBeforeSendCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioBeforeSend(base.InnerHandle, ref addNotifyAudioBeforeSendOptionsInternal, zero, onAudioBeforeSendCallbackInternal);
			Helper.Dispose<AddNotifyAudioBeforeSendOptionsInternal>(ref addNotifyAudioBeforeSendOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00013310 File Offset: 0x00011510
		public ulong AddNotifyAudioDevicesChanged(ref AddNotifyAudioDevicesChangedOptions options, object clientData, OnAudioDevicesChangedCallback completionDelegate)
		{
			AddNotifyAudioDevicesChangedOptionsInternal addNotifyAudioDevicesChangedOptionsInternal = default(AddNotifyAudioDevicesChangedOptionsInternal);
			addNotifyAudioDevicesChangedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnAudioDevicesChangedCallbackInternal onAudioDevicesChangedCallbackInternal = new OnAudioDevicesChangedCallbackInternal(RTCAudioInterface.OnAudioDevicesChangedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onAudioDevicesChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioDevicesChanged(base.InnerHandle, ref addNotifyAudioDevicesChangedOptionsInternal, zero, onAudioDevicesChangedCallbackInternal);
			Helper.Dispose<AddNotifyAudioDevicesChangedOptionsInternal>(ref addNotifyAudioDevicesChangedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0001337C File Offset: 0x0001157C
		public ulong AddNotifyAudioInputState(ref AddNotifyAudioInputStateOptions options, object clientData, OnAudioInputStateCallback completionDelegate)
		{
			AddNotifyAudioInputStateOptionsInternal addNotifyAudioInputStateOptionsInternal = default(AddNotifyAudioInputStateOptionsInternal);
			addNotifyAudioInputStateOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnAudioInputStateCallbackInternal onAudioInputStateCallbackInternal = new OnAudioInputStateCallbackInternal(RTCAudioInterface.OnAudioInputStateCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onAudioInputStateCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioInputState(base.InnerHandle, ref addNotifyAudioInputStateOptionsInternal, zero, onAudioInputStateCallbackInternal);
			Helper.Dispose<AddNotifyAudioInputStateOptionsInternal>(ref addNotifyAudioInputStateOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x000133E8 File Offset: 0x000115E8
		public ulong AddNotifyAudioOutputState(ref AddNotifyAudioOutputStateOptions options, object clientData, OnAudioOutputStateCallback completionDelegate)
		{
			AddNotifyAudioOutputStateOptionsInternal addNotifyAudioOutputStateOptionsInternal = default(AddNotifyAudioOutputStateOptionsInternal);
			addNotifyAudioOutputStateOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnAudioOutputStateCallbackInternal onAudioOutputStateCallbackInternal = new OnAudioOutputStateCallbackInternal(RTCAudioInterface.OnAudioOutputStateCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onAudioOutputStateCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyAudioOutputState(base.InnerHandle, ref addNotifyAudioOutputStateOptionsInternal, zero, onAudioOutputStateCallbackInternal);
			Helper.Dispose<AddNotifyAudioOutputStateOptionsInternal>(ref addNotifyAudioOutputStateOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00013454 File Offset: 0x00011654
		public ulong AddNotifyParticipantUpdated(ref AddNotifyParticipantUpdatedOptions options, object clientData, OnParticipantUpdatedCallback completionDelegate)
		{
			AddNotifyParticipantUpdatedOptionsInternal addNotifyParticipantUpdatedOptionsInternal = default(AddNotifyParticipantUpdatedOptionsInternal);
			addNotifyParticipantUpdatedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnParticipantUpdatedCallbackInternal onParticipantUpdatedCallbackInternal = new OnParticipantUpdatedCallbackInternal(RTCAudioInterface.OnParticipantUpdatedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onParticipantUpdatedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_RTCAudio_AddNotifyParticipantUpdated(base.InnerHandle, ref addNotifyParticipantUpdatedOptionsInternal, zero, onParticipantUpdatedCallbackInternal);
			Helper.Dispose<AddNotifyParticipantUpdatedOptionsInternal>(ref addNotifyParticipantUpdatedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000134C0 File Offset: 0x000116C0
		public AudioInputDeviceInfo? GetAudioInputDeviceByIndex(ref GetAudioInputDeviceByIndexOptions options)
		{
			GetAudioInputDeviceByIndexOptionsInternal getAudioInputDeviceByIndexOptionsInternal = default(GetAudioInputDeviceByIndexOptionsInternal);
			getAudioInputDeviceByIndexOptionsInternal.Set(ref options);
			IntPtr from = Bindings.EOS_RTCAudio_GetAudioInputDeviceByIndex(base.InnerHandle, ref getAudioInputDeviceByIndexOptionsInternal);
			Helper.Dispose<GetAudioInputDeviceByIndexOptionsInternal>(ref getAudioInputDeviceByIndexOptionsInternal);
			AudioInputDeviceInfo? result;
			Helper.Get<AudioInputDeviceInfoInternal, AudioInputDeviceInfo>(from, out result);
			return result;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00013504 File Offset: 0x00011704
		public uint GetAudioInputDevicesCount(ref GetAudioInputDevicesCountOptions options)
		{
			GetAudioInputDevicesCountOptionsInternal getAudioInputDevicesCountOptionsInternal = default(GetAudioInputDevicesCountOptionsInternal);
			getAudioInputDevicesCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_RTCAudio_GetAudioInputDevicesCount(base.InnerHandle, ref getAudioInputDevicesCountOptionsInternal);
			Helper.Dispose<GetAudioInputDevicesCountOptionsInternal>(ref getAudioInputDevicesCountOptionsInternal);
			return result;
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00013540 File Offset: 0x00011740
		public AudioOutputDeviceInfo? GetAudioOutputDeviceByIndex(ref GetAudioOutputDeviceByIndexOptions options)
		{
			GetAudioOutputDeviceByIndexOptionsInternal getAudioOutputDeviceByIndexOptionsInternal = default(GetAudioOutputDeviceByIndexOptionsInternal);
			getAudioOutputDeviceByIndexOptionsInternal.Set(ref options);
			IntPtr from = Bindings.EOS_RTCAudio_GetAudioOutputDeviceByIndex(base.InnerHandle, ref getAudioOutputDeviceByIndexOptionsInternal);
			Helper.Dispose<GetAudioOutputDeviceByIndexOptionsInternal>(ref getAudioOutputDeviceByIndexOptionsInternal);
			AudioOutputDeviceInfo? result;
			Helper.Get<AudioOutputDeviceInfoInternal, AudioOutputDeviceInfo>(from, out result);
			return result;
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00013584 File Offset: 0x00011784
		public uint GetAudioOutputDevicesCount(ref GetAudioOutputDevicesCountOptions options)
		{
			GetAudioOutputDevicesCountOptionsInternal getAudioOutputDevicesCountOptionsInternal = default(GetAudioOutputDevicesCountOptionsInternal);
			getAudioOutputDevicesCountOptionsInternal.Set(ref options);
			uint result = Bindings.EOS_RTCAudio_GetAudioOutputDevicesCount(base.InnerHandle, ref getAudioOutputDevicesCountOptionsInternal);
			Helper.Dispose<GetAudioOutputDevicesCountOptionsInternal>(ref getAudioOutputDevicesCountOptionsInternal);
			return result;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000135C0 File Offset: 0x000117C0
		public Result RegisterPlatformAudioUser(ref RegisterPlatformAudioUserOptions options)
		{
			RegisterPlatformAudioUserOptionsInternal registerPlatformAudioUserOptionsInternal = default(RegisterPlatformAudioUserOptionsInternal);
			registerPlatformAudioUserOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_RTCAudio_RegisterPlatformAudioUser(base.InnerHandle, ref registerPlatformAudioUserOptionsInternal);
			Helper.Dispose<RegisterPlatformAudioUserOptionsInternal>(ref registerPlatformAudioUserOptionsInternal);
			return result;
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x000135FA File Offset: 0x000117FA
		public void RemoveNotifyAudioBeforeRender(ulong notificationId)
		{
			Bindings.EOS_RTCAudio_RemoveNotifyAudioBeforeRender(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00013611 File Offset: 0x00011811
		public void RemoveNotifyAudioBeforeSend(ulong notificationId)
		{
			Bindings.EOS_RTCAudio_RemoveNotifyAudioBeforeSend(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00013628 File Offset: 0x00011828
		public void RemoveNotifyAudioDevicesChanged(ulong notificationId)
		{
			Bindings.EOS_RTCAudio_RemoveNotifyAudioDevicesChanged(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0001363F File Offset: 0x0001183F
		public void RemoveNotifyAudioInputState(ulong notificationId)
		{
			Bindings.EOS_RTCAudio_RemoveNotifyAudioInputState(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00013656 File Offset: 0x00011856
		public void RemoveNotifyAudioOutputState(ulong notificationId)
		{
			Bindings.EOS_RTCAudio_RemoveNotifyAudioOutputState(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0001366D File Offset: 0x0001186D
		public void RemoveNotifyParticipantUpdated(ulong notificationId)
		{
			Bindings.EOS_RTCAudio_RemoveNotifyParticipantUpdated(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00013684 File Offset: 0x00011884
		public Result SendAudio(ref SendAudioOptions options)
		{
			SendAudioOptionsInternal sendAudioOptionsInternal = default(SendAudioOptionsInternal);
			sendAudioOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_RTCAudio_SendAudio(base.InnerHandle, ref sendAudioOptionsInternal);
			Helper.Dispose<SendAudioOptionsInternal>(ref sendAudioOptionsInternal);
			return result;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000136C0 File Offset: 0x000118C0
		public Result SetAudioInputSettings(ref SetAudioInputSettingsOptions options)
		{
			SetAudioInputSettingsOptionsInternal setAudioInputSettingsOptionsInternal = default(SetAudioInputSettingsOptionsInternal);
			setAudioInputSettingsOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_RTCAudio_SetAudioInputSettings(base.InnerHandle, ref setAudioInputSettingsOptionsInternal);
			Helper.Dispose<SetAudioInputSettingsOptionsInternal>(ref setAudioInputSettingsOptionsInternal);
			return result;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x000136FC File Offset: 0x000118FC
		public Result SetAudioOutputSettings(ref SetAudioOutputSettingsOptions options)
		{
			SetAudioOutputSettingsOptionsInternal setAudioOutputSettingsOptionsInternal = default(SetAudioOutputSettingsOptionsInternal);
			setAudioOutputSettingsOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_RTCAudio_SetAudioOutputSettings(base.InnerHandle, ref setAudioOutputSettingsOptionsInternal);
			Helper.Dispose<SetAudioOutputSettingsOptionsInternal>(ref setAudioOutputSettingsOptionsInternal);
			return result;
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00013738 File Offset: 0x00011938
		public Result UnregisterPlatformAudioUser(ref UnregisterPlatformAudioUserOptions options)
		{
			UnregisterPlatformAudioUserOptionsInternal unregisterPlatformAudioUserOptionsInternal = default(UnregisterPlatformAudioUserOptionsInternal);
			unregisterPlatformAudioUserOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_RTCAudio_UnregisterPlatformAudioUser(base.InnerHandle, ref unregisterPlatformAudioUserOptionsInternal);
			Helper.Dispose<UnregisterPlatformAudioUserOptionsInternal>(ref unregisterPlatformAudioUserOptionsInternal);
			return result;
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00013774 File Offset: 0x00011974
		public void UpdateParticipantVolume(ref UpdateParticipantVolumeOptions options, object clientData, OnUpdateParticipantVolumeCallback completionDelegate)
		{
			UpdateParticipantVolumeOptionsInternal updateParticipantVolumeOptionsInternal = default(UpdateParticipantVolumeOptionsInternal);
			updateParticipantVolumeOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUpdateParticipantVolumeCallbackInternal onUpdateParticipantVolumeCallbackInternal = new OnUpdateParticipantVolumeCallbackInternal(RTCAudioInterface.OnUpdateParticipantVolumeCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUpdateParticipantVolumeCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAudio_UpdateParticipantVolume(base.InnerHandle, ref updateParticipantVolumeOptionsInternal, zero, onUpdateParticipantVolumeCallbackInternal);
			Helper.Dispose<UpdateParticipantVolumeOptionsInternal>(ref updateParticipantVolumeOptionsInternal);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x000137D0 File Offset: 0x000119D0
		public void UpdateReceiving(ref UpdateReceivingOptions options, object clientData, OnUpdateReceivingCallback completionDelegate)
		{
			UpdateReceivingOptionsInternal updateReceivingOptionsInternal = default(UpdateReceivingOptionsInternal);
			updateReceivingOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUpdateReceivingCallbackInternal onUpdateReceivingCallbackInternal = new OnUpdateReceivingCallbackInternal(RTCAudioInterface.OnUpdateReceivingCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUpdateReceivingCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAudio_UpdateReceiving(base.InnerHandle, ref updateReceivingOptionsInternal, zero, onUpdateReceivingCallbackInternal);
			Helper.Dispose<UpdateReceivingOptionsInternal>(ref updateReceivingOptionsInternal);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0001382C File Offset: 0x00011A2C
		public void UpdateReceivingVolume(ref UpdateReceivingVolumeOptions options, object clientData, OnUpdateReceivingVolumeCallback completionDelegate)
		{
			UpdateReceivingVolumeOptionsInternal updateReceivingVolumeOptionsInternal = default(UpdateReceivingVolumeOptionsInternal);
			updateReceivingVolumeOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUpdateReceivingVolumeCallbackInternal onUpdateReceivingVolumeCallbackInternal = new OnUpdateReceivingVolumeCallbackInternal(RTCAudioInterface.OnUpdateReceivingVolumeCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUpdateReceivingVolumeCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAudio_UpdateReceivingVolume(base.InnerHandle, ref updateReceivingVolumeOptionsInternal, zero, onUpdateReceivingVolumeCallbackInternal);
			Helper.Dispose<UpdateReceivingVolumeOptionsInternal>(ref updateReceivingVolumeOptionsInternal);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00013888 File Offset: 0x00011A88
		public void UpdateSending(ref UpdateSendingOptions options, object clientData, OnUpdateSendingCallback completionDelegate)
		{
			UpdateSendingOptionsInternal updateSendingOptionsInternal = default(UpdateSendingOptionsInternal);
			updateSendingOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUpdateSendingCallbackInternal onUpdateSendingCallbackInternal = new OnUpdateSendingCallbackInternal(RTCAudioInterface.OnUpdateSendingCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUpdateSendingCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAudio_UpdateSending(base.InnerHandle, ref updateSendingOptionsInternal, zero, onUpdateSendingCallbackInternal);
			Helper.Dispose<UpdateSendingOptionsInternal>(ref updateSendingOptionsInternal);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000138E4 File Offset: 0x00011AE4
		public void UpdateSendingVolume(ref UpdateSendingVolumeOptions options, object clientData, OnUpdateSendingVolumeCallback completionDelegate)
		{
			UpdateSendingVolumeOptionsInternal updateSendingVolumeOptionsInternal = default(UpdateSendingVolumeOptionsInternal);
			updateSendingVolumeOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnUpdateSendingVolumeCallbackInternal onUpdateSendingVolumeCallbackInternal = new OnUpdateSendingVolumeCallbackInternal(RTCAudioInterface.OnUpdateSendingVolumeCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onUpdateSendingVolumeCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_RTCAudio_UpdateSendingVolume(base.InnerHandle, ref updateSendingVolumeOptionsInternal, zero, onUpdateSendingVolumeCallbackInternal);
			Helper.Dispose<UpdateSendingVolumeOptionsInternal>(ref updateSendingVolumeOptionsInternal);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00013940 File Offset: 0x00011B40
		[MonoPInvokeCallback(typeof(OnAudioBeforeRenderCallbackInternal))]
		internal static void OnAudioBeforeRenderCallbackInternalImplementation(ref AudioBeforeRenderCallbackInfoInternal data)
		{
			OnAudioBeforeRenderCallback onAudioBeforeRenderCallback;
			AudioBeforeRenderCallbackInfo audioBeforeRenderCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<AudioBeforeRenderCallbackInfoInternal, OnAudioBeforeRenderCallback, AudioBeforeRenderCallbackInfo>(ref data, out onAudioBeforeRenderCallback, out audioBeforeRenderCallbackInfo);
			if (flag)
			{
				onAudioBeforeRenderCallback(ref audioBeforeRenderCallbackInfo);
			}
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00013968 File Offset: 0x00011B68
		[MonoPInvokeCallback(typeof(OnAudioBeforeSendCallbackInternal))]
		internal static void OnAudioBeforeSendCallbackInternalImplementation(ref AudioBeforeSendCallbackInfoInternal data)
		{
			OnAudioBeforeSendCallback onAudioBeforeSendCallback;
			AudioBeforeSendCallbackInfo audioBeforeSendCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<AudioBeforeSendCallbackInfoInternal, OnAudioBeforeSendCallback, AudioBeforeSendCallbackInfo>(ref data, out onAudioBeforeSendCallback, out audioBeforeSendCallbackInfo);
			if (flag)
			{
				onAudioBeforeSendCallback(ref audioBeforeSendCallbackInfo);
			}
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00013990 File Offset: 0x00011B90
		[MonoPInvokeCallback(typeof(OnAudioDevicesChangedCallbackInternal))]
		internal static void OnAudioDevicesChangedCallbackInternalImplementation(ref AudioDevicesChangedCallbackInfoInternal data)
		{
			OnAudioDevicesChangedCallback onAudioDevicesChangedCallback;
			AudioDevicesChangedCallbackInfo audioDevicesChangedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<AudioDevicesChangedCallbackInfoInternal, OnAudioDevicesChangedCallback, AudioDevicesChangedCallbackInfo>(ref data, out onAudioDevicesChangedCallback, out audioDevicesChangedCallbackInfo);
			if (flag)
			{
				onAudioDevicesChangedCallback(ref audioDevicesChangedCallbackInfo);
			}
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000139B8 File Offset: 0x00011BB8
		[MonoPInvokeCallback(typeof(OnAudioInputStateCallbackInternal))]
		internal static void OnAudioInputStateCallbackInternalImplementation(ref AudioInputStateCallbackInfoInternal data)
		{
			OnAudioInputStateCallback onAudioInputStateCallback;
			AudioInputStateCallbackInfo audioInputStateCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<AudioInputStateCallbackInfoInternal, OnAudioInputStateCallback, AudioInputStateCallbackInfo>(ref data, out onAudioInputStateCallback, out audioInputStateCallbackInfo);
			if (flag)
			{
				onAudioInputStateCallback(ref audioInputStateCallbackInfo);
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x000139E0 File Offset: 0x00011BE0
		[MonoPInvokeCallback(typeof(OnAudioOutputStateCallbackInternal))]
		internal static void OnAudioOutputStateCallbackInternalImplementation(ref AudioOutputStateCallbackInfoInternal data)
		{
			OnAudioOutputStateCallback onAudioOutputStateCallback;
			AudioOutputStateCallbackInfo audioOutputStateCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<AudioOutputStateCallbackInfoInternal, OnAudioOutputStateCallback, AudioOutputStateCallbackInfo>(ref data, out onAudioOutputStateCallback, out audioOutputStateCallbackInfo);
			if (flag)
			{
				onAudioOutputStateCallback(ref audioOutputStateCallbackInfo);
			}
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x00013A08 File Offset: 0x00011C08
		[MonoPInvokeCallback(typeof(OnParticipantUpdatedCallbackInternal))]
		internal static void OnParticipantUpdatedCallbackInternalImplementation(ref ParticipantUpdatedCallbackInfoInternal data)
		{
			OnParticipantUpdatedCallback onParticipantUpdatedCallback;
			ParticipantUpdatedCallbackInfo participantUpdatedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<ParticipantUpdatedCallbackInfoInternal, OnParticipantUpdatedCallback, ParticipantUpdatedCallbackInfo>(ref data, out onParticipantUpdatedCallback, out participantUpdatedCallbackInfo);
			if (flag)
			{
				onParticipantUpdatedCallback(ref participantUpdatedCallbackInfo);
			}
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x00013A30 File Offset: 0x00011C30
		[MonoPInvokeCallback(typeof(OnUpdateParticipantVolumeCallbackInternal))]
		internal static void OnUpdateParticipantVolumeCallbackInternalImplementation(ref UpdateParticipantVolumeCallbackInfoInternal data)
		{
			OnUpdateParticipantVolumeCallback onUpdateParticipantVolumeCallback;
			UpdateParticipantVolumeCallbackInfo updateParticipantVolumeCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UpdateParticipantVolumeCallbackInfoInternal, OnUpdateParticipantVolumeCallback, UpdateParticipantVolumeCallbackInfo>(ref data, out onUpdateParticipantVolumeCallback, out updateParticipantVolumeCallbackInfo);
			if (flag)
			{
				onUpdateParticipantVolumeCallback(ref updateParticipantVolumeCallbackInfo);
			}
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x00013A58 File Offset: 0x00011C58
		[MonoPInvokeCallback(typeof(OnUpdateReceivingCallbackInternal))]
		internal static void OnUpdateReceivingCallbackInternalImplementation(ref UpdateReceivingCallbackInfoInternal data)
		{
			OnUpdateReceivingCallback onUpdateReceivingCallback;
			UpdateReceivingCallbackInfo updateReceivingCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UpdateReceivingCallbackInfoInternal, OnUpdateReceivingCallback, UpdateReceivingCallbackInfo>(ref data, out onUpdateReceivingCallback, out updateReceivingCallbackInfo);
			if (flag)
			{
				onUpdateReceivingCallback(ref updateReceivingCallbackInfo);
			}
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x00013A80 File Offset: 0x00011C80
		[MonoPInvokeCallback(typeof(OnUpdateReceivingVolumeCallbackInternal))]
		internal static void OnUpdateReceivingVolumeCallbackInternalImplementation(ref UpdateReceivingVolumeCallbackInfoInternal data)
		{
			OnUpdateReceivingVolumeCallback onUpdateReceivingVolumeCallback;
			UpdateReceivingVolumeCallbackInfo updateReceivingVolumeCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UpdateReceivingVolumeCallbackInfoInternal, OnUpdateReceivingVolumeCallback, UpdateReceivingVolumeCallbackInfo>(ref data, out onUpdateReceivingVolumeCallback, out updateReceivingVolumeCallbackInfo);
			if (flag)
			{
				onUpdateReceivingVolumeCallback(ref updateReceivingVolumeCallbackInfo);
			}
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x00013AA8 File Offset: 0x00011CA8
		[MonoPInvokeCallback(typeof(OnUpdateSendingCallbackInternal))]
		internal static void OnUpdateSendingCallbackInternalImplementation(ref UpdateSendingCallbackInfoInternal data)
		{
			OnUpdateSendingCallback onUpdateSendingCallback;
			UpdateSendingCallbackInfo updateSendingCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UpdateSendingCallbackInfoInternal, OnUpdateSendingCallback, UpdateSendingCallbackInfo>(ref data, out onUpdateSendingCallback, out updateSendingCallbackInfo);
			if (flag)
			{
				onUpdateSendingCallback(ref updateSendingCallbackInfo);
			}
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00013AD0 File Offset: 0x00011CD0
		[MonoPInvokeCallback(typeof(OnUpdateSendingVolumeCallbackInternal))]
		internal static void OnUpdateSendingVolumeCallbackInternalImplementation(ref UpdateSendingVolumeCallbackInfoInternal data)
		{
			OnUpdateSendingVolumeCallback onUpdateSendingVolumeCallback;
			UpdateSendingVolumeCallbackInfo updateSendingVolumeCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<UpdateSendingVolumeCallbackInfoInternal, OnUpdateSendingVolumeCallback, UpdateSendingVolumeCallbackInfo>(ref data, out onUpdateSendingVolumeCallback, out updateSendingVolumeCallbackInfo);
			if (flag)
			{
				onUpdateSendingVolumeCallback(ref updateSendingVolumeCallbackInfo);
			}
		}

		// Token: 0x040005D3 RID: 1491
		public const int AddnotifyaudiobeforerenderApiLatest = 1;

		// Token: 0x040005D4 RID: 1492
		public const int AddnotifyaudiobeforesendApiLatest = 1;

		// Token: 0x040005D5 RID: 1493
		public const int AddnotifyaudiodeviceschangedApiLatest = 1;

		// Token: 0x040005D6 RID: 1494
		public const int AddnotifyaudioinputstateApiLatest = 1;

		// Token: 0x040005D7 RID: 1495
		public const int AddnotifyaudiooutputstateApiLatest = 1;

		// Token: 0x040005D8 RID: 1496
		public const int AddnotifyparticipantupdatedApiLatest = 1;

		// Token: 0x040005D9 RID: 1497
		public const int AudiobufferApiLatest = 1;

		// Token: 0x040005DA RID: 1498
		public const int AudioinputdeviceinfoApiLatest = 1;

		// Token: 0x040005DB RID: 1499
		public const int AudiooutputdeviceinfoApiLatest = 1;

		// Token: 0x040005DC RID: 1500
		public const int GetaudioinputdevicebyindexApiLatest = 1;

		// Token: 0x040005DD RID: 1501
		public const int GetaudioinputdevicescountApiLatest = 1;

		// Token: 0x040005DE RID: 1502
		public const int GetaudiooutputdevicebyindexApiLatest = 1;

		// Token: 0x040005DF RID: 1503
		public const int GetaudiooutputdevicescountApiLatest = 1;

		// Token: 0x040005E0 RID: 1504
		public const int RegisterplatformaudiouserApiLatest = 1;

		// Token: 0x040005E1 RID: 1505
		public const int SendaudioApiLatest = 1;

		// Token: 0x040005E2 RID: 1506
		public const int SetaudioinputsettingsApiLatest = 1;

		// Token: 0x040005E3 RID: 1507
		public const int SetaudiooutputsettingsApiLatest = 1;

		// Token: 0x040005E4 RID: 1508
		public const int UnregisterplatformaudiouserApiLatest = 1;

		// Token: 0x040005E5 RID: 1509
		public const int UpdateparticipantvolumeApiLatest = 1;

		// Token: 0x040005E6 RID: 1510
		public const int UpdatereceivingApiLatest = 1;

		// Token: 0x040005E7 RID: 1511
		public const int UpdatereceivingvolumeApiLatest = 1;

		// Token: 0x040005E8 RID: 1512
		public const int UpdatesendingApiLatest = 1;

		// Token: 0x040005E9 RID: 1513
		public const int UpdatesendingvolumeApiLatest = 1;
	}
}
