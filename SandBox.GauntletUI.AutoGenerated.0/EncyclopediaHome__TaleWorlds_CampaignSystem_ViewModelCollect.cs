﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x02000043 RID: 67
	public class EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM : BrushWidget, IGeneratedGauntletMovieRoot
	{
		// Token: 0x060010BC RID: 4284 RVA: 0x000794B7 File Offset: 0x000776B7
		public EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM(UIContext context) : base(context)
		{
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000794C0 File Offset: 0x000776C0
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new Widget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_0_0 = new NavigationScopeTargeter(base.Context);
			this._widget_0.AddChild(this._widget_0_0);
			this._widget_0_1 = new NavigatableListPanel(base.Context);
			this._widget_0.AddChild(this._widget_0_1);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0007953A File Offset: 0x0007773A
		public void SetIds()
		{
			this._widget_0_1.Id = "EncyclopediaHomeList";
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0007954C File Offset: 0x0007774C
		public void SetAttributes()
		{
			base.HeightSizePolicy = SizePolicy.StretchToParent;
			base.WidthSizePolicy = SizePolicy.StretchToParent;
			base.DoNotAcceptEvents = true;
			base.Brush = base.Context.GetBrush("Encyclopedia.Page.SoundBrush");
			this._widget_0.HeightSizePolicy = SizePolicy.Fixed;
			this._widget_0.WidthSizePolicy = SizePolicy.Fixed;
			this._widget_0.SuggestedHeight = 762f;
			this._widget_0.SuggestedWidth = 1465f;
			this._widget_0.HorizontalAlignment = HorizontalAlignment.Center;
			this._widget_0.VerticalAlignment = VerticalAlignment.Center;
			this._widget_0.DoNotAcceptEvents = true;
			this._widget_0_0.ScopeID = "EncyclopediaHomeScope";
			this._widget_0_0.ScopeParent = this._widget_0_1;
			this._widget_0_0.ScopeMovements = GamepadNavigationTypes.Horizontal;
			this._widget_0_0.IsDefaultNavigationScope = true;
			this._widget_0_0.HasCircularMovement = false;
			this._widget_0_1.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_0_1.MarginLeft = 15f;
			this._widget_0_1.HorizontalAlignment = HorizontalAlignment.Left;
			this._widget_0_1.VerticalAlignment = VerticalAlignment.Center;
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x00079668 File Offset: 0x00077868
		public void RefreshBindingWithChildren()
		{
			EncyclopediaHomeVM datasource_Root = this._datasource_Root;
			this.SetDataSource(null);
			this.SetDataSource(datasource_Root);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x0007968C File Offset: 0x0007788C
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
				if (this._datasource_Root_Lists != null)
				{
					this._datasource_Root_Lists.ListChanged -= this.OnList_datasource_Root_ListsChanged;
					for (int i = this._widget_0_1.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_0_1.GetChild(i);
						((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
						((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)this._widget_0_1.GetChild(i)).DestroyDataSource();
					}
					this._datasource_Root_Lists = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000797E7 File Offset: 0x000779E7
		public void SetDataSource(EncyclopediaHomeVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000797F0 File Offset: 0x000779F0
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000797FE File Offset: 0x000779FE
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0007980C File Offset: 0x00077A0C
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x0007981A File Offset: 0x00077A1A
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00079828 File Offset: 0x00077A28
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00079836 File Offset: 0x00077A36
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x00079844 File Offset: 0x00077A44
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x00079852 File Offset: 0x00077A52
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x00079860 File Offset: 0x00077A60
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x0007986E File Offset: 0x00077A6E
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Lists")
			{
				this.RefreshDataSource_datasource_Root_Lists(this._datasource_Root.Lists);
				return;
			}
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x00079890 File Offset: 0x00077A90
		public void OnList_datasource_Root_ListsChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				for (int i = this._widget_0_1.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_0_1.GetChild(i);
					((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_0_1.GetChild(i);
					((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)child2).SetDataSource(null);
					this._widget_0_1.RemoveChild(child2);
				}
				return;
			case ListChangedType.Sorted:
				for (int j = 0; j < this._datasource_Root_Lists.Count; j++)
				{
					ListTypeVM bindingObject = this._datasource_Root_Lists[j];
					this._widget_0_1.FindChild((Widget widget) => widget.GetComponent<GeneratedWidgetData>().Data == bindingObject).SetSiblingIndex(j, false);
				}
				return;
			case ListChangedType.ItemAdded:
			{
				EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate = new EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate(base.Context);
				GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate);
				ListTypeVM listTypeVM = this._datasource_Root_Lists[e.NewIndex];
				generatedWidgetData.Data = listTypeVM;
				encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.AddComponent(generatedWidgetData);
				this._widget_0_1.AddChildAtIndex(encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate, e.NewIndex);
				encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.CreateWidgets();
				encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.SetIds();
				encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.SetAttributes();
				encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.SetDataSource(listTypeVM);
				return;
			}
			case ListChangedType.ItemBeforeDeleted:
			{
				Widget child3 = this._widget_0_1.GetChild(e.NewIndex);
				((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)child3).OnBeforeRemovedChild(child3);
				return;
			}
			case ListChangedType.ItemDeleted:
			{
				Widget child4 = this._widget_0_1.GetChild(e.NewIndex);
				((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)child4).SetDataSource(null);
				this._widget_0_1.RemoveChild(child4);
				break;
			}
			case ListChangedType.ItemChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x00079A2C File Offset: 0x00077C2C
		private void RefreshDataSource_datasource_Root(EncyclopediaHomeVM newDataSource)
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
				if (this._datasource_Root_Lists != null)
				{
					this._datasource_Root_Lists.ListChanged -= this.OnList_datasource_Root_ListsChanged;
					for (int i = this._widget_0_1.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_0_1.GetChild(i);
						((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
						Widget child2 = this._widget_0_1.GetChild(i);
						((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)child2).SetDataSource(null);
						this._widget_0_1.RemoveChild(child2);
					}
					this._datasource_Root_Lists = null;
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
				this._datasource_Root_Lists = this._datasource_Root.Lists;
				if (this._datasource_Root_Lists != null)
				{
					this._datasource_Root_Lists.ListChanged += this.OnList_datasource_Root_ListsChanged;
					for (int j = 0; j < this._datasource_Root_Lists.Count; j++)
					{
						EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate = new EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate(base.Context);
						GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate);
						ListTypeVM listTypeVM = this._datasource_Root_Lists[j];
						generatedWidgetData.Data = listTypeVM;
						encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.AddComponent(generatedWidgetData);
						this._widget_0_1.AddChildAtIndex(encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate, j);
						encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.CreateWidgets();
						encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.SetIds();
						encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.SetAttributes();
						encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.SetDataSource(listTypeVM);
					}
				}
			}
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x00079D24 File Offset: 0x00077F24
		private void RefreshDataSource_datasource_Root_Lists(MBBindingList<ListTypeVM> newDataSource)
		{
			if (this._datasource_Root_Lists != null)
			{
				this._datasource_Root_Lists.ListChanged -= this.OnList_datasource_Root_ListsChanged;
				for (int i = this._widget_0_1.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_0_1.GetChild(i);
					((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_0_1.GetChild(i);
					((EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate)child2).SetDataSource(null);
					this._widget_0_1.RemoveChild(child2);
				}
				this._datasource_Root_Lists = null;
			}
			this._datasource_Root_Lists = newDataSource;
			this._datasource_Root_Lists = this._datasource_Root.Lists;
			if (this._datasource_Root_Lists != null)
			{
				this._datasource_Root_Lists.ListChanged += this.OnList_datasource_Root_ListsChanged;
				for (int j = 0; j < this._datasource_Root_Lists.Count; j++)
				{
					EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate = new EncyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate(base.Context);
					GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate);
					ListTypeVM listTypeVM = this._datasource_Root_Lists[j];
					generatedWidgetData.Data = listTypeVM;
					encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.AddComponent(generatedWidgetData);
					this._widget_0_1.AddChildAtIndex(encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate, j);
					encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.CreateWidgets();
					encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.SetIds();
					encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.SetAttributes();
					encyclopediaHome__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_EncyclopediaHomeVM_Dependency_1_ItemTemplate.SetDataSource(listTypeVM);
				}
			}
		}

		// Token: 0x04000354 RID: 852
		private BrushWidget _widget;

		// Token: 0x04000355 RID: 853
		private Widget _widget_0;

		// Token: 0x04000356 RID: 854
		private NavigationScopeTargeter _widget_0_0;

		// Token: 0x04000357 RID: 855
		private NavigatableListPanel _widget_0_1;

		// Token: 0x04000358 RID: 856
		private EncyclopediaHomeVM _datasource_Root;

		// Token: 0x04000359 RID: 857
		private MBBindingList<ListTypeVM> _datasource_Root_Lists;
	}
}
