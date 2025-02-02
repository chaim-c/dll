using System;
using System.Numerics;
using TaleWorlds.GauntletUI.Layout;

namespace TaleWorlds.GauntletUI.BaseTypes
{
	// Token: 0x02000063 RID: 99
	public class ListPanel : Container
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0001BEA0 File Offset: 0x0001A0A0
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		public StackLayout StackLayout { get; private set; }

		// Token: 0x06000663 RID: 1635 RVA: 0x0001BEB1 File Offset: 0x0001A0B1
		public ListPanel(UIContext context) : base(context)
		{
			this.StackLayout = new StackLayout();
			base.LayoutImp = this.StackLayout;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x0001BED1 File Offset: 0x0001A0D1
		protected override void OnUpdate(float dt)
		{
			base.OnUpdate(dt);
			this.UpdateListPanel();
			if (this.ResetSelectedOnLosingFocus && !base.CheckIsMyChildRecursive(base.EventManager.LatestMouseDownWidget))
			{
				base.IntValue = -1;
			}
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x0001BF02 File Offset: 0x0001A102
		private void UpdateListPanel()
		{
			if (base.AcceptDrop && this.IsDragHovering)
			{
				base.DragHoverInsertionIndex = this.GetIndexForDrop(base.EventManager.DraggedWidgetPosition);
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000666 RID: 1638 RVA: 0x0001BF2B File Offset: 0x0001A12B
		// (set) Token: 0x06000667 RID: 1639 RVA: 0x0001BF33 File Offset: 0x0001A133
		public override Predicate<Widget> AcceptDropPredicate { get; set; }

		// Token: 0x06000668 RID: 1640 RVA: 0x0001BF3C File Offset: 0x0001A13C
		public override int GetIndexForDrop(Vector2 draggedWidgetPosition)
		{
			return this.StackLayout.GetIndexForDrop(this, draggedWidgetPosition);
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x0001BF4B File Offset: 0x0001A14B
		public override Vector2 GetDropGizmoPosition(Vector2 draggedWidgetPosition)
		{
			return this.StackLayout.GetDropGizmoPosition(this, draggedWidgetPosition);
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x0001BF5C File Offset: 0x0001A15C
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

		// Token: 0x0600066B RID: 1643 RVA: 0x0001BF8F File Offset: 0x0001A18F
		protected internal override void OnDragHoverBegin()
		{
			this._dragHovering = true;
			base.SetMeasureAndLayoutDirty();
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001BF9E File Offset: 0x0001A19E
		protected internal override void OnDragHoverEnd()
		{
			this._dragHovering = false;
			base.SetMeasureAndLayoutDirty();
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001BFAD File Offset: 0x0001A1AD
		protected override bool OnPreviewDragHover()
		{
			return base.AcceptDrop;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600066E RID: 1646 RVA: 0x0001BFB5 File Offset: 0x0001A1B5
		public override bool IsDragHovering
		{
			get
			{
				return this._dragHovering;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0001BFBD File Offset: 0x0001A1BD
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0001BFC5 File Offset: 0x0001A1C5
		[Editor(false)]
		public bool ResetSelectedOnLosingFocus
		{
			get
			{
				return this._resetSelectedOnLosingFocus;
			}
			set
			{
				if (this._resetSelectedOnLosingFocus != value)
				{
					this._resetSelectedOnLosingFocus = value;
					base.OnPropertyChanged(value, "ResetSelectedOnLosingFocus");
				}
			}
		}

		// Token: 0x040002FC RID: 764
		private bool _dragHovering;

		// Token: 0x040002FD RID: 765
		private bool _resetSelectedOnLosingFocus;
	}
}
