using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Library.EventSystem;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.Party
{
	// Token: 0x0200002C RID: 44
	public class PlayerMoveTroopEvent : EventBase
	{
		// Token: 0x17000141 RID: 321
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00018DAE File Offset: 0x00016FAE
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00018DB6 File Offset: 0x00016FB6
		public CharacterObject Troop { get; private set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00018DBF File Offset: 0x00016FBF
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00018DC7 File Offset: 0x00016FC7
		public int Amount { get; private set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00018DD0 File Offset: 0x00016FD0
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00018DD8 File Offset: 0x00016FD8
		public bool IsPrisoner { get; private set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x00018DE1 File Offset: 0x00016FE1
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00018DE9 File Offset: 0x00016FE9
		public PartyScreenLogic.PartyRosterSide FromSide { get; private set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00018DF2 File Offset: 0x00016FF2
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00018DFA File Offset: 0x00016FFA
		public PartyScreenLogic.PartyRosterSide ToSide { get; private set; }

		// Token: 0x06000464 RID: 1124 RVA: 0x00018E03 File Offset: 0x00017003
		public PlayerMoveTroopEvent(CharacterObject troop, PartyScreenLogic.PartyRosterSide fromSide, PartyScreenLogic.PartyRosterSide toSide, int amount, bool isPrisoner)
		{
			this.Troop = troop;
			this.FromSide = fromSide;
			this.ToSide = toSide;
			this.IsPrisoner = isPrisoner;
			this.Amount = amount;
		}
	}
}
