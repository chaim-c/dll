using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x02000131 RID: 305
	[Serializable]
	public class PlayerJoinGameData
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x0000BF0B File Offset: 0x0000A10B
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x0000BF13 File Offset: 0x0000A113
		public PlayerData PlayerData { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0000BF1C File Offset: 0x0000A11C
		public PlayerId PlayerId
		{
			get
			{
				return this.PlayerData.PlayerId;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0000BF29 File Offset: 0x0000A129
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0000BF31 File Offset: 0x0000A131
		public string Name { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0000BF3A File Offset: 0x0000A13A
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0000BF42 File Offset: 0x0000A142
		public Guid? PartyId { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0000BF4B File Offset: 0x0000A14B
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0000BF53 File Offset: 0x0000A153
		public Dictionary<string, List<string>> UsedCosmetics { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0000BF5C File Offset: 0x0000A15C
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0000BF64 File Offset: 0x0000A164
		[JsonProperty]
		public string IpAddress { get; private set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0000BF6D File Offset: 0x0000A16D
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0000BF75 File Offset: 0x0000A175
		[JsonProperty]
		public bool IsAdmin { get; private set; }

		// Token: 0x060007DA RID: 2010 RVA: 0x0000BF7E File Offset: 0x0000A17E
		public PlayerJoinGameData()
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x0000BF86 File Offset: 0x0000A186
		public PlayerJoinGameData(PlayerData playerData, string name, Guid? partyId, Dictionary<string, List<string>> usedCosmetics, string ipAddress, bool isAdmin)
		{
			this.PlayerData = playerData;
			this.Name = name;
			this.PartyId = partyId;
			this.UsedCosmetics = usedCosmetics;
			this.IpAddress = ipAddress;
			this.IsAdmin = isAdmin;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public override string ToString()
		{
			return string.Format("Player Join Game Data: {0}, name={1}, party={2}, cosmetics={3}, ip={4}, isAdmin={5}", new object[]
			{
				this.PlayerId,
				this.Name,
				this.PartyId,
				this.UsedCosmetics.Count,
				this.IpAddress,
				this.IsAdmin
			});
		}
	}
}
