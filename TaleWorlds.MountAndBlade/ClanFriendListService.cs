using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleWorlds.LinQuick;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlatformService;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001E6 RID: 486
	public class ClanFriendListService : IFriendListService
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001B01 RID: 6913 RVA: 0x0005DAF7 File Offset: 0x0005BCF7
		bool IFriendListService.InGameStatusFetchable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x0005DAFA File Offset: 0x0005BCFA
		bool IFriendListService.AllowsFriendOperations
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x0005DAFD File Offset: 0x0005BCFD
		bool IFriendListService.CanInvitePlayersToPlatformSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001B04 RID: 6916 RVA: 0x0005DB00 File Offset: 0x0005BD00
		bool IFriendListService.IncludeInAllFriends
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x0005DB03 File Offset: 0x0005BD03
		public ClanFriendListService()
		{
			this._clanPlayerInfos = new Dictionary<PlayerId, ClanPlayerInfo>();
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x0005DB16 File Offset: 0x0005BD16
		string IFriendListService.GetServiceCodeName()
		{
			return "ClanFriends";
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x0005DB1D File Offset: 0x0005BD1D
		TextObject IFriendListService.GetServiceLocalizedName()
		{
			return new TextObject("{=j4F7tTzy}Clan", null);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x0005DB2A File Offset: 0x0005BD2A
		FriendListServiceType IFriendListService.GetFriendListServiceType()
		{
			return FriendListServiceType.Clan;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x0005DB2D File Offset: 0x0005BD2D
		IEnumerable<PlayerId> IFriendListService.GetAllFriends()
		{
			return this._clanPlayerInfos.Keys;
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x06001B0A RID: 6922 RVA: 0x0005DB3C File Offset: 0x0005BD3C
		// (remove) Token: 0x06001B0B RID: 6923 RVA: 0x0005DB74 File Offset: 0x0005BD74
		public event Action<PlayerId> OnUserStatusChanged;

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06001B0C RID: 6924 RVA: 0x0005DBAC File Offset: 0x0005BDAC
		// (remove) Token: 0x06001B0D RID: 6925 RVA: 0x0005DBE4 File Offset: 0x0005BDE4
		public event Action<PlayerId> OnFriendRemoved;

		// Token: 0x06001B0E RID: 6926 RVA: 0x0005DC1C File Offset: 0x0005BE1C
		async Task<bool> IFriendListService.GetUserOnlineStatus(PlayerId providedId)
		{
			bool result = false;
			ClanPlayerInfo clanPlayerInfo;
			this._clanPlayerInfos.TryGetValue(providedId, out clanPlayerInfo);
			if (clanPlayerInfo != null)
			{
				result = (clanPlayerInfo.State == AnotherPlayerState.InMultiplayerGame || clanPlayerInfo.State == AnotherPlayerState.AtLobby || clanPlayerInfo.State == AnotherPlayerState.InParty);
			}
			return await Task.FromResult<bool>(result);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x0005DC6C File Offset: 0x0005BE6C
		async Task<bool> IFriendListService.IsPlayingThisGame(PlayerId providedId)
		{
			return await((IFriendListService)this).GetUserOnlineStatus(providedId);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x0005DCBC File Offset: 0x0005BEBC
		async Task<string> IFriendListService.GetUserName(PlayerId providedId)
		{
			ClanPlayerInfo clanPlayerInfo;
			this._clanPlayerInfos.TryGetValue(providedId, out clanPlayerInfo);
			return await Task.FromResult<string>((clanPlayerInfo != null) ? clanPlayerInfo.PlayerName : null);
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x0005DD0C File Offset: 0x0005BF0C
		public async Task<PlayerId> GetUserWithName(string name)
		{
			ClanPlayerInfo clanPlayerInfo = this._clanPlayerInfos.Values.FirstOrDefaultQ((ClanPlayerInfo playerInfo) => playerInfo.PlayerName == name);
			return await Task.FromResult<PlayerId>((clanPlayerInfo != null) ? clanPlayerInfo.PlayerId : PlayerId.Empty);
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06001B12 RID: 6930 RVA: 0x0005DD5C File Offset: 0x0005BF5C
		// (remove) Token: 0x06001B13 RID: 6931 RVA: 0x0005DD94 File Offset: 0x0005BF94
		public event Action OnFriendListChanged;

		// Token: 0x06001B14 RID: 6932 RVA: 0x0005DDC9 File Offset: 0x0005BFC9
		public IEnumerable<PlayerId> GetPendingRequests()
		{
			return null;
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0005DDCC File Offset: 0x0005BFCC
		public IEnumerable<PlayerId> GetReceivedRequests()
		{
			return null;
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x0005DDD0 File Offset: 0x0005BFD0
		private void Dummy()
		{
			if (this.OnUserStatusChanged != null)
			{
				this.OnUserStatusChanged(default(PlayerId));
			}
			if (this.OnFriendRemoved != null)
			{
				this.OnFriendRemoved(default(PlayerId));
			}
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x0005DE18 File Offset: 0x0005C018
		public void OnClanInfoChanged(List<ClanPlayerInfo> playerInfosInClan)
		{
			this._clanPlayerInfos.Clear();
			if (playerInfosInClan != null)
			{
				foreach (ClanPlayerInfo clanPlayerInfo in playerInfosInClan)
				{
					this._clanPlayerInfos.Add(clanPlayerInfo.PlayerId, clanPlayerInfo);
				}
			}
			Action onFriendListChanged = this.OnFriendListChanged;
			if (onFriendListChanged == null)
			{
				return;
			}
			onFriendListChanged();
		}

		// Token: 0x04000894 RID: 2196
		public const string CodeName = "ClanFriends";

		// Token: 0x04000895 RID: 2197
		private readonly Dictionary<PlayerId, ClanPlayerInfo> _clanPlayerInfos;
	}
}
