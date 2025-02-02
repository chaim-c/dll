using System;
using TaleWorlds.PlayerServices;

namespace TaleWorlds.MountAndBlade.Diamond
{
	// Token: 0x0200012E RID: 302
	public class PartyPlayerInLobbyClient
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0000BE48 File Offset: 0x0000A048
		// (set) Token: 0x060007BC RID: 1980 RVA: 0x0000BE50 File Offset: 0x0000A050
		public PlayerId PlayerId { get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0000BE59 File Offset: 0x0000A059
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0000BE61 File Offset: 0x0000A061
		public string Name { get; private set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0000BE6A File Offset: 0x0000A06A
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0000BE72 File Offset: 0x0000A072
		public bool WaitingInvitation { get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0000BE7B File Offset: 0x0000A07B
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0000BE83 File Offset: 0x0000A083
		public bool IsPartyLeader { get; private set; }

		// Token: 0x060007C3 RID: 1987 RVA: 0x0000BE8C File Offset: 0x0000A08C
		public PartyPlayerInLobbyClient(PlayerId playerId, string name, bool isPartyLeader = false)
		{
			this.PlayerId = playerId;
			this.Name = name;
			this.IsPartyLeader = isPartyLeader;
			this.WaitingInvitation = true;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0000BEB0 File Offset: 0x0000A0B0
		public void SetAtParty()
		{
			this.WaitingInvitation = false;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0000BEB9 File Offset: 0x0000A0B9
		public void SetLeader()
		{
			this.IsPartyLeader = true;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0000BEC2 File Offset: 0x0000A0C2
		public void SetMember()
		{
			this.IsPartyLeader = false;
		}
	}
}
