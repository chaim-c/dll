﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.CharacterCreation;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.GauntletUI.GamepadNavigation;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x0200001F RID: 31
	public class CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_3_CharacterCreationGainedProperties__DependendPrefab : ListPanel
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x00048454 File Offset: 0x00046654
		public CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_3_CharacterCreationGainedProperties__DependendPrefab(UIContext context) : base(context)
		{
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00048460 File Offset: 0x00046660
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new NavigationScopeTargeter(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new NavigatableListPanel(base.Context);
			this._widget.AddChild(this._widget_1);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x000484B8 File Offset: 0x000466B8
		public void SetIds()
		{
			this._widget_1.Id = "GainedProperties";
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000484CC File Offset: 0x000466CC
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.CoverChildren;
			base.HeightSizePolicy = SizePolicy.CoverChildren;
			base.VerticalAlignment = VerticalAlignment.Center;
			base.MarginLeft = 20f;
			base.DoNotUseCustomScaleAndChildren = true;
			base.StackLayout.LayoutMethod = LayoutMethod.VerticalBottomToTop;
			this._widget_0.ScopeID = "GainedPropertiesScope";
			this._widget_0.ScopeParent = this._widget_1;
			this._widget_0.ScopeMovements = GamepadNavigationTypes.Horizontal;
			this._widget_0.AlternateScopeMovements = GamepadNavigationTypes.Vertical;
			this._widget_0.AlternateMovementStepSize = 3;
			this._widget_1.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.StackLayout.LayoutMethod = LayoutMethod.VerticalBottomToTop;
			this._widget_1.StepSize = 100;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00048588 File Offset: 0x00046788
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
				if (this._datasource_Root_GainGroups != null)
				{
					this._datasource_Root_GainGroups.ListChanged -= this.OnList_datasource_Root_GainGroupsChanged;
					for (int i = this._widget_1.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_1.GetChild(i);
						((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)child).OnBeforeRemovedChild(child);
						((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)this._widget_1.GetChild(i)).DestroyDataSource();
					}
					this._datasource_Root_GainGroups = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000486E3 File Offset: 0x000468E3
		public void SetDataSource(CharacterCreationGainedPropertiesVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x000486EC File Offset: 0x000468EC
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x000486FA File Offset: 0x000468FA
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x00048708 File Offset: 0x00046908
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00048716 File Offset: 0x00046916
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x00048724 File Offset: 0x00046924
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x00048732 File Offset: 0x00046932
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x00048740 File Offset: 0x00046940
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0004874E File Offset: 0x0004694E
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0004875C File Offset: 0x0004695C
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0004876A File Offset: 0x0004696A
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "GainGroups")
			{
				this.RefreshDataSource_datasource_Root_GainGroups(this._datasource_Root.GainGroups);
				return;
			}
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0004878C File Offset: 0x0004698C
		public void OnList_datasource_Root_GainGroupsChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				for (int i = this._widget_1.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_1.GetChild(i);
					((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_1.GetChild(i);
					((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)child2).SetDataSource(null);
					this._widget_1.RemoveChild(child2);
				}
				return;
			case ListChangedType.Sorted:
				for (int j = 0; j < this._datasource_Root_GainGroups.Count; j++)
				{
					CharacterCreationGainGroupItemVM bindingObject = this._datasource_Root_GainGroups[j];
					this._widget_1.FindChild((Widget widget) => widget.GetComponent<GeneratedWidgetData>().Data == bindingObject).SetSiblingIndex(j, false);
				}
				return;
			case ListChangedType.ItemAdded:
			{
				CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate = new CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate(base.Context);
				GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate);
				CharacterCreationGainGroupItemVM characterCreationGainGroupItemVM = this._datasource_Root_GainGroups[e.NewIndex];
				generatedWidgetData.Data = characterCreationGainGroupItemVM;
				characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.AddComponent(generatedWidgetData);
				this._widget_1.AddChildAtIndex(characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate, e.NewIndex);
				characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.CreateWidgets();
				characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.SetIds();
				characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.SetAttributes();
				characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.SetDataSource(characterCreationGainGroupItemVM);
				return;
			}
			case ListChangedType.ItemBeforeDeleted:
			{
				Widget child3 = this._widget_1.GetChild(e.NewIndex);
				((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)child3).OnBeforeRemovedChild(child3);
				return;
			}
			case ListChangedType.ItemDeleted:
			{
				Widget child4 = this._widget_1.GetChild(e.NewIndex);
				((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)child4).SetDataSource(null);
				this._widget_1.RemoveChild(child4);
				break;
			}
			case ListChangedType.ItemChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00048928 File Offset: 0x00046B28
		private void RefreshDataSource_datasource_Root(CharacterCreationGainedPropertiesVM newDataSource)
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
				if (this._datasource_Root_GainGroups != null)
				{
					this._datasource_Root_GainGroups.ListChanged -= this.OnList_datasource_Root_GainGroupsChanged;
					for (int i = this._widget_1.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_1.GetChild(i);
						((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)child).OnBeforeRemovedChild(child);
						Widget child2 = this._widget_1.GetChild(i);
						((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)child2).SetDataSource(null);
						this._widget_1.RemoveChild(child2);
					}
					this._datasource_Root_GainGroups = null;
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
				this._datasource_Root_GainGroups = this._datasource_Root.GainGroups;
				if (this._datasource_Root_GainGroups != null)
				{
					this._datasource_Root_GainGroups.ListChanged += this.OnList_datasource_Root_GainGroupsChanged;
					for (int j = 0; j < this._datasource_Root_GainGroups.Count; j++)
					{
						CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate = new CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate(base.Context);
						GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate);
						CharacterCreationGainGroupItemVM characterCreationGainGroupItemVM = this._datasource_Root_GainGroups[j];
						generatedWidgetData.Data = characterCreationGainGroupItemVM;
						characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.AddComponent(generatedWidgetData);
						this._widget_1.AddChildAtIndex(characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate, j);
						characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.CreateWidgets();
						characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.SetIds();
						characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.SetAttributes();
						characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.SetDataSource(characterCreationGainGroupItemVM);
					}
				}
			}
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00048C20 File Offset: 0x00046E20
		private void RefreshDataSource_datasource_Root_GainGroups(MBBindingList<CharacterCreationGainGroupItemVM> newDataSource)
		{
			if (this._datasource_Root_GainGroups != null)
			{
				this._datasource_Root_GainGroups.ListChanged -= this.OnList_datasource_Root_GainGroupsChanged;
				for (int i = this._widget_1.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_1.GetChild(i);
					((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_1.GetChild(i);
					((CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate)child2).SetDataSource(null);
					this._widget_1.RemoveChild(child2);
				}
				this._datasource_Root_GainGroups = null;
			}
			this._datasource_Root_GainGroups = newDataSource;
			this._datasource_Root_GainGroups = this._datasource_Root.GainGroups;
			if (this._datasource_Root_GainGroups != null)
			{
				this._datasource_Root_GainGroups.ListChanged += this.OnList_datasource_Root_GainGroupsChanged;
				for (int j = 0; j < this._datasource_Root_GainGroups.Count; j++)
				{
					CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate = new CharacterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate(base.Context);
					GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate);
					CharacterCreationGainGroupItemVM characterCreationGainGroupItemVM = this._datasource_Root_GainGroups[j];
					generatedWidgetData.Data = characterCreationGainGroupItemVM;
					characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.AddComponent(generatedWidgetData);
					this._widget_1.AddChildAtIndex(characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate, j);
					characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.CreateWidgets();
					characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.SetIds();
					characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.SetAttributes();
					characterCreationReviewStage__TaleWorlds_CampaignSystem_ViewModelCollection_CharacterCreation_CharacterCreationReviewStageVM_Dependency_4_ItemTemplate.SetDataSource(characterCreationGainGroupItemVM);
				}
			}
		}

		// Token: 0x040001DA RID: 474
		private ListPanel _widget;

		// Token: 0x040001DB RID: 475
		private NavigationScopeTargeter _widget_0;

		// Token: 0x040001DC RID: 476
		private NavigatableListPanel _widget_1;

		// Token: 0x040001DD RID: 477
		private CharacterCreationGainedPropertiesVM _datasource_Root;

		// Token: 0x040001DE RID: 478
		private MBBindingList<CharacterCreationGainGroupItemVM> _datasource_Root_GainGroups;
	}
}
