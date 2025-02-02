﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Items;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x02000101 RID: 257
	public class EncyclopediaSettlementPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaSettlementPageVM_Dependency_11_EncyclopediaSubPageHistoryElement__InheritedPrefab : Widget
	{
		// Token: 0x06004631 RID: 17969 RVA: 0x00226050 File Offset: 0x00224250
		public EncyclopediaSettlementPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaSettlementPageVM_Dependency_11_EncyclopediaSubPageHistoryElement__InheritedPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x0022605C File Offset: 0x0022425C
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

		// Token: 0x06004633 RID: 17971 RVA: 0x002260F8 File Offset: 0x002242F8
		public virtual void SetIds()
		{
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x002260FC File Offset: 0x002242FC
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

		// Token: 0x06004635 RID: 17973 RVA: 0x00226258 File Offset: 0x00224458
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

		// Token: 0x06004636 RID: 17974 RVA: 0x00226512 File Offset: 0x00224712
		public virtual void SetDataSource(EncyclopediaHistoryEventVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x0022651C File Offset: 0x0022471C
		private void EventListenerOf_widget_1_0(Widget widget, string commandName, object[] args)
		{
			if (commandName == "LinkClick")
			{
				string link = (string)args[0];
				this._datasource_Root.ExecuteLink(link);
			}
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x0022654C File Offset: 0x0022474C
		private void EventListenerOf_widget_1_1(Widget widget, string commandName, object[] args)
		{
			if (commandName == "LinkClick")
			{
				string link = (string)args[0];
				this._datasource_Root.ExecuteLink(link);
			}
		}

		// Token: 0x06004639 RID: 17977 RVA: 0x0022657B File Offset: 0x0022477B
		private void PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x0600463A RID: 17978 RVA: 0x00226584 File Offset: 0x00224784
		private void boolPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x0600463B RID: 17979 RVA: 0x0022658D File Offset: 0x0022478D
		private void floatPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x0600463C RID: 17980 RVA: 0x00226596 File Offset: 0x00224796
		private void Vec2PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x0600463D RID: 17981 RVA: 0x0022659F File Offset: 0x0022479F
		private void Vector2PropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x0600463E RID: 17982 RVA: 0x002265A8 File Offset: 0x002247A8
		private void doublePropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x002265B1 File Offset: 0x002247B1
		private void intPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x002265BA File Offset: 0x002247BA
		private void uintPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x002265C3 File Offset: 0x002247C3
		private void ColorPropertyChangedListenerOf_widget_1_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_0(propertyName);
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x002265CC File Offset: 0x002247CC
		private void HandleWidgetPropertyChangeOf_widget_1_0(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.HistoryEventText = this._widget_1_0.Text;
				return;
			}
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x002265F2 File Offset: 0x002247F2
		private void PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x002265FB File Offset: 0x002247FB
		private void boolPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x00226604 File Offset: 0x00224804
		private void floatPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x0022660D File Offset: 0x0022480D
		private void Vec2PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x00226616 File Offset: 0x00224816
		private void Vector2PropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06004648 RID: 17992 RVA: 0x0022661F File Offset: 0x0022481F
		private void doublePropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x00226628 File Offset: 0x00224828
		private void intPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x00226631 File Offset: 0x00224831
		private void uintPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x0022663A File Offset: 0x0022483A
		private void ColorPropertyChangedListenerOf_widget_1_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_1_1(propertyName);
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x00226643 File Offset: 0x00224843
		private void HandleWidgetPropertyChangeOf_widget_1_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.HistoryEventTimeText = this._widget_1_1.Text;
				return;
			}
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x00226669 File Offset: 0x00224869
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x00226677 File Offset: 0x00224877
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600464F RID: 17999 RVA: 0x00226685 File Offset: 0x00224885
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004650 RID: 18000 RVA: 0x00226693 File Offset: 0x00224893
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004651 RID: 18001 RVA: 0x002266A1 File Offset: 0x002248A1
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004652 RID: 18002 RVA: 0x002266AF File Offset: 0x002248AF
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x002266BD File Offset: 0x002248BD
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004654 RID: 18004 RVA: 0x002266CB File Offset: 0x002248CB
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x002266D9 File Offset: 0x002248D9
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x002266E8 File Offset: 0x002248E8
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

		// Token: 0x06004657 RID: 18007 RVA: 0x00226740 File Offset: 0x00224940
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

		// Token: 0x04000EA8 RID: 3752
		private Widget _widget;

		// Token: 0x04000EA9 RID: 3753
		private Widget _widget_0;

		// Token: 0x04000EAA RID: 3754
		private ListPanel _widget_1;

		// Token: 0x04000EAB RID: 3755
		private RichTextWidget _widget_1_0;

		// Token: 0x04000EAC RID: 3756
		private RichTextWidget _widget_1_1;

		// Token: 0x04000EAD RID: 3757
		private EncyclopediaHistoryEventVM _datasource_Root;
	}
}
