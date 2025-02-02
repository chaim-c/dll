using System;
using Epic.OnlineServices.Achievements;
using Epic.OnlineServices.AntiCheatClient;
using Epic.OnlineServices.AntiCheatServer;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.CustomInvites;
using Epic.OnlineServices.Ecom;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.KWS;
using Epic.OnlineServices.Leaderboards;
using Epic.OnlineServices.Lobby;
using Epic.OnlineServices.Metrics;
using Epic.OnlineServices.Mods;
using Epic.OnlineServices.P2P;
using Epic.OnlineServices.PlayerDataStorage;
using Epic.OnlineServices.Presence;
using Epic.OnlineServices.ProgressionSnapshot;
using Epic.OnlineServices.Reports;
using Epic.OnlineServices.RTC;
using Epic.OnlineServices.RTCAdmin;
using Epic.OnlineServices.Sanctions;
using Epic.OnlineServices.Sessions;
using Epic.OnlineServices.Stats;
using Epic.OnlineServices.TitleStorage;
using Epic.OnlineServices.UI;
using Epic.OnlineServices.UserInfo;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000645 RID: 1605
	public sealed class PlatformInterface : Handle
	{
		// Token: 0x060028DE RID: 10462 RVA: 0x0003CD7C File Offset: 0x0003AF7C
		public static Result Initialize(ref AndroidInitializeOptions options)
		{
			AndroidInitializeOptionsInternal androidInitializeOptionsInternal = default(AndroidInitializeOptionsInternal);
			androidInitializeOptionsInternal.Set(ref options);
			Result result = AndroidBindings.EOS_Initialize(ref androidInitializeOptionsInternal);
			Helper.Dispose<AndroidInitializeOptionsInternal>(ref androidInitializeOptionsInternal);
			return result;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x0003CDB0 File Offset: 0x0003AFB0
		public PlatformInterface()
		{
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x0003CDBA File Offset: 0x0003AFBA
		public PlatformInterface(IntPtr innerHandle) : base(innerHandle)
		{
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x0003CDC8 File Offset: 0x0003AFC8
		public Result CheckForLauncherAndRestart()
		{
			return Bindings.EOS_Platform_CheckForLauncherAndRestart(base.InnerHandle);
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x0003CDE8 File Offset: 0x0003AFE8
		public static PlatformInterface Create(ref Options options)
		{
			OptionsInternal optionsInternal = default(OptionsInternal);
			optionsInternal.Set(ref options);
			IntPtr from = Bindings.EOS_Platform_Create(ref optionsInternal);
			Helper.Dispose<OptionsInternal>(ref optionsInternal);
			PlatformInterface result;
			Helper.Get<PlatformInterface>(from, out result);
			return result;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0003CE28 File Offset: 0x0003B028
		public AchievementsInterface GetAchievementsInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetAchievementsInterface(base.InnerHandle);
			AchievementsInterface result;
			Helper.Get<AchievementsInterface>(from, out result);
			return result;
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0003CE50 File Offset: 0x0003B050
		public Result GetActiveCountryCode(EpicAccountId localUserId, out Utf8String outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(localUserId, ref zero);
			int size = 5;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Platform_GetActiveCountryCode(base.InnerHandle, zero, intPtr, ref size);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x0003CEA0 File Offset: 0x0003B0A0
		public Result GetActiveLocaleCode(EpicAccountId localUserId, out Utf8String outBuffer)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(localUserId, ref zero);
			int size = 10;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Platform_GetActiveLocaleCode(base.InnerHandle, zero, intPtr, ref size);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x0003CEF0 File Offset: 0x0003B0F0
		public AntiCheatClientInterface GetAntiCheatClientInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetAntiCheatClientInterface(base.InnerHandle);
			AntiCheatClientInterface result;
			Helper.Get<AntiCheatClientInterface>(from, out result);
			return result;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x0003CF18 File Offset: 0x0003B118
		public AntiCheatServerInterface GetAntiCheatServerInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetAntiCheatServerInterface(base.InnerHandle);
			AntiCheatServerInterface result;
			Helper.Get<AntiCheatServerInterface>(from, out result);
			return result;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x0003CF40 File Offset: 0x0003B140
		public ApplicationStatus GetApplicationStatus()
		{
			return Bindings.EOS_Platform_GetApplicationStatus(base.InnerHandle);
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0003CF60 File Offset: 0x0003B160
		public AuthInterface GetAuthInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetAuthInterface(base.InnerHandle);
			AuthInterface result;
			Helper.Get<AuthInterface>(from, out result);
			return result;
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0003CF88 File Offset: 0x0003B188
		public ConnectInterface GetConnectInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetConnectInterface(base.InnerHandle);
			ConnectInterface result;
			Helper.Get<ConnectInterface>(from, out result);
			return result;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x0003CFB0 File Offset: 0x0003B1B0
		public CustomInvitesInterface GetCustomInvitesInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetCustomInvitesInterface(base.InnerHandle);
			CustomInvitesInterface result;
			Helper.Get<CustomInvitesInterface>(from, out result);
			return result;
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0003CFD8 File Offset: 0x0003B1D8
		public Result GetDesktopCrossplayStatus(ref GetDesktopCrossplayStatusOptions options, out GetDesktopCrossplayStatusInfo outDesktopCrossplayStatusInfo)
		{
			GetDesktopCrossplayStatusOptionsInternal getDesktopCrossplayStatusOptionsInternal = default(GetDesktopCrossplayStatusOptionsInternal);
			getDesktopCrossplayStatusOptionsInternal.Set(ref options);
			GetDesktopCrossplayStatusInfoInternal @default = Helper.GetDefault<GetDesktopCrossplayStatusInfoInternal>();
			Result result = Bindings.EOS_Platform_GetDesktopCrossplayStatus(base.InnerHandle, ref getDesktopCrossplayStatusOptionsInternal, ref @default);
			Helper.Dispose<GetDesktopCrossplayStatusOptionsInternal>(ref getDesktopCrossplayStatusOptionsInternal);
			Helper.Get<GetDesktopCrossplayStatusInfoInternal, GetDesktopCrossplayStatusInfo>(ref @default, out outDesktopCrossplayStatusInfo);
			return result;
		}

		// Token: 0x060028ED RID: 10477 RVA: 0x0003D024 File Offset: 0x0003B224
		public EcomInterface GetEcomInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetEcomInterface(base.InnerHandle);
			EcomInterface result;
			Helper.Get<EcomInterface>(from, out result);
			return result;
		}

		// Token: 0x060028EE RID: 10478 RVA: 0x0003D04C File Offset: 0x0003B24C
		public FriendsInterface GetFriendsInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetFriendsInterface(base.InnerHandle);
			FriendsInterface result;
			Helper.Get<FriendsInterface>(from, out result);
			return result;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x0003D074 File Offset: 0x0003B274
		public KWSInterface GetKWSInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetKWSInterface(base.InnerHandle);
			KWSInterface result;
			Helper.Get<KWSInterface>(from, out result);
			return result;
		}

		// Token: 0x060028F0 RID: 10480 RVA: 0x0003D09C File Offset: 0x0003B29C
		public LeaderboardsInterface GetLeaderboardsInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetLeaderboardsInterface(base.InnerHandle);
			LeaderboardsInterface result;
			Helper.Get<LeaderboardsInterface>(from, out result);
			return result;
		}

		// Token: 0x060028F1 RID: 10481 RVA: 0x0003D0C4 File Offset: 0x0003B2C4
		public LobbyInterface GetLobbyInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetLobbyInterface(base.InnerHandle);
			LobbyInterface result;
			Helper.Get<LobbyInterface>(from, out result);
			return result;
		}

		// Token: 0x060028F2 RID: 10482 RVA: 0x0003D0EC File Offset: 0x0003B2EC
		public MetricsInterface GetMetricsInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetMetricsInterface(base.InnerHandle);
			MetricsInterface result;
			Helper.Get<MetricsInterface>(from, out result);
			return result;
		}

		// Token: 0x060028F3 RID: 10483 RVA: 0x0003D114 File Offset: 0x0003B314
		public ModsInterface GetModsInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetModsInterface(base.InnerHandle);
			ModsInterface result;
			Helper.Get<ModsInterface>(from, out result);
			return result;
		}

		// Token: 0x060028F4 RID: 10484 RVA: 0x0003D13C File Offset: 0x0003B33C
		public NetworkStatus GetNetworkStatus()
		{
			return Bindings.EOS_Platform_GetNetworkStatus(base.InnerHandle);
		}

		// Token: 0x060028F5 RID: 10485 RVA: 0x0003D15C File Offset: 0x0003B35C
		public Result GetOverrideCountryCode(out Utf8String outBuffer)
		{
			int size = 5;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Platform_GetOverrideCountryCode(base.InnerHandle, intPtr, ref size);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x0003D198 File Offset: 0x0003B398
		public Result GetOverrideLocaleCode(out Utf8String outBuffer)
		{
			int size = 10;
			IntPtr intPtr = Helper.AddAllocation(size);
			Result result = Bindings.EOS_Platform_GetOverrideLocaleCode(base.InnerHandle, intPtr, ref size);
			Helper.Get(intPtr, out outBuffer);
			Helper.Dispose(ref intPtr);
			return result;
		}

		// Token: 0x060028F7 RID: 10487 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
		public P2PInterface GetP2PInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetP2PInterface(base.InnerHandle);
			P2PInterface result;
			Helper.Get<P2PInterface>(from, out result);
			return result;
		}

		// Token: 0x060028F8 RID: 10488 RVA: 0x0003D1FC File Offset: 0x0003B3FC
		public PlayerDataStorageInterface GetPlayerDataStorageInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetPlayerDataStorageInterface(base.InnerHandle);
			PlayerDataStorageInterface result;
			Helper.Get<PlayerDataStorageInterface>(from, out result);
			return result;
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x0003D224 File Offset: 0x0003B424
		public PresenceInterface GetPresenceInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetPresenceInterface(base.InnerHandle);
			PresenceInterface result;
			Helper.Get<PresenceInterface>(from, out result);
			return result;
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x0003D24C File Offset: 0x0003B44C
		public ProgressionSnapshotInterface GetProgressionSnapshotInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetProgressionSnapshotInterface(base.InnerHandle);
			ProgressionSnapshotInterface result;
			Helper.Get<ProgressionSnapshotInterface>(from, out result);
			return result;
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x0003D274 File Offset: 0x0003B474
		public RTCAdminInterface GetRTCAdminInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetRTCAdminInterface(base.InnerHandle);
			RTCAdminInterface result;
			Helper.Get<RTCAdminInterface>(from, out result);
			return result;
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x0003D29C File Offset: 0x0003B49C
		public RTCInterface GetRTCInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetRTCInterface(base.InnerHandle);
			RTCInterface result;
			Helper.Get<RTCInterface>(from, out result);
			return result;
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
		public ReportsInterface GetReportsInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetReportsInterface(base.InnerHandle);
			ReportsInterface result;
			Helper.Get<ReportsInterface>(from, out result);
			return result;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x0003D2EC File Offset: 0x0003B4EC
		public SanctionsInterface GetSanctionsInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetSanctionsInterface(base.InnerHandle);
			SanctionsInterface result;
			Helper.Get<SanctionsInterface>(from, out result);
			return result;
		}

		// Token: 0x060028FF RID: 10495 RVA: 0x0003D314 File Offset: 0x0003B514
		public SessionsInterface GetSessionsInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetSessionsInterface(base.InnerHandle);
			SessionsInterface result;
			Helper.Get<SessionsInterface>(from, out result);
			return result;
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x0003D33C File Offset: 0x0003B53C
		public StatsInterface GetStatsInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetStatsInterface(base.InnerHandle);
			StatsInterface result;
			Helper.Get<StatsInterface>(from, out result);
			return result;
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0003D364 File Offset: 0x0003B564
		public TitleStorageInterface GetTitleStorageInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetTitleStorageInterface(base.InnerHandle);
			TitleStorageInterface result;
			Helper.Get<TitleStorageInterface>(from, out result);
			return result;
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0003D38C File Offset: 0x0003B58C
		public UIInterface GetUIInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetUIInterface(base.InnerHandle);
			UIInterface result;
			Helper.Get<UIInterface>(from, out result);
			return result;
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x0003D3B4 File Offset: 0x0003B5B4
		public UserInfoInterface GetUserInfoInterface()
		{
			IntPtr from = Bindings.EOS_Platform_GetUserInfoInterface(base.InnerHandle);
			UserInfoInterface result;
			Helper.Get<UserInfoInterface>(from, out result);
			return result;
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0003D3DC File Offset: 0x0003B5DC
		public static Result Initialize(ref InitializeOptions options)
		{
			InitializeOptionsInternal initializeOptionsInternal = default(InitializeOptionsInternal);
			initializeOptionsInternal.Set(ref options);
			Result result = Bindings.EOS_Initialize(ref initializeOptionsInternal);
			Helper.Dispose<InitializeOptionsInternal>(ref initializeOptionsInternal);
			return result;
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0003D410 File Offset: 0x0003B610
		public void Release()
		{
			Bindings.EOS_Platform_Release(base.InnerHandle);
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x0003D420 File Offset: 0x0003B620
		public Result SetApplicationStatus(ApplicationStatus newStatus)
		{
			return Bindings.EOS_Platform_SetApplicationStatus(base.InnerHandle, newStatus);
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0003D440 File Offset: 0x0003B640
		public Result SetNetworkStatus(NetworkStatus newStatus)
		{
			return Bindings.EOS_Platform_SetNetworkStatus(base.InnerHandle, newStatus);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0003D460 File Offset: 0x0003B660
		public Result SetOverrideCountryCode(Utf8String newCountryCode)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(newCountryCode, ref zero);
			Result result = Bindings.EOS_Platform_SetOverrideCountryCode(base.InnerHandle, zero);
			Helper.Dispose(ref zero);
			return result;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x0003D498 File Offset: 0x0003B698
		public Result SetOverrideLocaleCode(Utf8String newLocaleCode)
		{
			IntPtr zero = IntPtr.Zero;
			Helper.Set(newLocaleCode, ref zero);
			Result result = Bindings.EOS_Platform_SetOverrideLocaleCode(base.InnerHandle, zero);
			Helper.Dispose(ref zero);
			return result;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x0003D4D0 File Offset: 0x0003B6D0
		public static Result Shutdown()
		{
			return Bindings.EOS_Shutdown();
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x0003D4E9 File Offset: 0x0003B6E9
		public void Tick()
		{
			Bindings.EOS_Platform_Tick(base.InnerHandle);
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x0003D4F8 File Offset: 0x0003B6F8
		public static PlatformInterface Create(ref WindowsOptions options)
		{
			WindowsOptionsInternal windowsOptionsInternal = default(WindowsOptionsInternal);
			windowsOptionsInternal.Set(ref options);
			IntPtr from = WindowsBindings.EOS_Platform_Create(ref windowsOptionsInternal);
			Helper.Dispose<WindowsOptionsInternal>(ref windowsOptionsInternal);
			PlatformInterface result;
			Helper.Get<PlatformInterface>(from, out result);
			return result;
		}

		// Token: 0x04001273 RID: 4723
		public const int AndroidInitializeoptionssysteminitializeoptionsApiLatest = 2;

		// Token: 0x04001274 RID: 4724
		public const int CountrycodeMaxBufferLen = 5;

		// Token: 0x04001275 RID: 4725
		public const int CountrycodeMaxLength = 4;

		// Token: 0x04001276 RID: 4726
		public const int GetdesktopcrossplaystatusApiLatest = 1;

		// Token: 0x04001277 RID: 4727
		public const int InitializeApiLatest = 4;

		// Token: 0x04001278 RID: 4728
		public const int InitializeThreadaffinityApiLatest = 2;

		// Token: 0x04001279 RID: 4729
		public const int LocalecodeMaxBufferLen = 10;

		// Token: 0x0400127A RID: 4730
		public const int LocalecodeMaxLength = 9;

		// Token: 0x0400127B RID: 4731
		public const int OptionsApiLatest = 12;

		// Token: 0x0400127C RID: 4732
		public const int RtcoptionsApiLatest = 1;

		// Token: 0x0400127D RID: 4733
		public const int WindowsRtcoptionsplatformspecificoptionsApiLatest = 1;
	}
}
