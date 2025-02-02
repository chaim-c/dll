using System;
using TaleWorlds.MountAndBlade.Diamond;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200030E RID: 782
	public static class NetworkMain
	{
		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06002A95 RID: 10901 RVA: 0x000A537A File Offset: 0x000A357A
		// (set) Token: 0x06002A96 RID: 10902 RVA: 0x000A5381 File Offset: 0x000A3581
		public static LobbyClient GameClient { get; private set; }

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002A97 RID: 10903 RVA: 0x000A5389 File Offset: 0x000A3589
		// (set) Token: 0x06002A98 RID: 10904 RVA: 0x000A5390 File Offset: 0x000A3590
		public static CommunityClient CommunityClient { get; private set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06002A99 RID: 10905 RVA: 0x000A5398 File Offset: 0x000A3598
		// (set) Token: 0x06002A9A RID: 10906 RVA: 0x000A539F File Offset: 0x000A359F
		public static CustomBattleServer CustomBattleServer { get; private set; }

		// Token: 0x06002A9B RID: 10907 RVA: 0x000A53A7 File Offset: 0x000A35A7
		public static void SetPeers(LobbyClient gameClient, CommunityClient communityClient, CustomBattleServer customBattleServer)
		{
			NetworkMain.GameClient = gameClient;
			NetworkMain.CommunityClient = communityClient;
			NetworkMain.CustomBattleServer = customBattleServer;
		}
	}
}
