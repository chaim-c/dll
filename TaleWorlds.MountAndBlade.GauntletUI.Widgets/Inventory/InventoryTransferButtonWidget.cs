using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x02000137 RID: 311
	public class InventoryTransferButtonWidget : ButtonWidget
	{
		// Token: 0x06001070 RID: 4208 RVA: 0x0002DABB File Offset: 0x0002BCBB
		public InventoryTransferButtonWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0002DAC4 File Offset: 0x0002BCC4
		public void FireClickEvent()
		{
			if (this.IsSell)
			{
				base.EventFired("SellAction", Array.Empty<object>());
				return;
			}
			base.EventFired("BuyAction", Array.Empty<object>());
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0002DAF0 File Offset: 0x0002BCF0
		private void HandleVisuals()
		{
			int index;
			Brush brush;
			if (this.IsSell)
			{
				index = 0;
				brush = this.SellBrush;
			}
			else
			{
				index = base.ParentWidget.ParentWidget.ChildCount - 1;
				brush = this.BuyBrush;
			}
			if (this.ModifySiblingIndex)
			{
				base.ParentWidget.SetSiblingIndex(index, false);
			}
			base.Brush = brush;
		}

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001073 RID: 4211 RVA: 0x0002DB46 File Offset: 0x0002BD46
		// (set) Token: 0x06001074 RID: 4212 RVA: 0x0002DB4E File Offset: 0x0002BD4E
		[Editor(false)]
		public bool IsSell
		{
			get
			{
				return this._isSell;
			}
			set
			{
				if (this._isSell != value)
				{
					this._isSell = value;
					this.HandleVisuals();
					base.OnPropertyChanged(value, "IsSell");
				}
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001075 RID: 4213 RVA: 0x0002DB72 File Offset: 0x0002BD72
		// (set) Token: 0x06001076 RID: 4214 RVA: 0x0002DB7A File Offset: 0x0002BD7A
		[Editor(false)]
		public bool ModifySiblingIndex
		{
			get
			{
				return this._modifySiblingIndex;
			}
			set
			{
				if (this._modifySiblingIndex != value)
				{
					this._modifySiblingIndex = value;
					this.HandleVisuals();
					base.OnPropertyChanged(value, "ModifySiblingIndex");
				}
			}
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x0002DB9E File Offset: 0x0002BD9E
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x0002DBA6 File Offset: 0x0002BDA6
		[Editor(false)]
		public Brush BuyBrush
		{
			get
			{
				return this._buyBrush;
			}
			set
			{
				if (this._buyBrush != value)
				{
					this._buyBrush = value;
					base.OnPropertyChanged<Brush>(value, "BuyBrush");
				}
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x0002DBC4 File Offset: 0x0002BDC4
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x0002DBCC File Offset: 0x0002BDCC
		[Editor(false)]
		public Brush SellBrush
		{
			get
			{
				return this._sellBrush;
			}
			set
			{
				if (this._sellBrush != value)
				{
					this._sellBrush = value;
					base.OnPropertyChanged<Brush>(value, "SellBrush");
				}
			}
		}

		// Token: 0x0400077D RID: 1917
		private bool _isSell;

		// Token: 0x0400077E RID: 1918
		private bool _modifySiblingIndex;

		// Token: 0x0400077F RID: 1919
		private Brush _buyBrush;

		// Token: 0x04000780 RID: 1920
		private Brush _sellBrush;
	}
}
