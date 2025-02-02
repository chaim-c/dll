﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x020000CF RID: 207
	public class EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaPageVM_Dependency_6_ItemTemplate : ButtonWidget
	{
		// Token: 0x06003C51 RID: 15441 RVA: 0x001DF601 File Offset: 0x001DD801
		public EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaPageVM_Dependency_6_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06003C52 RID: 15442 RVA: 0x001DF60C File Offset: 0x001DD80C
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

		// Token: 0x06003C53 RID: 15443 RVA: 0x001DF686 File Offset: 0x001DD886
		public void SetIds()
		{
			base.Id = "DropdownItemButton";
		}

		// Token: 0x06003C54 RID: 15444 RVA: 0x001DF694 File Offset: 0x001DD894
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
			this._widget_0.Brush = base.Context.GetBrush("Standard.DropdownItem.Flat");
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_1.MarginLeft = 7f;
			this._widget_1.MarginRight = 7f;
			this._widget_1.VerticalAlignment = VerticalAlignment.Center;
			this._widget_1.Brush = base.Context.GetBrush("Standard.DropdownItem.Text");
			this._widget_1.IsAutoScrolling = false;
			this._widget_1.ScrollOnHoverWidget = this._widget.FindChild(new BindingPath("..\\DropdownItemButton"));
			this._widget_2.DoNotAcceptEvents = true;
			this._widget_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2.HeightSizePolicy = SizePolicy.StretchToParent;
		}

		// Token: 0x06003C55 RID: 15445 RVA: 0x001DF820 File Offset: 0x001DDA20
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

		// Token: 0x06003C56 RID: 15446 RVA: 0x001DFBA4 File Offset: 0x001DDDA4
		public void SetDataSource(EncyclopediaListSelectorItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06003C57 RID: 15447 RVA: 0x001DFBAD File Offset: 0x001DDDAD
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

		// Token: 0x06003C58 RID: 15448 RVA: 0x001DFBDF File Offset: 0x001DDDDF
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06003C59 RID: 15449 RVA: 0x001DFBE8 File Offset: 0x001DDDE8
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06003C5A RID: 15450 RVA: 0x001DFBF1 File Offset: 0x001DDDF1
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06003C5B RID: 15451 RVA: 0x001DFBFA File Offset: 0x001DDDFA
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06003C5C RID: 15452 RVA: 0x001DFC03 File Offset: 0x001DDE03
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06003C5D RID: 15453 RVA: 0x001DFC0C File Offset: 0x001DDE0C
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06003C5E RID: 15454 RVA: 0x001DFC15 File Offset: 0x001DDE15
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06003C5F RID: 15455 RVA: 0x001DFC1E File Offset: 0x001DDE1E
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06003C60 RID: 15456 RVA: 0x001DFC27 File Offset: 0x001DDE27
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x06003C61 RID: 15457 RVA: 0x001DFC30 File Offset: 0x001DDE30
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "IsEnabled")
			{
				this._datasource_Root.CanBeSelected = this._widget.IsEnabled;
				return;
			}
		}

		// Token: 0x06003C62 RID: 15458 RVA: 0x001DFC56 File Offset: 0x001DDE56
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003C63 RID: 15459 RVA: 0x001DFC5F File Offset: 0x001DDE5F
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003C64 RID: 15460 RVA: 0x001DFC68 File Offset: 0x001DDE68
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003C65 RID: 15461 RVA: 0x001DFC71 File Offset: 0x001DDE71
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003C66 RID: 15462 RVA: 0x001DFC7A File Offset: 0x001DDE7A
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003C67 RID: 15463 RVA: 0x001DFC83 File Offset: 0x001DDE83
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003C68 RID: 15464 RVA: 0x001DFC8C File Offset: 0x001DDE8C
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003C69 RID: 15465 RVA: 0x001DFC95 File Offset: 0x001DDE95
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003C6A RID: 15466 RVA: 0x001DFC9E File Offset: 0x001DDE9E
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x06003C6B RID: 15467 RVA: 0x001DFCA7 File Offset: 0x001DDEA7
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.StringItem = this._widget_1.Text;
				return;
			}
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x001DFCCD File Offset: 0x001DDECD
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003C6D RID: 15469 RVA: 0x001DFCDB File Offset: 0x001DDEDB
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003C6E RID: 15470 RVA: 0x001DFCE9 File Offset: 0x001DDEE9
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003C6F RID: 15471 RVA: 0x001DFCF7 File Offset: 0x001DDEF7
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003C70 RID: 15472 RVA: 0x001DFD05 File Offset: 0x001DDF05
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003C71 RID: 15473 RVA: 0x001DFD13 File Offset: 0x001DDF13
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x001DFD21 File Offset: 0x001DDF21
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003C73 RID: 15475 RVA: 0x001DFD2F File Offset: 0x001DDF2F
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x001DFD3D File Offset: 0x001DDF3D
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003C75 RID: 15477 RVA: 0x001DFD4C File Offset: 0x001DDF4C
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

		// Token: 0x06003C76 RID: 15478 RVA: 0x001DFDC0 File Offset: 0x001DDFC0
		private void ViewModelPropertyChangedListenerOf_datasource_Root_Hint(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x001DFDCE File Offset: 0x001DDFCE
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06003C78 RID: 15480 RVA: 0x001DFDDC File Offset: 0x001DDFDC
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06003C79 RID: 15481 RVA: 0x001DFDEA File Offset: 0x001DDFEA
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06003C7A RID: 15482 RVA: 0x001DFDF8 File Offset: 0x001DDFF8
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06003C7B RID: 15483 RVA: 0x001DFE06 File Offset: 0x001DE006
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06003C7C RID: 15484 RVA: 0x001DFE14 File Offset: 0x001DE014
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06003C7D RID: 15485 RVA: 0x001DFE22 File Offset: 0x001DE022
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06003C7E RID: 15486 RVA: 0x001DFE30 File Offset: 0x001DE030
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x06003C7F RID: 15487 RVA: 0x001DFE3E File Offset: 0x001DE03E
		private void HandleViewModelPropertyChangeOf_datasource_Root_Hint(string propertyName)
		{
		}

		// Token: 0x06003C80 RID: 15488 RVA: 0x001DFE40 File Offset: 0x001DE040
		private void RefreshDataSource_datasource_Root(EncyclopediaListSelectorItemVM newDataSource)
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

		// Token: 0x06003C81 RID: 15489 RVA: 0x001E0574 File Offset: 0x001DE774
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

		// Token: 0x04000C76 RID: 3190
		private ButtonWidget _widget;

		// Token: 0x04000C77 RID: 3191
		private ImageWidget _widget_0;

		// Token: 0x04000C78 RID: 3192
		private ScrollingRichTextWidget _widget_1;

		// Token: 0x04000C79 RID: 3193
		private HintWidget _widget_2;

		// Token: 0x04000C7A RID: 3194
		private EncyclopediaListSelectorItemVM _datasource_Root;

		// Token: 0x04000C7B RID: 3195
		private HintViewModel _datasource_Root_Hint;
	}
}
