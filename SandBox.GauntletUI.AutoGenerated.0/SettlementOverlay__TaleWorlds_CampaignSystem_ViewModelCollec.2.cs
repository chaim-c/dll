﻿using System;
using System.ComponentModel;
using System.Numerics;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu.Overlay;
using TaleWorlds.CampaignSystem.ViewModelCollection.Quests;
using TaleWorlds.Core;
using TaleWorlds.GauntletUI;
using TaleWorlds.GauntletUI.BaseTypes;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.GauntletUI.Layout;
using TaleWorlds.Library;
using TaleWorlds.MountAndBlade.GauntletUI.Widgets;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x02000099 RID: 153
	public class SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_1_ItemTemplate : Widget
	{
		// Token: 0x060025C1 RID: 9665 RVA: 0x0011D236 File Offset: 0x0011B436
		public SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_1_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x0011D240 File Offset: 0x0011B440
		private VisualDefinition CreateVisualDefinitionTopPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("TopPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = -6f
			});
			visualDefinition.AddVisualState(new VisualState("Disabled")
			{
				PositionYOffset = -400f
			});
			return visualDefinition;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x0011D29C File Offset: 0x0011B49C
		private VisualDefinition CreateVisualDefinitionCharacterPartyExtension()
		{
			VisualDefinition visualDefinition = new VisualDefinition("CharacterPartyExtension", 0.9f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionYOffset = 78f
			});
			visualDefinition.AddVisualState(new VisualState("Disabled")
			{
				PositionYOffset = -400f
			});
			return visualDefinition;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0011D2F8 File Offset: 0x0011B4F8
		public void CreateWidgets()
		{
			this._widget = this;
			this._widget_0 = new ImageIdentifierWidget(base.Context);
			this._widget.AddChild(this._widget_0);
			this._widget_1 = new ListPanel(base.Context);
			this._widget.AddChild(this._widget_1);
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x0011D350 File Offset: 0x0011B550
		public void SetIds()
		{
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0011D354 File Offset: 0x0011B554
		public void SetAttributes()
		{
			base.WidthSizePolicy = SizePolicy.Fixed;
			base.HeightSizePolicy = SizePolicy.Fixed;
			base.SuggestedWidth = 45f;
			base.SuggestedHeight = 30f;
			base.VerticalAlignment = VerticalAlignment.Bottom;
			base.MarginLeft = 3f;
			base.MarginRight = 3f;
			base.MarginTop = 4f;
			base.MarginBottom = 15f;
			this._widget_0.WidthSizePolicy = SizePolicy.StretchToParent;
			this._widget_0.HeightSizePolicy = SizePolicy.StretchToParent;
			this._widget_1.WidthSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.HeightSizePolicy = SizePolicy.CoverChildren;
			this._widget_1.StackLayout.LayoutMethod = LayoutMethod.HorizontalLeftToRight;
			this._widget_1.HorizontalAlignment = HorizontalAlignment.Right;
			this._widget_1.VerticalAlignment = VerticalAlignment.Top;
			this._widget_1.IsEnabled = false;
			this._widget_1.DoNotAcceptEvents = true;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x0011D42C File Offset: 0x0011B62C
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
				if (this._datasource_Root_Visual != null)
				{
					this._datasource_Root_Visual.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
					this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
					this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
					this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
					this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
					this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
					this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
					this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
					this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
					this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
					this._datasource_Root_Visual = null;
				}
				if (this._datasource_Root_Quests != null)
				{
					this._datasource_Root_Quests.ListChanged -= this.OnList_datasource_Root_QuestsChanged;
					for (int i = this._widget_1.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_1.GetChild(i);
						((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)child).OnBeforeRemovedChild(child);
						((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)this._widget_1.GetChild(i)).DestroyDataSource();
					}
					this._datasource_Root_Quests = null;
				}
				this._datasource_Root = null;
			}
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0011D737 File Offset: 0x0011B937
		public void SetDataSource(GameMenuPartyItemVM dataSource)
		{
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0011D740 File Offset: 0x0011B940
		private void PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, object e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x0011D749 File Offset: 0x0011B949
		private void boolPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, bool e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x0011D752 File Offset: 0x0011B952
		private void floatPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, float e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x0011D75B File Offset: 0x0011B95B
		private void Vec2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vec2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x0011D764 File Offset: 0x0011B964
		private void Vector2PropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Vector2 e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025CE RID: 9678 RVA: 0x0011D76D File Offset: 0x0011B96D
		private void doublePropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, double e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025CF RID: 9679 RVA: 0x0011D776 File Offset: 0x0011B976
		private void intPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, int e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x0011D77F File Offset: 0x0011B97F
		private void uintPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, uint e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025D1 RID: 9681 RVA: 0x0011D788 File Offset: 0x0011B988
		private void ColorPropertyChangedListenerOf_widget_0(PropertyOwnerObject propertyOwnerObject, string propertyName, Color e)
		{
			this.HandleWidgetPropertyChangeOf_widget_0(propertyName);
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x0011D791 File Offset: 0x0011B991
		private void HandleWidgetPropertyChangeOf_widget_0(string propertyName)
		{
			if (propertyName == "AdditionalArgs")
			{
				return;
			}
			if (propertyName == "ImageId")
			{
				return;
			}
			propertyName == "ImageTypeCode";
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0011D7BB File Offset: 0x0011B9BB
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x0011D7C9 File Offset: 0x0011B9C9
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x0011D7D7 File Offset: 0x0011B9D7
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x0011D7E5 File Offset: 0x0011B9E5
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x0011D7F3 File Offset: 0x0011B9F3
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0011D801 File Offset: 0x0011BA01
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0011D80F File Offset: 0x0011BA0F
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0011D81D File Offset: 0x0011BA1D
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0011D82B File Offset: 0x0011BA2B
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0011D839 File Offset: 0x0011BA39
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
			if (propertyName == "Visual")
			{
				this.RefreshDataSource_datasource_Root_Visual(this._datasource_Root.Visual);
				return;
			}
			if (propertyName == "Quests")
			{
				this.RefreshDataSource_datasource_Root_Quests(this._datasource_Root.Quests);
				return;
			}
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0011D879 File Offset: 0x0011BA79
		private void ViewModelPropertyChangedListenerOf_datasource_Root_Visual(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x0011D887 File Offset: 0x0011BA87
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x0011D895 File Offset: 0x0011BA95
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x0011D8A3 File Offset: 0x0011BAA3
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x0011D8B1 File Offset: 0x0011BAB1
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0011D8BF File Offset: 0x0011BABF
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x0011D8CD File Offset: 0x0011BACD
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x060025E4 RID: 9700 RVA: 0x0011D8DB File Offset: 0x0011BADB
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x060025E5 RID: 9701 RVA: 0x0011D8E9 File Offset: 0x0011BAE9
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root_Visual(e.PropertyName);
		}

		// Token: 0x060025E6 RID: 9702 RVA: 0x0011D8F8 File Offset: 0x0011BAF8
		private void HandleViewModelPropertyChangeOf_datasource_Root_Visual(string propertyName)
		{
			if (propertyName == "AdditionalArgs")
			{
				this._widget_0.AdditionalArgs = this._datasource_Root_Visual.AdditionalArgs;
				return;
			}
			if (propertyName == "Id")
			{
				this._widget_0.ImageId = this._datasource_Root_Visual.Id;
				return;
			}
			if (propertyName == "ImageTypeCode")
			{
				this._widget_0.ImageTypeCode = this._datasource_Root_Visual.ImageTypeCode;
				return;
			}
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x0011D974 File Offset: 0x0011BB74
		public void OnList_datasource_Root_QuestsChanged(object sender, ListChangedEventArgs e)
		{
			switch (e.ListChangedType)
			{
			case ListChangedType.Reset:
				for (int i = this._widget_1.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_1.GetChild(i);
					((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_1.GetChild(i);
					((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)child2).SetDataSource(null);
					this._widget_1.RemoveChild(child2);
				}
				return;
			case ListChangedType.Sorted:
				for (int j = 0; j < this._datasource_Root_Quests.Count; j++)
				{
					QuestMarkerVM bindingObject = this._datasource_Root_Quests[j];
					this._widget_1.FindChild((Widget widget) => widget.GetComponent<GeneratedWidgetData>().Data == bindingObject).SetSiblingIndex(j, false);
				}
				return;
			case ListChangedType.ItemAdded:
			{
				SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate = new SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate(base.Context);
				GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate);
				QuestMarkerVM questMarkerVM = this._datasource_Root_Quests[e.NewIndex];
				generatedWidgetData.Data = questMarkerVM;
				settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.AddComponent(generatedWidgetData);
				this._widget_1.AddChildAtIndex(settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate, e.NewIndex);
				settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.CreateWidgets();
				settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.SetIds();
				settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.SetAttributes();
				settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.SetDataSource(questMarkerVM);
				return;
			}
			case ListChangedType.ItemBeforeDeleted:
			{
				Widget child3 = this._widget_1.GetChild(e.NewIndex);
				((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)child3).OnBeforeRemovedChild(child3);
				return;
			}
			case ListChangedType.ItemDeleted:
			{
				Widget child4 = this._widget_1.GetChild(e.NewIndex);
				((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)child4).SetDataSource(null);
				this._widget_1.RemoveChild(child4);
				break;
			}
			case ListChangedType.ItemChanged:
				break;
			default:
				return;
			}
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x0011DB10 File Offset: 0x0011BD10
		private void RefreshDataSource_datasource_Root(GameMenuPartyItemVM newDataSource)
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
				if (this._datasource_Root_Visual != null)
				{
					this._datasource_Root_Visual.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
					this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
					this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
					this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
					this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
					this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
					this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
					this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
					this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
					this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
					this._datasource_Root_Visual = null;
				}
				if (this._datasource_Root_Quests != null)
				{
					this._datasource_Root_Quests.ListChanged -= this.OnList_datasource_Root_QuestsChanged;
					for (int i = this._widget_1.ChildCount - 1; i >= 0; i--)
					{
						Widget child = this._widget_1.GetChild(i);
						((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)child).OnBeforeRemovedChild(child);
						Widget child2 = this._widget_1.GetChild(i);
						((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)child2).SetDataSource(null);
						this._widget_1.RemoveChild(child2);
					}
					this._datasource_Root_Quests = null;
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
				this._datasource_Root_Visual = this._datasource_Root.Visual;
				if (this._datasource_Root_Visual != null)
				{
					this._datasource_Root_Visual.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
					this._datasource_Root_Visual.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
					this._widget_0.AdditionalArgs = this._datasource_Root_Visual.AdditionalArgs;
					this._widget_0.ImageId = this._datasource_Root_Visual.Id;
					this._widget_0.ImageTypeCode = this._datasource_Root_Visual.ImageTypeCode;
					this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
					this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
					this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
					this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
					this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
					this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
					this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
					this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
					this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
				}
				this._datasource_Root_Quests = this._datasource_Root.Quests;
				if (this._datasource_Root_Quests != null)
				{
					this._datasource_Root_Quests.ListChanged += this.OnList_datasource_Root_QuestsChanged;
					for (int j = 0; j < this._datasource_Root_Quests.Count; j++)
					{
						SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate = new SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate(base.Context);
						GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate);
						QuestMarkerVM questMarkerVM = this._datasource_Root_Quests[j];
						generatedWidgetData.Data = questMarkerVM;
						settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.AddComponent(generatedWidgetData);
						this._widget_1.AddChildAtIndex(settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate, j);
						settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.CreateWidgets();
						settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.SetIds();
						settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.SetAttributes();
						settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.SetDataSource(questMarkerVM);
					}
				}
			}
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x0011E1B4 File Offset: 0x0011C3B4
		private void RefreshDataSource_datasource_Root_Visual(ImageIdentifierVM newDataSource)
		{
			if (this._datasource_Root_Visual != null)
			{
				this._datasource_Root_Visual.PropertyChanged -= this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithValue -= this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithBoolValue -= this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithIntValue -= this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithFloatValue -= this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithUIntValue -= this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithColorValue -= this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithDoubleValue -= this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithVec2Value -= this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
				this._widget_0.PropertyChanged -= this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged -= this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged -= this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged -= this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged -= this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged -= this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged -= this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged -= this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged -= this.ColorPropertyChangedListenerOf_widget_0;
				this._datasource_Root_Visual = null;
			}
			this._datasource_Root_Visual = newDataSource;
			this._datasource_Root_Visual = this._datasource_Root.Visual;
			if (this._datasource_Root_Visual != null)
			{
				this._datasource_Root_Visual.PropertyChanged += this.ViewModelPropertyChangedListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithValue += this.ViewModelPropertyChangedWithValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithBoolValue += this.ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithIntValue += this.ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithFloatValue += this.ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithUIntValue += this.ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithColorValue += this.ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithDoubleValue += this.ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root_Visual;
				this._datasource_Root_Visual.PropertyChangedWithVec2Value += this.ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root_Visual;
				this._widget_0.AdditionalArgs = this._datasource_Root_Visual.AdditionalArgs;
				this._widget_0.ImageId = this._datasource_Root_Visual.Id;
				this._widget_0.ImageTypeCode = this._datasource_Root_Visual.ImageTypeCode;
				this._widget_0.PropertyChanged += this.PropertyChangedListenerOf_widget_0;
				this._widget_0.boolPropertyChanged += this.boolPropertyChangedListenerOf_widget_0;
				this._widget_0.floatPropertyChanged += this.floatPropertyChangedListenerOf_widget_0;
				this._widget_0.Vec2PropertyChanged += this.Vec2PropertyChangedListenerOf_widget_0;
				this._widget_0.Vector2PropertyChanged += this.Vector2PropertyChangedListenerOf_widget_0;
				this._widget_0.doublePropertyChanged += this.doublePropertyChangedListenerOf_widget_0;
				this._widget_0.intPropertyChanged += this.intPropertyChangedListenerOf_widget_0;
				this._widget_0.uintPropertyChanged += this.uintPropertyChangedListenerOf_widget_0;
				this._widget_0.ColorPropertyChanged += this.ColorPropertyChangedListenerOf_widget_0;
			}
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x0011E574 File Offset: 0x0011C774
		private void RefreshDataSource_datasource_Root_Quests(MBBindingList<QuestMarkerVM> newDataSource)
		{
			if (this._datasource_Root_Quests != null)
			{
				this._datasource_Root_Quests.ListChanged -= this.OnList_datasource_Root_QuestsChanged;
				for (int i = this._widget_1.ChildCount - 1; i >= 0; i--)
				{
					Widget child = this._widget_1.GetChild(i);
					((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)child).OnBeforeRemovedChild(child);
					Widget child2 = this._widget_1.GetChild(i);
					((SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate)child2).SetDataSource(null);
					this._widget_1.RemoveChild(child2);
				}
				this._datasource_Root_Quests = null;
			}
			this._datasource_Root_Quests = newDataSource;
			this._datasource_Root_Quests = this._datasource_Root.Quests;
			if (this._datasource_Root_Quests != null)
			{
				this._datasource_Root_Quests.ListChanged += this.OnList_datasource_Root_QuestsChanged;
				for (int j = 0; j < this._datasource_Root_Quests.Count; j++)
				{
					SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate = new SettlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate(base.Context);
					GeneratedWidgetData generatedWidgetData = new GeneratedWidgetData(settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate);
					QuestMarkerVM questMarkerVM = this._datasource_Root_Quests[j];
					generatedWidgetData.Data = questMarkerVM;
					settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.AddComponent(generatedWidgetData);
					this._widget_1.AddChildAtIndex(settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate, j);
					settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.CreateWidgets();
					settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.SetIds();
					settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.SetAttributes();
					settlementOverlay__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_Overlay_SettlementMenuOverlayVM_Dependency_6_ItemTemplate.SetDataSource(questMarkerVM);
				}
			}
		}

		// Token: 0x040007AA RID: 1962
		private Widget _widget;

		// Token: 0x040007AB RID: 1963
		private ImageIdentifierWidget _widget_0;

		// Token: 0x040007AC RID: 1964
		private ListPanel _widget_1;

		// Token: 0x040007AD RID: 1965
		private GameMenuPartyItemVM _datasource_Root;

		// Token: 0x040007AE RID: 1966
		private ImageIdentifierVM _datasource_Root_Visual;

		// Token: 0x040007AF RID: 1967
		private MBBindingList<QuestMarkerVM> _datasource_Root_Quests;
	}
}
