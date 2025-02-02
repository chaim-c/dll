﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.Core.ViewModelCollection.Generic;
using TaleWorlds.Core.ViewModelCollection.Information;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Tutorial;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x020000A2 RID: 162
	public class SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_10_ItemTemplate : Widget
	{
		// Token: 0x060027B9 RID: 10169 RVA: 0x0012B5F2 File Offset: 0x001297F2
		public SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_10_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x0012B5FC File Offset: 0x001297FC
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new NavigationTargetSwitcher(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new ButtonWidget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_1_0 = new TextWidget(base.Context);
			this._widget_1.AddChild(this._widget_1_0);
			this._widget_1_1 = new TutorialHighlightItemBrushWidget(base.Context);
			this._widget_1.AddChild(this._widget_1_1);
			this._widget_1_2 = new HintWidget(base.Context);
			this._widget_1.AddChild(this._widget_1_2);
			this._widget_2 = new HintWidget(base.Context);
			this._widget.AddChild(this._widget_2);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x0012B6DC File Offset: 0x001298DC
		public void SetIds()
		{
			this._widget_1.Id = "Button";
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x0012B6F0 File Offset: 0x001298F0
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedWidth = 227f;
			base.SuggestedHeight = 40f;
			base.MarginBottom = 8f;
			this._widget_0.FromTarget = this._widget;
			this._widget_0.ToTarget = this._widget_1;
			this._widget_1.DoNotPassEventsToChildren = true;
			this._widget_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.Brush = base.Context.GetBrush("ButtonBrush2");
			this._widget_1_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_1_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_1_0.PositionYOffset = 1f;
			this._widget_1_0.MarginLeft = 5f;
			this._widget_1_0.MarginRight = 5f;
			this._widget_1_0.Brush = base.Context.GetBrush("OverlayPopup.ActionButtonText");
			this._widget_1_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_1.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_1.Brush = base.Context.GetBrush("TutorialHighlightBrush");
			this._widget_1_1.IsVisible = false;
			this._widget_1_1.IsEnabled = false;
			this._widget_1_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_2.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1_2.IsDisabled = true;
			this._widget_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_2.IsDisabled = true;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x0012B89C File Offset: 0x00129A9C
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
				this._widget_1.EventFire -= this.EventListenerOf_widget_1;
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
				this._widget_1_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_0;
				this._widget_1_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_1;
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
					this._widget_1_2.EventFire -= this.EventListenerOf_widget_1_2;
					this._widget_2.EventFire -= this.EventListenerOf_widget_2;
					this._datasource_Root_Hint = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x0012BD1D File Offset: 0x00129F1D
		public void SetDataSource(StringItemWithEnabledAndHintVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x0012BD26 File Offset: 0x00129F26
		private void EventListenerOf_widget_1(Widget widget, string commandName, object[] args)
		{
			if (commandName == "Click")
			{
				this._datasource_Root.ExecuteAction();
			}
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x0012BD40 File Offset: 0x00129F40
		private void EventListenerOf_widget_1_2(Widget widget, string commandName, object[] args)
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

		// Token: 0x060027C1 RID: 10177 RVA: 0x0012BD72 File Offset: 0x00129F72
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

		// Token: 0x060027C2 RID: 10178 RVA: 0x0012BDA4 File Offset: 0x00129FA4
		private void PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x0012BDAD File Offset: 0x00129FAD
		private void boolPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x0012BDB6 File Offset: 0x00129FB6
		private void floatPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x0012BDBF File Offset: 0x00129FBF
		private void Vec2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x0012BDC8 File Offset: 0x00129FC8
		private void Vector2PropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x0012BDD1 File Offset: 0x00129FD1
		private void doublePropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x0012BDDA File Offset: 0x00129FDA
		private void intPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0012BDE3 File Offset: 0x00129FE3
		private void uintPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x0012BDEC File Offset: 0x00129FEC
		private void ColorPropertyChangedListenerOf_widget_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1(propertyName);
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x0012BDF5 File Offset: 0x00129FF5
		private void HandleWidgetPropertyChangeOf_widget_1(string propertyName)
		{
			if (propertyName == "IsEnabled")
			{
				this._datasource_Root.IsEnabled = this._widget_1.IsEnabled;
				return;
			}
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x0012BE1B File Offset: 0x0012A01B
		private void PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x0012BE24 File Offset: 0x0012A024
		private void boolPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0012BE2D File Offset: 0x0012A02D
		private void floatPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x0012BE36 File Offset: 0x0012A036
		private void Vec2PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x0012BE3F File Offset: 0x0012A03F
		private void Vector2PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x0012BE48 File Offset: 0x0012A048
		private void doublePropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x0012BE51 File Offset: 0x0012A051
		private void intPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0012BE5A File Offset: 0x0012A05A
		private void uintPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x0012BE63 File Offset: 0x0012A063
		private void ColorPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x0012BE6C File Offset: 0x0012A06C
		private void HandleWidgetPropertyChangeOf_widget_1_0(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.ActionText = this._widget_1_0.Text;
				return;
			}
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x0012BE92 File Offset: 0x0012A092
		private void PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x0012BE9B File Offset: 0x0012A09B
		private void boolPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x0012BEA4 File Offset: 0x0012A0A4
		private void floatPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0012BEAD File Offset: 0x0012A0AD
		private void Vec2PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x0012BEB6 File Offset: 0x0012A0B6
		private void Vector2PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x0012BEBF File Offset: 0x0012A0BF
		private void doublePropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x0012BEC8 File Offset: 0x0012A0C8
		private void intPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x060027DD RID: 10205 RVA: 0x0012BED1 File Offset: 0x0012A0D1
		private void uintPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x0012BEDA File Offset: 0x0012A0DA
		private void ColorPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x0012BEE3 File Offset: 0x0012A0E3
		private void HandleWidgetPropertyChangeOf_widget_1_1(string propertyName)
		{
			propertyName == "IsHighlightEnabled";
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x0012BEF1 File Offset: 0x0012A0F1
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x0012BEFF File Offset: 0x0012A0FF
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060027E2 RID: 10210 RVA: 0x0012BF0D File Offset: 0x0012A10D
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060027E3 RID: 10211 RVA: 0x0012BF1B File Offset: 0x0012A11B
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060027E4 RID: 10212 RVA: 0x0012BF29 File Offset: 0x0012A129
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x0012BF37 File Offset: 0x0012A137
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x0012BF45 File Offset: 0x0012A145
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0012BF53 File Offset: 0x0012A153
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0012BF61 File Offset: 0x0012A161
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x0012BF70 File Offset: 0x0012A170
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Hint")
			{
				this.RefreshDataSource_datasource_Root_Hint(this._datasource_Root.Hint);
				return;
			}
			if (propertyName == "IsEnabled")
			{
				this._widget_1.IsEnabled = this._datasource_Root.IsEnabled;
				return;
			}
			if (propertyName == "ActionText")
			{
				this._widget_1_0.Text = this._datasource_Root.ActionText;
				return;
			}
			propertyName == "IsHiglightEnabled";
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x0012BFF0 File Offset: 0x0012A1F0
		private void ViewModelPropertyChangedListenerOf_datasource_Root_Hint(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x0012BFFE File Offset: 0x0012A1FE
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x0012C00C File Offset: 0x0012A20C
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x0012C01A File Offset: 0x0012A21A
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x0012C028 File Offset: 0x0012A228
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x0012C036 File Offset: 0x0012A236
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x0012C044 File Offset: 0x0012A244
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x0012C052 File Offset: 0x0012A252
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x0012C060 File Offset: 0x0012A260
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Hint(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Hint(e.PropertyName);
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x0012C06E File Offset: 0x0012A26E
		private void HandleViewModelPropertyChangeOf_datasource_Root_Hint(string propertyName)
		{
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x0012C070 File Offset: 0x0012A270
		private void RefreshDataSource_datasource_Root(StringItemWithEnabledAndHintVM newDataSource)
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
				this._widget_1.EventFire -= this.EventListenerOf_widget_1;
				this._widget_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1;
				this._widget_1_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_0;
				this._widget_1_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_1_1;
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
					this._widget_1_2.EventFire -= this.EventListenerOf_widget_1_2;
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
				this._widget_1.IsEnabled = this._datasource_Root.IsEnabled;
				this._widget_1.EventFire += this.EventListenerOf_widget_1;
				this._widget_1.PropertyChanged += this.PropertyChangedListenerOf_widget_1;
				this._widget_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1;
				this._widget_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1;
				this._widget_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1;
				this._widget_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1;
				this._widget_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1;
				this._widget_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1;
				this._widget_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1;
				this._widget_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1;
				this._widget_1_0.Text = this._datasource_Root.ActionText;
				this._widget_1_0.PropertyChanged += this.PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1_0;
				this._widget_1_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1_0;
				this._widget_1_1.PropertyChanged += this.PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_1_1;
				this._widget_1_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_1_1;
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
					this._widget_1_2.EventFire += this.EventListenerOf_widget_1_2;
					this._widget_2.EventFire += this.EventListenerOf_widget_2;
				}
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x0012C99C File Offset: 0x0012AB9C
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
				this._widget_1_2.EventFire -= this.EventListenerOf_widget_1_2;
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
				this._widget_1_2.EventFire += this.EventListenerOf_widget_1_2;
				this._widget_2.EventFire += this.EventListenerOf_widget_2;
			}
		}

		// Token: 0x040007FB RID: 2043
		private Widget _widget;

		// Token: 0x040007FC RID: 2044
		private NavigationTargetSwitcher _widget_0;

		// Token: 0x040007FD RID: 2045
		private ButtonWidget _widget_1;

		// Token: 0x040007FE RID: 2046
		private TextWidget _widget_1_0;

		// Token: 0x040007FF RID: 2047
		private TutorialHighlightItemBrushWidget _widget_1_1;

		// Token: 0x04000800 RID: 2048
		private HintWidget _widget_1_2;

		// Token: 0x04000801 RID: 2049
		private HintWidget _widget_2;

		// Token: 0x04000802 RID: 2050
		private StringItemWithEnabledAndHintVM _datasource_Root;

		// Token: 0x04000803 RID: 2051
		private HintViewModel _datasource_Root_Hint;
	}
}
