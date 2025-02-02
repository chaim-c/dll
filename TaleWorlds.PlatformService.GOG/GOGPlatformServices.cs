using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Galaxy.Api;
using TaleWorlds.AchievementSystem;
using TaleWorlds.ActivitySystem;
using TaleWorlds.Avatar.PlayerServices;
using TaleWorlds.Diamond;
using TaleWorlds.Diamond.AccessProvider.GOG;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.PlayerServices;
using TaleWorlds.PlayerServices.Avatar;

namespace TaleWorlds.PlatformService.GOG
{
	// Token: 0x02000008 RID: 8
	public class GOGPlatformServices : IPlatformServices
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002449 File Offset: 0x00000649
		private static GOGPlatformServices Instance
		{
			get
			{
				return PlatformServices.Instance as GOGPlatformServices;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002458 File Offset: 0x00000658
		public GOGPlatformServices(PlatformInitParams initParams)
		{
			this.LoadAchievementDataFromXml((string)initParams["AchievementDataXmlPath"]);
			this._initParams = initParams;
			AvatarServices.AddAvatarService(PlayerIdProvidedTypes.GOG, new GOGPlatformAvatarService(this));
			this._gogFriendListService = new GOGFriendListService(this);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000024B6 File Offset: 0x000006B6
		string IPlatformServices.ProviderName
		{
			get
			{
				return "GOG";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000024C0 File Offset: 0x000006C0
		string IPlatformServices.UserId
		{
			get
			{
				return GalaxyInstance.User().GetGalaxyID().ToUint64().ToString();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000024E4 File Offset: 0x000006E4
		PlayerId IPlatformServices.PlayerId
		{
			get
			{
				return GalaxyInstance.User().GetGalaxyID().ToPlayerId();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000024F5 File Offset: 0x000006F5
		bool IPlatformServices.UserLoggedIn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000024FC File Offset: 0x000006FC
		void IPlatformServices.LoginUser()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002503 File Offset: 0x00000703
		string IPlatformServices.UserDisplayName
		{
			get
			{
				if (!this._initialized)
				{
					return string.Empty;
				}
				return GalaxyInstance.Friends().GetPersonaName();
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000251D File Offset: 0x0000071D
		IReadOnlyCollection<PlayerId> IPlatformServices.BlockedUsers
		{
			get
			{
				return new List<PlayerId>();
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002524 File Offset: 0x00000724
		IAchievementService IPlatformServices.GetAchievementService()
		{
			return new GOGAchievementService(this);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000252C File Offset: 0x0000072C
		IActivityService IPlatformServices.GetActivityService()
		{
			return new TestActivityService();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002533 File Offset: 0x00000733
		Task<bool> IPlatformServices.VerifyString(string content)
		{
			return Task.FromResult<bool>(true);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000253B File Offset: 0x0000073B
		void IPlatformServices.GetPlatformId(PlayerId playerId, Action<object> callback)
		{
			callback(new GalaxyID(playerId.Part4));
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000254F File Offset: 0x0000074F
		bool IPlatformServices.IsPermanentMuteAvailable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002554 File Offset: 0x00000754
		bool IPlatformServices.Initialize(IFriendListService[] additionalFriendListServices)
		{
			if (!this._initialized)
			{
				this._friendListServices = new IFriendListService[additionalFriendListServices.Length + 1];
				this._friendListServices[0] = this._gogFriendListService;
				for (int i = 0; i < additionalFriendListServices.Length; i++)
				{
					this._friendListServices[i + 1] = additionalFriendListServices[i];
				}
				this._initialized = false;
				InitParams initpParams = new InitParams("53550366963454221", "c17786edab4b6b3915ab55cfc5bb5a9a0a80b9a2d55d22c0767c9c18477efdb9");
				Debug.Print("Initializing GalaxyPeer instance...", 0, Debug.DebugColor.White, 17592186044416UL);
				try
				{
					GalaxyInstance.Init(initpParams);
					try
					{
						IUser user = GalaxyInstance.User();
						AuthenticationListener authenticationListener = new AuthenticationListener(this);
						user.SignInGalaxy(true, authenticationListener);
						while (!authenticationListener.GotResult)
						{
							GalaxyInstance.ProcessData();
							Thread.Sleep(5);
						}
						this._gogFriendListService.RequestFriendList();
					}
					catch (GalaxyInstance.Error arg)
					{
						Debug.Print("SignInGalaxy failed for reason " + arg, 0, Debug.DebugColor.White, 17592186044416UL);
					}
					this.InitListeners();
					this.RequestUserStatsAndAchievements();
					this._initialized = true;
				}
				catch (GalaxyInstance.Error arg2)
				{
					Debug.Print("Galaxy Init failed for reason " + arg2, 0, Debug.DebugColor.White, 17592186044416UL);
					this._initialized = false;
				}
			}
			return this._initialized;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000268C File Offset: 0x0000088C
		void IPlatformServices.Tick(float dt)
		{
			GalaxyInstance.ProcessData();
			if (this._initialized)
			{
				this.CheckStoreStats();
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000026A1 File Offset: 0x000008A1
		void IPlatformServices.Terminate()
		{
			GalaxyInstance.Shutdown(true);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000026A9 File Offset: 0x000008A9
		private void InvalidateStats()
		{
			this._statsLastInvalidated = new DateTime?(DateTime.Now);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000026BC File Offset: 0x000008BC
		private void CheckStoreStats()
		{
			if (this._statsLastInvalidated != null && DateTime.Now.Subtract(this._statsLastInvalidated.Value).TotalSeconds > 5.0 && DateTime.Now.Subtract(this._statsLastStored).TotalSeconds > 30.0)
			{
				this._statsLastStored = DateTime.Now;
				GalaxyInstance.Stats().StoreStatsAndAchievements();
				this._statsLastInvalidated = null;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600003F RID: 63 RVA: 0x00002748 File Offset: 0x00000948
		// (remove) Token: 0x06000040 RID: 64 RVA: 0x00002780 File Offset: 0x00000980
		public event Action<AvatarData> OnAvatarUpdated;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000041 RID: 65 RVA: 0x000027B8 File Offset: 0x000009B8
		// (remove) Token: 0x06000042 RID: 66 RVA: 0x000027F0 File Offset: 0x000009F0
		public event Action<string> OnNameUpdated;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000043 RID: 67 RVA: 0x00002828 File Offset: 0x00000A28
		// (remove) Token: 0x06000044 RID: 68 RVA: 0x00002860 File Offset: 0x00000A60
		public event Action<bool, TextObject> OnSignInStateUpdated;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000045 RID: 69 RVA: 0x00002898 File Offset: 0x00000A98
		// (remove) Token: 0x06000046 RID: 70 RVA: 0x000028D0 File Offset: 0x00000AD0
		public event Action OnBlockedUserListUpdated;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000047 RID: 71 RVA: 0x00002908 File Offset: 0x00000B08
		// (remove) Token: 0x06000048 RID: 72 RVA: 0x00002940 File Offset: 0x00000B40
		public event Action<string> OnTextEnteredFromPlatform;

		// Token: 0x06000049 RID: 73 RVA: 0x00002978 File Offset: 0x00000B78
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

		// Token: 0x0600004A RID: 74 RVA: 0x000029E9 File Offset: 0x00000BE9
		bool IPlatformServices.IsPlayerProfileCardAvailable(PlayerId providedId)
		{
			return false;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000029EC File Offset: 0x00000BEC
		void IPlatformServices.ShowPlayerProfileCard(PlayerId providedId)
		{
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000029EE File Offset: 0x00000BEE
		internal void ClearAvatarCache()
		{
			this._avatarCache.Clear();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000029FC File Offset: 0x00000BFC
		async Task<AvatarData> IPlatformServices.GetUserAvatar(PlayerId providedId)
		{
			AvatarData result;
			if (providedId.ProvidedType == PlayerIdProvidedTypes.GOGReal)
			{
				GalaxyID galaxyID = new GalaxyID(providedId.Part4);
				if (this._avatarCache.ContainsKey(providedId))
				{
					result = this._avatarCache[providedId];
				}
				else
				{
					UserInformationRetrieveListener listener = new UserInformationRetrieveListener();
					GalaxyInstance.Friends().RequestUserInformation(galaxyID, 4U, listener);
					while (!listener.GotResult)
					{
						await Task.Delay(5);
					}
					uint num = 184U;
					uint num2 = 184U;
					uint num3 = 4U * num * num2;
					byte[] array = new byte[num3];
					GalaxyInstance.Friends().GetFriendAvatarImageRGBA(galaxyID, AvatarType.AVATAR_TYPE_LARGE, array, num3);
					AvatarData avatarData = new AvatarData(array, num, num2);
					Dictionary<PlayerId, AvatarData> avatarCache = this._avatarCache;
					lock (avatarCache)
					{
						if (!this._avatarCache.ContainsKey(providedId))
						{
							this._avatarCache.Add(providedId, avatarData);
						}
					}
					result = avatarData;
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002A49 File Offset: 0x00000C49
		PlatformInitParams IPlatformServices.GetInitParams()
		{
			return this._initParams;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002A51 File Offset: 0x00000C51
		internal Task<PlayerId> GetUserWithName(string name)
		{
			return Task.FromResult<PlayerId>(PlayerId.Empty);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002A60 File Offset: 0x00000C60
		Task<bool> IPlatformServices.ShowOverlayForWebPage(string url)
		{
			bool result = false;
			Debug.Print("Opening overlay with web page " + url, 0, Debug.DebugColor.White, 17592186044416UL);
			try
			{
				GalaxyInstance.Utils().ShowOverlayWithWebPage(url);
				Debug.Print("Opened overlay with web page " + url, 0, Debug.DebugColor.White, 17592186044416UL);
				result = true;
			}
			catch (GalaxyInstance.Error error)
			{
				Debug.Print(string.Concat(new object[]
				{
					"Could not open overlay with web page ",
					url,
					" for reason ",
					error
				}), 0, Debug.DebugColor.White, 17592186044416UL);
				result = false;
			}
			return Task.FromResult<bool>(result);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B08 File Offset: 0x00000D08
		Task<ILoginAccessProvider> IPlatformServices.CreateLobbyClientLoginProvider()
		{
			return Task.FromResult<ILoginAccessProvider>(new GOGLoginAccessProvider());
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B14 File Offset: 0x00000D14
		IFriendListService[] IPlatformServices.GetFriendListServices()
		{
			return this._friendListServices;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B1C File Offset: 0x00000D1C
		void IPlatformServices.CheckPrivilege(Privilege privilege, bool displayResolveUI, PrivilegeResult callback)
		{
			callback(true);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B25 File Offset: 0x00000D25
		void IPlatformServices.CheckPermissionWithUser(Permission privilege, PlayerId targetPlayerId, PermissionResult callback)
		{
			callback(true);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B2E File Offset: 0x00000D2E
		bool IPlatformServices.RegisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback)
		{
			return false;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B31 File Offset: 0x00000D31
		bool IPlatformServices.UnregisterPermissionChangeEvent(PlayerId targetPlayerId, Permission permission, PermissionChanged callback)
		{
			return false;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002B34 File Offset: 0x00000D34
		void IPlatformServices.ShowRestrictedInformation()
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B36 File Offset: 0x00000D36
		void IPlatformServices.OnFocusGained()
		{
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B38 File Offset: 0x00000D38
		private void LoadAchievementDataFromXml(string xmlPath)
		{
			this._achievementDatas = new List<GOGPlatformServices.AchievementData>();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(xmlPath);
			XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Achievement");
			for (int i = 0; i < elementsByTagName.Count; i++)
			{
				XmlNode xmlNode = elementsByTagName[i];
				string innerText = xmlNode.Attributes.GetNamedItem("name").InnerText;
				List<ValueTuple<string, int>> list = new List<ValueTuple<string, int>>();
				XmlNodeList xmlNodeList = null;
				for (int j = 0; j < xmlNode.ChildNodes.Count; j++)
				{
					if (xmlNode.ChildNodes[j].Name == "Requirements")
					{
						xmlNodeList = xmlNode.ChildNodes[j].ChildNodes;
						break;
					}
				}
				for (int k = 0; k < xmlNodeList.Count; k++)
				{
					XmlNode xmlNode2 = xmlNodeList[k];
					string value = xmlNode2.Attributes["statName"].Value;
					int item = int.Parse(xmlNode2.Attributes["threshold"].Value);
					list.Add(new ValueTuple<string, int>(value, item));
				}
				this._achievementDatas.Add(new GOGPlatformServices.AchievementData(innerText, list));
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C68 File Offset: 0x00000E68
		internal async Task<string> GetUserName(PlayerId providedId)
		{
			string result;
			if (!providedId.IsValid || providedId.ProvidedType != PlayerIdProvidedTypes.GOG)
			{
				result = null;
			}
			else
			{
				GalaxyID gogId = providedId.ToGOGID();
				IFriends friends = GalaxyInstance.Friends();
				UserInformationRetrieveListener informationRetriever = new UserInformationRetrieveListener();
				friends.RequestUserInformation(gogId, 0U, informationRetriever);
				while (!informationRetriever.GotResult)
				{
					await Task.Delay(5);
				}
				string friendPersonaName = friends.GetFriendPersonaName(gogId);
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

		// Token: 0x0600005B RID: 91 RVA: 0x00002CB0 File Offset: 0x00000EB0
		internal bool SetStat(string name, int value)
		{
			bool result;
			try
			{
				Debug.Print(string.Concat(new object[]
				{
					"trying to set stat:",
					name,
					" to value:",
					value
				}), 0, Debug.DebugColor.White, 17592186044416UL);
				GalaxyInstance.Stats().SetStatInt(name, value);
				for (int i = 0; i < this._achievementDatas.Count; i++)
				{
					GOGPlatformServices.AchievementData achievementData = this._achievementDatas[i];
					foreach (ValueTuple<string, int> valueTuple in achievementData.RequiredStats)
					{
						if (valueTuple.Item1 == name)
						{
							if (value >= valueTuple.Item2)
							{
								this.CheckStatsAndUnlockAchievement(achievementData);
								break;
							}
							break;
						}
					}
				}
				this.InvalidateStats();
				result = true;
			}
			catch (Exception ex)
			{
				Debug.Print("Could not set stat: " + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
				result = false;
			}
			return result;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002DC4 File Offset: 0x00000FC4
		internal Task<int> GetStat(string name)
		{
			int result = -1;
			try
			{
				result = GalaxyInstance.Stats().GetStatInt(name);
			}
			catch (Exception ex)
			{
				Debug.Print("Could not get stat: " + ex.Message, 0, Debug.DebugColor.White, 17592186044416UL);
			}
			return Task.FromResult<int>(result);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002E1C File Offset: 0x0000101C
		internal Task<int[]> GetStats(string[] names)
		{
			List<int> list = new List<int>();
			foreach (string name in names)
			{
				list.Add(this.GetStat(name).Result);
			}
			return Task.FromResult<int[]>(list.ToArray());
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002E60 File Offset: 0x00001060
		private void RequestUserStatsAndAchievements()
		{
			try
			{
				GalaxyInstance.Stats().RequestUserStatsAndAchievements();
			}
			catch (GalaxyInstance.Error arg)
			{
				Debug.Print("Achievements definitions could not be retrieved: " + arg, 0, Debug.DebugColor.White, 17592186044416UL);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002EA8 File Offset: 0x000010A8
		private GOGAchievement GetGOGAchievement(string name, GalaxyID galaxyID)
		{
			bool achieved = false;
			uint num = 0U;
			GalaxyInstance.Stats().GetAchievement(name, ref achieved, ref num, GalaxyInstance.User().GetGalaxyID());
			return new GOGAchievement
			{
				AchievementName = name,
				Name = GalaxyInstance.Stats().GetAchievementDisplayName(name),
				Description = GalaxyInstance.Stats().GetAchievementDescription(name),
				Achieved = achieved
			};
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002F08 File Offset: 0x00001108
		private void CheckStatsAndUnlockAchievements()
		{
			for (int i = 0; i < this._achievementDatas.Count; i++)
			{
				GOGPlatformServices.AchievementData achievementData = this._achievementDatas[i];
				this.CheckStatsAndUnlockAchievement(achievementData);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002F40 File Offset: 0x00001140
		private void CheckStatsAndUnlockAchievement(in GOGPlatformServices.AchievementData achievementData)
		{
			if (!this.GetGOGAchievement(achievementData.AchievementName, GalaxyInstance.User().GetGalaxyID()).Achieved)
			{
				bool flag = true;
				foreach (ValueTuple<string, int> valueTuple in achievementData.RequiredStats)
				{
					if (GalaxyInstance.Stats().GetStatInt(valueTuple.Item1) < valueTuple.Item2)
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					Debug.Print("trying to set achievement:" + achievementData.AchievementName, 0, Debug.DebugColor.White, 17592186044416UL);
					GalaxyInstance.Stats().SetAchievement(achievementData.AchievementName);
				}
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002FF8 File Offset: 0x000011F8
		private void InitListeners()
		{
			this._achievementRetrieveListener = new UserStatsAndAchievementsRetrieveListener();
			this._achievementRetrieveListener.OnUserStatsAndAchievementsRetrieved += this.OnUserStatsAndAchievementsRetrieved;
			this._statsAndAchievementsStoreListener = new StatsAndAchievementsStoreListener();
			this._statsAndAchievementsStoreListener.OnUserStatsAndAchievementsStored += this.OnUserStatsAndAchievementsStored;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003049 File Offset: 0x00001249
		private void OnUserStatsAndAchievementsStored(bool success, IStatsAndAchievementsStoreListener.FailureReason? failureReason)
		{
			if (!success)
			{
				Debug.Print("Failed to store user stats and achievements: " + failureReason.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00003076 File Offset: 0x00001276
		private void OnUserStatsAndAchievementsRetrieved(GalaxyID userID, bool success, IUserStatsAndAchievementsRetrieveListener.FailureReason? failureReason)
		{
			if (success)
			{
				this.CheckStatsAndUnlockAchievements();
				return;
			}
			Debug.Print("Failed to receive user stats and achievements: " + failureReason.ToString(), 0, Debug.DebugColor.White, 17592186044416UL);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000030AA File Offset: 0x000012AA
		public void ShowGamepadTextInput(string descriptionText, string existingText, uint maxChars, bool isObfuscated)
		{
		}

		// Token: 0x04000008 RID: 8
		private const string ClientID = "53550366963454221";

		// Token: 0x04000009 RID: 9
		private const string ClientSecret = "c17786edab4b6b3915ab55cfc5bb5a9a0a80b9a2d55d22c0767c9c18477efdb9";

		// Token: 0x0400000A RID: 10
		private PlatformInitParams _initParams;

		// Token: 0x0400000B RID: 11
		private GOGFriendListService _gogFriendListService;

		// Token: 0x0400000C RID: 12
		private IFriendListService[] _friendListServices;

		// Token: 0x0400000D RID: 13
		private bool _initialized;

		// Token: 0x0400000E RID: 14
		private DateTime? _statsLastInvalidated;

		// Token: 0x0400000F RID: 15
		private DateTime _statsLastStored = DateTime.MinValue;

		// Token: 0x04000010 RID: 16
		private UserStatsAndAchievementsRetrieveListener _achievementRetrieveListener;

		// Token: 0x04000011 RID: 17
		private StatsAndAchievementsStoreListener _statsAndAchievementsStoreListener;

		// Token: 0x04000012 RID: 18
		private List<GOGPlatformServices.AchievementData> _achievementDatas;

		// Token: 0x04000013 RID: 19
		private Dictionary<PlayerId, AvatarData> _avatarCache = new Dictionary<PlayerId, AvatarData>();

		// Token: 0x02000015 RID: 21
		private readonly struct AchievementData
		{
			// Token: 0x0600009B RID: 155 RVA: 0x0000386A File Offset: 0x00001A6A
			public AchievementData(string achievementName, [TupleElementNames(new string[]
			{
				"StatName",
				"Threshold"
			})] IReadOnlyList<ValueTuple<string, int>> requiredStats)
			{
				this.AchievementName = achievementName;
				this.RequiredStats = requiredStats;
			}

			// Token: 0x0400003F RID: 63
			public readonly string AchievementName;

			// Token: 0x04000040 RID: 64
			[TupleElementNames(new string[]
			{
				"StatName",
				"Threshold"
			})]
			public readonly IReadOnlyList<ValueTuple<string, int>> RequiredStats;
		}
	}
}
