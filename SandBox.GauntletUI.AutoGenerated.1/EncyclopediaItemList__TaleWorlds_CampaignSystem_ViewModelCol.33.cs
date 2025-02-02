﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.ExtraWidgets;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x020000E9 RID: 233
	public class EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaSettlementPageVM_Dependency_5_EncyclopediaItemListItem__InheritedPrefab : EncyclopediaListItemButtonWidget
	{
		// Token: 0x060040A8 RID: 16552 RVA: 0x002002E3 File Offset: 0x001FE4E3
		public EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaSettlementPageVM_Dependency_5_EncyclopediaItemListItem__InheritedPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x002002EC File Offset: 0x001FE4EC
		public virtual void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ListPanel(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_0_1 = new Widget(base.Context);
			this._widget_0.AddChild(this._widget_0_1);
			this._widget_0_2 = new ScrollingTextWidget(base.Context);
			this._widget_0.AddChild(this._widget_0_2);
			this._widget_0_3 = new TextWidget(base.Context);
			this._widget_0.AddChild(this._widget_0_3);
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x002003AA File Offset: 0x001FE5AA
		public virtual void SetIds()
		{
			this._widget_0.Id = "ItemParentListPanel";
			this._widget_0_2.Id = "ListItemNameTextWidget";
			this._widget_0_3.Id = "ListComparedValueTextWidget";
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x002003DC File Offset: 0x001FE5DC
		public virtual void SetAttributes()
		{
			base.DoNotPassEventsToChildren = true;
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.SuggestedHeight = 37f;
			base.Brush = base.Context.GetBrush("Encyclopedia.ListButton");
			base.InfoAvailableItemNameBrush = base.Context.GetBrush("EncyclopediaList.Filter.Name.Text");
			base.InfoUnvailableItemNameBrush = base.Context.GetBrush("EncyclopediaList.Item.InfoUnavailable.Name.Text");
			base.ListItemNameTextWidget = this._widget_0_2;
			base.ListComparedValueTextWidget = this._widget_0_3;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0.StackLayout.LayoutMethod = LayoutMethod.HorizontalLeftToRight;
			this._widget_0_0.DoNotPassEventsToChildren = true;
			this._widget_0_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_0.SuggestedWidth = 35f;
			this._widget_0_0.SuggestedHeight = 35f;
			this._widget_0_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_0.Sprite = base.Context.SpriteData.GetSprite("Encyclopedia\\star_without_glow");
			this._widget_0_0.Color = new Color(0.9960785f, 0.7568628f, 0.3411765f, 1f);
			this._widget_0_1.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_1.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0_1.SuggestedWidth = 35f;
			this._widget_0_1.SuggestedHeight = 35f;
			this._widget_0_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0_1.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0_2.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0_2.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_2.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0_2.SuggestedWidth = 500f;
			this._widget_0_2.MarginLeft = 5f;
			this._widget_0_3.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0_3.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_3.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0_3.MarginLeft = 30f;
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x002005E8 File Offset: 0x001FE7E8
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
				this._widget_0_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_0;
				this._widget_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_1;
				this._widget_0_2.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_2;
				this._widget_0_3.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_3;
				this._datasource_Root = null;
			}
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x00200AF8 File Offset: 0x001FECF8
		public virtual void SetDataSource(EncyclopediaListItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x00200B04 File Offset: 0x001FED04
		private void EventListenerOf_widget(Widget widget, string commandName, object[] args)
		{
			if (commandName == "Click")
			{
				this._datasource_Root.Execute();
			}
			if (commandName == "HoverBegin")
			{
				this._datasource_Root.ExecuteBeginTooltip();
			}
			if (commandName == "HoverEnd")
			{
				this._datasource_Root.ExecuteEndTooltip();
			}
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x00200B59 File Offset: 0x001FED59
		private void PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x00200B62 File Offset: 0x001FED62
		private void boolPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x00200B6B File Offset: 0x001FED6B
		private void floatPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060040B2 RID: 16562 RVA: 0x00200B74 File Offset: 0x001FED74
		private void Vec2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060040B3 RID: 16563 RVA: 0x00200B7D File Offset: 0x001FED7D
		private void Vector2PropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060040B4 RID: 16564 RVA: 0x00200B86 File Offset: 0x001FED86
		private void doublePropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x00200B8F File Offset: 0x001FED8F
		private void intPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x00200B98 File Offset: 0x001FED98
		private void uintPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x00200BA1 File Offset: 0x001FEDA1
		private void ColorPropertyChangedListenerOf_widget(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget(propertyName);
		}

		// Token: 0x060040B8 RID: 16568 RVA: 0x00200BAC File Offset: 0x001FEDAC
		private void HandleWidgetPropertyChangeOf_widget(string propertyName)
		{
			if (propertyName == "IsHidden")
			{
				this._datasource_Root.IsFiltered = this._widget.IsHidden;
				return;
			}
			if (propertyName == "ListItemId")
			{
				this._datasource_Root.Id = this._widget.ListItemId;
				return;
			}
			if (propertyName == "IsInfoAvailable")
			{
				this._datasource_Root.PlayerCanSeeValues = this._widget.IsInfoAvailable;
				return;
			}
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x00200C25 File Offset: 0x001FEE25
		private void PropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x00200C2E File Offset: 0x001FEE2E
		private void boolPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x00200C37 File Offset: 0x001FEE37
		private void floatPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060040BC RID: 16572 RVA: 0x00200C40 File Offset: 0x001FEE40
		private void Vec2PropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060040BD RID: 16573 RVA: 0x00200C49 File Offset: 0x001FEE49
		private void Vector2PropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060040BE RID: 16574 RVA: 0x00200C52 File Offset: 0x001FEE52
		private void doublePropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060040BF RID: 16575 RVA: 0x00200C5B File Offset: 0x001FEE5B
		private void intPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060040C0 RID: 16576 RVA: 0x00200C64 File Offset: 0x001FEE64
		private void uintPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x00200C6D File Offset: 0x001FEE6D
		private void ColorPropertyChangedListenerOf_widget_0_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_0(propertyName);
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x00200C76 File Offset: 0x001FEE76
		private void HandleWidgetPropertyChangeOf_widget_0_0(string propertyName)
		{
			if (propertyName == "IsVisible")
			{
				this._datasource_Root.IsBookmarked = this._widget_0_0.IsVisible;
				return;
			}
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x00200C9C File Offset: 0x001FEE9C
		private void PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060040C4 RID: 16580 RVA: 0x00200CA5 File Offset: 0x001FEEA5
		private void boolPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060040C5 RID: 16581 RVA: 0x00200CAE File Offset: 0x001FEEAE
		private void floatPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060040C6 RID: 16582 RVA: 0x00200CB7 File Offset: 0x001FEEB7
		private void Vec2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060040C7 RID: 16583 RVA: 0x00200CC0 File Offset: 0x001FEEC0
		private void Vector2PropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060040C8 RID: 16584 RVA: 0x00200CC9 File Offset: 0x001FEEC9
		private void doublePropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060040C9 RID: 16585 RVA: 0x00200CD2 File Offset: 0x001FEED2
		private void intPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060040CA RID: 16586 RVA: 0x00200CDB File Offset: 0x001FEEDB
		private void uintPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060040CB RID: 16587 RVA: 0x00200CE4 File Offset: 0x001FEEE4
		private void ColorPropertyChangedListenerOf_widget_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_1(propertyName);
		}

		// Token: 0x060040CC RID: 16588 RVA: 0x00200CED File Offset: 0x001FEEED
		private void HandleWidgetPropertyChangeOf_widget_0_1(string propertyName)
		{
			if (propertyName == "IsHidden")
			{
				this._datasource_Root.IsBookmarked = this._widget_0_1.IsHidden;
				return;
			}
		}

		// Token: 0x060040CD RID: 16589 RVA: 0x00200D13 File Offset: 0x001FEF13
		private void PropertyChangedListenerOf_widget_0_2(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_2(propertyName);
		}

		// Token: 0x060040CE RID: 16590 RVA: 0x00200D1C File Offset: 0x001FEF1C
		private void boolPropertyChangedListenerOf_widget_0_2(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_2(propertyName);
		}

		// Token: 0x060040CF RID: 16591 RVA: 0x00200D25 File Offset: 0x001FEF25
		private void floatPropertyChangedListenerOf_widget_0_2(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_2(propertyName);
		}

		// Token: 0x060040D0 RID: 16592 RVA: 0x00200D2E File Offset: 0x001FEF2E
		private void Vec2PropertyChangedListenerOf_widget_0_2(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_2(propertyName);
		}

		// Token: 0x060040D1 RID: 16593 RVA: 0x00200D37 File Offset: 0x001FEF37
		private void Vector2PropertyChangedListenerOf_widget_0_2(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_2(propertyName);
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x00200D40 File Offset: 0x001FEF40
		private void doublePropertyChangedListenerOf_widget_0_2(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_2(propertyName);
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x00200D49 File Offset: 0x001FEF49
		private void intPropertyChangedListenerOf_widget_0_2(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_2(propertyName);
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x00200D52 File Offset: 0x001FEF52
		private void uintPropertyChangedListenerOf_widget_0_2(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_2(propertyName);
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x00200D5B File Offset: 0x001FEF5B
		private void ColorPropertyChangedListenerOf_widget_0_2(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_2(propertyName);
		}

		// Token: 0x060040D6 RID: 16598 RVA: 0x00200D64 File Offset: 0x001FEF64
		private void HandleWidgetPropertyChangeOf_widget_0_2(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.Name = this._widget_0_2.Text;
				return;
			}
		}

		// Token: 0x060040D7 RID: 16599 RVA: 0x00200D8A File Offset: 0x001FEF8A
		private void PropertyChangedListenerOf_widget_0_3(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_3(propertyName);
		}

		// Token: 0x060040D8 RID: 16600 RVA: 0x00200D93 File Offset: 0x001FEF93
		private void boolPropertyChangedListenerOf_widget_0_3(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_3(propertyName);
		}

		// Token: 0x060040D9 RID: 16601 RVA: 0x00200D9C File Offset: 0x001FEF9C
		private void floatPropertyChangedListenerOf_widget_0_3(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_3(propertyName);
		}

		// Token: 0x060040DA RID: 16602 RVA: 0x00200DA5 File Offset: 0x001FEFA5
		private void Vec2PropertyChangedListenerOf_widget_0_3(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_3(propertyName);
		}

		// Token: 0x060040DB RID: 16603 RVA: 0x00200DAE File Offset: 0x001FEFAE
		private void Vector2PropertyChangedListenerOf_widget_0_3(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_3(propertyName);
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x00200DB7 File Offset: 0x001FEFB7
		private void doublePropertyChangedListenerOf_widget_0_3(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_3(propertyName);
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x00200DC0 File Offset: 0x001FEFC0
		private void intPropertyChangedListenerOf_widget_0_3(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_3(propertyName);
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x00200DC9 File Offset: 0x001FEFC9
		private void uintPropertyChangedListenerOf_widget_0_3(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_3(propertyName);
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00200DD2 File Offset: 0x001FEFD2
		private void ColorPropertyChangedListenerOf_widget_0_3(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0_3(propertyName);
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x00200DDB File Offset: 0x001FEFDB
		private void HandleWidgetPropertyChangeOf_widget_0_3(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.ComparedValue = this._widget_0_3.Text;
				return;
			}
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x00200E01 File Offset: 0x001FF001
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060040E2 RID: 16610 RVA: 0x00200E0F File Offset: 0x001FF00F
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060040E3 RID: 16611 RVA: 0x00200E1D File Offset: 0x001FF01D
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x00200E2B File Offset: 0x001FF02B
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060040E5 RID: 16613 RVA: 0x00200E39 File Offset: 0x001FF039
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x00200E47 File Offset: 0x001FF047
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060040E7 RID: 16615 RVA: 0x00200E55 File Offset: 0x001FF055
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x00200E63 File Offset: 0x001FF063
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x00200E71 File Offset: 0x001FF071
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x00200E80 File Offset: 0x001FF080
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "IsFiltered")
			{
				this._widget.IsHidden = this._datasource_Root.IsFiltered;
				return;
			}
			if (propertyName == "Id")
			{
				this._widget.ListItemId = this._datasource_Root.Id;
				return;
			}
			if (propertyName == "PlayerCanSeeValues")
			{
				this._widget.IsInfoAvailable = this._datasource_Root.PlayerCanSeeValues;
				return;
			}
			if (propertyName == "IsBookmarked")
			{
				this._widget_0_0.IsVisible = this._datasource_Root.IsBookmarked;
				this._widget_0_1.IsHidden = this._datasource_Root.IsBookmarked;
				return;
			}
			if (propertyName == "Name")
			{
				this._widget_0_2.Text = this._datasource_Root.Name;
				return;
			}
			if (propertyName == "ComparedValue")
			{
				this._widget_0_3.Text = this._datasource_Root.ComparedValue;
				return;
			}
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x00200F7C File Offset: 0x001FF17C
		private void RefreshDataSource_datasource_Root(EncyclopediaListItemVM newDataSource)
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
				this._widget_0_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_0;
				this._widget_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_1;
				this._widget_0_2.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_2;
				this._widget_0_3.PropertyChanged -= this.PropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0_3;
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
				this._widget.IsHidden = this._datasource_Root.IsFiltered;
				this._widget.ListItemId = this._datasource_Root.Id;
				this._widget.IsInfoAvailable = this._datasource_Root.PlayerCanSeeValues;
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
				this._widget_0_0.IsVisible = this._datasource_Root.IsBookmarked;
				this._widget_0_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0_0;
				this._widget_0_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0_0;
				this._widget_0_1.IsHidden = this._datasource_Root.IsBookmarked;
				this._widget_0_1.PropertyChanged += this.PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0_1;
				this._widget_0_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0_1;
				this._widget_0_2.Text = this._datasource_Root.Name;
				this._widget_0_2.PropertyChanged += this.PropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0_2;
				this._widget_0_2.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0_2;
				this._widget_0_3.Text = this._datasource_Root.ComparedValue;
				this._widget_0_3.PropertyChanged += this.PropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0_3;
				this._widget_0_3.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0_3;
			}
		}

		// Token: 0x04000D8C RID: 3468
		private EncyclopediaListItemButtonWidget _widget;

		// Token: 0x04000D8D RID: 3469
		private ListPanel _widget_0;

		// Token: 0x04000D8E RID: 3470
		private Widget _widget_0_0;

		// Token: 0x04000D8F RID: 3471
		private Widget _widget_0_1;

		// Token: 0x04000D90 RID: 3472
		private ScrollingTextWidget _widget_0_2;

		// Token: 0x04000D91 RID: 3473
		private TextWidget _widget_0_3;

		// Token: 0x04000D92 RID: 3474
		private EncyclopediaListItemVM _datasource_Root;
	}
}
