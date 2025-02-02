using System;
using System.Collections.Generic;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000133 RID: 307
	[Serializable]
	public class GameServerProperties
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0000C085 File Offset: 0x0000A285
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0000C08D File Offset: 0x0000A28D
		public string Name { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0000C096 File Offset: 0x0000A296
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0000C09E File Offset: 0x0000A29E
		public string Address { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0000C0A7 File Offset: 0x0000A2A7
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		public int Port { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
		// (set) Token: 0x060007EF RID: 2031 RVA: 0x0000C0C0 File Offset: 0x0000A2C0
		public string Region { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0000C0C9 File Offset: 0x0000A2C9
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0000C0D1 File Offset: 0x0000A2D1
		public string GameModule { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0000C0DA File Offset: 0x0000A2DA
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0000C0E2 File Offset: 0x0000A2E2
		public string GameType { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0000C0EB File Offset: 0x0000A2EB
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0000C0F3 File Offset: 0x0000A2F3
		public string Map { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0000C104 File Offset: 0x0000A304
		public string UniqueMapId { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0000C10D File Offset: 0x0000A30D
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0000C115 File Offset: 0x0000A315
		public string GamePassword { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0000C11E File Offset: 0x0000A31E
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0000C126 File Offset: 0x0000A326
		public string AdminPassword { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0000C12F File Offset: 0x0000A32F
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0000C137 File Offset: 0x0000A337
		public int MaxPlayerCount { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0000C140 File Offset: 0x0000A340
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0000C148 File Offset: 0x0000A348
		public bool PasswordProtected { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0000C151 File Offset: 0x0000A351
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x0000C159 File Offset: 0x0000A359
		public bool IsOfficial { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0000C162 File Offset: 0x0000A362
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0000C16A File Offset: 0x0000A36A
		public bool ByOfficialProvider { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0000C173 File Offset: 0x0000A373
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0000C17B File Offset: 0x0000A37B
		public bool CrossplayEnabled { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0000C184 File Offset: 0x0000A384
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0000C18C File Offset: 0x0000A38C
		public int Permission { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0000C195 File Offset: 0x0000A395
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0000C19D File Offset: 0x0000A39D
		public PlayerId HostId { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0000C1A6 File Offset: 0x0000A3A6
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x0000C1AE File Offset: 0x0000A3AE
		public string HostName { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0000C1B7 File Offset: 0x0000A3B7
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0000C1BF File Offset: 0x0000A3BF
		public List<ModuleInfoModel> LoadedModules { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		public bool AllowsOptionalModules { get; set; }

		// Token: 0x06000810 RID: 2064 RVA: 0x0000C1D9 File Offset: 0x0000A3D9
		public GameServerProperties()
		{
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0000C1E4 File Offset: 0x0000A3E4
		public GameServerProperties(string name, string address, int port, string region, string gameModule, string gameType, string map, string uniqueMapId, string gamePassword, string adminPassword, int maxPlayerCount, bool isOfficial, bool byOfficialProvider, bool crossplayEnabled, PlayerId hostId, string hostName, List<ModuleInfoModel> loadedModules, bool allowsOptionalModules, int permission)
		{
			this.Name = name;
			this.Address = address;
			this.Port = port;
			this.Region = region;
			this.GameModule = gameModule;
			this.GameType = gameType;
			this.Map = map;
			this.GamePassword = gamePassword;
			this.UniqueMapId = uniqueMapId;
			this.AdminPassword = adminPassword;
			this.MaxPlayerCount = maxPlayerCount;
			this.IsOfficial = isOfficial;
			this.ByOfficialProvider = byOfficialProvider;
			this.CrossplayEnabled = crossplayEnabled;
			this.HostId = hostId;
			this.HostName = hostName;
			this.LoadedModules = loadedModules;
			this.AllowsOptionalModules = allowsOptionalModules;
			this.PasswordProtected = (gamePassword != null);
			this.Permission = permission;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0000C298 File Offset: 0x0000A498
		public void CheckAndReplaceProxyAddress(IReadOnlyDictionary<string, string> proxyAddressMap)
		{
			string address;
			if (proxyAddressMap != null && proxyAddressMap.TryGetValue(this.Address, out address))
			{
				this.Address = address;
			}
		}
	}
}
