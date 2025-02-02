using System;
using TaleWorlds.Core;

namespace TaleWorlds.CampaignSystem.GameState
{
	// Token: 0x02000331 RID: 817
	public class CraftingState : GameState
	{
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000C1E96 File Offset: 0x000C0096
		public override bool IsMenuState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002E89 RID: 11913 RVA: 0x000C1E99 File Offset: 0x000C0099
		// (set) Token: 0x06002E8A RID: 11914 RVA: 0x000C1EA1 File Offset: 0x000C00A1
		public Crafting CraftingLogic { get; private set; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002E8B RID: 11915 RVA: 0x000C1EAA File Offset: 0x000C00AA
		// (set) Token: 0x06002E8C RID: 11916 RVA: 0x000C1EB2 File Offset: 0x000C00B2
		public ICraftingStateHandler Handler
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

		// Token: 0x06002E8D RID: 11917 RVA: 0x000C1EBB File Offset: 0x000C00BB
		public void InitializeLogic(Crafting newCraftingLogic, bool isReplacingWeaponClass = false)
		{
			this.CraftingLogic = newCraftingLogic;
			if (this._handler != null)
			{
				if (isReplacingWeaponClass)
				{
					this._handler.OnCraftingLogicRefreshed();
					return;
				}
				this._handler.OnCraftingLogicInitialized();
			}
		}

		// Token: 0x04000DEB RID: 3563
		private ICraftingStateHandler _handler;
	}
}
