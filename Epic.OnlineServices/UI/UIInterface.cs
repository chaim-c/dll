using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000079 RID: 121
	public sealed class UIInterface : Handle
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00007462 File Offset: 0x00005662
		public UIInterface()
		{
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0000746C File Offset: 0x0000566C
		public UIInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00007478 File Offset: 0x00005678
		public Result AcknowledgeEventId(ref AcknowledgeEventIdOptions options)
		{
			AcknowledgeEventIdOptionsInternal acknowledgeEventIdOptionsInternal = default(AcknowledgeEventIdOptionsInternal);
			acknowledgeEventIdOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_UI_AcknowledgeEventId(base.InnerHandle, ref acknowledgeEventIdOptionsInternal);
			Helper.Dispose<AcknowledgeEventIdOptionsInternal>(ref acknowledgeEventIdOptionsInternal);
			return result;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000074B4 File Offset: 0x000056B4
		public ulong AddNotifyDisplaySettingsUpdated(ref AddNotifyDisplaySettingsUpdatedOptions options, object clientData, OnDisplaySettingsUpdatedCallback notificationFn)
		{
			AddNotifyDisplaySettingsUpdatedOptionsInternal addNotifyDisplaySettingsUpdatedOptionsInternal = default(AddNotifyDisplaySettingsUpdatedOptionsInternal);
			addNotifyDisplaySettingsUpdatedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnDisplaySettingsUpdatedCallbackInternal onDisplaySettingsUpdatedCallbackInternal = new OnDisplaySettingsUpdatedCallbackInternal(UIInterface.OnDisplaySettingsUpdatedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onDisplaySettingsUpdatedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_UI_AddNotifyDisplaySettingsUpdated(base.InnerHandle, ref addNotifyDisplaySettingsUpdatedOptionsInternal, zero, onDisplaySettingsUpdatedCallbackInternal);
			Helper.Dispose<AddNotifyDisplaySettingsUpdatedOptionsInternal>(ref addNotifyDisplaySettingsUpdatedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00007520 File Offset: 0x00005720
		public bool GetFriendsExclusiveInput(ref GetFriendsExclusiveInputOptions options)
		{
			GetFriendsExclusiveInputOptionsInternal getFriendsExclusiveInputOptionsInternal = default(GetFriendsExclusiveInputOptionsInternal);
			getFriendsExclusiveInputOptionsInternal.Set(ref options);
			int from = Bindings.EOS_UI_GetFriendsExclusiveInput(base.InnerHandle, ref getFriendsExclusiveInputOptionsInternal);
			Helper.Dispose<GetFriendsExclusiveInputOptionsInternal>(ref getFriendsExclusiveInputOptionsInternal);
			bool result;
			Helper.Get(from, out result);
			return result;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00007564 File Offset: 0x00005764
		public bool GetFriendsVisible(ref GetFriendsVisibleOptions options)
		{
			GetFriendsVisibleOptionsInternal getFriendsVisibleOptionsInternal = default(GetFriendsVisibleOptionsInternal);
			getFriendsVisibleOptionsInternal.Set(ref options);
			int from = Bindings.EOS_UI_GetFriendsVisible(base.InnerHandle, ref getFriendsVisibleOptionsInternal);
			Helper.Dispose<GetFriendsVisibleOptionsInternal>(ref getFriendsVisibleOptionsInternal);
			bool result;
			Helper.Get(from, out result);
			return result;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x000075A8 File Offset: 0x000057A8
		public NotificationLocation GetNotificationLocationPreference()
		{
			return Bindings.EOS_UI_GetNotificationLocationPreference(base.InnerHandle);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000075C8 File Offset: 0x000057C8
		public KeyCombination GetToggleFriendsKey(ref GetToggleFriendsKeyOptions options)
		{
			GetToggleFriendsKeyOptionsInternal getToggleFriendsKeyOptionsInternal = default(GetToggleFriendsKeyOptionsInternal);
			getToggleFriendsKeyOptionsInternal.Set(ref options);
			KeyCombination result = Bindings.EOS_UI_GetToggleFriendsKey(base.InnerHandle, ref getToggleFriendsKeyOptionsInternal);
			Helper.Dispose<GetToggleFriendsKeyOptionsInternal>(ref getToggleFriendsKeyOptionsInternal);
			return result;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00007604 File Offset: 0x00005804
		public void HideFriends(ref HideFriendsOptions options, object clientData, OnHideFriendsCallback completionDelegate)
		{
			HideFriendsOptionsInternal hideFriendsOptionsInternal = default(HideFriendsOptionsInternal);
			hideFriendsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnHideFriendsCallbackInternal onHideFriendsCallbackInternal = new OnHideFriendsCallbackInternal(UIInterface.OnHideFriendsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onHideFriendsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UI_HideFriends(base.InnerHandle, ref hideFriendsOptionsInternal, zero, onHideFriendsCallbackInternal);
			Helper.Dispose<HideFriendsOptionsInternal>(ref hideFriendsOptionsInternal);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00007660 File Offset: 0x00005860
		public bool IsSocialOverlayPaused(ref IsSocialOverlayPausedOptions options)
		{
			IsSocialOverlayPausedOptionsInternal isSocialOverlayPausedOptionsInternal = default(IsSocialOverlayPausedOptionsInternal);
			isSocialOverlayPausedOptionsInternal.Set(ref options);
			int from = Bindings.EOS_UI_IsSocialOverlayPaused(base.InnerHandle, ref isSocialOverlayPausedOptionsInternal);
			Helper.Dispose<IsSocialOverlayPausedOptionsInternal>(ref isSocialOverlayPausedOptionsInternal);
			bool result;
			Helper.Get(from, out result);
			return result;
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x000076A4 File Offset: 0x000058A4
		public bool IsValidKeyCombination(KeyCombination keyCombination)
		{
			int from = Bindings.EOS_UI_IsValidKeyCombination(base.InnerHandle, keyCombination);
			bool result;
			Helper.Get(from, out result);
			return result;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x000076D0 File Offset: 0x000058D0
		public Result PauseSocialOverlay(ref PauseSocialOverlayOptions options)
		{
			PauseSocialOverlayOptionsInternal pauseSocialOverlayOptionsInternal = default(PauseSocialOverlayOptionsInternal);
			pauseSocialOverlayOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_UI_PauseSocialOverlay(base.InnerHandle, ref pauseSocialOverlayOptionsInternal);
			Helper.Dispose<PauseSocialOverlayOptionsInternal>(ref pauseSocialOverlayOptionsInternal);
			return result;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000770A File Offset: 0x0000590A
		public void RemoveNotifyDisplaySettingsUpdated(ulong id)
		{
			Bindings.EOS_UI_RemoveNotifyDisplaySettingsUpdated(base.InnerHandle, id);
			Helper.RemoveCallbackByNotificationId(id);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00007724 File Offset: 0x00005924
		public Result SetDisplayPreference(ref SetDisplayPreferenceOptions options)
		{
			SetDisplayPreferenceOptionsInternal setDisplayPreferenceOptionsInternal = default(SetDisplayPreferenceOptionsInternal);
			setDisplayPreferenceOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_UI_SetDisplayPreference(base.InnerHandle, ref setDisplayPreferenceOptionsInternal);
			Helper.Dispose<SetDisplayPreferenceOptionsInternal>(ref setDisplayPreferenceOptionsInternal);
			return result;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00007760 File Offset: 0x00005960
		public Result SetToggleFriendsKey(ref SetToggleFriendsKeyOptions options)
		{
			SetToggleFriendsKeyOptionsInternal setToggleFriendsKeyOptionsInternal = default(SetToggleFriendsKeyOptionsInternal);
			setToggleFriendsKeyOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_UI_SetToggleFriendsKey(base.InnerHandle, ref setToggleFriendsKeyOptionsInternal);
			Helper.Dispose<SetToggleFriendsKeyOptionsInternal>(ref setToggleFriendsKeyOptionsInternal);
			return result;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000779C File Offset: 0x0000599C
		public void ShowBlockPlayer(ref ShowBlockPlayerOptions options, object clientData, OnShowBlockPlayerCallback completionDelegate)
		{
			ShowBlockPlayerOptionsInternal showBlockPlayerOptionsInternal = default(ShowBlockPlayerOptionsInternal);
			showBlockPlayerOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnShowBlockPlayerCallbackInternal onShowBlockPlayerCallbackInternal = new OnShowBlockPlayerCallbackInternal(UIInterface.OnShowBlockPlayerCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onShowBlockPlayerCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UI_ShowBlockPlayer(base.InnerHandle, ref showBlockPlayerOptionsInternal, zero, onShowBlockPlayerCallbackInternal);
			Helper.Dispose<ShowBlockPlayerOptionsInternal>(ref showBlockPlayerOptionsInternal);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x000077F8 File Offset: 0x000059F8
		public void ShowFriends(ref ShowFriendsOptions options, object clientData, OnShowFriendsCallback completionDelegate)
		{
			ShowFriendsOptionsInternal showFriendsOptionsInternal = default(ShowFriendsOptionsInternal);
			showFriendsOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnShowFriendsCallbackInternal onShowFriendsCallbackInternal = new OnShowFriendsCallbackInternal(UIInterface.OnShowFriendsCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onShowFriendsCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UI_ShowFriends(base.InnerHandle, ref showFriendsOptionsInternal, zero, onShowFriendsCallbackInternal);
			Helper.Dispose<ShowFriendsOptionsInternal>(ref showFriendsOptionsInternal);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00007854 File Offset: 0x00005A54
		public void ShowReportPlayer(ref ShowReportPlayerOptions options, object clientData, OnShowReportPlayerCallback completionDelegate)
		{
			ShowReportPlayerOptionsInternal showReportPlayerOptionsInternal = default(ShowReportPlayerOptionsInternal);
			showReportPlayerOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnShowReportPlayerCallbackInternal onShowReportPlayerCallbackInternal = new OnShowReportPlayerCallbackInternal(UIInterface.OnShowReportPlayerCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, completionDelegate, onShowReportPlayerCallbackInternal, Array.Empty<Delegate>());
			Bindings.EOS_UI_ShowReportPlayer(base.InnerHandle, ref showReportPlayerOptionsInternal, zero, onShowReportPlayerCallbackInternal);
			Helper.Dispose<ShowReportPlayerOptionsInternal>(ref showReportPlayerOptionsInternal);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000078B0 File Offset: 0x00005AB0
		[MonoPInvokeCallback(typeof(OnDisplaySettingsUpdatedCallbackInternal))]
		internal static void OnDisplaySettingsUpdatedCallbackInternalImplementation(ref OnDisplaySettingsUpdatedCallbackInfoInternal data)
		{
			OnDisplaySettingsUpdatedCallback onDisplaySettingsUpdatedCallback;
			OnDisplaySettingsUpdatedCallbackInfo onDisplaySettingsUpdatedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnDisplaySettingsUpdatedCallbackInfoInternal, OnDisplaySettingsUpdatedCallback, OnDisplaySettingsUpdatedCallbackInfo>(ref data, out onDisplaySettingsUpdatedCallback, out onDisplaySettingsUpdatedCallbackInfo);
			if (flag)
			{
				onDisplaySettingsUpdatedCallback(ref onDisplaySettingsUpdatedCallbackInfo);
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000078D8 File Offset: 0x00005AD8
		[MonoPInvokeCallback(typeof(OnHideFriendsCallbackInternal))]
		internal static void OnHideFriendsCallbackInternalImplementation(ref HideFriendsCallbackInfoInternal data)
		{
			OnHideFriendsCallback onHideFriendsCallback;
			HideFriendsCallbackInfo hideFriendsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<HideFriendsCallbackInfoInternal, OnHideFriendsCallback, HideFriendsCallbackInfo>(ref data, out onHideFriendsCallback, out hideFriendsCallbackInfo);
			if (flag)
			{
				onHideFriendsCallback(ref hideFriendsCallbackInfo);
			}
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00007900 File Offset: 0x00005B00
		[MonoPInvokeCallback(typeof(OnShowBlockPlayerCallbackInternal))]
		internal static void OnShowBlockPlayerCallbackInternalImplementation(ref OnShowBlockPlayerCallbackInfoInternal data)
		{
			OnShowBlockPlayerCallback onShowBlockPlayerCallback;
			OnShowBlockPlayerCallbackInfo onShowBlockPlayerCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnShowBlockPlayerCallbackInfoInternal, OnShowBlockPlayerCallback, OnShowBlockPlayerCallbackInfo>(ref data, out onShowBlockPlayerCallback, out onShowBlockPlayerCallbackInfo);
			if (flag)
			{
				onShowBlockPlayerCallback(ref onShowBlockPlayerCallbackInfo);
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00007928 File Offset: 0x00005B28
		[MonoPInvokeCallback(typeof(OnShowFriendsCallbackInternal))]
		internal static void OnShowFriendsCallbackInternalImplementation(ref ShowFriendsCallbackInfoInternal data)
		{
			OnShowFriendsCallback onShowFriendsCallback;
			ShowFriendsCallbackInfo showFriendsCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<ShowFriendsCallbackInfoInternal, OnShowFriendsCallback, ShowFriendsCallbackInfo>(ref data, out onShowFriendsCallback, out showFriendsCallbackInfo);
			if (flag)
			{
				onShowFriendsCallback(ref showFriendsCallbackInfo);
			}
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00007950 File Offset: 0x00005B50
		[MonoPInvokeCallback(typeof(OnShowReportPlayerCallbackInternal))]
		internal static void OnShowReportPlayerCallbackInternalImplementation(ref OnShowReportPlayerCallbackInfoInternal data)
		{
			OnShowReportPlayerCallback onShowReportPlayerCallback;
			OnShowReportPlayerCallbackInfo onShowReportPlayerCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnShowReportPlayerCallbackInfoInternal, OnShowReportPlayerCallback, OnShowReportPlayerCallbackInfo>(ref data, out onShowReportPlayerCallback, out onShowReportPlayerCallbackInfo);
			if (flag)
			{
				onShowReportPlayerCallback(ref onShowReportPlayerCallbackInfo);
			}
		}

		// Token: 0x04000270 RID: 624
		public const int AcknowledgecorrelationidApiLatest = 1;

		// Token: 0x04000271 RID: 625
		public const int AcknowledgeeventidApiLatest = 1;

		// Token: 0x04000272 RID: 626
		public const int AddnotifydisplaysettingsupdatedApiLatest = 1;

		// Token: 0x04000273 RID: 627
		public const int EventidInvalid = 0;

		// Token: 0x04000274 RID: 628
		public const int GetfriendsexclusiveinputApiLatest = 1;

		// Token: 0x04000275 RID: 629
		public const int GetfriendsvisibleApiLatest = 1;

		// Token: 0x04000276 RID: 630
		public const int GettogglefriendskeyApiLatest = 1;

		// Token: 0x04000277 RID: 631
		public const int HidefriendsApiLatest = 1;

		// Token: 0x04000278 RID: 632
		public const int IssocialoverlaypausedApiLatest = 1;

		// Token: 0x04000279 RID: 633
		public const int PausesocialoverlayApiLatest = 1;

		// Token: 0x0400027A RID: 634
		public const int PrepresentApiLatest = 1;

		// Token: 0x0400027B RID: 635
		public const int ReportkeyeventApiLatest = 1;

		// Token: 0x0400027C RID: 636
		public const int SetdisplaypreferenceApiLatest = 1;

		// Token: 0x0400027D RID: 637
		public const int SettogglefriendskeyApiLatest = 1;

		// Token: 0x0400027E RID: 638
		public const int ShowblockplayerApiLatest = 1;

		// Token: 0x0400027F RID: 639
		public const int ShowfriendsApiLatest = 1;

		// Token: 0x04000280 RID: 640
		public const int ShowreportplayerApiLatest = 1;
	}
}
