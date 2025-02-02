﻿using System;
using System.ComponentModel;
using TaleWorlds.CampaignSystem.ViewModelCollection.Encyclopedia.List;
using TaleWorlds.GauntletUI;
using TaleWorlds.Library;

namespace SandBox.GauntletUI.AutoGenerated1
{
	// Token: 0x020000DC RID: 220
	public class EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_1_ItemTemplate : EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_4_EncyclopediaFilterGroup__InheritedPrefab
	{
		// Token: 0x06003EB4 RID: 16052 RVA: 0x001F16B3 File Offset: 0x001EF8B3
		public EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_1_ItemTemplate(UIContext context) : base(context)
		{
		}

		// Token: 0x06003EB5 RID: 16053 RVA: 0x001F16BC File Offset: 0x001EF8BC
		public override void CreateWidgets()
		{
			base.CreateWidgets();
			this._widget = this;
		}

		// Token: 0x06003EB6 RID: 16054 RVA: 0x001F16CB File Offset: 0x001EF8CB
		public override void SetIds()
		{
			base.SetIds();
		}

		// Token: 0x06003EB7 RID: 16055 RVA: 0x001F16D3 File Offset: 0x001EF8D3
		public override void SetAttributes()
		{
			base.SetAttributes();
			base.MarginBottom = 30f;
		}

		// Token: 0x06003EB8 RID: 16056 RVA: 0x001F16E8 File Offset: 0x001EF8E8
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

		// Token: 0x06003EB9 RID: 16057 RVA: 0x001F17DC File Offset: 0x001EF9DC
		public override void SetDataSource(EncyclopediaFilterGroupVM dataSource)
		{
			base.SetDataSource(dataSource);
			this.RefreshDataSource_datasource_Root(dataSource);
		}

		// Token: 0x06003EBA RID: 16058 RVA: 0x001F17EC File Offset: 0x001EF9EC
		private void ViewModelPropertyChangedListenerOf_datasource_Root(object sender, PropertyChangedEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003EBB RID: 16059 RVA: 0x001F17FA File Offset: 0x001EF9FA
		private void ViewModelPropertyChangedWithValueListenerOf_datasource_Root(object sender, PropertyChangedWithValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003EBC RID: 16060 RVA: 0x001F1808 File Offset: 0x001EFA08
		private void ViewModelPropertyChangedWithBoolValueListenerOf_datasource_Root(object sender, PropertyChangedWithBoolValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003EBD RID: 16061 RVA: 0x001F1816 File Offset: 0x001EFA16
		private void ViewModelPropertyChangedWithIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003EBE RID: 16062 RVA: 0x001F1824 File Offset: 0x001EFA24
		private void ViewModelPropertyChangedWithFloatValueListenerOf_datasource_Root(object sender, PropertyChangedWithFloatValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003EBF RID: 16063 RVA: 0x001F1832 File Offset: 0x001EFA32
		private void ViewModelPropertyChangedWithUIntValueListenerOf_datasource_Root(object sender, PropertyChangedWithUIntValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003EC0 RID: 16064 RVA: 0x001F1840 File Offset: 0x001EFA40
		private void ViewModelPropertyChangedWithColorValueListenerOf_datasource_Root(object sender, PropertyChangedWithColorValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003EC1 RID: 16065 RVA: 0x001F184E File Offset: 0x001EFA4E
		private void ViewModelPropertyChangedWithDoubleValueListenerOf_datasource_Root(object sender, PropertyChangedWithDoubleValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003EC2 RID: 16066 RVA: 0x001F185C File Offset: 0x001EFA5C
		private void ViewModelPropertyChangedWithVec2ValueListenerOf_datasource_Root(object sender, PropertyChangedWithVec2ValueEventArgs e)
		{
			this.HandleViewModelPropertyChangeOf_datasource_Root(e.PropertyName);
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x001F186A File Offset: 0x001EFA6A
		private void HandleViewModelPropertyChangeOf_datasource_Root(string propertyName)
		{
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x001F186C File Offset: 0x001EFA6C
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

		// Token: 0x04000D09 RID: 3337
		private EncyclopediaItemList__TaleWorlds_CampaignSystem_ViewModelCollection_Encyclopedia_Pages_EncyclopediaFactionPageVM_Dependency_4_EncyclopediaFilterGroup__InheritedPrefab _widget;

		// Token: 0x04000D0A RID: 3338
		private EncyclopediaFilterGroupVM _datasource_Root;
	}
}
