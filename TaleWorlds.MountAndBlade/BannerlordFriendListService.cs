using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaleWorlds.Localization;
using TaleWorlds.MountAndBlade.Diamond;
using TaleWorlds.PlatformService;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002D2 RID: 722
	public class BannerlordFriendListService : IFriendListService
	{
		// Token: 0x14000079 RID: 121
		// (add) Token: 0x060027C6 RID: 10182 RVA: 0x0009952C File Offset: 0x0009772C
		// (remove) Token: 0x060027C7 RID: 10183 RVA: 0x00099564 File Offset: 0x00097764
		public event Action<PlayerId> OnUserStatusChanged;

		// Token: 0x1400007A RID: 122
		// (add) Token: 0x060027C8 RID: 10184 RVA: 0x0009959C File Offset: 0x0009779C
		// (remove) Token: 0x060027C9 RID: 10185 RVA: 0x000995D4 File Offset: 0x000977D4
		public event Action<PlayerId> OnFriendRemoved;

		// Token: 0x1400007B RID: 123
		// (add) Token: 0x060027CA RID: 10186 RVA: 0x0009960C File Offset: 0x0009780C
		// (remove) Token: 0x060027CB RID: 10187 RVA: 0x00099644 File Offset: 0x00097844
		public event Action OnFriendListChanged;

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060027CC RID: 10188 RVA: 0x00099679 File Offset: 0x00097879
		bool IFriendListService.InGameStatusFetchable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060027CD RID: 10189 RVA: 0x0009967C File Offset: 0x0009787C
		bool IFriendListService.AllowsFriendOperations
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060027CE RID: 10190 RVA: 0x0009967F File Offset: 0x0009787F
		bool IFriendListService.CanInvitePlayersToPlatformSession
		{
			get
			{
				return PlatformServices.InvitationServices != null;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060027CF RID: 10191 RVA: 0x00099689 File Offset: 0x00097889
		bool IFriendListService.IncludeInAllFriends
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x0009968C File Offset: 0x0009788C
		public BannerlordFriendListService()
		{
			this.Friends = new List<FriendInfo>();
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x0009969F File Offset: 0x0009789F
		string IFriendListService.GetServiceCodeName()
		{
			return "TaleWorlds";
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x000996A6 File Offset: 0x000978A6
		TextObject IFriendListService.GetServiceLocalizedName()
		{
			return new TextObject("{=!}TaleWorlds", null);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x000996B3 File Offset: 0x000978B3
		FriendListServiceType IFriendListService.GetFriendListServiceType()
		{
			return FriendListServiceType.Bannerlord;
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x000996B8 File Offset: 0x000978B8
		IEnumerable<PlayerId> IFriendListService.GetPendingRequests()
		{
			return from f in this.Friends
			where f.Status == FriendStatus.Pending
			select f.Id;
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x00099714 File Offset: 0x00097914
		IEnumerable<PlayerId> IFriendListService.GetReceivedRequests()
		{
			return from f in this.Friends
			where f.Status == FriendStatus.Received
			select f.Id;
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x00099770 File Offset: 0x00097970
		IEnumerable<PlayerId> IFriendListService.GetAllFriends()
		{
			return from f in this.Friends
			where f.Status == FriendStatus.Accepted
			select f.Id;
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x000997CC File Offset: 0x000979CC
		Task<bool> IFriendListService.GetUserOnlineStatus(PlayerId providedId)
		{
			foreach (FriendInfo friendInfo in this.Friends)
			{
				if (friendInfo.Id.Equals(providedId))
				{
					return Task.FromResult<bool>(friendInfo.IsOnline);
				}
			}
			return Task.FromResult<bool>(false);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x00099840 File Offset: 0x00097A40
		Task<bool> IFriendListService.IsPlayingThisGame(PlayerId providedId)
		{
			return ((IFriendListService)this).GetUserOnlineStatus(providedId);
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0009984C File Offset: 0x00097A4C
		Task<string> IFriendListService.GetUserName(PlayerId providedId)
		{
			foreach (FriendInfo friendInfo in this.Friends)
			{
				if (friendInfo.Id.Equals(providedId))
				{
					return Task.FromResult<string>(friendInfo.Name);
				}
			}
			return Task.FromResult<string>(null);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000998C0 File Offset: 0x00097AC0
		Task<PlayerId> IFriendListService.GetUserWithName(string name)
		{
			foreach (FriendInfo friendInfo in this.Friends)
			{
				if (friendInfo.Name == name)
				{
					return Task.FromResult<PlayerId>(friendInfo.Id);
				}
			}
			return Task.FromResult<PlayerId>(default(PlayerId));
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x00099938 File Offset: 0x00097B38
		public void OnFriendListReceived(FriendInfo[] friends)
		{
			List<FriendInfo> friends2 = this.Friends;
			this.Friends = new List<FriendInfo>(friends);
			List<PlayerId> list = null;
			bool flag = false;
			using (List<FriendInfo>.Enumerator enumerator = this.Friends.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FriendInfo friend = enumerator.Current;
					int num = friends2.FindIndex((FriendInfo o) => o.Id.Equals(friend.Id));
					if (num < 0)
					{
						flag = true;
					}
					else
					{
						FriendInfo friendInfo = friends2[num];
						friends2.RemoveAt(num);
						if (friendInfo.Status != friend.Status)
						{
							flag = true;
						}
						else if (friendInfo.IsOnline != friend.IsOnline)
						{
							if (list == null)
							{
								list = new List<PlayerId>();
							}
							list.Add(friendInfo.Id);
						}
					}
					if (flag)
					{
						break;
					}
				}
			}
			if (!flag)
			{
				if (friends2.Count > 0)
				{
					foreach (FriendInfo friendInfo2 in friends2)
					{
						Action<PlayerId> onFriendRemoved = this.OnFriendRemoved;
						if (onFriendRemoved != null)
						{
							onFriendRemoved(friendInfo2.Id);
						}
					}
				}
				if (list != null)
				{
					foreach (PlayerId obj in list)
					{
						Action<PlayerId> onUserStatusChanged = this.OnUserStatusChanged;
						if (onUserStatusChanged != null)
						{
							onUserStatusChanged(obj);
						}
					}
				}
				return;
			}
			Action onFriendListChanged = this.OnFriendListChanged;
			if (onFriendListChanged == null)
			{
				return;
			}
			onFriendListChanged();
		}

		// Token: 0x04000EB5 RID: 3765
		protected List<FriendInfo> Friends;
	}
}
