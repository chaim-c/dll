﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Inventory;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Information;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x0200012A RID: 298
	public class Inventory__TaleWorlds_CampaignSystem_ViewModelCollection_Inventory_SPInventoryVM_Dependency_31_ItemTemplate : TooltipPropertyWidget
	{
		// Token: 0x0600560D RID: 22029 RVA: 0x002ABB6A File Offset: 0x002A9D6A
		public Inventory__TaleWorlds_CampaignSystem_ViewModelCollection_Inventory_SPInventoryVM_Dependency_31_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x002ABB74 File Offset: 0x002A9D74
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ListPanel(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new ListPanel(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_0_0_0 = new Widget(base.Context);
			this._widget_0_0.AddChild(this._widget_0_0_0);
			this._widget_0_0_0_0 = new RichTextWidget(base.Context);
			this._widget_0_0_0.AddChild(this._widget_0_0_0_0);
			this._widget_0_0_1 = new Widget(base.Context);
			this._widget_0_0.AddChild(this._widget_0_0_1);
			this._widget_0_0_1_0 = new RichTextWidget(base.Context);
			this._widget_0_0_1.AddChild(this._widget_0_0_1_0);
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x002ABC54 File Offset: 0x002A9E54
		public void SetIds()
		{
			this._widget_0.Id = "ListPanel";
			this._widget_0_0.Id = "ValueBackgroundSpriteWidget";
			this._widget_0_0_0.Id = "DefinitionLabelContainer";
			this._widget_0_0_0_0.Id = "DefinitionLabel";
			this._widget_0_0_1.Id = "ValueLabelContainer";
			this._widget_0_0_1_0.Id = "ValueLabel";
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x002ABCC4 File Offset: 0x002A9EC4
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.CoverChildren;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.HorizontalAlignment = HorizontalAlignment.Center;
			base.MarginLeft = 3f;
			base.MarginRight = 3f;
			base.DefaultSeperatorSpriteName = "General\\TooltipHint\\tooltip_frame";
			base.DefinitionLabelContainer = this._widget_0_0_0;
			base.DefinitionLabel = this._widget_0_0_0_0;
			base.DescriptionTextBrush = base.Context.GetBrush("Tooltip.Description.Text");
			base.RundownSeperatorSpriteName = "tooltip_divider_abovestats_9";
			base.SubtextBrush = base.Context.GetBrush("Tooltip.SubText.Text");
			base.TitleBackgroundSpriteName = "General\\TooltipHint\\tooltip_title_base";
			base.TitleTextBrush = base.Context.GetBrush("Tooltip.Title.Text");
			base.ValueBackgroundSpriteWidget = this._widget_0_0;
			base.ValueLabel = this._widget_0_0_1_0;
			base.ValueLabelContainer = this._widget_0_0_1;
			base.ValueNameTextBrush = base.Context.GetBrush("Tooltip.ValueName.Text");
			base.ValueTextBrush = base.Context.GetBrush("Tooltip.Value.Text");
			this._widget_0.DoNotPassEventsToChildren = true;
			this._widget_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0.VerticalAlignment = VerticalAlignment.Top;
			this._widget_0.StackLayout.LayoutMethod = LayoutMethod.HorizontalLeftToRight;
			this._widget_0_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0.StackLayout.LayoutMethod = LayoutMethod.HorizontalLeftToRight;
			this._widget_0_0_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_0_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_0_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_0_0.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_0_0_0_0.Brush = base.Context.GetBrush("InventoryTooltipDescriptionFontBrush");
			this._widget_0_0_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_0_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_1.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_0_0_1_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_1_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_0_1_0.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_0_0_1_0.Brush = base.Context.GetBrush("Tooltip.Value.Text");
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x002ABEF8 File Offset: 0x002AA0F8
		public void DestroyDataSource()
		{
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
				if (this._datasource_Root_PropertyHint != null)
				{
					this._datasource_Root_PropertyHint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_PropertyHint;
					this._widget_0.EventFire -= this.EventListenerOf_widget_0;
					this._datasource_Root_PropertyHint = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x002AC1AD File Offset: 0x002AA3AD
		public void SetDataSource(ItemMenuTooltipPropertyVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x002AC1B6 File Offset: 0x002AA3B6
		private void EventListenerOf_widget_0(Widget widget, string commandName, object[] args)
		{
			if (commandName == "HoverBegin")
			{
				this._datasource_Root_PropertyHint.ExecuteBeginHint();
			}
			if (commandName == "HoverEnd")
			{
				this._datasource_Root_PropertyHint.ExecuteEndHint();
			}
		}

		// Token: 0x06005614 RID: 22036 RVA: 0x002AC1E8 File Offset: 0x002AA3E8
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x002AC1F1 File Offset: 0x002AA3F1
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x002AC1FA File Offset: 0x002AA3FA
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x002AC203 File Offset: 0x002AA403
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06005618 RID: 22040 RVA: 0x002AC20C File Offset: 0x002AA40C
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x002AC215 File Offset: 0x002AA415
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x002AC21E File Offset: 0x002AA41E
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x002AC227 File Offset: 0x002AA427
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x002AC230 File Offset: 0x002AA430
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x002AC23C File Offset: 0x002AA43C
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "DefinitionText")
			{
				this._datasource_Root.DefinitionLabel = this._widget.DefinitionText;
				return;
			}
			if (propertyName == "TextColor")
			{
				this._datasource_Root.TextColor = this._widget.TextColor;
				return;
			}
			if (propertyName == "TextHeight")
			{
				this._datasource_Root.TextHeight = this._widget.TextHeight;
				return;
			}
			if (propertyName == "ValueText")
			{
				this._datasource_Root.ValueLabel = this._widget.ValueText;
				return;
			}
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x002AC2D9 File Offset: 0x002AA4D9
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x002AC2E7 File Offset: 0x002AA4E7
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x002AC2F5 File Offset: 0x002AA4F5
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x002AC303 File Offset: 0x002AA503
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x002AC311 File Offset: 0x002AA511
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x002AC31F File Offset: 0x002AA51F
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x002AC32D File Offset: 0x002AA52D
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x002AC33B File Offset: 0x002AA53B
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x002AC349 File Offset: 0x002AA549
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x002AC358 File Offset: 0x002AA558
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "PropertyHint")
			{
				this.RefreshDataSource_datasource_Root_PropertyHint(this._datasource_Root.PropertyHint);
				return;
			}
			if (propertyName == "DefinitionLabel")
			{
				this._widget.DefinitionText = this._datasource_Root.DefinitionLabel;
				return;
			}
			if (propertyName == "TextColor")
			{
				this._widget.TextColor = this._datasource_Root.TextColor;
				return;
			}
			if (propertyName == "TextHeight")
			{
				this._widget.TextHeight = this._datasource_Root.TextHeight;
				return;
			}
			if (propertyName == "ValueLabel")
			{
				this._widget.ValueText = this._datasource_Root.ValueLabel;
				return;
			}
		}

		// Token: 0x06005628 RID: 22056 RVA: 0x002AC414 File Offset: 0x002AA614
		private void ViewModelPropertyChangedListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x002AC422 File Offset: 0x002AA622
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x002AC430 File Offset: 0x002AA630
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x0600562B RID: 22059 RVA: 0x002AC43E File Offset: 0x002AA63E
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x0600562C RID: 22060 RVA: 0x002AC44C File Offset: 0x002AA64C
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x0600562D RID: 22061 RVA: 0x002AC45A File Offset: 0x002AA65A
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x0600562E RID: 22062 RVA: 0x002AC468 File Offset: 0x002AA668
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x0600562F RID: 22063 RVA: 0x002AC476 File Offset: 0x002AA676
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x06005630 RID: 22064 RVA: 0x002AC484 File Offset: 0x002AA684
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x06005631 RID: 22065 RVA: 0x002AC492 File Offset: 0x002AA692
		private void HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(string propertyName)
		{
		}

		// Token: 0x06005632 RID: 22066 RVA: 0x002AC494 File Offset: 0x002AA694
		private void RefreshDataSource_datasource_Root(ItemMenuTooltipPropertyVM newDataSource)
		{
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
				if (this._datasource_Root_PropertyHint != null)
				{
					this._datasource_Root_PropertyHint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_PropertyHint;
					this._widget_0.EventFire -= this.EventListenerOf_widget_0;
					this._datasource_Root_PropertyHint = null;
				}
				this._datasource_Root = null;
			}
			this._datasource_Root = newDataSource;
			if (this._datasource_Root != null)
			{
				this._datasource_Root.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root;
				this._datasource_Root.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root;
				this._widget.DefinitionText = this._datasource_Root.DefinitionLabel;
				this._widget.TextColor = this._datasource_Root.TextColor;
				this._widget.TextHeight = this._datasource_Root.TextHeight;
				this._widget.ValueText = this._datasource_Root.ValueLabel;
				this._widget.PropertyChanged += this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged += this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget;
				this._datasource_Root_PropertyHint = this._datasource_Root.PropertyHint;
				if (this._datasource_Root_PropertyHint != null)
				{
					this._datasource_Root_PropertyHint.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_PropertyHint;
					this._datasource_Root_PropertyHint.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_PropertyHint;
					this._widget_0.EventFire += this.EventListenerOf_widget_0;
				}
			}
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x002ACA54 File Offset: 0x002AAC54
		private void RefreshDataSource_datasource_Root_PropertyHint(HintViewModel newDataSource)
		{
			if (this._datasource_Root_PropertyHint != null)
			{
				this._datasource_Root_PropertyHint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_PropertyHint;
				this._widget_0.EventFire -= this.EventListenerOf_widget_0;
				this._datasource_Root_PropertyHint = null;
			}
			this._datasource_Root_PropertyHint = newDataSource;
			this._datasource_Root_PropertyHint = this._datasource_Root.PropertyHint;
			if (this._datasource_Root_PropertyHint != null)
			{
				this._datasource_Root_PropertyHint.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_PropertyHint;
				this._datasource_Root_PropertyHint.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_PropertyHint;
				this._widget_0.EventFire += this.EventListenerOf_widget_0;
			}
		}

		// Token: 0x040011BF RID: 4543
		private TooltipPropertyWidget _widget;

		// Token: 0x040011C0 RID: 4544
		private ListPanel _widget_0;

		// Token: 0x040011C1 RID: 4545
		private ListPanel _widget_0_0;

		// Token: 0x040011C2 RID: 4546
		private Widget _widget_0_0_0;

		// Token: 0x040011C3 RID: 4547
		private RichTextWidget _widget_0_0_0_0;

		// Token: 0x040011C4 RID: 4548
		private Widget _widget_0_0_1;

		// Token: 0x040011C5 RID: 4549
		private RichTextWidget _widget_0_0_1_0;

		// Token: 0x040011C6 RID: 4550
		private ItemMenuTooltipPropertyVM _datasource_Root;

		// Token: 0x040011C7 RID: 4551
		private HintViewModel _datasource_Root_PropertyHint;
	}
}
