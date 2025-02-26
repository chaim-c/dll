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
	// Token: 0x0200012D RID: 301
	public class Inventory__TaleWorlds_CampaignSystem_ViewModelCollection_Inventory_SPInventoryVM_Dependency_34_ItemTemplate : TooltipPropertyWidget
	{
		// Token: 0x06005682 RID: 22146 RVA: 0x002AE766 File Offset: 0x002AC966
		public Inventory__TaleWorlds_CampaignSystem_ViewModelCollection_Inventory_SPInventoryVM_Dependency_34_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06005683 RID: 22147 RVA: 0x002AE770 File Offset: 0x002AC970
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

		// Token: 0x06005684 RID: 22148 RVA: 0x002AE850 File Offset: 0x002ACA50
		public void SetIds()
		{
			this._widget_0.Id = "ListPanel";
			this._widget_0_0.Id = "ValueBackgroundSpriteWidget";
			this._widget_0_0_0.Id = "DefinitionLabelContainer";
			this._widget_0_0_0_0.Id = "DefinitionLabel";
			this._widget_0_0_1.Id = "ValueLabelContainer";
			this._widget_0_0_1_0.Id = "ValueLabel";
		}

		// Token: 0x06005685 RID: 22149 RVA: 0x002AE8C0 File Offset: 0x002ACAC0
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.CoverChildren;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.HorizontalAlignment = HorizontalAlignment.Right;
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

		// Token: 0x06005686 RID: 22150 RVA: 0x002AEAF4 File Offset: 0x002ACCF4
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

		// Token: 0x06005687 RID: 22151 RVA: 0x002AEDA9 File Offset: 0x002ACFA9
		public void SetDataSource(ItemMenuTooltipPropertyVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06005688 RID: 22152 RVA: 0x002AEDB2 File Offset: 0x002ACFB2
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

		// Token: 0x06005689 RID: 22153 RVA: 0x002AEDE4 File Offset: 0x002ACFE4
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600568A RID: 22154 RVA: 0x002AEDED File Offset: 0x002ACFED
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600568B RID: 22155 RVA: 0x002AEDF6 File Offset: 0x002ACFF6
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x002AEDFF File Offset: 0x002ACFFF
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600568D RID: 22157 RVA: 0x002AEE08 File Offset: 0x002AD008
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600568E RID: 22158 RVA: 0x002AEE11 File Offset: 0x002AD011
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x002AEE1A File Offset: 0x002AD01A
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x002AEE23 File Offset: 0x002AD023
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x002AEE2C File Offset: 0x002AD02C
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x002AEE38 File Offset: 0x002AD038
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

		// Token: 0x06005693 RID: 22163 RVA: 0x002AEED5 File Offset: 0x002AD0D5
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005694 RID: 22164 RVA: 0x002AEEE3 File Offset: 0x002AD0E3
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005695 RID: 22165 RVA: 0x002AEEF1 File Offset: 0x002AD0F1
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005696 RID: 22166 RVA: 0x002AEEFF File Offset: 0x002AD0FF
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005697 RID: 22167 RVA: 0x002AEF0D File Offset: 0x002AD10D
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005698 RID: 22168 RVA: 0x002AEF1B File Offset: 0x002AD11B
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06005699 RID: 22169 RVA: 0x002AEF29 File Offset: 0x002AD129
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600569A RID: 22170 RVA: 0x002AEF37 File Offset: 0x002AD137
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600569B RID: 22171 RVA: 0x002AEF45 File Offset: 0x002AD145
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600569C RID: 22172 RVA: 0x002AEF54 File Offset: 0x002AD154
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

		// Token: 0x0600569D RID: 22173 RVA: 0x002AF010 File Offset: 0x002AD210
		private void ViewModelPropertyChangedListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x0600569E RID: 22174 RVA: 0x002AF01E File Offset: 0x002AD21E
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x0600569F RID: 22175 RVA: 0x002AF02C File Offset: 0x002AD22C
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x060056A0 RID: 22176 RVA: 0x002AF03A File Offset: 0x002AD23A
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x060056A1 RID: 22177 RVA: 0x002AF048 File Offset: 0x002AD248
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x060056A2 RID: 22178 RVA: 0x002AF056 File Offset: 0x002AD256
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x060056A3 RID: 22179 RVA: 0x002AF064 File Offset: 0x002AD264
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x060056A4 RID: 22180 RVA: 0x002AF072 File Offset: 0x002AD272
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x060056A5 RID: 22181 RVA: 0x002AF080 File Offset: 0x002AD280
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_PropertyHint(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(e.PropertyName);
		}

		// Token: 0x060056A6 RID: 22182 RVA: 0x002AF08E File Offset: 0x002AD28E
		private void HandleViewModelPropertyChangeOf_datasource_Root_PropertyHint(string propertyName)
		{
		}

		// Token: 0x060056A7 RID: 22183 RVA: 0x002AF090 File Offset: 0x002AD290
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

		// Token: 0x060056A8 RID: 22184 RVA: 0x002AF650 File Offset: 0x002AD850
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

		// Token: 0x040011D2 RID: 4562
		private TooltipPropertyWidget _widget;

		// Token: 0x040011D3 RID: 4563
		private ListPanel _widget_0;

		// Token: 0x040011D4 RID: 4564
		private ListPanel _widget_0_0;

		// Token: 0x040011D5 RID: 4565
		private Widget _widget_0_0_0;

		// Token: 0x040011D6 RID: 4566
		private RichTextWidget _widget_0_0_0_0;

		// Token: 0x040011D7 RID: 4567
		private Widget _widget_0_0_1;

		// Token: 0x040011D8 RID: 4568
		private RichTextWidget _widget_0_0_1_0;

		// Token: 0x040011D9 RID: 4569
		private ItemMenuTooltipPropertyVM _datasource_Root;

		// Token: 0x040011DA RID: 4570
		private HintViewModel _datasource_Root_PropertyHint;
	}
}
