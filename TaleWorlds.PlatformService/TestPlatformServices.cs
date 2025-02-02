using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleWorlds.AchievementSystem;
using TaleWorlds.ActivitySystem;
using TaleWorlds.Diamond;
using TaleWorlds.Diamond.AccessProvider.Test;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.PlayerServices;
using TaleWorlds.PlayerServices.Avatar;

namespace TaleWorlds.PlatformService
{
	// Token: 0x02000013 RID: 19
	public class TestPlatformServices : IPlatformServices
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000029B4 File Offset: 0x00000BB4
		public TestPlatformServices(string userName)
		{
			this._userName = userName;
			this._loginAccessProvider = new TestLoginAccessProvider();
			ILoginAccessProvider loginAccessProvider = this._loginAccessProvider;
			loginAccessProvider.Initialize(this._userName, null);
			this._playerId = loginAccessProvider.GetPlayerId();
			this._testFriendListService = new TestFriendListService(userName, this._playerId);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002A0B File Offset: 0x00000C0B
		string IPlatformServices.ProviderName
		{
			get
			{
				return "Test";
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002A14 File Offset: 0x00000C14
		string IPlatformServices.UserId
		{
			get
			{
				return this._playerId.ToString();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002A35 File Offset: 0x00000C35
		PlayerId IPlatformServices.PlayerId
		{
			get
			{
				return this._playerId;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002A3D File Offset: 0x00000C3D
		string IPlatformServices.UserDisplayName
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002A45 File Offset: 0x00000C45
		IReadOnlyCollection<PlayerId> IPlatformServices.BlockedUsers
		{
			get
			{
				return new List<PlayerId>();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002A4C File Offset: 0x00000C4C
		bool IPlatformServices.IsPermanentMuteAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00002A4F File Offset: 0x00000C4F
		bool IPlatformServices.Initialize(IFriendListService[] additionalFriendListServices)
		{
			return false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002A52 File Offset: 0x00000C52
		void IPlatformServices.Terminate()
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00002A54 File Offset: 0x00000C54
		bool IPlatformServices.IsPlayerProfileCardAvailable(PlayerId providedId)
		{
			return false;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002A57 File Offset: 0x00000C57
		bool IPlatformServices.UserLoggedIn
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00002A5A File Offset: 0x00000C5A
		void IPlatformServices.LoginUser()
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00002A5C File Offset: 0x00000C5C
		void IPlatformServices.ShowPlayerProfileCard(PlayerId providedId)
		{
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002A5E File Offset: 0x00000C5E
		Task<AvatarData> IPlatformServices.GetUserAvatar(PlayerId providedId)
		{
			return Task.FromResult<AvatarData>(null);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00002A66 File Offset: 0x00000C66
		IFriendListService[] IPlatformServices.GetFriendListServices()
		{
			return new IFriendListService[]
			{
				this._testFriendListService
			};
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00002A77 File Offset: 0x00000C77
		IAchievementService IPlatformServices.GetAchievementService()
		{
			return new TestAchievementService();
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00002A7E File Offset: 0x00000C7E
		IActivityService IPlatformServices.GetActivityService()
		{
			return new TestActivityService();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00002A85 File Offset: 0x00000C85
		Task<bool> IPlatformServices.ShowOverlayForWebPage(string url)
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000C6 RID: 198 RVA: 0x00002A90 File Offset: 0x00000C90
		// (remove) Token: 0x060000C7 RID: 199 RVA: 0x00002AC8 File Offset: 0x00000CC8
		public event Action<AvatarData> OnAvatarUpdated;

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060000C8 RID: 200 RVA: 0x00002B00 File Offset: 0x00000D00
		// (remove) Token: 0x060000C9 RID: 201 RVA: 0x00002B38 File Offset: 0x00000D38
		public event Action<string> OnNameUpdated;

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060000CA RID: 202 RVA: 0x00002B70 File Offset: 0x00000D70
		// (remove) Token: 0x060000CB RID: 203 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public event Action<bool, TextObject> OnSignInStateUpdated;

		// Token: 0x060000CC RID: 204 RVA: 0x00002BE0 File Offset: 0x00000DE0
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

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060000CD RID: 205 RVA: 0x00002C54 File Offset: 0x00000E54
		// (remove) Token: 0x060000CE RID: 206 RVA: 0x00002C8C File Offset: 0x00000E8C
		public event Action OnBlockedUserListUpdated;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060000CF RID: 207 RVA: 0x00002CC4 File Offset: 0x00000EC4
		// (remove) Token: 0x060000D0 RID: 208 RVA: 0x00002CFC File Offset: 0x00000EFC
		public event Action<string> OnTextEnteredFromPlatform;

		// Token: 0x060000D1 RID: 209 RVA: 0x00002D31 File Offset: 0x00000F31
		public void Tick(float dt)
		{
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002D33 File Offset: 0x00000F33
		PlatformInitParams IPlatformServices.GetInitParams()
		{
			return new PlatformInitParams();
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00002D3A File Offset: 0x00000F3A
		public void ActivateFriendList()
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00002D3C File Offset: 0x00000F3C
		Task<ILoginAccessProvider> IPlatformServices.CreateLobbyClientLoginProvider()
		{
			return Task.FromResult<ILoginAccessProvider>(this._loginAccessProvider);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00002D49 File Offset: 0x00000F49
		void IPlatformServices.CheckPrivilege(Privilege privilege, bool displayResolveUI, PrivilegeResult callback)
		{
			callback(true);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002D52 File Offset: 0x00000F52
		void IPlatformServices.CheckPermissionWithUser(Permission privilege, PlayerId targetPlayerId, PermissionResult callback)
		{
			callback(true);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00002D5B File Offset: 0x00000F5B
		Task<bool> IPlatformServices.VerifyString(string content)
		{
			return Task.FromResult<bool>(true);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00002D63 File Offset: 0x00000F63
		void IPlatformServices.GetPlatformId(PlayerId playerId, Action<object> callback)
		{
			callback(0);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00002D71 File Offset: 0x00000F71
		void IPlatformServices.ShowRestrictedInformation()
		{
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002D73 File Offset: 0x00000F73
		void IPlatformServices.OnFocusGained()
		{
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00002D75 File Offset: 0x00000F75
		bool IPlatformServices.RegisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback)
		{
			return true;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00002D78 File Offset: 0x00000F78
		bool IPlatformServices.UnregisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback)
		{
			return true;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002D7B File Offset: 0x00000F7B
		void IPlatformServices.ShowGamepadTextInput(string descriptionText, string existingText, uint maxChars, bool isObfuscated)
		{
		}

		// Token: 0x04000037 RID: 55
		private readonly string _userName;

		// Token: 0x04000038 RID: 56
		private readonly PlayerId _playerId;

		// Token: 0x04000039 RID: 57
		private TestLoginAccessProvider _loginAccessProvider;

		// Token: 0x0400003A RID: 58
		private TestFriendListService _testFriendListService;
	}
}
