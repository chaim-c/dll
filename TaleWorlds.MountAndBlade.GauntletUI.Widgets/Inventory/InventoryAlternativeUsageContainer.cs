using System;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Inventory
{
	// Token: 0x02000129 RID: 297
	public class InventoryAlternativeUsageContainer : Container
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x0002A478 File Offset: 0x00028678
		public InventoryAlternativeUsageContainer(UIContext context) : base(context)
		{
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0002A4A0 File Offset: 0x000286A0
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

		// Token: 0x06000F59 RID: 3929 RVA: 0x0002A4D4 File Offset: 0x000286D4
		protected override void OnChildAdded(Widget child)
		{
			base.OnChildAdded(child);
			foreach (Action<Widget, Widget> action in this.ItemAddEventHandlers)
			{
				action(this, child);
			}
			base.EventFired("ItemAdd", Array.Empty<object>());
			this.SetChildrenLayout();
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0002A544 File Offset: 0x00028744
		protected override void OnChildRemoved(Widget child)
		{
			base.OnChildRemoved(child);
			if (base.IntValue < base.ChildCount - 1)
			{
				base.IntValue = -1;
			}
			foreach (Action<Widget, Widget> action in this.ItemRemoveEventHandlers)
			{
				action(this, child);
			}
			base.EventFired("ItemRemove", Array.Empty<object>());
			this.SetChildrenLayout();
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0002A5CC File Offset: 0x000287CC
		private void SetChildrenLayout()
		{
			if (base.ChildCount == 0)
			{
				return;
			}
			int num = MathF.Ceiling((float)base.ChildCount / (float)this.ColumnLimit);
			for (int i = 0; i < num; i++)
			{
				int num2 = MathF.Min(this.ColumnLimit, base.ChildCount - (num - 1) * this.ColumnLimit);
				int num3 = i * (int)this.CellHeight;
				for (int j = 0; j < num2; j++)
				{
					int num4 = (int)(((float)j - ((float)num2 - 1f) / 2f) * this.CellWidth);
					int i2 = i * this.ColumnLimit + j;
					Widget child = base.GetChild(i2);
					if (num4 > 0)
					{
						child.MarginLeft = (float)(num4 * 2);
					}
					else if (num4 < 0)
					{
						child.MarginRight = (float)(-(float)num4 * 2);
					}
					child.MarginTop = (float)num3;
				}
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0002A6A1 File Offset: 0x000288A1
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0002A6A9 File Offset: 0x000288A9
		[Editor(false)]
		public int ColumnLimit
		{
			get
			{
				return this._columnLimit;
			}
			set
			{
				if (this._columnLimit != value)
				{
					this._columnLimit = value;
					base.OnPropertyChanged(value, "ColumnLimit");
				}
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0002A6C7 File Offset: 0x000288C7
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x0002A6CF File Offset: 0x000288CF
		[Editor(false)]
		public float CellWidth
		{
			get
			{
				return this._cellWidth;
			}
			set
			{
				if (this._cellWidth != value)
				{
					this._cellWidth = value;
					base.OnPropertyChanged(value, "CellWidth");
				}
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x0002A6ED File Offset: 0x000288ED
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x0002A6F5 File Offset: 0x000288F5
		[Editor(false)]
		public float CellHeight
		{
			get
			{
				return this._cellHeight;
			}
			set
			{
				if (this._cellHeight != value)
				{
					this._cellHeight = value;
					base.OnPropertyChanged(value, "CellHeight");
				}
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000F62 RID: 3938 RVA: 0x0002A713 File Offset: 0x00028913
		// (set) Token: 0x06000F63 RID: 3939 RVA: 0x0002A71B File Offset: 0x0002891B
		public override Predicate<Widget> AcceptDropPredicate { get; set; }

		// Token: 0x06000F64 RID: 3940 RVA: 0x0002A724 File Offset: 0x00028924
		public override Vector2 GetDropGizmoPosition(Vector2 draggedWidgetPosition)
		{
			return Vector2.Zero;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0002A72B File Offset: 0x0002892B
		public override int GetIndexForDrop(Vector2 draggedWidgetPosition)
		{
			return -1;
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000F66 RID: 3942 RVA: 0x0002A72E File Offset: 0x0002892E
		public override bool IsDragHovering { get; }

		// Token: 0x04000707 RID: 1799
		private int _columnLimit = 2;

		// Token: 0x04000708 RID: 1800
		private float _cellWidth = 100f;

		// Token: 0x04000709 RID: 1801
		private float _cellHeight = 100f;
	}
}
