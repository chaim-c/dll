﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x02000056 RID: 86
	public class EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaConceptPageVM_Dependency_8_EncyclopediaFilterListItem__InheritedPrefab : EncyclopediaFilterListItemButtonWidget
	{
		// Token: 0x060013E2 RID: 5090 RVA: 0x00090B1B File Offset: 0x0008ED1B
		public EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaConceptPageVM_Dependency_8_EncyclopediaFilterListItem__InheritedPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00090B24 File Offset: 0x0008ED24
		public virtual void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new Widget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_1 = new ScrollingTextWidget(base.Context);
			this._widget.AddChild(this._widget_1);
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x00090B9E File Offset: 0x0008ED9E
		public virtual void SetIds()
		{
			this._widget_0.Id = "ToggleIndicatorParent";
			this._widget_0_0.Id = "ToggleIndicator";
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00090BC0 File Offset: 0x0008EDC0
		public virtual void SetAttributes()
		{
			base.DoNotPassEventsToChildren = true;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.SuggestedHeight = 36f;
			base.ToggleIndicator = this._widget_0_0;
			base.ButtonType = ButtonType.Toggle;
			base.Brush = base.Context.GetBrush("Encyclopedia.FilterListButton");
			this._widget_0.DoNotPassEventsToChildren = true;
			this._widget_0.Sprite = base.Context.SpriteData.GetSprite("Encyclopedia\\list_filters_checkbox");
			this._widget_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0.SuggestedHeight = 44f;
			this._widget_0.SuggestedWidth = 44f;
			this._widget_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0.IsEnabled = false;
			this._widget_0_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_0_0.Sprite = base.Context.SpriteData.GetSprite("Encyclopedia\\list_filters_checkbox_full");
			this._widget_0_0.IsDisabled = true;
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.SuggestedHeight = 35f;
			this._widget_1.MarginLeft = 43f;
			this._widget_1.Brush = base.Context.GetBrush("EncyclopediaList.Filter.Name.Text");
			this._widget_1.Brush.FontSize = 30;
			this._widget_1.IsDisabled = true;
			this._widget_1.PositionYOffset = 0f;
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x00090D58 File Offset: 0x0008EF58
		public virtual void DestroyDataSource()
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

		// Token: 0x060013E7 RID: 5095 RVA: 0x00090FFB File Offset: 0x0008F1FB
		public virtual void SetDataSource(EncyclopediaListFilterVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00091004 File Offset: 0x0008F204
		private void EventListenerOf_widget(Widget widget, string commandName, object[] args)
		{
			if (commandName == "Click")
			{
				this._datasource_Root.ExecuteOnFilterActivated();
			}
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0009101E File Offset: 0x0008F21E
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00091027 File Offset: 0x0008F227
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00091030 File Offset: 0x0008F230
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00091039 File Offset: 0x0008F239
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x00091042 File Offset: 0x0008F242
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0009104B File Offset: 0x0008F24B
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x00091054 File Offset: 0x0008F254
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0009105D File Offset: 0x0008F25D
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x00091066 File Offset: 0x0008F266
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0009106F File Offset: 0x0008F26F
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "IsSelected")
			{
				this._datasource_Root.IsSelected = this._widget.IsSelected;
				return;
			}
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x00091095 File Offset: 0x0008F295
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0009109E File Offset: 0x0008F29E
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x000910A7 File Offset: 0x0008F2A7
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x000910B0 File Offset: 0x0008F2B0
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x000910B9 File Offset: 0x0008F2B9
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x000910C2 File Offset: 0x0008F2C2
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013F9 RID: 5113 RVA: 0x000910CB File Offset: 0x0008F2CB
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013FA RID: 5114 RVA: 0x000910D4 File Offset: 0x0008F2D4
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x000910DD File Offset: 0x0008F2DD
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x000910E6 File Offset: 0x0008F2E6
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.Name = this._widget_1.Text;
				return;
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x0009110C File Offset: 0x0008F30C
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0009111A File Offset: 0x0008F31A
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x00091128 File Offset: 0x0008F328
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x00091136 File Offset: 0x0008F336
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00091144 File Offset: 0x0008F344
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00091152 File Offset: 0x0008F352
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00091160 File Offset: 0x0008F360
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x0009116E File Offset: 0x0008F36E
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x0009117C File Offset: 0x0008F37C
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x0009118C File Offset: 0x0008F38C
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "IsSelected")
			{
				this._widget.IsSelected = this._datasource_Root.IsSelected;
				return;
			}
			if (propertyName == "Name")
			{
				this._widget_1.Text = this._datasource_Root.Name;
				return;
			}
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x000911E4 File Offset: 0x0008F3E4
		private void RefreshDataSource_datasource_Root(EncyclopediaListFilterVM newDataSource)
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
				this._widget_1.Text = this._datasource_Root.Name;
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

		// Token: 0x04000416 RID: 1046
		private EncyclopediaFilterListItemButtonWidget _widget;

		// Token: 0x04000417 RID: 1047
		private Widget _widget_0;

		// Token: 0x04000418 RID: 1048
		private Widget _widget_0_0;

		// Token: 0x04000419 RID: 1049
		private ScrollingTextWidget _widget_1;

		// Token: 0x0400041A RID: 1050
		private EncyclopediaListFilterVM _datasource_Root;
	}
}
