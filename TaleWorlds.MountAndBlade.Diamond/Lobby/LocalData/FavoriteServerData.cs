using System;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x0200017A RID: 378
	public class FavoriteServerData : MultiplayerLocalData
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00011503 File Offset: 0x0000F703
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0001150B File Offset: 0x0000F70B
		public string Address { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00011514 File Offset: 0x0000F714
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x0001151C File Offset: 0x0000F71C
		public int Port { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00011525 File Offset: 0x0000F725
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x0001152D File Offset: 0x0000F72D
		public string GameType { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x00011536 File Offset: 0x0000F736
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x0001153E File Offset: 0x0000F73E
		public string Name { get; set; }

		// Token: 0x06000A7F RID: 2687 RVA: 0x00011547 File Offset: 0x0000F747
		private FavoriteServerData()
		{
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0001154F File Offset: 0x0000F74F
		public static FavoriteServerData CreateFrom(GameServerEntry serverEntry)
		{
			if (serverEntry == null)
			{
				return null;
			}
			return new FavoriteServerData
			{
				Address = serverEntry.Address,
				Port = serverEntry.Port,
				GameType = serverEntry.GameType,
				Name = serverEntry.ServerName
			};
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0001158C File Offset: 0x0000F78C
		public override bool HasSameContentWith(MultiplayerLocalData other)
		{
			FavoriteServerData favoriteServerData;
			return (favoriteServerData = (other as FavoriteServerData)) != null && (this.Address == favoriteServerData.Address && this.Port == favoriteServerData.Port && this.GameType == favoriteServerData.GameType) && this.Name == favoriteServerData.Name;
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x000115EC File Offset: 0x0000F7EC
		public bool HasSameContentWith(GameServerEntry serverEntry)
		{
			return this.Address == serverEntry.Address && this.Port == serverEntry.Port && this.GameType == serverEntry.GameType && this.Name == serverEntry.ServerName;
		}
	}
}
