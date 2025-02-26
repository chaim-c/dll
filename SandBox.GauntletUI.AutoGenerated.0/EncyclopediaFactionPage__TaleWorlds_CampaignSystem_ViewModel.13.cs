﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x02000031 RID: 49
	public class EncyclopediaFactionPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_12_EncyclopediaSubPageHistoryElement__InheritedPrefab : Widget
	{
		// Token: 0x06000D18 RID: 3352 RVA: 0x000608CC File Offset: 0x0005EACC
		public EncyclopediaFactionPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_12_EncyclopediaSubPageHistoryElement__InheritedPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x000608D8 File Offset: 0x0005EAD8
		public virtual void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new Widget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new ListPanel(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_1_0 = new RichTextWidget(base.Context);
			this._widget_1.AddChild(this._widget_1_0);
			this._widget_1_1 = new RichTextWidget(base.Context);
			this._widget_1.AddChild(this._widget_1_1);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00060974 File Offset: 0x0005EB74
		public virtual void SetIds()
		{
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00060978 File Offset: 0x0005EB78
		public virtual void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.MarginTop = 5f;
			this._widget_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0.SuggestedHeight = 6f;
			this._widget_0.SuggestedWidth = 6f;
			this._widget_0.Sprite = base.Context.SpriteData.GetSprite("Encyclopedia\\subpage_ball");
			this._widget_0.MarginTop = 15f;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.StackLayout.LayoutMethod = LayoutMethod.VerticalBottomToTop;
			this._widget_1_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1_0.MarginLeft = 15f;
			this._widget_1_0.Brush = base.Context.GetBrush("Encyclopedia.SubPage.History.Text");
			this._widget_1_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1_1.MarginLeft = 15f;
			this._widget_1_1.Brush = base.Context.GetBrush("Encyclopedia.SubPage.History.Text");
			this._widget_1_1.Brush.FontSize = 16;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00060AD4 File Offset: 0x0005ECD4
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
				this._widget_1_0.EventFire -= this.EventListenerOf_widget_1_0;
				this._widget_1_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_0;
				this._widget_1_1.EventFire -= this.EventListenerOf_widget_1_1;
				this._widget_1_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_1;
				this._datasource_Root = null;
			}
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00060D8E File Offset: 0x0005EF8E
		public virtual void SetDataSource(EncyclopediaHistoryEventVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00060D98 File Offset: 0x0005EF98
		private void EventListenerOf_widget_1_0(Widget widget, string commandName, object[] args)
		{
			if (commandName == "LinkClick")
			{
				string link = (string)args[0];
				this._datasource_Root.ExecuteLink(link);
			}
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00060DC8 File Offset: 0x0005EFC8
		private void EventListenerOf_widget_1_1(Widget widget, string commandName, object[] args)
		{
			if (commandName == "LinkClick")
			{
				string link = (string)args[0];
				this._datasource_Root.ExecuteLink(link);
			}
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00060DF7 File Offset: 0x0005EFF7
		private void PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00060E00 File Offset: 0x0005F000
		private void boolPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00060E09 File Offset: 0x0005F009
		private void floatPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x00060E12 File Offset: 0x0005F012
		private void Vec2PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00060E1B File Offset: 0x0005F01B
		private void Vector2PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00060E24 File Offset: 0x0005F024
		private void doublePropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x00060E2D File Offset: 0x0005F02D
		private void intPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00060E36 File Offset: 0x0005F036
		private void uintPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00060E3F File Offset: 0x0005F03F
		private void ColorPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00060E48 File Offset: 0x0005F048
		private void HandleWidgetPropertyChangeOf_widget_1_0(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.HistoryEventText = this._widget_1_0.Text;
				return;
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00060E6E File Offset: 0x0005F06E
		private void PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00060E77 File Offset: 0x0005F077
		private void boolPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00060E80 File Offset: 0x0005F080
		private void floatPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00060E89 File Offset: 0x0005F089
		private void Vec2PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x00060E92 File Offset: 0x0005F092
		private void Vector2PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x00060E9B File Offset: 0x0005F09B
		private void doublePropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x00060EA4 File Offset: 0x0005F0A4
		private void intPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x00060EAD File Offset: 0x0005F0AD
		private void uintPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00060EB6 File Offset: 0x0005F0B6
		private void ColorPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00060EBF File Offset: 0x0005F0BF
		private void HandleWidgetPropertyChangeOf_widget_1_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.HistoryEventTimeText = this._widget_1_1.Text;
				return;
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00060EE5 File Offset: 0x0005F0E5
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00060EF3 File Offset: 0x0005F0F3
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00060F01 File Offset: 0x0005F101
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00060F0F File Offset: 0x0005F10F
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00060F1D File Offset: 0x0005F11D
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00060F2B File Offset: 0x0005F12B
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00060F39 File Offset: 0x0005F139
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00060F47 File Offset: 0x0005F147
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x00060F55 File Offset: 0x0005F155
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x00060F64 File Offset: 0x0005F164
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "HistoryEventText")
			{
				this._widget_1_0.Text = this._datasource_Root.HistoryEventText;
				return;
			}
			if (propertyName == "HistoryEventTimeText")
			{
				this._widget_1_1.Text = this._datasource_Root.HistoryEventTimeText;
				return;
			}
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x00060FBC File Offset: 0x0005F1BC
		private void RefreshDataSource_datasource_Root(EncyclopediaHistoryEventVM newDataSource)
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
				this._widget_1_0.EventFire -= this.EventListenerOf_widget_1_0;
				this._widget_1_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_0;
				this._widget_1_1.EventFire -= this.EventListenerOf_widget_1_1;
				this._widget_1_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_1;
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
				this._widget_1_0.Text = this._datasource_Root.HistoryEventText;
				this._widget_1_0.EventFire += this.EventListenerOf_widget_1_0;
				this._widget_1_0.PropertyChanged += this.PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1_0;
				this._widget_1_1.Text = this._datasource_Root.HistoryEventTimeText;
				this._widget_1_1.EventFire += this.EventListenerOf_widget_1_1;
				this._widget_1_1.PropertyChanged += this.PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1_1;
			}
		}

		// Token: 0x04000299 RID: 665
		private Widget _widget;

		// Token: 0x0400029A RID: 666
		private Widget _widget_0;

		// Token: 0x0400029B RID: 667
		private ListPanel _widget_1;

		// Token: 0x0400029C RID: 668
		private RichTextWidget _widget_1_0;

		// Token: 0x0400029D RID: 669
		private RichTextWidget _widget_1_1;

		// Token: 0x0400029E RID: 670
		private EncyclopediaHistoryEventVM _datasource_Root;
	}
}
