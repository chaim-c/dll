using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020002F9 RID: 761
	public class MultiplayerIntermissionVotingManager
	{
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x0009EFEC File Offset: 0x0009D1EC
		public static MultiplayerIntermissionVotingManager Instance
		{
			get
			{
				MultiplayerIntermissionVotingManager result;
				if ((result = MultiplayerIntermissionVotingManager._instance) == null)
				{
					result = (MultiplayerIntermissionVotingManager._instance = new MultiplayerIntermissionVotingManager());
				}
				return result;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x0009F002 File Offset: 0x0009D202
		// (set) Token: 0x0600296B RID: 10603 RVA: 0x0009F00A File Offset: 0x0009D20A
		public List<IntermissionVoteItem> MapVoteItems { get; private set; }

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x0009F013 File Offset: 0x0009D213
		// (set) Token: 0x0600296D RID: 10605 RVA: 0x0009F01B File Offset: 0x0009D21B
		public List<IntermissionVoteItem> CultureVoteItems { get; private set; }

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x0009F024 File Offset: 0x0009D224
		// (set) Token: 0x0600296F RID: 10607 RVA: 0x0009F02C File Offset: 0x0009D22C
		public List<CustomGameUsableMap> UsableMaps { get; private set; }

		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06002970 RID: 10608 RVA: 0x0009F038 File Offset: 0x0009D238
		// (remove) Token: 0x06002971 RID: 10609 RVA: 0x0009F070 File Offset: 0x0009D270
		public event MultiplayerIntermissionVotingManager.MapItemAddedDelegate OnMapItemAdded;

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06002972 RID: 10610 RVA: 0x0009F0A8 File Offset: 0x0009D2A8
		// (remove) Token: 0x06002973 RID: 10611 RVA: 0x0009F0E0 File Offset: 0x0009D2E0
		public event MultiplayerIntermissionVotingManager.CultureItemAddedDelegate OnCultureItemAdded;

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06002974 RID: 10612 RVA: 0x0009F118 File Offset: 0x0009D318
		// (remove) Token: 0x06002975 RID: 10613 RVA: 0x0009F150 File Offset: 0x0009D350
		public event MultiplayerIntermissionVotingManager.MapItemVoteCountChangedDelegate OnMapItemVoteCountChanged;

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06002976 RID: 10614 RVA: 0x0009F188 File Offset: 0x0009D388
		// (remove) Token: 0x06002977 RID: 10615 RVA: 0x0009F1C0 File Offset: 0x0009D3C0
		public event MultiplayerIntermissionVotingManager.CultureItemVoteCountChangedDelegate OnCultureItemVoteCountChanged;

		// Token: 0x06002978 RID: 10616 RVA: 0x0009F1F8 File Offset: 0x0009D3F8
		public MultiplayerIntermissionVotingManager()
		{
			this.MapVoteItems = new List<IntermissionVoteItem>();
			this.CultureVoteItems = new List<IntermissionVoteItem>();
			this.UsableMaps = new List<CustomGameUsableMap>();
			this._votesOfPlayers = new Dictionary<PlayerId, List<string>>();
			this.IsMapVoteEnabled = true;
			this.IsCultureVoteEnabled = true;
			this.IsDisableMapVoteOverride = false;
			this.IsDisableCultureVoteOverride = false;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x0009F254 File Offset: 0x0009D454
		public void AddMapItem(string mapID)
		{
			if (!this.MapVoteItems.ContainsItem(mapID))
			{
				IntermissionVoteItem intermissionVoteItem = this.MapVoteItems.Add(mapID);
				MultiplayerIntermissionVotingManager.MapItemAddedDelegate onMapItemAdded = this.OnMapItemAdded;
				if (onMapItemAdded != null)
				{
					onMapItemAdded(intermissionVoteItem.Id);
				}
				this.SortVotesAndPickBest();
			}
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x0009F299 File Offset: 0x0009D499
		public void AddUsableMap(CustomGameUsableMap usableMap)
		{
			this.UsableMaps.Add(usableMap);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x0009F2A8 File Offset: 0x0009D4A8
		public List<string> GetUsableMaps(string gameType)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this.UsableMaps.Count; i++)
			{
				if (this.UsableMaps[i].isCompatibleWithAllGameTypes || this.UsableMaps[i].compatibleGameTypes.Contains(gameType))
				{
					list.Add(this.UsableMaps[i].map);
				}
			}
			return list;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x0009F318 File Offset: 0x0009D518
		public void AddCultureItem(string cultureID)
		{
			if (!this.CultureVoteItems.ContainsItem(cultureID))
			{
				IntermissionVoteItem intermissionVoteItem = this.CultureVoteItems.Add(cultureID);
				MultiplayerIntermissionVotingManager.CultureItemAddedDelegate onCultureItemAdded = this.OnCultureItemAdded;
				if (onCultureItemAdded != null)
				{
					onCultureItemAdded(intermissionVoteItem.Id);
				}
				this.SortVotesAndPickBest();
			}
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x0009F360 File Offset: 0x0009D560
		public void AddVote(PlayerId voterID, string itemID, int voteCount)
		{
			if (this.MapVoteItems.ContainsItem(itemID))
			{
				IntermissionVoteItem item = this.MapVoteItems.GetItem(itemID);
				item.IncreaseVoteCount(voteCount);
				MultiplayerIntermissionVotingManager.MapItemVoteCountChangedDelegate onMapItemVoteCountChanged = this.OnMapItemVoteCountChanged;
				if (onMapItemVoteCountChanged != null)
				{
					onMapItemVoteCountChanged(item.Index, item.VoteCount);
				}
			}
			else if (this.CultureVoteItems.ContainsItem(itemID))
			{
				IntermissionVoteItem item2 = this.CultureVoteItems.GetItem(itemID);
				item2.IncreaseVoteCount(voteCount);
				MultiplayerIntermissionVotingManager.CultureItemVoteCountChangedDelegate onCultureItemVoteCountChanged = this.OnCultureItemVoteCountChanged;
				if (onCultureItemVoteCountChanged != null)
				{
					onCultureItemVoteCountChanged(item2.Index, item2.VoteCount);
				}
			}
			else
			{
				Debug.FailedAssert("Item with ID does not exist.", "C:\\Develop\\MB3\\Source\\Bannerlord\\TaleWorlds.MountAndBlade\\Network\\Gameplay\\MultiplayerIntermissionVotingManager.cs", "AddVote", 116);
			}
			if (!this._votesOfPlayers.ContainsKey(voterID))
			{
				this._votesOfPlayers.Add(voterID, new List<string>());
			}
			if (voteCount == 1)
			{
				this._votesOfPlayers[voterID].Add(itemID);
			}
			else if (voteCount == -1)
			{
				this._votesOfPlayers[voterID].Remove(itemID);
			}
			this.SortVotesAndPickBest();
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x0009F459 File Offset: 0x0009D659
		public void SetVotesOfMap(int mapItemIndex, int voteCount)
		{
			this.MapVoteItems[mapItemIndex].SetVoteCount(voteCount);
			MultiplayerIntermissionVotingManager.MapItemVoteCountChangedDelegate onMapItemVoteCountChanged = this.OnMapItemVoteCountChanged;
			if (onMapItemVoteCountChanged == null)
			{
				return;
			}
			onMapItemVoteCountChanged(mapItemIndex, voteCount);
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x0009F47F File Offset: 0x0009D67F
		public void SetVotesOfCulture(int cultureItemIndex, int voteCount)
		{
			this.CultureVoteItems[cultureItemIndex].SetVoteCount(voteCount);
			MultiplayerIntermissionVotingManager.CultureItemVoteCountChangedDelegate onCultureItemVoteCountChanged = this.OnCultureItemVoteCountChanged;
			if (onCultureItemVoteCountChanged == null)
			{
				return;
			}
			onCultureItemVoteCountChanged(cultureItemIndex, voteCount);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x0009F4A8 File Offset: 0x0009D6A8
		public void ClearVotes()
		{
			foreach (IntermissionVoteItem intermissionVoteItem in this.MapVoteItems)
			{
				intermissionVoteItem.SetVoteCount(0);
				MultiplayerIntermissionVotingManager.MapItemVoteCountChangedDelegate onMapItemVoteCountChanged = this.OnMapItemVoteCountChanged;
				if (onMapItemVoteCountChanged != null)
				{
					onMapItemVoteCountChanged(intermissionVoteItem.Index, intermissionVoteItem.VoteCount);
				}
			}
			foreach (IntermissionVoteItem intermissionVoteItem2 in this.CultureVoteItems)
			{
				intermissionVoteItem2.SetVoteCount(0);
				MultiplayerIntermissionVotingManager.CultureItemVoteCountChangedDelegate onCultureItemVoteCountChanged = this.OnCultureItemVoteCountChanged;
				if (onCultureItemVoteCountChanged != null)
				{
					onCultureItemVoteCountChanged(intermissionVoteItem2.Index, intermissionVoteItem2.VoteCount);
				}
			}
			this._votesOfPlayers.Clear();
		}

		// Token: 0x06002981 RID: 10625 RVA: 0x0009F584 File Offset: 0x0009D784
		public void ClearItems()
		{
			this.MapVoteItems.Clear();
			this.CultureVoteItems.Clear();
			this._votesOfPlayers.Clear();
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0009F5A7 File Offset: 0x0009D7A7
		public bool IsCultureItem(string itemID)
		{
			return this.CultureVoteItems.ContainsItem(itemID);
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x0009F5B5 File Offset: 0x0009D7B5
		public bool IsMapItem(string itemID)
		{
			return this.MapVoteItems.ContainsItem(itemID);
		}

		// Token: 0x06002984 RID: 10628 RVA: 0x0009F5C4 File Offset: 0x0009D7C4
		public void HandlePlayerDisconnect(PlayerId playerID)
		{
			if (this._votesOfPlayers.ContainsKey(playerID))
			{
				foreach (string itemID in this._votesOfPlayers[playerID].ToList<string>())
				{
					this.AddVote(playerID, itemID, -1);
				}
				this._votesOfPlayers.Remove(playerID);
			}
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x0009F640 File Offset: 0x0009D840
		public void SelectRandomCultures()
		{
			string[] array = new string[]
			{
				"khuzait",
				"aserai",
				"battania",
				"vlandia",
				"sturgia",
				"empire"
			};
			Random random = new Random();
			string value = array[random.Next(0, array.Length)];
			string value2 = array[random.Next(0, array.Length)];
			MultiplayerOptions.OptionType.CultureTeam1.SetValue(value, MultiplayerOptions.MultiplayerOptionsAccessMode.NextMapOptions);
			MultiplayerOptions.OptionType.CultureTeam2.SetValue(value2, MultiplayerOptions.MultiplayerOptionsAccessMode.NextMapOptions);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x0009F6B6 File Offset: 0x0009D8B6
		public bool IsPeerVotedForItem(NetworkCommunicator peer, string itemID)
		{
			return this._votesOfPlayers.ContainsKey(peer.VirtualPlayer.Id) && this._votesOfPlayers[peer.VirtualPlayer.Id].Contains(itemID);
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x0009F6F0 File Offset: 0x0009D8F0
		public void SortVotesAndPickBest()
		{
			if (GameNetwork.IsServer)
			{
				if (this.IsMapVoteEnabled)
				{
					List<IntermissionVoteItem> list = this.MapVoteItems.ToList<IntermissionVoteItem>();
					if (list.Count > 1)
					{
						list.Sort((IntermissionVoteItem m1, IntermissionVoteItem m2) => -m1.VoteCount.CompareTo(m2.VoteCount));
						string id = list[0].Id;
						MultiplayerOptions.OptionType.Map.SetValue(id, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
					}
				}
				if (this.IsCultureVoteEnabled)
				{
					List<IntermissionVoteItem> list2 = this.CultureVoteItems.ToList<IntermissionVoteItem>();
					if (list2.Count > 2)
					{
						list2.Sort((IntermissionVoteItem c1, IntermissionVoteItem c2) => -c1.VoteCount.CompareTo(c2.VoteCount));
						string id2 = list2[0].Id;
						string id3 = list2[1].Id;
						if (list2[0].VoteCount > 2 * list2[1].VoteCount)
						{
							id3 = list2[0].Id;
						}
						MultiplayerOptions.OptionType.CultureTeam1.SetValue(id2, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
						MultiplayerOptions.OptionType.CultureTeam2.SetValue(id3, MultiplayerOptions.MultiplayerOptionsAccessMode.CurrentMapOptions);
					}
				}
			}
		}

		// Token: 0x04000FF9 RID: 4089
		public const int MaxAllowedMapCount = 100;

		// Token: 0x04000FFA RID: 4090
		private static MultiplayerIntermissionVotingManager _instance;

		// Token: 0x04000FFB RID: 4091
		public bool IsAutomatedBattleSwitchingEnabled;

		// Token: 0x04000FFC RID: 4092
		public bool IsMapVoteEnabled;

		// Token: 0x04000FFD RID: 4093
		public bool IsCultureVoteEnabled;

		// Token: 0x04000FFE RID: 4094
		public bool IsDisableMapVoteOverride;

		// Token: 0x04000FFF RID: 4095
		public bool IsDisableCultureVoteOverride;

		// Token: 0x04001000 RID: 4096
		public string InitialGameType;

		// Token: 0x04001004 RID: 4100
		private readonly Dictionary<PlayerId, List<string>> _votesOfPlayers;

		// Token: 0x04001005 RID: 4101
		public MultiplayerIntermissionState CurrentVoteState;

		// Token: 0x020005B7 RID: 1463
		// (Invoke) Token: 0x06003AF3 RID: 15091
		public delegate void MapItemAddedDelegate(string mapId);

		// Token: 0x020005B8 RID: 1464
		// (Invoke) Token: 0x06003AF7 RID: 15095
		public delegate void CultureItemAddedDelegate(string cultureId);

		// Token: 0x020005B9 RID: 1465
		// (Invoke) Token: 0x06003AFB RID: 15099
		public delegate void MapItemVoteCountChangedDelegate(int mapItemIndex, int voteCount);

		// Token: 0x020005BA RID: 1466
		// (Invoke) Token: 0x06003AFF RID: 15103
		public delegate void CultureItemVoteCountChangedDelegate(int cultureItemIndex, int voteCount);
	}
}
