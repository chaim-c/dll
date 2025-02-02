using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleWorlds.Localization;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.PlatformService.Epic
{
	// Token: 0x02000006 RID: 6
	public class EpicThirdPartyFriendListService : IFriendListService
	{
		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000060 RID: 96 RVA: 0x000031B8 File Offset: 0x000013B8
		// (remove) Token: 0x06000061 RID: 97 RVA: 0x000031F0 File Offset: 0x000013F0
		public event Action<PlayerId> OnUserStatusChanged;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000062 RID: 98 RVA: 0x00003228 File Offset: 0x00001428
		// (remove) Token: 0x06000063 RID: 99 RVA: 0x00003260 File Offset: 0x00001460
		public event Action<PlayerId> OnFriendRemoved;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000064 RID: 100 RVA: 0x00003298 File Offset: 0x00001498
		// (remove) Token: 0x06000065 RID: 101 RVA: 0x000032D0 File Offset: 0x000014D0
		public event Action OnFriendListChanged;

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003305 File Offset: 0x00001505
		bool IFriendListService.InGameStatusFetchable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003308 File Offset: 0x00001508
		bool IFriendListService.AllowsFriendOperations
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000330B File Offset: 0x0000150B
		bool IFriendListService.CanInvitePlayersToPlatformSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000330E File Offset: 0x0000150E
		bool IFriendListService.IncludeInAllFriends
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003311 File Offset: 0x00001511
		string IFriendListService.GetServiceCodeName()
		{
			return "EpicThirdParty";
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003318 File Offset: 0x00001518
		TextObject IFriendListService.GetServiceLocalizedName()
		{
			return new TextObject("{=!}Epic Third Party", null);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003325 File Offset: 0x00001525
		FriendListServiceType IFriendListService.GetFriendListServiceType()
		{
			return FriendListServiceType.EpicThirdParty;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003328 File Offset: 0x00001528
		IEnumerable<PlayerId> IFriendListService.GetAllFriends()
		{
			return null;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000332B File Offset: 0x0000152B
		Task<bool> IFriendListService.GetUserOnlineStatus(PlayerId providedId)
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003333 File Offset: 0x00001533
		Task<bool> IFriendListService.IsPlayingThisGame(PlayerId providedId)
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000333B File Offset: 0x0000153B
		Task<string> IFriendListService.GetUserName(PlayerId providedId)
		{
			return Task.FromResult<string>("-");
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003347 File Offset: 0x00001547
		Task<PlayerId> IFriendListService.GetUserWithName(string name)
		{
			return Task.FromResult<PlayerId>(PlayerId.Empty);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003354 File Offset: 0x00001554
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
			if (this.OnFriendListChanged != null)
			{
				this.OnFriendListChanged();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000033AC File Offset: 0x000015AC
		IEnumerable<PlayerId> IFriendListService.GetPendingRequests()
		{
			return null;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033AF File Offset: 0x000015AF
		IEnumerable<PlayerId> IFriendListService.GetReceivedRequests()
		{
			return null;
		}
	}
}
