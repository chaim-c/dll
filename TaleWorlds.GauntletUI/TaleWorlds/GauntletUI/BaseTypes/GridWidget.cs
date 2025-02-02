using System;
using System.Numerics;
using TaleWorlds.GauntletUI.Layout;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000060 RID: 96
	public class GridWidget : Container
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001B353 File Offset: 0x00019553
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0001B35B File Offset: 0x0001955B
		public GridLayout GridLayout { get; private set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001B364 File Offset: 0x00019564
		// (set) Token: 0x06000639 RID: 1593 RVA: 0x0001B36C File Offset: 0x0001956C
		[Editor(false)]
		public float DefaultCellWidth
		{
			get
			{
				return this._defaultCellWidth;
			}
			set
			{
				if (this._defaultCellWidth != value)
				{
					this._defaultCellWidth = value;
					base.OnPropertyChanged(value, "DefaultCellWidth");
				}
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600063A RID: 1594 RVA: 0x0001B38A File Offset: 0x0001958A
		public float DefaultScaledCellWidth
		{
			get
			{
				return this.DefaultCellWidth * base._scaleToUse;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0001B399 File Offset: 0x00019599
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0001B3A1 File Offset: 0x000195A1
		[Editor(false)]
		public float DefaultCellHeight
		{
			get
			{
				return this._defaultCellHeight;
			}
			set
			{
				if (this._defaultCellHeight != value)
				{
					this._defaultCellHeight = value;
					base.OnPropertyChanged(value, "DefaultCellHeight");
				}
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0001B3BF File Offset: 0x000195BF
		public float DefaultScaledCellHeight
		{
			get
			{
				return this.DefaultCellHeight * base._scaleToUse;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x0600063E RID: 1598 RVA: 0x0001B3CE File Offset: 0x000195CE
		// (set) Token: 0x0600063F RID: 1599 RVA: 0x0001B3D6 File Offset: 0x000195D6
		[Editor(false)]
		public int RowCount
		{
			get
			{
				return this._rowCount;
			}
			set
			{
				if (this._rowCount != value)
				{
					this._rowCount = value;
					base.OnPropertyChanged(value, "RowCount");
				}
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0001B3F4 File Offset: 0x000195F4
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x0001B3FC File Offset: 0x000195FC
		[Editor(false)]
		public int ColumnCount
		{
			get
			{
				return this._columnCount;
			}
			set
			{
				if (this._columnCount != value)
				{
					this._columnCount = value;
					base.OnPropertyChanged(value, "ColumnCount");
				}
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001B41A File Offset: 0x0001961A
		// (set) Token: 0x06000643 RID: 1603 RVA: 0x0001B422 File Offset: 0x00019622
		[Editor(false)]
		public bool UseDynamicCellWidth
		{
			get
			{
				return this._useDynamicCellWidth;
			}
			set
			{
				if (this._useDynamicCellWidth != value)
				{
					this._useDynamicCellWidth = value;
					base.OnPropertyChanged(value, "UseDynamicCellWidth");
				}
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000644 RID: 1604 RVA: 0x0001B440 File Offset: 0x00019640
		// (set) Token: 0x06000645 RID: 1605 RVA: 0x0001B448 File Offset: 0x00019648
		[Editor(false)]
		public bool UseDynamicCellHeight
		{
			get
			{
				return this._useDynamicCellHeight;
			}
			set
			{
				if (this._useDynamicCellHeight != value)
				{
					this._useDynamicCellHeight = value;
					base.OnPropertyChanged(value, "UseDynamicCellHeight");
				}
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000646 RID: 1606 RVA: 0x0001B466 File Offset: 0x00019666
		// (set) Token: 0x06000647 RID: 1607 RVA: 0x0001B46E File Offset: 0x0001966E
		public override Predicate<Widget> AcceptDropPredicate { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x0001B477 File Offset: 0x00019677
		public override bool IsDragHovering
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001B47A File Offset: 0x0001967A
		public GridWidget(UIContext context) : base(context)
		{
			this.GridLayout = new GridLayout();
			base.LayoutImp = this.GridLayout;
			this.RowCount = -1;
			this.ColumnCount = -1;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001B4A8 File Offset: 0x000196A8
		public override Vector2 GetDropGizmoPosition(Vector2 draggedWidgetPosition)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0001B4AF File Offset: 0x000196AF
		public override int GetIndexForDrop(Vector2 draggedWidgetPosition)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x0001B4B8 File Offset: 0x000196B8
		public override void OnChildSelected(Widget widget)
		{
			int intValue = -1;
			for (int i = 0; i < base.ChildCount; i++)
			{
				if (widget == base.GetChild(i))
				{
					intValue = i;
				}
			}
			base.IntValue = intValue;
		}

		// Token: 0x040002ED RID: 749
		private float _defaultCellWidth;

		// Token: 0x040002EE RID: 750
		private float _defaultCellHeight;

		// Token: 0x040002EF RID: 751
		private int _rowCount;

		// Token: 0x040002F0 RID: 752
		private int _columnCount;

		// Token: 0x040002F1 RID: 753
		private bool _useDynamicCellWidth;

		// Token: 0x040002F2 RID: 754
		private bool _useDynamicCellHeight;

		// Token: 0x040002F3 RID: 755
		public const int DefaultRowCount = 3;

		// Token: 0x040002F4 RID: 756
		public const int DefaultColumnCount = 3;
	}
}
