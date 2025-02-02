﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated0
{
	// Token: 0x0200004F RID: 79
	public class EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaConceptPageVM_Dependency_1_ItemTemplate : EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaConceptPageVM_Dependency_4_EncyclopediaFilterGroup__InheritedPrefab
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x0008A95F File Offset: 0x00088B5F
		public EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaConceptPageVM_Dependency_1_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0008A968 File Offset: 0x00088B68
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0008A977 File Offset: 0x00088B77
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0008A97F File Offset: 0x00088B7F
		public override void SetAttributes()
		{
			base.SetAttributes();
			base.MarginBottom = 30f;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0008A994 File Offset: 0x00088B94
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

		// Token: 0x060012F6 RID: 4854 RVA: 0x0008AA88 File Offset: 0x00088C88
		public override void SetDataSource(EncyclopediaFilterGroupVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0008AA98 File Offset: 0x00088C98
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0008AAA6 File Offset: 0x00088CA6
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0008AAB4 File Offset: 0x00088CB4
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x0008AAC2 File Offset: 0x00088CC2
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0008AAD0 File Offset: 0x00088CD0
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0008AADE File Offset: 0x00088CDE
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0008AAEC File Offset: 0x00088CEC
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0008AAFA File Offset: 0x00088CFA
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x0008AB08 File Offset: 0x00088D08
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x0008AB16 File Offset: 0x00088D16
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0008AB18 File Offset: 0x00088D18
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

		// Token: 0x040003E3 RID: 995
		private EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaConceptPageVM_Dependency_4_EncyclopediaFilterGroup__InheritedPrefab _widget;

		// Token: 0x040003E4 RID: 996
		private EncyclopediaFilterGroupVM _datasource_Root;
	}
}
