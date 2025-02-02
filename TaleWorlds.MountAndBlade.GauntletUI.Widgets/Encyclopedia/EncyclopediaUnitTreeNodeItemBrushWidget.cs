using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia
{
	// Token: 0x02000151 RID: 337
	public class EncyclopediaUnitTreeNodeItemBrushWidget : BrushWidget
	{
		// Token: 0x060011D1 RID: 4561 RVA: 0x000315A6 File Offset: 0x0002F7A6
		public EncyclopediaUnitTreeNodeItemBrushWidget(UIContext context) : base(context)
		{
			this._listItemAddedHandler = new Action<Widget, Widget>(this.OnListItemAdded);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000315C4 File Offset: 0x0002F7C4
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this._isLinesDirty)
			{
				if (this.ChildContainer.ChildCount == this.LineContainer.ChildCount)
				{
					float num = base.GlobalPosition.X + base.Size.X * 0.5f;
					for (int i = 0; i < this.ChildContainer.ChildCount; i++)
					{
						Widget child = this.ChildContainer.GetChild(i);
						Widget child2 = this.LineContainer.GetChild(i);
						float num2 = child.GlobalPosition.X + child.Size.X * 0.5f;
						bool flag = num > num2;
						child2.SetState(flag ? "Left" : "Right");
						float num3 = MathF.Abs(num - num2);
						child2.ScaledSuggestedWidth = num3;
						child2.ScaledPositionXOffset = (num3 * 0.5f + 5f * base._scaleToUse) * (float)(flag ? -1 : 1);
					}
				}
				this._isLinesDirty = false;
			}
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000316D0 File Offset: 0x0002F8D0
		public void OnListItemAdded(Widget parentWidget, Widget addedWidget)
		{
			Widget widget = this.CreateLineWidget();
			if (this.ChildContainer.ChildCount == 1)
			{
				widget.SetState("Straight");
				return;
			}
			this._isLinesDirty = true;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00031708 File Offset: 0x0002F908
		private Widget CreateLineWidget()
		{
			BrushWidget brushWidget = new BrushWidget(base.Context)
			{
				WidthSizePolicy = SizePolicy.Fixed,
				HeightSizePolicy = SizePolicy.StretchToParent,
				Brush = this.LineBrush
			};
			brushWidget.SuggestedWidth = (float)brushWidget.ReadOnlyBrush.Sprite.Width;
			brushWidget.SuggestedHeight = (float)brushWidget.ReadOnlyBrush.Sprite.Height;
			brushWidget.HorizontalAlignment = HorizontalAlignment.Center;
			brushWidget.AddState("Left");
			brushWidget.AddState("Right");
			brushWidget.AddState("Straight");
			this.LineContainer.AddChild(brushWidget);
			return brushWidget;
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0003179E File Offset: 0x0002F99E
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x000317A6 File Offset: 0x0002F9A6
		[Editor(false)]
		public bool IsAlternativeUpgrade
		{
			get
			{
				return this._isAlternativeUpgrade;
			}
			set
			{
				if (value != this._isAlternativeUpgrade)
				{
					this._isAlternativeUpgrade = value;
					base.OnPropertyChanged(value, "IsAlternativeUpgrade");
				}
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x000317C4 File Offset: 0x0002F9C4
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x000317CC File Offset: 0x0002F9CC
		[Editor(false)]
		public ListPanel ChildContainer
		{
			get
			{
				return this._childContainer;
			}
			set
			{
				if (this._childContainer != value)
				{
					ListPanel childContainer = this._childContainer;
					if (childContainer != null)
					{
						childContainer.ItemAddEventHandlers.Remove(this._listItemAddedHandler);
					}
					this._childContainer = value;
					base.OnPropertyChanged<ListPanel>(value, "ChildContainer");
					ListPanel childContainer2 = this._childContainer;
					if (childContainer2 == null)
					{
						return;
					}
					childContainer2.ItemAddEventHandlers.Add(this._listItemAddedHandler);
				}
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x060011D9 RID: 4569 RVA: 0x0003182D File Offset: 0x0002FA2D
		// (set) Token: 0x060011DA RID: 4570 RVA: 0x00031835 File Offset: 0x0002FA35
		[Editor(false)]
		public Widget LineContainer
		{
			get
			{
				return this._lineContainer;
			}
			set
			{
				if (this._lineContainer != value)
				{
					this._lineContainer = value;
					base.OnPropertyChanged<Widget>(value, "LineContainer");
				}
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x00031853 File Offset: 0x0002FA53
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x0003185B File Offset: 0x0002FA5B
		[Editor(false)]
		public Brush LineBrush
		{
			get
			{
				return this._lineBrush;
			}
			set
			{
				if (this._lineBrush != value)
				{
					this._lineBrush = value;
					base.OnPropertyChanged<Brush>(value, "LineBrush");
				}
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x00031879 File Offset: 0x0002FA79
		// (set) Token: 0x060011DE RID: 4574 RVA: 0x00031881 File Offset: 0x0002FA81
		[Editor(false)]
		public Brush AlternateLineBrush
		{
			get
			{
				return this._alternateLineBrush;
			}
			set
			{
				if (this._alternateLineBrush != value)
				{
					this._alternateLineBrush = value;
					base.OnPropertyChanged<Brush>(value, "AlternateLineBrush");
				}
			}
		}

		// Token: 0x04000822 RID: 2082
		private Action<Widget, Widget> _listItemAddedHandler;

		// Token: 0x04000823 RID: 2083
		private bool _isLinesDirty;

		// Token: 0x04000824 RID: 2084
		private bool _isAlternativeUpgrade;

		// Token: 0x04000825 RID: 2085
		private ListPanel _childContainer;

		// Token: 0x04000826 RID: 2086
		private Widget _lineContainer;

		// Token: 0x04000827 RID: 2087
		private Brush _lineBrush;

		// Token: 0x04000828 RID: 2088
		private Brush _alternateLineBrush;
	}
}
