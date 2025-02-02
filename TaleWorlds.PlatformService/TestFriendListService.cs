using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaleWorlds.Diamond.AccessProvider.Test;
using TaleWorlds.Localization;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.PlatformService
{
	// Token: 0x02000012 RID: 18
	public class TestFriendListService : IFriendListService
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00002688 File Offset: 0x00000888
		public TestFriendListService(string userName, PlayerId myPlayerId)
		{
			this._userName = userName;
			this._playerId = myPlayerId;
			this._testUserNames = new Dictionary<PlayerId, string>();
			this._testUserPlayerIds = new Dictionary<string, PlayerId>();
			this._testUserNames.Add(this._playerId, this._userName);
			this._testUserPlayerIds.Add(this._userName, this._playerId);
			for (int i = 1; i <= 12; i++)
			{
				string text = "TestPlayer" + i;
				PlayerId playerIdFromUserName = TestLoginAccessProvider.GetPlayerIdFromUserName(text);
				if (!this._testUserNames.ContainsKey(playerIdFromUserName))
				{
					this._testUserNames.Add(playerIdFromUserName, text);
					this._testUserPlayerIds.Add(text, playerIdFromUserName);
				}
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000273A File Offset: 0x0000093A
		string IFriendListService.GetServiceCodeName()
		{
			return "Test";
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00002741 File Offset: 0x00000941
		TextObject IFriendListService.GetServiceLocalizedName()
		{
			return new TextObject("{=!}Test", null);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000274E File Offset: 0x0000094E
		FriendListServiceType IFriendListService.GetFriendListServiceType()
		{
			return FriendListServiceType.Test;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00002751 File Offset: 0x00000951
		Task<bool> IFriendListService.GetUserOnlineStatus(PlayerId providedId)
		{
			if (this._testUserNames.ContainsKey(providedId))
			{
				return Task.FromResult<bool>(true);
			}
			return Task.FromResult<bool>(false);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000276E File Offset: 0x0000096E
		Task<bool> IFriendListService.IsPlayingThisGame(PlayerId providedId)
		{
			if (this._testUserNames.ContainsKey(providedId))
			{
				return Task.FromResult<bool>(true);
			}
			return Task.FromResult<bool>(false);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000278B File Offset: 0x0000098B
		bool IFriendListService.InGameStatusFetchable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000278E File Offset: 0x0000098E
		bool IFriendListService.AllowsFriendOperations
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002791 File Offset: 0x00000991
		bool IFriendListService.CanInvitePlayersToPlatformSession
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002794 File Offset: 0x00000994
		bool IFriendListService.IncludeInAllFriends
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00002797 File Offset: 0x00000997
		IEnumerable<PlayerId> IFriendListService.GetAllFriends()
		{
			List<string> list = new List<string>();
			if (this._userName == "TestPlayer1" || this._userName == "TestPlayer2" || this._userName == "TestPlayer3" || this._userName == "TestPlayer4" || this._userName == "TestPlayer5" || this._userName == "TestPlayer6")
			{
				list.Add("TestPlayer1");
				list.Add("TestPlayer2");
				list.Add("TestPlayer3");
				list.Add("TestPlayer4");
				list.Add("TestPlayer5");
				list.Add("TestPlayer6");
			}
			else if (this._userName == "TestPlayer7" || this._userName == "TestPlayer8" || this._userName == "TestPlayer9" || this._userName == "TestPlayer10" || this._userName == "TestPlayer11" || this._userName == "TestPlayer12")
			{
				list.Add("TestPlayer7");
				list.Add("TestPlayer8");
				list.Add("TestPlayer9");
				list.Add("TestPlayer10");
				list.Add("TestPlayer11");
				list.Add("TestPlayer12");
			}
			else
			{
				list.Add("TestPlayer1");
				list.Add("TestPlayer2");
				list.Add("TestPlayer3");
				list.Add("TestPlayer4");
				list.Add("TestPlayer5");
				list.Add("TestPlayer6");
				list.Add("TestPlayer7");
				list.Add("TestPlayer8");
				list.Add("TestPlayer9");
				list.Add("TestPlayer10");
				list.Add("TestPlayer11");
				list.Add("TestPlayer12");
			}
			foreach (string text in list)
			{
				if (this._userName != text)
				{
					yield return TestLoginAccessProvider.GetPlayerIdFromUserName(text);
				}
			}
			List<string>.Enumerator enumerator = default(List<string>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000027A7 File Offset: 0x000009A7
		IEnumerable<PlayerId> IFriendListService.GetPendingRequests()
		{
			return null;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000027AA File Offset: 0x000009AA
		IEnumerable<PlayerId> IFriendListService.GetReceivedRequests()
		{
			return null;
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060000AB RID: 171 RVA: 0x000027B0 File Offset: 0x000009B0
		// (remove) Token: 0x060000AC RID: 172 RVA: 0x000027E8 File Offset: 0x000009E8
		public event Action<PlayerId> OnUserStatusChanged;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060000AD RID: 173 RVA: 0x00002820 File Offset: 0x00000A20
		// (remove) Token: 0x060000AE RID: 174 RVA: 0x00002858 File Offset: 0x00000A58
		public event Action<PlayerId> OnFriendRemoved;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000AF RID: 175 RVA: 0x00002890 File Offset: 0x00000A90
		// (remove) Token: 0x060000B0 RID: 176 RVA: 0x000028C8 File Offset: 0x00000AC8
		public event Action OnFriendListChanged;

		// Token: 0x060000B1 RID: 177 RVA: 0x00002900 File Offset: 0x00000B00
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

		// Token: 0x060000B2 RID: 178 RVA: 0x00002958 File Offset: 0x00000B58
		Task<string> IFriendListService.GetUserName(PlayerId providedId)
		{
			string result = "-";
			string text;
			if (this._testUserNames.TryGetValue(providedId, out text))
			{
				result = text;
			}
			return Task.FromResult<string>(result);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00002984 File Offset: 0x00000B84
		Task<PlayerId> IFriendListService.GetUserWithName(string name)
		{
			PlayerId result = default(PlayerId);
			PlayerId playerId;
			if (this._testUserPlayerIds.TryGetValue(name, out playerId))
			{
				result = playerId;
			}
			return Task.FromResult<PlayerId>(result);
		}

		// Token: 0x04000030 RID: 48
		private string _userName;

		// Token: 0x04000031 RID: 49
		private PlayerId _playerId;

		// Token: 0x04000032 RID: 50
		private Dictionary<PlayerId, string> _testUserNames;

		// Token: 0x04000033 RID: 51
		private Dictionary<string, PlayerId> _testUserPlayerIds;
	}
}
