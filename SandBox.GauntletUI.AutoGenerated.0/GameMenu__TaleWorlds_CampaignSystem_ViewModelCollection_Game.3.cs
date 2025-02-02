﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x0200006B RID: 107
	public class GameMenu__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_GameMenuVM_Dependency_2_ItemTemplate : GameMenu__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_GameMenuVM_Dependency_4_GameMenuItem__InheritedPrefab
	{
		// Token: 0x06001770 RID: 6000 RVA: 0x000ABA43 File Offset: 0x000A9C43
		public GameMenu__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_GameMenuVM_Dependency_2_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x000ABA4C File Offset: 0x000A9C4C
		private VisualDefinition CreateVisualDefinitionMenu()
		{
			VisualDefinition visualDefinition = new VisualDefinition("Menu", 0.65f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				PositionXOffset = 0f
			});
			visualDefinition.AddVisualState(new VisualState("Disabled")
			{
				PositionXOffset = -530f
			});
			return visualDefinition;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x000ABAA8 File Offset: 0x000A9CA8
		private VisualDefinition CreateVisualDefinitionInfoPanel()
		{
			VisualDefinition visualDefinition = new VisualDefinition("InfoPanel", 0.45f, 0f, true);
			visualDefinition.AddVisualState(new VisualState("Default")
			{
				SuggestedWidth = 900f,
				SuggestedHeight = 170f,
				PositionXOffset = 50f
			});
			visualDefinition.AddVisualState(new VisualState("Disabled")
			{
				SuggestedWidth = 900f,
				SuggestedHeight = 0f,
				PositionXOffset = 50f
			});
			return visualDefinition;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x000ABB30 File Offset: 0x000A9D30
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000ABB3F File Offset: 0x000A9D3F
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000ABB47 File Offset: 0x000A9D47
		public override void SetAttributes()
		{
			base.SetAttributes();
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000ABB50 File Offset: 0x000A9D50
		public override void DestroyDataSource()
		{
			base.DestroyDataSource();
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
				this._datasource_Root = null;
			}
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000ABC44 File Offset: 0x000A9E44
		public override void SetDataSource(GameMenuItemVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x000ABC54 File Offset: 0x000A9E54
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x000ABC62 File Offset: 0x000A9E62
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000ABC70 File Offset: 0x000A9E70
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x000ABC7E File Offset: 0x000A9E7E
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x000ABC8C File Offset: 0x000A9E8C
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x000ABC9A File Offset: 0x000A9E9A
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000ABCA8 File Offset: 0x000A9EA8
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x000ABCB6 File Offset: 0x000A9EB6
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x000ABCC4 File Offset: 0x000A9EC4
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000ABCD2 File Offset: 0x000A9ED2
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000ABCD4 File Offset: 0x000A9ED4
		private void RefreshDataSource_datasource_Root(GameMenuItemVM newDataSource)
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
			}
		}

		// Token: 0x040004FC RID: 1276
		private GameMenu__TaleWorlds_CampaignSystem_ViewModelCollection_GameMenu_GameMenuVM_Dependency_4_GameMenuItem__InheritedPrefab _widget;

		// Token: 0x040004FD RID: 1277
		private GameMenuItemVM _datasource_Root;
	}
}
