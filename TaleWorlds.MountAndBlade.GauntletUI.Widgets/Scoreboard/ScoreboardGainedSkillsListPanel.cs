﻿using System;
using System.Collections.Generic;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;

namespace TaleWorlds.MountAndBlade.GauntletUI.Widgets.Scoreboard
{
	// Token: 0x02000050 RID: 80
	public class ScoreboardGainedSkillsListPanel : ListPanel
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000DC0B File Offset: 0x0000BE0B
		public ScoreboardGainedSkillsListPanel(UIContext context) : base(context)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000DC1F File Offset: 0x0000BE1F
		protected override void OnLateUpdate(float dt)
		{
			if (base.IsVisible)
			{
				this.UpdateVerticalPosRelatedToCurrentUnit();
			}
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000DC30 File Offset: 0x0000BE30
		private void UpdateVerticalPosRelatedToCurrentUnit()
		{
			if (this._currentUnit == null)
			{
				base.ScaledPositionYOffset = -1000f;
				return;
			}
			float num = base.EventManager.PageSize.Y - 107f * base._scaleToUse;
			if (num - (this._currentUnit.GlobalPosition.Y + base.Size.Y) < base.Size.Y + this._scrollGradientPadding)
			{
				float num2 = num - (this._scrollGradientPadding * base._scaleToUse + base.Size.Y);
				base.ScaledPositionYOffset = num2 - this._currentUnit.GlobalPosition.Y + this._currentUnit.ParentWidget.ParentWidget.ParentWidget.LocalPosition.Y;
				return;
			}
			base.ScaledPositionYOffset = this._currentUnit.ParentWidget.ParentWidget.ParentWidget.LocalPosition.Y;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000DD20 File Offset: 0x0000BF20
		public void SetCurrentUnit(ScoreboardSkillItemHoverToggleWidget unit)
		{
			this._currentUnit = unit;
			if (unit != null)
			{
				if (base.ChildCount > 0)
				{
					base.RemoveAllChildren();
				}
				List<Widget> allSkillWidgets = this._currentUnit.GetAllSkillWidgets();
				for (int i = 0; i < allSkillWidgets.Count; i++)
				{
					Widget widget = allSkillWidgets[i];
					Widget widget2 = new Widget(base.Context);
					this.CopyWidgetProperties(ref widget2, widget);
					Widget widget3 = new Widget(base.Context);
					this.CopyWidgetProperties(ref widget3, widget.Children[0]);
					TextWidget widget4 = new TextWidget(base.Context);
					this.CopyWidgetProperties(ref widget4, (TextWidget)widget.Children[1]);
					base.AddChild(widget2);
					widget2.AddChild(widget3);
					widget2.AddChild(widget4);
				}
				this.UpdateVerticalPosRelatedToCurrentUnit();
				base.IsVisible = true;
				return;
			}
			base.RemoveAllChildren();
			base.IsVisible = false;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000DE00 File Offset: 0x0000C000
		private void CopyWidgetProperties(ref TextWidget targetTextWidget, TextWidget sourceTextWidget)
		{
			targetTextWidget.Text = sourceTextWidget.Text;
			Widget widget = targetTextWidget;
			this.CopyWidgetProperties(ref widget, sourceTextWidget);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0000DE28 File Offset: 0x0000C028
		private void CopyWidgetProperties(ref Widget targetWidget, Widget sourceWidget)
		{
			targetWidget.WidthSizePolicy = sourceWidget.WidthSizePolicy;
			targetWidget.HeightSizePolicy = sourceWidget.HeightSizePolicy;
			targetWidget.SuggestedWidth = sourceWidget.SuggestedWidth;
			targetWidget.SuggestedHeight = sourceWidget.SuggestedHeight;
			targetWidget.MarginTop = sourceWidget.MarginTop;
			targetWidget.MarginBottom = sourceWidget.MarginBottom;
			targetWidget.MarginLeft = sourceWidget.MarginLeft;
			targetWidget.MarginRight = sourceWidget.MarginRight;
			targetWidget.Sprite = sourceWidget.Sprite;
			BrushWidget brushWidget;
			BrushWidget brushWidget2;
			if ((brushWidget = (targetWidget as BrushWidget)) != null && (brushWidget2 = (sourceWidget as BrushWidget)) != null)
			{
				brushWidget.Brush = brushWidget2.ReadOnlyBrush;
			}
			targetWidget.VerticalAlignment = sourceWidget.VerticalAlignment;
		}

		// Token: 0x040001D9 RID: 473
		private float _scrollGradientPadding = 55f;

		// Token: 0x040001DA RID: 474
		private ScoreboardSkillItemHoverToggleWidget _currentUnit;
	}
}
