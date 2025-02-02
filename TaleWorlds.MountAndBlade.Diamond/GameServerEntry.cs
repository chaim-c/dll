using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.Library;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200011F RID: 287
	[Serializable]
	public class GameServerEntry
	{
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x00008458 File Offset: 0x00006658
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x00008460 File Offset: 0x00006660
		[JsonProperty]
		public CustomBattleId Id { get; private set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x00008469 File Offset: 0x00006669
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x00008471 File Offset: 0x00006671
		[JsonProperty]
		public string Address { get; private set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000847A File Offset: 0x0000667A
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x00008482 File Offset: 0x00006682
		[JsonProperty]
		public int Port { get; private set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0000848B File Offset: 0x0000668B
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x00008493 File Offset: 0x00006693
		[JsonProperty]
		public string Region { get; private set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000849C File Offset: 0x0000669C
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x000084A4 File Offset: 0x000066A4
		[JsonProperty]
		public int PlayerCount { get; private set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x000084AD File Offset: 0x000066AD
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x000084B5 File Offset: 0x000066B5
		[JsonProperty]
		public int MaxPlayerCount { get; private set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x000084BE File Offset: 0x000066BE
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x000084C6 File Offset: 0x000066C6
		[JsonProperty]
		public string ServerName { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x000084CF File Offset: 0x000066CF
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x000084D7 File Offset: 0x000066D7
		[JsonProperty]
		public string GameModule { get; private set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x000084E0 File Offset: 0x000066E0
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x000084E8 File Offset: 0x000066E8
		[JsonProperty]
		public string GameType { get; private set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000084F1 File Offset: 0x000066F1
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x000084F9 File Offset: 0x000066F9
		[JsonProperty]
		public string Map { get; private set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x00008502 File Offset: 0x00006702
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x0000850A File Offset: 0x0000670A
		[JsonProperty]
		public string UniqueMapId { get; private set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x00008513 File Offset: 0x00006713
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0000851B File Offset: 0x0000671B
		[JsonProperty]
		public int Ping { get; private set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x00008524 File Offset: 0x00006724
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0000852C File Offset: 0x0000672C
		[JsonProperty]
		public bool IsOfficial { get; private set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00008535 File Offset: 0x00006735
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0000853D File Offset: 0x0000673D
		[JsonProperty]
		public bool ByOfficialProvider { get; private set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x00008546 File Offset: 0x00006746
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x0000854E File Offset: 0x0000674E
		[JsonProperty]
		public bool PasswordProtected { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x00008557 File Offset: 0x00006757
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0000855F File Offset: 0x0000675F
		[JsonProperty]
		public int Permission { get; private set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00008568 File Offset: 0x00006768
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x00008570 File Offset: 0x00006770
		[JsonProperty]
		public bool CrossplayEnabled { get; private set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x00008579 File Offset: 0x00006779
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x00008581 File Offset: 0x00006781
		[JsonProperty]
		public PlayerId HostId { get; private set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0000858A File Offset: 0x0000678A
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x00008592 File Offset: 0x00006792
		[JsonProperty]
		public string HostName { get; private set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000859B File Offset: 0x0000679B
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x000085A3 File Offset: 0x000067A3
		[JsonProperty]
		public List<ModuleInfoModel> LoadedModules { get; private set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x000085AC File Offset: 0x000067AC
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x000085B4 File Offset: 0x000067B4
		[JsonProperty]
		public bool AllowsOptionalModules { get; private set; }

		// Token: 0x0600066B RID: 1643 RVA: 0x000085BD File Offset: 0x000067BD
		public GameServerEntry()
		{
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x000085C8 File Offset: 0x000067C8
		public GameServerEntry(CustomBattleId id, string serverName, string address, int port, string region, string gameModule, string gameType, string map, string uniqueMapId, int playerCount, int maxPlayerCount, bool isOfficial, bool byOfficialProvider, bool crossplayEnabled, PlayerId hostId, string hostName, List<ModuleInfoModel> loadedModules, bool allowsOptionalModules, bool passwordProtected = false, int permission = 0)
		{
			this.Id = id;
			this.ServerName = serverName;
			this.Address = address;
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.Map = map;
			this.UniqueMapId = uniqueMapId;
			this.PlayerCount = playerCount;
			this.MaxPlayerCount = maxPlayerCount;
			this.Port = port;
			this.Region = region;
			this.IsOfficial = isOfficial;
			this.ByOfficialProvider = byOfficialProvider;
			this.CrossplayEnabled = crossplayEnabled;
			this.HostId = hostId;
			this.HostName = hostName;
			this.LoadedModules = loadedModules;
			this.AllowsOptionalModules = allowsOptionalModules;
			this.PasswordProtected = passwordProtected;
			this.Permission = permission;
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x00008678 File Offset: 0x00006878
		public static void FilterGameServerEntriesBasedOnCrossplay(ref List<GameServerEntry> serverList, bool hasCrossplayPrivilege)
		{
			bool flag = ApplicationPlatform.CurrentPlatform == Platform.GDKDesktop;
			if (flag && !hasCrossplayPrivilege)
			{
				serverList.RemoveAll((GameServerEntry s) => s.CrossplayEnabled);
				return;
			}
			if (!flag)
			{
				serverList.RemoveAll((GameServerEntry s) => !s.CrossplayEnabled);
			}
		}
	}
}
