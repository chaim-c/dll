using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party
{
	// Token: 0x02000060 RID: 96
	public class PartyQuestProgressWidget : Widget
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x0000F668 File Offset: 0x0000D868
		public PartyQuestProgressWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000F674 File Offset: 0x0000D874
		private void UpdateDividers()
		{
			if (this.DividerContainer == null || this.DividerBrush == null)
			{
				return;
			}
			int itemCount = this.ItemCount;
			if (this.DividerContainer.ChildCount > itemCount)
			{
				int num = this.DividerContainer.ChildCount - itemCount;
				for (int i = 0; i < num; i++)
				{
					this.DividerContainer.RemoveChild(this.DividerContainer.GetChild(i));
				}
			}
			else if (itemCount > this.DividerContainer.ChildCount)
			{
				int num2 = itemCount - this.DividerContainer.ChildCount;
				for (int j = 0; j < num2; j++)
				{
					this.DividerContainer.AddChild(this.CreateDivider());
				}
			}
			this.UpdateDividerPositions();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000F720 File Offset: 0x0000D920
		private Widget CreateDivider()
		{
			Widget widget = new Widget(base.Context);
			widget.WidthSizePolicy = SizePolicy.StretchToParent;
			widget.HeightSizePolicy = SizePolicy.StretchToParent;
			BrushWidget brushWidget = new BrushWidget(base.Context);
			brushWidget.WidthSizePolicy = SizePolicy.Fixed;
			brushWidget.HeightSizePolicy = SizePolicy.Fixed;
			brushWidget.Brush = this.DividerBrush;
			brushWidget.SuggestedWidth = (float)brushWidget.ReadOnlyBrush.Sprite.Width;
			brushWidget.SuggestedHeight = (float)brushWidget.ReadOnlyBrush.Sprite.Height;
			brushWidget.HorizontalAlignment = HorizontalAlignment.Right;
			brushWidget.VerticalAlignment = VerticalAlignment.Center;
			brushWidget.PositionXOffset = (float)brushWidget.ReadOnlyBrush.Sprite.Width * 0.5f;
			widget.AddChild(brushWidget);
			return widget;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		private void UpdateDividerPositions()
		{
			int childCount = this.DividerContainer.ChildCount;
			float num = this.DividerContainer.Size.X / (float)(childCount + 1);
			for (int i = 0; i < childCount; i++)
			{
				Widget child = this.DividerContainer.GetChild(i);
				child.PositionXOffset = (float)i * num - child.Size.X / 2f;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000F830 File Offset: 0x0000DA30
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x0000F838 File Offset: 0x0000DA38
		[Editor(false)]
		public int ItemCount
		{
			get
			{
				return this._itemCount;
			}
			set
			{
				if (this._itemCount != value)
				{
					this._itemCount = value;
					base.OnPropertyChanged(value, "ItemCount");
					this.UpdateDividers();
				}
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000F85C File Offset: 0x0000DA5C
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0000F864 File Offset: 0x0000DA64
		[Editor(false)]
		public ListPanel DividerContainer
		{
			get
			{
				return this._dividerContainer;
			}
			set
			{
				if (this._dividerContainer != value)
				{
					this._dividerContainer = value;
					base.OnPropertyChanged<ListPanel>(value, "DividerContainer");
				}
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000F882 File Offset: 0x0000DA82
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0000F88A File Offset: 0x0000DA8A
		[Editor(false)]
		public Brush DividerBrush
		{
			get
			{
				return this._dividerBrush;
			}
			set
			{
				if (this._dividerBrush != value)
				{
					this._dividerBrush = value;
					base.OnPropertyChanged<Brush>(value, "DividerBrush");
				}
			}
		}

		// Token: 0x0400022F RID: 559
		private int _itemCount;

		// Token: 0x04000230 RID: 560
		private ListPanel _dividerContainer;

		// Token: 0x04000231 RID: 561
		private Brush _dividerBrush;
	}
}
