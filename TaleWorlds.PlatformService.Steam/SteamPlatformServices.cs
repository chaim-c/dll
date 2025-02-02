using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Steamworks;
using TaleWorlds.AchievementSystem;
using TaleWorlds.ActivitySystem;
using TaleWorlds.Avatar.PlayerServices;
using TaleWorlds.Diamond;
using TaleWorlds.Diamond.AccessProvider.Steam;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.ModuleManager;
using TaleWorlds.PlayerServices;
using TaleWorlds.PlayerServices.Avatar;

namespace TaleWorlds.PlatformService.Steam
{
	// Token: 0x02000006 RID: 6
	public class SteamPlatformServices : IPlatformServices
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002565 File Offset: 0x00000765
		private static SteamPlatformServices Instance
		{
			get
			{
				return PlatformServices.Instance as SteamPlatformServices;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002571 File Offset: 0x00000771
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002579 File Offset: 0x00000779
		internal bool Initialized { get; private set; }

		// Token: 0x06000037 RID: 55 RVA: 0x00002582 File Offset: 0x00000782
		public SteamPlatformServices(PlatformInitParams initParams)
		{
			this._initParams = initParams;
			AvatarServices.AddAvatarService(PlayerIdProvidedTypes.Steam, new SteamPlatformAvatarService(this));
			this._achievementService = new SteamAchievementService(this);
			this._steamFriendListService = new SteamFriendListService(this);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000025C0 File Offset: 0x000007C0
		string IPlatformServices.ProviderName
		{
			get
			{
				return "Steam";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000025C8 File Offset: 0x000007C8
		string IPlatformServices.UserId
		{
			get
			{
				return ((ulong)SteamUser.GetSteamID()).ToString();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000025E7 File Offset: 0x000007E7
		PlayerId IPlatformServices.PlayerId
		{
			get
			{
				return SteamUser.GetSteamID().ToPlayerId();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000025F3 File Offset: 0x000007F3
		bool IPlatformServices.UserLoggedIn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025FA File Offset: 0x000007FA
		void IPlatformServices.LoginUser()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002601 File Offset: 0x00000801
		string IPlatformServices.UserDisplayName
		{
			get
			{
				if (!this.Initialized)
				{
					return string.Empty;
				}
				return SteamFriends.GetPersonaName();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002616 File Offset: 0x00000816
		IReadOnlyCollection<PlayerId> IPlatformServices.BlockedUsers
		{
			get
			{
				return new List<PlayerId>();
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003F RID: 63 RVA: 0x0000261D File Offset: 0x0000081D
		bool IPlatformServices.IsPermanentMuteAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002620 File Offset: 0x00000820
		bool IPlatformServices.Initialize(IFriendListService[] additionalFriendListServices)
		{
			this._friendListServices = new IFriendListService[additionalFriendListServices.Length + 1];
			this._friendListServices[0] = this._steamFriendListService;
			for (int i = 0; i < additionalFriendListServices.Length; i++)
			{
				this._friendListServices[i + 1] = additionalFriendListServices[i];
			}
			if (!SteamAPI.Init())
			{
				return false;
			}
			ModuleHelper.InitializePlatformModuleExtension(new SteamModuleExtension());
			this.InitCallbacks();
			this._achievementService.Initialize();
			SteamUserStats.RequestCurrentStats();
			this.Initialized = true;
			return true;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002698 File Offset: 0x00000898
		void IPlatformServices.Tick(float dt)
		{
			if (this.Initialized)
			{
				SteamAPI.RunCallbacks();
				this._achievementService.Tick(dt);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000026B3 File Offset: 0x000008B3
		void IPlatformServices.Terminate()
		{
			SteamAPI.Shutdown();
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000043 RID: 67 RVA: 0x000026BC File Offset: 0x000008BC
		// (remove) Token: 0x06000044 RID: 68 RVA: 0x000026F4 File Offset: 0x000008F4
		public event Action<AvatarData> OnAvatarUpdated;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000045 RID: 69 RVA: 0x0000272C File Offset: 0x0000092C
		// (remove) Token: 0x06000046 RID: 70 RVA: 0x00002764 File Offset: 0x00000964
		public event Action<string> OnNameUpdated;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000047 RID: 71 RVA: 0x0000279C File Offset: 0x0000099C
		// (remove) Token: 0x06000048 RID: 72 RVA: 0x000027D4 File Offset: 0x000009D4
		public event Action<bool, TextObject> OnSignInStateUpdated;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000049 RID: 73 RVA: 0x0000280C File Offset: 0x00000A0C
		// (remove) Token: 0x0600004A RID: 74 RVA: 0x00002844 File Offset: 0x00000A44
		public event Action OnBlockedUserListUpdated;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600004B RID: 75 RVA: 0x0000287C File Offset: 0x00000A7C
		// (remove) Token: 0x0600004C RID: 76 RVA: 0x000028B4 File Offset: 0x00000AB4
		public event Action<string> OnTextEnteredFromPlatform;

		// Token: 0x0600004D RID: 77 RVA: 0x000028E9 File Offset: 0x00000AE9
		void IPlatformServices.ShowGamepadTextInput(string descriptionText, string existingText, uint maxChars, bool isObfuscated)
		{
			if (this.Initialized)
			{
				SteamUtils.ShowGamepadTextInput(isObfuscated ? EGamepadTextInputMode.k_EGamepadTextInputModePassword : EGamepadTextInputMode.k_EGamepadTextInputModeNormal, EGamepadTextInputLineMode.k_EGamepadTextInputLineModeSingleLine, descriptionText, maxChars, existingText);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002905 File Offset: 0x00000B05
		bool IPlatformServices.IsPlayerProfileCardAvailable(PlayerId providedId)
		{
			return false;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002908 File Offset: 0x00000B08
		void IPlatformServices.ShowPlayerProfileCard(PlayerId providedId)
		{
			SteamFriends.ActivateGameOverlayToUser("steamid", providedId.ToSteamId());
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000291C File Offset: 0x00000B1C
		async Task<AvatarData> IPlatformServices.GetUserAvatar(PlayerId providedId)
		{
			AvatarData result;
			if (!providedId.IsValid)
			{
				result = null;
			}
			else if (this._avatarCache.ContainsKey(providedId))
			{
				result = this._avatarCache[providedId];
			}
			else
			{
				if (this._avatarCache.Count > 300)
				{
					this._avatarCache.Clear();
				}
				long startTime = DateTime.UtcNow.Ticks;
				CSteamID steamId = providedId.ToSteamId();
				if (SteamFriends.RequestUserInformation(steamId, false))
				{
					while (!SteamPlatformServices._avatarUpdates.Contains(steamId) && !this.TimedOut(startTime, 5000L))
					{
						await Task.Delay(5);
					}
					SteamPlatformServices._avatarUpdates.Remove(steamId);
				}
				int userAvatar = SteamFriends.GetLargeFriendAvatar(steamId);
				if (userAvatar == -1)
				{
					while (!SteamPlatformServices._avatarLoadedUpdates.Contains(steamId) && !this.TimedOut(startTime, 5000L))
					{
						await Task.Delay(5);
					}
					SteamPlatformServices._avatarLoadedUpdates.Remove(steamId);
					while (userAvatar == -1 && !this.TimedOut(startTime, 5000L))
					{
						userAvatar = SteamFriends.GetLargeFriendAvatar(steamId);
					}
				}
				if (userAvatar != -1)
				{
					uint num;
					uint num2;
					SteamUtils.GetImageSize(userAvatar, out num, out num2);
					if (num != 0U)
					{
						uint num3 = num * num2 * 4U;
						byte[] array = new byte[num3];
						if (SteamUtils.GetImageRGBA(userAvatar, array, (int)num3))
						{
							AvatarData avatarData = new AvatarData(array, num, num2);
							Dictionary<PlayerId, AvatarData> avatarCache = this._avatarCache;
							lock (avatarCache)
							{
								if (!this._avatarCache.ContainsKey(providedId))
								{
									this._avatarCache.Add(providedId, avatarData);
								}
							}
							return avatarData;
						}
					}
				}
				result = null;
			}
			return result;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002969 File Offset: 0x00000B69
		public void ClearAvatarCache()
		{
			this._avatarCache.Clear();
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002978 File Offset: 0x00000B78
		private bool TimedOut(long startUTCTicks, long timeOut)
		{
			return (long)(DateTime.Now - new DateTime(startUTCTicks)).Milliseconds > timeOut;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000029A4 File Offset: 0x00000BA4
		internal async Task<string> GetUserName(PlayerId providedId)
		{
			string result;
			if (!providedId.IsValid || providedId.ProvidedType != PlayerIdProvidedTypes.Steam)
			{
				result = null;
			}
			else
			{
				long startTime = DateTime.UtcNow.Ticks;
				CSteamID steamId = providedId.ToSteamId();
				if (SteamFriends.RequestUserInformation(steamId, false))
				{
					while (!SteamPlatformServices._nameUpdates.Contains(steamId) && !this.TimedOut(startTime, 5000L))
					{
						await Task.Delay(5);
					}
					SteamPlatformServices._nameUpdates.Remove(steamId);
				}
				string friendPersonaName = SteamFriends.GetFriendPersonaName(steamId);
				if (!string.IsNullOrEmpty(friendPersonaName))
				{
					result = friendPersonaName;
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000029F1 File Offset: 0x00000BF1
		PlatformInitParams IPlatformServices.GetInitParams()
		{
			return this._initParams;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000029F9 File Offset: 0x00000BF9
		IAchievementService IPlatformServices.GetAchievementService()
		{
			return this._achievementService;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002A01 File Offset: 0x00000C01
		IActivityService IPlatformServices.GetActivityService()
		{
			return new TestActivityService();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002A08 File Offset: 0x00000C08
		async Task<bool> IPlatformServices.ShowOverlayForWebPage(string url)
		{
			await Task.Delay(0);
			SteamFriends.ActivateGameOverlayToWebPage(url, EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
			return true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002A4D File Offset: 0x00000C4D
		void IPlatformServices.CheckPrivilege(Privilege privilege, bool displayResolveUI, PrivilegeResult callback)
		{
			callback(true);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002A56 File Offset: 0x00000C56
		void IPlatformServices.CheckPermissionWithUser(Permission privilege, PlayerId targetPlayerId, PermissionResult callback)
		{
			callback(true);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002A5F File Offset: 0x00000C5F
		bool IPlatformServices.RegisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback)
		{
			return false;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A62 File Offset: 0x00000C62
		bool IPlatformServices.UnregisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback)
		{
			return false;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002A65 File Offset: 0x00000C65
		void IPlatformServices.ShowRestrictedInformation()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002A67 File Offset: 0x00000C67
		Task<bool> IPlatformServices.VerifyString(string content)
		{
			return Task.FromResult<bool>(true);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002A6F File Offset: 0x00000C6F
		void IPlatformServices.GetPlatformId(PlayerId playerId, Action<object> callback)
		{
			callback(playerId.ToSteamId());
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002A82 File Offset: 0x00000C82
		void IPlatformServices.OnFocusGained()
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002A84 File Offset: 0x00000C84
		internal Task<bool> GetUserOnlineStatus(PlayerId providedId)
		{
			SteamUtils.GetAppID();
			if (SteamFriends.GetFriendPersonaState(new CSteamID(providedId.Part4)) != EPersonaState.k_EPersonaStateOffline)
			{
				return Task.FromResult<bool>(true);
			}
			return Task.FromResult<bool>(false);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002AAC File Offset: 0x00000CAC
		internal Task<bool> IsPlayingThisGame(PlayerId providedId)
		{
			AppId_t appID = SteamUtils.GetAppID();
			FriendGameInfo_t friendGameInfo_t;
			if (SteamFriends.GetFriendGamePlayed(new CSteamID(providedId.Part4), out friendGameInfo_t) && friendGameInfo_t.m_gameID.AppID() == appID)
			{
				return Task.FromResult<bool>(true);
			}
			return Task.FromResult<bool>(false);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002AF8 File Offset: 0x00000CF8
		internal async Task<PlayerId> GetUserWithName(string name)
		{
			await Task.Delay(0);
			int num = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate);
			CSteamID steamId = default(CSteamID);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				CSteamID friendByIndex = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate);
				if (SteamFriends.GetFriendPersonaName(friendByIndex).Equals(name))
				{
					steamId = friendByIndex;
					num2++;
				}
			}
			num = SteamFriends.GetCoplayFriendCount();
			for (int j = 0; j < num; j++)
			{
				CSteamID coplayFriend = SteamFriends.GetCoplayFriend(j);
				if (SteamFriends.GetFriendPersonaName(coplayFriend).Equals(name))
				{
					steamId = coplayFriend;
					num2++;
				}
			}
			PlayerId result;
			if (num2 != 1)
			{
				result = default(PlayerId);
			}
			else
			{
				result = steamId.ToPlayerId();
			}
			return result;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002B40 File Offset: 0x00000D40
		private async void OnAvatarUpdateReceived(ulong userId)
		{
			int userAvatar = -1;
			while (userAvatar == -1)
			{
				userAvatar = SteamFriends.GetLargeFriendAvatar(new CSteamID(userId));
				await Task.Delay(5);
			}
			if (userAvatar != -1)
			{
				uint num;
				uint num2;
				SteamUtils.GetImageSize(userAvatar, out num, out num2);
				if (num != 0U)
				{
					uint num3 = num * num2 * 4U;
					byte[] array = new byte[num3];
					if (SteamUtils.GetImageRGBA(userAvatar, array, (int)num3))
					{
						Action<AvatarData> onAvatarUpdated = this.OnAvatarUpdated;
						if (onAvatarUpdated != null)
						{
							onAvatarUpdated(new AvatarData(array, num, num2));
						}
					}
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002B84 File Offset: 0x00000D84
		private void OnNameUpdateReceived(PlayerId userId)
		{
			string friendPersonaName = SteamFriends.GetFriendPersonaName(userId.ToSteamId());
			if (!string.IsNullOrEmpty(friendPersonaName))
			{
				Action<string> onNameUpdated = this.OnNameUpdated;
				if (onNameUpdated == null)
				{
					return;
				}
				onNameUpdated(friendPersonaName);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002BB6 File Offset: 0x00000DB6
		private void Dummy()
		{
			if (this.OnSignInStateUpdated != null)
			{
				this.OnSignInStateUpdated(false, null);
			}
			if (this.OnBlockedUserListUpdated != null)
			{
				this.OnBlockedUserListUpdated();
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002BE0 File Offset: 0x00000DE0
		private void InitCallbacks()
		{
			this._personaStateChangeT = Callback<PersonaStateChange_t>.Create(new Callback<PersonaStateChange_t>.DispatchDelegate(SteamPlatformServices.UserInformationUpdated));
			this._avatarImageLoadedT = Callback<AvatarImageLoaded_t>.Create(new Callback<AvatarImageLoaded_t>.DispatchDelegate(SteamPlatformServices.AvatarLoaded));
			this._gamepadTextInputDismissedT = Callback<GamepadTextInputDismissed_t>.Create(new Callback<GamepadTextInputDismissed_t>.DispatchDelegate(this.GamepadTextInputDismissed));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002C32 File Offset: 0x00000E32
		private static void AvatarLoaded(AvatarImageLoaded_t avatarImageLoadedT)
		{
			SteamPlatformServices._avatarLoadedUpdates.Add(avatarImageLoadedT.m_steamID);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002C44 File Offset: 0x00000E44
		private static void UserInformationUpdated(PersonaStateChange_t pCallback)
		{
			if ((pCallback.m_nChangeFlags & EPersonaChange.k_EPersonaChangeAvatar) != (EPersonaChange)0)
			{
				SteamPlatformServices._avatarUpdates.Add(new CSteamID(pCallback.m_ulSteamID));
				SteamPlatformServices.Instance.OnAvatarUpdateReceived(pCallback.m_ulSteamID);
				return;
			}
			if ((pCallback.m_nChangeFlags & EPersonaChange.k_EPersonaChangeName) != (EPersonaChange)0)
			{
				SteamPlatformServices._nameUpdates.Add(new CSteamID(pCallback.m_ulSteamID));
				SteamPlatformServices.Instance.OnNameUpdateReceived(new CSteamID(pCallback.m_ulSteamID).ToPlayerId());
				return;
			}
			if ((pCallback.m_nChangeFlags & EPersonaChange.k_EPersonaChangeGamePlayed) != (EPersonaChange)0)
			{
				SteamPlatformServices.HandleOnUserStatusChanged(new CSteamID(pCallback.m_ulSteamID).ToPlayerId());
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002CDC File Offset: 0x00000EDC
		private void GamepadTextInputDismissed(GamepadTextInputDismissed_t gamepadTextInputDismissedT)
		{
			if (gamepadTextInputDismissedT.m_bSubmitted)
			{
				string obj;
				SteamUtils.GetEnteredGamepadTextInput(out obj, SteamUtils.GetEnteredGamepadTextLength());
				Action<string> onTextEnteredFromPlatform = this.OnTextEnteredFromPlatform;
				if (onTextEnteredFromPlatform == null)
				{
					return;
				}
				onTextEnteredFromPlatform(obj);
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002D0F File Offset: 0x00000F0F
		private static void HandleOnUserStatusChanged(PlayerId playerId)
		{
			SteamPlatformServices.Instance._steamFriendListService.HandleOnUserStatusChanged(playerId);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002D21 File Offset: 0x00000F21
		Task<ILoginAccessProvider> IPlatformServices.CreateLobbyClientLoginProvider()
		{
			return Task.FromResult<ILoginAccessProvider>(new SteamLoginAccessProvider());
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002D2D File Offset: 0x00000F2D
		IFriendListService[] IPlatformServices.GetFriendListServices()
		{
			return this._friendListServices;
		}

		// Token: 0x04000010 RID: 16
		private PlatformInitParams _initParams;

		// Token: 0x04000011 RID: 17
		private SteamFriendListService _steamFriendListService;

		// Token: 0x04000012 RID: 18
		private IFriendListService[] _friendListServices;

		// Token: 0x04000013 RID: 19
		public SteamAchievementService _achievementService;

		// Token: 0x04000019 RID: 25
		private Dictionary<PlayerId, AvatarData> _avatarCache = new Dictionary<PlayerId, AvatarData>();

		// Token: 0x0400001A RID: 26
		private const int CommandRequestTimeOut = 5000;

		// Token: 0x0400001B RID: 27
		private Callback<PersonaStateChange_t> _personaStateChangeT;

		// Token: 0x0400001C RID: 28
		private Callback<AvatarImageLoaded_t> _avatarImageLoadedT;

		// Token: 0x0400001D RID: 29
		private Callback<GamepadTextInputDismissed_t> _gamepadTextInputDismissedT;

		// Token: 0x0400001E RID: 30
		private static List<CSteamID> _avatarUpdates = new List<CSteamID>();

		// Token: 0x0400001F RID: 31
		private static List<CSteamID> _avatarLoadedUpdates = new List<CSteamID>();

		// Token: 0x04000020 RID: 32
		private static List<CSteamID> _nameUpdates = new List<CSteamID>();
	}
}
