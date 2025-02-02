﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets.Party;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x020000F1 RID: 241
	public class EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_4_EncyclopediaFilterGroup__InheritedPrefab : Widget
	{
		// Token: 0x06004214 RID: 16916 RVA: 0x0020AA46 File Offset: 0x00208C46
		public EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_4_EncyclopediaFilterGroup__InheritedPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x0020AA50 File Offset: 0x00208C50
		public virtual void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new NavigationScopeTargeter(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new NavigationAutoScrollWidget(base.Context);
			this._widget.AddChild(this._widget_1);
			this._widget_2 = new PartyHeaderToggleWidget(base.Context);
			this._widget.AddChild(this._widget_2);
			this._widget_2_0 = new ListPanel(base.Context);
			this._widget_2.AddChild(this._widget_2_0);
			this._widget_2_0_0 = new ImageWidget(base.Context);
			this._widget_2_0.AddChild(this._widget_2_0_0);
			this._widget_2_0_1 = new TextWidget(base.Context);
			this._widget_2_0.AddChild(this._widget_2_0_1);
			this._widget_2_0_2 = new ImageWidget(base.Context);
			this._widget_2_0.AddChild(this._widget_2_0_2);
			this._widget_3 = new NavigatableListPanel(base.Context);
			this._widget.AddChild(this._widget_3);
		}

		// Token: 0x06004216 RID: 16918 RVA: 0x0020AB74 File Offset: 0x00208D74
		public virtual void SetIds()
		{
			this._widget_2.Id = "HeaderToggle";
			this._widget_2_0.Id = "Description";
			this._widget_2_0_0.Id = "CollapseIndicator";
			this._widget_3.Id = "Filters";
		}

		// Token: 0x06004217 RID: 16919 RVA: 0x0020ABC4 File Offset: 0x00208DC4
		public virtual void SetAttributes()
		{
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.DoNotAcceptEvents = true;
			this._widget_0.ScopeID = "EncyclopediaFilterGroupScope";
			this._widget_0.ScopeParent = this._widget;
			this._widget_0.ScopeMovements = GamepadNavigationTypes.Vertical;
			this._widget_1.TrackedWidget = this._widget_2;
			this._widget_2.DoNotPassEventsToChildren = true;
			this._widget_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_2.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_2.VerticalAlignment = VerticalAlignment.Top;
			this._widget_2.CollapseIndicator = this._widget_2_0_0;
			this._widget_2.ListPanel = this._widget_3;
			this._widget_2.RenderLate = true;
			this._widget_2.WidgetToClose = this._widget_3;
			this._widget_2.UpdateChildrenStates = true;
			this._widget_2.GamepadNavigationIndex = 0;
			this._widget_2_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2_0.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_2_0.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_2_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_2_0.DoNotAcceptEvents = true;
			this._widget_2_0.UpdateChildrenStates = true;
			this._widget_2_0.StackLayout.LayoutMethod = LayoutMethod.HorizontalLeftToRight;
			this._widget_2_0_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_2_0_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_2_0_0.SuggestedHeight = 19f;
			this._widget_2_0_0.SuggestedWidth = 19f;
			this._widget_2_0_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_2_0_0.PositionXOffset = -10f;
			this._widget_2_0_0.PositionYOffset = -3f;
			this._widget_2_0_0.Brush = base.Context.GetBrush("SPOptions.GameKeysgroup.ExpandIndicator");
			this._widget_2_0_0.OverrideDefaultStateSwitchingEnabled = true;
			this._widget_2_0_1.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_2_0_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_2_0_1.Brush = base.Context.GetBrush("SPOptions.GameKeysGroup.Title.Text");
			this._widget_2_0_1.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_2_0_2.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_2_0_2.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_2_0_2.SuggestedHeight = 2f;
			this._widget_2_0_2.VerticalAlignment = VerticalAlignment.Center;
			this._widget_2_0_2.MarginLeft = 5f;
			this._widget_2_0_2.Brush = base.Context.GetBrush("SPOptions.CollapserLine");
			this._widget_3.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_3.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_3.StackLayout.LayoutMethod = LayoutMethod.VerticalTopToBottom;
			this._widget_3.MarginTop = 40f;
			this._widget_3.DoNotAcceptEvents = true;
			this._widget_3.MarginLeft = 5f;
			this._widget_3.MaxIndex = 10000;
		}

		// Token: 0x06004218 RID: 16920 RVA: 0x0020AEA0 File Offset: 0x002090A0
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
				this._widget_2_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_2_0_1;
				if (this._datasource_Root_Filters != null)
				{
					this._datasource_Root_Filters.ListChanged -= this.OnList_datasource_Root_FiltersChanged;
					for (int i = this._widget_3.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_3.GetChild(i);
						((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)child).OnBeforeRemovedChild(child);
						((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)this._widget_3.GetChild(i)).DestroyDataSource();
					}
					this._datasource_Root_Filters = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x0020B0CA File Offset: 0x002092CA
		public virtual void SetDataSource(EncyclopediaFilterGroupVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x0600421A RID: 16922 RVA: 0x0020B0D3 File Offset: 0x002092D3
		private void PropertyChangedListenerOf_widget_2_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_0_1(propertyName);
		}

		// Token: 0x0600421B RID: 16923 RVA: 0x0020B0DC File Offset: 0x002092DC
		private void boolPropertyChangedListenerOf_widget_2_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_0_1(propertyName);
		}

		// Token: 0x0600421C RID: 16924 RVA: 0x0020B0E5 File Offset: 0x002092E5
		private void floatPropertyChangedListenerOf_widget_2_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_0_1(propertyName);
		}

		// Token: 0x0600421D RID: 16925 RVA: 0x0020B0EE File Offset: 0x002092EE
		private void Vec2PropertyChangedListenerOf_widget_2_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_0_1(propertyName);
		}

		// Token: 0x0600421E RID: 16926 RVA: 0x0020B0F7 File Offset: 0x002092F7
		private void Vector2PropertyChangedListenerOf_widget_2_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_0_1(propertyName);
		}

		// Token: 0x0600421F RID: 16927 RVA: 0x0020B100 File Offset: 0x00209300
		private void doublePropertyChangedListenerOf_widget_2_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_0_1(propertyName);
		}

		// Token: 0x06004220 RID: 16928 RVA: 0x0020B109 File Offset: 0x00209309
		private void intPropertyChangedListenerOf_widget_2_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_0_1(propertyName);
		}

		// Token: 0x06004221 RID: 16929 RVA: 0x0020B112 File Offset: 0x00209312
		private void uintPropertyChangedListenerOf_widget_2_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_0_1(propertyName);
		}

		// Token: 0x06004222 RID: 16930 RVA: 0x0020B11B File Offset: 0x0020931B
		private void ColorPropertyChangedListenerOf_widget_2_0_1(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_2_0_1(propertyName);
		}

		// Token: 0x06004223 RID: 16931 RVA: 0x0020B124 File Offset: 0x00209324
		private void HandleWidgetPropertyChangeOf_widget_2_0_1(string propertyName)
		{
			if (propertyName == "Text")
			{
				this._datasource_Root.FilterName = this._widget_2_0_1.Text;
				return;
			}
		}

		// Token: 0x06004224 RID: 16932 RVA: 0x0020B14A File Offset: 0x0020934A
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004225 RID: 16933 RVA: 0x0020B158 File Offset: 0x00209358
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004226 RID: 16934 RVA: 0x0020B166 File Offset: 0x00209366
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004227 RID: 16935 RVA: 0x0020B174 File Offset: 0x00209374
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x0020B182 File Offset: 0x00209382
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x0020B190 File Offset: 0x00209390
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600422A RID: 16938 RVA: 0x0020B19E File Offset: 0x0020939E
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600422B RID: 16939 RVA: 0x0020B1AC File Offset: 0x002093AC
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600422C RID: 16940 RVA: 0x0020B1BA File Offset: 0x002093BA
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600422D RID: 16941 RVA: 0x0020B1C8 File Offset: 0x002093C8
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Filters")
			{
				this.RefreshDataSource_datasource_Root_Filters(this._datasource_Root.Filters);
				return;
			}
			if (propertyName == "FilterName")
			{
				this._widget_2_0_1.Text = this._datasource_Root.FilterName;
				return;
			}
		}

		// Token: 0x0600422E RID: 16942 RVA: 0x0020B218 File Offset: 0x00209418
		public void OnList_datasource_Root_FiltersChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				for (int i = this._widget_3.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_3.GetChild(i);
					((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_3.GetChild(i);
					((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)child2).SetDataSource(null);
					this._widget_3.RemoveChild(child2);
				}
				return;
			case ListChangedType.Sorted:
				for (int j = 0; j < this._datasource_Root_Filters.Count; j++)
				{
					EncyclopediaListFilterVM bindingObject = this._datasource_Root_Filters[j];
					this._widget_3.FindChild((Widget widget) => widget.GetComponent<GeneratedWidgetData>().Data == bindingObject).SetSiblingIndex(j, false);
				}
				return;
			case ListChangedType.ItemAdded:
			{
				EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate = new EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate(base.Context);
				GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate);
				EncyclopediaListFilterVM encyclopediaListFilterVM = this._datasource_Root_Filters[e.NewIndex];
				generatedWidgetData.Data = encyclopediaListFilterVM;
				encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.AddComponent(generatedWidgetData);
				this._widget_3.AddChildAtIndex(encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate, e.NewIndex);
				encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.CreateWidgets();
				encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.SetIds();
				encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.SetAttributes();
				encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.SetDataSource(encyclopediaListFilterVM);
				return;
			}
			case ListChangedType.ItemBeforeDeleted:
			{
				Widget child3 = this._widget_3.GetChild(e.NewIndex);
				((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)child3).OnBeforeRemovedChild(child3);
				return;
			}
			case ListChangedType.ItemDeleted:
			{
				Widget child4 = this._widget_3.GetChild(e.NewIndex);
				((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)child4).SetDataSource(null);
				this._widget_3.RemoveChild(child4);
				break;
			}
			case ListChangedType.ItemChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x0600422F RID: 16943 RVA: 0x0020B3B4 File Offset: 0x002095B4
		private void RefreshDataSource_datasource_Root(EncyclopediaFilterGroupVM newDataSource)
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
				this._widget_2_0_1.PropertyChanged -= this.PropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_2_0_1;
				if (this._datasource_Root_Filters != null)
				{
					this._datasource_Root_Filters.ListChanged -= this.OnList_datasource_Root_FiltersChanged;
					for (int i = this._widget_3.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_3.GetChild(i);
						((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)child).OnBeforeRemovedChild(child);
						Widget child2 = this._widget_3.GetChild(i);
						((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)child2).SetDataSource(null);
						this._widget_3.RemoveChild(child2);
					}
					this._datasource_Root_Filters = null;
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
				this._widget_2_0_1.Text = this._datasource_Root.FilterName;
				this._widget_2_0_1.PropertyChanged += this.PropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.intPropertyChanged += this.intPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_2_0_1;
				this._widget_2_0_1.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_2_0_1;
				this._datasource_Root_Filters = this._datasource_Root.Filters;
				if (this._datasource_Root_Filters != null)
				{
					this._datasource_Root_Filters.ListChanged += this.OnList_datasource_Root_FiltersChanged;
					for (int j = 0; j < this._datasource_Root_Filters.Count; j++)
					{
						EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate = new EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate(base.Context);
						GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate);
						EncyclopediaListFilterVM encyclopediaListFilterVM = this._datasource_Root_Filters[j];
						generatedWidgetData.Data = encyclopediaListFilterVM;
						encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.AddComponent(generatedWidgetData);
						this._widget_3.AddChildAtIndex(encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate, j);
						encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.CreateWidgets();
						encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.SetIds();
						encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.SetAttributes();
						encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.SetDataSource(encyclopediaListFilterVM);
					}
				}
			}
		}

		// Token: 0x06004230 RID: 16944 RVA: 0x0020B860 File Offset: 0x00209A60
		private void RefreshDataSource_datasource_Root_Filters(MBBindingList<EncyclopediaListFilterVM> newDataSource)
		{
			if (this._datasource_Root_Filters != null)
			{
				this._datasource_Root_Filters.ListChanged -= this.OnList_datasource_Root_FiltersChanged;
				for (int i = this._widget_3.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_3.GetChild(i);
					((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_3.GetChild(i);
					((EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate)child2).SetDataSource(null);
					this._widget_3.RemoveChild(child2);
				}
				this._datasource_Root_Filters = null;
			}
			this._datasource_Root_Filters = newDataSource;
			this._datasource_Root_Filters = this._datasource_Root.Filters;
			if (this._datasource_Root_Filters != null)
			{
				this._datasource_Root_Filters.ListChanged += this.OnList_datasource_Root_FiltersChanged;
				for (int j = 0; j < this._datasource_Root_Filters.Count; j++)
				{
					EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate = new EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate(base.Context);
					GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate);
					EncyclopediaListFilterVM encyclopediaListFilterVM = this._datasource_Root_Filters[j];
					generatedWidgetData.Data = encyclopediaListFilterVM;
					encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.AddComponent(generatedWidgetData);
					this._widget_3.AddChildAtIndex(encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate, j);
					encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.CreateWidgets();
					encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.SetIds();
					encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.SetAttributes();
					encyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_7_ItemTemplate.SetDataSource(encyclopediaListFilterVM);
				}
			}
		}

		// Token: 0x04000DE0 RID: 3552
		private Widget _widget;

		// Token: 0x04000DE1 RID: 3553
		private NavigationScopeTargeter _widget_0;

		// Token: 0x04000DE2 RID: 3554
		private NavigationAutoScrollWidget _widget_1;

		// Token: 0x04000DE3 RID: 3555
		private PartyHeaderToggleWidget _widget_2;

		// Token: 0x04000DE4 RID: 3556
		private ListPanel _widget_2_0;

		// Token: 0x04000DE5 RID: 3557
		private ImageWidget _widget_2_0_0;

		// Token: 0x04000DE6 RID: 3558
		private TextWidget _widget_2_0_1;

		// Token: 0x04000DE7 RID: 3559
		private ImageWidget _widget_2_0_2;

		// Token: 0x04000DE8 RID: 3560
		private NavigatableListPanel _widget_3;

		// Token: 0x04000DE9 RID: 3561
		private EncyclopediaFilterGroupVM _datasource_Root;

		// Token: 0x04000DEA RID: 3562
		private MBBindingList<EncyclopediaListFilterVM> _datasource_Root_Filters;
	}
}
