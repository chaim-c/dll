using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x020000EE RID: 238
	[Serializable]
	public class AvailableCustomGames
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x000051FE File Offset: 0x000033FE
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x00005206 File Offset: 0x00003406
		[JsonProperty]
		public List<GameServerEntry> CustomGameServerInfos { get; private set; }

		// Token: 0x06000488 RID: 1160 RVA: 0x0000520F File Offset: 0x0000340F
		public AvailableCustomGames()
		{
			this.CustomGameServerInfos = new List<GameServerEntry>();
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00005224 File Offset: 0x00003424
		public AvailableCustomGames GetCustomGamesByPermission(int playerPermission)
		{
			AvailableCustomGames availableCustomGames = new AvailableCustomGames();
			foreach (GameServerEntry gameServerEntry in this.CustomGameServerInfos)
			{
				if (gameServerEntry.Permission <= playerPermission)
				{
					availableCustomGames.CustomGameServerInfos.Add(gameServerEntry);
				}
			}
			return availableCustomGames;
		}
	}
}
