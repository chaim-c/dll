using System;

namespace TaleWorlds.CampaignSystem.Party
{
	// Token: 0x020002A8 RID: 680
	public struct TroopTradeDifference
	{
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x000A8A10 File Offset: 0x000A6C10
		// (set) Token: 0x06002770 RID: 10096 RVA: 0x000A8A18 File Offset: 0x000A6C18
		public CharacterObject Troop { get; set; }

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002771 RID: 10097 RVA: 0x000A8A21 File Offset: 0x000A6C21
		// (set) Token: 0x06002772 RID: 10098 RVA: 0x000A8A29 File Offset: 0x000A6C29
		public bool IsPrisoner { get; set; }

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002773 RID: 10099 RVA: 0x000A8A32 File Offset: 0x000A6C32
		// (set) Token: 0x06002774 RID: 10100 RVA: 0x000A8A3A File Offset: 0x000A6C3A
		public int FromCount { get; set; }

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002775 RID: 10101 RVA: 0x000A8A43 File Offset: 0x000A6C43
		// (set) Token: 0x06002776 RID: 10102 RVA: 0x000A8A4B File Offset: 0x000A6C4B
		public int ToCount { get; set; }

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002777 RID: 10103 RVA: 0x000A8A54 File Offset: 0x000A6C54
		public int DifferenceCount
		{
			get
			{
				return this.FromCount - this.ToCount;
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002778 RID: 10104 RVA: 0x000A8A63 File Offset: 0x000A6C63
		// (set) Token: 0x06002779 RID: 10105 RVA: 0x000A8A6B File Offset: 0x000A6C6B
		public bool IsEmpty { get; private set; }

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600277A RID: 10106 RVA: 0x000A8A74 File Offset: 0x000A6C74
		public static TroopTradeDifference Empty
		{
			get
			{
				return new TroopTradeDifference
				{
					IsEmpty = true
				};
			}
		}
	}
}
