using System;
using Epic.OnlineServices.AntiCheatCommon;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005BD RID: 1469
	public sealed class AntiCheatServerInterface : Handle
	{
		// Token: 0x060025BF RID: 9663 RVA: 0x0003804C File Offset: 0x0003624C
		public AntiCheatServerInterface()
		{
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x00038056 File Offset: 0x00036256
		public AntiCheatServerInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00038064 File Offset: 0x00036264
		public ulong AddNotifyClientActionRequired(ref AddNotifyClientActionRequiredOptions options, object clientData, OnClientActionRequiredCallback notificationFn)
		{
			AddNotifyClientActionRequiredOptionsInternal addNotifyClientActionRequiredOptionsInternal = default(AddNotifyClientActionRequiredOptionsInternal);
			addNotifyClientActionRequiredOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnClientActionRequiredCallbackInternal onClientActionRequiredCallbackInternal = new OnClientActionRequiredCallbackInternal(AntiCheatServerInterface.OnClientActionRequiredCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onClientActionRequiredCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatServer_AddNotifyClientActionRequired(base.InnerHandle, ref addNotifyClientActionRequiredOptionsInternal, zero, onClientActionRequiredCallbackInternal);
			Helper.Dispose<AddNotifyClientActionRequiredOptionsInternal>(ref addNotifyClientActionRequiredOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x000380D0 File Offset: 0x000362D0
		public ulong AddNotifyClientAuthStatusChanged(ref AddNotifyClientAuthStatusChangedOptions options, object clientData, OnClientAuthStatusChangedCallback notificationFn)
		{
			AddNotifyClientAuthStatusChangedOptionsInternal addNotifyClientAuthStatusChangedOptionsInternal = default(AddNotifyClientAuthStatusChangedOptionsInternal);
			addNotifyClientAuthStatusChangedOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnClientAuthStatusChangedCallbackInternal onClientAuthStatusChangedCallbackInternal = new OnClientAuthStatusChangedCallbackInternal(AntiCheatServerInterface.OnClientAuthStatusChangedCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onClientAuthStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatServer_AddNotifyClientAuthStatusChanged(base.InnerHandle, ref addNotifyClientAuthStatusChangedOptionsInternal, zero, onClientAuthStatusChangedCallbackInternal);
			Helper.Dispose<AddNotifyClientAuthStatusChangedOptionsInternal>(ref addNotifyClientAuthStatusChangedOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0003813C File Offset: 0x0003633C
		public ulong AddNotifyMessageToClient(ref AddNotifyMessageToClientOptions options, object clientData, OnMessageToClientCallback notificationFn)
		{
			AddNotifyMessageToClientOptionsInternal addNotifyMessageToClientOptionsInternal = default(AddNotifyMessageToClientOptionsInternal);
			addNotifyMessageToClientOptionsInternal.Set(ref options);
			IntPtr zero = IntPtr.Zero;
			OnMessageToClientCallbackInternal onMessageToClientCallbackInternal = new OnMessageToClientCallbackInternal(AntiCheatServerInterface.OnMessageToClientCallbackInternalImplementation);
			Helper.AddCallback(out zero, clientData, notificationFn, onMessageToClientCallbackInternal, Array.Empty<Delegate>());
			ulong num = Bindings.EOS_AntiCheatServer_AddNotifyMessageToClient(base.InnerHandle, ref addNotifyMessageToClientOptionsInternal, zero, onMessageToClientCallbackInternal);
			Helper.Dispose<AddNotifyMessageToClientOptionsInternal>(ref addNotifyMessageToClientOptionsInternal);
			Helper.AssignNotificationIdToCallback(zero, num);
			return num;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x000381A8 File Offset: 0x000363A8
		public Result BeginSession(ref BeginSessionOptions options)
		{
			BeginSessionOptionsInternal beginSessionOptionsInternal = default(BeginSessionOptionsInternal);
			beginSessionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_BeginSession(base.InnerHandle, ref beginSessionOptionsInternal);
			Helper.Dispose<BeginSessionOptionsInternal>(ref beginSessionOptionsInternal);
			return result;
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x000381E4 File Offset: 0x000363E4
		public Result EndSession(ref EndSessionOptions options)
		{
			EndSessionOptionsInternal endSessionOptionsInternal = default(EndSessionOptionsInternal);
			endSessionOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_EndSession(base.InnerHandle, ref endSessionOptionsInternal);
			Helper.Dispose<EndSessionOptionsInternal>(ref endSessionOptionsInternal);
			return result;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x00038220 File Offset: 0x00036420
		public Result GetProtectMessageOutputLength(ref GetProtectMessageOutputLengthOptions options, out uint outBufferSizeBytes)
		{
			GetProtectMessageOutputLengthOptionsInternal getProtectMessageOutputLengthOptionsInternal = default(GetProtectMessageOutputLengthOptionsInternal);
			getProtectMessageOutputLengthOptionsInternal.Set(ref options);
			outBufferSizeBytes = Helper.GetDefault<uint>();
			Result result = Bindings.EOS_AntiCheatServer_GetProtectMessageOutputLength(base.InnerHandle, ref getProtectMessageOutputLengthOptionsInternal, ref outBufferSizeBytes);
			Helper.Dispose<GetProtectMessageOutputLengthOptionsInternal>(ref getProtectMessageOutputLengthOptionsInternal);
			return result;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x00038264 File Offset: 0x00036464
		public Result LogEvent(ref LogEventOptions options)
		{
			LogEventOptionsInternal logEventOptionsInternal = default(LogEventOptionsInternal);
			logEventOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogEvent(base.InnerHandle, ref logEventOptionsInternal);
			Helper.Dispose<LogEventOptionsInternal>(ref logEventOptionsInternal);
			return result;
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x000382A0 File Offset: 0x000364A0
		public Result LogGameRoundEnd(ref LogGameRoundEndOptions options)
		{
			LogGameRoundEndOptionsInternal logGameRoundEndOptionsInternal = default(LogGameRoundEndOptionsInternal);
			logGameRoundEndOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogGameRoundEnd(base.InnerHandle, ref logGameRoundEndOptionsInternal);
			Helper.Dispose<LogGameRoundEndOptionsInternal>(ref logGameRoundEndOptionsInternal);
			return result;
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000382DC File Offset: 0x000364DC
		public Result LogGameRoundStart(ref LogGameRoundStartOptions options)
		{
			LogGameRoundStartOptionsInternal logGameRoundStartOptionsInternal = default(LogGameRoundStartOptionsInternal);
			logGameRoundStartOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogGameRoundStart(base.InnerHandle, ref logGameRoundStartOptionsInternal);
			Helper.Dispose<LogGameRoundStartOptionsInternal>(ref logGameRoundStartOptionsInternal);
			return result;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x00038318 File Offset: 0x00036518
		public Result LogPlayerDespawn(ref LogPlayerDespawnOptions options)
		{
			LogPlayerDespawnOptionsInternal logPlayerDespawnOptionsInternal = default(LogPlayerDespawnOptionsInternal);
			logPlayerDespawnOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerDespawn(base.InnerHandle, ref logPlayerDespawnOptionsInternal);
			Helper.Dispose<LogPlayerDespawnOptionsInternal>(ref logPlayerDespawnOptionsInternal);
			return result;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x00038354 File Offset: 0x00036554
		public Result LogPlayerRevive(ref LogPlayerReviveOptions options)
		{
			LogPlayerReviveOptionsInternal logPlayerReviveOptionsInternal = default(LogPlayerReviveOptionsInternal);
			logPlayerReviveOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerRevive(base.InnerHandle, ref logPlayerReviveOptionsInternal);
			Helper.Dispose<LogPlayerReviveOptionsInternal>(ref logPlayerReviveOptionsInternal);
			return result;
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00038390 File Offset: 0x00036590
		public Result LogPlayerSpawn(ref LogPlayerSpawnOptions options)
		{
			LogPlayerSpawnOptionsInternal logPlayerSpawnOptionsInternal = default(LogPlayerSpawnOptionsInternal);
			logPlayerSpawnOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerSpawn(base.InnerHandle, ref logPlayerSpawnOptionsInternal);
			Helper.Dispose<LogPlayerSpawnOptionsInternal>(ref logPlayerSpawnOptionsInternal);
			return result;
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x000383CC File Offset: 0x000365CC
		public Result LogPlayerTakeDamage(ref LogPlayerTakeDamageOptions options)
		{
			LogPlayerTakeDamageOptionsInternal logPlayerTakeDamageOptionsInternal = default(LogPlayerTakeDamageOptionsInternal);
			logPlayerTakeDamageOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerTakeDamage(base.InnerHandle, ref logPlayerTakeDamageOptionsInternal);
			Helper.Dispose<LogPlayerTakeDamageOptionsInternal>(ref logPlayerTakeDamageOptionsInternal);
			return result;
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x00038408 File Offset: 0x00036608
		public Result LogPlayerTick(ref LogPlayerTickOptions options)
		{
			LogPlayerTickOptionsInternal logPlayerTickOptionsInternal = default(LogPlayerTickOptionsInternal);
			logPlayerTickOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerTick(base.InnerHandle, ref logPlayerTickOptionsInternal);
			Helper.Dispose<LogPlayerTickOptionsInternal>(ref logPlayerTickOptionsInternal);
			return result;
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x00038444 File Offset: 0x00036644
		public Result LogPlayerUseAbility(ref LogPlayerUseAbilityOptions options)
		{
			LogPlayerUseAbilityOptionsInternal logPlayerUseAbilityOptionsInternal = default(LogPlayerUseAbilityOptionsInternal);
			logPlayerUseAbilityOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerUseAbility(base.InnerHandle, ref logPlayerUseAbilityOptionsInternal);
			Helper.Dispose<LogPlayerUseAbilityOptionsInternal>(ref logPlayerUseAbilityOptionsInternal);
			return result;
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x00038480 File Offset: 0x00036680
		public Result LogPlayerUseWeapon(ref LogPlayerUseWeaponOptions options)
		{
			LogPlayerUseWeaponOptionsInternal logPlayerUseWeaponOptionsInternal = default(LogPlayerUseWeaponOptionsInternal);
			logPlayerUseWeaponOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_LogPlayerUseWeapon(base.InnerHandle, ref logPlayerUseWeaponOptionsInternal);
			Helper.Dispose<LogPlayerUseWeaponOptionsInternal>(ref logPlayerUseWeaponOptionsInternal);
			return result;
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x000384BC File Offset: 0x000366BC
		public Result ProtectMessage(ref ProtectMessageOptions options, ArraySegment<byte> outBuffer, out uint outBytesWritten)
		{
			ProtectMessageOptionsInternal protectMessageOptionsInternal = default(ProtectMessageOptionsInternal);
			protectMessageOptionsInternal.Set(ref options);
			outBytesWritten = 0U;
			IntPtr outBuffer2 = Helper.AddPinnedBuffer(outBuffer);
			Result result = Bindings.EOS_AntiCheatServer_ProtectMessage(base.InnerHandle, ref protectMessageOptionsInternal, outBuffer2, ref outBytesWritten);
			Helper.Dispose<ProtectMessageOptionsInternal>(ref protectMessageOptionsInternal);
			Helper.Dispose(ref outBuffer2);
			return result;
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0003850C File Offset: 0x0003670C
		public Result ReceiveMessageFromClient(ref ReceiveMessageFromClientOptions options)
		{
			ReceiveMessageFromClientOptionsInternal receiveMessageFromClientOptionsInternal = default(ReceiveMessageFromClientOptionsInternal);
			receiveMessageFromClientOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_ReceiveMessageFromClient(base.InnerHandle, ref receiveMessageFromClientOptionsInternal);
			Helper.Dispose<ReceiveMessageFromClientOptionsInternal>(ref receiveMessageFromClientOptionsInternal);
			return result;
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x00038548 File Offset: 0x00036748
		public Result RegisterClient(ref RegisterClientOptions options)
		{
			RegisterClientOptionsInternal registerClientOptionsInternal = default(RegisterClientOptionsInternal);
			registerClientOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_RegisterClient(base.InnerHandle, ref registerClientOptionsInternal);
			Helper.Dispose<RegisterClientOptionsInternal>(ref registerClientOptionsInternal);
			return result;
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00038584 File Offset: 0x00036784
		public Result RegisterEvent(ref RegisterEventOptions options)
		{
			RegisterEventOptionsInternal registerEventOptionsInternal = default(RegisterEventOptionsInternal);
			registerEventOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_RegisterEvent(base.InnerHandle, ref registerEventOptionsInternal);
			Helper.Dispose<RegisterEventOptionsInternal>(ref registerEventOptionsInternal);
			return result;
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000385BE File Offset: 0x000367BE
		public void RemoveNotifyClientActionRequired(ulong notificationId)
		{
			Bindings.EOS_AntiCheatServer_RemoveNotifyClientActionRequired(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x000385D5 File Offset: 0x000367D5
		public void RemoveNotifyClientAuthStatusChanged(ulong notificationId)
		{
			Bindings.EOS_AntiCheatServer_RemoveNotifyClientAuthStatusChanged(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x000385EC File Offset: 0x000367EC
		public void RemoveNotifyMessageToClient(ulong notificationId)
		{
			Bindings.EOS_AntiCheatServer_RemoveNotifyMessageToClient(base.InnerHandle, notificationId);
			Helper.RemoveCallbackByNotificationId(notificationId);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x00038604 File Offset: 0x00036804
		public Result SetClientDetails(ref SetClientDetailsOptions options)
		{
			SetClientDetailsOptionsInternal setClientDetailsOptionsInternal = default(SetClientDetailsOptionsInternal);
			setClientDetailsOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_SetClientDetails(base.InnerHandle, ref setClientDetailsOptionsInternal);
			Helper.Dispose<SetClientDetailsOptionsInternal>(ref setClientDetailsOptionsInternal);
			return result;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x00038640 File Offset: 0x00036840
		public Result SetClientNetworkState(ref SetClientNetworkStateOptions options)
		{
			SetClientNetworkStateOptionsInternal setClientNetworkStateOptionsInternal = default(SetClientNetworkStateOptionsInternal);
			setClientNetworkStateOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_SetClientNetworkState(base.InnerHandle, ref setClientNetworkStateOptionsInternal);
			Helper.Dispose<SetClientNetworkStateOptionsInternal>(ref setClientNetworkStateOptionsInternal);
			return result;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0003867C File Offset: 0x0003687C
		public Result SetGameSessionId(ref SetGameSessionIdOptions options)
		{
			SetGameSessionIdOptionsInternal setGameSessionIdOptionsInternal = default(SetGameSessionIdOptionsInternal);
			setGameSessionIdOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_SetGameSessionId(base.InnerHandle, ref setGameSessionIdOptionsInternal);
			Helper.Dispose<SetGameSessionIdOptionsInternal>(ref setGameSessionIdOptionsInternal);
			return result;
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x000386B8 File Offset: 0x000368B8
		public Result UnprotectMessage(ref UnprotectMessageOptions options, ArraySegment<byte> outBuffer, out uint outBytesWritten)
		{
			UnprotectMessageOptionsInternal unprotectMessageOptionsInternal = default(UnprotectMessageOptionsInternal);
			unprotectMessageOptionsInternal.Set(ref options);
			outBytesWritten = 0U;
			IntPtr outBuffer2 = Helper.AddPinnedBuffer(outBuffer);
			Result result = Bindings.EOS_AntiCheatServer_UnprotectMessage(base.InnerHandle, ref unprotectMessageOptionsInternal, outBuffer2, ref outBytesWritten);
			Helper.Dispose<UnprotectMessageOptionsInternal>(ref unprotectMessageOptionsInternal);
			Helper.Dispose(ref outBuffer2);
			return result;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x00038708 File Offset: 0x00036908
		public Result UnregisterClient(ref UnregisterClientOptions options)
		{
			UnregisterClientOptionsInternal unregisterClientOptionsInternal = default(UnregisterClientOptionsInternal);
			unregisterClientOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_AntiCheatServer_UnregisterClient(base.InnerHandle, ref unregisterClientOptionsInternal);
			Helper.Dispose<UnregisterClientOptionsInternal>(ref unregisterClientOptionsInternal);
			return result;
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00038744 File Offset: 0x00036944
		[MonoPInvokeCallback(typeof(OnClientActionRequiredCallbackInternal))]
		internal static void OnClientActionRequiredCallbackInternalImplementation(ref OnClientActionRequiredCallbackInfoInternal data)
		{
			OnClientActionRequiredCallback onClientActionRequiredCallback;
			OnClientActionRequiredCallbackInfo onClientActionRequiredCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnClientActionRequiredCallbackInfoInternal, OnClientActionRequiredCallback, OnClientActionRequiredCallbackInfo>(ref data, out onClientActionRequiredCallback, out onClientActionRequiredCallbackInfo);
			if (flag)
			{
				onClientActionRequiredCallback(ref onClientActionRequiredCallbackInfo);
			}
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0003876C File Offset: 0x0003696C
		[MonoPInvokeCallback(typeof(OnClientAuthStatusChangedCallbackInternal))]
		internal static void OnClientAuthStatusChangedCallbackInternalImplementation(ref OnClientAuthStatusChangedCallbackInfoInternal data)
		{
			OnClientAuthStatusChangedCallback onClientAuthStatusChangedCallback;
			OnClientAuthStatusChangedCallbackInfo onClientAuthStatusChangedCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnClientAuthStatusChangedCallbackInfoInternal, OnClientAuthStatusChangedCallback, OnClientAuthStatusChangedCallbackInfo>(ref data, out onClientAuthStatusChangedCallback, out onClientAuthStatusChangedCallbackInfo);
			if (flag)
			{
				onClientAuthStatusChangedCallback(ref onClientAuthStatusChangedCallbackInfo);
			}
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00038794 File Offset: 0x00036994
		[MonoPInvokeCallback(typeof(OnMessageToClientCallbackInternal))]
		internal static void OnMessageToClientCallbackInternalImplementation(ref OnMessageToClientCallbackInfoInternal data)
		{
			OnMessageToClientCallback onMessageToClientCallback;
			OnMessageToClientCallbackInfo onMessageToClientCallbackInfo;
			bool flag = Helper.TryGetAndRemoveCallback<OnMessageToClientCallbackInfoInternal, OnMessageToClientCallback, OnMessageToClientCallbackInfo>(ref data, out onMessageToClientCallback, out onMessageToClientCallbackInfo);
			if (flag)
			{
				onMessageToClientCallback(ref onMessageToClientCallbackInfo);
			}
		}

		// Token: 0x04001089 RID: 4233
		public const int AddnotifyclientactionrequiredApiLatest = 1;

		// Token: 0x0400108A RID: 4234
		public const int AddnotifyclientauthstatuschangedApiLatest = 1;

		// Token: 0x0400108B RID: 4235
		public const int AddnotifymessagetoclientApiLatest = 1;

		// Token: 0x0400108C RID: 4236
		public const int BeginsessionApiLatest = 3;

		// Token: 0x0400108D RID: 4237
		public const int BeginsessionMaxRegistertimeout = 120;

		// Token: 0x0400108E RID: 4238
		public const int BeginsessionMinRegistertimeout = 10;

		// Token: 0x0400108F RID: 4239
		public const int EndsessionApiLatest = 1;

		// Token: 0x04001090 RID: 4240
		public const int GetprotectmessageoutputlengthApiLatest = 1;

		// Token: 0x04001091 RID: 4241
		public const int ProtectmessageApiLatest = 1;

		// Token: 0x04001092 RID: 4242
		public const int ReceivemessagefromclientApiLatest = 1;

		// Token: 0x04001093 RID: 4243
		public const int RegisterclientApiLatest = 2;

		// Token: 0x04001094 RID: 4244
		public const int SetclientnetworkstateApiLatest = 1;

		// Token: 0x04001095 RID: 4245
		public const int UnprotectmessageApiLatest = 1;

		// Token: 0x04001096 RID: 4246
		public const int UnregisterclientApiLatest = 1;
	}
}
