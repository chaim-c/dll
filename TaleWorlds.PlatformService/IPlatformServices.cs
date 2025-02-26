﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleWorlds.AchievementSystem;
using TaleWorlds.ActivitySystem;
using TaleWorlds.Diamond;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.PlayerServices;
using TaleWorlds.PlayerServices.Avatar;

namespace TaleWorlds.PlatformService
{
	// Token: 0x0200000A RID: 10
	public interface IPlatformServices
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000028 RID: 40
		string ProviderName { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000029 RID: 41
		string UserId { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002A RID: 42
		PlayerId PlayerId { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43
		string UserDisplayName { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44
		bool UserLoggedIn { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002D RID: 45
		bool IsPermanentMuteAvailable { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002E RID: 46
		IReadOnlyCollection<PlayerId> BlockedUsers { get; }

		// Token: 0x0600002F RID: 47
		void LoginUser();

		// Token: 0x06000030 RID: 48
		bool Initialize(IFriendListService[] additionalFriendListServices);

		// Token: 0x06000031 RID: 49
		PlatformInitParams GetInitParams();

		// Token: 0x06000032 RID: 50
		void Terminate();

		// Token: 0x06000033 RID: 51
		void Tick(float dt);

		// Token: 0x06000034 RID: 52
		Task<AvatarData> GetUserAvatar(PlayerId providedId);

		// Token: 0x06000035 RID: 53
		Task<bool> ShowOverlayForWebPage(string url);

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000036 RID: 54
		// (remove) Token: 0x06000037 RID: 55
		event Action<AvatarData> OnAvatarUpdated;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000038 RID: 56
		// (remove) Token: 0x06000039 RID: 57
		event Action<string> OnNameUpdated;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600003A RID: 58
		// (remove) Token: 0x0600003B RID: 59
		event Action<bool, TextObject> OnSignInStateUpdated;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600003C RID: 60
		// (remove) Token: 0x0600003D RID: 61
		event Action<string> OnTextEnteredFromPlatform;

		// Token: 0x0600003E RID: 62
		Task<ILoginAccessProvider> CreateLobbyClientLoginProvider();

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600003F RID: 63
		// (remove) Token: 0x06000040 RID: 64
		event Action OnBlockedUserListUpdated;

		// Token: 0x06000041 RID: 65
		IFriendListService[] GetFriendListServices();

		// Token: 0x06000042 RID: 66
		IAchievementService GetAchievementService();

		// Token: 0x06000043 RID: 67
		IActivityService GetActivityService();

		// Token: 0x06000044 RID: 68
		void CheckPrivilege(Privilege privilege, bool displayResolveUI, PrivilegeResult callback);

		// Token: 0x06000045 RID: 69
		void CheckPermissionWithUser(Permission permission, PlayerId targetPlayerId, PermissionResult callback);

		// Token: 0x06000046 RID: 70
		bool IsPlayerProfileCardAvailable(PlayerId providedId);

		// Token: 0x06000047 RID: 71
		void ShowPlayerProfileCard(PlayerId providedId);

		// Token: 0x06000048 RID: 72
		void ShowGamepadTextInput(string descriptionText, string existingText, uint maxChars, bool isObfuscated);

		// Token: 0x06000049 RID: 73
		void GetPlatformId(PlayerId playerId, Action<object> callback);

		// Token: 0x0600004A RID: 74
		void OnFocusGained();

		// Token: 0x0600004B RID: 75
		void ShowRestrictedInformation();

		// Token: 0x0600004C RID: 76
		Task<bool> VerifyString(string content);

		// Token: 0x0600004D RID: 77
		bool RegisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback);

		// Token: 0x0600004E RID: 78
		bool UnregisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback);
	}
}
