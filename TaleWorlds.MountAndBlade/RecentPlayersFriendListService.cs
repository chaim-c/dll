using System;
using System.Collections.Generic;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlatformService;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000374 RID: 884
	public class RecentPlayersFriendListService : BannerlordFriendListService, IFriendListService
	{
		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x000C9B7D File Offset: 0x000C7D7D
		bool IFriendListService.IncludeInAllFriends
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x000C9B80 File Offset: 0x000C7D80
		bool IFriendListService.CanInvitePlayersToPlatformSession
		{
			get
			{
				return PlatformServices.InvitationServices != null;
			}
		}

		// Token: 0x0600309C RID: 12444 RVA: 0x000C9B8A File Offset: 0x000C7D8A
		TextObject IFriendListService.GetServiceLocalizedName()
		{
			return new TextObject("{=XvSRoOzM}Recently Played Players", null);
		}

		// Token: 0x0600309D RID: 12445 RVA: 0x000C9B97 File Offset: 0x000C7D97
		string IFriendListService.GetServiceCodeName()
		{
			return "RecentlyPlayedPlayers";
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x000C9B9E File Offset: 0x000C7D9E
		IEnumerable<PlayerId> IFriendListService.GetAllFriends()
		{
			return RecentPlayersManager.GetPlayersOrdered();
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x000C9BA5 File Offset: 0x000C7DA5
		FriendListServiceType IFriendListService.GetFriendListServiceType()
		{
			return FriendListServiceType.RecentPlayers;
		}
	}
}
