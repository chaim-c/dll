using System;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000231 RID: 561
	public class GameStartupInfo
	{
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001EB6 RID: 7862 RVA: 0x0006E201 File Offset: 0x0006C401
		// (set) Token: 0x06001EB7 RID: 7863 RVA: 0x0006E209 File Offset: 0x0006C409
		public GameStartupType StartupType { get; internal set; }

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x0006E212 File Offset: 0x0006C412
		// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x0006E21A File Offset: 0x0006C41A
		public DedicatedServerType DedicatedServerType { get; internal set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x0006E223 File Offset: 0x0006C423
		// (set) Token: 0x06001EBB RID: 7867 RVA: 0x0006E22B File Offset: 0x0006C42B
		public bool PlayerHostedDedicatedServer { get; internal set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x0006E234 File Offset: 0x0006C434
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x0006E23C File Offset: 0x0006C43C
		public bool IsSinglePlatformServer { get; internal set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x0006E245 File Offset: 0x0006C445
		// (set) Token: 0x06001EBF RID: 7871 RVA: 0x0006E24D File Offset: 0x0006C44D
		public string CustomServerHostIP { get; internal set; } = string.Empty;

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001EC0 RID: 7872 RVA: 0x0006E256 File Offset: 0x0006C456
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x0006E25E File Offset: 0x0006C45E
		public int ServerPort { get; internal set; }

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x0006E267 File Offset: 0x0006C467
		// (set) Token: 0x06001EC3 RID: 7875 RVA: 0x0006E26F File Offset: 0x0006C46F
		public string ServerRegion { get; internal set; }

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001EC4 RID: 7876 RVA: 0x0006E278 File Offset: 0x0006C478
		// (set) Token: 0x06001EC5 RID: 7877 RVA: 0x0006E280 File Offset: 0x0006C480
		public sbyte ServerPriority { get; internal set; }

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001EC6 RID: 7878 RVA: 0x0006E289 File Offset: 0x0006C489
		// (set) Token: 0x06001EC7 RID: 7879 RVA: 0x0006E291 File Offset: 0x0006C491
		public string ServerGameMode { get; internal set; }

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001EC8 RID: 7880 RVA: 0x0006E29A File Offset: 0x0006C49A
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x0006E2A2 File Offset: 0x0006C4A2
		public string CustomGameServerConfigFile { get; internal set; }

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001ECA RID: 7882 RVA: 0x0006E2AB File Offset: 0x0006C4AB
		// (set) Token: 0x06001ECB RID: 7883 RVA: 0x0006E2B3 File Offset: 0x0006C4B3
		public string CustomGameServerNameOverride { get; internal set; }

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001ECC RID: 7884 RVA: 0x0006E2BC File Offset: 0x0006C4BC
		// (set) Token: 0x06001ECD RID: 7885 RVA: 0x0006E2C4 File Offset: 0x0006C4C4
		public string CustomGameServerPasswordOverride { get; internal set; }

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001ECE RID: 7886 RVA: 0x0006E2CD File Offset: 0x0006C4CD
		// (set) Token: 0x06001ECF RID: 7887 RVA: 0x0006E2D5 File Offset: 0x0006C4D5
		public string CustomGameServerAuthToken { get; internal set; }

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001ED0 RID: 7888 RVA: 0x0006E2DE File Offset: 0x0006C4DE
		// (set) Token: 0x06001ED1 RID: 7889 RVA: 0x0006E2E6 File Offset: 0x0006C4E6
		public bool CustomGameServerAllowsOptionalModules { get; internal set; } = true;

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001ED2 RID: 7890 RVA: 0x0006E2EF File Offset: 0x0006C4EF
		// (set) Token: 0x06001ED3 RID: 7891 RVA: 0x0006E2F7 File Offset: 0x0006C4F7
		public string OverridenUserName { get; internal set; }

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x0006E300 File Offset: 0x0006C500
		// (set) Token: 0x06001ED5 RID: 7893 RVA: 0x0006E308 File Offset: 0x0006C508
		public string PremadeGameType { get; internal set; }

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x0006E311 File Offset: 0x0006C511
		// (set) Token: 0x06001ED7 RID: 7895 RVA: 0x0006E319 File Offset: 0x0006C519
		public int Permission { get; internal set; }

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x0006E322 File Offset: 0x0006C522
		// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x0006E32A File Offset: 0x0006C52A
		public string EpicExchangeCode { get; internal set; }

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x0006E333 File Offset: 0x0006C533
		// (set) Token: 0x06001EDB RID: 7899 RVA: 0x0006E33B File Offset: 0x0006C53B
		public bool IsContinueGame { get; internal set; }

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x0006E344 File Offset: 0x0006C544
		// (set) Token: 0x06001EDD RID: 7901 RVA: 0x0006E34C File Offset: 0x0006C54C
		public double ServerBandwidthLimitInMbps { get; internal set; }

		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x0006E355 File Offset: 0x0006C555
		// (set) Token: 0x06001EDF RID: 7903 RVA: 0x0006E35D File Offset: 0x0006C55D
		public int ServerTickRate { get; internal set; }
	}
}
