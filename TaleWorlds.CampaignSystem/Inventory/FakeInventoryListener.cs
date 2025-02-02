using System;
using TaleWorlds.CampaignSystem.Party;
using TaleWorlds.Localization;

namespace TaleWorlds.CampaignSystem.Inventory
{
	// Token: 0x020000CE RID: 206
	public class FakeInventoryListener : InventoryListener
	{
		// Token: 0x060012D1 RID: 4817 RVA: 0x00055D01 File Offset: 0x00053F01
		public override int GetGold()
		{
			return 0;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00055D04 File Offset: 0x00053F04
		public override TextObject GetTraderName()
		{
			return TextObject.Empty;
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00055D0B File Offset: 0x00053F0B
		public override void SetGold(int gold)
		{
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00055D0D File Offset: 0x00053F0D
		public override void OnTransaction()
		{
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00055D0F File Offset: 0x00053F0F
		public override PartyBase GetOppositeParty()
		{
			return null;
		}
	}
}
