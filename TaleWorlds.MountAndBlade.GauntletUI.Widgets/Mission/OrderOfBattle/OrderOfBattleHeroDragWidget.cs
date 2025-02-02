using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Mission.OrderOfBattle
{
	// Token: 0x020000E6 RID: 230
	public class OrderOfBattleHeroDragWidget : Widget
	{
		// Token: 0x06000BEA RID: 3050 RVA: 0x00020C5A File Offset: 0x0001EE5A
		public OrderOfBattleHeroDragWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00020C64 File Offset: 0x0001EE64
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (!base.IsVisible)
			{
				return;
			}
			if (this._isDirty && this.StackDragWidget != null)
			{
				base.RemoveAllChildren();
				for (int i = 0; i < this.StackCount; i++)
				{
					BrushWidget brushWidget = new BrushWidget(base.Context)
					{
						Brush = this.StackDragWidget.ReadOnlyBrush,
						DoNotAcceptEvents = false,
						SuggestedHeight = this.StackDragWidget.SuggestedHeight,
						SuggestedWidth = this.StackDragWidget.SuggestedWidth,
						ScaledPositionXOffset = (float)(i * 5),
						ScaledPositionYOffset = (float)(i * 5)
					};
					if (i == this.StackCount - 1)
					{
						BrushWidget widget = new BrushWidget(brushWidget.Context)
						{
							Brush = base.Context.GetBrush(this.InnerBrushName),
							WidthSizePolicy = SizePolicy.StretchToParent,
							HeightSizePolicy = SizePolicy.StretchToParent,
							MarginBottom = 5f,
							MarginTop = 5f,
							MarginLeft = 5f,
							MarginRight = 5f,
							HorizontalAlignment = HorizontalAlignment.Center,
							VerticalAlignment = VerticalAlignment.Center
						};
						ImageIdentifierWidget widget2 = new ImageIdentifierWidget(brushWidget.Context)
						{
							WidthSizePolicy = SizePolicy.Fixed,
							HeightSizePolicy = SizePolicy.Fixed,
							SuggestedWidth = this.StackThumbnailWidget.SuggestedWidth,
							SuggestedHeight = this.StackThumbnailWidget.SuggestedHeight,
							MarginTop = this.StackThumbnailWidget.MarginTop,
							MarginLeft = this.StackThumbnailWidget.MarginLeft,
							AdditionalArgs = this.StackThumbnailWidget.AdditionalArgs,
							ImageId = this.StackThumbnailWidget.ImageId,
							ImageTypeCode = this.StackThumbnailWidget.ImageTypeCode
						};
						brushWidget.AddChild(widget);
						brushWidget.AddChild(widget2);
					}
					base.AddChild(brushWidget);
				}
				this._isDirty = false;
			}
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00020E34 File Offset: 0x0001F034
		private void OnStackCountChanged()
		{
			this._isDirty = true;
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x00020E3D File Offset: 0x0001F03D
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x00020E45 File Offset: 0x0001F045
		[Editor(false)]
		public int StackCount
		{
			get
			{
				return this._stackCount;
			}
			set
			{
				if (value != this._stackCount)
				{
					this._stackCount = value;
					base.OnPropertyChanged(value, "StackCount");
					this.OnStackCountChanged();
				}
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00020E69 File Offset: 0x0001F069
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x00020E71 File Offset: 0x0001F071
		[Editor(false)]
		public BrushWidget StackDragWidget
		{
			get
			{
				return this._stackDragWidget;
			}
			set
			{
				if (value != this._stackDragWidget)
				{
					this._stackDragWidget = value;
					base.OnPropertyChanged<BrushWidget>(value, "StackDragWidget");
				}
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00020E8F File Offset: 0x0001F08F
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x00020E97 File Offset: 0x0001F097
		[Editor(false)]
		public ImageIdentifierWidget StackThumbnailWidget
		{
			get
			{
				return this._stackThumbnailWidget;
			}
			set
			{
				if (value != this._stackThumbnailWidget)
				{
					this._stackThumbnailWidget = value;
					base.OnPropertyChanged<ImageIdentifierWidget>(value, "StackThumbnailWidget");
				}
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00020EB5 File Offset: 0x0001F0B5
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x00020EBD File Offset: 0x0001F0BD
		[Editor(false)]
		public string InnerBrushName
		{
			get
			{
				return this._innerBrushName;
			}
			set
			{
				if (value != this._innerBrushName)
				{
					this._innerBrushName = value;
					base.OnPropertyChanged<string>(value, "InnerBrushName");
				}
			}
		}

		// Token: 0x0400056A RID: 1386
		private bool _isDirty;

		// Token: 0x0400056B RID: 1387
		private int _stackCount;

		// Token: 0x0400056C RID: 1388
		private BrushWidget _stackDragWidget;

		// Token: 0x0400056D RID: 1389
		private ImageIdentifierWidget _stackThumbnailWidget;

		// Token: 0x0400056E RID: 1390
		private string _innerBrushName;
	}
}
