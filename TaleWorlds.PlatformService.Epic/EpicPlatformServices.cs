using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epic.OnlineServices;
using Epic.OnlineServices.Achievements;
using Epic.OnlineServices.Auth;
using Epic.OnlineServices.Connect;
using Epic.OnlineServices.Friends;
using Epic.OnlineServices.Platform;
using Epic.OnlineServices.Presence;
using Epic.OnlineServices.Stats;
using Epic.OnlineServices.UserInfo;
using Newtonsoft.Json;
using TaleWorlds.AchievementSystem;
using TaleWorlds.ActivitySystem;
using TaleWorlds.Avatar.PlayerServices;
using TaleWorlds.Diamond;
using TaleWorlds.Diamond.AccessProvider.Epic;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.PlayerServices;
using TaleWorlds.PlayerServices.Avatar;

namespace TaleWorlds.PlatformService.Epic
{
	// Token: 0x02000005 RID: 5
	public class EpicPlatformServices : IPlatformServices
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002397 File Offset: 0x00000597
		private static EpicPlatformServices Instance
		{
			get
			{
				return PlatformServices.Instance as EpicPlatformServices;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000023A3 File Offset: 0x000005A3
		public string UserId
		{
			get
			{
				if (this._epicAccountId == null)
				{
					return "";
				}
				return this._epicAccountId.ToString();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023C4 File Offset: 0x000005C4
		string IPlatformServices.UserDisplayName
		{
			get
			{
				return this._epicUserName;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023CC File Offset: 0x000005CC
		IReadOnlyCollection<PlayerId> IPlatformServices.BlockedUsers
		{
			get
			{
				return new List<PlayerId>();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023D3 File Offset: 0x000005D3
		bool IPlatformServices.IsPermanentMuteAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023D6 File Offset: 0x000005D6
		public EpicPlatformServices(PlatformInitParams initParams)
		{
			this._initParams = initParams;
			AvatarServices.AddAvatarService(PlayerIdProvidedTypes.Epic, new EpicPlatformAvatarService());
			this._epicFriendListService = new EpicFriendListService(this);
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000029 RID: 41 RVA: 0x00002414 File Offset: 0x00000614
		// (remove) Token: 0x0600002A RID: 42 RVA: 0x0000244C File Offset: 0x0000064C
		public event Action<AvatarData> OnAvatarUpdated;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600002B RID: 43 RVA: 0x00002484 File Offset: 0x00000684
		// (remove) Token: 0x0600002C RID: 44 RVA: 0x000024BC File Offset: 0x000006BC
		public event Action<string> OnNameUpdated;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600002D RID: 45 RVA: 0x000024F4 File Offset: 0x000006F4
		// (remove) Token: 0x0600002E RID: 46 RVA: 0x0000252C File Offset: 0x0000072C
		public event Action<bool, TextObject> OnSignInStateUpdated;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600002F RID: 47 RVA: 0x00002564 File Offset: 0x00000764
		// (remove) Token: 0x06000030 RID: 48 RVA: 0x0000259C File Offset: 0x0000079C
		public event Action OnBlockedUserListUpdated;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000031 RID: 49 RVA: 0x000025D4 File Offset: 0x000007D4
		// (remove) Token: 0x06000032 RID: 50 RVA: 0x0000260C File Offset: 0x0000080C
		public event Action<string> OnTextEnteredFromPlatform;

		// Token: 0x06000033 RID: 51 RVA: 0x00002644 File Offset: 0x00000844
		public bool Initialize(IFriendListService[] additionalFriendListServices)
		{
			this._friendListServices = new IFriendListService[additionalFriendListServices.Length + 1];
			this._friendListServices[0] = this._epicFriendListService;
			for (int i = 0; i < additionalFriendListServices.Length; i++)
			{
				this._friendListServices[i + 1] = additionalFriendListServices[i];
			}
			InitializeOptions initializeOptions = default(InitializeOptions);
			initializeOptions.ProductName = "Bannerlord";
			initializeOptions.ProductVersion = "1.2.11.45697";
			Result result = PlatformInterface.Initialize(ref initializeOptions);
			if (result != Result.Success)
			{
				this._initFailReason = new TextObject("{=BJ1626h7}Epic platform initialization failed: {FAILREASON}.", null);
				this._initFailReason.SetTextVariable("FAILREASON", result.ToString());
				Debug.Print("Epic PlatformInterface.Initialize Failed:" + result, 0, Debug.DebugColor.White, 17592186044416UL);
				return false;
			}
			ClientCredentials clientCredentials = new ClientCredentials
			{
				ClientId = "e2cf3228b2914793b9a5e5570bad92b3",
				ClientSecret = "Fk5W1E6t1zExNqEUfjyNZinYrkDcDTA63sf5MfyDbQG4"
			};
			Options options = new Options
			{
				ProductId = "6372ed7350f34ffc9ace219dff4b9f40",
				SandboxId = "aeac94c7a11048758064b82f8f8d20ed",
				ClientCredentials = clientCredentials,
				IsServer = false,
				DeploymentId = "e77799aa8a5143f199b2cda9937a133f"
			};
			this._platform = PlatformInterface.Create(ref options);
			AddNotifyFriendsUpdateOptions addNotifyFriendsUpdateOptions = default(AddNotifyFriendsUpdateOptions);
			this._platform.GetFriendsInterface().AddNotifyFriendsUpdate(ref addNotifyFriendsUpdateOptions, null, delegate(ref OnFriendsUpdateInfo callbackInfo)
			{
				this._epicFriendListService.UserStatusChanged(EpicPlatformServices.EpicAccountIdToPlayerId(callbackInfo.TargetUserId));
			});
			Epic.OnlineServices.Auth.Credentials value = new Epic.OnlineServices.Auth.Credentials
			{
				Type = LoginCredentialType.ExchangeCode,
				Token = this.ExchangeCode
			};
			bool failed = false;
			Epic.OnlineServices.Auth.LoginOptions loginOptions = new Epic.OnlineServices.Auth.LoginOptions
			{
				Credentials = new Epic.OnlineServices.Auth.Credentials?(value)
			};
			this._platform.GetAuthInterface().Login(ref loginOptions, null, delegate(ref Epic.OnlineServices.Auth.LoginCallbackInfo callbackInfo)
			{
				if (callbackInfo.ResultCode != Result.Success)
				{
					failed = true;
					Debug.Print("Epic AuthInterface.Login Failed:" + callbackInfo.ResultCode, 0, Debug.DebugColor.White, 17592186044416UL);
					return;
				}
				EpicAccountId epicAccountId = callbackInfo.LocalUserId;
				QueryUserInfoOptions queryUserInfoOptions = new QueryUserInfoOptions
				{
					LocalUserId = epicAccountId,
					TargetUserId = epicAccountId
				};
				this._platform.GetUserInfoInterface().QueryUserInfo(ref queryUserInfoOptions, null, delegate(ref QueryUserInfoCallbackInfo queryCallbackInfo)
				{
					if (queryCallbackInfo.ResultCode != Result.Success)
					{
						failed = true;
						Debug.Print("Epic UserInfoInterface.QueryUserInfo Failed:" + queryCallbackInfo.ResultCode, 0, Debug.DebugColor.White, 17592186044416UL);
						return;
					}
					CopyUserInfoOptions copyUserInfoOptions = new CopyUserInfoOptions
					{
						LocalUserId = epicAccountId,
						TargetUserId = epicAccountId
					};
					UserInfoData? userInfoData;
					this._platform.GetUserInfoInterface().CopyUserInfo(ref copyUserInfoOptions, out userInfoData);
					this._epicUserName = (((userInfoData != null) ? userInfoData.GetValueOrDefault().DisplayName : null) ?? "");
					this._epicAccountId = epicAccountId;
				});
			});
			while (this._epicAccountId == null && !failed)
			{
				this._platform.Tick();
			}
			if (failed)
			{
				this._initFailReason = new TextObject("{=KoKdRd1u}Could not login to Epic", null);
				return false;
			}
			return this.Connect();
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000287F File Offset: 0x00000A7F
		private string ExchangeCode
		{
			get
			{
				return (string)this._initParams["ExchangeCode"];
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002898 File Offset: 0x00000A98
		private void Dummy()
		{
			if (this.OnAvatarUpdated != null)
			{
				this.OnAvatarUpdated(null);
			}
			if (this.OnNameUpdated != null)
			{
				this.OnNameUpdated(null);
			}
			if (this.OnSignInStateUpdated != null)
			{
				this.OnSignInStateUpdated(false, null);
			}
			if (this.OnBlockedUserListUpdated != null)
			{
				this.OnBlockedUserListUpdated();
			}
			if (this.OnTextEnteredFromPlatform != null)
			{
				this.OnTextEnteredFromPlatform(null);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000290C File Offset: 0x00000B0C
		private void RefreshConnection(ref AuthExpirationCallbackInfo clientData)
		{
			try
			{
				this.Connect();
			}
			catch (Exception ex)
			{
				Debug.Print("RefreshConnection:" + ex.Message + " " + Environment.StackTrace, 5, Debug.DebugColor.White, 17592186044416UL);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002960 File Offset: 0x00000B60
		private bool Connect()
		{
			bool failed = false;
			CopyUserAuthTokenOptions copyUserAuthTokenOptions = default(CopyUserAuthTokenOptions);
			Token? token;
			this._platform.GetAuthInterface().CopyUserAuthToken(ref copyUserAuthTokenOptions, this._epicAccountId, out token);
			if (token == null)
			{
				this._initFailReason = new TextObject("{=oGIdsL8h}Could not retrieve token", null);
				return false;
			}
			this._accessToken = token.Value.AccessToken;
			this._platform.GetConnectInterface().RemoveNotifyAuthExpiration(this._refreshConnectionCallbackId);
			Epic.OnlineServices.Connect.LoginOptions loginOptions = new Epic.OnlineServices.Connect.LoginOptions
			{
				Credentials = new Epic.OnlineServices.Connect.Credentials?(new Epic.OnlineServices.Connect.Credentials
				{
					Token = this._accessToken,
					Type = ExternalCredentialType.Epic
				})
			};
			OnCreateUserCallback <>9__1;
			this._platform.GetConnectInterface().Login(ref loginOptions, null, delegate(ref Epic.OnlineServices.Connect.LoginCallbackInfo data)
			{
				if (data.ResultCode == Result.InvalidUser)
				{
					CreateUserOptions createUserOptions = new CreateUserOptions
					{
						ContinuanceToken = data.ContinuanceToken
					};
					ConnectInterface connectInterface = this._platform.GetConnectInterface();
					object clientData = null;
					OnCreateUserCallback completionDelegate;
					if ((completionDelegate = <>9__1) == null)
					{
						completionDelegate = (<>9__1 = delegate(ref CreateUserCallbackInfo res)
						{
							if (res.ResultCode != Result.Success)
							{
								failed = true;
								return;
							}
							this._localUserId = res.LocalUserId;
						});
					}
					connectInterface.CreateUser(ref createUserOptions, clientData, completionDelegate);
					return;
				}
				if (data.ResultCode != Result.Success)
				{
					failed = true;
					return;
				}
				this._localUserId = data.LocalUserId;
			});
			while (this._localUserId == null && !failed)
			{
				this._platform.Tick();
			}
			if (failed)
			{
				this._initFailReason = new TextObject("{=KoKdRd1u}Could not login to Epic", null);
				return false;
			}
			AddNotifyAuthExpirationOptions addNotifyAuthExpirationOptions = default(AddNotifyAuthExpirationOptions);
			this._refreshConnectionCallbackId = this._platform.GetConnectInterface().AddNotifyAuthExpiration(ref addNotifyAuthExpirationOptions, token, new OnAuthExpirationCallback(this.RefreshConnection));
			this.QueryStats();
			this.QueryDefinitions();
			return true;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public void Terminate()
		{
			if (this._platform != null)
			{
				this._platform.Release();
				this._platform = null;
				PlatformInterface.Shutdown();
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public void Tick(float dt)
		{
			if (this._platform != null)
			{
				this._platform.Tick();
				this.ProcessIngestStatsQueue();
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002B11 File Offset: 0x00000D11
		string IPlatformServices.ProviderName
		{
			get
			{
				return "Epic";
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002B18 File Offset: 0x00000D18
		PlayerId IPlatformServices.PlayerId
		{
			get
			{
				return EpicPlatformServices.EpicAccountIdToPlayerId(this._epicAccountId);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B25 File Offset: 0x00000D25
		bool IPlatformServices.IsPlayerProfileCardAvailable(PlayerId providedId)
		{
			return false;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002B28 File Offset: 0x00000D28
		void IPlatformServices.ShowPlayerProfileCard(PlayerId providedId)
		{
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002B2A File Offset: 0x00000D2A
		bool IPlatformServices.UserLoggedIn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B31 File Offset: 0x00000D31
		void IPlatformServices.LoginUser()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B38 File Offset: 0x00000D38
		Task<AvatarData> IPlatformServices.GetUserAvatar(PlayerId providedId)
		{
			return Task.FromResult<AvatarData>(null);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B40 File Offset: 0x00000D40
		Task<bool> IPlatformServices.ShowOverlayForWebPage(string url)
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B48 File Offset: 0x00000D48
		Task<ILoginAccessProvider> IPlatformServices.CreateLobbyClientLoginProvider()
		{
			return Task.FromResult<ILoginAccessProvider>(new EpicLoginAccessProvider(this._platform, this._epicAccountId, this._epicUserName, this._accessToken, this._initFailReason));
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B72 File Offset: 0x00000D72
		PlatformInitParams IPlatformServices.GetInitParams()
		{
			return this._initParams;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B7A File Offset: 0x00000D7A
		IAchievementService IPlatformServices.GetAchievementService()
		{
			return new EpicAchievementService(this);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B82 File Offset: 0x00000D82
		IActivityService IPlatformServices.GetActivityService()
		{
			return new TestActivityService();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B89 File Offset: 0x00000D89
		void IPlatformServices.CheckPrivilege(Privilege privilege, bool displayResolveUI, PrivilegeResult callback)
		{
			callback(true);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B92 File Offset: 0x00000D92
		void IPlatformServices.CheckPermissionWithUser(Permission privilege, PlayerId targetPlayerId, PermissionResult callback)
		{
			callback(true);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B9B File Offset: 0x00000D9B
		bool IPlatformServices.RegisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback)
		{
			return false;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002B9E File Offset: 0x00000D9E
		bool IPlatformServices.UnregisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback)
		{
			return false;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002BA1 File Offset: 0x00000DA1
		void IPlatformServices.ShowRestrictedInformation()
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002BA3 File Offset: 0x00000DA3
		Task<bool> IPlatformServices.VerifyString(string content)
		{
			return Task.FromResult<bool>(true);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BAB File Offset: 0x00000DAB
		void IPlatformServices.OnFocusGained()
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BAD File Offset: 0x00000DAD
		void IPlatformServices.GetPlatformId(PlayerId playerId, Action<object> callback)
		{
			callback(EpicPlatformServices.PlayerIdToEpicAccountId(playerId));
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002BBC File Offset: 0x00000DBC
		internal async Task<string> GetUserName(PlayerId providedId)
		{
			string result;
			if (!providedId.IsValid || providedId.ProvidedType != PlayerIdProvidedTypes.Epic)
			{
				result = null;
			}
			else
			{
				EpicAccountId targetUserId = EpicPlatformServices.PlayerIdToEpicAccountId(providedId);
				UserInfoData? userInfoData = await this.GetUserInfo(targetUserId);
				if (userInfoData == null)
				{
					result = "";
				}
				else
				{
					result = userInfoData.Value.DisplayName;
				}
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C0C File Offset: 0x00000E0C
		internal async Task<bool> GetUserOnlineStatus(PlayerId providedId)
		{
			EpicAccountId targetUserId = EpicPlatformServices.PlayerIdToEpicAccountId(providedId);
			await this.GetUserInfo(targetUserId);
			Info? info = await this.GetUserPresence(targetUserId);
			bool result;
			if (info == null)
			{
				result = false;
			}
			else
			{
				result = (info.Value.Status == Status.Online);
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C5C File Offset: 0x00000E5C
		internal async Task<bool> IsPlayingThisGame(PlayerId providedId)
		{
			Info? info = await this.GetUserPresence(EpicPlatformServices.PlayerIdToEpicAccountId(providedId));
			bool result;
			if (info == null)
			{
				result = false;
			}
			else
			{
				result = (info.Value.ProductId == "6372ed7350f34ffc9ace219dff4b9f40");
			}
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002CAC File Offset: 0x00000EAC
		internal Task<PlayerId> GetUserWithName(string name)
		{
			TaskCompletionSource<PlayerId> tsc = new TaskCompletionSource<PlayerId>();
			QueryUserInfoByDisplayNameOptions queryUserInfoByDisplayNameOptions = new QueryUserInfoByDisplayNameOptions
			{
				LocalUserId = this._epicAccountId,
				DisplayName = name
			};
			this._platform.GetUserInfoInterface().QueryUserInfoByDisplayName(ref queryUserInfoByDisplayNameOptions, null, delegate(ref QueryUserInfoByDisplayNameCallbackInfo callbackInfo)
			{
				if (callbackInfo.ResultCode == Result.Success)
				{
					PlayerId result = EpicPlatformServices.EpicAccountIdToPlayerId(callbackInfo.TargetUserId);
					tsc.SetResult(result);
					return;
				}
				throw new Exception("Could not retrieve player from EOS");
			});
			return tsc.Task;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D18 File Offset: 0x00000F18
		internal IEnumerable<PlayerId> GetAllFriends()
		{
			List<PlayerId> friends = new List<PlayerId>();
			bool? success = null;
			QueryFriendsOptions queryFriendsOptions = new QueryFriendsOptions
			{
				LocalUserId = this._epicAccountId
			};
			this._platform.GetFriendsInterface().QueryFriends(ref queryFriendsOptions, null, delegate(ref QueryFriendsCallbackInfo callbackInfo)
			{
				if (callbackInfo.ResultCode == Result.Success)
				{
					GetFriendsCountOptions getFriendsCountOptions = new GetFriendsCountOptions
					{
						LocalUserId = this._epicAccountId
					};
					int friendsCount = this._platform.GetFriendsInterface().GetFriendsCount(ref getFriendsCountOptions);
					for (int i = 0; i < friendsCount; i++)
					{
						GetFriendAtIndexOptions getFriendAtIndexOptions = new GetFriendAtIndexOptions
						{
							LocalUserId = this._epicAccountId,
							Index = i
						};
						EpicAccountId friendAtIndex = this._platform.GetFriendsInterface().GetFriendAtIndex(ref getFriendAtIndexOptions);
						friends.Add(EpicPlatformServices.EpicAccountIdToPlayerId(friendAtIndex));
					}
					success = new bool?(true);
					return;
				}
				success = new bool?(false);
			});
			while (success == null)
			{
				this._platform.Tick();
				Task.Delay(5);
			}
			return friends;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DA8 File Offset: 0x00000FA8
		public void QueryDefinitions()
		{
			AchievementsInterface achievementsInterface = this._platform.GetAchievementsInterface();
			QueryDefinitionsOptions queryDefinitionsOptions = new QueryDefinitionsOptions
			{
				LocalUserId = this._localUserId
			};
			achievementsInterface.QueryDefinitions(ref queryDefinitionsOptions, null, delegate(ref OnQueryDefinitionsCompleteCallbackInfo data)
			{
			});
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002DFE File Offset: 0x00000FFE
		internal bool SetStat(string name, int value)
		{
			this._ingestStatsQueue.Add(new EpicPlatformServices.IngestStatsQueueItem
			{
				Name = name,
				Value = value
			});
			return true;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E20 File Offset: 0x00001020
		internal Task<int> GetStat(string name)
		{
			StatsInterface statsInterface = this._platform.GetStatsInterface();
			CopyStatByNameOptions copyStatByNameOptions = new CopyStatByNameOptions
			{
				Name = name,
				TargetUserId = this._localUserId
			};
			Stat? stat;
			if (statsInterface.CopyStatByName(ref copyStatByNameOptions, out stat) == Result.Success)
			{
				return Task.FromResult<int>(stat.Value.Value);
			}
			return Task.FromResult<int>(-1);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002E84 File Offset: 0x00001084
		internal Task<int[]> GetStats(string[] names)
		{
			List<int> list = new List<int>();
			foreach (string name in names)
			{
				list.Add(this.GetStat(name).Result);
			}
			return Task.FromResult<int[]>(list.ToArray());
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002EC8 File Offset: 0x000010C8
		private void ProcessIngestStatsQueue()
		{
			if (!this._writingStats && DateTime.Now.Subtract(this._statsLastWrittenOn).TotalSeconds > 5.0 && this._ingestStatsQueue.Count > 0)
			{
				this._statsLastWrittenOn = DateTime.Now;
				this._writingStats = true;
				StatsInterface statsInterface = this._platform.GetStatsInterface();
				List<IngestData> stats = new List<IngestData>();
				while (this._ingestStatsQueue.Count > 0)
				{
					EpicPlatformServices.IngestStatsQueueItem ingestStatsQueueItem;
					if (this._ingestStatsQueue.TryTake(out ingestStatsQueueItem))
					{
						stats.Add(new IngestData
						{
							StatName = ingestStatsQueueItem.Name,
							IngestAmount = ingestStatsQueueItem.Value
						});
					}
				}
				IngestStatOptions ingestStatOptions = new IngestStatOptions
				{
					Stats = stats.ToArray(),
					LocalUserId = this._localUserId,
					TargetUserId = this._localUserId
				};
				statsInterface.IngestStat(ref ingestStatOptions, null, delegate(ref IngestStatCompleteCallbackInfo data)
				{
					if (data.ResultCode != Result.Success)
					{
						foreach (IngestData ingestData in stats)
						{
							this._ingestStatsQueue.Add(new EpicPlatformServices.IngestStatsQueueItem
							{
								Name = ingestData.StatName,
								Value = ingestData.IngestAmount
							});
						}
					}
					this.QueryStats();
					this._writingStats = false;
				});
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002FF2 File Offset: 0x000011F2
		private static PlayerId EpicAccountIdToPlayerId(EpicAccountId epicAccountId)
		{
			return new PlayerId(3, epicAccountId.ToString());
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003000 File Offset: 0x00001200
		private static EpicAccountId PlayerIdToEpicAccountId(PlayerId playerId)
		{
			byte[] b = new ArraySegment<byte>(playerId.ToByteArray(), 16, 16).ToArray<byte>();
			Guid guid = new Guid(b);
			return EpicAccountId.FromString(guid.ToString("N"));
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003048 File Offset: 0x00001248
		private Task<UserInfoData?> GetUserInfo(EpicAccountId targetUserId)
		{
			TaskCompletionSource<UserInfoData?> tsc = new TaskCompletionSource<UserInfoData?>();
			QueryUserInfoOptions queryUserInfoOptions = new QueryUserInfoOptions
			{
				LocalUserId = this._epicAccountId,
				TargetUserId = targetUserId
			};
			this._platform.GetUserInfoInterface().QueryUserInfo(ref queryUserInfoOptions, null, delegate(ref QueryUserInfoCallbackInfo callbackInfo)
			{
				if (callbackInfo.ResultCode == Result.Success)
				{
					CopyUserInfoOptions copyUserInfoOptions = new CopyUserInfoOptions
					{
						LocalUserId = this._epicAccountId,
						TargetUserId = targetUserId
					};
					UserInfoData? result;
					this._platform.GetUserInfoInterface().CopyUserInfo(ref copyUserInfoOptions, out result);
					tsc.SetResult(result);
					return;
				}
				tsc.SetResult(null);
			});
			return tsc.Task;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000030C4 File Offset: 0x000012C4
		private Task<Info?> GetUserPresence(EpicAccountId targetUserId)
		{
			TaskCompletionSource<Info?> tsc = new TaskCompletionSource<Info?>();
			QueryPresenceOptions queryPresenceOptions = new QueryPresenceOptions
			{
				LocalUserId = this._epicAccountId,
				TargetUserId = targetUserId
			};
			this._platform.GetPresenceInterface().QueryPresence(ref queryPresenceOptions, null, delegate(ref QueryPresenceCallbackInfo callbackInfo)
			{
				if (callbackInfo.ResultCode != Result.Success)
				{
					tsc.SetResult(null);
					return;
				}
				HasPresenceOptions hasPresenceOptions = new HasPresenceOptions
				{
					LocalUserId = this._epicAccountId,
					TargetUserId = targetUserId
				};
				if (this._platform.GetPresenceInterface().HasPresence(ref hasPresenceOptions))
				{
					CopyPresenceOptions copyPresenceOptions = new CopyPresenceOptions
					{
						LocalUserId = this._epicAccountId,
						TargetUserId = targetUserId
					};
					Info? result;
					this._platform.GetPresenceInterface().CopyPresence(ref copyPresenceOptions, out result);
					tsc.SetResult(result);
					return;
				}
				tsc.SetResult(null);
			});
			return tsc.Task;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003140 File Offset: 0x00001340
		private void QueryStats()
		{
			QueryStatsOptions queryStatsOptions = new QueryStatsOptions
			{
				LocalUserId = this._localUserId,
				TargetUserId = this._localUserId
			};
			this._platform.GetStatsInterface().QueryStats(ref queryStatsOptions, null, delegate(ref OnQueryStatsCompleteCallbackInfo data)
			{
			});
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000031A3 File Offset: 0x000013A3
		IFriendListService[] IPlatformServices.GetFriendListServices()
		{
			return this._friendListServices;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000031AB File Offset: 0x000013AB
		public void ShowGamepadTextInput(string descriptionText, string existingText, uint maxChars, bool isObfuscated)
		{
		}

		// Token: 0x04000009 RID: 9
		private EpicAccountId _epicAccountId;

		// Token: 0x0400000A RID: 10
		private ProductUserId _localUserId;

		// Token: 0x0400000B RID: 11
		private string _accessToken;

		// Token: 0x0400000C RID: 12
		private string _epicUserName;

		// Token: 0x0400000D RID: 13
		private PlatformInterface _platform;

		// Token: 0x0400000E RID: 14
		private PlatformInitParams _initParams;

		// Token: 0x0400000F RID: 15
		private EpicFriendListService _epicFriendListService;

		// Token: 0x04000010 RID: 16
		private IFriendListService[] _friendListServices;

		// Token: 0x04000016 RID: 22
		private TextObject _initFailReason;

		// Token: 0x04000017 RID: 23
		private ulong _refreshConnectionCallbackId;

		// Token: 0x04000018 RID: 24
		private ConcurrentBag<EpicPlatformServices.IngestStatsQueueItem> _ingestStatsQueue = new ConcurrentBag<EpicPlatformServices.IngestStatsQueueItem>();

		// Token: 0x04000019 RID: 25
		private bool _writingStats;

		// Token: 0x0400001A RID: 26
		private DateTime _statsLastWrittenOn = DateTime.MinValue;

		// Token: 0x0400001B RID: 27
		private const int MinStatsWriteInterval = 5;

		// Token: 0x02000007 RID: 7
		private class IngestStatsQueueItem
		{
			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000075 RID: 117 RVA: 0x000033B2 File Offset: 0x000015B2
			// (set) Token: 0x06000076 RID: 118 RVA: 0x000033BA File Offset: 0x000015BA
			public string Name { get; set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000077 RID: 119 RVA: 0x000033C3 File Offset: 0x000015C3
			// (set) Token: 0x06000078 RID: 120 RVA: 0x000033CB File Offset: 0x000015CB
			public int Value { get; set; }
		}

		// Token: 0x02000008 RID: 8
		private class EpicAuthErrorResponse
		{
			// Token: 0x17000014 RID: 20
			// (get) Token: 0x0600007A RID: 122 RVA: 0x000033DC File Offset: 0x000015DC
			// (set) Token: 0x0600007B RID: 123 RVA: 0x000033E4 File Offset: 0x000015E4
			[JsonProperty("errorCode")]
			public string ErrorCode { get; set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600007C RID: 124 RVA: 0x000033ED File Offset: 0x000015ED
			// (set) Token: 0x0600007D RID: 125 RVA: 0x000033F5 File Offset: 0x000015F5
			[JsonProperty("errorMessage")]
			public string ErrorMessage { get; set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600007E RID: 126 RVA: 0x000033FE File Offset: 0x000015FE
			// (set) Token: 0x0600007F RID: 127 RVA: 0x00003406 File Offset: 0x00001606
			[JsonProperty("numericErrorCode")]
			public int NumericErrorCode { get; set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x06000080 RID: 128 RVA: 0x0000340F File Offset: 0x0000160F
			// (set) Token: 0x06000081 RID: 129 RVA: 0x00003417 File Offset: 0x00001617
			[JsonProperty("error_description")]
			public string ErrorDescription { get; set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000082 RID: 130 RVA: 0x00003420 File Offset: 0x00001620
			// (set) Token: 0x06000083 RID: 131 RVA: 0x00003428 File Offset: 0x00001628
			[JsonProperty("error")]
			public string Error { get; set; }
		}

		// Token: 0x02000009 RID: 9
		private class EpicAuthResponse
		{
			// Token: 0x17000019 RID: 25
			// (get) Token: 0x06000085 RID: 133 RVA: 0x00003439 File Offset: 0x00001639
			// (set) Token: 0x06000086 RID: 134 RVA: 0x00003441 File Offset: 0x00001641
			[JsonProperty("access_token")]
			public string AccessToken { get; set; }

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000087 RID: 135 RVA: 0x0000344A File Offset: 0x0000164A
			// (set) Token: 0x06000088 RID: 136 RVA: 0x00003452 File Offset: 0x00001652
			[JsonProperty("expires_in")]
			public int ExpiresIn { get; set; }

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000089 RID: 137 RVA: 0x0000345B File Offset: 0x0000165B
			// (set) Token: 0x0600008A RID: 138 RVA: 0x00003463 File Offset: 0x00001663
			[JsonProperty("expires_at")]
			public DateTime ExpiresAt { get; set; }

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x0600008B RID: 139 RVA: 0x0000346C File Offset: 0x0000166C
			// (set) Token: 0x0600008C RID: 140 RVA: 0x00003474 File Offset: 0x00001674
			[JsonProperty("token_type")]
			public string TokenType { get; set; }

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x0600008D RID: 141 RVA: 0x0000347D File Offset: 0x0000167D
			// (set) Token: 0x0600008E RID: 142 RVA: 0x00003485 File Offset: 0x00001685
			[JsonProperty("refresh_token")]
			public string RefreshToken { get; set; }

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600008F RID: 143 RVA: 0x0000348E File Offset: 0x0000168E
			// (set) Token: 0x06000090 RID: 144 RVA: 0x00003496 File Offset: 0x00001696
			[JsonProperty("refresh_expires")]
			public int RefreshExpires { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x06000091 RID: 145 RVA: 0x0000349F File Offset: 0x0000169F
			// (set) Token: 0x06000092 RID: 146 RVA: 0x000034A7 File Offset: 0x000016A7
			[JsonProperty("refresh_expires_at")]
			public DateTime RefreshExpiresAt { get; set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x06000093 RID: 147 RVA: 0x000034B0 File Offset: 0x000016B0
			// (set) Token: 0x06000094 RID: 148 RVA: 0x000034B8 File Offset: 0x000016B8
			[JsonProperty("account_id")]
			public string AccountId { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x06000095 RID: 149 RVA: 0x000034C1 File Offset: 0x000016C1
			// (set) Token: 0x06000096 RID: 150 RVA: 0x000034C9 File Offset: 0x000016C9
			[JsonProperty("client_id")]
			public string ClientId { get; set; }

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x06000097 RID: 151 RVA: 0x000034D2 File Offset: 0x000016D2
			// (set) Token: 0x06000098 RID: 152 RVA: 0x000034DA File Offset: 0x000016DA
			[JsonProperty("internal_client")]
			public bool InternalClient { get; set; }

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x06000099 RID: 153 RVA: 0x000034E3 File Offset: 0x000016E3
			// (set) Token: 0x0600009A RID: 154 RVA: 0x000034EB File Offset: 0x000016EB
			[JsonProperty("client_service")]
			public string ClientService { get; set; }

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x0600009B RID: 155 RVA: 0x000034F4 File Offset: 0x000016F4
			// (set) Token: 0x0600009C RID: 156 RVA: 0x000034FC File Offset: 0x000016FC
			[JsonProperty("displayName")]
			public string DisplayName { get; set; }

			// Token: 0x17000025 RID: 37
			// (get) Token: 0x0600009D RID: 157 RVA: 0x00003505 File Offset: 0x00001705
			// (set) Token: 0x0600009E RID: 158 RVA: 0x0000350D File Offset: 0x0000170D
			[JsonProperty("app")]
			public string App { get; set; }

			// Token: 0x17000026 RID: 38
			// (get) Token: 0x0600009F RID: 159 RVA: 0x00003516 File Offset: 0x00001716
			// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000351E File Offset: 0x0000171E
			[JsonProperty("in_app_id")]
			public string InAppId { get; set; }

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003527 File Offset: 0x00001727
			// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000352F File Offset: 0x0000172F
			[JsonProperty("device_id")]
			public string DeviceId { get; set; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003538 File Offset: 0x00001738
			// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003540 File Offset: 0x00001740
			[JsonProperty("product_id")]
			public string ProductId { get; set; }
		}
	}
}
