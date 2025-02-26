﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.WeaponCrafting;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Crafting;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x020000A5 RID: 165
	public class Crafting__TaleWorlds_CampaignSystem_ViewModelCollection_WeaponCrafting_CraftingVM_Dependency_43_ItemTemplate : ValueBasedVisibilityWidget
	{
		// Token: 0x060032BB RID: 12987 RVA: 0x0019D581 File Offset: 0x0019B781
		public Crafting__TaleWorlds_CampaignSystem_ViewModelCollection_WeaponCrafting_CraftingVM_Dependency_43_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x0019D58C File Offset: 0x0019B78C
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ListPanel(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_0_0_0 = new CraftingMaterialVisualBrushWidget(base.Context);
			this._widget_0_0.AddChild(this._widget_0_0_0);
			this._widget_0_1 = new TextWidget(base.Context);
			this._widget_0.AddChild(this._widget_0_1);
			this._widget_1 = new HintWidget(base.Context);
			this._widget.AddChild(this._widget_1);
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x0019D64A File Offset: 0x0019B84A
		public void SetIds()
		{
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x0019D64C File Offset: 0x0019B84C
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.CoverChildren;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.VerticalAlignment = VerticalAlignment.Center;
			base.DoNotPassEventsToChildren = true;
			base.WatchType = ValueBasedVisibilityWidget.WatchTypes.BiggerThan;
			base.IndexToBeVisible = 0;
			this._widget_0.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.StackLayout.LayoutMethod = LayoutMethod.VerticalBottomToTop;
			this._widget_0_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.SuggestedWidth = 50f;
			this._widget_0_0.SuggestedHeight = 50f;
			this._widget_0_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0_0_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_0_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_0_0.SuggestedWidth = 75f;
			this._widget_0_0_0.SuggestedHeight = 55f;
			this._widget_0_0_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_0_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0_0_0.Brush = base.Context.GetBrush("Crafting.Material.Brush");
			this._widget_0_1.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_1.Brush = base.Context.GetBrush("Refinement.Amount.Text");
			this._widget_0_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_1.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0_1.Brush.FontSize = 20;
			this._widget_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_1.IsDisabled = true;
		}

		// Token: 0x060032BF RID: 12991 RVA: 0x0019D7E8 File Offset: 0x0019B9E8
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
				this._widget_0_0_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_1;
				if (this._datasource_Root_ResourceHint != null)
				{
					this._datasource_Root_ResourceHint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_ResourceHint;
					this._widget_1.EventFire -= this.EventListenerOf_widget_1;
					this._datasource_Root_ResourceHint = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x060032C0 RID: 12992 RVA: 0x0019DC3B File Offset: 0x0019BE3B
		public void SetDataSource(CraftingResourceItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060032C1 RID: 12993 RVA: 0x0019DC44 File Offset: 0x0019BE44
		private void EventListenerOf_widget_1(Widget widget, string commandName, object[] args)
		{
			if (commandName == "HoverBegin")
			{
				this._datasource_Root_ResourceHint.ExecuteBeginHint();
			}
			if (commandName == "HoverEnd")
			{
				this._datasource_Root_ResourceHint.ExecuteEndHint();
			}
		}

		// Token: 0x060032C2 RID: 12994 RVA: 0x0019DC76 File Offset: 0x0019BE76
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x0019DC7F File Offset: 0x0019BE7F
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x0019DC88 File Offset: 0x0019BE88
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x0019DC91 File Offset: 0x0019BE91
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x0019DC9A File Offset: 0x0019BE9A
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x0019DCA3 File Offset: 0x0019BEA3
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x0019DCAC File Offset: 0x0019BEAC
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x0019DCB5 File Offset: 0x0019BEB5
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x0019DCBE File Offset: 0x0019BEBE
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x0019DCC7 File Offset: 0x0019BEC7
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "IndexToWatch")
			{
				this._datasource_Root.ResourceAmount = this._widget.IndexToWatch;
				return;
			}
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x0019DCED File Offset: 0x0019BEED
		private void PropertyChangedListenerOf_widget_0_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0_0(propertyName);
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x0019DCF6 File Offset: 0x0019BEF6
		private void boolPropertyChangedListenerOf_widget_0_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0_0(propertyName);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x0019DCFF File Offset: 0x0019BEFF
		private void floatPropertyChangedListenerOf_widget_0_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0_0(propertyName);
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x0019DD08 File Offset: 0x0019BF08
		private void Vec2PropertyChangedListenerOf_widget_0_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0_0(propertyName);
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x0019DD11 File Offset: 0x0019BF11
		private void Vector2PropertyChangedListenerOf_widget_0_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0_0(propertyName);
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x0019DD1A File Offset: 0x0019BF1A
		private void doublePropertyChangedListenerOf_widget_0_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0_0(propertyName);
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x0019DD23 File Offset: 0x0019BF23
		private void intPropertyChangedListenerOf_widget_0_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0_0(propertyName);
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x0019DD2C File Offset: 0x0019BF2C
		private void uintPropertyChangedListenerOf_widget_0_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0_0(propertyName);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x0019DD35 File Offset: 0x0019BF35
		private void ColorPropertyChangedListenerOf_widget_0_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0_0(propertyName);
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x0019DD3E File Offset: 0x0019BF3E
		private void HandleWidgetPropertyChangeOf_widget_0_0_0(string propertyName)
		{
			if (propertyName == "MaterialType")
			{
				this._datasource_Root.ResourceMaterialTypeAsStr = this._widget_0_0_0.MaterialType;
				return;
			}
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x0019DD64 File Offset: 0x0019BF64
		private void PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x0019DD6D File Offset: 0x0019BF6D
		private void boolPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x0019DD76 File Offset: 0x0019BF76
		private void floatPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x0019DD7F File Offset: 0x0019BF7F
		private void Vec2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x0019DD88 File Offset: 0x0019BF88
		private void Vector2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x0019DD91 File Offset: 0x0019BF91
		private void doublePropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x0019DD9A File Offset: 0x0019BF9A
		private void intPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x0019DDA3 File Offset: 0x0019BFA3
		private void uintPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x0019DDAC File Offset: 0x0019BFAC
		private void ColorPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x0019DDB5 File Offset: 0x0019BFB5
		private void HandleWidgetPropertyChangeOf_widget_0_1(string propertyName)
		{
			if (propertyName == "IntText")
			{
				this._datasource_Root.ResourceAmount = this._widget_0_1.IntText;
				return;
			}
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x0019DDDB File Offset: 0x0019BFDB
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x0019DDE9 File Offset: 0x0019BFE9
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x0019DDF7 File Offset: 0x0019BFF7
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x0019DE05 File Offset: 0x0019C005
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x0019DE13 File Offset: 0x0019C013
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x0019DE21 File Offset: 0x0019C021
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x0019DE2F File Offset: 0x0019C02F
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x0019DE3D File Offset: 0x0019C03D
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x0019DE4B File Offset: 0x0019C04B
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x0019DE5C File Offset: 0x0019C05C
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "ResourceHint")
			{
				this.RefreshDataSource_datasource_Root_ResourceHint(this._datasource_Root.ResourceHint);
				return;
			}
			if (propertyName == "ResourceAmount")
			{
				this._widget.IndexToWatch = this._datasource_Root.ResourceAmount;
				this._widget_0_1.IntText = this._datasource_Root.ResourceAmount;
				return;
			}
			if (propertyName == "ResourceMaterialTypeAsStr")
			{
				this._widget_0_0_0.MaterialType = this._datasource_Root.ResourceMaterialTypeAsStr;
				return;
			}
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x0019DEE6 File Offset: 0x0019C0E6
		private void ViewModelPropertyChangedListenerOf_datasource_Root_ResourceHint(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(e.PropertyName);
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x0019DEF4 File Offset: 0x0019C0F4
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_ResourceHint(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(e.PropertyName);
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x0019DF02 File Offset: 0x0019C102
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_ResourceHint(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(e.PropertyName);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x0019DF10 File Offset: 0x0019C110
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_ResourceHint(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(e.PropertyName);
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x0019DF1E File Offset: 0x0019C11E
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_ResourceHint(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(e.PropertyName);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x0019DF2C File Offset: 0x0019C12C
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_ResourceHint(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(e.PropertyName);
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x0019DF3A File Offset: 0x0019C13A
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_ResourceHint(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(e.PropertyName);
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x0019DF48 File Offset: 0x0019C148
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_ResourceHint(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(e.PropertyName);
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x0019DF56 File Offset: 0x0019C156
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_ResourceHint(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(e.PropertyName);
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x0019DF64 File Offset: 0x0019C164
		private void HandleViewModelPropertyChangeOf_datasource_Root_ResourceHint(string propertyName)
		{
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x0019DF68 File Offset: 0x0019C168
		private void RefreshDataSource_datasource_Root(CraftingResourceItemVM newDataSource)
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
				this._widget_0_0_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_1;
				if (this._datasource_Root_ResourceHint != null)
				{
					this._datasource_Root_ResourceHint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_ResourceHint;
					this._widget_1.EventFire -= this.EventListenerOf_widget_1;
					this._datasource_Root_ResourceHint = null;
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
				this._widget.IndexToWatch = this._datasource_Root.ResourceAmount;
				this._widget.PropertyChanged += this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged += this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget;
				this._widget_0_0_0.MaterialType = this._datasource_Root.ResourceMaterialTypeAsStr;
				this._widget_0_0_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_0_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0_0_0;
				this._widget_0_1.IntText = this._datasource_Root.ResourceAmount;
				this._widget_0_1.PropertyChanged += this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0_1;
				this._datasource_Root_ResourceHint = this._datasource_Root.ResourceHint;
				if (this._datasource_Root_ResourceHint != null)
				{
					this._datasource_Root_ResourceHint.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_ResourceHint;
					this._datasource_Root_ResourceHint.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_ResourceHint;
					this._widget_1.EventFire += this.EventListenerOf_widget_1;
				}
			}
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x0019E850 File Offset: 0x0019CA50
		private void RefreshDataSource_datasource_Root_ResourceHint(HintViewModel newDataSource)
		{
			if (this._datasource_Root_ResourceHint != null)
			{
				this._datasource_Root_ResourceHint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_ResourceHint;
				this._widget_1.EventFire -= this.EventListenerOf_widget_1;
				this._datasource_Root_ResourceHint = null;
			}
			this._datasource_Root_ResourceHint = newDataSource;
			this._datasource_Root_ResourceHint = this._datasource_Root.ResourceHint;
			if (this._datasource_Root_ResourceHint != null)
			{
				this._datasource_Root_ResourceHint.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_ResourceHint;
				this._datasource_Root_ResourceHint.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_ResourceHint;
				this._widget_1.EventFire += this.EventListenerOf_widget_1;
			}
		}

		// Token: 0x04000AA7 RID: 2727
		private ValueBasedVisibilityWidget _widget;

		// Token: 0x04000AA8 RID: 2728
		private ListPanel _widget_0;

		// Token: 0x04000AA9 RID: 2729
		private Widget _widget_0_0;

		// Token: 0x04000AAA RID: 2730
		private CraftingMaterialVisualBrushWidget _widget_0_0_0;

		// Token: 0x04000AAB RID: 2731
		private TextWidget _widget_0_1;

		// Token: 0x04000AAC RID: 2732
		private HintWidget _widget_1;

		// Token: 0x04000AAD RID: 2733
		private CraftingResourceItemVM _datasource_Root;

		// Token: 0x04000AAE RID: 2734
		private HintViewModel _datasource_Root_ResourceHint;
	}
}
