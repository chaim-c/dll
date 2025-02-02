using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.TwoDimension;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Information.RundownTooltip
{
	// Token: 0x0200013D RID: 317
	public class RundownColumnDividerCollectionWidget : ListPanel
	{
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0002F6B6 File Offset: 0x0002D8B6
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x0002F6BE File Offset: 0x0002D8BE
		public float DividerWidth { get; set; }

		// Token: 0x060010EF RID: 4335 RVA: 0x0002F6C7 File Offset: 0x0002D8C7
		public RundownColumnDividerCollectionWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0002F6F0 File Offset: 0x0002D8F0
		public void Refresh(IReadOnlyList<float> columnWidths)
		{
			base.RemoveAllChildren();
			for (int i = 0; i < columnWidths.Count - 1; i++)
			{
				Widget widget = this.CreateFixedSpaceWidget(columnWidths[i] * base._inverseScaleToUse - this.DividerWidth);
				base.AddChild(widget);
				base.AddChild(this.CreateDividerWidget());
			}
			base.AddChild(this.CreateStretchedSpaceWidget());
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0002F751 File Offset: 0x0002D951
		private Widget CreateFixedSpaceWidget(float width)
		{
			return new Widget(base.Context)
			{
				WidthSizePolicy = SizePolicy.Fixed,
				HeightSizePolicy = SizePolicy.StretchToParent,
				SuggestedWidth = width
			};
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0002F773 File Offset: 0x0002D973
		private Widget CreateStretchedSpaceWidget()
		{
			return new Widget(base.Context)
			{
				WidthSizePolicy = SizePolicy.StretchToParent,
				HeightSizePolicy = SizePolicy.StretchToParent
			};
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0002F78E File Offset: 0x0002D98E
		private Widget CreateDividerWidget()
		{
			return new Widget(base.Context)
			{
				WidthSizePolicy = SizePolicy.Fixed,
				HeightSizePolicy = SizePolicy.StretchToParent,
				SuggestedWidth = this.DividerWidth,
				Sprite = this.DividerSprite,
				Color = this.DividerColor
			};
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0002F7CD File Offset: 0x0002D9CD
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x0002F7D5 File Offset: 0x0002D9D5
		[Editor(false)]
		public Sprite DividerSprite
		{
			get
			{
				return this._dividerSprite;
			}
			set
			{
				if (value != this._dividerSprite)
				{
					this._dividerSprite = value;
					base.OnPropertyChanged<Sprite>(value, "DividerSprite");
				}
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0002F7F3 File Offset: 0x0002D9F3
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0002F7FB File Offset: 0x0002D9FB
		[Editor(false)]
		public Color DividerColor
		{
			get
			{
				return this._dividerColor;
			}
			set
			{
				if (value != this._dividerColor)
				{
					this._dividerColor = value;
					base.OnPropertyChanged(value, "DividerColor");
				}
			}
		}

		// Token: 0x040007CA RID: 1994
		private Sprite _dividerSprite;

		// Token: 0x040007CB RID: 1995
		private Color _dividerColor = new Color(1f, 1f, 1f, 1f);
	}
}
