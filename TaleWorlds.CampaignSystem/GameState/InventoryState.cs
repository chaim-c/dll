using System;
using TaleWorlds.CampaignSystem.Inventory;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000338 RID: 824
	public class InventoryState : PlayerGameState
	{
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06002EAC RID: 11948 RVA: 0x000C1FEE File Offset: 0x000C01EE
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06002EAD RID: 11949 RVA: 0x000C1FF1 File Offset: 0x000C01F1
		// (set) Token: 0x06002EAE RID: 11950 RVA: 0x000C1FF9 File Offset: 0x000C01F9
		public InventoryLogic InventoryLogic { get; private set; }

		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06002EAF RID: 11951 RVA: 0x000C2002 File Offset: 0x000C0202
		// (set) Token: 0x06002EB0 RID: 11952 RVA: 0x000C200A File Offset: 0x000C020A
		public IInventoryStateHandler Handler
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

		// Token: 0x06002EB1 RID: 11953 RVA: 0x000C2013 File Offset: 0x000C0213
		public void InitializeLogic(InventoryLogic inventoryLogic)
		{
			this.InventoryLogic = inventoryLogic;
		}

		// Token: 0x04000DF1 RID: 3569
		private IInventoryStateHandler _handler;
	}
}
