﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.Core.ViewModelCollection.Selector;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace TaleWorlds.MountAndBlade.GauntletUI.AutoGenerated1
{
	// Token: 0x0200001A RID: 26
	public class FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_20_ItemTemplate : ButtonWidget
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0002D027 File Offset: 0x0002B227
		public FaceGen__TaleWorlds_MountAndBlade_ViewModelCollection_FaceGenerator_FaceGenVM_Dependency_20_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0002D030 File Offset: 0x0002B230
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ImageWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new ScrollingRichTextWidget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_2 = new HintWidget(base.Context);
			this._widget.AddChild(this._widget_2);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0002D0AA File Offset: 0x0002B2AA
		public void SetIds()
		{
			base.Id = "DropdownItemButton";
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0002D0B8 File Offset: 0x0002B2B8
		public void SetAttributes()
		{
			base.DoNotUseCustomScale = true;
			base.DoNotPassEventsToChildren = true;
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedHeight = 29f;
			base.MarginLeft = 10f;
			base.MarginRight = 10f;
			base.HorizontalAlignment = HorizontalAlignment.Center;
			base.VerticalAlignment = VerticalAlignment.Bottom;
			base.ButtonType = ButtonType.Radio;
			base.UpdateChildrenStates = true;
			base.Brush = base.Context.GetBrush("Standard.DropdownItem.SoundBrush");
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.MarginLeft = 5f;
			this._widget_0.MarginRight = 5f;
			this._widget_0.Brush = base.Context.GetBrush("Standard.DropdownItem");
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_1.MarginLeft = 7f;
			this._widget_1.MarginRight = 7f;
			this._widget_1.VerticalAlignment = VerticalAlignment.Center;
			this._widget_1.Brush = base.Context.GetBrush("SPOptions.Dropdown.Item.Text");
			this._widget_1.IsAutoScrolling = false;
			this._widget_1.ScrollOnHoverWidget = this._widget.FindChild(new BindingPath("..\\DropdownItemButton"));
			this._widget_2.DoNotAcceptEvents = true;
			this._widget_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2.HeightSizePolicy = SizePolicy.StretchToParent;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0002D244 File Offset: 0x0002B444
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
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
				if (this._datasource_Root_Hint != null)
				{
					this._datasource_Root_Hint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
					this._widget_2.EventFire -= this.EventListenerOf_widget_2;
					this._datasource_Root_Hint = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0002D5C8 File Offset: 0x0002B7C8
		public void SetDataSource(SelectorItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0002D5D1 File Offset: 0x0002B7D1
		private void EventListenerOf_widget_2(Widget widget, string commandName, object[] args)
		{
			if (commandName == "HoverBegin")
			{
				this._datasource_Root_Hint.ExecuteBeginHint();
			}
			if (commandName == "HoverEnd")
			{
				this._datasource_Root_Hint.ExecuteEndHint();
			}
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0002D603 File Offset: 0x0002B803
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0002D60C File Offset: 0x0002B80C
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0002D615 File Offset: 0x0002B815
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0002D61E File Offset: 0x0002B81E
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0002D627 File Offset: 0x0002B827
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0002D630 File Offset: 0x0002B830
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0002D639 File Offset: 0x0002B839
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x0002D642 File Offset: 0x0002B842
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0002D64B File Offset: 0x0002B84B
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002D654 File Offset: 0x0002B854
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "IsEnabled")
			{
				this._datasource_Root.CanBeSelected = this._widget.IsEnabled;
				return;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0002D67A File Offset: 0x0002B87A
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0002D683 File Offset: 0x0002B883
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0002D68C File Offset: 0x0002B88C
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0002D695 File Offset: 0x0002B895
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0002D69E File Offset: 0x0002B89E
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0002D6A7 File Offset: 0x0002B8A7
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002D6B0 File Offset: 0x0002B8B0
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002D6B9 File Offset: 0x0002B8B9
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002D6C2 File Offset: 0x0002B8C2
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002D6CB File Offset: 0x0002B8CB
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.StringItem = this._widget_1.Text;
				return;
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002D6F1 File Offset: 0x0002B8F1
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002D6FF File Offset: 0x0002B8FF
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002D70D File Offset: 0x0002B90D
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0002D71B File Offset: 0x0002B91B
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0002D729 File Offset: 0x0002B929
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002D737 File Offset: 0x0002B937
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002D745 File Offset: 0x0002B945
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002D753 File Offset: 0x0002B953
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x0002D761 File Offset: 0x0002B961
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0002D770 File Offset: 0x0002B970
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Hint")
			{
				this.RefreshDataSource_datasource_Root_Hint(this._datasource_Root.Hint);
				return;
			}
			if (propertyName == "CanBeSelected")
			{
				this._widget.IsEnabled = this._datasource_Root.CanBeSelected;
				return;
			}
			if (propertyName == "StringItem")
			{
				this._widget_1.Text = this._datasource_Root.StringItem;
				return;
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x0002D7E4 File Offset: 0x0002B9E4
		private void ViewModelPropertyChangedListenerOf_datasource_Root_Hint(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x0002D7F2 File Offset: 0x0002B9F2
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0002D800 File Offset: 0x0002BA00
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0002D80E File Offset: 0x0002BA0E
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0002D81C File Offset: 0x0002BA1C
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x0002D82A File Offset: 0x0002BA2A
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x0002D838 File Offset: 0x0002BA38
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0002D846 File Offset: 0x0002BA46
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x0002D854 File Offset: 0x0002BA54
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0002D862 File Offset: 0x0002BA62
		private void HandleViewModelPropertyChangeOf_datasource_Root_Hint(string propertyName)
		{
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0002D864 File Offset: 0x0002BA64
		private void RefreshDataSource_datasource_Root(SelectorItemVM newDataSource)
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
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
				if (this._datasource_Root_Hint != null)
				{
					this._datasource_Root_Hint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
					this._widget_2.EventFire -= this.EventListenerOf_widget_2;
					this._datasource_Root_Hint = null;
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
				this._widget.IsEnabled = this._datasource_Root.CanBeSelected;
				this._widget.PropertyChanged += this.PropertyChangedListenerOf_widget;
				this._widget.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget;
				this._widget.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget;
				this._widget.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget;
				this._widget.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget;
				this._widget.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget;
				this._widget.intPropertyChanged += this.intPropertyChangedListenerOf_widget;
				this._widget.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget;
				this._widget.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget;
				this._widget_1.Text = this._datasource_Root.StringItem;
				this._widget_1.PropertyChanged += this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1;
				this._datasource_Root_Hint = this._datasource_Root.Hint;
				if (this._datasource_Root_Hint != null)
				{
					this._datasource_Root_Hint.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
					this._datasource_Root_Hint.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
					this._widget_2.EventFire += this.EventListenerOf_widget_2;
				}
			}
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0002DF98 File Offset: 0x0002C198
		private void RefreshDataSource_datasource_Root_Hint(HintViewModel newDataSource)
		{
			if (this._datasource_Root_Hint != null)
			{
				this._datasource_Root_Hint.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
				this._widget_2.EventFire -= this.EventListenerOf_widget_2;
				this._datasource_Root_Hint = null;
			}
			this._datasource_Root_Hint = newDataSource;
			this._datasource_Root_Hint = this._datasource_Root.Hint;
			if (this._datasource_Root_Hint != null)
			{
				this._datasource_Root_Hint.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint;
				this._datasource_Root_Hint.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint;
				this._widget_2.EventFire += this.EventListenerOf_widget_2;
			}
		}

		// Token: 0x04000169 RID: 361
		private ButtonWidget _widget;

		// Token: 0x0400016A RID: 362
		private ImageWidget _widget_0;

		// Token: 0x0400016B RID: 363
		private ScrollingRichTextWidget _widget_1;

		// Token: 0x0400016C RID: 364
		private HintWidget _widget_2;

		// Token: 0x0400016D RID: 365
		private SelectorItemVM _datasource_Root;

		// Token: 0x0400016E RID: 366
		private HintViewModel _datasource_Root_Hint;
	}
}
