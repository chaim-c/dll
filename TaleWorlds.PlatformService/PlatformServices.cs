using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TaleWorlds.Library;

namespace TaleWorlds.PlatformService
{
	// Token: 0x02000011 RID: 17
	public class PlatformServices
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000245F File Offset: 0x0000065F
		public static IPlatformServices Instance
		{
			get
			{
				return PlatformServices._platformServices;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002466 File Offset: 0x00000666
		public static IPlatformInvitationServices InvitationServices
		{
			get
			{
				return PlatformServices._platformServices as IPlatformInvitationServices;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002472 File Offset: 0x00000672
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002479 File Offset: 0x00000679
		public static Action<SessionInvitationType> OnSessionInvitationAccepted { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002481 File Offset: 0x00000681
		// (set) Token: 0x06000086 RID: 134 RVA: 0x00002488 File Offset: 0x00000688
		public static Action OnPlatformRequestedMultiplayer { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002490 File Offset: 0x00000690
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00002497 File Offset: 0x00000697
		public static bool IsPlatformRequestedMultiplayer { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000249F File Offset: 0x0000069F
		// (set) Token: 0x0600008A RID: 138 RVA: 0x000024A6 File Offset: 0x000006A6
		public static SessionInvitationType SessionInvitationType { get; private set; } = SessionInvitationType.None;

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000024AE File Offset: 0x000006AE
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000024B5 File Offset: 0x000006B5
		public static bool IsPlatformRequestedContinueGame { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000024BD File Offset: 0x000006BD
		public static string ProviderName
		{
			get
			{
				return PlatformServices._platformServices.ProviderName;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008E RID: 142 RVA: 0x000024C9 File Offset: 0x000006C9
		public static string UserId
		{
			get
			{
				return PlatformServices._platformServices.UserId;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000024D5 File Offset: 0x000006D5
		static PlatformServices()
		{
			PlatformServices.IsPlatformRequestedMultiplayer = false;
			PlatformServices._platformServices = new NullPlatformServices();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000024ED File Offset: 0x000006ED
		public static void Setup(IPlatformServices platformServices)
		{
			PlatformServices._platformServices = platformServices;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000024F5 File Offset: 0x000006F5
		public static bool Initialize(IFriendListService[] additionalFriendListServices)
		{
			return PlatformServices._platformServices.Initialize(additionalFriendListServices);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002502 File Offset: 0x00000702
		public static void Terminate()
		{
			PlatformServices._platformServices.Terminate();
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000250E File Offset: 0x0000070E
		public static void ConnectionStateChanged(bool isAuthenticated)
		{
			Action<bool> onConnectionStateChanged = PlatformServices.OnConnectionStateChanged;
			if (onConnectionStateChanged == null)
			{
				return;
			}
			onConnectionStateChanged(isAuthenticated);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002520 File Offset: 0x00000720
		public static void MultiplayerGameStateChanged(bool isPlaying)
		{
			Action<bool> onMultiplayerGameStateChanged = PlatformServices.OnMultiplayerGameStateChanged;
			if (onMultiplayerGameStateChanged == null)
			{
				return;
			}
			onMultiplayerGameStateChanged(isPlaying);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002532 File Offset: 0x00000732
		public static void LobbyClientStateChanged(bool atLobby, bool isPartyLeaderOrSolo)
		{
			Action<bool, bool> onLobbyClientStateChanged = PlatformServices.OnLobbyClientStateChanged;
			if (onLobbyClientStateChanged == null)
			{
				return;
			}
			onLobbyClientStateChanged(atLobby, isPartyLeaderOrSolo);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002548 File Offset: 0x00000748
		public static void FireOnSessionInvitationAccepted(SessionInvitationType sessionInvitationType)
		{
			PlatformServices.SessionInvitationType = sessionInvitationType;
			if (PlatformServices.OnSessionInvitationAccepted != null)
			{
				Delegate[] invocationList = PlatformServices.OnSessionInvitationAccepted.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Action<SessionInvitationType> action;
					if ((action = (invocationList[i] as Action<SessionInvitationType>)) != null)
					{
						action(sessionInvitationType);
					}
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00002590 File Offset: 0x00000790
		public static void FireOnPlatformRequestedMultiplayer()
		{
			PlatformServices.IsPlatformRequestedMultiplayer = true;
			if (PlatformServices.OnPlatformRequestedMultiplayer != null)
			{
				Delegate[] invocationList = PlatformServices.OnPlatformRequestedMultiplayer.GetInvocationList();
				for (int i = 0; i < invocationList.Length; i++)
				{
					Action action;
					if ((action = (invocationList[i] as Action)) != null)
					{
						action();
					}
				}
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000025D5 File Offset: 0x000007D5
		public static void OnSessionInvitationHandled()
		{
			PlatformServices.SessionInvitationType = SessionInvitationType.None;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000025DD File Offset: 0x000007DD
		public static void OnPlatformMultiplayerRequestHandled()
		{
			PlatformServices.IsPlatformRequestedMultiplayer = false;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000025E5 File Offset: 0x000007E5
		public static void SetIsPlatformRequestedContinueGame(bool isRequested)
		{
			PlatformServices.IsPlatformRequestedContinueGame = true;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000025F0 File Offset: 0x000007F0
		public static async Task<string> FilterString(string content, string defaultContent)
		{
			TaskAwaiter<bool> taskAwaiter = PlatformServices.Instance.VerifyString(content).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<bool> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<bool>);
			}
			string result;
			if (!taskAwaiter.GetResult())
			{
				result = defaultContent;
			}
			else
			{
				result = content;
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00002640 File Offset: 0x00000840
		[CommandLineFunctionality.CommandLineArgumentFunction("trigger_invitation", "platform_services")]
		public static string TriggerInvitation(List<string> strings)
		{
			SessionInvitationType sessionInvitationType;
			if (strings.Count == 0 || !Enum.TryParse<SessionInvitationType>(strings[0], out sessionInvitationType))
			{
				sessionInvitationType = SessionInvitationType.Multiplayer;
			}
			PlatformServices.FireOnSessionInvitationAccepted(sessionInvitationType);
			return "Triggered invitation with " + sessionInvitationType;
		}

		// Token: 0x04000027 RID: 39
		private static IPlatformServices _platformServices;

		// Token: 0x04000028 RID: 40
		public static Action<bool> OnConnectionStateChanged;

		// Token: 0x04000029 RID: 41
		public static Action<bool> OnMultiplayerGameStateChanged;

		// Token: 0x0400002A RID: 42
		public static Action<bool, bool> OnLobbyClientStateChanged;
	}
}
