﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Order;
using TaleWorlds.MountAndBlade.ViewModelCollection.Order;

namespace TaleWorlds.MountAndBlade.GauntletUI.AutoGenerated0
{
	// Token: 0x0200002D RID: 45
	public class OrderRadial__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM_Dependency_8_ItemTemplate : OrderSiegeMachineItemButtonWidget
	{
		// Token: 0x0600097C RID: 2428 RVA: 0x00043AE7 File Offset: 0x00041CE7
		public OrderRadial__TaleWorlds_MountAndBlade_ViewModelCollection_Order_MissionOrderVM_Dependency_8_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00043AF0 File Offset: 0x00041CF0
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new BrushWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new TextWidget(base.Context);
			this._widget.AddChild(this._widget_1);
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00043B48 File Offset: 0x00041D48
		public void SetIds()
		{
			this._widget_0.Id = "Icon";
			this._widget_1.Id = "RemainingCountText";
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00043B6C File Offset: 0x00041D6C
		public void SetAttributes()
		{
			base.DoNotPassEventsToChildren = true;
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedWidth = 60f;
			base.SuggestedHeight = 60f;
			base.Brush = base.Context.GetBrush("Order.Siege.Deployment.MachineItem");
			base.ButtonType = ButtonType.Radio;
			base.IsDisabled = true;
			base.MachineIconWidget = this._widget_0;
			base.RemainingCountWidget = this._widget_1;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.MarginLeft = 7f;
			this._widget_0.MarginRight = 7f;
			this._widget_0.MarginBottom = 7f;
			this._widget_0.MarginTop = 7f;
			this._widget_0.Brush = base.Context.GetBrush("Order.Siege.Deployment.MachineIcon");
			this._widget_1.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_1.VerticalAlignment = VerticalAlignment.Top;
			this._widget_1.MarginRight = 5f;
			this._widget_1.MarginTop = 5f;
			this._widget_1.Brush.FontSize = 18;
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x00043CB8 File Offset: 0x00041EB8
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
				this._widget.EventFire -= this.EventListenerOf_widget;
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
				this._datasource_Root = null;
			}
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x00043F5B File Offset: 0x0004215B
		public void SetDataSource(DeploymentSiegeMachineVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x00043F64 File Offset: 0x00042164
		private void EventListenerOf_widget(Widget widget, string commandName, object[] args)
		{
			if (commandName == "Click")
			{
				this._datasource_Root.ExecuteAction();
			}
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x00043F7E File Offset: 0x0004217E
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00043F87 File Offset: 0x00042187
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00043F90 File Offset: 0x00042190
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00043F99 File Offset: 0x00042199
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x00043FA2 File Offset: 0x000421A2
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x00043FAB File Offset: 0x000421AB
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00043FB4 File Offset: 0x000421B4
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00043FBD File Offset: 0x000421BD
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x00043FC6 File Offset: 0x000421C6
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00043FD0 File Offset: 0x000421D0
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "IsSelected")
			{
				this._datasource_Root.IsSelected = this._widget.IsSelected;
				return;
			}
			if (propertyName == "MachineClass")
			{
				this._datasource_Root.MachineClass = this._widget.MachineClass;
				return;
			}
			if (propertyName == "RemainingCount")
			{
				this._datasource_Root.RemainingCount = this._widget.RemainingCount;
				return;
			}
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00044049 File Offset: 0x00042249
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00044052 File Offset: 0x00042252
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0004405B File Offset: 0x0004225B
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x00044064 File Offset: 0x00042264
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0004406D File Offset: 0x0004226D
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x00044076 File Offset: 0x00042276
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0004407F File Offset: 0x0004227F
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00044088 File Offset: 0x00042288
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x00044091 File Offset: 0x00042291
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0004409A File Offset: 0x0004229A
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "IntText")
			{
				this._datasource_Root.RemainingCount = this._widget_1.IntText;
				return;
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x000440C0 File Offset: 0x000422C0
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x000440CE File Offset: 0x000422CE
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000440DC File Offset: 0x000422DC
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x000440EA File Offset: 0x000422EA
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x000440F8 File Offset: 0x000422F8
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00044106 File Offset: 0x00042306
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00044114 File Offset: 0x00042314
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00044122 File Offset: 0x00042322
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00044130 File Offset: 0x00042330
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00044140 File Offset: 0x00042340
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "IsSelected")
			{
				this._widget.IsSelected = this._datasource_Root.IsSelected;
				return;
			}
			if (propertyName == "MachineClass")
			{
				this._widget.MachineClass = this._datasource_Root.MachineClass;
				return;
			}
			if (propertyName == "RemainingCount")
			{
				this._widget.RemainingCount = this._datasource_Root.RemainingCount;
				this._widget_1.IntText = this._datasource_Root.RemainingCount;
				return;
			}
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x000441D0 File Offset: 0x000423D0
		private void RefreshDataSource_datasource_Root(DeploymentSiegeMachineVM newDataSource)
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
				this._widget.EventFire -= this.EventListenerOf_widget;
				this._widget.PropertyChanged -= this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged -= this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget;
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
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
				this._widget.IsSelected = this._datasource_Root.IsSelected;
				this._widget.MachineClass = this._datasource_Root.MachineClass;
				this._widget.RemainingCount = this._datasource_Root.RemainingCount;
				this._widget.EventFire += this.EventListenerOf_widget;
				this._widget.PropertyChanged += this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged += this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget;
				this._widget_1.IntText = this._datasource_Root.RemainingCount;
				this._widget_1.PropertyChanged += this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1;
			}
		}

		// Token: 0x04000187 RID: 391
		private OrderSiegeMachineItemButtonWidget _widget;

		// Token: 0x04000188 RID: 392
		private BrushWidget _widget_0;

		// Token: 0x04000189 RID: 393
		private TextWidget _widget_1;

		// Token: 0x0400018A RID: 394
		private DeploymentSiegeMachineVM _datasource_Root;
	}
}
