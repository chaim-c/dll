﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.Pages;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Encyclopedia;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x020000C2 RID: 194
	public class EncyclopediaClanPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaClanPageVM_Dependency_9_EncyclopediaDivider__DependendPrefab : EncyclopediaDividerButtonWidget
	{
		// Token: 0x060039BB RID: 14779 RVA: 0x001CCFEC File Offset: 0x001CB1EC
		public EncyclopediaClanPage__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaClanPageVM_Dependency_9_EncyclopediaDivider__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x060039BC RID: 14780 RVA: 0x001CCFF8 File Offset: 0x001CB1F8
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new NavigationScopeTargeter(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new NavigationAutoScrollWidget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_2 = new ListPanel(base.Context);
			this._widget.AddChild(this._widget_2);
			this._widget_2_0 = new BrushWidget(base.Context);
			this._widget_2.AddChild(this._widget_2_0);
			this._widget_2_1 = new TextWidget(base.Context);
			this._widget_2.AddChild(this._widget_2_1);
			this._widget_2_2 = new ImageWidget(base.Context);
			this._widget_2.AddChild(this._widget_2_2);
		}

		// Token: 0x060039BD RID: 14781 RVA: 0x001CD0D8 File Offset: 0x001CB2D8
		public void SetIds()
		{
			this._widget_2.Id = "PlacementListPanel";
			this._widget_2_0.Id = "CollapseIndicator";
			this._widget_2_1.Id = "Text";
		}

		// Token: 0x060039BE RID: 14782 RVA: 0x001CD10C File Offset: 0x001CB30C
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.CollapseIndicator = this._widget_2_0;
			base.ItemListWidget = this._widget.FindChild(new BindingPath("..\\MembersGrid"));
			base.UpdateChildrenStates = true;
			base.DoNotPassEventsToChildren = true;
			base.ExtendCursorAreaLeft = 5f;
			base.ExtendCursorAreaRight = 5f;
			base.ExtendCursorAreaTop = 5f;
			base.ExtendCursorAreaBottom = 5f;
			base.GamepadNavigationIndex = 0;
			this._widget_0.ScopeID = "EncyclopediaDividerScope";
			this._widget_0.ScopeParent = this._widget;
			this._widget_0.NavigateFromScopeEdges = true;
			this._widget_0.UseDiscoveryAreaAsScopeEdges = true;
			this._widget_1.TrackedWidget = this._widget;
			this._widget_1.ScrollYOffset = 35;
			this._widget_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_2.UpdateChildrenStates = true;
			this._widget_2_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_2_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_2_0.SuggestedHeight = 19f;
			this._widget_2_0.SuggestedWidth = 19f;
			this._widget_2_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_2_0.PositionYOffset = -3f;
			this._widget_2_0.Brush = base.Context.GetBrush("SPOptions.GameKeysgroup.ExpandIndicator");
			this._widget_2_1.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_2_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_2_1.Brush = base.Context.GetBrush("Encyclopedia.SubPage.Header.Text");
			this._widget_2_1.MarginLeft = 10f;
			this._widget_2_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2_2.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_2_2.SuggestedHeight = 2f;
			this._widget_2_2.VerticalAlignment = VerticalAlignment.Center;
			this._widget_2_2.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_2_2.MarginLeft = 15f;
			this._widget_2_2.Brush = base.Context.GetBrush("SPOptions.CollapserLine");
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x001CD328 File Offset: 0x001CB528
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
				this._widget_2_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_2_1;
				this._datasource_Root = null;
			}
		}

		// Token: 0x060039C0 RID: 14784 RVA: 0x001CD4E5 File Offset: 0x001CB6E5
		public void SetDataSource(EncyclopediaClanPageVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x001CD4EE File Offset: 0x001CB6EE
		private void PropertyChangedListenerOf_widget_2_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_1(propertyName);
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x001CD4F7 File Offset: 0x001CB6F7
		private void boolPropertyChangedListenerOf_widget_2_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_1(propertyName);
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x001CD500 File Offset: 0x001CB700
		private void floatPropertyChangedListenerOf_widget_2_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_1(propertyName);
		}

		// Token: 0x060039C4 RID: 14788 RVA: 0x001CD509 File Offset: 0x001CB709
		private void Vec2PropertyChangedListenerOf_widget_2_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_1(propertyName);
		}

		// Token: 0x060039C5 RID: 14789 RVA: 0x001CD512 File Offset: 0x001CB712
		private void Vector2PropertyChangedListenerOf_widget_2_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_1(propertyName);
		}

		// Token: 0x060039C6 RID: 14790 RVA: 0x001CD51B File Offset: 0x001CB71B
		private void doublePropertyChangedListenerOf_widget_2_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_1(propertyName);
		}

		// Token: 0x060039C7 RID: 14791 RVA: 0x001CD524 File Offset: 0x001CB724
		private void intPropertyChangedListenerOf_widget_2_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_1(propertyName);
		}

		// Token: 0x060039C8 RID: 14792 RVA: 0x001CD52D File Offset: 0x001CB72D
		private void uintPropertyChangedListenerOf_widget_2_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_1(propertyName);
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x001CD536 File Offset: 0x001CB736
		private void ColorPropertyChangedListenerOf_widget_2_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_1(propertyName);
		}

		// Token: 0x060039CA RID: 14794 RVA: 0x001CD53F File Offset: 0x001CB73F
		private void HandleWidgetPropertyChangeOf_widget_2_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.MembersText = this._widget_2_1.Text;
				return;
			}
		}

		// Token: 0x060039CB RID: 14795 RVA: 0x001CD565 File Offset: 0x001CB765
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060039CC RID: 14796 RVA: 0x001CD573 File Offset: 0x001CB773
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x001CD581 File Offset: 0x001CB781
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x001CD58F File Offset: 0x001CB78F
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x001CD59D File Offset: 0x001CB79D
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060039D0 RID: 14800 RVA: 0x001CD5AB File Offset: 0x001CB7AB
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x001CD5B9 File Offset: 0x001CB7B9
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x001CD5C7 File Offset: 0x001CB7C7
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x001CD5D5 File Offset: 0x001CB7D5
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x001CD5E3 File Offset: 0x001CB7E3
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "MembersText")
			{
				this._widget_2_1.Text = this._datasource_Root.MembersText;
				return;
			}
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x001CD60C File Offset: 0x001CB80C
		private void RefreshDataSource_datasource_Root(EncyclopediaClanPageVM newDataSource)
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
				this._widget_2_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_2_1;
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
				this._widget_2_1.Text = this._datasource_Root.MembersText;
				this._widget_2_1.PropertyChanged += this.PropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_2_1;
				this._widget_2_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_2_1;
			}
		}

		// Token: 0x04000BE0 RID: 3040
		private EncyclopediaDividerButtonWidget _widget;

		// Token: 0x04000BE1 RID: 3041
		private NavigationScopeTargeter _widget_0;

		// Token: 0x04000BE2 RID: 3042
		private NavigationAutoScrollWidget _widget_1;

		// Token: 0x04000BE3 RID: 3043
		private ListPanel _widget_2;

		// Token: 0x04000BE4 RID: 3044
		private BrushWidget _widget_2_0;

		// Token: 0x04000BE5 RID: 3045
		private TextWidget _widget_2_1;

		// Token: 0x04000BE6 RID: 3046
		private ImageWidget _widget_2_2;

		// Token: 0x04000BE7 RID: 3047
		private EncyclopediaClanPageVM _datasource_Root;
	}
}
