﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.TownManagement;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Menu.TownManagement;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x020000BC RID: 188
	public class TownManagement__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_TownManagement_TownManagementVM_Dependency_7_ItemTemplate : DevelopmentItemVisualWidget
	{
		// Token: 0x060032A6 RID: 12966 RVA: 0x0018E90E File Offset: 0x0018CB0E
		public TownManagement__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_TownManagement_TownManagementVM_Dependency_7_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x0018E918 File Offset: 0x0018CB18
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new HintWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new DevelopmentQueueVisualIconWidget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_1_0 = new Widget(base.Context);
			this._widget_1.AddChild(this._widget_1_0);
			this._widget_1_0_0 = new TextWidget(base.Context);
			this._widget_1_0.AddChild(this._widget_1_0_0);
			this._widget_1_1 = new BrushWidget(base.Context);
			this._widget_1.AddChild(this._widget_1_1);
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x0018E9D6 File Offset: 0x0018CBD6
		public void SetIds()
		{
			this._widget_1.Id = "HammerIconWidget";
			this._widget_1_0.Id = "QueueIconWidget";
			this._widget_1_1.Id = "InProgressIconWidget";
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x0018EA08 File Offset: 0x0018CC08
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedWidth = 56f;
			base.SuggestedHeight = 56f;
			base.MarginBottom = 5f;
			base.IsDaily = false;
			base.UseSmallVariant = true;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.IsDisabled = true;
			this._widget_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_1.SuggestedWidth = 20f;
			this._widget_1.SuggestedHeight = 20f;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_1.VerticalAlignment = VerticalAlignment.Top;
			this._widget_1.InProgressIconWidget = this._widget_1_1;
			this._widget_1.IsEnabled = false;
			this._widget_1.QueueIconWidget = this._widget_1_0;
			this._widget_1_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_1_0.SuggestedWidth = 20f;
			this._widget_1_0.SuggestedHeight = 20f;
			this._widget_1_0.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_1_0.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_1_0_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_1_0_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1_0_0.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_1_0_0.VerticalAlignment = VerticalAlignment.Top;
			this._widget_1_0_0.PositionXOffset = -15f;
			this._widget_1_0_0.PositionYOffset = -2f;
			this._widget_1_0_0.Brush = base.Context.GetBrush("TownManagement.Queue.Index.Text");
			this._widget_1_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_1_1.SuggestedWidth = 24f;
			this._widget_1_1.SuggestedHeight = 24f;
			this._widget_1_1.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_1_1.VerticalAlignment = VerticalAlignment.Bottom;
			this._widget_1_1.PositionXOffset = -4f;
			this._widget_1_1.PositionYOffset = -2f;
			this._widget_1_1.Sprite = base.Context.SpriteData.GetSprite("SPGeneral\\TownManagement\\project_popup_hammer_icon");
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x0018EC40 File Offset: 0x0018CE40
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
				this._widget_0.EventFire -= this.EventListenerOf_widget_0;
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
				this._widget_1_0_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_0_0;
				this._datasource_Root = null;
			}
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x0018EFB2 File Offset: 0x0018D1B2
		public void SetDataSource(SettlementBuildingProjectVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x0018EFBB File Offset: 0x0018D1BB
		private void EventListenerOf_widget_0(Widget widget, string commandName, object[] args)
		{
			commandName == "HoverBegin";
			commandName == "HoverEnd";
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x0018EFD5 File Offset: 0x0018D1D5
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x0018EFDE File Offset: 0x0018D1DE
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x0018EFE7 File Offset: 0x0018D1E7
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x0018EFF0 File Offset: 0x0018D1F0
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x0018EFF9 File Offset: 0x0018D1F9
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x0018F002 File Offset: 0x0018D202
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x0018F00B File Offset: 0x0018D20B
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x0018F014 File Offset: 0x0018D214
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x0018F01D File Offset: 0x0018D21D
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x0018F026 File Offset: 0x0018D226
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "SpriteCode")
			{
				this._datasource_Root.VisualCode = this._widget.SpriteCode;
				return;
			}
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x0018F04C File Offset: 0x0018D24C
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x0018F055 File Offset: 0x0018D255
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x0018F05E File Offset: 0x0018D25E
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x0018F067 File Offset: 0x0018D267
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x0018F070 File Offset: 0x0018D270
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x0018F079 File Offset: 0x0018D279
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x0018F082 File Offset: 0x0018D282
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x0018F08B File Offset: 0x0018D28B
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x0018F094 File Offset: 0x0018D294
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x0018F09D File Offset: 0x0018D29D
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "QueueIndex")
			{
				this._datasource_Root.DevelopmentQueueIndex = this._widget_1.QueueIndex;
				return;
			}
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x0018F0C3 File Offset: 0x0018D2C3
		private void PropertyChangedListenerOf_widget_1_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0_0(propertyName);
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x0018F0CC File Offset: 0x0018D2CC
		private void boolPropertyChangedListenerOf_widget_1_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0_0(propertyName);
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x0018F0D5 File Offset: 0x0018D2D5
		private void floatPropertyChangedListenerOf_widget_1_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0_0(propertyName);
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x0018F0DE File Offset: 0x0018D2DE
		private void Vec2PropertyChangedListenerOf_widget_1_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0_0(propertyName);
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x0018F0E7 File Offset: 0x0018D2E7
		private void Vector2PropertyChangedListenerOf_widget_1_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0_0(propertyName);
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x0018F0F0 File Offset: 0x0018D2F0
		private void doublePropertyChangedListenerOf_widget_1_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0_0(propertyName);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x0018F0F9 File Offset: 0x0018D2F9
		private void intPropertyChangedListenerOf_widget_1_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0_0(propertyName);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x0018F102 File Offset: 0x0018D302
		private void uintPropertyChangedListenerOf_widget_1_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0_0(propertyName);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x0018F10B File Offset: 0x0018D30B
		private void ColorPropertyChangedListenerOf_widget_1_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0_0(propertyName);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x0018F114 File Offset: 0x0018D314
		private void HandleWidgetPropertyChangeOf_widget_1_0_0(string propertyName)
		{
			if (propertyName == "IntText")
			{
				this._datasource_Root.DevelopmentQueueIndex = this._widget_1_0_0.IntText;
				return;
			}
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x0018F13A File Offset: 0x0018D33A
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x0018F148 File Offset: 0x0018D348
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x0018F156 File Offset: 0x0018D356
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x0018F164 File Offset: 0x0018D364
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x0018F172 File Offset: 0x0018D372
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x0018F180 File Offset: 0x0018D380
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x0018F18E File Offset: 0x0018D38E
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x0018F19C File Offset: 0x0018D39C
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x0018F1AA File Offset: 0x0018D3AA
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x0018F1B8 File Offset: 0x0018D3B8
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "VisualCode")
			{
				this._widget.SpriteCode = this._datasource_Root.VisualCode;
				return;
			}
			if (propertyName == "DevelopmentQueueIndex")
			{
				this._widget_1.QueueIndex = this._datasource_Root.DevelopmentQueueIndex;
				this._widget_1_0_0.IntText = this._datasource_Root.DevelopmentQueueIndex;
				return;
			}
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x0018F224 File Offset: 0x0018D424
		private void RefreshDataSource_datasource_Root(SettlementBuildingProjectVM newDataSource)
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
				this._widget_0.EventFire -= this.EventListenerOf_widget_0;
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
				this._widget_1_0_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_0_0;
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
				this._widget.SpriteCode = this._datasource_Root.VisualCode;
				this._widget.PropertyChanged += this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged += this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget;
				this._widget_0.EventFire += this.EventListenerOf_widget_0;
				this._widget_1.QueueIndex = this._datasource_Root.DevelopmentQueueIndex;
				this._widget_1.PropertyChanged += this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1;
				this._widget_1_0_0.IntText = this._datasource_Root.DevelopmentQueueIndex;
				this._widget_1_0_0.PropertyChanged += this.PropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1_0_0;
				this._widget_1_0_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1_0_0;
			}
		}

		// Token: 0x04000A19 RID: 2585
		private DevelopmentItemVisualWidget _widget;

		// Token: 0x04000A1A RID: 2586
		private HintWidget _widget_0;

		// Token: 0x04000A1B RID: 2587
		private DevelopmentQueueVisualIconWidget _widget_1;

		// Token: 0x04000A1C RID: 2588
		private Widget _widget_1_0;

		// Token: 0x04000A1D RID: 2589
		private TextWidget _widget_1_0_0;

		// Token: 0x04000A1E RID: 2590
		private BrushWidget _widget_1_1;

		// Token: 0x04000A1F RID: 2591
		private SettlementBuildingProjectVM _datasource_Root;
	}
}
