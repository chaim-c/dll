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

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x02000054 RID: 84
	public class EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaConceptPageVM_Dependency_6_ItemTemplate : ButtonWidget
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x0008F61D File Offset: 0x0008D81D
		public EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaConceptPageVM_Dependency_6_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0008F628 File Offset: 0x0008D828
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

		// Token: 0x060013A2 RID: 5026 RVA: 0x0008F6A2 File Offset: 0x0008D8A2
		public void SetIds()
		{
			base.Id = "DropdownItemButton";
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0008F6B0 File Offset: 0x0008D8B0
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

		// Token: 0x060013A4 RID: 5028 RVA: 0x0008F83C File Offset: 0x0008DA3C
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

		// Token: 0x060013A5 RID: 5029 RVA: 0x0008FBC0 File Offset: 0x0008DDC0
		public void SetDataSource(EncyclopediaListSelectorItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0008FBC9 File Offset: 0x0008DDC9
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

		// Token: 0x060013A7 RID: 5031 RVA: 0x0008FBFB File Offset: 0x0008DDFB
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0008FC04 File Offset: 0x0008DE04
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0008FC0D File Offset: 0x0008DE0D
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x0008FC16 File Offset: 0x0008DE16
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0008FC1F File Offset: 0x0008DE1F
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0008FC28 File Offset: 0x0008DE28
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0008FC31 File Offset: 0x0008DE31
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0008FC3A File Offset: 0x0008DE3A
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013AF RID: 5039 RVA: 0x0008FC43 File Offset: 0x0008DE43
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013B0 RID: 5040 RVA: 0x0008FC4C File Offset: 0x0008DE4C
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "IsEnabled")
			{
				this._datasource_Root.CanBeSelected = this._widget.IsEnabled;
				return;
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0008FC72 File Offset: 0x0008DE72
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0008FC7B File Offset: 0x0008DE7B
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0008FC84 File Offset: 0x0008DE84
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0008FC8D File Offset: 0x0008DE8D
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x0008FC96 File Offset: 0x0008DE96
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x0008FC9F File Offset: 0x0008DE9F
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0008FCA8 File Offset: 0x0008DEA8
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x0008FCB1 File Offset: 0x0008DEB1
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x0008FCBA File Offset: 0x0008DEBA
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x0008FCC3 File Offset: 0x0008DEC3
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.StringItem = this._widget_1.Text;
				return;
			}
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0008FCE9 File Offset: 0x0008DEE9
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013BC RID: 5052 RVA: 0x0008FCF7 File Offset: 0x0008DEF7
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x0008FD05 File Offset: 0x0008DF05
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013BE RID: 5054 RVA: 0x0008FD13 File Offset: 0x0008DF13
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013BF RID: 5055 RVA: 0x0008FD21 File Offset: 0x0008DF21
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x0008FD2F File Offset: 0x0008DF2F
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013C1 RID: 5057 RVA: 0x0008FD3D File Offset: 0x0008DF3D
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013C2 RID: 5058 RVA: 0x0008FD4B File Offset: 0x0008DF4B
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013C3 RID: 5059 RVA: 0x0008FD59 File Offset: 0x0008DF59
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013C4 RID: 5060 RVA: 0x0008FD68 File Offset: 0x0008DF68
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

		// Token: 0x060013C5 RID: 5061 RVA: 0x0008FDDC File Offset: 0x0008DFDC
		private void ViewModelPropertyChangedListenerOf_datasource_Root_Hint(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060013C6 RID: 5062 RVA: 0x0008FDEA File Offset: 0x0008DFEA
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x0008FDF8 File Offset: 0x0008DFF8
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x0008FE06 File Offset: 0x0008E006
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x0008FE14 File Offset: 0x0008E014
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0008FE22 File Offset: 0x0008E022
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x0008FE30 File Offset: 0x0008E030
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x0008FE3E File Offset: 0x0008E03E
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0008FE4C File Offset: 0x0008E04C
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0008FE5A File Offset: 0x0008E05A
		private void HandleViewModelPropertyChangeOf_datasource_Root_Hint(string propertyName)
		{
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0008FE5C File Offset: 0x0008E05C
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

		// Token: 0x060013D0 RID: 5072 RVA: 0x00090590 File Offset: 0x0008E790
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

		// Token: 0x0400040E RID: 1038
		private ButtonWidget _widget;

		// Token: 0x0400040F RID: 1039
		private ImageWidget _widget_0;

		// Token: 0x04000410 RID: 1040
		private ScrollingRichTextWidget _widget_1;

		// Token: 0x04000411 RID: 1041
		private HintWidget _widget_2;

		// Token: 0x04000412 RID: 1042
		private EncyclopediaListSelectorItemVM _datasource_Root;

		// Token: 0x04000413 RID: 1043
		private HintViewModel _datasource_Root_Hint;
	}
}
