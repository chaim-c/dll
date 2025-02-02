using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.CampaignSystem.Settlements;
using TaleWorlds.CampaignSystem.Settlements.Workshops;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x0200032F RID: 815
	public class ClanState : GameState
	{
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002E75 RID: 11893 RVA: 0x000C1DDA File Offset: 0x000BFFDA
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002E76 RID: 11894 RVA: 0x000C1DDD File Offset: 0x000BFFDD
		// (set) Token: 0x06002E77 RID: 11895 RVA: 0x000C1DE5 File Offset: 0x000BFFE5
		public Hero InitialSelectedHero { get; private set; }

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06002E78 RID: 11896 RVA: 0x000C1DEE File Offset: 0x000BFFEE
		// (set) Token: 0x06002E79 RID: 11897 RVA: 0x000C1DF6 File Offset: 0x000BFFF6
		public PartyBase InitialSelectedParty { get; private set; }

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06002E7A RID: 11898 RVA: 0x000C1DFF File Offset: 0x000BFFFF
		// (set) Token: 0x06002E7B RID: 11899 RVA: 0x000C1E07 File Offset: 0x000C0007
		public Settlement InitialSelectedSettlement { get; private set; }

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002E7C RID: 11900 RVA: 0x000C1E10 File Offset: 0x000C0010
		// (set) Token: 0x06002E7D RID: 11901 RVA: 0x000C1E18 File Offset: 0x000C0018
		public Workshop InitialSelectedWorkshop { get; private set; }

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002E7E RID: 11902 RVA: 0x000C1E21 File Offset: 0x000C0021
		// (set) Token: 0x06002E7F RID: 11903 RVA: 0x000C1E29 File Offset: 0x000C0029
		public Alley InitialSelectedAlley { get; private set; }

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002E80 RID: 11904 RVA: 0x000C1E32 File Offset: 0x000C0032
		// (set) Token: 0x06002E81 RID: 11905 RVA: 0x000C1E3A File Offset: 0x000C003A
		public IClanStateHandler Handler
		{
			get
			{
				return this._handler;
			}
			set
			{
				this._handler = value;
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x000C1E43 File Offset: 0x000C0043
		public ClanState()
		{
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000C1E4B File Offset: 0x000C004B
		public ClanState(Hero initialSelectedHero)
		{
			this.InitialSelectedHero = initialSelectedHero;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x000C1E5A File Offset: 0x000C005A
		public ClanState(PartyBase initialSelectedParty)
		{
			this.InitialSelectedParty = initialSelectedParty;
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x000C1E69 File Offset: 0x000C0069
		public ClanState(Settlement initialSelectedSettlement)
		{
			this.InitialSelectedSettlement = initialSelectedSettlement;
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000C1E78 File Offset: 0x000C0078
		public ClanState(Workshop initialSelectedWorkshop)
		{
			this.InitialSelectedWorkshop = initialSelectedWorkshop;
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x000C1E87 File Offset: 0x000C0087
		public ClanState(Alley initialSelectedAlley)
		{
			this.InitialSelectedAlley = initialSelectedAlley;
		}

		// Token: 0x04000DE9 RID: 3561
		private IClanStateHandler _handler;
	}
}
