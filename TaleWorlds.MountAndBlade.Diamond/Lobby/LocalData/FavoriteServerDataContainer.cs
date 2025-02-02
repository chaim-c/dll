using System;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.Diamond.Lobby.LocalData
{
	// Token: 0x0200017B RID: 379
	public class FavoriteServerDataContainer : MultiplayerLocalDataContainer<FavoriteServerData>
	{
		// Token: 0x06000A83 RID: 2691 RVA: 0x00011640 File Offset: 0x0000F840
		protected override string GetSaveDirectoryName()
		{
			return "Data";
		}

		// Token: 0x06000A84 RID: 2692 RVA: 0x00011647 File Offset: 0x0000F847
		protected override string GetSaveFileName()
		{
			return "FavoriteServers.json";
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x00011650 File Offset: 0x0000F850
		public bool TryGetServerData(GameServerEntry serverEntry, out FavoriteServerData favoriteServerData)
		{
			favoriteServerData = null;
			MBReadOnlyList<FavoriteServerData> entries = base.GetEntries();
			for (int i = 0; i < entries.Count; i++)
			{
				FavoriteServerData favoriteServerData2 = entries[i];
				if (favoriteServerData2.HasSameContentWith(serverEntry))
				{
					favoriteServerData = favoriteServerData2;
					return true;
				}
			}
			return false;
		}
	}
}
