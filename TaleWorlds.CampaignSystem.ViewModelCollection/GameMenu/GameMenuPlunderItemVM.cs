using System;
using TaleWorlds.Core;
using TaleWorlds.Library;

namespace TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu
{
	// Token: 0x0200008C RID: 140
	public class GameMenuPlunderItemVM : ViewModel
	{
		// Token: 0x06000DD4 RID: 3540 RVA: 0x00037DE0 File Offset: 0x00035FE0
		public GameMenuPlunderItemVM(ItemRosterElement item)
		{
			this._item = item;
			this.Visual = new ImageIdentifierVM(item.EquipmentElement.Item, "");
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00037E1C File Offset: 0x0003601C
		public void ExecuteBeginTooltip()
		{
			if (this._item.EquipmentElement.Item != null)
			{
				InformationManager.ShowTooltip(typeof(ItemObject), new object[]
				{
					this._item.EquipmentElement
				});
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00037E66 File Offset: 0x00036066
		public void ExecuteEndTooltip()
		{
			MBInformationManager.HideInformations();
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00037E6D File Offset: 0x0003606D
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x00037E75 File Offset: 0x00036075
		[DataSourceProperty]
		public ImageIdentifierVM Visual
		{
			get
			{
				return this._visual;
			}
			set
			{
				if (value != this._visual)
				{
					this._visual = value;
					base.OnPropertyChangedWithValue<ImageIdentifierVM>(value, "Visual");
				}
			}
		}

		// Token: 0x04000660 RID: 1632
		private ItemRosterElement _item;

		// Token: 0x04000661 RID: 1633
		private ImageIdentifierVM _visual;
	}
}
