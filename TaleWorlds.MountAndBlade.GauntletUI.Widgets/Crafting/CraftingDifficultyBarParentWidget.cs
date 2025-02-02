using System;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting
{
	// Token: 0x0200015E RID: 350
	public class CraftingDifficultyBarParentWidget : Widget
	{
		// Token: 0x0600126D RID: 4717 RVA: 0x00032831 File Offset: 0x00030A31
		public CraftingDifficultyBarParentWidget(UIContext context) : base(context)
		{
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00032845 File Offset: 0x00030A45
		private void OnWidgetPositionUpdated(PropertyOwnerObject ownerObject, string propertyName, object value)
		{
			if (propertyName == "Text")
			{
				this._areOffsetsDirty = true;
			}
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0003285C File Offset: 0x00030A5C
		protected override void OnLateUpdate(float dt)
		{
			base.OnLateUpdate(dt);
			if (this.SmithingLevelTextWidget != null && this.OrderDifficultyTextWidget != null)
			{
				if (this._updatePositions)
				{
					TextWidget textWidget = (this.OrderDifficulty < this.SmithingLevel) ? this.SmithingLevelTextWidget : this.OrderDifficultyTextWidget;
					TextWidget textWidget2 = (textWidget == this.SmithingLevelTextWidget) ? this.OrderDifficultyTextWidget : this.SmithingLevelTextWidget;
					if (textWidget.GlobalPosition.Y + (textWidget.Size.Y + this._offsetIntolerance) >= textWidget2.GlobalPosition.Y)
					{
						textWidget.PositionYOffset = -textWidget.Size.Y;
						textWidget2.PositionYOffset = 0f;
					}
					else
					{
						textWidget.PositionYOffset = 0f;
						textWidget2.PositionYOffset = 0f;
					}
					this._updatePositions = false;
				}
				if (this._areOffsetsDirty)
				{
					this.SmithingLevelTextWidget.PositionYOffset = 0f;
					this.OrderDifficultyTextWidget.PositionYOffset = 0f;
					this._updatePositions = true;
					this._areOffsetsDirty = false;
				}
			}
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001270 RID: 4720 RVA: 0x00032963 File Offset: 0x00030B63
		// (set) Token: 0x06001271 RID: 4721 RVA: 0x0003296B File Offset: 0x00030B6B
		public int OrderDifficulty { get; set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001272 RID: 4722 RVA: 0x00032974 File Offset: 0x00030B74
		// (set) Token: 0x06001273 RID: 4723 RVA: 0x0003297C File Offset: 0x00030B7C
		public int SmithingLevel { get; set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00032985 File Offset: 0x00030B85
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x0003298D File Offset: 0x00030B8D
		public TextWidget SmithingLevelTextWidget
		{
			get
			{
				return this._smithingLevelTextWidget;
			}
			set
			{
				if (value != this._smithingLevelTextWidget)
				{
					this._smithingLevelTextWidget = value;
					this._smithingLevelTextWidget.PropertyChanged += this.OnWidgetPositionUpdated;
				}
			}
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x000329B6 File Offset: 0x00030BB6
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x000329BE File Offset: 0x00030BBE
		public TextWidget OrderDifficultyTextWidget
		{
			get
			{
				return this._orderDifficultyTextWidget;
			}
			set
			{
				if (value != this._orderDifficultyTextWidget)
				{
					this._orderDifficultyTextWidget = value;
					this._orderDifficultyTextWidget.PropertyChanged += this.OnWidgetPositionUpdated;
				}
			}
		}

		// Token: 0x04000866 RID: 2150
		private float _offsetIntolerance = 3f;

		// Token: 0x04000867 RID: 2151
		private bool _areOffsetsDirty;

		// Token: 0x04000868 RID: 2152
		private bool _updatePositions;

		// Token: 0x0400086B RID: 2155
		private TextWidget _smithingLevelTextWidget;

		// Token: 0x0400086C RID: 2156
		private TextWidget _orderDifficultyTextWidget;
	}
}
